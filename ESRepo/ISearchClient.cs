﻿namespace ESRepo
{
    public interface ISearchClient
    {
        public SearchResult GetProducts();
        public SearchResult Filter(string searchInput, string[] brands);
        public SearchResult Search(string searchInput);
    }
}
