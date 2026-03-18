namespace Tradio.Infrastructure;

public interface IDbHelper
{
    Task<string> ExportAllDataToSqlAsync();
    Task ImportSqlDataAsync(string sqlContent);
}