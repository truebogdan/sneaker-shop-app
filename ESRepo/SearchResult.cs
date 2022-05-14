using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESRepo
{
    public class SearchResult
    {
        public  IEnumerable<ProductModel>  Products{ get; set; }
        public Dictionary<string,long> Brands { get; set; }
    }
}
