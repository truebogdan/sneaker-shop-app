
using Microsoft.Extensions.Configuration;
using Nest;

namespace ESRepo
{
    public class ESClient : ISearchClient
    {
        private readonly ElasticClient client;
        public ESClient(IConfiguration configuration)
        {
            client = new ElasticClient(new Uri(configuration.GetConnectionString("ElasticsearchConnection")));
        }

        public async Task AddProduct(ProductModel product)
        {
            await client.CreateAsync<ProductModel>(product, a => a.Index("sneakers-index").Id(product.Guid));
        }

        public async Task DeleteProduct(string guid)
        {
            await client.DeleteAsync<ProductModel>(guid , d => d.Index("sneakers-index"));
        }

        public async Task<SearchResult> Filter(string searchInput, string[] brands)
        {
            var response = await client.SearchAsync<ProductModel>(s =>
               s.Index("sneakers-index").Size(2000).Query(q =>
                   q.Bool(b =>
                     b.Must(m => m.Match(m =>
                         m.Field(f =>
                            f.Description).Query(searchInput)), m =>
                      m.Terms(ts => ts.Field(f => f.Brand.Suffix("keyword")).Terms(brands))))));

            var aggregation = await client.SearchAsync<ProductModel>(s =>
               s.Index("sneakers-index").Size(0).Query(q =>
                     q.Match(m =>
                        m.Field(f =>
                           f.Description).Query(searchInput))).Aggregations(aggs =>
                    aggs.Terms("brands", t =>
                       t.Field(f =>
                          f.Brand.Suffix("keyword")))));

            return new SearchResult() { Products = response.Documents, Brands = ConvertBucketsToDictionary(aggregation.Aggregations.Terms("brands").Buckets) };
        }

        public async Task<ProductModel> GetProductByGuid(string guid)
        {
           var response = await client.SearchAsync<ProductModel>(s => s.Index("sneakers-index").Size(2000).Query(q=>
                q.Match( m => m.Field(f=> 
                    f.Guid.Suffix("keyword")).Query(guid))
                ));
            return response.Documents.FirstOrDefault();
        }

        public SearchResult GetProducts()
        {

            var response = client.Search<ProductModel>(s =>
               s.Index("sneakers-index").Size(2000).Query(q =>
                   q.MatchAll()).Aggregations(aggs =>
                    aggs.Terms("brands", t =>
                       t.Field(f =>
                          f.Brand.Suffix("keyword")))));

            return new SearchResult() { Products = response.Documents, Brands = ConvertBucketsToDictionary(response.Aggregations.Terms("brands").Buckets) };
        }

        public SearchResult Search(string searchInput)
        {
            var response = client.Search<ProductModel>(s =>
                s.Index("sneakers-index").Size(2000).Query(q =>
                    q.Match(m =>
                        m.Field(f =>
                           f.Description).Query(searchInput))).Aggregations(aggs =>
                     aggs.Terms("brands", t =>
                        t.Field(f =>
                           f.Brand.Suffix("keyword")))));

            return new SearchResult() { Products = response.Documents, Brands = ConvertBucketsToDictionary(response.Aggregations.Terms("brands").Buckets) };
        }

        public async Task UpdatePrice(string guid, double price)
        {
            var product = await GetProductByGuid(guid);
            product.Price = price.ToString();
            var partialUpdateResponse = await client.UpdateAsync<ProductModel, object>(guid, u => u.Doc(product).Index("sneakers-index"));
        }

        private Dictionary<string, long> ConvertBucketsToDictionary(IReadOnlyCollection<KeyedBucket<string>> buckets)
        {
            Dictionary<string, long> aggregation = new Dictionary<string, long>();
            foreach (KeyedBucket<string> bucket in buckets)
            {
                aggregation.Add(bucket.Key, (long)bucket.DocCount);
            }
            return aggregation;
        }
    }
}
