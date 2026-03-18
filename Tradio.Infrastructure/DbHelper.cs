using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tradio.Infrastructure;

public class DbHelper : IDbHelper
{
    private readonly ApplicationDbContext _context;

    public DbHelper(ApplicationDbContext context)
    {
        _context = context;
    }

public async Task<string> ExportAllDataToSqlAsync()
{
    var sqlBuilder = new StringBuilder();
    
    var ignoredTypes = new List<string> 
    { 
        "Categories", "Countries", "Cities", 
        "AspNetRoles", "AspNetUserRoles", "SubscriptionTypes"
    };
    
    var allEntities = _context.Model.GetEntityTypes()
        .Where(e => !ignoredTypes.Contains(e.ClrType.Name) && 
                    !ignoredTypes.Contains(e.GetTableName() ?? ""))
        .ToList();
    
    var sortedEntities = SortEntitiesByDependencies(allEntities);

    foreach (var entityType in sortedEntities)
    {
        var tableName = entityType.GetTableName();
        if (string.IsNullOrEmpty(tableName)) continue;

        var schema = entityType.GetSchema();
        var properties = entityType.GetProperties().Select(p => p.GetColumnName()).ToList();
        string fullTableName = string.IsNullOrEmpty(schema) ? $"[{tableName}]" : $"[{schema}].[{tableName}]";

        var query = _context.AsGenericQueryable(entityType.ClrType).AsNoTracking();
        var data = await query.ToListAsync();

        if (data.Any())
        {
            sqlBuilder.AppendLine($"-- Data for table {fullTableName}");
            
            var hasIdentity = entityType.GetProperties().Any(p => p.ValueGenerated == ValueGenerated.OnAdd);
            
            if (hasIdentity)
                sqlBuilder.AppendLine($"SET IDENTITY_INSERT {fullTableName} ON;");

            foreach (var item in data)
            {
                var values = new List<string>();
                foreach (var prop in entityType.GetProperties())
                {
                    var value = prop.PropertyInfo?.GetValue(item);
                    values.Add(FormatSqlValue(value));
                }

                sqlBuilder.AppendLine($"INSERT INTO {fullTableName} ({string.Join(", ", properties.Select(p => $"[{p}]"))}) VALUES ({string.Join(", ", values)});");
            }

            if (hasIdentity)
                sqlBuilder.AppendLine($"SET IDENTITY_INSERT {fullTableName} OFF;");
            
            sqlBuilder.AppendLine("GO\n");
        }
    }

    return sqlBuilder.ToString();
}

private List<IEntityType> SortEntitiesByDependencies(List<IEntityType> entities)
{
    var sorted = new List<IEntityType>();
    var visited = new HashSet<IEntityType>();

    foreach (var entity in entities)
    {
        Visit(entity, visited, sorted, entities);
    }

    return sorted;
}

private void Visit(IEntityType entity, HashSet<IEntityType> visited, List<IEntityType> sorted, List<IEntityType> allEntities)
{
    if (!visited.Contains(entity))
    {
        visited.Add(entity);
        
        var dependencies = entity.GetForeignKeys()
            .Select(fk => fk.PrincipalEntityType)
            .Where(t => t != entity && allEntities.Contains(t));

        foreach (var dep in dependencies)
        {
            Visit(dep, visited, sorted, allEntities);
        }

        sorted.Add(entity);
    }
}

public async Task ImportSqlDataAsync(string sqlContent)
{
    var commands = sqlContent.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

    foreach (var command in commands)
    {
        var cleanCommand = command.Trim();
        if (!string.IsNullOrWhiteSpace(cleanCommand))
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(cleanCommand);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

private string FormatSqlValue(object? value)
{
    if (value == null || value == DBNull.Value) return "NULL";
    if (value is string s) return $"N'{s.Replace("'", "''")}'";
    if (value is bool b) return b ? "1" : "0";
    if (value is DateTime dt) return $"'{dt:yyyy-MM-dd HH:mm:ss}'";
    if (value is Guid g) return $"'{g}'";
    return value.ToString()?.Replace(",", ".") ?? "NULL";
}
}