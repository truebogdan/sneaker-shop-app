using Microsoft.AspNetCore.Mvc;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using ESRepo;
using SneakerShopApp.Models;

namespace SneakerShopApp.Controllers
{
    public class ShopController : Controller
    {
        private readonly ISearchClient _esclient;

        public ShopController(ISearchClient esclient)
        {
            _esclient = esclient;
        }

        public IActionResult Index()
        {

            var productsList = _esclient.GetProducts();

            var model = new ShopModel() { Products=productsList};

            return View(model);
        }
    }
}
