using Tradio.Server.Attributes;

namespace Tradio.Server.RequestsModel.Services
{
    public class CreateServiceRequestModel
    {
        [StringLengthWithCode(200, MinimumLength = 5)]
        public string Name { get; set; } = string.Empty;

        [StringLengthWithCode(1000, MinimumLength = 5)]
        public string Description { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public int Price { get; set; }

        public bool IsVisible { get; set; }
    }
}
