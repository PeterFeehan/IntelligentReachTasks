using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace task2
{
    // By having a base set of methods we can use diffewrent setups and teardowns when we use ultimately the same tests
    internal class BaseMethods
    {

        public static IWebDriver GetDriver()  // setup a headless driver
        {
            ChromeOptions options = new();
            options.AddArgument("--headless=new");
            IWebDriver driver = new ChromeDriver(options);
            return driver;
        }

        // as loading time can be variable we want to avoid a false result
        public static bool waitForNewTitle(WebDriver driver, string newTitle, int timeout)
        {
            while (timeout != 0)  // wait for the timeout
            { 
                if (driver.Title == newTitle) // does the result map
                {
                    return true;  // once it's as expected return true
                }
                else
                {
                    Thread.Sleep(1000);  // wait one second
                    timeout--;  // reduce timeout
                }
            }
            return false;  // if it fails return false
        }

        public static void Click_specified_button_by_id_changes_page(IWebDriver driver, string objectId, string newPageTitle)
        {
            IWebElement button = driver.FindElement(By.Id(objectId));  // Find the class
            button.Click();  // Click the button
            Assert.True(waitForNewTitle((WebDriver)driver, newPageTitle, 5));  // assert the page is correct
        }

        public static void Click_specified_button_by_class_changes_page(IWebDriver driver, string className, string newPageTitle)
        {
            IWebElement button = driver.FindElement(By.ClassName(className));  // Find the class
            button.Click();  // Click the button
            Assert.True(waitForNewTitle((WebDriver)driver, newPageTitle, 5));  // assert the page is correct
        }

        public static void Click_specified_button_by_text_changes_page(IWebDriver driver, string text, string newPageTitle)
        {
            IWebElement button = driver.FindElement(By.XPath(String.Format("//*[text()='{0}']", text)));  // Find the text
            button.Click();  // Click the button
            Assert.True(waitForNewTitle((WebDriver)driver, newPageTitle, 5));  // assert the page is correct
        }

    }
}
