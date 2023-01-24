using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests;

public class Tests
{
    private IWebDriver _driver;
    [SetUp]
    public void Setup()
    {
        _driver = new ChromeDriver();
        _driver.Navigate().GoToUrl("https://www.google.com/");
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}