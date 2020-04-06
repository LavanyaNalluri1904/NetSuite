using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Setup;
using AUT.Selenium.CommonReUsablePages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Engine.UIHandlers.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AUT.Selenium.ApplicationSpecific.MAW.Pages
{
    public class DonationReviewPage : AbstractTemplatePage
    {

        #region UI Object Repository
        private By lblDonationDuration = By.ClassName("js--donation-duration-selected");
        private By lblDonationAmount = By.ClassName("js--donation-amount-selected");
        private By lblDonorBenifitting = By.ClassName("js--donor-intent-selected");
        private By btnReviewEdit = By.XPath("//div[@class='repeater-table hide--xs']//button[contains(text(),'Review/Edit')]");
        private By txtFirstName = By.Name("donor.name.first");
        private By txtLastName = By.Name("donor.name.last");
        private By chkDonateOnBehalfOfCommpany = By.Id("company-donation-toggle");
        private By txtCompanyName = By.Name("donor.employer");
        private By txtEmailAddress = By.Name("donor.email");
        private By txtStreetAddress1 = By.Name("donor.address.street1");
        private By txtStreetAddress2 = By.Name("donor.address.street2");
        private By txtPostalCode = By.Name("donor.address.zip");

        private By rbtnCreditCard = By.Id("payment-type-credit");
        private By rbtnPayPal = By.Id("payment-type-paypal");
        private By txtCreditCardNumber = By.Name("card_number");
        private By imgVisa = By.XPath("//img[@alt='Visa']");
        private By imgMasterCard = By.XPath("//img[@alt='MasterCard']");
        private By imgAmericanExpress = By.XPath("//img[@alt='American Express']");
        private By imgDiscover = By.XPath("//img[@alt='Discover']");
        private By listExpirationMonth = By.Name("card_exp_date_month");
        private By listExpirationYear = By.Name("card_exp_date_year");
        private By txtCvv = By.Name("card_cvv");

        private By btnSubmitDonation = By.XPath("//button[contains(text(),'Submit Donation')]");
        private By btnClearFormAndStartOver = By.XPath("//a[contains(text(),'Clear form and start over')]");

        #endregion

        #region Public Methods

        #region Validate donation review page controls
        /// <summary>
        /// Validate donation review page controls
        /// </summary>
        public void validateDonationReviewPageControls()
        {
            try
            {
                driver.CheckElementExists(lblDonationDuration, "Donation Duration");
                driver.CheckElementExists(lblDonationAmount, "Donation Amount");
                driver.VerifyTextValue(lblDonationAmount, "$100", "Donation Amount");
                driver.CheckElementExists(lblDonorBenifitting, "Donation Benifitting");
                driver.VerifyTextValue(lblDonorBenifitting, "Make-A-Wish® America", "Benefitting");
                driver.CheckElementExists(btnReviewEdit, "REVIEW/EDIT button");
                driver.ScrollPage();
                driver.CheckElementExists(txtFirstName, "First Name");
                driver.CheckElementExists(txtLastName, "Last Name");
                driver.CheckElementExists(chkDonateOnBehalfOfCommpany, "Donate on behalf of company");
                driver.ClickElement(chkDonateOnBehalfOfCommpany, "Donate on behalf of company");
                driver.CheckElementExists(txtCompanyName, "Company Name");
                driver.CheckElementExists(txtEmailAddress, "Email Address");
                driver.CheckElementExists(txtStreetAddress1, "Street Address 1");
                driver.CheckElementExists(txtStreetAddress2, "Street Address 2");
                driver.CheckElementExists(txtPostalCode, "Postal Code");
                driver.ScrollPage();
                driver.CheckElementExists(rbtnCreditCard, "Credit Card");
                driver.CheckElementExists(rbtnPayPal, "Paypal");
                driver.CheckElementExists(txtCreditCardNumber, "Credit Card Number");
                driver.ScrollPage();
                driver.CheckElementExists(imgVisa, "Visa");
                driver.CheckElementExists(imgMasterCard, "Master Card");
                driver.CheckElementExists(imgAmericanExpress, "American Express");
                driver.CheckElementExists(imgDiscover, "Discover");
                driver.CheckElementExists(listExpirationMonth, "Expiration Month");
                driver.CheckElementExists(listExpirationYear, "Expiration Year");
                driver.CheckElementExists(txtCvv, "CVV");
                driver.CheckElementExists(btnSubmitDonation, "Submit Donation");
                driver.CheckElementExists(btnClearFormAndStartOver, "Clear form and start over");


            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("ValidateDonationReviewPageControls", "Failed to validate controls on Donation review page", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Validate donation amount
        /// <summary>
        /// Validate donation amount 
        /// </summary>
        /// <param name="amount"></param>
        public void validateDonationAmount(string amount)
        {
            try
            {
                driver.VerifyTextValue(lblDonationAmount, amount, "Donation Amount");

            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateDonationAmount", "Failed to validate single donation amount", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Validate Benefitting chapter name
        /// <summary>
        /// Method to Validate Benefitting chapter name
        /// </summary>
        /// <param name="name"></param>
        public void validateBenefittingChapter(string name)
        {
            try
            {
                driver.VerifyTextValue(lblDonorBenifitting, name, "Benefitting Chapter");

            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateBenefittingChapter", "Failed to validate donor benefitting chapter name", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Review/Edit donation information
        /// <summary>
        /// Method to review/edit donation information
        /// </summary>
        public void reviewAndEditDonationInformation()
        {
            try
            {
                driver.ClickElement(btnReviewEdit, "Review/Edit button");
                driver.WaitForPageLoad(TimeSpan.FromSeconds(60));

            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("reviewAndEditDonationInformation", "Failed to click review/edit button", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Method to update YOUR INFORMATION section
        /// <summary>
        /// Method to update YOUR INFORMATION section
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="emailAddress"></param>
        /// <param name="streetAddress1"></param>
        /// <param name="postalCode"></param>
        /// <param name="streetAddress2"></param>
        public void updateYourInformation(string firstName, string lastName, string emailAddress, string streetAddress1, string postalCode, string streetAddress2 = "")
        {
            try
            {
                driver.ScrollPage();
                driver.SendKeysToElement(txtFirstName, firstName, "First Name");
                driver.SendKeysToElement(txtLastName, lastName, "Last Name");
                driver.SendKeysToElement(txtEmailAddress, emailAddress, "Email Address");
                driver.SendKeysToElement(txtStreetAddress1, streetAddress1, "Street Address 1");
                driver.SendKeysToElement(txtStreetAddress2, streetAddress2, "Street Address 2");
                driver.SendKeysToElement(txtPostalCode, postalCode, "Postal Code");
                driver.ScrollPage();

            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("updateYourInformation", "Failed to update YOUR INFORMATION section", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Method to update BILLING INFORMATION section
        /// <summary>
        /// Method to update BILLING INFORMATION section
        /// </summary>
        /// <param name="paymentType"></param>
        /// <param name="creditCardNumber"></param>
        /// <param name="expirationMonth"></param>
        /// <param name="expirationYear"></param>
        /// <param name="cvv"></param>
        public void updateBillingInformation(string paymentType, string creditCardNumber = "", string expirationMonth = "", string expirationYear = "", string cvv = "")
        {
            try
            {
                if (paymentType == "CREDIT CARD")
                {
                    driver.ScrollPage();
                    driver.ClickElement(rbtnCreditCard, "CREDIT CARD");
                    driver.SendKeysToElement(txtCreditCardNumber, creditCardNumber, "Credit Card Number");
                    driver.SelectDropdownItemByText(listExpirationMonth, expirationMonth, "Expiration Month");
                    driver.SelectDropdownItemByText(listExpirationYear, expirationYear, "Expiration Year");
                    driver.SendKeysToElement(txtCvv, cvv, "CVV number");
                }
                else
                {
                    driver.ScrollPage();
                    driver.ClickElement(rbtnPayPal, "PAYPAL");
                }
                driver.ScrollPage();

            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("updateBillingInformation", "Failed to update billing information section", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Submit Donation
        /// <summary>
        /// Method to submit donation
        /// </summary>
        public void submitDonation()
        {
            try
            {
                driver.ScrollToPageBottom();
                driver.ClickElement(btnSubmitDonation, "SUBMIT DONATION");
                if (driver.isAlertPresent())
                {
                    IAlert devAlert = driver.SwitchTo().Alert();
                    devAlert.SendKeys("testdev");
                    devAlert.Accept();
                }
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("submitDonation", "Failed to submit donation", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Verify invalid credit card number error
        /// <summary>
        /// Method to verify invalid credit card number
        /// </summary>
        public void verifyErrorCreditCardNumber()
        {
            try
            {

                driver.WaitForPageLoad(TimeSpan.FromSeconds(60));
                driver.ValidateRequiredFieldError(txtCreditCardNumber, "Credit Card Number", "class", "input-error");
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("verifyErrorCreditCardNumber", "Failed to verify the error Highlight of CreditCardNumber", EngineSetup.GetScreenShotPath());
            }

            
        }
        #endregion

        #region  Verify YOUR INFORMATION field errors
        /// <summary>
        /// Method to verify YOUR INFORMATION field errors
        /// </summary>
        public void verifyYourInformationFieldErrors()
        {
            try
            {
                driver.ScrollPage();
                driver.ValidateRequiredFieldError(txtFirstName, "First Name", "class", "input-error");
                driver.ValidateRequiredFieldError(txtLastName, "Last Number", "class", "input-error");
                driver.ValidateRequiredFieldError(txtEmailAddress, "Email Address", "class", "input-error");
                driver.ValidateRequiredFieldError(txtStreetAddress1, "Street Address 1", "class", "input-error");
                driver.ValidateRequiredFieldError(txtPostalCode, "Postal Code", "class", "input-error");                
                driver.ScrollPage();

            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("verifyYourInformationFieldErrors", "Failed to verify the Required field errors for YourInformation fields", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region validate Billing Information required field errors
        /// <summary>
        /// Method to validate Billing Information required field errors
        /// </summary>
        public void VerifyBillingSectionFieldsErrors()
        {
            try
            {
                driver.WaitForPageLoad(TimeSpan.FromSeconds(30));
                driver.ValidateRequiredFieldError(txtCreditCardNumber, "Credit Card", "class", "input-error");
                driver.ValidateRequiredFieldError(listExpirationMonth, "Expiration Month", "class", "input-error");
                driver.ValidateRequiredFieldError(listExpirationYear, "Expiration Year", "class", "input-error");
                driver.ValidateRequiredFieldError(txtCvv, "CVV", "class", "input-error");
                driver.ScrollPage();

            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("verifyBillingSectionFieldsErrors", "Failed to verify the error Highlight of YourInformation", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion 

        #region click clear form and start over
        /// <summary>
        /// Method to click clear form and start over
        /// </summary>
        public void ClickClearformandstartoverbutton()
        {
            try
            {
                driver.ScrollToPageBottom();
                driver.ClickElement(btnClearFormAndStartOver, "Clearformandstartoverbutton");
                driver.WaitForPageLoad(TimeSpan.FromSeconds(60));

            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("ClickClearformandstartoverbutton", "Failed to Clearformandstartover", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #endregion
    }
}
