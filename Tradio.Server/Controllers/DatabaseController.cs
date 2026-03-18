using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tradio.Application.Services.Database;
using Tradio.Server.Common;
using Tradio.Server.RequestsModel.Database;

namespace Tradio.Server.Controllers;

[Authorize(Roles = DefaultRoles.AdminRole)]
[ApiController]
[Route("api/[controller]")]
public class DatabaseController : ControllerBase
{
    private readonly IDatabaseService _databaseService;

    public DatabaseController(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    [HttpGet("export-inserts")]
    public async Task<IActionResult> ExportInserts()
    {
        var result = await _databaseService.ExportInsertsAsync();
        return File(result, "application/sql", $"tradio_full_dump_{DateTime.Now:yyyyMMdd}.sql");
    }
    
    [HttpPost("import-inserts")]
    public async Task<IActionResult> ImportInserts(ImportRequest request)
    {
        try 
        {
            await _databaseService.ImportBackupAsync(request.SqlScript);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}