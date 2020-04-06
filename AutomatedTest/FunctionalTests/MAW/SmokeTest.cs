using System;
using RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AUT.Selenium.ApplicationSpecific.MAW.Pages;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using static AUT.Selenium.ApplicationSpecific.MAW.Pages.UserTransaction;

namespace AutomatedTest.FunctionalTests.MAW
{
    [TestClass]
    public class SmokeTest : TestBaseTemplate
    {
        #region PageObject
        HomePage home = new HomePage();
        DonatePage donate = new DonatePage();
        DonationReviewPage donationReview = new DonationReviewPage();
        ThankYouPage thankyou = new ThankYouPage();
        #endregion

        [TestMethod]
        [TestCategory("Failure Scenario")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_Verify_Donation_With_Invalid_Credit_Details()
        {

            #region TC_WEB_030 - Donation with valid Credit Card information
            this.TESTREPORT.InitTestCase("TC_WEB - Donation with invalid Credit Card information", "Verify a donation with invalid credit card details");
            #region Test Data
            string url = TestContext.DataRow["Url"].ToString();
            string yourFirstName = TestContext.DataRow["YourFirstName"].ToString();
            string yourLastName = TestContext.DataRow["YourLastName"].ToString();
            string emailAddress = TestContext.DataRow["YourEmailAddress"].ToString();
            string yourStreetAddress1 = TestContext.DataRow["YourStreetAddress1"].ToString();
            string yourStreetAddress2 = TestContext.DataRow["YourStreetAddress2"].ToString();
            string yourPostalCode = TestContext.DataRow["YourPostalCode"].ToString();
            string creditCardNumber = TestContext.DataRow["InvalidCreditCardNumber"].ToString();
            string expirationMonth = TestContext.DataRow["ExpirationMonth"].ToString();
            string expirationYear = TestContext.DataRow["ExpirationYear"].ToString();
            string cvv = TestContext.DataRow["CVV"].ToString();

            #endregion
            home.navigateToDonationPage(url);
            donate.selectDonationAmount("$5");
            donate.navigateToDonationReviewPage();
            donationReview.updateYourInformation(yourFirstName, yourLastName, emailAddress, yourStreetAddress1, yourPostalCode, yourStreetAddress2);
            donationReview.updateBillingInformation("CREDIT CARD", creditCardNumber, expirationMonth, expirationYear, cvv);
            donationReview.submitDonation(); //uncomment for actual donation
            thankyou.validateThankYouPage();
            thankyou.validateUserTransactionAndRefund();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }



        //[TestMethod, Description("Verify Home page navigation"), TestCategory("Smoke")]
        //[DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        //public void TC001_Verify_Navigation_To_HomePage()
        //{

        //    #region Navigate to home
        //    this.TESTREPORT.InitTestCase("Home Page Navigation", "Verify navigation to home page");
        //    string url = TestContext.DataRow["Url"].ToString(); //readCSV("MAWTestData", "Url");
        //    home.NavigateToHome(url);            
        //    this.TESTREPORT.UpdateTestCaseStatus();
        //    #endregion  

        //}

        //[TestMethod, Description("Verify Home page navigation"), TestCategory("Smoke")]
        //[DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        //public void TC002_Verify_Home_Page_Controls()
        //{

        //    #region Navigate to home
        //    this.TESTREPORT.InitTestCase("Home Page Controls", "Verify controls on home page");
        //    string url = TestContext.DataRow["Url"].ToString(); //readCSV("MAWTestData", "Url");
        //    home.NavigateToHome(url);
        //    home.ValidateHomePageControls();
        //    this.TESTREPORT.UpdateTestCaseStatus();
        //    #endregion

        //}

        //[TestMethod, Description("Verify elements on donation page"), TestCategory("Smoke")]
        //[DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        //public void TC003_Verify_Donation_Page_Controls()
        //{

        //    #region Navigate to Donation Page and validate elements
        //    this.TESTREPORT.InitTestCase("Donation Page Controls", "Verify elements on donation page");
        //    string url = TestContext.DataRow["Url"].ToString(); //readCSV("MAWTestData", "Url");
        //    home.NavigateToDonationPage(url);
        //    donate.ValidateDonatePageElements();
        //    this.TESTREPORT.UpdateTestCaseStatus();
        //    #endregion

        //}

        //[TestMethod, Description("Verify Satellite Data Collector value"), TestCategory("Smoke")]
        //[DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        //public void TC004_Verify_Satellite_Data_Collector_Values()
        //{

        //    #region Validate Satellite Data Collector value
        //    this.TESTREPORT.InitTestCase("Satellite Data Collector", "Verify Satellite Data Collector values");
        //    string url = TestContext.DataRow["Url"].ToString(); //readCSV("MAWTestData", "Url");
        //    home.NavigateToDonationPage(url);
        //    donate.validateSatelliteDataCollectorValue();
        //    this.TESTREPORT.UpdateTestCaseStatus();
        //    #endregion

        //}


        //[TestMethod, Description("Verify Satellite Data Collector value"), TestCategory("Smoke")]
        //[DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        //public void TC005_Verify_Satellite_Footer_Script_Container_Values()
        //{

        //    #region Validate Satellite Data Collector value
        //    this.TESTREPORT.InitTestCase("Satellite Footer Script Container", "Verify Satellite Footer Script Container values");
        //    string url = TestContext.DataRow["Url"].ToString(); //readCSV("MAWTestData", "Url");
        //    home.NavigateToDonationPage(url);
        //    donate.validateSatelliteFooterScriptContainerValues();
        //    this.TESTREPORT.UpdateTestCaseStatus();
        //    #endregion

        //}


        //[TestMethod, Description("Verify Local donation clear form and start over scenario"), TestCategory("Smoke")]
        //[DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        //public void TC006_Verify_Local_Donation_Clear_Form_And_Start_Over()
        //{

        //    #region Validate Local Chapter donation by Clear form and start over 
        //    this.TESTREPORT.InitTestCase("Local Donation Clear form and Start Over", "Verify Local Chapter donation by Clear form and start over ");
        //    string url = TestContext.DataRow["Url"].ToString(); //readCSV("MAWTestData", "Url");
        //    home.NavigateToDonationPage(url);
        //    donate.ValidateDonatePageElements();
        //    donate.validateLocalDonationAfterClearForm();
        //    this.TESTREPORT.UpdateTestCaseStatus();
        //    #endregion

        //}

        //[TestMethod, Description("Verify JSON data in FTP"), TestCategory("Smoke")]
        //[DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        //public void TC007_Verify_JSONData()
        //{

        //    #region Validate Local Chapter donation by Clear form and start over 
        //    this.TESTREPORT.InitTestCase("JSON Parser", "Verify JSON data in FTP ");
        //    string url = TestContext.DataRow["Url"].ToString(); //readCSV("MAWTestData", "Url");
        //    home.NavigateToDonationPage(url);
        //    donate.verifyJsonDataFTP();
        //    this.TESTREPORT.UpdateTestCaseStatus();
        //    #endregion

        //}



        //[TestMethod, Description("Verify API"), TestCategory("Smoke")]
        //[DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        //public void TC_Test_User_Transaction_LO_API()
        //{

        //    this.TESTREPORT.InitTestCase("LO API", "Verify API");
        //    string url = TestContext.DataRow["Url"].ToString();

        //    var client = new RestClient("https://secure2.convio.net/wishdev/site/SRConsAPI?method=getUserTransactions&api_key=mu3fefod&v=1.0&login_name=cignity_qa&login_password=VvT4dPaX3HHr&cons_id=2951007&begin_date=2017-05-02&end_date=2017-05-03&response_format=json");
        //    var request = new RestRequest(Method.POST);
        //    IRestResponse response = client.Execute(request);
        //    Console.WriteLine(response.Content);
        //    string jsonString = response.Content;

        //    JObject consResponse = JObject.Parse(jsonString);
        //    if (consResponse.ToString().Contains("getConsTransactionsResponse"))
        //    {
        //        string transaction = consResponse["getConsTransactionsResponse"].ToString();
        //        JObject objTransaction = JObject.Parse(transaction);

        //        if (!objTransaction.ToString().Equals("{}"))
        //        {
        //            UserTransaction.RootObject[] result;
        //            UserTransaction.RootObject result1;

        //            transaction = objTransaction["transaction"].ToString();
        //            if (transaction.StartsWith("["))
        //                result = JsonConvert.DeserializeObject<UserTransaction.RootObject[]>(transaction);
        //            else
        //                result1 = JsonConvert.DeserializeObject<UserTransaction.RootObject>(transaction);

        //        }
        //    }
        //    this.TESTREPORT.UpdateTestCaseStatus();

        //}

        //

        //[TestMethod, TestCategory("Smoke")]
        //[DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        //public void TC_Test_Refund_Transaction_LO_API()
        //{

        //    this.TESTREPORT.InitTestCase("LO API", "Verify API");
        //    string url = TestContext.DataRow["Url"].ToString();
        //    //11083
        //    var client = new RestClient("https://secure2.convio.net/wishdev/site/SRDonationAPI?method=refundTransaction&api_key=mu3fefod&v=1.0&login_name=cignity_qa&login_password=VvT4dPaX3HHr&refund_transaction_request=<refundRequest xmlns=\"http://convio.com/crm/v1.0\"><donRefundInfoList><donRefundInfo><refund_type>full</refund_type><user_confirmation_code>938-1303-1-5055-11083</user_confirmation_code></donRefundInfo></donRefundInfoList></refundRequest>");
        //    var request = new RestRequest(Method.POST);
        //    IRestResponse response = client.Execute(request);
        //    Console.WriteLine(response.Content);

        //    this.TESTREPORT.UpdateTestCaseStatus();

        //}

        //[TestMethod, TestCategory("Smoke")]
        //[DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        //public void TC_VerifyUserTransaction()
        //{

        //    this.TESTREPORT.InitTestCase("LO API", "Verify API");
        //    ThankYouPage thank = new ThankYouPage();


        //    //thank.validateUserTransaction("2017-04-26", "", "10", "9946", "************9850", "VISA", "Solomon Huynh", "Huynh", "4742 N 24th Street, Suite 400", "Phoenix", "AZ", "United States", "85016");

        //    thank.validateUserTransaction("2017-05-11", "", "", "11083");
        //    thank.validateRefundTransaction("", "11083", "");


        //    this.TESTREPORT.UpdateTestCaseStatus();

        //}

    }
}