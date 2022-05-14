﻿using Microsoft.AspNetCore.Mvc;
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

            var searchResult = _esclient.GetProducts();
            var model = new ShopModel() { Products = searchResult.Products, Brands = searchResult.Brands, Checked = Array.Empty<string>() , SearchInput="" };
            return View(model);
        }
        [HttpPost]
        public IActionResult AddProductToCart(ProductModel product , string searchInput, string[] brands)
        {
            if (User.Identity.IsAuthenticated)
            {
                _cart.AddProduct(new CartProductModel { Customer = User.Identity.Name, Brand = product.Brand, Price = product.Price, Description = product.Description, ImgUrl = product.ImgUrl });
                return Filter(brands,searchInput);
            }
            else
                return Redirect("~/Identity/Account/Login");
           

        }

        public IActionResult Cart()
        {
            if (User.Identity.IsAuthenticated)
            {
                var products = _cart.GetCartProducts(User.Identity.Name);
                return View(new CartModel() { CartProducts = products ,Total = ShoppingCart.TotalCost(products)});
            }
            else
                return Redirect("~/Identity/Account/Login");
        }

        [HttpPost]
        public IActionResult CheckoutCart(String customer)
        {
            if (User.Identity.IsAuthenticated)
            {
                _cart.Checkout(customer);
                return RedirectToAction("Index");
            }
            else
                return Redirect("~/Identity/Account/Login");
        }
        [HttpPost]
        public IActionResult Filter(string[] brands, string searchInput)
        {
            brands = brands.Where(brand => brand != "false").ToArray();
            var searchResult = _esclient.Filter(searchInput,brands);
            var model = new ShopModel() { Products = searchResult.Products, Brands = searchResult.Brands , Checked=brands , SearchInput=searchInput};
            return View("Index",model);
        }

        [HttpPost]

        public IActionResult Search(string searchInput)
        {
            var searchResult = _esclient.Search(searchInput);
            var model = new ShopModel() { Products = searchResult.Products, Brands = searchResult.Brands, Checked = Array.Empty<string>() , SearchInput=searchInput};
            return View("Index",model);
        }
    }
}
