using DBRepo.Builder;

namespace DBRepo
{
    public class OrdersManager : IOrdersManager
    {


        private readonly DbShopContext _context;

        public OrdersManager(DbShopContext context)
        {
            _context = context;
        }

        public void AddOrder(string customer, double totalCost, IEnumerable<CartProductModel> cartProducts)
        {
            Order order = new Order() { Customer = customer, Total = totalCost };
            List<OrderProduct> orderProducts = new List<OrderProduct>();
            foreach (var product in cartProducts)
            {
                var ord = OrderProductBuilder.CreateNew().SetImgUrl(product.ImgUrl).SetDescription(product.Description).SetBrand(product.Brand).SetPrice(product.Price).SetOrder(order).Build();
                orderProducts.Add(ord);
            }
            _context.Orders.Add(order);
            _context.OrderProducts.AddRange(orderProducts);
            _context.SaveChanges();

        }
    }
}
