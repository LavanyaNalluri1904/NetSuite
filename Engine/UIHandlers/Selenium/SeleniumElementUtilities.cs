using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using System.Windows.Forms;
using TestReporter;
using Engine.Setup;
using System.Net;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

namespace Engine.UIHandlers.Selenium
{
    /// <summary>
    /// @Author - Pavan Parmar
    /// </summary>
    public static class SeleniumElementUtilities
    {
        private static IReporter testReport = EngineSetup.TestReport;
        private static String mainWindow = null;
        #region Non-Public Methods
        
        /// <summary>
        /// Simulates the send keys.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="chars">The chars.</param>
        internal static void SimulateSendKeys(IWebDriver driver, string chars)
        {
            try
            {
                SendKeys.SendWait(chars);
                testReport.LogSuccess("SimulateSendKeys", String.Format("Invoked SendKeys.SendWait for input - <mark>{0}</mark>", chars));
            }

            catch (Exception ex)
            {

                testReport.LogFailure("SimulateSendKeys", String.Format("Error While Invoking SendKeys.SendWait for input - <mark>{0}</mark>", chars));
                testReport.LogException(ex, EngineSetup.GetScreenShotPath());
            }
        }
       
        #endregion

        #region Public Methods
        /// <summary>
        /// Highlights the specified locator.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="count">The count.</param>
        public static void Highlight(this IWebDriver driver, By locator, int count = 5)
        {
            if (driver.IsElementPresent(locator))
            {
                try
                {
                    //blink the item
                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                    for (int i = 0; i < count; i++)
                    {
                        js.ExecuteScript("arguments[0].setAttribute('style',arguments[1]);", driver.FindElement(locator), "color: green; border: 2px solid yellow;");
                        Thread.Sleep(50);
                        js.ExecuteScript("arguments[0].setAttribute('style',arguments[1]);", driver.FindElement(locator), "");
                    }
                }
                catch (Exception) { }
            }
        }
        /// <summary>
        /// Determines whether the element is present.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="locator">The locator.</param>
        /// <returns></returns>
        public static bool IsElementPresent(this IWebDriver driver, By locator)
        {
            IWebElement webElement = null;
            bool isPresent = false;
            try
            {
                webElement = driver.FindElement(locator);
                isPresent = true;

            }
            catch (NoSuchElementException ex)
            {
                isPresent = false;
            }

            catch (WebDriverException ex)
            {
                isPresent = false;
            }
            catch (Exception)
            {
                isPresent = false;
            }
            return isPresent;
        }

        /// <summary>
        /// Determines whether the element is present.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="controlName">The control name for reporting.</param>
        /// <returns></returns>
        public static bool CheckElementExists(this IWebDriver driver, By locator, string controlName)
        {
            IWebElement webElement = null;
            bool isPresent = false;
            try
            {
                webElement = driver.FindElement(locator);
                isPresent = true;
            }
            catch (NoSuchElementException ex)
            {
                isPresent = false;
            }

            catch (WebDriverException ex)
            {
                isPresent = false;
            }
            catch (Exception ex)
            {
                isPresent = false;
            }
            if (isPresent)
            {
                driver.Highlight(locator);
                testReport.LogSuccess("CheckElementExists - "+ controlName, String.Format("Found the element <mark>{0}</mark> successfully - Locator: <mark>{1}</mark>", controlName, locator.ToString()));
            }
            else
            {
                testReport.LogFailure("CheckElementExists - " + controlName, String.Format("Element <mark>{0}</mark> does not exist - Locator: <mark>{1}</mark>", controlName, locator.ToString()), EngineSetup.GetScreenShotPath());
            }
            return isPresent;
        }


        /// <summary>
        /// Determines whether the element is not present.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="controlName">The control name for reporting.</param>
        /// <returns></returns>
        public static bool CheckElementDoesnotExists(this IWebDriver driver, By locator, string controlName)
        {
            IWebElement webElement = null;
            bool isNotPresent = true;
            try
            {
                webElement = driver.FindElement(locator);
                isNotPresent = false;
            }
            catch (NoSuchElementException ex)
            {
                isNotPresent = true;
            }

            catch (WebDriverException ex)
            {
                isNotPresent = true;
            }
            catch (Exception ex)
            {
                isNotPresent = true;
            }
            if (isNotPresent)
            {
                testReport.LogSuccess("CheckElementDoesnotExists - " + controlName, String.Format("The element <mark>{0}</mark> does not exist- Locator: <mark>{1}</mark>", controlName, locator.ToString()));
            }
            else
            {
                testReport.LogFailure("CheckElementDoesnotExists - "+ controlName, String.Format("Element <mark>{0}</mark> found on the page- Locator: <mark>{1}</mark>", controlName, locator.ToString()), EngineSetup.GetScreenShotPath());
            }
            return isNotPresent;
        }

