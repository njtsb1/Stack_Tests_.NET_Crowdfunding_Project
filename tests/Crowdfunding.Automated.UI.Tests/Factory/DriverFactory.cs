using System;
using System.Text;
using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Crowdfunding.AutomatedUITests
{
	public class DriverFactory
    {
        private IWebDriver _driver;

        // Class constructor. 
        public DriverFactory()
        {
            // Initializes the browser using the ChromeDriver that is in the path /usr/share/applications/
            //ChromeDriverService service = ChromeDriverService.CreateDefaultService("/usr/share/applications/");
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService("/usr/share/applications/");
            
            // Creates a port to open the browser.
            service.Port = new Random().Next(64000, 64800);
            
            /*ChromeOptions options = new ChromeOptions();
            options.AddArgument("headless");
            options.AddArgument("no-sandbox");
            //options.AddArgument("proxy-server='direct://'");
            options.AddArgument("proxy-auto-detect");
            options.AddArgument("proxy-bypass-list=*");
            options.AddUserProfilePreference("disable-popup-blocking", "true");
*/
            // Initializes selenium's IWebDriver, it is the one that provides the queries and manipulations of the pages. 
            //_driver = new ChromeDriver(service, options);
            CodePagesEncodingProvider.Instance.GetEncoding(437);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            FirefoxOptions options = new FirefoxOptions();
            options.AddArgument("-headless");
            options.AddArgument("-safe-mode");
            options.AddArgument("-ignore-certificate-errors");
            FirefoxProfile profile = new FirefoxProfile();
            profile.AcceptUntrustedCertificates = true;
            profile.AssumeUntrustedCertificateIssuer = false;
            options.Profile = profile;
            
            _driver = new FirefoxDriver(service, options);
            
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _driver.Manage().Window.Maximize();
        }

        // Navigate to a given URL
        public void NavigateToUrl(String url)
        {            
            _driver.Navigate().GoToUrl(url);
        }

        // Terminate driver and service.
        public void Close()
        {
            _driver.Quit();
        }

        // Provide driver.
        public IWebDriver GetWebDriver()
        {
            return _driver;
        }
    }
}