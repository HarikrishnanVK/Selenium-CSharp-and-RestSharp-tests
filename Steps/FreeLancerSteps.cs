using System;
using TechTalk.SpecFlow;
using SeleniumWithSpecflow.Pages;

namespace SeleniumWithSpecflow.Steps
{
    [Binding]
    public class FreeLancerSteps
    {
        private FreeLancerPage freeLancer;
        private readonly ScenarioContext _scenarioContext;

        public FreeLancerSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I launch '(.*)' for freelancer page")]
        public void GivenILaunchForFreelancerPage(string browserName)
        {
            freeLancer = new FreeLancerPage(browserName, _scenarioContext);
        }
        
        [When(@"I navigate to freelancer website")]
        public void WhenINavigateToFreelancerWebsite()
        {
            freeLancer.NavigateToUrl();
        }
        
        [Then(@"I verify the search engine")]
        public void ThenIVerifyTheSearchEngine()
        {
            freeLancer.VerifySearchEngine();
        }
        
        [Then(@"I hire a freelancer")]
        public void ThenIHireAFreelancer()
        {
            freeLancer.HireFreeLancers();
        }
    }
}
