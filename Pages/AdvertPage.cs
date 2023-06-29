using SeleniumWithSpecflow.Utlities;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Resources;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using ICSharpCode.Decompiler.Util;
using log4net;
using System.Reflection;

namespace SeleniumWithSpecflow.Pages
{
    [Binding]
    public class AdvertPage : GenericMethods
    {
        private readonly ScenarioContext _scenarioContext;
        private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public AdvertPage(string browser, ScenarioContext scenarioContext) : base(browser, scenarioContext)
        {
            _scenarioContext = scenarioContext;
            PageFactory.InitElements(driver, this);
        }

        #region WebElements
        [FindsBy(How = How.XPath, Using = "//h2[text()='What can I measure?']//following::h3")]
        private IList<IWebElement> Headings { set; get; }

        #endregion

        #region Methods

        public void NavigateToSite()
        {
            using (ResXResourceSet resx = new ResXResourceSet(@"..\..\SiteURLs.resx"))
            {
                string url = resx.GetString("BaseURL");
                driver.Navigate().GoToUrl(url);
            }
        }

        public List<string> ReadHeaders()
        {
            List<string> actualHeaders = new List<string>();
            foreach(var header in Headings)
            {
                WaitForElementToExist(header);
                actualHeaders.Add(header.Text);
            }
            return actualHeaders;
        }
        #endregion
    }
}
