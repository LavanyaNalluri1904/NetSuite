﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestReporter;
using System.IO;
using Engine.Factories;
namespace Engine.Setup
{
    /// <summary>
    /// @Author - Pavan Parmar
    /// </summary>
    public class EngineSetup
    {
        private static string randomString = null;
        private const string FILETESTCONFIGURATION = "TestConfiguration.properties";        
        private static string executablePath = StandardUtilities.FileUtilities.readPropertyFile(FILETESTCONFIGURATION, "executablePath");
        private static string reportType = StandardUtilities.FileUtilities.readPropertyFile(FILETESTCONFIGURATION, "reportType"); 
        private static string testReportFile = StandardUtilities.FileUtilities.readPropertyFile(FILETESTCONFIGURATION, "testReportFile");
        private static IReporter testReportInternal = null;
        private static string screenShotFolder = new FileInfo(testReportFile).Directory.FullName + Path.DirectorySeparatorChar + "ScreenShots";
        private static int lastScreenShotCount = 1;
        private static string browser = StandardUtilities.FileUtilities.readPropertyFile(FILETESTCONFIGURATION, "browser");
        private static string platform = StandardUtilities.FileUtilities.readPropertyFile(FILETESTCONFIGURATION, "platform");
        private static int defaultTimeOutForSelenium = Int32.Parse(StandardUtilities.FileUtilities.readPropertyFile(FILETESTCONFIGURATION, "seleniumDefaultTimeOut"));
        public const int TimeOutConstant = 60;
        private static string apiKey = StandardUtilities.FileUtilities.readPropertyFile(FILETESTCONFIGURATION, "apiKey");
        private static string loginName = StandardUtilities.FileUtilities.readPropertyFile(FILETESTCONFIGURATION, "loginName");
        private static string loginPassword = StandardUtilities.FileUtilities.readPropertyFile(FILETESTCONFIGURATION, "loginPassword");
        private static string constituentId = StandardUtilities.FileUtilities.readPropertyFile(FILETESTCONFIGURATION, "constituentId");
        private static string devApiUrl = StandardUtilities.FileUtilities.readPropertyFile(FILETESTCONFIGURATION, "DevApiUrl");
        private static string prodApiUrl = StandardUtilities.FileUtilities.readPropertyFile(FILETESTCONFIGURATION, "ProdApiUrl");
        
        /// <summary>
        /// Initializes the <see cref="EngineSetup"/> class.
        /// </summary>
        static EngineSetup()
        {
            if (Directory.Exists(screenShotFolder))
            {
                Directory.Delete(screenShotFolder, true);
            }
        }
        /// <summary>
        /// Gets the random value.
        /// </summary>
        /// <returns></returns>
        public static string GetRandomValue()
        {
            if(EngineSetup.randomString == null)
            {
                EngineSetup.randomString = String.Format("{0}{1}", Environment.MachineName, DateTime.Now.ToString("ddmmssff"));
            }
            return EngineSetup.randomString;
        }

        /// <summary>
        /// Gets or sets the application path.
        /// </summary>
        /// <value>
        /// The application path.
        /// </value>
        public static string ApplicationPath
        {
            get { return new FileInfo(EngineSetup.executablePath).FullName; }
            set { EngineSetup.executablePath = value; }
           
        }
        /// <summary>
        /// Gets or sets the type of the test report.
        /// </summary>
        /// <value>
        /// The type of the test report.
        /// </value>
        public static string TestReportType
        {
            get {
                return EngineSetup.reportType; }
            set { EngineSetup.reportType = value; }
        }
        /// <summary>
        /// Gets or sets the name of the test report file.
        /// </summary>
        /// <value>
        /// The name of the test report file.
        /// </value>
        public static string TestReportFileName
        {
            get { return new FileInfo(EngineSetup.testReportFile).FullName; }
            set { EngineSetup.testReportFile = value; }
        }

        /// <summary>
        /// Gets or sets the test screen shot folder.
        /// </summary>
        /// <value>
        /// The test screen shot folder.
        /// </value>
        public static string TestScreenShotFolder
        {
            get { return EngineSetup.screenShotFolder; }
            set { EngineSetup.screenShotFolder = value; }
        }
        /// <summary>
        /// Gets the test report.
        /// </summary>
        /// <value>
        /// The test report.
        /// </value>
        public static IReporter TestReport
        {
            get
            {
                if(EngineSetup.testReportInternal == null)
                {
                    EngineSetup.testReportInternal = ReportFactory.GetReport(EngineSetup.TestReportType,true, true);
                }
                return EngineSetup.testReportInternal;
            }
        }

