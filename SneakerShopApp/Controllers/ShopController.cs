﻿using DBRepo;
using ESRepo;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SneakerShopApp.Models;

namespace SneakerShopApp.Controllers
{
    public class ShopController : Controller
    {
        private readonly ISearchClient _esclient;
        private readonly ICart _cart;
        private readonly ILogger<ShopController> _logger;

        public ShopController(ISearchClient esclient, ICart cart, ILogger<ShopController> logger)
        {
            _esclient = esclient;
            _cart = cart;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var searchResult = _esclient.GetProducts();

            foreach(var product in searchResult.Products)
            {
                List<string> imagesList = JsonConvert.DeserializeObject<List<string>>(product.ImgUrl);

                product.ImgUrl = imagesList.FirstOrDefault();
            }
            var model = new ShopModel() { Products = searchResult.Products, Brands = searchResult.Brands, Checked = Array.Empty<string>(), SearchInput = "" };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddProductToCart(ProductModel product)
        {
            if (User.Identity.IsAuthenticated)
            {
                _cart.AddProduct(new CartProductModel { Customer = User.Identity.Name, Brand = product.Brand, Price = product.Price, Description = product.Description, ImgUrl = product.ImgUrl });
                var username = User.Identity.Name;
                _logger.LogWarning("User {username} just added the {brand} at {date} to his cart", username, product.Brand, DateTime.Now);
                return await ShowDetails(product.Guid.ToString());
            }
            else
                return Redirect("~/Identity/Account/Login");


        }

        [HttpPost]
        public async Task<IActionResult> ShowDetails(string guid)
        {
            var product = await _esclient.GetProductByGuid(guid);
            List<string> images = JsonConvert.DeserializeObject<List<string>>(product.ImgUrl);
            var model = new DetailsModel() { Product = product, Images = images };
            return View("Details",model);
        }

        public IActionResult Cart()
        {
            if (User.Identity.IsAuthenticated)
            {
                var products = _cart.GetCartProducts(User.Identity.Name);
                return View("Cart", new CartModel() { CartProducts = products, Total = ShoppingCart.TotalCost(products) });
            }
            else
                return Redirect("~/Identity/Account/Login");
        }

        [HttpPost]
        public IActionResult CheckoutCart(String customer)
        {
            if (User.Identity.IsAuthenticated)
            {
                var a = _cart.GetCartProducts(User.Identity.Name);
                return RedirectToAction("Checkout");
            }
            else
                return Redirect("~/Identity/Account/Login");
        }
        [HttpPost]
        public async Task<IActionResult> FilterAsync(string[] brands, string searchInput)
        {
            brands = brands.Where(brand => brand != "false").ToArray();
            var searchResult = await _esclient.Filter(searchInput, brands);
            foreach (var product in searchResult.Products)
            {
                List<string> imagesList = JsonConvert.DeserializeObject<List<string>>(product.ImgUrl);

                product.ImgUrl = imagesList.FirstOrDefault();
            }
            var model = new ShopModel() { Products = searchResult.Products, Brands = searchResult.Brands, Checked = brands, SearchInput = searchInput };
            return View("Index", model);
        }

        [HttpPost]

        public IActionResult Search(string searchInput)
        {
            var searchResult = _esclient.Search(searchInput);
            foreach (var product in searchResult.Products)
            {
                List<string> imagesList = JsonConvert.DeserializeObject<List<string>>(product.ImgUrl);

                product.ImgUrl = imagesList.FirstOrDefault();
            }
            var model = new ShopModel() { Products = searchResult.Products, Brands = searchResult.Brands, Checked = Array.Empty<string>(), SearchInput = searchInput };
            return View("Index", model);
        }
        [HttpPost]
        public IActionResult DeleteFromCart(int productId)
        {

            if (User.Identity.IsAuthenticated)
            {
                _cart.RemoveProduct(productId);
                return Cart();
            }
            else
                return Redirect("~/Identity/Account/Login");
        }

        [HttpGet]
        public JsonResult GetCartProducts()
        {
            if (User.Identity.IsAuthenticated)
            {
                var products = _cart.GetCartProducts(User.Identity.Name);
                return Json(products);
            }
            else
            {
                return null;
            }
        }
        public IActionResult Checkout()
        {
           return View("Checkout");
        }

        public IActionResult OrderConfirmation()
        {
            if (User.Identity.IsAuthenticated)
            {
                _cart.Checkout(User.Identity.Name);
                return View();
            }
            else
                return Redirect("~/Identity/Account/Login");
        }
    }
}
