using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRepo.Iter
{
    internal interface AbstractCollection
    {
        Iterator CreateIterator(IEnumerable<CartProductModel> cartProducts);
    }
}
