using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTests.Util;

public static class WaitUntil
{
    public static void WaitElement(IWebDriver webDriver, By locator, int seconds = 4)
    {
        new WebDriverWait(webDriver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementIsVisible(locator));
    }
}