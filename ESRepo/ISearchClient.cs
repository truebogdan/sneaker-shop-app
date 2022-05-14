using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESRepo
{
    public interface ISearchClient
    {
        public SearchResult GetProducts();
        public SearchResult Filter(string searchInput,string[] brands);
        public SearchResult Search(string searchInput);
    }
}
