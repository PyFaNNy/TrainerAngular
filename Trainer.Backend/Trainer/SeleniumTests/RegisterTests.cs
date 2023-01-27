using SeleniumTests.Data;
using SeleniumTests.Models;
using SeleniumTests.PageObjects;
using SeleniumTests.Util;
using Trainer.Enums;

namespace SeleniumTests;

[TestFixture]
public class RegisterTests : BaseTest
{
    [Test]
    public void RegistrationTest()
    {
        var header = new MainPageObject(_webDriver);
        var user = new User
        {
            FirstName = TestGenerateData.GenerateRandomString(8, true),
            LastName = TestGenerateData.GenerateRandomString(8, true),
            MiddleName = TestGenerateData.GenerateRandomString(8, true),
            Email = TestGenerateData.GenerateRandomEmail(10, EmailDomain.Gmail),
            Password = Users.PASSWORD,
            PasswordConfirm = Users.PASSWORD,
            Role = UserRole.Manager
        };
        header
            .Register()
            .CreateUser(user)
            .SendCode("666666");
        
        Assert.Pass();
    }
}