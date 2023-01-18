using System;

using NUnit.Framework;

using OpenQA.Selenium;

using OpenQA.Selenium.Chrome;

using OpenQA.Selenium.Support.UI;

using SeleniumExtras.WaitHelpers;

using System;
using System.Xml.Linq;

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


        /*
        [Test]
        
        public void FullSequence()
        {
            //maximaze window
            driver.Manage().Window.Maximize();

            //ignore google chrome's safety recomandation
            driver.Navigate().GoToUrl("https://localhost:7249");  //open the application


            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); //initiate explicit wait obj



            //click on details button
            IWebElement detailsButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[@id='details-button']")));
            detailsButton.Click();


            //click on proceed to application button
            IWebElement proceedButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='proceed-link']")));
            proceedButton.Click();


            //get on login page
            IWebElement logInRedirectButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains(text(), 'Login')]")));
            logInRedirectButton.Click();


            //wrong username and password log in test
            IWebElement inputEmail = wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='Input_Email']")));
            inputEmail.SendKeys("andrei@yahoo.com");
            IWebElement inputPassword = wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='Input_Password']")));
            inputPassword.SendKeys("andrei");

            //click on log in button
            IWebElement logInButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[contains(text(), 'Log in')]")));
            logInButton.Click();


            //check if error message appears
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[contains(text(), 'Invalid login attempt')]")));
            Assert.IsTrue(driver.FindElement(By.XPath("//*[contains(text(), 'Invalid login attempt')]")).Displayed);

            //no username or no password log in test
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='Input_Password']"))); //wait page to load
            driver.FindElement(By.XPath("//*[@id='Input_Email']")).Clear();
            driver.FindElement(By.XPath("//*[@id='Input_Password']")).Clear();

            //no username test
            driver.FindElement(By.XPath("//*[@id='Input_Password']")).SendKeys("florin");
            driver.FindElement(By.XPath("//button[contains(text(), 'Log in')]")).Click();

            wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[contains(text(), 'Invalid login attempt')]")));
            Assert.IsTrue(driver.FindElement(By.XPath("//*[contains(text(), 'Invalid login attempt')]")).Displayed);

            //no password test
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='Input_Email']"))); //wait page to load
            driver.FindElement(By.XPath("//*[@id='Input_Email']")).Clear();
            driver.FindElement(By.XPath("//*[@id='Input_Password']")).Clear();

            driver.FindElement(By.XPath("//*[@id='Input_Password']")).SendKeys("florin_nic07@yahoo.com");
            driver.FindElement(By.XPath("//button[contains(text(), 'Log in')]")).Click();

            wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[contains(text(), 'Invalid login attempt')]"))); //wait page to load
            Assert.IsTrue(driver.FindElement(By.XPath("//*[contains(text(), 'Invalid login attempt')]")).Displayed);


            //correct username and password log in test
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='Input_Email']"))); //wait page to load
            driver.FindElement(By.XPath("//*[@id='Input_Email']")).Clear();
            driver.FindElement(By.XPath("//*[@id='Input_Password']")).Clear();
            driver.FindElement(By.XPath("//*[@id='Input_Email']")).SendKeys("florin_nic07@yahoo.com");
            driver.FindElement(By.XPath("//*[@id='Input_Password']")).SendKeys("florin");


            //click on log in button
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[contains(text(), 'Log in')]")));  //wait page to load
            driver.FindElement(By.XPath("//button[contains(text(), 'Log in')]")).Click();


            // click on shop button
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//li[@class='nav-item' and contains(a, 'Shop')]"))); //wait page to load
            driver.FindElement(By.XPath("//li[@class='nav-item' and contains(a, 'Shop')]")).Click();

            wait.Until(ExpectedConditions.ElementExists(By.XPath("//form[@action='/Shop/Search']/*[1]"))); //wait page to load
            driver.FindElement(By.XPath("//form[@action='/Shop/Search']/*[1]")).SendKeys("Black"); // send keys to search box input
            driver.FindElement(By.XPath("//form[@action='/Shop/Search']/*[2]")).Click(); // press on filter button


            wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='card-body' and contains(h6 , 'Sneakers BALMAIN, Unicorn Low Top, Orange Black')]/form/button")));
            driver.FindElement(By.XPath("//div[@class='card-body' and contains(h6 , 'Sneakers BALMAIN, Unicorn Low Top, Orange Black')]/form/button")).Click();  //click on details button for balmain orange black shoes


            //select shoes' number
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//select[@id='size']"))); //wait page to load
            SelectElement selectItem = new SelectElement(driver.FindElement(By.XPath("//select[@id='size']")));
            selectItem.SelectByValue("44");


            driver.FindElement(By.XPath("//button[@class='btn btn-danger' and contains(text(), 'Add To Cart')]")).Click(); // add item to cart


            wait.Until(ExpectedConditions.ElementExists(By.XPath("//a[@class='nav-link' and @href='/Shop/Cart']")));
            driver.FindElement(By.XPath("//a[@class='nav-link' and @href='/Shop/Cart']")).Click(); // click on shop icon (inventory)


            //check if the item appears in the shopping card
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='card-body col-md-6']")));
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


            //click check out button
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[@class='btn btn-danger' and contains(. , 'Check Out')]")));
            driver.FindElement(By.XPath("//button[@class='btn btn-danger' and contains(. , 'Check Out')]")).Click();


            //fill in full name input
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//input[@id='customer-name']")));
            driver.FindElement(By.XPath("//input[@id='customer-name']")).SendKeys("Gigi Becali");

            //verify that user received error message for not filling in all the input labels (user is on the same page)
            Assert.IsTrue(driver.FindElement(By.XPath("//a[@class='navbar-brand' and contains(. ,'SneakerShopApp')]")).Displayed);

            //fill in address input
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//input[@id='customer-address']")));
            driver.FindElement(By.XPath("//input[@id='customer-address']")).SendKeys("Pipera, Str Oilor, nr1");

            //verify that user received error message for not filling in all the input labels (user is on the same page)
            Assert.IsTrue(driver.FindElement(By.XPath("//a[@class='navbar-brand' and contains(. ,'SneakerShopApp')]")).Displayed);

            //fill in phone number input
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//input[@id='customer-phone']")));
            driver.FindElement(By.XPath("//input[@id='customer-phone']")).SendKeys("0123456789");

            //verify that user received error message for not filling in all the input labels (user is on the same page)
            Assert.IsTrue(driver.FindElement(By.XPath("//a[@class='navbar-brand' and contains(. ,'SneakerShopApp')]")).Displayed);

            //fill in phone number input
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//input[@id='customer-email']")));
            driver.FindElement(By.XPath("//input[@id='customer-email']")).SendKeys("becali@awdasd.com");

            //click on checkout button
            driver.FindElement(By.XPath("//button[@class='btn btn-danger my-3' and contains (. ,'Checkout')]")).Click();


            //verify amount to be paid (should match the one from sneakershopapp)
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='_34xbI' and contains (. ,'Rates are locked for 15 minutes')]")));  //wait for page to load
            IWebElement finalMoney = driver.FindElement(By.XPath("//div[@class='_1Hl7N']"));
            string finalResult = "";
            foreach (Char i in finalMoney.Text.ToString())
            {
                if (Char.IsNumber(i))
                {
                    finalResult += i;
                }
                else if (i == '.')
                {
                    break;
                }

            }

            int finalResultInt = int.Parse(finalResult);
            Assert.That(finalResultInt, Is.EqualTo(totalPrice));


            //select payment method
            driver.FindElement(By.XPath("//button[@class='_3ctB3' and @data-test='rateButtonEGLD']")).Click(); //egld method chosen


        }
        */

        /*

        [Test]

        public void FullAdminSequence()
        {
            //maximaze window
            driver.Manage().Window.Maximize();

            //ignore google chrome's safety recomandation
            driver.Navigate().GoToUrl("https://localhost:7249");  //open the application


            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); //initiate explicit wait obj



            //click on details button
            IWebElement detailsButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[@id='details-button']")));
            detailsButton.Click();


            //click on proceed to application button
            IWebElement proceedButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='proceed-link']")));
            proceedButton.Click();


            //get on login page
            IWebElement logInRedirectButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains(text(), 'Login')]")));
            logInRedirectButton.Click();


            //log in admin credentials
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='Input_Email']"))); //wait page to load
            driver.FindElement(By.XPath("//*[@id='Input_Email']")).SendKeys("admin@sneakershop.com");
            driver.FindElement(By.XPath("//*[@id='Input_Password']")).SendKeys("adminadmin");


            //click on log in button
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[contains(text(), 'Log in')]")));  //wait page to load
            driver.FindElement(By.XPath("//button[contains(text(), 'Log in')]")).Click();


            //verify admin panel button in navbar and click it
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//a[@class='nav-link text-dark' and contains(. , 'Admin Panel')]"))); //wait page to load
            driver.FindElement(By.XPath("//a[@class='nav-link text-dark' and contains(. , 'Admin Panel')]")).Click();


            //verify orders,customers,sales and earnings
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//h5[@class='card-title text-uppercase text-muted mb-0']"))); //wait page to load
            Assert.IsTrue(driver.FindElement(By.XPath("//h5[@class='card-title text-uppercase text-muted mb-0' and contains(. , 'Orders')]")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//h5[@class='card-title text-uppercase text-muted mb-0' and contains(. , 'Customers')]")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//h5[@class='card-title text-uppercase text-muted mb-0' and contains(. , 'Sales')]")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//h5[@class='card-title text-uppercase text-muted mb-0' and contains(. , 'Earnings')]")).Displayed);


            //go to products page (admin required)
            driver.FindElement(By.XPath("//button[@class='nav-link' and contains(. , 'Products')]")).Click();



            /////////    ADD PRODUCT TEST - ADMIN   /////////
            

            //click on add product button
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='tab-pane fade active show']/button"))); //wait page to load
            driver.FindElement(By.XPath("//div[@class='tab-pane fade active show']/button")).Click();

            //description input
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='modal-dialog']/div/div[2]/input")));
            driver.FindElement(By.XPath("//div[@class='modal-dialog']/div/div[2]/input")).SendKeys("Ronaldo best footballer");

            //long description input
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//textarea[@id='long-description']")));
            driver.FindElement(By.XPath("//textarea[@id='long-description']")).SendKeys("Test product, Ronaldo best footballer, winter sales");

            //price input
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='price']")));
            driver.FindElement(By.XPath("//input[@id='price']")).SendKeys("9999");

            //click on create product button
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@id='create-btn']")));
            driver.FindElement(By.XPath("//button[@id='create-btn']")).Click();

            //refresh page
            driver.Navigate().Refresh();

            //go to shop page
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[@class='nav-link text-dark' and contains(. ,'Shop')]")));
            driver.FindElement(By.XPath("//a[@class='nav-link text-dark' and contains(. ,'Shop')]")).Click();

            //filter for the new product
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//form[@action='/Shop/Search']/input")));
            driver.FindElement(By.XPath("//form[@action='/Shop/Search']/input")).SendKeys("Ronaldo");

            driver.FindElement(By.XPath("//form[@action='/Shop/Search']/button")).Click();

            //verify if product appears
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='card mx-auto my-auto' and contains(. ,'Ronaldo best footballer')]/div/h6")));
            Assert.IsTrue(driver.FindElement(By.XPath("//div[@class='card mx-auto my-auto' and contains(. ,'Ronaldo best footballer')]/div/h6")).Displayed);


            /////////    DELETED PRODUCT TEST - ADMIN   /////////


            //go to admin panel
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//a[@class='nav-link text-dark' and contains(. , 'Admin Panel')]"))); //wait page to load
            driver.FindElement(By.XPath("//a[@class='nav-link text-dark' and contains(. , 'Admin Panel')]")).Click();

            //go to products page (admin required)
            driver.FindElement(By.XPath("//button[@class='nav-link' and contains(. , 'Products')]")).Click();


            //scroll until element is viewed
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='card-body' and contains(. ,'Ronaldo')]/button")));
            IWebElement deleteProductElement = driver.FindElement(By.XPath("//div[@class='card-body' and contains(. ,'Ronaldo')]/button"));
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView({behavior: 'smooth', block: 'center'})", deleteProductElement);


            Thread.Sleep(2000);
            //click on delete button
            driver.FindElement(By.XPath("//div[@class='card-body' and contains(. ,'Ronaldo')]/button")).Click();

            //go to shop page
            IWebElement shopElement = driver.FindElement(By.XPath("//a[@class='nav-link text-dark' and contains(. ,'Shop')]"));
            js.ExecuteScript("arguments[0].scrollIntoView({behavior: 'smooth', block: 'center'})", shopElement);
            Thread.Sleep(2000);
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[@class='nav-link text-dark' and contains(. ,'Shop')]")));
            driver.FindElement(By.XPath("//a[@class='nav-link text-dark' and contains(. ,'Shop')]")).Click();

            //filter for the new product
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//form[@action='/Shop/Search']/input")));
            driver.FindElement(By.XPath("//form[@action='/Shop/Search']/input")).SendKeys("Ronaldo");

            driver.FindElement(By.XPath("//form[@action='/Shop/Search']/button")).Click();

            //verify if product appears
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@class='display-4' and contains( . , 'Shop')]")));
            var elementList = driver.FindElements(By.XPath("//div[@class='card mx-auto my-auto' and contains(. ,'Ronaldo best footballer')]/div/h6"));
            if (elementList.Count == 0)
            {
                Assert True;
            }
            else
            {
                Assert False;
            }

        }
        */

        [Test]

        public void RegisterTest()
        {
            //maximaze window
            driver.Manage().Window.Maximize();

            //ignore google chrome's safety recomandation
            driver.Navigate().GoToUrl("https://localhost:7249");  //open the application


            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); //initiate explicit wait obj



            //click on details button
            IWebElement detailsButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[@id='details-button']")));
            detailsButton.Click();


            //click on proceed to application button
            IWebElement proceedButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='proceed-link']")));
            proceedButton.Click();


            //get on register page
            IWebElement logInRedirectButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains(text(), 'Register')]")));
            logInRedirectButton.Click();

            //TEST try to register without filling email and password input
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[@id='registerSubmit']"))); //wait page to load
            driver.FindElement(By.XPath("//button[@id='registerSubmit']")).Click(); //click on register button

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(. , 'The Email field is required.')]"))); // wait for message to appear
            Assert.IsTrue(driver.FindElement(By.XPath("//span[contains(. , 'The Email field is required.')]")).Displayed); //verify error message
            Assert.IsTrue(driver.FindElement(By.XPath("//span[contains(. , 'The Password field is required.')]")).Displayed); //verify error message

            //TEST try to register without filling email input
            driver.FindElement(By.XPath("//input[@id='Input_Password']")).SendKeys("parola"); //fill in password
            Assert.IsTrue(driver.FindElement(By.XPath("//span[contains(. , 'The Email field is required.')]")).Displayed); //verify error message

            //TEST try to register without filling password input
            driver.FindElement(By.XPath("//input[@id='Input_Password']")).Clear(); //remove password input
            driver.FindElement(By.XPath("//input[@id='Input_Email']")).SendKeys("testemail@testemail"); //fill in email
            Assert.IsTrue(driver.FindElement(By.XPath("//span[contains(. , 'The Password field is required.')]")).Displayed); //verify error message

            //TEST try to register without filling confirmation password input
            driver.FindElement(By.XPath("//input[@id='Input_Password']")).SendKeys("parola"); //fill in password
            driver.FindElement(By.XPath("//button[@id='registerSubmit']")).Click(); //click on register button
            Assert.IsTrue(driver.FindElement(By.XPath("//span[contains(. , 'The password and confirmation password do not match.')]")).Displayed); //verify error message

            //TEST input email without @
            driver.FindElement(By.XPath("//input[@id='Input_Email']")).Clear();
            driver.FindElement(By.XPath("//input[@id='Input_Password']")).Clear();
            driver.FindElement(By.XPath("//input[@id='Input_Email']")).SendKeys("testemail"); //fill in email

            driver.FindElement(By.XPath("//input[@id='Input_Password']")).SendKeys("parola"); //fill in password 
            
            driver.FindElement(By.XPath("//input[@id='Input_ConfirmPassword']")).SendKeys("parola"); // fill in confirm password

            wait.Until(ExpectedConditions.ElementExists(By.XPath("//span[contains(. , 'The Email field is not a valid e-mail address.')]"))); // wait for message to appear
            Assert.IsTrue(driver.FindElement(By.XPath("//span[contains(. , 'The Email field is not a valid e-mail address.')]")).Displayed); //verify error message


            //TEST 


            //TEST correct email, correct password, wrong confirm password
            //clear old inputs
            driver.FindElement(By.XPath("//input[@id='Input_Email']")).Clear();
            driver.FindElement(By.XPath("//input[@id='Input_Password']")).Clear();
            driver.FindElement(By.XPath("//input[@id='Input_ConfirmPassword']")).Clear();

            driver.FindElement(By.XPath("//input[@id='Input_Email']")).SendKeys("testemail@testemail.com"); //fill in email

            driver.FindElement(By.XPath("//input[@id='Input_Password']")).SendKeys("parola"); //fill in password 

            driver.FindElement(By.XPath("//input[@id='Input_ConfirmPassword']")).SendKeys("awdawdwadadwa"); // fill in confirm password (wrong value)

            wait.Until(ExpectedConditions.ElementExists(By.XPath("//span[contains(. , 'The password and confirmation password do not match.')]"))); // wait for message to appear
            Assert.IsTrue(driver.FindElement(By.XPath("//span[contains(. , 'The password and confirmation password do not match.')]")).Displayed); //verify error message


            //TEST correct email, correct password, correct confirm password
            //clear old inputs
            driver.FindElement(By.XPath("//input[@id='Input_Email']")).Clear();
            driver.FindElement(By.XPath("//input[@id='Input_Password']")).Clear();
            driver.FindElement(By.XPath("//input[@id='Input_ConfirmPassword']")).Clear();

            driver.FindElement(By.XPath("//input[@id='Input_Email']")).SendKeys("testemail@testemail.com"); //fill in email

            driver.FindElement(By.XPath("//input[@id='Input_Password']")).SendKeys("parola"); //fill in password 

            driver.FindElement(By.XPath("//input[@id='Input_ConfirmPassword']")).SendKeys("parola"); // fill in confirm password

            driver.FindElement(By.XPath("//button[@id='registerSubmit']")).Click(); //click on register button

            //verify if login is successful
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//a[@class='nav-link text-dark' and contains(. , 'testemail')]"))); // wait for message to appear
            Assert.IsTrue(driver.FindElement(By.XPath("//a[@class='nav-link text-dark' and contains(. , 'testemail')]")).Displayed); //verify error message
            





        }



        /*

                [Test]

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

                }


                [Test]

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
                }

        */


    }

}