        public static Image GetEntireScreenshot(this IWebDriver driver)
        {
            // Get the total size of the page
            var totalWidth = (int)(long)((IJavaScriptExecutor)driver).ExecuteScript("return document.body.offsetWidth"); //documentElement.scrollWidth");
            var totalHeight = (int)(long)((IJavaScriptExecutor)driver).ExecuteScript("return  document.body.parentNode.scrollHeight");
            // Get the size of the viewport
            var viewportWidth = (int)(long)((IJavaScriptExecutor)driver).ExecuteScript("return document.body.clientWidth"); //documentElement.scrollWidth");
            var viewportHeight = (int)(long)((IJavaScriptExecutor)driver).ExecuteScript("return window.innerHeight"); //documentElement.scrollWidth");

            // We only care about taking multiple images together if it doesn't already fit
            if (totalWidth <= viewportWidth && totalHeight <= viewportHeight)
            {
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                return ScreenshotToImage(screenshot);
            }
            // Split the screen in multiple Rectangles
            var rectangles = new List<Rectangle>();
            // Loop until the totalHeight is reached
            for (var y = 0; y < totalHeight; y += viewportHeight)
            {
                var newHeight = viewportHeight;
                // Fix if the height of the element is too big
                if (y + viewportHeight > totalHeight)
                {
                    newHeight = totalHeight - y;
                }
                // Loop until the totalWidth is reached
                for (var x = 0; x < totalWidth; x += viewportWidth)
                {
                    var newWidth = viewportWidth;
                    // Fix if the Width of the Element is too big
                    if (x + viewportWidth > totalWidth)
                    {
                        newWidth = totalWidth - x;
                    }
                    // Create and add the Rectangle
                    var currRect = new Rectangle(x, y, newWidth, newHeight);
                    rectangles.Add(currRect);
                }
            }
            // Build the Image
            var stitchedImage = new Bitmap(totalWidth, totalHeight);
            // Get all Screenshots and stitch them together
            var previous = Rectangle.Empty;
            foreach (var rectangle in rectangles)
            {
                // Calculate the scrolling (if needed)
                if (previous != Rectangle.Empty)
                {
                    var xDiff = rectangle.Right - previous.Right;
                    var yDiff = rectangle.Bottom - previous.Bottom;
                    // Scroll
                    ((IJavaScriptExecutor)driver).ExecuteScript(String.Format("window.scrollBy({0}, {1})", xDiff, yDiff));
                }
                // Take Screenshot

                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                // Build an Image out of the Screenshot
                var screenshotImage = ScreenshotToImage(screenshot);
                // Calculate the source Rectangle
                var sourceRectangle = new Rectangle(viewportWidth - rectangle.Width, viewportHeight - rectangle.Height, rectangle.Width, rectangle.Height);
                // Copy the Image
                using (var graphics = Graphics.FromImage(stitchedImage))
                {
                    graphics.DrawImage(screenshotImage, rectangle, sourceRectangle, GraphicsUnit.Pixel);
                }
                // Set the Previous Rectangle
                previous = rectangle;
            }
            return stitchedImage;
        }

        private static Image ScreenshotToImage(Screenshot screenshot)
        {
            Image screenshotImage;
            using (var memStream = new MemoryStream(screenshot.AsByteArray))
            {
                screenshotImage = Image.FromStream(memStream);
            }
            return screenshotImage;
        }

        /// <summary>
        /// Determines whether the element's Displayed attribute is true .
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="controlName">The control name for reporting.</param>
        /// <returns></returns>
        public static bool CheckElementDisplayed(this IWebDriver driver, By locator, string controlName)
        {
            IWebElement webElement = null;
            bool isPresent = false;
            try
            {
                webElement = driver.FindElement(locator);
                if(webElement.Displayed)
                    isPresent = true;

            }
            catch (NoSuchElementException ex)
            {
                isPresent = false;
            }

            catch (WebDriverException ex)
            {
                isPresent = false;
            }
            catch (Exception)
            {
                isPresent = false;
            }
            if (isPresent)
            {
                driver.Highlight(locator);
                testReport.LogSuccess("CheckElementDisplayed - " + controlName, String.Format("Found the element <mark>{0}</mark> is displayed successfully - Locator: <mark>{1}</mark>", controlName, locator.ToString()));
            }
            else
            {
                testReport.LogFailure("CheckElementDisplayed - " + controlName, String.Format("Element <mark>{0}</mark> is not displayed - Locator: <mark>{1}</mark>", controlName, locator.ToString()), EngineSetup.GetScreenShotPath());
            }
            return isPresent;
        }

