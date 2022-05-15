namespace ESRepo
{
    public class SearchResult
    {
        public IEnumerable<ProductModel> Products { get; set; }
        public Dictionary<string, long> Brands { get; set; }
    }
}
