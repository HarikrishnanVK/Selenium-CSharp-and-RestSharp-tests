using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SeleniumWithSpecflow.Utlities
{
    class Drivers : IDisposable
    {
        private IWebDriver driver;
        private AndroidDriver<AppiumWebElement> androidDriver;
        private ScenarioContext _scenarioContext;

        public Drivers(string browserType, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            driver = LaunchDriver(browserType);
        }

        public IWebDriver LaunchDriver(string browserType)
        {
            switch (browserType.ToLower().Replace(" ", ""))
            {
                case "desktopchrome":
                    driver = LaunchChrome();
                    _scenarioContext["driver"] = driver;
                    return driver;
                case "desktopfirefox":
                    return LaunchFirefox();
                case "androiddriver":
                    return LaunchAndroidDriver();
                case "remotechrome":
                    return LaunchAppiumForWeb();
                default:
                    throw new Exception("Specified" + browserType + "is not included in the framework");
            }
        }

        private IWebDriver LaunchChrome()
        {
            ChromeOptions options = new ChromeOptions();
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            options.AcceptInsecureCertificates = true;
            options.AddArgument("ignore-certificate-errors");
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            return driver;
        }

        private IWebDriver LaunchFirefox()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            options.AcceptInsecureCertificates = true;
            options.AddArgument("ignore-certificate-errors");
            options.AddAdditionalCapability(CapabilityType.Timeouts, TimeSpan.FromSeconds(30));
            return new FirefoxDriver(options);
        }

        private AndroidDriver<AppiumWebElement> LaunchAndroidDriver()
        {
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Pixel_6a");
            capabilities.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UIAutomator2");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "13");
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, @"D:\Automation_POC\Playwright_Sample_Tests\app\android\app-appstore-release.apk");

            AppiumLocalService appiumService = new AppiumServiceBuilder().WithIPAddress("127.0.0.1").UsingPort(4724).Build();
            androidDriver = new AndroidDriver<AppiumWebElement>(appiumService, capabilities);
            return androidDriver;
        }

        private RemoteWebDriver LaunchAppiumForWeb()
        {
            ChromeOptions options = new ChromeOptions();
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            options.AcceptInsecureCertificates = true;
            options.AddArgument("ignore-certificate-errors");
            options.AddAdditionalCapability(CapabilityType.Timeouts, TimeSpan.FromSeconds(30));
            return new RemoteWebDriver(new Uri("http://127.0.0.1:4724/wd/hub"), options);
        }

        public void Dispose()
        {
            driver.Quit();
        }

        public IWebDriver getDriver => driver;
    }
}
