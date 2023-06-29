using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using static SeleniumExtras.WaitHelpers.ExpectedConditions;
using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using log4net;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium.Interactions;

namespace SeleniumWithSpecflow.Utlities
{
    [Binding]
    public abstract class GenericMethods
    {
        private Drivers driverType;
        private readonly ScenarioContext _scenarioContext;
        private WebDriverWait wait;
        private TimeSpan waitTime = TimeSpan.FromSeconds(15);
        private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public GenericMethods(string browser, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            driverType = new Drivers(browser, _scenarioContext);
            driver = driverType.getDriver;
            log.Info($"{browser} is launched");
        }

        public IWebDriver driver { get; set; }

        private WebDriverWait WaitForCondition()
        {
            wait = new WebDriverWait(driver, waitTime);
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            return wait;
        }

        public bool WaitForElementToExist(IWebElement element)
        {
            bool result = false;
            WaitForCondition().Until(ElementToBeClickable(element));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            log.Info($"{element} is visible in UI");
            result = true;
            return result;
        }

        public string GetText(IWebElement element)
        {
            WaitForCondition().Until(ElementToBeClickable(element));
            log.Info($"{element.Text} is scrapped from UI");
            return element.Text;
        }

        public void EnterText(IWebElement element, string content)
        {
            IWebElement webElement = WaitForCondition().Until(ElementToBeClickable(element));
            webElement.Clear();
            webElement.SendKeys(content);
            log.Info($"{content} is enetered through {element}");
        }

        public void WaitAndClick(IWebElement element)
        {
            IWebElement webElement = WaitForCondition().Until(ElementToBeClickable(element));
            webElement.Click();
            log.Info($"Browse jobs is clicked");
        }

        public void WaitAndHover(IWebElement element)
        {
            IWebElement webElement = WaitForCondition().Until(ElementToBeClickable(element));
            new Actions(driver).MoveToElement(element).Perform();
            log.Info($"hovered to {webElement}");
        }

        public void WaitForNavigation(string pageTitle)
        {
            bool title = WaitForCondition().Until(TitleContains(pageTitle));
            Assert.That(title, $"Navigation as {pageTitle} returned {title}");
            log.Info($"Navigated to site with title : {title}");
        }

        public IWebDriver SwitchToNewWindow()
        {
            Thread.Sleep(5000);
            ReadOnlyCollection<string> windowHandles = driver.WindowHandles;
            string currentWindowHandle = driver.CurrentWindowHandle;
            foreach (var window in windowHandles)
            {
                if (!window.Equals(currentWindowHandle))
                    driver = driver.SwitchTo().Window(window);
            }
            return driver;
        }

    }
}
