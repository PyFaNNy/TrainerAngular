using SeleniumTests.Models;
using SeleniumTests.Util;

namespace SeleniumTests.PageObjects;

public class RegisterPageObject
{
    private IWebDriver _webDriver;
    
    private readonly By _firstNameInput = By.XPath("//input[@id='mat-input-0']");
    private readonly By _firstNameError = By.XPath("//input[@id='mat-input-0']/parent::div/span");
    
    private readonly By _middleNameInput = By.XPath("//input[@id='mat-input-1']");
    private readonly By _middleNameError = By.XPath("//input[@id='mat-input-1']/parent::div/span");
    
    private readonly By _lastNameInput = By.XPath("//input[@id='mat-input-2']");
    private readonly By _lastNameError = By.XPath("//input[@id='mat-input-2']/parent::div/span");
    
    private readonly By _emailInput = By.XPath("//input[@id='mat-input-3']");
    private readonly By _emailError = By.XPath("//input[@id='mat-input-3']/parent::div/span");
    
    private readonly By _passwordInput = By.XPath("//input[@id='mat-input-4']");
    private readonly By _passwordError = By.XPath("//input[@id='mat-input-4']/parent::div/span");
    
    private readonly By _passwordConfirmInput = By.XPath("//input[@id='mat-input-5']");
    private readonly By _passwordConfirmError = By.XPath("//input[@id='mat-input-5']/parent::div/span");
    
    private readonly By _roleSelect = By.XPath("//*[@id='mat-select-0']");
    
    private readonly By _registerButton = By.XPath("//button[@id='registerBtn']");


    public RegisterPageObject(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }

    public OtpPageObject CreateUser(User user)
    {
        _webDriver.FindElement(_firstNameInput).SendKeys(user.FirstName);
        _webDriver.FindElement(_lastNameInput).SendKeys(user.LastName);
        _webDriver.FindElement(_middleNameInput).SendKeys(user.MiddleName);
        _webDriver.FindElement(_emailInput).SendKeys(user.Email);
        _webDriver.FindElement(_passwordInput).SendKeys(user.Password);
        _webDriver.FindElement(_passwordConfirmInput).SendKeys(user.PasswordConfirm);
        _webDriver.FindElement(_roleSelect).Click();
        _webDriver.FindElement(By.XPath($"//mat-option[@value='{user.Role.ToString()}']")).Click();
        _webDriver.FindElement(_registerButton).Click();

        return new OtpPageObject(_webDriver);
    }
    
    #region GetErrors
    public string GetFirstNameErrorMessage()
    {
        WaitUntil.WaitElement(_webDriver,_firstNameError);
        return _webDriver.FindElement(_firstNameError).Text;
    }
    
    public string GetLastNameErrorMessage()
    {
        WaitUntil.WaitElement(_webDriver,_lastNameError);
        return _webDriver.FindElement(_lastNameError).Text;
    }
    
    public string GetMiddleNameErrorMessage()
    {
        WaitUntil.WaitElement(_webDriver,_middleNameError);
        return _webDriver.FindElement(_middleNameError).Text;
    }
    
    public string GetEmailErrorMessage()
    {
        WaitUntil.WaitElement(_webDriver,_emailError);
        return _webDriver.FindElement(_emailError).Text;
    }
    
    public string GetPasswordErrorMessage()
    {
        WaitUntil.WaitElement(_webDriver,_passwordError);
        return _webDriver.FindElement(_passwordError).Text;
    }
    
    public string GetPasswordConfirmErrorMessage()
    {
        WaitUntil.WaitElement(_webDriver,_passwordConfirmError);
        return _webDriver.FindElement(_passwordConfirmError).Text;
    }
    #endregion
}