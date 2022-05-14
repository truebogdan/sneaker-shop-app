using ESRepo;

namespace SneakerShopApp.Models
{
    public class ShopModel
    {

        public IEnumerable<ProductModel>? Products { get; set; }
        public Dictionary<string, long>? Brands { get; set; }
        public string[]? Checked { get; set; }
        public string? SearchInput { get; set; }
    }
}
