using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRepo.Iter
{
     class Iterator : IAbstractIterator
    {
        private ConcreteCollection collection;
        private int index=0;
        private int step = 1;

        public Iterator(ConcreteCollection collection)
        {
            this.collection = collection;
        }

        public bool IsCompleted
        {
            get { return index >= collection.Count-1; }
        }

        public CartProductModel First()
        {
            index = 0;
            if(collection.Count > 0)
            return collection.GetOrderProduct(index);
            return Next();
        }

        public CartProductModel Next()
        {
            index += step;
            if (!IsCompleted)
            {
                return collection.GetOrderProduct(index);
            }
            else
            {
                return null;
            }
        }
    }
}
