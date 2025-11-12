using Tradio.Domain;

public class Payment
{
    public int Id { get; set; }

    public int ApplicationUserServiceId { get; set; }
    public ApplicationUserService ApplicationUserService { get; set; } = default!;

    public int Price { get; set; }

    public DateTime CreationDateTime { get; set; }
}