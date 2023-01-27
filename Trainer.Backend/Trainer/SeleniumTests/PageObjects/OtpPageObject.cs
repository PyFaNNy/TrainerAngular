using SeleniumTests.Util;

namespace SeleniumTests.PageObjects;

public class OtpPageObject
{
    private IWebDriver _webDriver;
    
    private readonly By _otpInput = By.XPath("//input[@id='mat-input-6']");
    private readonly By _otpError = By.XPath("//input[@id='mat-input-6']/parent::div/span");

    private readonly By _sendButton = By.XPath("//button[@color]");
    
    public OtpPageObject(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }

    public MainPageObject SendCode(string code)
    {
        WaitUntil.WaitElement(_webDriver,_otpInput);
        _webDriver.FindElement(_otpInput).SendKeys(code);
        _webDriver.FindElement(_sendButton).Click();

        return new MainPageObject(_webDriver);
    }

    public string GetErrorMessage()
    {
        WaitUntil.WaitElement(_webDriver,_otpError);
        return _webDriver.FindElement(_otpError).Text;
    }
}