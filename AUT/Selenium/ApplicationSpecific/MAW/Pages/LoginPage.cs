using AUT.Selenium.CommonReUsablePages;
using Engine.Setup;
using Engine.UIHandlers.Selenium;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUT.Selenium.ApplicationSpecific.MAW.Pages
{
    public class LoginPage : AbstractTemplatePage
    {

        #region Constructors
        public LoginPage()
        { 
        }

        public LoginPage(IWebDriver driver)
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
        #endregion

    }
}
