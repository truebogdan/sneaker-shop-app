using DBRepo;
using ESRepo;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SneakerShopApp.Controllers
{
    public class AdminController : Controller
    {
        private FirebaseStorage _storage;
        private ISearchClient _esclient;
        private IOrdersManager _ordersManager;
        public AdminController(ISearchClient esclient , IOrdersManager ordersManager)
        {
            _storage = new FirebaseStorage("sneakershop-31c90.appspot.com");
            _esclient = esclient;
            _ordersManager = ordersManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CreateProduct(ProductModel product, List<IFormFile> files)
        {
            List<string> imagesUrls = new List<string>();
            foreach (var file in files)
            {
                var imageUrl = await _storage.Child(Guid.NewGuid() + file.FileName).PutAsync(file.OpenReadStream());
                imagesUrls.Add(imageUrl);
            }

            product.ImgUrl = JsonConvert.SerializeObject(imagesUrls);
            product.Guid = Guid.NewGuid();
            await _esclient.AddProduct(product);
            string[] response = { product.Guid.ToString(), imagesUrls.First() };
            return Json(response);
        }

        [HttpGet]
        public JsonResult GetAllProducts()
        {
            var searchResult = _esclient.GetProducts();

            foreach (var product in searchResult.Products)
            {
                List<string> imagesList = JsonConvert.DeserializeObject<List<string>>(product.ImgUrl);

                product.ImgUrl = imagesList.FirstOrDefault();
            }

            return new JsonResult(searchResult.Products);
        }

        [HttpPost]
        public async void UpdateProductPrice(string guid , double price)
        {
           await _esclient.UpdatePrice(guid, price);
        }

        [HttpPost]
        public async void DeleteProduct(string guid)
        {
            await _esclient.DeleteProduct(guid);
        }

        [HttpGet]
        public string GetAllOrders()
        {
            List<object> result = new List<object>();
            var orders = _ordersManager.GetAllOrders();
            foreach(var order in orders.Keys)
            {
                result.Add(new {
                    products = orders.GetValueOrDefault(order),
                    total = order.Total,
                    customer = order.Customer,
                    orderId = order.OrderId,
                    isCompleted = order.IsCompleted
                });
            }
            return JsonConvert.SerializeObject(result);
        }

        [HttpPost]
        public void CompleteOrder(int orderId)
        {
            _ordersManager.CompleteOrder(orderId);
        }
    }
}
