using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SeleniumWithSpecflow.Utlities
{
    [Binding]
    public static class ExtensionMethods
    {
        public static void xtJSClick(this IWebElement element, IWebDriver driver = null)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", element);
        }

        public static void CtrlClick(this IWebElement element, IWebDriver driver = null)
        {
            new Actions(driver).SendKeys(Keys.Control).Click(element).Build().Perform();
        }
    }
}
