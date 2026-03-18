using System.Text;
using Tradio.Infrastructure;

namespace Tradio.Application.Services.Database;

public class DatabaseService : IDatabaseService
{
    private readonly IDbHelper _dbHelper;

    public DatabaseService(IDbHelper dbHelper)
    {
        _dbHelper = dbHelper;
    }

    public async Task<byte[]> ExportInsertsAsync()
    {
        var sql = await _dbHelper.ExportAllDataToSqlAsync();
        return Encoding.UTF8.GetBytes(sql);
    }
    
    public async Task ImportBackupAsync(string sqlScript)
    {
        if (string.IsNullOrWhiteSpace(sqlScript))
            throw new ArgumentException("Script is empty.");

        await _dbHelper.ImportSqlDataAsync(sqlScript);
    }
}