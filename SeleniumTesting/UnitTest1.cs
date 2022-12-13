using NUnit.Framework;

using OpenQA.Selenium;

using OpenQA.Selenium.Chrome;

using System.Collections.ObjectModel;

using System.IO;

using System;

using System.Threading;

using OpenQA.Selenium.Interactions;

using OpenQA.Selenium.Support.UI;

using System.Timers;


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

        [Test]

        public void FullSequence()
        {

        }
    }

}