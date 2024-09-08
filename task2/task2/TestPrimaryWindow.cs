using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace task2
{
    public class TestPrimaryWindow : IDisposable
    {
        private IWebDriver driver = BaseMethods.GetDriver();  // using this we can have a headless driver

        public TestPrimaryWindow() // Set up. This runs before each test in this class
        {
            // every tests starts by navigating to google and accepting the cookies
            this.driver.Navigate().GoToUrl("https://www.google.com");
            IWebElement acceptAll = driver.FindElement(By.Id("L2AGLb"));
            acceptAll.Click();

            try
            {
                IWebElement noPopup = driver.FindElement(By.Id("L2AGLb"));
                Assert.Fail("Cookie popup not Closed");  // if we fail in setup the test should fail
            }
            catch
            {
                Assert.Equal("Google", driver.Title);
            }
        }

        public void Dispose()  // Tear down. This occurs after every test in this class
        {
            driver.Quit();
            driver.Dispose();
            // Every test should end with the driver closing the window pass or fail
        }

        // This is just a sanity check. A real person would be needed to observe the search results
        [Fact]
        private void Searching_terms_opens_new_search_query()  
        {
            IWebElement searchBar = driver.FindElement(By.ClassName("gLFyf"));  // find the search bar
            searchBar.SendKeys("intelligent reach"); // enter search term
            searchBar.SendKeys(Keys.Return); // search

            Assert.Contains("?q=intelligent+reach", driver.Url); // result should be a query
            // As we only know what should be in the URL this is the only way we can confirm this test without a human present
        }

        [InlineData("Gmail", "Gmail")]
        [InlineData("Images", "Google Images")]
        [InlineData("Privacy", "Privacy Policy – Privacy & Terms – Google")]
        [InlineData("Terms", "Google Terms of Service – Privacy & Terms – Google")]
        [InlineData("About", "Google - About Google, our culture and company news")]
        [InlineData("Advertising","Google Ads – Get Customers and Sell More with Online Advertising")]
        [InlineData("Business", "Google for Small Business - Resources to get your small business online")]
        [InlineData(" How Search works ", "Google Search – Discover how Google Search works")]
        [Theory]
        private void Click_specified_button_by_text_changes_page(string text, string newPageTitle)
        {
            BaseMethods.Click_specified_button_by_text_changes_page(this.driver, text, newPageTitle);
        }
    }
}
