namespace ESRepo
{
    public interface ISearchClient
    {
        public SearchResult GetProducts();
        public Task<SearchResult> Filter(string searchInput, string[] brands);
        public SearchResult Search(string searchInput);
        public Task AddProduct(ProductModel product);
        public Task<ProductModel> GetProductByGuid(string guid);
        public Task UpdatePrice(string guid, double price);
        public Task DeleteProduct(string guid);
    }
}
