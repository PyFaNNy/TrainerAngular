using SeleniumTests.Util;

namespace SeleniumTests.PageObjects;

public class MainPageObject
{
    private IWebDriver _webDriver;

    private readonly By _loginButton = By.XPath("//a[@id='loginBtn']");
    private readonly By _logoutButton = By.XPath("//a[@id='logoutBtn']");
    private readonly By _registerButton = By.XPath("//a[@id='registerBtn']");
    private readonly By _patientsButton = By.XPath("//a[@id='patentsBtn']");
    private readonly By _examinationsButton = By.XPath("//a[@id='examinationsBtn']");
    private readonly By _adminButton = By.XPath("//a[@id='adminBtn']");
    private readonly By _userEmail = By.XPath("//a[@id='userEmail']");
    
    public MainPageObject(IWebDriver webWebDriver)
    {
        _webDriver = webWebDriver;
    }

    public LoginPageObject SignIn()
    {
        _webDriver.FindElement(_loginButton).Click();
        return new LoginPageObject(_webDriver);
    }
    
    public RegisterPageObject Register()
    {
        _webDriver.FindElement(_registerButton).Click();
        return new RegisterPageObject(_webDriver);
    }

    public string GetUserEmail()
    {
        WaitUntil.WaitElement(_webDriver,_userEmail);
        return _webDriver.FindElement(_userEmail).Text;
    }

    public string GetUrl()
    {
        return _webDriver.Url;
    }
}