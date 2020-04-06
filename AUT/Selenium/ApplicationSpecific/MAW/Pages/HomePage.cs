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
using GallopReporter;
using System.Windows.Forms;

namespace AUT.Selenium.ApplicationSpecific.MAW.Pages
{
    public class HomePage : AbstractTemplatePage
    {
        #region Constructors
        public HomePage()
        {
        }

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }
        #endregion

        #region UI Object Repository
        private By btnDonate = By.CssSelector(".donate");
        private By lnkWishes = By.XPath("//a[@title='Wishes']");
        private By lnkAboutUs = By.XPath("//a[@title='About Us']");
        private By lnkLocalChapters = By.XPath("//a[@title='Local Chapters']");
        private By lnkReferAChild = By.XPath("//a[@title='Refer a Child']");
        private By lnkWaysToHelp = By.XPath("//a[@title='Ways To Help']");
        private By imgMakeAWish = By.XPath("//img[@alt='Make a Wish']");
        private By txtSearch = By.Id("tSearch");

        private By btnDonateOnCarousel =  By.XPath("//a[@id='body_1_rptSliderItems_hSliderLink_0']//img[@alt='Donate']");

        #endregion

        #region Public Methods

        #region Navigate to Make-A-Wish Home page
        /// <summary>
        /// Method to navigate to home page
        /// </summary>
        /// <param name="url"></param>
        public void NavigateToHome(string url)
        {
            try
            {
                driver.Navigate().GoToUrl(url);
                if (driver.isAlertPresent())
                {
                    IAlert devAlert = driver.SwitchTo().Alert();
                    devAlert.SendKeys("testdev");
                    devAlert.Accept();
                }
                driver.sleepTime(2000);
                this.TESTREPORT.LogSuccess("NavigateToHome", String.Format("Navigated to url: <mark>{0}</mark> successfully",url));
                driver.CheckElementExists(btnDonate, "DONATE");
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("NavigateToHome", "Failed to navigate to url:" + url, EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Navigate to donation page
        /// <summary>
        /// Method to navigate to Donation page
        /// </summary>
        /// <param name="url"></param>
        public void navigateToDonationPage(string url)
        {
            try
            {
                NavigateToHome(url);
                if(!driver.Url.Contains("wishdev"))
                    driver.ClickElement(btnDonate, "Donate");
                driver.WaitForPageLoad(TimeSpan.FromSeconds(60));
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("NavigateToDonationPage", "Error occurred while navigating to donation page", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion
        public void LunchPage(string url)
        {
            driver.Navigate().GoToUrl(url);
            driver.sleepTime(2000);
            driver.sleepTime(2000);
          //this.TESTREPORT.LogSuccess("NavigateToHome", String.Format("Navigated to url: <mark>{0}</mark> successfully", url));
            
           

        }




        #region Validate controls on Home page
        /// <summary>
        /// Method to validate home page controls
        /// </summary>
        /// <param name="chapterName"></param>
        public bool validateHomePageControls(string chapterName="")
        {
            bool isValid=false;
            try
            {
                isValid= driver.CheckElementNotDisplayed(lnkWishes,"WISHES");
                isValid&= driver.CheckElementDoesnotExists(lnkAboutUs,"ABOUT US");
                isValid&= driver.CheckElementDoesnotExists(lnkReferAChild,"REFER A CHILD");
                isValid&= driver.CheckElementDoesnotExists(lnkWaysToHelp, "WAYS TO HELP");
                isValid&= driver.CheckElementDoesnotExists(imgMakeAWish, "MAKE A WISH LOGO");
                isValid&= driver.CheckElementDoesnotExists(txtSearch, "Search");
                isValid&= driver.CheckElementDoesnotExists(btnDonate, "DONATE");

                if (chapterName != "")
                {
                    driver.CheckElementExists(By.XPath("//img[@alt='" + chapterName + "']"), "Chapter:" + chapterName);
                }
                else
                {
                    driver.CheckElementExists(lnkLocalChapters, "LOCAL CHAPTERS"); //Only for America
                }
                
            }
            
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("ValidateHomePageControls", "Error occurred while validating elements on home page", EngineSetup.GetScreenShotPath());
            }
            return isValid;

        }

        public void navigateToDonationPageUsingCarousel(string url)
        {
            try
            {
                NavigateToHome(url);
                if (!driver.Url.Contains("wishdev"))
                    driver.ClickElement(btnDonateOnCarousel, "Donate");
                driver.WaitForPageLoad(TimeSpan.FromSeconds(60));
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("navigateToDonationPageUsingCarousel", "Error occurred while navigating to donation page using Donate button on carousel", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #endregion
    }
}
