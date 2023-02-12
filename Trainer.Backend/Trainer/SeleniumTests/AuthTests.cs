using SeleniumTests.Data;
using SeleniumTests.PageObjects;

namespace SeleniumTests;

[TestFixture]
public class AuthTests : BaseTest
{
    [TestCase(Users.doctorUserName, Users.doctorPassword, ExpectedResult = Users.doctorUserName)]
    [TestCase(Users.adminUserName, Users.adminPassword, ExpectedResult = Users.adminUserName)]
    [TestCase(Users.managerUserName, Users.managerPassword, ExpectedResult = Users.managerUserName)]
    public string CheckDefaultAccountsTest(string login, string password)
    {
        var header = new MainPageObject(_webDriver);

        header
            .SignIn()
            .Login(login, password);
        
        var email = header.GetUserEmail();

        return email;
    }
    
    [TestCase("asda", "asda", ExpectedResult = ErrorMessenge.AUTHERROR)]
    public string FailAuthTest(string login, string password)
    {
        var header = new MainPageObject(_webDriver);

        var loginPage = header
            .SignIn();
        loginPage
            .Login(login, password);
        
        var error = loginPage.GetErrorMessage();

        return error;
    }
}