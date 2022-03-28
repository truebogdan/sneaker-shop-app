
using Microsoft.Extensions.Configuration;
using Nest;

namespace ESRepo
{
    public class ESClient:ISearchClient
    {
        private readonly ElasticClient client;
        public ESClient(IConfiguration configuration)
        {
            client = new ElasticClient(new Uri(configuration.GetConnectionString("ElasticsearchConnection")));
        }

        public IEnumerable<ProductModel> GetProducts()
        {
            var response = client.Search<ProductModel>( s=> s.Index("sneakers-index"));
            return response.Documents.ToList();
        }
    }
}
