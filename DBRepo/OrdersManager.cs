using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRepo
{
    public class OrdersManager : IOrdersManager
    {


        private readonly DbShopContext _context;

        public OrdersManager(DbShopContext context)
        {
            _context = context;
        }

        public void AddOrder( string customer,double totalCost, IEnumerable<CartProductModel> cartProducts)
        {
            Order order = new Order() { Customer = customer, Total = totalCost };
            List<OrderProduct> orderProducts = new List<OrderProduct>();
            foreach(var product in cartProducts)
            {
                orderProducts.Add(new OrderProduct() { Description = product.Description, Price = product.Price, Brand = product.Brand, ImgUrl = product.ImgUrl, Order = order });
            }
            _context.Orders.Add(order);
            _context.OrderProducts.AddRange(orderProducts);
            _context.SaveChanges();

        }
    }
}
