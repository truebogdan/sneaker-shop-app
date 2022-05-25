using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRepo.Iter
{
    internal class ConcreteCollection : AbstractCollection
    {
        private List<CartProductModel> orderList=new List<CartProductModel>();
        public Iterator CreateIterator(IEnumerable<CartProductModel> cartProducts)
        {
            return new Iterator(this);
        }
        public int Count
        {
            get { return orderList.Count; }
        }
        public void AddOrderProduct(CartProductModel orderProduct)
        {
            orderList.Add(orderProduct);
        }
        public CartProductModel GetOrderProduct(int IndexPosition)
        {
            return orderList[IndexPosition];
        }
    }
}
