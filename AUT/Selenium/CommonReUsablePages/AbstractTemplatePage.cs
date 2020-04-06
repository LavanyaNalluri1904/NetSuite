using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Engine.Factories;
using Engine.UIHandlers.Selenium;
using TestReporter;
using Engine.Setup;
using OpenQA.Selenium;
using System.Windows.Forms;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using AUT.Selenium.ApplicationSpecific.MAW.Pages;
using System.Text.RegularExpressions;

namespace AUT.Selenium.CommonReUsablePages
{
    public abstract class AbstractTemplatePage
    {
        protected IWebDriver driver = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractTemplatePage"/> class.
        /// </summary>
        protected AbstractTemplatePage()
        {
            this.driver = WebDriverFactory.getWebDriver();
           
        }

        /// <summary>
        /// Gets the testreport.
        /// </summary>
        /// <value>
        /// The testreport.
        /// </value>
        protected IReporter TESTREPORT
        {
            get { return EngineSetup.TestReport; }
        }

        /// <summary>
        /// Gets the screenshotfile.
        /// </summary>
        /// <value>
        /// The screenshotfile.
        /// </value>
        protected string SCREENSHOTFILE
        {
            get { return EngineSetup.GetScreenShotPath(); }
        }

        /// <summary>
        /// Method to verify User transaction with the data returned by LO API
        /// </summary>
        /// <param name="transactionDate">Transaction Date YYYY-MM-DD</param>
        /// <param name="constituentId">Constituent Id</param>
        /// <param name="amount">Transaction amount '$5.00'</param>
        /// <param name="transactionId">Transaction id</param>
        /// <param name="creditCardNumber">Credit Card Number '************1111'</param>
        /// <param name="creditCardType">Credit Card Type 'VISA'</param>
        /// <param name="billingFirstName">Billing First Name</param>
        /// <param name="billinglastName">Billing Last Name</param>
        /// <param name="street1Address">Street Address 1</param>
        /// <param name="city">City</param>
        /// <param name="state">State</param>
        /// <param name="country">Country</param>
        /// <param name="postalCode">Postal Code</param>
        /// <param name="transactionType">Transaction Type - DONATION or RECURRING_GIFT</param>
        protected void VerifyUserTransaction(string transactionDate = "", string constituentId = "", string amount = "", string transactionId = "", string creditCardNumber = "", string creditCardType = "", string billingFirstName = "", string billinglastName = "", string street1Address = "", string city = "", string state = "", string country = "", string postalCode = "", string transactionType = "")
        {
            try
            {
                DateTime date;
                if (transactionDate == "")
                {
                    transactionDate = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (!DateTime.TryParse(transactionDate, out date))
                {
                    this.TESTREPORT.LogFailure("VerifyUserTransaction", String.Format("Error while invoking User Transaction API, please pass valid transaction date"));
                    return;
                }
                if (constituentId == "")
                {
                    constituentId = EngineSetup.CONSTITUENTID;
                }

                RestClient client;
                if (driver.Url.Contains("wishdev"))
                    client = new RestClient(EngineSetup.DEVAPIURL + "/SRConsAPI?method=getUserTransactions&api_key=" + EngineSetup.APIKEY + "&v=1.0&login_name=" + EngineSetup.LOGINNAME + "&login_password=" + EngineSetup.LOGINPASSWORD + "&cons_id=" + constituentId + "&begin_date=" + transactionDate + "&end_date=" + Convert.ToDateTime(transactionDate).AddDays(1).ToString("yyyy-MM-dd") + "&response_format=json");
                else
                    client = new RestClient(EngineSetup.PRODAPIURL + "/SRConsAPI?method=getUserTransactions&api_key=" + EngineSetup.APIKEY + "&v=1.0&login_name=" + EngineSetup.LOGINNAME + "&login_password=" + EngineSetup.LOGINPASSWORD + "&cons_id=" + constituentId + "&begin_date=" + transactionDate + "&end_date=" + Convert.ToDateTime(transactionDate).AddDays(1).ToString("yyyy-MM-dd") + "&response_format=json");


                var request = new RestRequest(Method.POST);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
                string jsonString = response.Content;
                if (jsonString.Contains("errorResponse") || !jsonString.Contains("getConsTransactionsResponse"))
                {
                    this.TESTREPORT.LogFailure("VerifyUserTransaction", String.Format("Error while invoking User Transaction API, Error details:{0}", jsonString));
                    return;
                }
                JObject consResponse = JObject.Parse(jsonString);
                string transaction = consResponse["getConsTransactionsResponse"].ToString();
                JObject objTransaction = JObject.Parse(transaction);
                if (!objTransaction.ToString().Equals("{}"))
                {
                    transaction = objTransaction["transaction"].ToString();

                    UserTransaction.RootObject lastTransaction;
                    UserTransaction.RootObject[] transactionList;

                    if (transaction.StartsWith("["))
                    {
                        //multiple transactions
                        transactionList = JsonConvert.DeserializeObject<UserTransaction.RootObject[]>(transaction);
                        lastTransaction = transactionList[transactionList.Length - 1];

                    }
                    else
                    {
                        //single transaction
                        lastTransaction = JsonConvert.DeserializeObject<UserTransaction.RootObject>(transaction);
                    }


                    if (amount != "") //6.00
                    {
                        if (!amount.Contains("."))
                            amount = amount + ".00";//append 00 if not available
                        if (amount.Contains("$"))
                            amount = amount.Replace("$", "");//append 00 if not available
                        if (lastTransaction.amount.@decimal.Equals(amount))
                            logResult("Amount", amount, lastTransaction.amount.@decimal);
                        else
                            logResult("Amount", amount, lastTransaction.amount.@decimal, "Fail");
                    }

                    if (transactionId != "") //9930
                    {
                        if (lastTransaction.confirmation_code.EndsWith(transactionId))
                            logResult("Transaction Id", transactionId, lastTransaction.confirmation_code);
                        else
                            logResult("Transaction Id", transactionId, lastTransaction.confirmation_code, "Fail");
                    }

                    if (creditCardNumber != "") //************1234
                    {
                        if (lastTransaction.credit_card_number.Equals(creditCardNumber))
                            logResult("Credit Card Number", creditCardNumber, lastTransaction.credit_card_number);
                        else
                            logResult("Credit Card Number", creditCardNumber, lastTransaction.credit_card_number, "Fail");
                    }

                    if (creditCardType != "") //VISA
                    {
                        if (lastTransaction.credit_card_type.Equals(creditCardType))
                            logResult("Credit Card Type", creditCardType, lastTransaction.credit_card_type);
                        else
                            logResult("Credit Card Type", creditCardType, lastTransaction.credit_card_type, "Fail");

                    }

                    if (billingFirstName != "") //Solomon Huynh
                    {
                        if (lastTransaction.billing_name.first.Equals(billingFirstName))
                            logResult("Billing First Name", billingFirstName, lastTransaction.billing_name.first);
                        else
                            logResult("Billing First Name", billingFirstName, lastTransaction.billing_name.first, "Fail");
                    }

                    if (billinglastName != "") //Huynh
                    {
                        if (lastTransaction.billing_name.last.Equals(billinglastName))
                            logResult("Billing Last Name", billinglastName, lastTransaction.billing_name.last);
                        else
                            logResult("Billing Last Name", billinglastName, lastTransaction.billing_name.last, "Fail");
                    }

                    if (street1Address != "") //4742 N 24th Street, Suite 400
                    {
                        if (lastTransaction.billing_address.street1.Equals(street1Address))
                            logResult("Street Address 1", street1Address, lastTransaction.billing_address.street1);
                        else
                            logResult("Street Address 1", street1Address, lastTransaction.billing_address.street1, "Fail");
                    }

                    if (city != "") //Phoenix
                    {
                        if (lastTransaction.billing_address.city.Equals(city))
                            logResult("City", city, lastTransaction.billing_address.city);
                        else
                            logResult("City", city, lastTransaction.billing_address.city, "Fail");
                    }

                    if (state != "") //AZ
                    {
                        if (lastTransaction.billing_address.state.Equals(state))
                            logResult("State", state, lastTransaction.billing_address.state);
                        else
                            logResult("State", state, lastTransaction.billing_address.state, "Fail");
                    }

                    if (country != "") //United States
                    {
                        if (lastTransaction.billing_address.country.Equals(country))
                            logResult("Country", country, lastTransaction.billing_address.country);
                        else
                            logResult("Country", country, lastTransaction.billing_address.country, "Fail");
                    }

                    if (postalCode != "") //85016
                    {
                        if (lastTransaction.billing_address.zip.Equals(postalCode))
                            logResult("Postal Code", postalCode, lastTransaction.billing_address.zip);
                        else
                            logResult("Postal Code", postalCode, lastTransaction.billing_address.zip, "Fail");
                    }
                    //DONATION or RECURRING_GIFT
                    if (transactionType != "")
                    {
                        if (lastTransaction.transaction_type.Equals(transactionType))
                            logResult("Transaction Type", transactionType, lastTransaction.transaction_type);
                        else
                            logResult("Transaction Type", transactionType, lastTransaction.transaction_type, "Fail");
                    }

                }
                else
                {
                    this.TESTREPORT.LogFailure("VerifyUserTransaction", String.Format("No Transaction found in L.O!!"));
                }

            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("VerifyUserTransaction", String.Format("Error occurred while validating last user transaction"));
                this.TESTREPORT.LogException(ex, this.SCREENSHOTFILE);
            }

        }

        /// <summary>
        /// Method used for reporting API results
        /// </summary>
        /// <param name="fieldName">API field Name</param>
        /// <param name="expectedValue">Expected Value</param>
        /// <param name="actualValue">LO Transaction Value</param>
        /// <param name="status">Pass/Fail</param>
        private void logResult(string fieldName, string expectedValue, string actualValue, string status = "Pass")
        {
            if (status.Equals("Pass"))
                this.TESTREPORT.LogSuccess("VerifyUserTransaction - " + fieldName, String.Format("{0}: <mark>{1}</mark> matches with the L.O transaction {2}: <mark>{3}</mark>", fieldName, expectedValue, fieldName, actualValue));
            else
                this.TESTREPORT.LogFailure("VerifyUserTransaction - " + fieldName, String.Format("{0}: <mark>{1}</mark> does not match with the L.O transaction {2}: <mark>{3}</mark>", fieldName, expectedValue, fieldName, actualValue));

        }

        /// <summary>
        /// Method to refund a donation transaction
        /// </summary>
        /// <param name="transactionDate">Date of transaction. If blank, the current date will be taken</param>
        /// <param name="transactionId">last digits of Transaction id. If blank, the last transaction of the day will be refunded</param>
        /// <param name="constituentId">Consituent id. If blank, constituent id from testconfiguration file will be taken</param>
        protected void RefundTransaction(string transactionDate = "", string transactionId = "", string constituentId = "")
        {
            try
            {

                DateTime date;
                if (transactionDate == "")
                {
                    transactionDate = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (!DateTime.TryParse(transactionDate, out date))
                {
                    this.TESTREPORT.LogFailure("refundTransaction", String.Format("Error while invoking User Transaction API, please pass valid transaction date"));
                    return;
                }
                if (constituentId == "")
                {
                    constituentId = EngineSetup.CONSTITUENTID;
                }

                RestClient client;
                if (driver.Url.Contains("wishdev"))
                    client = new RestClient(EngineSetup.DEVAPIURL + "/SRConsAPI?method=getUserTransactions&api_key=" + EngineSetup.APIKEY + "&v=1.0&login_name=" + EngineSetup.LOGINNAME + "&login_password=" + EngineSetup.LOGINPASSWORD + "&cons_id=" + constituentId + "&begin_date=" + transactionDate + "&end_date=" + Convert.ToDateTime(transactionDate).AddDays(1).ToString("yyyy-MM-dd") + "&response_format=json");
                else
                    client = new RestClient(EngineSetup.PRODAPIURL + "/SRConsAPI?method=getUserTransactions&api_key=" + EngineSetup.APIKEY + "&v=1.0&login_name=" + EngineSetup.LOGINNAME + "&login_password=" + EngineSetup.LOGINPASSWORD + "&cons_id=" + constituentId + "&begin_date=" + transactionDate + "&end_date=" + Convert.ToDateTime(transactionDate).AddDays(1).ToString("yyyy-MM-dd") + "&response_format=json");


                var request = new RestRequest(Method.POST);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
                string jsonString = response.Content;
                if (jsonString.Contains("errorResponse") || !jsonString.Contains("getConsTransactionsResponse"))
                {
                    this.TESTREPORT.LogFailure("refundTransaction", String.Format("Error while invoking User Transaction API, Error details:{0}", jsonString));
                    return;
                }
                JObject consResponse = JObject.Parse(jsonString);
                string transaction = consResponse["getConsTransactionsResponse"].ToString();
                JObject objTransaction = JObject.Parse(transaction);
                string confirmation_code = "";
                if (!objTransaction.ToString().Equals("{}"))
                {
                    transaction = objTransaction["transaction"].ToString();

                    UserTransaction.RootObject lastTransaction;
                    UserTransaction.RootObject[] transactionList;

                    if (transaction.StartsWith("["))
                    {
                        //multiple transactions
                        transactionList = JsonConvert.DeserializeObject<UserTransaction.RootObject[]>(transaction);
                        lastTransaction = transactionList[transactionList.Length - 1];

                    }
                    else
                    {
                        //single transaction
                        lastTransaction = JsonConvert.DeserializeObject<UserTransaction.RootObject>(transaction);
                    }

                    confirmation_code = lastTransaction.confirmation_code;

                    if (transactionId != "") //9930
                    {
                        if (lastTransaction.confirmation_code.EndsWith(transactionId))
                        {
                            logResult("Transaction Id", transactionId, lastTransaction.confirmation_code);
                        }
                        else
                        {
                            logResult("Transaction Id", transactionId, lastTransaction.confirmation_code, "Fail");
                            confirmation_code = "";

                        }
                    }

                    if (confirmation_code != "")
                    {

                        RestClient refundClient;
                        if (driver.Url.Contains("wishdev"))
                        {
                            refundClient = new RestClient(EngineSetup.DEVAPIURL + "/SRDonationAPI?method=refundTransaction&api_key=" + EngineSetup.APIKEY + "&v=1.0&login_name=" + EngineSetup.LOGINNAME + "&login_password=" + EngineSetup.LOGINPASSWORD + "&refund_transaction_request=<refundRequest xmlns=\"http://convio.com/crm/v1.0\"><donRefundInfoList><donRefundInfo><refund_type>full</refund_type><user_confirmation_code>" + confirmation_code + "</user_confirmation_code></donRefundInfo></donRefundInfoList></refundRequest>");
                        }
                        else
                        {
                            refundClient = new RestClient(EngineSetup.PRODAPIURL + "/SRDonationAPI?method=refundTransaction&api_key=" + EngineSetup.APIKEY + "&v=1.0&login_name=" + EngineSetup.LOGINNAME + "&login_password=" + EngineSetup.LOGINPASSWORD + "&refund_transaction_request=<refundRequest xmlns=\"http://convio.com/crm/v1.0\"><donRefundInfoList><donRefundInfo><refund_type>full</refund_type><user_confirmation_code>" + confirmation_code + "</user_confirmation_code></donRefundInfo></donRefundInfoList></refundRequest>");
                        }


                        var refundRequest = new RestRequest(Method.POST);
                        IRestResponse refundResponse = refundClient.Execute(refundRequest);

                        if (jsonString.Contains("errorResponse"))
                        {
                            this.TESTREPORT.LogFailure("refundTransaction", String.Format("Error response received from RefundTransaction API, Error details:{0}", jsonString));
                            return;
                        }

                        var refundTransactionId = Regex.Match(refundResponse.Content, @"(<refund_transaction_id>)\w*").Value;
                        refundTransactionId = refundTransactionId.Replace("<refund_transaction_id>", "");

                        if (refundResponse.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            this.TESTREPORT.LogSuccess("refundTransaction", String.Format("User transaction with confirmation code: <mark>{0}</mark>, has been refunded successfully. Refund Transaction id: <mark>{1}</mark>", confirmation_code, refundTransactionId));

                        }

                        //Console.WriteLine(refundResponse.Content);
                    }
                    else
                    {
                        this.TESTREPORT.LogFailure("refundTransaction", String.Format("No Transactions found in L.O!!"));
                        return;
                    }

                }
                else
                {
                    this.TESTREPORT.LogFailure("refundTransaction", String.Format("No Transactions found in L.O!!"));
                }

            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("refundTransaction", String.Format("Error occurred while refunding latest transaction"));
                this.TESTREPORT.LogException(ex, this.SCREENSHOTFILE);
            }

        }

        /// <summary>
        /// Validates the not null or empty.
        /// </summary>
        /// <param name="text">The text.</param>
        protected void ValidateNotNullOrEmpty(string text)
        {
            this.TESTREPORT.LogInfo(String.Format("Text - {0} Will Be Verified For Not Null And Empty", text));
            if (string.IsNullOrEmpty(text)) //fail
            {
                this.TESTREPORT.LogFailure("ValidateNotNullOrEmpty", String.Format("Expected Text - NotNullOrEmpty, Actual Text - <mark>{0}</mark> ", text), this.SCREENSHOTFILE);
            }
            else
            {
                this.TESTREPORT.LogSuccess("ValidateNotNullOrEmpty", String.Format("Expected Text - NotNullOrEmpty, Actual Text - <mark>{0}</mark> ", text));
            }
            this.TESTREPORT.LogInfo(String.Format("Text - {0} Was Verified For Not Null And Empty", text));
        }

        
        /// <summary>
        /// Waits the till element exists.
        /// </summary>
        /// <param name="by">The by.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="timeOutInSecs">The time out in secs.</param>
        protected void WaitTillElementExists(By by, string locator = "", int timeOutInSecs = EngineSetup.TimeOutConstant)
        {
            try
            {
                WebDriverWait webDriverCondWait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(timeOutInSecs));
                try
                {
                    webDriverCondWait.Until(ExpectedConditions.ElementExists(by));
                    this.driver.Highlight(by);

                    this.TESTREPORT.LogSuccess("WaitTillElementExists", String.Format("Control <mark>{0}</mark> - <mark>{1}</mark> Was Found Within <mark>{2}</mark> Secs", locator, by.ToString(), timeOutInSecs));

                }
                catch (WebDriverTimeoutException)
                {

                    this.TESTREPORT.LogInfo(String.Format("Control <mark>{0}</mark> - <mark>{1}</mark> Was Not Found Within <mark>{2}</mark> Secs", locator, by.ToString(), timeOutInSecs));
                }

            }
            catch (Exception ex)
            {
               
                this.TESTREPORT.LogException(ex,this.SCREENSHOTFILE);
            }
        }

        /// <summary>
        /// Waits the till element disappeared.
        /// </summary>
        /// <param name="by">The by.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="timeOut">The time out.</param>
        protected void WaitTillElementDisappeared(By by, string locator = "", int timeOut = EngineSetup.TimeOutConstant)
        {
            try
            {
                //if locator english form not provided then convert given object identification to english form
                locator = locator.Trim().Equals("") ? by.ToString() : locator;

                WebDriverWait webDriverCondWait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(timeOut));
                try
                {
                    webDriverCondWait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));

                    this.TESTREPORT.LogSuccess("WaitTillElementDisappeared", String.Format("Expected Element <mark>{0}</mark> Disappeared Within <mark>{1}</mark> Secs", locator, timeOut));

                }
                catch (WebDriverTimeoutException)
                {

                    this.TESTREPORT.LogFailure("WaitTillElementDisappeared", String.Format("Expected Element To Disappear, but Element <mark>{0}</mark> still exists After <mark>{1}</mark> Secs", locator, timeOut), this.SCREENSHOTFILE);

                }
            }
            catch (Exception ex)
            {

                this.TESTREPORT.LogException(ex);
            }


        }

        /// <summary>
        /// Verifies the values against by objects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyPairTemplateNameExpectedValue">The key pair template name expected value.</param>
        /// <param name="objectIdPattern">The object identifier pattern.</param>
        /// <param name="timeOutInSecs">The time out in secs.</param>
        /// <param name="isPageRefreshRequired">if set to <c>true</c> [is page refresh required].</param>
        protected virtual void VerifyValuesAgainstByObjects<T>(Dictionary<T, string> keyPairTemplateNameExpectedValue, string objectIdPattern, int timeOutInSecs = 5, bool isPageRefreshRequired = false)
        {

            foreach (KeyValuePair<T, string> kvPair in keyPairTemplateNameExpectedValue)
            {

                try
                {
                    //get labelName from keyvalue pair                
                    object label = kvPair.Key;
                    //create By Locator at runtime
                    By by = null;
                    if (label is string)
                    {
                        by = By.XPath(String.Format(objectIdPattern, (string)label));
                    }
                    else if (label is By)
                    {
                        by = (By)label;
                    }

                    //get expected value from keyvalue pair
                    string expectedValue = kvPair.Value;
                    this.WaitTillElementExists(by, "", timeOutInSecs);
                    if (this.driver.FindElements(by).Count > 0)
                    {
                        this.driver.Highlight(by);
                        try
                        {
                            //get actual value
                            string actualValue = this.driver.GetElementText(by);

                            //log the result
                            if (!actualValue.Trim().Contains(expectedValue.Trim()) && isPageRefreshRequired == false) //fail
                            {

                                this.TESTREPORT.LogFailure("VerifyValuesAgainstByObjects", String.Format("Content In Control - <mark>{0}</mark> Does Not Match With Expected Within <mark>{3}</mark> Secs. Expected - <mark>{1}</mark> , Actual - <mark>{2}</mark>", by.ToString(), expectedValue, actualValue, timeOutInSecs), this.SCREENSHOTFILE);
                            }
                            else if (!actualValue.Trim().Contains(expectedValue.Trim()) && isPageRefreshRequired == true)
                            {
                                int MAXTIMEOUT = timeOutInSecs * 1000;
                                int elapsedTimeout = 0;
                                while (!actualValue.Trim().Contains(expectedValue.Trim()) && elapsedTimeout < MAXTIMEOUT)
                                {
                                    this.driver.RefreshPage();
                                    this.WaitTillElementExists(by, "", timeOutInSecs);
                                    actualValue = this.driver.GetElementText(by);
                                    elapsedTimeout = elapsedTimeout + 1000;
                                }
                                if (!actualValue.Trim().Contains(expectedValue.Trim()))
                                {
                                    this.TESTREPORT.LogFailure("VerifyValuesAgainstByObjects", String.Format("Content In Control - <mark>{0}</mark> Does Not Match With Expected Within <mark>{3}</mark> Secs. Expected - <mark>{1}</mark> , Actual - <mark>{2}</mark>", by.ToString(), expectedValue, actualValue, timeOutInSecs), this.SCREENSHOTFILE);
                                }
                            }
                            else //pass
                            {

                                this.TESTREPORT.LogSuccess("VerifyValuesAgainstByObjects", String.Format("Content In Control - <mark>{0}</mark> Matches With Expected Within <mark>{3}</mark> Secs. Expected - <mark>{1}</mark> , Actual - <mark>{2}</mark>", by.ToString(), expectedValue, actualValue, timeOutInSecs));
                            }
                        }
                        catch (Exception ex)
                        {
                           
                            this.TESTREPORT.LogException(ex);
                        }
                    }
                    else
                    {
                       
                        this.TESTREPORT.LogFailure("VerifyValuesAgainstByObjects", String.Format("The Element - <mark>{0}</mark> Was Not Found", by.ToString()), this.SCREENSHOTFILE);
                    }
                }
                catch (Exception ex)
                {
                    this.TESTREPORT.LogException(ex);
                }

            }
            keyPairTemplateNameExpectedValue.Clear();
        }

        /// <summary>
        /// Verifies the objects presence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listObjects">The list objects.</param>
        /// <param name="timeOutInSecs">The time out in secs.</param>
        /// <param name="isPageRefreshRequired">if set to <c>true</c> [is page refresh required].</param>
        protected virtual void VerifyObjectsPresence<T>(List<T> listObjects, int timeOutInSecs = 5, bool isPageRefreshRequired = false)
        {
            foreach (T listItem in listObjects)
            {

                try
                {
                    
                    By by = null;
                    if (listItem is string)
                    {
                        by = By.XPath(String.Format("{0}",listItem));
                    }
                    else if (listItem is By)
                    {
                        object o = listItem;
                        by = (By)o;
                    }

                    //get expected value from keyvalue pair
                   
                    this.WaitTillElementExists(by, by.ToString(), timeOutInSecs);
                    if (this.driver.FindElements(by).Count > 0)
                    {
                        this.driver.Highlight(by);
                        try
                        {
                            
                            //log the result
                            if (!this.driver.ElementPresent(by,by.ToString()) && isPageRefreshRequired == false) //fail
                            {

                                this.TESTREPORT.LogFailure("VerifyObjectsPresence", String.Format("Object -  <mark>{0}</mark> Was Not Found  Within <mark>{1}</mark> Secs.", by.ToString(), timeOutInSecs), this.SCREENSHOTFILE);
                            }
                            else if (!this.driver.ElementPresent(by, by.ToString()) && isPageRefreshRequired == true)
                            {
                                int MAXTIMEOUT = timeOutInSecs * 1000;
                                int elapsedTimeout = 0;
                                while (!this.driver.ElementPresent(by, by.ToString()) && elapsedTimeout < MAXTIMEOUT)
                                {
                                    this.driver.RefreshPage();
                                    this.WaitTillElementExists(by, by.ToString(), timeOutInSecs);
                                    
                                    elapsedTimeout = elapsedTimeout + 1000;
                                }
                                if (!this.driver.ElementPresent(by, by.ToString()))
                                {
                                    this.TESTREPORT.LogFailure("VerifyObjectsPresence", String.Format("Object -  <mark>{0}</mark> Was Not Found  Within <mark>{1}</mark> Secs.", by.ToString(), timeOutInSecs), this.SCREENSHOTFILE);
                                }
                            }
                            else //pass
                            {

                                this.TESTREPORT.LogSuccess("VerifyObjectsPresence", String.Format("Object -  <mark>{0}</mark> Was Found  Within <mark>{1}</mark> Secs.", by.ToString(), timeOutInSecs));
                            }
                        }
                        catch (Exception ex)
                        {

                            this.TESTREPORT.LogException(ex);
                        }
                    }
                    else
                    {

                        this.TESTREPORT.LogFailure("VerifyObjectsPresence", String.Format("Object - <mark>{0}</mark> Was Not Found  Within <mark>{1}</mark> Secs.", by.ToString(), timeOutInSecs), this.SCREENSHOTFILE);
                    }
                }
                catch (Exception ex)
                {
                    this.TESTREPORT.LogException(ex);
                }

            }
            listObjects.Clear();

        }

        /// <summary>
        /// Simulates the think time in milli secs.
        /// </summary>
        /// <param name="milliSecs">The milli secs.</param>
        protected void SimulateThinkTimeInMilliSecs(int milliSecs)
        {
            Thread.Sleep(milliSecs);            
            this.TESTREPORT.LogSuccess("SimulateThinkTimeInMilliSecs", String.Format("Wait Introduced For <mark>{0}</mark> milliseconds", milliSecs));
        }

        /// <summary>
        /// Simulates the send keys.
        /// </summary>
        /// <param name="chars">The chars.</param>
        protected void SimulateSendKeys(string chars)
        {
            try
            {                
                SendKeys.SendWait(chars);
                //this.TESTREPORT.LogSuccess("SimulateSendKeys", String.Format("Invoked SendKeys.SendWait for input - <mark>{0}</mark>", chars));
            }

            catch (Exception ex)
            {
                
                //this.TESTREPORT.LogFailure("SimulateSendKeys", String.Format("Error While Invoking SendKeys.SendWait for input - <mark>{0}</mark>", chars));
                //this.TESTREPORT.LogException(ex, this.SCREENSHOTFILE);
            }
        }


        /// <summary>
        /// Verify the presence of text in a read only collection
        /// </summary>
        /// <param name="table">table</param>
        /// <param name="findtext">search text</param>
        protected bool CheckTextPresenceInTable(IReadOnlyCollection<IWebElement> table, string findtext)
        {
            try
            {
                bool flag = false;
                foreach (var txt in table)
                {
                    if (txt.Text.Contains(findtext))
                    {
                        this.TESTREPORT.LogSuccess("CheckTextPresenceInTable", String.Format("Found <mark>{0}</mark> in the table successfully", findtext));
                        flag = true;
                        return true;
                    }

                }

                if (!flag)
                {
                    this.TESTREPORT.LogFailure("CheckTextPresenceInTable", String.Format("Unable to find text in table - <mark>{0}</mark>", findtext));
                }

                return false;

            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("CheckTextPresenceInTable", String.Format("Error While finding text in table - <mark>{0}</mark>", findtext));
                this.TESTREPORT.LogException(ex, this.SCREENSHOTFILE);
                return false;
            }
           
        }


        

    }

   
}
