using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestReporter;
using Engine.Setup;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using StandardUtilities;
using Engine.Factories;

namespace AutomatedTest.FunctionalTests.MAW
{
    [TestClass]
    public class TestBaseTemplate
    {
        public string dataFileName = null;
        public int currentFileRowPointer = 1;
        public TestContext testContextInstance;
        public static IWebDriver driver = null;
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [AssemblyInitialize]
        public static void BeforeAllTestsExecution(TestContext testContext)
        {
            #region Get webdriver instance - MAW Donation
            Console.WriteLine(String.Format("Current Directory - {0}", System.IO.Directory.GetCurrentDirectory()));
            driver = WebDriverFactory.getWebDriver(EngineSetup.BROWSER);
            Console.WriteLine("title " + driver.Title);
            #endregion
        }
        [AssemblyCleanup]
        public static void AfterAllTestsExecution()
        {

            //after execution, update extent report with gallop logo 
            /*driver can not be initialized in static method as driver is instance variable*/
            if(driver is InternetExplorerDriver)
            {
                driver.Quit();
            }   
            else
            {
                driver.Close();
                driver.Quit();

            }

            WebDriverFactory.Free();   
            EngineSetup.TestReport.Close();
            TestBaseTemplate.UpdateTestReport();
        }
        [ClassInitialize]
        public static void BeforeAllTestsInATestClassExecution(TestContext testContext)
        {

        }
        [ClassCleanup]
        public static void AfterAllTestsInATestClassExecution()
        {

        }

        ////Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void BeforeEachTestCaseExecution()
        {


        }

        ////Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void AfterEachTestCaseExecution()
        {

        }


        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>




        protected string RANDVALUE
        {
            get { return EngineSetup.GetRandomValue(); }

        }

        protected string DATAFILENAME
        {
            get {
                //this.dataFileName = String.Format("{0}{1}", this.GetType().Name, ".csv");
                this.dataFileName = System.IO.Directory.GetCurrentDirectory();
                //this.dataFileName = System.IO.Directory.GetCurrentDirectory()+"\\TestData\\BKFS";   //uncomment for debugging             
                return this.dataFileName; }

        }

        protected int DATAFILEROWPOINTER
        {
            get { return this.currentFileRowPointer; }
            set { this.currentFileRowPointer = value; }
        }

        public IReporter TESTREPORT
        {
            get { return EngineSetup.TestReport; }
        }

        protected string SCREENSHOTFILE
        {
            get { return EngineSetup.GetScreenShotPath(); }
        }
        /*To update extentReport to have Gallop Logo*/
        protected static void UpdateTestReport()
        {
            /*Dictionary should contain
            * SourceFile
            * Text1ToBeReplaced
            * Text1ToReplace
            * Text2ToBeReplaced
            * Text2ToReplace 
            * Text3ToBeReplaced
            * Text3ToReplace 
         
            */
            Dictionary<string, string> keyValuePair = new Dictionary<string, string>();
            if (EngineSetup.TestReport is ExtentReporter)
            {

                keyValuePair["SourceFile"] = EngineSetup.TestReportFileName;

                //Text1ToBeReplaced
                string str = "<div class='logo-container'>\r\n                                    <a class='logo-content' href='http://extentreports.relevantcodes.com'>\r\n                                        <span>ExtentReports</span>\r\n                                    </a>\r\n                                    <a href='#' data-activates='slide-out' class='button-collapse hide-on-large-only'><i class='mdi-navigation-apps'></i></a>\r\n                                </div>";


                keyValuePair["Text1ToBeReplaced"] = str;


                //Text1ToReplace                
                str = "<div class='logo-container' style='height:50px;width:200px;'>\r\n                                    <a href='http://www.gallop.net/'>\r\n                                        <img border='0' alt='Gallop' src='gallop_logo.png' width='200' height='35'>\r\n                                    </a>\r\n                                     <a href='#' data-activates='slide-out' class='button-collapse hide-on-large-only'><i class='mdi-navigation-apps'></i></a>\r\n                                </div>";

                keyValuePair["Text1ToReplace"] = str;


                //Text2ToBeReplaced
                str = "<span class='report-name'>";
                keyValuePair["Text2ToBeReplaced"] = str;

                //Text2ToReplace
                str = "<span class='report-name'><div style='width:220px;' align='right'>";
                keyValuePair["Text2ToReplace"] = str;

                //Text3ToBeReplaced
               str = "</span> <span class='report-headline'>";                
                keyValuePair["Text3ToBeReplaced"] = str;

                //Text3ToReplace
                str = "</div></span> <span class='report-headline'>";
                keyValuePair["Text3ToReplace"] = str;
            }
            ITestReportManipulator updateExtentReport = new ExtentReportManipulator(keyValuePair);
            EngineSetup.TestReport.ManipulateTestReport(updateExtentReport);

        }

        public string readCSV(string fileName, string columnName)
        {
            string data = "";
            try
            {
                data = FileUtilities.GetCSVData(this.DATAFILENAME+"\\"+ fileName+".csv",
                                         columnName.Trim(), this.DATAFILEROWPOINTER);
                data = data.Replace("{random}", this.RANDVALUE);
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("readCSV", ex.Message);
            }
            return data;
        }


    }
}
