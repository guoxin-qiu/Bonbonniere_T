using Bonbonniere.UITests.Tools;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace Bonbonniere.UITests
{
    public class SampleTest
    {
        private RemoteWebDriver _webDriver;

        [Fact(Skip = "Pause")]
        public void TestWithChromeDriver()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                //driver.Navigate().GoToUrl(@"https://automatetheplanet.com/multiple-files-page-objects-item-templates/");
                driver.Navigate().GoToUrl(@"https://www.baidu.com/");
                var link = driver.FindElement(By.PartialLinkText("About  Baidu"));
                var jsToBeExecuted = $"window.scroll(0, {link.Location.Y});";
                ((IJavaScriptExecutor)driver).ExecuteScript(jsToBeExecuted);
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("About  Baidu")));
                clickableElement.Click();
            }
        }

        [Fact]
        public void Test2()
        {
            _webDriver = WebDriverHelper.CurrentDriver;

            _webDriver.Navigate().GoToUrl(@"https://www.baidu.com/");
            var link = _webDriver.FindElement(By.PartialLinkText("About  Baidu"));
            var jsToBeExecuted = $"window.scroll(0, {link.Location.Y});";
            ((IJavaScriptExecutor)_webDriver).ExecuteScript(jsToBeExecuted);
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromMinutes(1));
            var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("About  Baidu")));
            clickableElement.Click();

            WebDriverHelper.QuitDriver();
        }
    }
}