        /// <summary>
        /// Determines whether the element's is Displayed attribute is false .
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="controlName">The control name for reporting.</param>
        /// <returns></returns>
        public static bool CheckElementNotDisplayed(this IWebDriver driver, By locator, string controlName)
        {
            IWebElement webElement = null;
            bool isPresent = false;
            try
            {
                webElement = driver.FindElement(locator);
                if (!webElement.Displayed)
                    isPresent = true;

            }
            catch (NoSuchElementException ex)
            {
                isPresent = false;
            }

            catch (WebDriverException ex)
            {
                isPresent = false;
            }
            catch (Exception)
            {
                isPresent = false;
            }
            if (isPresent)
            {
                testReport.LogSuccess("CheckElementNotDisplayed - " + controlName, String.Format("The element <mark>{0}</mark> is not displayed on the page - Locator: <mark>{1}</mark>", controlName, locator.ToString()));
            }
            else
            {
                testReport.LogFailure("CheckElementNotDisplayed - " + controlName, String.Format("Found the Element <mark>{0}</mark> is displayed on the page - Locator: <mark>{1}</mark>", controlName, locator.ToString()), EngineSetup.GetScreenShotPath());
            }
            return isPresent;
        }

        /// <summary>
        /// Used to check the status of radio button - checked/unchecked
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="state">checked/unchecked</param>
        /// <param name="controlName">The control name for reporting.</param>
        /// <returns></returns>
        public static bool VerifyInputRadioButtonStatus(this IWebDriver driver, By locator,string state, string controlName)
        {
            IWebElement webElement = null;
            bool result = false;
            try
            {
                webElement = driver.FindElement(locator).FindElement(By.XPath(".."));
                state = state.ToLower();
                switch (state)
                {
                    case "checked":
                        if (webElement.GetAttribute("class") == "checked")
                            result = true;
                        break;
                    case "unchecked":
                    case "":
                        if (webElement.GetAttribute("class") == "" || webElement.GetAttribute("class") == "unchecked" || webElement.GetAttribute("class")==null)
                            result = true;
                        break;
                }
            }
            catch (NoSuchElementException ex)
            {
                result = false;
            }

            catch (WebDriverException ex)
            {
                result = false;
            }
            catch (Exception)
            {
                result = false;
            }
            if (result)
            {
                testReport.LogSuccess("VerifyInputRadioButtonStatus - " + controlName, String.Format("Found the radio button <mark>{0}</mark> is in {1} state - Locator: <mark>{2}</mark>", controlName, state, locator.ToString()));
            }
            else
            {
                testReport.LogFailure("VerifyInputRadioButtonStatus - " + controlName, String.Format("Found the radio button <mark>{0}</mark> is not in {1} state - Locator: <mark>{2}</mark>", controlName, state, locator.ToString()), EngineSetup.GetScreenShotPath());
            }
            return result;
        }

        /// <summary>
        /// Used to check the status of radio button - checked/unchecked
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="state">checked/unchecked</param>
        /// <param name="controlName">The control name for reporting.</param>
        /// <returns></returns>
        public static bool VerifyCheckboxStatus(this IWebDriver driver, By locator, string state, string controlName)
        {
            IWebElement webElement = null;
            bool result = false;
            try
            {
                webElement = driver.FindElement(locator).FindElement(By.XPath(".."));
                state = state.ToLower();
                switch (state)
                {
                    case "checked":
                        if (webElement.GetAttribute("class") == "checked")
                            result = true;
                        break;
                    case "unchecked":
                    case "":
                        if (webElement.GetAttribute("class") == "" || webElement.GetAttribute("class") == "unchecked" || webElement.GetAttribute("class") == null)
                            result = true;
                        break;
                }
            }
            catch (NoSuchElementException ex)
            {
                result = false;
            }

            catch (WebDriverException ex)
            {
                result = false;
            }
            catch (Exception)
            {
                result = false;
            }
            if (result)
            {
                testReport.LogSuccess("VerifyCheckboxStatus - " + controlName, String.Format("Found the checkbox <mark>{0}</mark> is in {1} state - Locator: <mark>{2}</mark>", controlName, state, locator.ToString()));
            }
            else
            {
                testReport.LogFailure("VerifyCheckboxStatus - " + controlName, String.Format("Found the checkbox <mark>{0}</mark> is not in {1} state - Locator: <mark>{2}</mark>", controlName, state, locator.ToString()), EngineSetup.GetScreenShotPath());
            }
            return result;
        }

        /// <summary>
        /// Identifies a tag in DOM of a page and searches for data in that tag
        /// </summary>
        /// <param name="driver">web driver instance</param>
        /// <param name="elementId">Id attribute of the tag</param>
        /// <param name="searchValue">value to be searched within tag</param>
        /// <returns></returns>
        public static bool CheckTagInformationInSource(this IWebDriver driver,string elementId, string searchValue)
        {
            try
            {
                string source = driver.PageSource;
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                string html = source;
                doc.LoadHtml(html);
                HtmlAgilityPack.HtmlNode element = doc.GetElementbyId(elementId);
                if (element.InnerText.Contains(searchValue))
                {
                    testReport.LogSuccess("Tag information - "+ elementId, String.Format("Successfully validated the data: <mark>{0}</mark> in element <mark>{1}</mark>", searchValue,elementId));
                    return true;
                }
                else
                {
                    testReport.LogFailure("Tag information - " + elementId, String.Format("Failed to validate data: <mark>{0}</mark> in element <mark>{1}</mark>",searchValue,elementId),EngineSetup.GetScreenShotPath());
                    return false;
                }
            }
            catch (Exception ex)
            {
                testReport.LogFailure("Tag information - " + elementId, String.Format("Failed to validate data: <mark>{0}</mark> in element <mark>{1}</mark>", searchValue, elementId), EngineSetup.GetScreenShotPath());
                return false;
            }


        }

