using Microsoft.AspNetCore.Mvc;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using ESRepo;
using SneakerShopApp.Models;
using DBRepo;

namespace SneakerShopApp.Controllers
{
    public class ShopController : Controller
    {
        private readonly ISearchClient _esclient;
        private readonly ICart _cart;

        public ShopController(ISearchClient esclient, ICart cart)
        {
            _esclient = esclient;
            _cart = cart;
        }

        public IActionResult Index()
        {

            var productsList = _esclient.GetProducts();
            var model = new ShopModel() { Products=productsList};
            return View(model);
        }
        [HttpPost]
        public IActionResult AddProductToCart(ProductModel product)
        {
            if (User.Identity.IsAuthenticated)
            {
                _cart.AddProduct(new CartProductModel { Customer = User.Identity.Name, Brand = product.Brand, Price = product.Price, Description = product.Description, ImgUrl = product.ImgUrl });
                return RedirectToAction("Index");
            }
            else
                return Redirect("~/Identity/Account/Login");
           

        }

        public IActionResult Cart()
        {
            if (User.Identity.IsAuthenticated)
            {
                var products = _cart.GetCartProducts(User.Identity.Name);
                return View(new CartModel() { CartProducts = products ,Total=TotalCost(products)});
            }
            else
                return Redirect("~/Identity/Account/Login");
        }

        public double TotalCost(IEnumerable<CartProductModel> products)
        {   double total = 0;
            foreach(var product in products)
            {

              total+= Double.Parse(product.Price);
            }
            return total;
        }
    }
}
