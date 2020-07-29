using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AUT.Selenium.ApplicationSpecific.MAW.Pages;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using static AUT.Selenium.ApplicationSpecific.MAW.Pages.UserTransaction;

using Microsoft.WindowsAzure.MobileServices;

namespace AutomatedTest.FunctionalTests.MAW
{
    [TestClass]
    public class NetSuiteTest : TestBaseTemplate
    {
        #region PageObject
       NetSuiteLoginPage login = new NetSuiteLoginPage();
        NetSuiteAuthenticationPage authPage = new NetSuiteAuthenticationPage();
        NetSuiteRolePage rolePage = new NetSuiteRolePage();
        NetSuiteCustomSegmentsPage customSegmentPage = new NetSuiteCustomSegmentsPage();
        NetSuiteAccountingListPage accountingListPage = new NetSuiteAccountingListPage();
        #endregion

        /*[TestMethod]
        [TestCategory("Verifying Segmentation REGION Values")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_Segmentation_RegionValues()
        {
            #region TC Segmentation Region Values
            this.TESTREPORT.InitTestCase("TC_Login - Navigate to URL ", "Verify the login page and enter the corresponding credentials");
            #region Test Data
            string url = TestContext.DataRow["Url"].ToString();
            string emailAddress = TestContext.DataRow["Username"].ToString();
            string password = TestContext.DataRow["Password"].ToString();
            #endregion

            login.NavigateToLoginPage(url);
            login.EnterLoginCredentialsNavigateToHomePage(emailAddress, password);
            authPage.AdditionalAuthentication();
            rolePage.RoleSelection("MAW Controller");
            customSegmentPage.CustomSegments();
            customSegmentPage.CustomSegmentsRegionValue();
            rolePage.Logout();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion
        }

        [TestMethod]
        [TestCategory("Verifying Segmentation GRANT Values")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_Segmentation_GrantValues()
        {
            #region Test Data
            string url = TestContext.DataRow["Url"].ToString();
            string emailAddress = TestContext.DataRow["Username"].ToString();
            string password = TestContext.DataRow["Password"].ToString();
            #endregion

            #region TC Segmentation GRANT Values
            this.TESTREPORT.InitTestCase("TC_Login - Navigate to URL ", "Verify the login page and enter the corresponding credentials");           

            login.NavigateToLoginPage(url);
            login.EnterLoginCredentialsNavigateToHomePage(emailAddress, password);
            authPage.AdditionalAuthentication();
            rolePage.RoleSelection("MAW Controller");
            customSegmentPage.CustomSegments();
            customSegmentPage.CustomSegmentsGrantValue();
            rolePage.Logout();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion
        }
*/

        /**
         * Test Owner : Lavanya Nalluri
         * Test Case Name : ProcureToPay - NSP_r3.1
         * Description : Login as 'MAW Controller', 
         *               Navigate to Setup > Accounting > Accounting Lists 
         *               The test is verifying WebElement after succesful navigation at each page.
         *               Verifying the Accounting List from the application.
         * **/
        [TestMethod]
        [TestCategory("ProcureToPay Accounting List - NSP_r3.1")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_ProcureToPay_AccountingList_NSPr3_1() 
        {
            bool isValid = false;
            #region Test Data
            string url = TestContext.DataRow["Url"].ToString();
            string emailAddress = TestContext.DataRow["Username"].ToString();
            string password = TestContext.DataRow["Password"].ToString();
            #endregion

            #region ProcureToPay Accounting List - NSP_r.3.1
            this.TESTREPORT.InitTestCase("ProcureToPay Accounting List - NSP_r3.1", "Verify ACCOUNTING LIST Values exists");
            login.NavigateToLoginPage(url);
            login.EnterLoginCredentialsNavigateToHomePage(emailAddress, password);
            authPage.AdditionalAuthentication();
            rolePage.RoleSelection("MAW Controller");
            isValid = accountingListPage.AccountingListPage();
            isValid = accountingListPage.AccountingListElements();
            if (isValid)
            {
                this.TESTREPORT.LogSuccess("Test successfully navigated to Accounting Lists Page", "Test Navigated to Setup > Acccounting > Accounting Lists Page");
            }
            else
            {
                this.TESTREPORT.LogFailure("Test Failed to navigate Accounting Lists Page", "Please Check the navigation!!");
            }
            //Assert.IsTrue(isValid, "ProcureToPay Accounting Lists Page Navigation is success.");
            
            rolePage.Logout();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

    }
}