        /// <summary>
        /// Method to validate data in a JSON file
        /// </summary>
        /// <param name="driver">web driver instance</param>
        /// <param name="jsonFilePath">JSON file path</param>
        /// <param name="searchValue">value to search</param>
        /// <returns></returns>
        public static bool VerifyDataInJsonFile(this IWebDriver driver, string jsonFilePath,string searchValue)
        {
            WebClient request = new WebClient();

            try
            {
                byte[] newFileData = request.DownloadData(jsonFilePath);
                string jsonData = System.Text.Encoding.UTF8.GetString(newFileData);
                if (jsonData.Contains(searchValue))
                {
                    testReport.LogSuccess("JSON Data Validation - " + searchValue, String.Format("Successfully validated the data: <mark>{0}</mark> in JSON File", searchValue));
                    return true;
                }
                else
                {
                    testReport.LogFailure("JSON Data Validation - " + searchValue, String.Format("Failed to validate data: <mark>{0}</mark> in JSON File", searchValue), EngineSetup.GetScreenShotPath());
                    return false;
                }

            }
            catch (WebException ex)
            {
                testReport.LogFailure("JSON Data Validation - " + searchValue, String.Format("Failed to validate data: <mark>{0}</mark> in JSON File", searchValue), EngineSetup.GetScreenShotPath());
                return false;
            }
        }



