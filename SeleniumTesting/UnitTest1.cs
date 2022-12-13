using NUnit.Framework;

using OpenQA.Selenium;

using OpenQA.Selenium.Chrome;

using OpenQA.Selenium.Firefox;

using System;

using System.Collections.ObjectModel;

using System.IO;

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

        public void verifyInitialPage()
        {
            driver.Navigate().GoToUrl("http://localhost:7249");  //open the application

            //verify nav bar buttons
            Assert.IsTrue(driver.FindElement(By.XPath("//a[contains(text(), 'Register')]")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//a[contains(text(), 'Login')]")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//a[contains(text(), 'Home')]")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//a[contains(text(), 'Shop')]")).Displayed);

            //verify middle h1 text
            Assert.IsTrue(driver.FindElement(By.XPath("h1[contains(text(), 'Welcome to SneakerShop!')]")).Displayed);
            

        }
    }
}