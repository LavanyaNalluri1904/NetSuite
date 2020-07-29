using AUT.Selenium.CommonReUsablePages;
using Engine.Setup;
using Engine.UIHandlers.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUT.Selenium.ApplicationSpecific.MAW.Pages
{
    public class NetSuiteLoginPage : AbstractTemplatePage
    {

        #region Constructors
        public NetSuiteLoginPage()
        {
        }

        public NetSuiteLoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        #endregion

        #region UI Object Repository
        private By username = By.Id("userName");
        private By password = By.Id("password");
        private By loginIn = By.Id("submitButton");
        #endregion

        #region Public Methods
        public void NavigateToLoginPage(String url)
        {

            try
            {
                driver.Navigate().GoToUrl(url);
                this.TESTREPORT.LogSuccess("NavigateToLogin", String.Format("Navigated to url: <mark>{0}</mark> successfully", url));
                driver.CheckElementExists(loginIn, "Log In");
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("NavigateToLogin", "Failed to navigate to url:" + url, EngineSetup.GetScreenShotPath());
            }
        }

        public void EnterLoginCredentialsNavigateToHomePage(String usrname, String pswd)
        {
            try
            {
                driver.FindElement(username).SendKeys(usrname);
                driver.FindElement(password).SendKeys(pswd);
                driver.FindElement(loginIn).Click();
                driver.WaitForPageLoad(TimeSpan.FromSeconds(30));
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("Enter Login Credentials and verify the Home Page", "Failed to enter the credentials or Home page Verification", EngineSetup.GetScreenShotPath());
            }
        }

       
        #endregion

    }
}
