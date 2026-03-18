namespace Tradio.Application.Services.Database;

public interface IDatabaseService
{
    Task<byte[]> ExportInsertsAsync();
    Task ImportBackupAsync(string sqlScript);
}