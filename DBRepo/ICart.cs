namespace DBRepo
{
    public interface ICart
    {
        public void AddProduct(CartProductModel cartProduct);
        public void RemoveProduct(int productId);
        public IEnumerable<CartProductModel> GetCartProducts(String customer);

        public void Checkout(String customer);

    }
}
