namespace DBRepo
{
    public interface IOrdersManager
    {
        public void AddOrder(string customer, double totalCost, IEnumerable<CartProductModel> cartProducts , string name , string address , string phone);
        public Dictionary<Order, List<OrderProduct>> GetAllOrders();
        public void CompleteOrder(int orderId);
        public int GetOrdersCount();
        public int GetProductsSoldCount();
        public int GetCustomersCount();
        public double GetTotalEarnings();
    }
}
