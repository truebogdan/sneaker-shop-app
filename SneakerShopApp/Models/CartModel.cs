using DBRepo;

namespace SneakerShopApp.Models
{
    public class CartModel
    {
        public  IEnumerable<CartProductModel> CartProducts{ get; set; }
        public  double Total { get; set; }  
    }
}
