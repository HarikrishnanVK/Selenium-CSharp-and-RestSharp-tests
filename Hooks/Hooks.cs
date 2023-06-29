using SeleniumWithSpecflow.Utlities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(10)]
namespace SeleniumWithSpecflow.Hooks
{
    [Binding]
    public class Hooks
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [BeforeScenario]
        public void InitializeLogs()
        {
            log4net.Config.XmlConfigurator.
                ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\log4net.config"));
            log.Info("Application is working");
        }

        [AfterScenario]
        public void DisposeDriver(ScenarioContext scenarioContext)
        {
            try
            {
                var driver = (IWebDriver)scenarioContext["driver"];
                driver.Quit();
            }
            catch
            {
                log.Info("driver is already closed");
            }
        }
    }
}
