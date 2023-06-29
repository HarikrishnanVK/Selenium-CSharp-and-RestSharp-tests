using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumWithSpecflow.Pages;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SeleniumWithSpecflow.Steps
{
    [Binding]
    class AdvertSteps
    {
        private AdvertPage advert;
        private readonly ScenarioContext _scenarioContext;

        public AdvertSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I launch '(.*)' for advertpage")]
        public void Intialize(string browserName)
        {
            advert = new AdvertPage(browserName, _scenarioContext);
        }

        [When(@"I navigate to website")]
        public void Navigate()
        {
            advert.NavigateToSite();
        }

        [Then(@"I read the measurement headings")]
        public void ReadHeadings(Table table)
        {
            IList<string> expectedListOfData = advert.ReadHeaders();
            dynamic actualListOfData = table.CreateDynamicSet();
            // CollectionAssert.AreEqual(actualListOfData, expectedListOfData);
            int count = 0;
            foreach (var actualData in actualListOfData)
            {
                Assert.AreEqual(actualData.measurements, expectedListOfData[count]);
                count++;
            }
        }
    }


}
