using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRepo
{
    public interface IOrdersManager
    {   
        public void AddOrder(string customer, double totalCost, IEnumerable<CartProductModel> cartProducts);
    }
}
