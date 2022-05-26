namespace DBRepo.Iter
{
    internal class ConcreteCollection : AbstractCollection
    {
        private List<CartProductModel> orderList = new List<CartProductModel>();
        public Iterator CreateIterator(IEnumerable<CartProductModel> cartProducts)
        {
            return new Iterator(this);
        }
        public int Count
        {
            get { return orderList.Count; }
        }
        public void AddOrderProduct(CartProductModel orderProduct)
        {
            orderList.Add(orderProduct);
        }
        public CartProductModel GetOrderProduct(int IndexPosition)
        {
            return orderList[IndexPosition];
        }
    }
}
