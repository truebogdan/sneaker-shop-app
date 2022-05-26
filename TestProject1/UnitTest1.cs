using DBRepo;
namespace TestProject1
{
    [TestClass]
    public class DBRepoTest
    {
        // private readonly DbShopContext _contextut;
        // private readonly Mock<IOrdersManager> _loggermock = new Mock<IOrdersManager>();

        [TestMethod]
        [DataRow("230", "470", 700)]
        [DataRow("2", "5", 7)]
        [DataRow("1200", "1400", 2600)]
        [DataRow("0", "45", 45)]
        [DataRow("100", "470", 570)]
        [DataRow("0", "0", 0)]
        [DataRow("-23.45", "-53.54", 0)]
        [DataRow("1200.1", "1200.2", 2400.3)]
        [DataRow("-230", "-1", 0)]
        [DataRow("0", "34", 34)]
        

        public void TotalCost(string a, string b, double c)
        {
            CartProductModel product1 = new CartProductModel();
            product1.Price = a;
            CartProductModel product2 = new CartProductModel();
            product2.Price = b;
            var productslist = new List<CartProductModel>();
            productslist.Add(product1);
            productslist.Add(product2);


            Assert.AreEqual(c, ShoppingCart.TotalCost(productslist));
        }

        [TestMethod]
        [DataRow("-242", "-1", 0)]
        [DataRow("0", "-332", 0)]
        [DataRow("-2000", "-1200", 0)]
        public void TotalCostNegativePrice(string a, string b, double c)
        {
            CartProductModel product1 = new CartProductModel();
            product1.Price = a;
            CartProductModel product2 = new CartProductModel();
            product2.Price = b;
            var productslist = new List<CartProductModel>();
            productslist.Add(product1);
            productslist.Add(product2);


            Assert.AreEqual(c, ShoppingCart.TotalCost(productslist));
        }

        [TestMethod]
        [DataRow("-242.23", "-1.21", 0)]
        [DataRow("0", "332.2", 332.2)]
        [DataRow("2000.3", "1200.3", 3200.6)]
        public void TotalCostDoublePrice(string a, string b, double c)
        {
            CartProductModel product1 = new CartProductModel();
            product1.Price = a;
            CartProductModel product2 = new CartProductModel();
            product2.Price = b;
            var productslist = new List<CartProductModel>();
            productslist.Add(product1);
            productslist.Add(product2);


            Assert.AreEqual(c, ShoppingCart.TotalCost(productslist));
        }

        [TestMethod]

        public void TestTotalCostWithEmptyList()
        {

            var productslist = new List<CartProductModel>();

            Assert.AreEqual(0, ShoppingCart.TotalCost(productslist));
        }

        [TestMethod]

        public void TestTotalCostWithNullList()
        {

            List<CartProductModel> productlist = null;

            Assert.AreEqual(0, ShoppingCart.TotalCost(productlist));
        }

        [TestMethod]

        public void CheckIfProductInListIsNull()
        {
            CartProductModel product1 = null;
            var productslist = new List<CartProductModel>();
            productslist.Add(product1);

            Assert.AreEqual(0, ShoppingCart.TotalCost(productslist));

        }

        [TestMethod]
        public void CheckIfPriceProductsInListIs0()
        {
            CartProductModel product1 = new CartProductModel();
            product1.Price = "-230";
            var productslist = new List<CartProductModel>();
            productslist.Add(product1);

            Assert.AreEqual(0, ShoppingCart.TotalCost(productslist));
        }




    }
}