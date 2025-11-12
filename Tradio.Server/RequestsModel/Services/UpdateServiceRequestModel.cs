using System.ComponentModel.DataAnnotations;

namespace Tradio.Server.RequestsModel.Services
{
    public class UpdateServiceRequestModel
    {
        public int Id { get; set; }

        [StringLength(200, MinimumLength = 5)]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000, MinimumLength = 5)]
        public string Description { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public int Price { get; set; }

        public bool IsVisible { get; set; }
    }
}
