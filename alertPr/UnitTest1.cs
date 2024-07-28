using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace alertPr
{
    [TestFixture]
    public class Tests
    {

        private IWebDriver driver;
        private WebDriverWait waitHandle;

        [SetUp]
        public void Setup()
        {
            string path = "T:\\הנדסת תוכנה\\שנה ב\\קבוצה ב\\תלמידות\\מירי בלאקר\\alertPr\\chromedriver-win64";
            driver = new ChromeDriver(path);
        }

        [Test]
        public void WaitToAlert()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/alerts");

            IWebElement button = driver.FindElement(By.Id("timerAlertButton"));

            button.Click();
            TimeSpan timeOut;
            waitHandle = new WebDriverWait(driver, TimeSpan.FromSeconds(6));

            waitHandle.Until(ExpectedConditions.AlertIsPresent());

            IAlert alert = driver.SwitchTo().Alert();

            alert.Accept();
        }

        [Test]
        public void PassBetweenWindows()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/browser-windows");

            IWebElement button = driver.FindElement(By.Id("windowButton"));

            string currentWindow = driver.CurrentWindowHandle.ToString();

            button.Click();

            List<string> windowHandles = new List<string>(driver.WindowHandles);

            driver.SwitchTo().Window(windowHandles[1]);

            Assert.AreEqual(driver.Url, "https://demoqa.com/sample");

            ((IJavaScriptExecutor)driver).ExecuteScript("window.close()");

            driver.SwitchTo().Window(currentWindow);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}