using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Setup;
using Engine.UIHandlers.Selenium;
using AUT.Selenium.CommonReUsablePages;

namespace AUT.Selenium.ApplicationSpecific.MAW.Pages
{
    public class NetSuiteAuthenticationPage : AbstractTemplatePage
    {

        #region UI Object Repository
        private By additionalAuthenticationQuestion = By.CssSelector("body > form > table > tbody > tr:nth-child(3) > td > table > tbody > tr:nth-child(2) > td.smalltextnolink.text-opensans");
        private By additionalAuthenticationAnswer = By.Name("answer");
        private By additionalAuthenticationSubmit = By.CssSelector("body > form > table > tbody > tr:nth-child(4) > td > input");
        private By homepageverification = By.XPath("//div[contains(text(),'Feedback')]");
        #endregion

        #region Constants
        string additionalAuthenticationFirstQuestion = "meet your spouse";
        string additionalAuthenticationFirstQuestionAnswerValue = "hyderabad";
        string additionalAuthenticationSecondQuestion = "What city would you choose to never visit?";
        string additionalAuthenticationSecondQuestionAnswerValue = "pakistan";
        string additionalAuthenticationThirdQuestion = "What was your childhood nickname?";
        string additionalAuthenticationThirdQuestionAnswerValue = "chinni";
        string feedbackElementInHomePage = "Feedback";
        #endregion

        #region Public Methods
        public void AdditionalAuthentication() 
        {
            try
            {
                string question = driver.FindElement(additionalAuthenticationQuestion).Text;
                if (question.Contains(additionalAuthenticationFirstQuestion))
                {
                    driver.FindElement(additionalAuthenticationAnswer).SendKeys(additionalAuthenticationFirstQuestionAnswerValue);
                    driver.FindElement(additionalAuthenticationSubmit).Click();
                    driver.WaitForPageLoad(TimeSpan.FromSeconds(60));
                    driver.CheckElementExists(homepageverification, feedbackElementInHomePage);
                    this.TESTREPORT.LogSuccess("Additional Authentication ","Navigation of Additional Authentication is success");

                }
                else
                if (question.Contains(additionalAuthenticationSecondQuestion))
                {
                    driver.FindElement(additionalAuthenticationAnswer).SendKeys(additionalAuthenticationSecondQuestionAnswerValue);
                    driver.FindElement(additionalAuthenticationSubmit).Click();
                    driver.WaitForPageLoad(TimeSpan.FromSeconds(60));
                    driver.CheckElementExists(homepageverification, feedbackElementInHomePage);
                    this.TESTREPORT.LogSuccess("Additional Authentication ", "Navigation of Additional Authentication is success");
                }
                else
                if (question.Contains(additionalAuthenticationThirdQuestion))
                {
                    driver.FindElement(additionalAuthenticationAnswer).SendKeys(additionalAuthenticationThirdQuestionAnswerValue);
                    driver.FindElement(additionalAuthenticationSubmit).Click();
                    driver.WaitForPageLoad(TimeSpan.FromSeconds(60));
                    driver.CheckElementExists(homepageverification, feedbackElementInHomePage);
                    this.TESTREPORT.LogSuccess("Additional Authentication ", "Navigation of Additional Authentication is success");
                }
            
            }catch(Exception ex)
            {
                this.TESTREPORT.LogFailure("Additional Authentication Page", "You have entered an invalid security answer. Please try again." +  EngineSetup.GetScreenShotPath());

            }
        }
        #endregion
    }
}
