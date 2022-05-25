using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRepo.Iter
{
    internal interface IAbstractIterator
    {
        CartProductModel First();
        CartProductModel Next();
        bool IsCompleted { get; }
    }
}
