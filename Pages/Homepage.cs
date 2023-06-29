using SeleniumWithSpecflow.Utlities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SeleniumWithSpecflow.Pages
{
    [Binding]
    class Homepage : GenericMethods
    {
        private readonly ScenarioContext _scenarioContext;
       
        public Homepage(string browser, ScenarioContext scenarioContext) : base(browser, scenarioContext)
        {
            _scenarioContext = scenarioContext;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='user-name']")]
        private IWebElement UserName { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='password']")]
        private IWebElement Password { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='login-button']")]
        private IWebElement LoginButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "span[class=title]")]
        private IWebElement PageTitle { get; set; }

        public void NavigateToUrl()
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }

        public void EnterUsername(string userName)
        {
            EnterText(UserName, userName);
        }
        public void EnterPassword(string password)
        {
            EnterText(Password, password);
        }

        public void LoginAndConfirm()
        {
            LoginButton.xtJSClick(driver);
            string text = GetText(PageTitle);
            Assert.AreEqual(text, "Products");
        }

        public void QuitDriver()
        {
            driver.Quit();
        }
    }
}
