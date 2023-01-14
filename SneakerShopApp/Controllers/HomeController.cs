using DBRepo;
using Microsoft.AspNetCore.Mvc;
using SneakerShopApp.Models;
using System.Diagnostics;

namespace SneakerShopApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICart _cart;
        public HomeController(ILogger<HomeController> logger, ICart cart)
        {
            _logger = logger;
            _cart = cart;
        }

        public IActionResult Index()
        {
            ViewBag.CartItems = _cart.GetCartProducts(User.Identity.Name).ToList().Count();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}