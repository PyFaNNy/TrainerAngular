using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests;

public class Tests
{
    private IWebDriver _driver;
    
    private readonly By _signInButton = By.XPath("//a[text()='Login']");
    private readonly By _loginButton = By.XPath("//button[@id='loginBtn']");
    private readonly By _loginInput = By.XPath("//input[@id='mat-input-0']");
    private readonly By _passwordInput = By.XPath("//input[@id='mat-input-1']");
    private readonly By _userEmail = By.XPath("//a[@id='userEmail']");
    
    private readonly string _userName = "trainerdoctor@gmail.com";
    private readonly string _userPassword = "doctor";
    
    [SetUp]
    public void Setup()
    {
        _driver = new ChromeDriver();
        _driver.Navigate().GoToUrl("http://localhost:4200/home");
        _driver.Manage().Window.Maximize();
    }

    [Test]
    public void Test1()
    {
        var signIn = _driver.FindElement(_signInButton);
        signIn.Click();
        
        var login = _driver.FindElement(_loginInput);
        var password = _driver.FindElement(_passwordInput);
        var loginBtn = _driver.FindElement(_loginButton);
        login.SendKeys(_userName);
        password.SendKeys(_userPassword);
        
        loginBtn.Click();
        Thread.Sleep(1000);
        var email = _driver.FindElement(_userEmail).Text;
        
        Assert.AreEqual(_userName, email);
    }
}