
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

        public SearchResult Filter(string searchInput, string[] brands)
        {
            var response = client.Search<ProductModel>(s =>
               s.Index("sneakers-index").Size(2000).Query(q =>
                   q.Bool(b =>
                     b.Must(m => m.Match(m =>
                         m.Field(f =>
                            f.Description).Query(searchInput)), m =>
                      m.Terms(ts => ts.Field(f => f.Brand.Suffix("keyword")).Terms(brands))))));

            var aggregation = client.Search<ProductModel>(s =>
               s.Index("sneakers-index").Size(0).Query(q =>
                     q.Match(m =>
                        m.Field(f =>
                           f.Description).Query(searchInput))).Aggregations(aggs =>
                    aggs.Terms("brands", t =>
                       t.Field(f =>
                          f.Brand.Suffix("keyword")))));
            return new SearchResult() { Products = response.Documents, Brands = ConvertBucketsToDictionary(aggregation.Aggregations.Terms("brands").Buckets) };
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