        /// <summary>
        /// wait for the element to load on the page. waits for it to exists then for it to be visible.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        public static void WaitElementExistsAndVisible(this IWebDriver driver, By locator)
        {
            //WatchSpinner(driver);
            //In case of IE, ExpectedConditions defined for chrome/implementation through WebDriverWait.Until causes Timeout. Issue with IEDriver
            if (driver is InternetExplorerDriver || driver is EdgeDriver)
            {
                try
                {
                    int MAXTIMEOUT = EngineSetup.DefaultTimeoutForSelenium * 1000;
                    int threadSleepTime = 1000;
                    while (threadSleepTime <= MAXTIMEOUT && driver.IsElementPresent(locator) == false)
                    {
                        if (threadSleepTime == MAXTIMEOUT)
                        {
                            
                            testReport.LogFailure("WaitElementExistsAndVisible", String.Format("Exception thrown by SeleniumElementUtilities.WaitElementExistsAndVisible : Object With By <mark>{0}</mark> Not Found", locator.ToString()), EngineSetup.GetScreenShotPath());
                        }
                        Thread.Sleep(1000);
                        threadSleepTime = threadSleepTime + 1000;
                    }

                }
                catch (Exception)
                {

                }

            }
            else //if not IE and edge
            {
                try
                {
                    ExpectedConditions.ElementExists(locator).Invoke(driver);
                    ExpectedConditions.ElementIsVisible(locator).Invoke(driver);
                    ExpectedConditions.ElementToBeClickable(locator).Invoke(driver);

                }
                catch (Exception)
                {
                    //AutomationLogging.LogErrorMessage(String.Format("Exception thrown by SeleniumElementUtilities.WaitElementExistsAndVisible : Object With By {0} Not Found", locator.ToString()), driver, true);
                }

            }
            //scroll bar
            if (driver.IsElementPresent(locator))
            {
                //scroll bar
                object objElementTop = ((IJavaScriptExecutor)driver).ExecuteScript(
                        "return arguments[0].getBoundingClientRect().top;", driver.FindElement(locator));
                double dblElementTop = Convert.ToDouble(objElementTop);
                try
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,dblElementTop)", "");
                }
                catch (Exception)
                {

                }

            }
            driver.Highlight(locator);

        }

       
        /// <summary>
        /// waits for an element (with the given locator) to be present on the page
        /// a timeout can be defined or the default will be assigned
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <param name="defaultTimeout"></param>
        /// <returns></returns>
        public static IWebDriver WaitElementPresent(this IWebDriver driver, By locator,
            int defaultTimeout = EngineSetup.TimeOutConstant)
        {
            return SeleniumFrameworkExtensions.CompleteAction(() =>
            {
                driver.WaitElementExistsAndVisible(locator);
                /*return*/
                driver.FindElement(locator);
                return driver;
            }
                , defaultTimeout, driver, String.Format("Element found (locator -> <mark>{0}</mark>)", locator), "WaitElementPresent");
        }

        /// <summary>
        /// Elements the present.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="controlName">Name of the control.</param>
        /// <param name="defaultTimeout">The default timeout.</param>
        /// <returns></returns>
        public static bool ElementPresent(this IWebDriver driver, By locator, string controlName,
            int defaultTimeout = EngineSetup.TimeOutConstant)
        {
            return SeleniumFrameworkExtensions.CompleteAction(() => {  return driver.FindElements(locator).Count > 0; }
                , defaultTimeout, driver,
                String.Format("Checked For Presence Of Element - ControlName: <mark>{0}</mark>", controlName), "ElementPresent-"+controlName);
        }

        /// <summary>
        /// Webs the element present.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="defaultTimeout">The default timeout.</param>
        /// <returns></returns>
        public static bool WebElementPresent(this IWebDriver driver, IWebElement locator,
            int defaultTimeout = EngineSetup.TimeOutConstant)
        {
            return SeleniumFrameworkExtensions.CompleteAction(() => {  return locator != null; }
                , defaultTimeout, driver);
        }


        /// <summary>
        /// waits for a given element to be stale.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <param name="defaultTimeout"></param>
        /// <returns></returns>
        /// 
        //this doesnt work....i need the element so when i click i can then wait if stale
        public static IWebDriver WaitElementStale(this IWebDriver driver, By locator,
            int defaultTimeout = EngineSetup.TimeOutConstant)
        {
            var element = driver.FindElement(locator);
            return SeleniumFrameworkExtensions.CompleteAction(() =>
            {
                try
                {
                    element.GetAttribute("id");
                }
                catch (StaleElementReferenceException)
                {
                    return driver;
                }
                //make custom exception?
                throw new WebDriverTimeoutException(String.Format("Element did not become stale within <mark>{0}</mark> seconds",
                    defaultTimeout));
            }, defaultTimeout, driver);
        }

       
        /// <summary>
        /// waits for a given element's attribute to equal the expected value.
        /// a timeout can be specified or the default timeout is assigned
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <param name="attrName"></param>
        /// <param name="attrValue"></param>
        /// <param name="defaultTimeout"></param>
        /// <returns></returns>
        public static IWebDriver WaitElementAttrEquals(this IWebDriver driver, By locator, string attrName,
            string attrValue, string controlName, int defaultTimeout = EngineSetup.TimeOutConstant)
        {
            return SeleniumFrameworkExtensions.CompleteAction(() =>
            {
                if (driver.FindElement(locator).GetAttribute(attrName).Contains(attrValue))
                {
                    return driver;
                }
                throw new Exception(
                    String.Format(
                        "Attribute (<mark>{0}</mark>) not equal to specified value (<mark>{1}</mark>) after <mark>{2}</mark> seconds: locator -> <mark>{3}</mark>", attrName,
                        attrValue, defaultTimeout, locator));
            }, defaultTimeout, driver, String.Format("Performed action 'WAIT' on element - ControlName: <mark>{0}</mark> till the value is <mark>{1}</mark>, locator - <mark>{2}</mark>", controlName, attrValue,locator), "WaitElementAttrEquals-" + controlName);
        }

        /// <summary>
        /// Waits the element text equals.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="textValue">The text value.</param>
        /// <param name="defaultTimeout">The default timeout.</param>
        /// <returns></returns>
        public static IWebDriver WaitElementTextEquals(this IWebDriver driver, By locator, string textValue, string controlName,
            int defaultTimeout = EngineSetup.TimeOutConstant)
        {
            return SeleniumFrameworkExtensions.CompleteAction(() =>
            {
                if (driver.FindElement(locator).Text == textValue)
                {
                    return driver;
                }
                throw new Exception(
                    String.Format("Text not equal to specified value (<mark>{0}</mark>) after <mark>{1}</mark> seconds: locator -> <mark>{2}</mark>", textValue,
                        defaultTimeout, locator));
            }, defaultTimeout, driver, String.Format("Element -> <mark>{0}</mark> - text equal to <mark>'{1}'</mark>", locator, textValue), "WaitElementTextEquals-" + controlName);
        }

        /// <summary>
        /// Returns the specified attribute of the given control
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <param name="attributeName"></param>
        /// <param name="defaultTimeout"></param>
        /// <returns></returns>
        public static string GetElementAttribute(this IWebDriver driver, By locator, string attributeName,
            int defaultTimeout = EngineSetup.TimeOutConstant)
        {
            return
                SeleniumFrameworkExtensions.CompleteAction(
                    () => {  return driver.FindElement(locator).GetAttribute(attributeName); }, defaultTimeout, driver);
        }

        /// <summary>
        /// Validate element is highlighted when error occurs
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <param name="attributeName"></param>
        /// <param name="defaultTimeout"></param>
        /// <returns></returns>
        public static bool ValidateRequiredFieldError(this IWebDriver driver, By locator, string controlName,string attributeName, string attributeValue,
            int defaultTimeout = EngineSetup.TimeOutConstant)
        {
            bool flag = false;
            return
                SeleniumFrameworkExtensions.CompleteAction(
                    () => {
                        flag = (driver.FindElement(locator).GetAttribute(attributeName).Contains(attributeValue)) ? true : false;
                        if (flag)
                        {
                            testReport.LogSuccess("ValidateRequiredFieldError - "+ controlName, String.Format("The element <mark>{0}</mark> is found to be a Required Field - Locator: <mark>{1}</mark>", controlName , locator.ToString()));
                        }
                        else
                        {
                            testReport.LogFailure("ValidateRequiredFieldError - " + controlName, String.Format("The element <mark>{0}</mark> is not found to be a Required Field - Locator: <mark>{1}</mark>", controlName, locator.ToString()), EngineSetup.GetScreenShotPath());
                        }
                        return flag;
                    }
                    , defaultTimeout, driver);
        }

        /// <summary>
        /// returns the specified CSS value of the given attribute
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <param name="propertyName"></param>
        /// <param name="defaultTimeout"></param>
        /// <returns></returns>
        public static string GetElementCssValue(this IWebDriver driver, By locator, string propertyName,
            int defaultTimeout = EngineSetup.TimeOutConstant)
        {
            return
                SeleniumFrameworkExtensions.CompleteAction(
                    () => {  return driver.FindElement(locator).GetCssValue(propertyName); }, defaultTimeout, driver);
        }

        /// <summary>
        /// get the text of a given element
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <param name="defaultTimeout"></param>
        /// <returns></returns>
        public static string GetElementText(this IWebDriver driver, By locator,
            int defaultTimeout = EngineSetup.TimeOutConstant)
        {
            return SeleniumFrameworkExtensions.CompleteAction(() => {  return driver.FindElement(locator).Text; },
                defaultTimeout, driver);
        }

        /// <summary>
        /// Validate text of a given element
        /// </summary>
        /// <param name="driver"> selenium driver instance</param>
        /// <param name="locator"> location information</param>
        /// <param name="expectedValue"> value to be validated</param>
        /// <param name="defaultTimeout"></param>
        /// <returns></returns>
        public static bool VerifyTextValue(this IWebDriver driver, By locator, string expectedValue,string controlName="",
            int defaultTimeout = EngineSetup.TimeOutConstant)
        {
            bool flag = false;
            return SeleniumFrameworkExtensions.CompleteAction(() => {
                string txt = driver.FindElement(locator).GetAttribute("textContent");
                if (txt == "")
                    txt = driver.FindElement(locator).GetAttribute("value");
                if (txt == "")
                    txt = driver.FindElement(locator).GetAttribute("text");
                flag = (txt.Trim().Equals(expectedValue)) ? true : false;
                if (flag)
                {
                    testReport.LogSuccess("VerifyTextValue - "+ controlName, String.Format("The element's text value: <mark>{0}</mark> matched successfully with expected value: <mark>{1}</mark>- Locator: <mark>{2}</mark>", txt, expectedValue, locator.ToString()));
                }
                else
                {
                    testReport.LogFailure("VerifyTextValue - " + controlName, String.Format("The element's text value <mark>{0}</mark> does not matched with expected value: <mark>{1}</mark>- Locator: <mark>{2}</mark>", txt, expectedValue, locator.ToString()), EngineSetup.GetScreenShotPath());
                }

            return flag;
            },
                defaultTimeout, driver);
        }

        /// <summary>
        /// Gets the element property.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="propName">Name of the property.</param>
        /// <param name="defaultTimeout">The default timeout.</param>
        /// <returns></returns>
        public static object GetElementProperty(this IWebDriver driver, By locator, string propName,
            int defaultTimeout = EngineSetup.TimeOutConstant)
        {
            return
                SeleniumFrameworkExtensions.CompleteAction(
                    () =>
                    {                        
                        return ((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0][arguments[1]];",
                            driver.FindElement(locator), propName);
                    }, defaultTimeout, driver);
        }

        /// <summary>
        /// Gets the name of the element tag.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="defaultTimeout">The default timeout.</param>
        /// <returns></returns>
        public static string GetElementTagName(this IWebDriver driver, By locator,
            int defaultTimeout = EngineSetup.TimeOutConstant)
        {
            return SeleniumFrameworkExtensions.CompleteAction(() => {  return driver.FindElement(locator).TagName; },
                defaultTimeout, driver);
        }

        /// <summary>
        /// Gets the element bounds.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="locator">The locator.</param>
        /// <returns></returns>
        public static object GetElementBounds(this IWebDriver driver, By locator)
        {
            return
                SeleniumFrameworkExtensions.CompleteAction(
                    () =>
                    {
                        return
                            ((IJavaScriptExecutor)driver).ExecuteScript(
                                "return arguments[0].getBoundingClientRect();", driver.FindElement(locator));
                    },
                    EngineSetup.DefaultTimeoutForSelenium, driver);
        }

        /// <summary>
        /// Gets the element top.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="locator">The locator.</param>
        /// <returns></returns>
        public static object GetElementTop(this IWebDriver driver, By locator)
        {
            return
                SeleniumFrameworkExtensions.CompleteAction(
                    () =>
                    {
                        return
                            ((IJavaScriptExecutor)driver).ExecuteScript(
                                "return arguments[0].getBoundingClientRect().top;", driver.FindElement(locator));
                    },
                    EngineSetup.DefaultTimeoutForSelenium, driver);
        }

        /// <summary>
        /// Gets the element left.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="locator">The locator.</param>
        /// <returns></returns>
        public static object GetElementLeft(this IWebDriver driver, By locator)
        {
            return
                SeleniumFrameworkExtensions.CompleteAction(
                    () =>
                    {
                        return
                            ((IJavaScriptExecutor)driver).ExecuteScript(
                                "return arguments[0].getBoundingClientRect().left;", driver.FindElement(locator));
                    },
                    EngineSetup.DefaultTimeoutForSelenium, driver);
        }

        /// <summary>
        /// Gets the width of the element.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="locator">The locator.</param>
        /// <returns></returns>
        public static object GetElementWidth(this IWebDriver driver, By locator)
        {
            return
                SeleniumFrameworkExtensions.CompleteAction(
                    () =>
                    {
                        return
                            ((IJavaScriptExecutor)driver).ExecuteScript(
                                "return arguments[0].getBoundingClientRect().width;", driver.FindElement(locator));
                    },
                    EngineSetup.DefaultTimeoutForSelenium, driver);
        }

        /// <summary>
        /// Gets the height of the element.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="locator">The locator.</param>
        /// <returns></returns>
        public static object GetElementHeight(this IWebDriver driver, By locator)
        {
            return
                SeleniumFrameworkExtensions.CompleteAction(
                    () =>
                    {
                        return
                            ((IJavaScriptExecutor)driver).ExecuteScript(
                                "return arguments[0].getBoundingClientRect().height;", driver.FindElement(locator));
                    },
                    EngineSetup.DefaultTimeoutForSelenium, driver);
        }

        /// <summary>
        /// Gets the dropdown selected option text.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="defaultTimeout">The default timeout.</param>
        /// <returns></returns>
        public static string GetDropdownSelectedOptionText(this IWebDriver driver, By locator,
            int defaultTimeout = EngineSetup.TimeOutConstant)
        {
            return SeleniumFrameworkExtensions.CompleteAction(() =>
            {
                driver.WaitElementExistsAndVisible(locator);
                return new SelectElement(driver.FindElement(locator)).SelectedOption.Text;
            }, defaultTimeout, driver);
        }
        /// <summary>
        /// Scrolls to page bottom.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <returns></returns>
        public static object ScrollToPageBottom(this IWebDriver driver)
        {
            
            //testReport.LogInfo("ScrollToPageBottom Will Be Called");
            object obj = null;
            try
            {
                obj =
                    ((IJavaScriptExecutor)driver).ExecuteScript(
                        "window.scrollTo(0,document.body.scrollHeight)", "");

                //testReport.LogSuccess("ScrollToPageBottom", "ScrollToPageBottom Was Called Successfully");
                Thread.Sleep(1000);

            }
            catch (Exception)
            {

            }
            return obj;

        }
        /// <summary>
        /// Scrolls to page top.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <returns></returns>
        public static object ScrollToPageTop(this IWebDriver driver)
        {
            
            testReport.LogInfo("ScrollToPageTop Will Be Called");
            object obj = null;
            try
            {
                obj =
                    ((IJavaScriptExecutor)driver).ExecuteScript(
                        "window.scrollTo(0, - document.body.scrollHeight)", "");

                testReport.LogSuccess("ScrollToPageTop", "ScrollToPageTop Was Called Successfully");
                Thread.Sleep(1000);
            }
            catch (Exception)
            {

            }
            return obj;

        }

        /*
         Parameter	Type	Description
            horizantalAlongXAxis	Number	Required. How many pixels to scroll by, along the x-axis (horizontal). Positive values will scroll to the left, while negative values will scroll to the right
            verticalAlongYAxis	Number	Required. How many pixels to scroll by, along the y-axis (vertical). Positive values will scroll down, while negative values scroll up
         */
        /// <summary>
        /// Scrolls the page.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="horizantalAlongXAxis">The horizantal along x axis.</param>
        /// <param name="verticalAlongYAxis">The vertical along y axis.</param>
        /// <returns></returns>
        public static object ScrollPage(this IWebDriver driver, int horizantalAlongXAxis = 0, int verticalAlongYAxis = 200)
        {
           
            //testReport.LogInfo("ScrollPage Will Be Called");
            object obj = null;
            try
            {
                obj =
                    ((IJavaScriptExecutor)driver).ExecuteScript(
                        "window.scrollBy("+horizantalAlongXAxis+", "+verticalAlongYAxis+")");

                //testReport.LogSuccess("ScrollPage", String.Format("ScrollPage Was Called Successfully With horizantalAlongXAxis - {0}, verticalAlongYAxis - {1}", horizantalAlongXAxis, verticalAlongYAxis));
                Thread.Sleep(1000);
            }
            catch (Exception)
            {

            }
            return obj;

        }

        /// <summary>
        /// Waits the element enabled.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="defaultTimeout">The default timeout.</param>
        /// <returns></returns>
        public static IWebDriver WaitElementEnabled(this IWebDriver driver, By locator,
            int defaultTimeout = EngineSetup.TimeOutConstant)
        {
            return SeleniumFrameworkExtensions.CompleteAction(() =>
            {
                driver.WaitElementEnabled(locator);
                /*return*/
                driver.FindElement(locator);
                return driver;
            }
                , defaultTimeout, driver, String.Format("Element found (locator -> <mark>{0}</mark>)", locator),"WaitElementEnabled");
        }


        /// <summary>
        /// Waits for page to load.
        /// </summary>
        /// <param name="driver">Extension method type, IWebDriver</param>        
        /// <param name="timeout">The amount of time to wait for the page to load before returning.</param>
        /// <param name="suppressException">if <c>true</c>, the timeout will not throw an exception. (Default is <c>true</c>)</param> 
        /// <returns><c>true</c> if page loads within the specified time <c>false</c> otherwise.</returns>
        public static void WaitForPageLoad(this IWebDriver driver, TimeSpan timeout, bool suppressException = true)
        {
            try
            {
                IWait<IWebDriver> wait = new WebDriverWait(driver, timeout);
                wait.Until(driver1 => (driver as IJavaScriptExecutor).ExecuteScript("return document.readyState").Equals("complete")
                    && (bool)(driver as IJavaScriptExecutor).ExecuteScript("return (window.jQuery != null) && (jQuery.active == 0);"));
            }
            catch (Exception exception)
            {
                if (exception is WebDriverTimeoutException)
                    Console.WriteLine(@"The specified wait time has expired.");

                Console.WriteLine(@"{0}:{1}", "The Page failed to load properly. See Exception details.", exception.Message);
                if (!suppressException)
                    throw exception;
            }

        }

        /// <summary>
        /// Waits For An element to be loaded in the DOM. This function will wait for the element for the specified TimeSpan.
        /// </summary>
        /// <param name="driver">Extension method type, IWebDriver</param>
        /// <param name="by">The By Locator. </see></param>
        /// <param name="timeout">The amount of time to wait for the element to load before returning.</param>   
        /// <param name="suppressException">if <c>true</c>, the timeout will not throw an exception. (Default is <c>true</c>)</param> 
        public static void WaitForElement(this IWebDriver driver, By by, TimeSpan timeout, bool suppressException = true)
        {
            try
            {
                var wait = new WebDriverWait(driver, timeout);
                wait.Until(ExpectedConditions.ElementIsVisible(by));
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("Element Not Found. " + e.Message);
                if (!suppressException)
                    throw e;
            }
            catch (WebDriverTimeoutException toe)
            {
                Console.WriteLine("Element Timeout Exception. " + toe.Message);
                if (!suppressException)
                    throw toe;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown. Please see Exception details " + ex.Message);
                if (!suppressException)
                    throw ex;
            }
        }

        /// <summary>
        /// FindElement  - Wait for the element for specified time or until it exists and returns the IWebElement
        /// </summary>
        /// <param name="driver">The driver - This web Driver</param>
        /// <param name="by">The by - Locator using By</param>
        /// <param name="timeout">The amount of time to wait for the element to load before returning.</param>       
        public static IWebElement FindElement(this IWebDriver driver, By by, TimeSpan timeout)
        {
            var wait = new WebDriverWait(driver, timeout);
            return wait.Until(drv => driver.FindElement(@by).Displayed ? driver.FindElement(@by) : null);
        }

        /// <summary>
        /// FindElements  - Waits for the elements for specified time or until they exist and returns the IWebElement Collection
        /// </summary>
        /// <param name="driver">The driver - This web Driver</param>
        /// <param name="by">The by - Locator using By</param>
        /// <param name="timeout">The amount of time to wait for the elements to load before returning.</param>       
        public static IReadOnlyCollection<IWebElement> FindElements(this IWebDriver driver, By by, TimeSpan timeout)
        {
            var wait = new WebDriverWait(driver, timeout);
            return wait.Until(drv => (driver.FindElements(@by).Count > 0) ? driver.FindElements(@by) : null);
        }

        public static void switchToNewWindow(this IWebDriver driver)
        {
            mainWindow = driver.CurrentWindowHandle;
            String popupWindow = null;
            ReadOnlyCollection<String> handles =driver.WindowHandles;

            foreach (String windowHandle in handles)
            {
                if(windowHandle!=mainWindow)
                {
                    popupWindow = windowHandle;
                    break;
                }

            }
            driver.SwitchTo().Window(popupWindow);
        }

        public static void switchBackToMainWindow(this IWebDriver driver)
        {
            driver.Close();
            driver.SwitchTo().Window(mainWindow);
        }
        #endregion
    }
}
