public class PaymentDto
{
    public int Id { get; set; }
    public int ApplicationUserServiceId { get; set; }
    public int Price { get; set; }
    public DateTime CreationDateTime { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public string ToUserId { get; set; } = string.Empty;
}