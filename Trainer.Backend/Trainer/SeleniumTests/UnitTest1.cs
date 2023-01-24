using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTests.PageObjects;

namespace SeleniumTests;

public class Tests
{
    private IWebDriver _driver;
    
    private readonly string _userName = "trainerdoctor@gmail.com";
    private readonly string _userPassword = "doctor";

    [SetUp]
    public void Setup()
    {
        _driver = new ChromeDriver();
        _driver.Navigate().GoToUrl("http://localhost:4200/home");
        _driver.Manage().Window.Maximize();
    }

    [Test]
    public void Test1()
    {
        var header = new HeaderPageObject(_driver);

        header
            .SignIn()
            .Login(_userName, _userPassword);
        
        var email = header.GetUserEmail();

        Assert.AreEqual(_userName, email);
    }

    [TearDown]
    public void TearDown()
    {
        _driver.Quit();
    }
}