namespace SeleniumTests.PageObjects;

public class HeaderPageObject
{
    private IWebDriver _driver;

    private readonly By _loginButton = By.XPath("//a[@id='loginBtn']");
    private readonly By _logoutButton = By.XPath("//a[@id='logoutBtn']");
    private readonly By _registerButton = By.XPath("//a[@id='registerBtn']");
    private readonly By _patientsButton = By.XPath("//a[@id='patentsBtn']");
    private readonly By _examinationsButton = By.XPath("//a[@id='examinationsBtn']");
    private readonly By _adminButton = By.XPath("//a[@id='adminBtn']");
    private readonly By _userEmail = By.XPath("//a[@id='userEmail']");
    
    public HeaderPageObject(IWebDriver webDriver)
    {
        _driver = webDriver;
    }

    public LoginPageObject SignIn()
    {
        _driver.FindElement(_loginButton);
        return new LoginPageObject(_driver);
    }

    public string GetUserEmail()
    {
        return _driver.FindElement(_userEmail).Text;
    }
}