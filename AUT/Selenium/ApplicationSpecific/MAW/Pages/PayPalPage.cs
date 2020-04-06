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
    public class PayPalPage : AbstractTemplatePage
    {

        #region UI Object Repository
        private By imgPaypalLogo = By.Id("paypalLogo");
        private By lblMerchantName = By.CssSelector(".merchantName.ng-binding.ng-scope");
        private By lblTotalPrice = By.CssSelector("#totalWrapper format-currency");
        private By framePaypalLogin = By.XPath("//iframe[@title='PayPal - Log In']");
        private By lnkCancelLink = By.Id("cancelLink");
        private By txtEmail = By.XPath("//input[@id='email']");  //By.Name("login_email");
        private By txtPassword = By.XPath("//input[@id='password']"); //By.Name("login_password");
        //private By txtEmail = By.XPath("//input[@id='email']");  //By.Name("login_email");
        //private By txtPassword = By.XPath("//input[@id='password']"); //By.Name("login_password");
        private By btnLogin = By.Id("btnLogin");
        private By lblPaypalLogo = By.ClassName("paypal-logo paypal-logo-long");
        private By btnContinue = By.Id("confirmButtonTop");


        #endregion

        #region Public Methods

        #region Validate Paypal billing screen
        /// <summary>
        /// Method to verify Paypal screen
        /// </summary>
        /// <param name="merchantName"></param>
        /// <param name="totalPrice"></param>
        public void verifyPayPalScreen()
        {
            try
            {
                driver.sleepTime(10000);
                WaitTillElementExists(lnkCancelLink);
                if (!driver.IsElementPresent(btnContinue))
                {
                    driver.SwitchToFrameByLocator(framePaypalLogin);
                    driver.CheckElementExists(txtEmail, "Email");
                    driver.CheckElementExists(txtPassword, "Password");
                    driver.CheckElementExists(btnLogin, "Login");
                    driver.SwitchToDefaultFrame();
                    driver.ScrollToPageBottom();
                    driver.CheckElementExists(lnkCancelLink, "PayPal Cancel link");
                }
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("verifyPayPalScreen", "Failed to verify the PayPal screen", EngineSetup.GetScreenShotPath());
            }

        }
        
        #endregion

        #region Method to cancel paypal transaction
        /// <summary>
        /// Method to cancel paypal transaction
        /// </summary>
        public void cancelPaypalTransaction()
        {
            try
            {
                driver.ScrollPage();
                driver.ClickElement(lnkCancelLink, "PayPal Cancel Link");
                if (driver.isAlertPresent())
                {
                    IAlert devAlert = driver.SwitchTo().Alert();
                    devAlert.SendKeys("testdev");
                    devAlert.Accept();
                }
                driver.WaitForPageLoad(TimeSpan.FromSeconds(60));

            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("cancelPaypalTransaction", "Failed to cancel Paypal transaction", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion 


        #region Method to validate paypal transaction
        /// <summary>
        /// Method to validate paypal transaction
        /// </summary>
        /// <param name="emailId">EMail id</param>
        /// <param name="password">Password</param>
        public void validatePaypalTransaction(string emailId, string password)
        {
            try
            {
                driver.sleepTime(10000);
                if (driver.IsElementPresent(framePaypalLogin))
                {
                    driver.SwitchToFrameByLocator(framePaypalLogin);
                    driver.SendKeysToElement(txtEmail, emailId, "Email");
                    driver.SendKeysToElement(txtPassword, password, "Email");
                    driver.ClickElement(btnLogin, "Login");
                    driver.SwitchToDefaultFrame();
                    driver.sleepTime(10000);
                }

                driver.WaitForPageLoad(TimeSpan.FromSeconds(30));
                driver.WaitElementExistsAndVisible(btnContinue);
                driver.CheckElementExists(lnkCancelLink, "Cancel");
                driver.ClickElement(btnContinue, "Continue");

                if (driver.isAlertPresent())
                {
                    IAlert devAlert = driver.SwitchTo().Alert();
                    devAlert.SendKeys("testdev");
                    devAlert.Accept();
                }
                driver.WaitForPageLoad(TimeSpan.FromSeconds(60));

            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validatePaypalTransaction", "Failed to validate Paypal transaction", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion 

        #endregion
    }
}
