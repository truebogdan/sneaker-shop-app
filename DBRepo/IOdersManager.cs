namespace DBRepo
{
    public interface IOrdersManager
    {
        public void AddOrder(string customer, double totalCost, IEnumerable<CartProductModel> cartProducts);
        public Dictionary<Order, List<OrderProduct>> GetAllOrders();
        public void CompleteOrder(int orderId);
    }
}
