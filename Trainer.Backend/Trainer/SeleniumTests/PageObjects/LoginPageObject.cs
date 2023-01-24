using OpenQA.Selenium;

namespace SeleniumTests.PageObjects;

public class LoginPageObject
{
    private IWebDriver _driver;

    private readonly By _loginInput = By.XPath("//input[@id='mat-input-0']");
    private readonly By _passwordInput = By.XPath("//input[@id='mat-input-1']");
    private readonly By _loginButton = By.XPath("//button[@id='loginBtn']");
    public LoginPageObject(IWebDriver webDriver)
    {
        _driver = webDriver;
    }

    public HeaderPageObject Login(string login, string password)
    {
        _driver.FindElement(_loginInput).SendKeys(login);
        _driver.FindElement(_passwordInput).SendKeys(password);
        _driver.FindElement(_loginButton).Click();

        return new HeaderPageObject(_driver);
    }
}