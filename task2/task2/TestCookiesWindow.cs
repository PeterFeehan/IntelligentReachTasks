using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace task2
{
    public class TestCookiesWindow: IDisposable
    {
        private IWebDriver driver = BaseMethods.GetDriver();  // using this we can have a headless driver

        public TestCookiesWindow() // Set up. This runs before each test in this class
        {
            this.driver.Navigate().GoToUrl("https://www.google.com");
            // Every test in this class begins with it going to this URL
        }

        public void Dispose()  // Tear down. This occurs after every test in this class
        {
            driver.Quit();
            driver.Dispose();
            // Every test should end with the driver closing the window pass or fail
        }

        [InlineData("L2AGLb")] // Accept button
        [InlineData("W0wltc")] // Reject button
        [Theory]  // To avoid redudant code we can use a theory to pass in details
        private void Click_accept_or_reject_removes_popup(string classId) 
        {
            IWebElement acceptAll = driver.FindElement(By.Id(classId));  // find the button
            acceptAll.Click();  // click it

            try // we expect the element not to be found so we should catch if find element throws an exception
            {
                IWebElement noPopup = driver.FindElement(By.Id(classId));
                Assert.Fail("Popup not Closed");  // This should never be hit but if it is something has gone wrong
            }
            catch
            {
                Assert.Equal("Google", driver.Title);  // we still want to be on Google
            }
        }

        [InlineData("eOjPIe", "Personalization settings & cookies")]  //More options
        [Theory]  // we've only got one data set here but to future proof it I've set it as a Theory
        private void Click_specified_button_by_class_changes_page(string className, string newPageTitle)
        {
            BaseMethods.Click_specified_button_by_class_changes_page(this.driver, className, newPageTitle);
        }

        [InlineData("RP3V5c", "Privacy Policy – Privacy & Terms – Google")] // Privacy
        [InlineData("HQ1lb", "Google Terms of Service – Privacy & Terms – Google")]  // Terms
        [InlineData("gksS1d", "Sign in - Google Accounts")]  // Sign in
        [Theory]
        private void Click_specified_button_by_id_changes_page(string objectId, string newPageTitle)
        {
            BaseMethods.Click_specified_button_by_id_changes_page(this.driver, objectId, newPageTitle);
        }
    }
}