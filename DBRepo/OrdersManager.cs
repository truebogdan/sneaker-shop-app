using DBRepo.Builder;
using DBRepo.Iter;
using Microsoft.Extensions.Logging;

namespace DBRepo
{
    public class OrdersManager : IOrdersManager
    {


        private readonly DbShopContext _context;
        private readonly ILogger<OrdersManager> _logger;
        public OrdersManager(DbShopContext context, ILogger<OrdersManager> logger)
        {
            _context = context;
            _logger = logger;   
        }

        public void AddOrder(string customer, double totalCost, IEnumerable<CartProductModel> cartProducts)
        {
            Order order = new Order() { Customer = customer, Total = totalCost };
            List<OrderProduct> orderProducts = new List<OrderProduct>();
            ConcreteCollection collection = new ConcreteCollection();
            Iterator iterator = collection.CreateIterator(cartProducts);
            for (CartProductModel cart = iterator.First(); !iterator.IsCompleted; cart = iterator.Next())
            {
                var ord = OrderProductBuilder.CreateNew().SetImgUrl(cart.ImgUrl).SetDescription(cart.Description).SetBrand(cart.Brand).SetPrice(cart.Price).SetOrder(order).Build();
                orderProducts.Add(ord);
            }
            //foreach (var product in cartProducts)
            //{
            //    var ord = OrderProductBuilder.CreateNew().SetImgUrl(product.ImgUrl).SetDescription(product.Description).SetBrand(product.Brand).SetPrice(product.Price).SetOrder(order).Build();
            //    orderProducts.Add(ord);
            //}
            _context.Orders.Add(order);
            _context.OrderProducts.AddRange(orderProducts);
            _context.SaveChanges();

        }
    }
}
