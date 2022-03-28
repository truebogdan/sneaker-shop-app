using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRepo
{
     public interface ICart
    {
        public void AddProduct(CartProductModel cartProduct);
        public IEnumerable<CartProductModel> GetCartProducts(String customer);     
    }
}
