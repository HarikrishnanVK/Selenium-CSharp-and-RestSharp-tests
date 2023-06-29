using SeleniumWithSpecflow.Pages;
using SeleniumWithSpecflow.Utlities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using NUnit.Framework;

namespace SeleniumWithSpecflow.Steps
{
    [Binding]
    public class SauceLabSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private Homepage homePage;

        public SauceLabSteps(ScenarioContext scenarioContext) 
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I launch '(.*)' for homepage")]
        public void Intialize(string browserName)
        {
            homePage = new Homepage(browserName, _scenarioContext);
        }

        [Then(@"I navigate to Swag labs")]
        public void GivenINavigateToSwagLabs()
        {
            homePage.NavigateToUrl();
        }

        [Then(@"I close the browser")]
        public void TearDown()
        {
            homePage.QuitDriver();
        }

        [When(@"I enter the username")]
        public void WhenIEnterTheUsername(Table table)
        {
            dynamic datas = table.CreateDynamicSet();
            foreach(var data in datas)
            {
                homePage.EnterUsername(data.userName);
                _scenarioContext["userName"] = data.userName;
            }
        }

        [When(@"I enter the password '(.*)'")]
        public void WhenIEnterThePassword(string psswd)
        {
            string userName = (string)_scenarioContext["userName"];
            Console.WriteLine($"username capatured from scenario context : {userName}");
            homePage.EnterPassword(psswd);
        }

        [Then(@"I login to homepage")]
        public void ThenILoginToHomepage()
        {
            homePage.LoginAndConfirm();
        }

    }
}
