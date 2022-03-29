using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRepo
{
    public class ShoppingCart : ICart
    {
        private readonly DbShopContext _context;
        private readonly IOrdersManager _ordersManager;

        public ShoppingCart(DbShopContext context , IOrdersManager ordersManager)
        {
            _context = context;
            _ordersManager = ordersManager;
        }

        public void AddProduct(CartProductModel cartProduct)
        {
            _context.Add(cartProduct);
            _context.SaveChanges(); 
        }

        public IEnumerable<CartProductModel> GetCartProducts(string customer)
        {
            return _context.CartProducts.Where(x => x.Customer == customer).ToList();
        }

        public void Checkout(string customer)
        {
            IEnumerable<CartProductModel> productsFromCart = _context.CartProducts.Where(_x => _x.Customer == customer).ToList();

            _context.CartProducts.RemoveRange(productsFromCart);

            _context.SaveChanges();

            _ordersManager.AddOrder(customer,TotalCost(productsFromCart),productsFromCart);

         }
        public static double TotalCost(IEnumerable<CartProductModel> products)
        {
            double total = 0;
            foreach (var product in products)
            {

                total += Double.Parse(product.Price);
            }
            return total;
        }
    }
}
