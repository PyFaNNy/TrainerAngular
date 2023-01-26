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
    
    [TestCase("asda", "asda", ExpectedResult = ErrorMessenge.AUTHERROR)]
    public string FailAuthTest(string login, string password)
    {
        var header = new HeaderPageObject(_driver);

        var loginPage = header
            .SignIn();

        loginPage
            .Login(login, password);
        
        var error = loginPage.GetErrorMessage();

        return error;
    }
    
    [TearDown]
    public void TearDown()
    {
        
    }
}