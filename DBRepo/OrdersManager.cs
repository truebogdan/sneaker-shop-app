using DBRepo.Builder;
using DBRepo.Iter;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

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

        public void AddOrder(string customer, double totalCost, IEnumerable<CartProductModel> cartProducts, string name , string address, string phone)
        {
            Order order = new Order() { Customer = customer, Total = totalCost, IsCompleted = false , FullName = name , Address = address , Phone = phone};
            List<OrderProduct> orderProducts = new List<OrderProduct>();
            ConcreteCollection collection = new ConcreteCollection();
            Iterator iterator = collection.CreateIterator(cartProducts);
            //for (CartProductModel cart = iterator.First(); !iterator.IsCompleted; cart = iterator.Next())
            //{
            //    var ord = OrderProductBuilder.CreateNew().SetImgUrl(cart.ImgUrl).SetDescription(cart.Description).SetBrand(cart.Brand).SetPrice(cart.Price).SetOrder(order).Build();
            //    orderProducts.Add(ord);
            //}


            foreach (var product in cartProducts)
            {
                var ord = OrderProductBuilder.CreateNew().SetImgUrl(product.ImgUrl).SetDescription(product.Description).SetBrand(product.Brand).SetPrice(product.Price).SetOrder(order).Build();
                orderProducts.Add(ord);
                ord.Size= product.Size;
            }


            _context.Orders.Add(order);
            _context.OrderProducts.AddRange(orderProducts);
            _context.SaveChanges();

        }

        public void CompleteOrder(int orderId)
        {
            _context.Orders.Where(o => o.OrderId == orderId).First().IsCompleted = true;
            _context.SaveChanges();
        }

        public Dictionary<Order, List<OrderProduct>> GetAllOrders()
        {
            Dictionary<Order, List<OrderProduct>> result = new Dictionary<Order, List<OrderProduct>>();
            List<OrderProduct> ordersList = _context.OrderProducts.Join(_context.Orders, op => op.Order, o => o, (op, o) => new OrderProduct
            {
                Id = op.Id,
                ImgUrl = op.ImgUrl,
                Description = op.Description,
                Price = op.Price,
                Size = op.Size,
                Order = o
            }).ToList();


            foreach (var product in ordersList)
            {
                var products = result.GetValueOrDefault(product.Order);
                if(products is null)
                {
                     result.Add(product.Order, new List<OrderProduct> { product});

                }
                else
                {
                    products.Add(product);
                }
            }
            return result;
        }
    }
}
