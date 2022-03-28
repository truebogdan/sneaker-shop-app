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

        public ShoppingCart(DbShopContext context)
        {
            _context = context;
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
    }
}
