namespace Tradio.Domain
{
    public class Climate
    {
        public int Id { get; set; }

        public double Temperature { get; set; }

        public double Humidity { get; set; }

        public DateTime CreationDateTime { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}