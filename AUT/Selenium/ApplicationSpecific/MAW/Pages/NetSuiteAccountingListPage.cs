using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AUT.Selenium.CommonReUsablePages;
using Engine.UIHandlers.Selenium;
using OpenQA.Selenium;
using Engine.Setup;
using Engine.UIHandlers.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace AUT.Selenium.ApplicationSpecific.MAW.Pages
{
    public class NetSuiteAccountingListPage : AbstractTemplatePage
    {
        #region Constructors
        public NetSuiteAccountingListPage()
        {

        }

        public NetSuiteAccountingListPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        #endregion

        #region UI Object Repository
        private By setUpObject = By.XPath("//body[contains(@class,'ext-webkit ext-chrome')]/div[@id='pageContainer']/div[@id='div__header']/div[@id='ns_navigation']/ul[@id='ns-header-menu-main']/li[@id='ns-header-menu-main-item7']/a/span[1]");
        private By accountingObject = By.XPath("//td[contains(text(),'Accounting')]");
        private By accountingListObject = By.XPath("//a[contains(text(),'Accounting Lists')]");
        private By setupManager = By.XPath("//h1[@class='uir-record-type']");
        private By exportCSVButton = By.XPath("//div[contains(@class,'uir-list-icon-button uir-list-export-csv')]");
        private By budgetCategoryElement = By.XPath("//td[contains(text(),'Budget Category')]");
        private By miscCategoryElement = By.XPath("//td[contains(text(),'1099-MISC Category')]");
        #endregion

        #region Constants
        string accountingConst = "Accounting";
        string accountingListTitle = "Accounting Lists";
        string exportCSVConst = "Export - CSV";
        string budgetCategoryConst = "Budget Category";
        string miscCategoryConst = "1099-MISC Category";
        #endregion

        #region Public Methods

        #region Navigate to Setup > Accounting > Accounting List Page
        public bool AccountingListPage() 
        {
            bool isValid = false;

            try
            {
                driver.FindElement(setUpObject).Click();
                driver.WaitElementPresent(setupManager);
                driver.ClickElementUsingWebElement(accountingObject, accountingConst);
                driver.ClickElementUsingWebElement(accountingListObject, accountingListTitle);
                driver.WaitForPageLoad(TimeSpan.FromSeconds(50));
                driver.SwitchTo().Frame(1);
                isValid = driver.CheckElementExists(exportCSVButton, exportCSVConst);
                

            }
            catch(Exception ex)
            {
                this.TESTREPORT.LogFailure("Error in Accounting List Page: ", EngineSetup.GetScreenShotPath());
            }

            return isValid;

        }

        public bool AccountingListElements()
        {
            bool isValid = false;
            try
            {
                isValid = driver.CheckElementExists(budgetCategoryElement, budgetCategoryConst);
                isValid = driver.CheckElementExists(miscCategoryElement, miscCategoryConst);
                driver.SwitchTo().DefaultContent();
            }
            catch(Exception ex)
            {

            }

            return isValid;

        }

        #endregion


        #endregion
    }
}
