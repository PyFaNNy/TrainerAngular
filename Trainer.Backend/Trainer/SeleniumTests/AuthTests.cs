using OpenQA.Selenium.Chrome;
using SeleniumTests.Data;
using SeleniumTests.PageObjects;

namespace SeleniumTests;

public class Tests
{
    private IWebDriver _driver;
    
    [SetUp]
    public void Setup()
    {
        _driver = new ChromeDriver();
        _driver.Navigate().GoToUrl("http://localhost:4200/home");
        _driver.Manage().Window.Maximize();
    }

    [TestCase(UsersLoginPage.doctorUserName, UsersLoginPage.doctorPassword, ExpectedResult = UsersLoginPage.doctorUserName)]
    [TestCase(UsersLoginPage.adminUserName, UsersLoginPage.adminPassword, ExpectedResult = UsersLoginPage.adminUserName)]
    [TestCase(UsersLoginPage.managerUserName, UsersLoginPage.managerPassword, ExpectedResult = UsersLoginPage.managerUserName)]
    public string CheckDefaultAccountsTest(string login, string password)
    {
        var header = new HeaderPageObject(_driver);

        header
            .SignIn()
            .Login(login, password);
        
        var email = header.GetUserEmail();

        return email;
    }

    [TearDown]
    public void TearDown()
    {
        _driver.Quit();
    }
}