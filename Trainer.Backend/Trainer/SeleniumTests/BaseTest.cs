using OpenQA.Selenium.Chrome;
using SeleniumTests.Data;

namespace SeleniumTests;

public class BaseTest
{
    protected IWebDriver _webDriver;

    [OneTimeSetUp]
    protected void DoBeforeAllTheTests()
    {
    }
    
    [OneTimeTearDown]
    protected void DoAfterAllTheTests()
    {
    }

    [TearDown]
    protected void DoAfterEach()
    {
        _webDriver.Quit();
    }

    [SetUp]
    protected void DoBeforeEach()
    {
        _webDriver = new ChromeDriver();
        _webDriver.Navigate().GoToUrl(TestSettings.URL);
        _webDriver.Manage().Window.Maximize();
    }
}