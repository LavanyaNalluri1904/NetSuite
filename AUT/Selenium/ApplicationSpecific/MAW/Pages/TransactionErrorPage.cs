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
    public class TransactionErrorPage : AbstractTemplatePage
    {
        #region UI Object Repository
        private By imgLogo = By.XPath("//img[contains(@alt,'Make-A-Wish')]");
        private By lblPayPalError = By.CssSelector("h1#body_1_h1PageTitle");
        private By lnkTryAgain = By.XPath("//a[text()='Please try again']");
        #endregion

        #region Public Methods

        #region Verify Paypal error page
        /// <summary>
        /// Method to verify Paypal error page
        /// </summary>
        public void verifyPayPalErrorScreen()
        {
            try
            {
                driver.CheckElementExists(imgLogo, "Make A Wish image Logo");
                driver.VerifyTextValue(lblPayPalError, "PayPal Error", "Paypal Error");
                driver.CheckElementExists(lnkTryAgain, "Please Try Again");

            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("verifyPayPalScreen", "Failed to verify the PayPal screen", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #endregion
    }
}
