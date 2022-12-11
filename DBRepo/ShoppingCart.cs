namespace DBRepo
{
    public class ShoppingCart : ICart
    {
        private readonly DbShopContext _context;
        private readonly IOrdersManager _ordersManager;

        public ShoppingCart(DbShopContext context, IOrdersManager ordersManager)
        {
            _context = context;
            _ordersManager = ordersManager;
        }

        public void AddProduct(CartProductModel cartProduct)
        {
            _context.CartProducts.Add(cartProduct);
            _context.SaveChanges();
        }

        public IEnumerable<CartProductModel> GetCartProducts(string customer)
        {
            return _context.CartProducts.Where(x => x.Customer == customer).ToList();
        }

        public void Checkout(string customer , string name , string address , string phone)
        {
            IEnumerable<CartProductModel> productsFromCart = GetCartProducts(customer);

            _context.CartProducts.RemoveRange(productsFromCart);

            _context.SaveChanges();

            _ordersManager.AddOrder(customer, TotalCost(productsFromCart), productsFromCart, name , address,phone);

        }

        public static double TotalCost(IEnumerable<CartProductModel> products)
        {
            if (products == null)
            {
                return 0;
            }
            double total = 0;
            foreach (var product in products)
            {
                if(product == null || product.Price.Contains("-") )
                {
                    continue;
                }
                total += Double.Parse(product.Price);
            }
            return total;
        }

        public void RemoveProduct(int productId)
        {
            var productToRemove = _context.CartProducts.SingleOrDefault(p => p.CartProductModelId == productId);

            _context.CartProducts.Remove(productToRemove);
            _context.SaveChanges();
        }
    }
}
