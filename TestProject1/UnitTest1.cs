using DBRepo;
namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TotalCost()
        {
            CartProductModel product1 = new CartProductModel();
            product1.Price = "230";
            CartProductModel product2 = new CartProductModel();
            product2.Price = "240";
            List<CartProductModel> productslist = new List<CartProductModel>();
            productslist.Add(product1);
            productslist.Add(product2);


            Assert.AreEqual(470, ShoppingCart.TotalCost(productslist));
        }

        [TestMethod]

        public void CheckIfListIsNull()
        {

            List<CartProductModel> productslist = new List<CartProductModel>();

            Assert.AreEqual(0, ShoppingCart.TotalCost(productslist));
        }

        
    }
}