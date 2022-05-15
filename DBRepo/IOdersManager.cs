namespace DBRepo
{
    public interface IOrdersManager
    {
        public void AddOrder(string customer, double totalCost, IEnumerable<CartProductModel> cartProducts);
    }
}
