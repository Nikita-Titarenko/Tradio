using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Tradio.Server.ValidationErrors;

namespace Tradio.Server.Attributes;

public class StringLengthWithCodeAttribute : StringLengthAttribute
{
    public const string ErrorCode = "STRING_LENGTH";

    public StringLengthWithCodeAttribute(int maximumLength) : base(maximumLength)
    {
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var result = base.IsValid(value, validationContext);
        if (result == ValidationResult.Success)
        {
            return ValidationResult.Success!;
        }

        var error = new BaseValidationError
        {
            Field = validationContext.MemberName?.ToLower() ?? string.Empty,
            Code = ErrorCode,
            Message = result?.ErrorMessage ?? string.Empty,
            AdditionalData = new Dictionary<string, string>
            {
                { "max", MaximumLength.ToString() },
                { "min", MinimumLength.ToString() }
            }
        };

        return new ValidationResult(
            JsonSerializer.Serialize(error),
            memberNames: result?.MemberNames
        );
    }
}