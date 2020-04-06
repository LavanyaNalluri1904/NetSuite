using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.UIHandlers.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Engine.Setup;
using AUT.Selenium.CommonReUsablePages;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace AUT.Selenium.ApplicationSpecific.MAW.Pages
{
    public class ThankYouPage : AbstractTemplatePage
    {
        #region UI Object Repository
        private By imgMakeAWish = By.XPath("//img[@alt='Make a Wish']"); 
        private By lblThankYou = By.Id("body_1_h1PageTitle"); //Thank You! message
        private By btnPrintReceipt = By.XPath("//button[contains(text(),'Print Receipt')]");
        private By lblDonationType = By.XPath("//div[@class='repeater-table-row']//span[@class='no-wrap']"); //Single Donation
        private By lblDonationAmount = By.XPath("//div[@class='repeater-table-row']//h5[@class='ty-currency']");//$5
        private By lblChapter = By.XPath("//div[@class='repeater-table-row']//p[@class='repeater-chapter']"); //Make-A-Wish® America
        private By lblHTMLReceipt = By.XPath("//div[@class='repeater-table-row']");
        //Solomon Huynh Huynh
        //Transaction ID: 9726
        //Date: 4/19/17

        private By lnkTwitter = By.XPath("//a[@title='Share on Twitter']");
        private By lnkFacebook = By.XPath("//a[@title='Share on Facebook']");
        private By lnkSeeIfCompanyMatchesGift = By.XPath("//a[contains(text(),'See if your company will match your gift.')]");

        #endregion

        #region Public Methods

        #region Validate Thank You Page
        /// <summary>
        /// Method to validate Thank You Page
        /// </summary>
        public void validateThankYouPage(string amount="")
        {
            try
            {
                driver.WaitForElement(lblThankYou, TimeSpan.FromSeconds(60));
                //driver.CheckElementExists(lblThankYou, "THANK YOU!");
                driver.CheckElementExists(btnPrintReceipt, "PRINT RECEIPT");
                if(amount!="")
                    driver.VerifyTextValue(lblDonationAmount, amount, "Donation Amount");
                //driver.CheckElementExists(lblHTMLReceipt, "HTML Receipt");
                driver.WaitForPageLoad(TimeSpan.FromSeconds(60));
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateThankYouPage", "Error occurred while validating Thank You page", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Validate user transaction and refund
        /// <summary>
        /// Method to validate user transaction and refund
        /// </summary>
        /// <param name="transactionType"></param>
        public void validateUserTransactionAndRefund(string transactionType="",string amount="")
        {
            try
            {
                string transactionId = getTransactionId();

                if (transactionId != "")
                {
                    if (transactionType != "")
                    {
                        VerifyUserTransaction("", "", "", transactionId, "", "", "", "", "", "", "", "", "", transactionType);
                    }
                    else if (amount != "")
                    {
                        VerifyUserTransaction("", "", amount, transactionId);
                    }
                    else
                    {
                        VerifyUserTransaction("", "", "", transactionId);
                    }

                    RefundTransaction("", transactionId, "");
                }
                else
                {
                    this.TESTREPORT.LogFailure("validateUserTransactionAndRefund", "Error occurred while validating transaction! Transaction id not found", EngineSetup.GetScreenShotPath());

                }
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateUserTransactionAndRefund", "Error occurred while validating user transaction & refund", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion        

        #region Get Transaction Id on Thank You page
        /// <summary>
        /// Method to get Transaction Id on Thank You page
        /// </summary>
        /// <returns>transaction id</returns>
        public string getTransactionId()
        {
            try
            {
                //driver.CheckElementExists(lblHTMLReceipt, "HTML Receipt");
                string transactionDetails = driver.GetElementText(lblHTMLReceipt);
                string[] TransactionList = transactionDetails.Split('\n');
                string transactionId = "";
                foreach (string transaction in TransactionList)
                {
                    if (transaction.Contains("Transaction ID"))
                    {
                        transactionId = transaction.Replace("\r", "").Replace("Transaction ID: ", "");
                        this.TESTREPORT.LogSuccess("Transaction ID", String.Format("Donation Transaction ID: <mark>{0}</mark>",transactionId));
                        break;
                    }
                }

                return transactionId;
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion

        #region Validate User Transaction using LO API
        /// <summary>
        /// Method to validate User Transaction API
        /// </summary>
        public void validateUserTransaction(string transactionDate, string constituentId="", string amount = "", string transactionId = "", string creditCardNumber = "", string creditCardType = "", string billingFirstName = "", string billinglastName = "", string street1Address = "", string city = "", string state = "", string country = "", string postalCode = "",string transactionType="")
        {
            try
            {
                VerifyUserTransaction(transactionDate,constituentId,amount,transactionId,creditCardNumber,creditCardType,billingFirstName,billinglastName,street1Address,city,state,country,postalCode,transactionType);
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateUserTransaction", "Error occurred while validating User Transaction using LO API", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Validate Refund Transaction using LO API
        /// <summary>
        /// Method to validate Refund Transaction API
        /// </summary>
        public void validateRefundTransaction(string transactionDate, string transactionId = "", string constituentId = "")
        {
            try
            {
                RefundTransaction(transactionDate, transactionId,constituentId);
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateRefundTransaction", "Error occurred while validating RefundTransaction using LO API", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #endregion
    }
}
