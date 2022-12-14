using NUnit.Framework;

using OpenQA.Selenium;

using OpenQA.Selenium.Chrome;

using OpenQA.Selenium.Support.UI;


namespace SeleniumTesting
{

    public class Tests
    {

        IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            driver = new ChromeDriver(path + @"\drivers\");

        }

        [Test]

        public void FullSequence()
        {
            //maximaze window
            driver.Manage().Window.Maximize();

            //ignore google chrome's safety recomandation
            driver.Navigate().GoToUrl("https://localhost:7249");  //open the application

            Thread.Sleep(1000);


            //click on advanced button
            driver.FindElement(By.XPath("//button[@id='details-button']")).Click();

            Thread.Sleep(1000);

            //click on 
            driver.FindElement(By.XPath("//*[@id='proceed-link']")).Click();

            Thread.Sleep(1500);

            //get on login page
            driver.FindElement(By.XPath("//a[contains(text(), 'Login')]")).Click();
            
            Thread.Sleep(500);

            //wrong username and password log in test
            driver.FindElement(By.XPath("//*[@id='Input_Email']")).SendKeys("andrei@yahoo.com");
            driver.FindElement(By.XPath("//*[@id='Input_Password']")).SendKeys("andrei");

            
            driver.FindElement(By.XPath("//button[contains(text(), 'Log in')]")).Click(); //click on log in button

            Thread.Sleep(500);

            Assert.IsTrue(driver.FindElement(By.XPath("//*[contains(text(), 'Invalid login attempt')]")).Displayed); //check if error message appears

            driver.FindElement(By.XPath("//*[@id='Input_Email']")).Clear(); //clear email input
            driver.FindElement(By.XPath("//*[@id='Input_Password']")).Clear(); //clear password input

            //correct username and password log in test
            driver.FindElement(By.XPath("//*[@id='Input_Email']")).SendKeys("florin_nic07@yahoo.com");
            driver.FindElement(By.XPath("//*[@id='Input_Password']")).SendKeys("florin");

            Thread.Sleep(500);
            //click on log in button
            driver.FindElement(By.XPath("//button[contains(text(), 'Log in')]")).Click();

            Thread.Sleep(500);


            driver.FindElement(By.XPath("//li[@class='nav-item' and contains(a, 'Shop')]")).Click(); // click on shop button

            Thread.Sleep(500);

            driver.FindElement(By.XPath("//form[@action='/Shop/Search']/*[1]")).SendKeys("Black"); // send keys to search box input
            driver.FindElement(By.XPath("//form[@action='/Shop/Search']/*[2]")).Click(); // press on filter button

            Thread.Sleep(500);

            driver.FindElement(By.XPath("//div[@class='card-body' and contains(h6 , 'Sneakers BALMAIN, Unicorn Low Top, Orange Black')]/form/button")).Click();  //click on details button for balmain orange black shoes

            Thread.Sleep(500);

            //select shoes' number
            SelectElement selectItem = new SelectElement(driver.FindElement(By.XPath("//select[@id='size']")));
            selectItem.SelectByValue("44");


            driver.FindElement(By.XPath("//button[@class='btn btn-danger' and contains(text(), 'Add To Cart')]")).Click(); // add item to cart

            Thread.Sleep(500);

            driver.FindElement(By.XPath("//a[@class='nav-link' and @href='/Shop/Cart']")).Click(); // click on shop icon (inventory)

            Thread.Sleep(500);

            //check if the item appears in the shopping card
            Assert.IsTrue(driver.FindElement(By.XPath("//div[@class='card-body col-md-6']/p[contains(text(), 'Sneakers BALMAIN, Unicorn Low Top, Orange Black')]/following-sibling::p[contains(text(), '44')]")).Displayed);


            //verify if total cost value matches the individual costs summed up
            var totalDivs = driver.FindElements(By.XPath("//p[@class='card-text' and contains(text(), 'Ron')]")); //get individual costs
            int totalPrice = 0;
            foreach (WebElement x in totalDivs)
            {
                string result = "";
                foreach (Char i in x.Text.ToString())
                {
                    
                    if(Char.IsNumber(i))
                    {
                        result += i;
                    }
                }
                
                int value = int.Parse(result);

                totalPrice += value;

            }

            //compare totalPrice variable with Total Cost value from the website
            var totalPriceFromWebsite = driver.FindElement(By.XPath("//h3[contains(text(), 'Total cost')]"));
            string resultWebsite = "";
            foreach(Char i in totalPriceFromWebsite.Text.ToString())
            {
                if(Char.IsNumber(i))
                {
                    resultWebsite += i;
                }
                
            }
            Console.WriteLine("Website result: " + resultWebsite);
            Console.WriteLine("Individual costs summed up: " + totalPrice);
            int valueWebiste = int.Parse(resultWebsite);


            Assert.That(valueWebiste, Is.EqualTo(totalPrice));
            








        }

/*        [Test]

        public void InitialPage()
        {
            //ignore google chrome's safety recomandation
            driver.Navigate().GoToUrl("https://localhost:7249");  //open the application

            Thread.Sleep(1000);
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));


            //click on advanced button
            driver.FindElement(By.XPath("//button[@id='details-button']")).Click();

            Thread.Sleep(1000);

            //click on 
            driver.FindElement(By.XPath("//*[@id='proceed-link']")).Click();

            Thread.Sleep(1500);

            //verify nav bar buttons
            Assert.IsTrue(driver.FindElement(By.XPath("//a[contains(text(), 'Register')]")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//a[contains(text(), 'Login')]")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//a[contains(text(), 'Home')]")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//a[contains(text(), 'Shop')]")).Displayed);

            //verify middle h1 text
            Assert.IsTrue(driver.FindElement(By.XPath("//h1[contains(text(), 'Welcome to SneakerShop!')]")).Displayed);


            //verify featured products shoe list
            var featuredProductsList = driver.FindElements(By.XPath("//a[@class='featured__item']"));
            Console.Write(featuredProductsList.Count);
            Assert.IsTrue(featuredProductsList.Count == 3);

            //verify first shoe image
            var firstShoeImage = driver.FindElement(By.XPath("//img[contains(@src, '/img/shoe-1.png')]"));
            Assert.IsTrue(firstShoeImage.Displayed);

            //verify second shoe image
            var secondShoeImage = driver.FindElement(By.XPath("//img[contains(@src, '/img/shoe-2.png')]"));
            Assert.IsTrue(secondShoeImage.Displayed);

            //verify third shoe image
            var thirdShoeImage = driver.FindElement(By.XPath("//img[contains(@src, '/img/shoe-3.png')]"));
            Assert.IsTrue(thirdShoeImage.Displayed);

        }*/


/*        [Test]

        public void LogInPage()
        {
            //click on log in button
            driver.FindElement(By.XPath("//a[contains(text(), 'Login')]")).Click();

            Thread.Sleep(500);

            //verify URL link
            String URL = driver.Url.ToString();
            Assert.IsTrue(URL.Equals("https://localhost:7249/Identity/Account/Login"));

            //verify nav bar buttons
            Assert.IsTrue(driver.FindElement(By.XPath("//a[contains(text(), 'Register')]")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//a[contains(text(), 'Login')]")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//a[contains(text(), 'Home')]")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//a[contains(text(), 'Shop')]")).Displayed);

            //verify form labels
            Assert.IsTrue(driver.FindElement(By.XPath("//label[@class='form-label' and contains(text(), 'Email')]")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//label[@class='form-label' and contains(text(), 'Password')]")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//label[@class='form-label' and @for='Input_RememberMe']")).Displayed);

            //verify log in buttons
            Assert.IsTrue(driver.FindElement(By.XPath("//button[contains(text(), 'Log in')]")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//button[contains(text(), 'Facebook')]")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//button[contains(text(), 'Google')]")).Displayed);
        }*/


    }

}