        public static string CaptureScreenShot(string fileName)
        {
            string screenShotFileName = "";

            return screenShotFileName;
        }

        /// <summary>
        /// Gets the screen shot path.
        /// </summary>
        /// <returns></returns>
        public static string GetScreenShotPath()
        {
            string fileName = String.Format("{0}{1}ScreenShot.jpeg", EngineSetup.TestScreenShotFolder, Path.DirectorySeparatorChar);
            try
            {
                //Verifying if the file already exists, if so append the numbers 1,2  so on to the fine name.			

                FileInfo myPngImage = new FileInfo(fileName);
                if(!myPngImage.Directory.Exists)
                {
                    myPngImage.Directory.Create();
                }
                
                while (myPngImage.Exists)
                {
                    try
                    {
                        string tempFileName = null;
                        if (fileName.Contains("_"))
                        {
                           tempFileName = fileName.Substring(0, fileName.IndexOf('_'));
                           
                        }
                        else
                        {
                            tempFileName = fileName.Substring(0, fileName.IndexOf(".jpeg"));
                        }
                        fileName = tempFileName;
                        fileName = String.Format("{0}_{1}.jpeg", fileName, EngineSetup.lastScreenShotCount++);
                        myPngImage = new FileInfo(fileName);
                    }
                    catch(Exception ex)
                    {
                        EngineSetup.TestReport.LogException(ex, EngineSetup.GetScreenShotPath());
                    }
                    
                   
                }
                return fileName;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                return fileName;
            }
        }

        /// <summary>
        /// Gets the default timeout for selenium.
        /// </summary>
        /// <value>
        /// The default timeout for selenium.
        /// </value>
        public static int DefaultTimeoutForSelenium
        {
            get
            {
                return EngineSetup.defaultTimeOutForSelenium;
            }
        }

        /// <summary>
        /// Gets or sets the browser.
        /// </summary>
        /// <value>
        /// The browser.
        /// </value>
        public static string BROWSER
        {
            get
            {
                //environment variable will be read in case of Jenkins parameterized build execution
                return Environment.GetEnvironmentVariable("browser") !=null ? Environment.GetEnvironmentVariable("browser") : EngineSetup.browser;
            }
            set { EngineSetup.browser = value; }
        }

        /// <summary>
        /// Gets or sets the platform.
        /// </summary>
        /// <value>
        /// The browser.
        /// </value>
        public static string PLATFORM
        {
            get
            {
                return  EngineSetup.platform;
            }
            set { EngineSetup.platform = value; }
        }

        /// <summary>
        /// Gets or sets the apikey.
        /// </summary>
        /// <value>
        /// The apikey.
        /// </value>
        public static string APIKEY
        {
            get { return EngineSetup.apiKey; }
            set { EngineSetup.apiKey = value;}
        }


        /// <summary>
        /// Gets or sets the loginName.
        /// </summary>
        /// <value>
        /// The loginName.
        /// </value>
        public static string LOGINNAME
        {
            get { return EngineSetup.loginName; }
            set { EngineSetup.loginName = value; }
        }

        /// <summary>
        /// Gets or sets the loginPassword.
        /// </summary>
        /// <value>
        /// The loginPassword.
        /// </value>
        public static string LOGINPASSWORD
        {
            get { return EngineSetup.loginPassword; }
            set { EngineSetup.loginPassword = value; }
        }

        /// <summary>
        /// Gets or sets the constituentId.
        /// </summary>
        /// <value>
        /// The constituentId.
        /// </value>
        public static string CONSTITUENTID
        {
            get { return EngineSetup.constituentId; }
            set { EngineSetup.constituentId = value; }
        }

        /// <summary>
        /// Gets or sets the devApiUrl.
        /// </summary>
        /// <value>
        /// The devApiUrl.
        /// </value>
        public static string DEVAPIURL
        {
            get {
                if (devApiUrl.EndsWith("/"))
                    devApiUrl = devApiUrl.Substring(0, devApiUrl.Length - 1);
                return EngineSetup.devApiUrl;
            }
            set { EngineSetup.devApiUrl = value; }
        }

        /// <summary>
        /// Gets or sets the prodApiUrl.
        /// </summary>
        /// <value>
        /// The prodApiUrl.
        /// </value>
        public static string PRODAPIURL
        {
            get {
                if (prodApiUrl.EndsWith("/"))
                    prodApiUrl = prodApiUrl.Substring(0, prodApiUrl.Length - 1);
                return EngineSetup.prodApiUrl;
            }
            set { EngineSetup.prodApiUrl = value; }
        }
    }
}
