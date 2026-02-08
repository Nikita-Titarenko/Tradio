namespace Tradio.Server.ValidationErrors;

public class BaseValidationError
{
    public string Code { get; set; } = string.Empty;

    public string Field { get; set; } = string.Empty;
    
    public string? Message { get; set; }

    public Dictionary<string, string>? AdditionalData { get; set; }
}
