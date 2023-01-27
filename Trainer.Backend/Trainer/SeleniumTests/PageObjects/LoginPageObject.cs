using SeleniumTests.Util;

namespace SeleniumTests.PageObjects;

public class LoginPageObject
{
    private IWebDriver _driver;

    private readonly By _loginInput = By.XPath("//input[@id='mat-input-0']");
    private readonly By _passwordInput = By.XPath("//input[@id='mat-input-1']");
    private readonly By _loginButton = By.XPath("//button[@id='loginBtn']");
    private readonly By _error = By.XPath("//span[@class='text-danger']");
    
    public LoginPageObject(IWebDriver webDriver)
    {
        _driver = webDriver;
    }

    public MainPageObject Login(string login, string password)
    {
        _driver.FindElement(_loginInput).SendKeys(login);
        _driver.FindElement(_passwordInput).SendKeys(password);
        _driver.FindElement(_loginButton).Click();

        return new MainPageObject(_driver);
    }
    
    public string GetErrorMessage()
    {
        WaitUntil.WaitElement(_driver,_error);
        return _driver.FindElement(_error).Text;
    }
}