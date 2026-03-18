namespace Tradio.Application.Dtos.Payments;

public class PaymentDto
{
    public int Id { get; set; }

    public int ApplicationUserServiceId { get; set; }
    
    public string ServiceName { get; set; } = string.Empty;

    public int Price { get; set; }

    public DateTime CreationDateTime { get; set; }
    
    public bool AreYouProvider { get; set; }
}