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
   public  class NetSuiteSmokeTest : TestBaseTemplate
    {
        #region PageObject
        LoginPage login = new LoginPage();
        #endregion

        [TestMethod]
        [TestCategory("Login Success Scenario")]
        public void TC_Success_Login() 
        {
            #region TC_Success
            this.TESTREPORT.InitTestCase("TC_Login - Navigate to URL ", "Verify the login page and enter the corresponding credentials");
            #region Test Data
            string url = "https://system.na0.netsuite.com/pages/customerlogin.jsp";
            string username = "lavanya.nalluri@cigniti.com";
            string password = "Dhana!12Lakshmi";
            #endregion

            login.NavigateToLoginPage(url);
            #endregion
        }

    }
}
