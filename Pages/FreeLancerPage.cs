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
using log4net;
using System.Reflection;

namespace SeleniumWithSpecflow.Pages
{
    [Binding]
    public class FreeLancerPage : GenericMethods
    {
        private readonly ScenarioContext _scenarioContext;
        private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public FreeLancerPage(string browser, ScenarioContext scenarioContext) : base(browser, scenarioContext)
        {
            _scenarioContext = scenarioContext;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[fltrackinglabel=BrowseJobs]")]
        private IWebElement browserJobsLink { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#job-hero-location-title")]
        private IWebElement freelancerHomeTitle { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#keyword-input")]
        private IWebElement searchInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = "a[data-qtsb-label=link-project-title]")]
        private IList<IWebElement> jobLinks { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#post-project-banner-close")]
        private IWebElement bannerCloseButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='NativeElement ng-star-inserted'][contains(text(),'Hire Freelancers')]")]
        private IWebElement hireFreeLancersMenu { get; set; }

        [FindsBy(How = How.XPath, Using = "(//fl-bit[@fltrackingsection='SeoNavBarMegaMenu']//div[@class='NativeElement ng-star-inserted'][contains(text(),'By Location')])[1]")]
        private IWebElement myLocationSubMenu { get; set; }

        [FindsBy(How = How.XPath, Using = "//fl-bit[@class='Item ng-star-inserted']//a[contains(text(),'Hire Freelancers in India')]")]
        private IWebElement indianLocationSubMenu { get; set; }

        [FindsBy(How = How.CssSelector, Using = "ul[class='Breadcrumbs-list'] span")]
        private IList<IWebElement> breadCrumbs { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[@id='breadcrumb-search-title'][text()='automation']")]
        private IWebElement breadCrumbSearchTitle { get; set; }

        [FindsBy(How = How.XPath, Using = "(//div[@class='freelancer-details-header']//a)[2]")]
        private IWebElement seoProviders { get; set; }

        [FindsBy(How = How.XPath, Using = "(//img[contains(@src,'/badges/verified')])[2]")]
        private IWebElement seoProvidersTag { get; set; }

        public void NavigateToUrl()
        {
            driver.Navigate().GoToUrl("https://www.freelancer.com/");
        }

        public void VerifySearchEngine()
        {
            WaitAndClick(browserJobsLink);
            log.Info($"Navigated to {driver.Url}");
            WaitForNavigation("Jobs and Contests");
            string header = GetText(freelancerHomeTitle);
            Assert.AreEqual(header.Trim(), "Top Jobs");
            log.Info($"{header} is present");
            EnterText(searchInput, "automation testing");
            log.Info($"serach keyword entered as automation testing in {driver.Url}");
            searchInput.SendKeys(Keys.Enter);
            WaitForElementToExist(breadCrumbSearchTitle);
            bool isPresent = false;
            foreach (var locator in jobLinks)
            {
                string attributeValue = locator.GetAttribute("href");
                if (attributeValue.ToLower().Contains("automation"))
                {
                    isPresent = true;
                    break;
                }
            }
            if (isPresent)
            {
                Assert.IsTrue(isPresent);
                log.Info($"search is able to work on the keyword");
            }
            else
                Assert.Fail($"search is not able to work on the keyword");
        }

        public void BlockImages()
        {
            browserJobsLink.CtrlClick();
            driver = SwitchToNewWindow();
            WaitAndClick(bannerCloseButton);
        }

        public void HireFreeLancers()
        {
            WaitAndHover(hireFreeLancersMenu);
            WaitAndHover(myLocationSubMenu);
            WaitAndClick(indianLocationSubMenu);
            WaitForNavigation("Freelancers in India For Hire");
            breadCrumbs.ToList().ForEach(x =>
            {
                if (x.Text.Trim().Equals("India"))
                    Assert.AreEqual("India", x.Text.Trim());
            });
            breadCrumbs.ToList().ForEach(x =>
            {
                if (x.Text.Trim().Equals("Freelancers"))
                    Assert.AreEqual("Freelancers", x.Text.Trim());
            });
            WaitAndClick(seoProviders);
            driver = SwitchToNewWindow();
            Assert.IsTrue(WaitForElementToExist(seoProvidersTag));
        }

        public void QuitDriver()
        {
            driver.Quit();
        }
    }
}
