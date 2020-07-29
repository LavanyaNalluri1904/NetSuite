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
    public class NetSuiteRolePage : AbstractTemplatePage
    {
        #region Constructions
        public NetSuiteRolePage()
        {
        }

        public NetSuiteRolePage(IWebDriver driver)
        {
            this.driver = driver;
        }
        #endregion

        #region UI Object Repository
        private By headerMenuUserRole = By.XPath("//body[contains(@class,'ext-webkit ext-chrome')]/div[@id='pageContainer']/div[@id='div__header']/div[@id='ns_header']/div[@id='ns-header-menu-userrole']/div[@id='spn_cRR_d1']/a[1]");
        private By admin = By.XPath("//tr[@id='row0']//a[contains(@class,'dottedlink')][contains(text(),'Administrator')]");
        private By mawController = By.XPath("//a[contains(text(),'MAW Controller')]");
        public By homePageRoleSelected = By.XPath("//span[@class='ns-role-name']");
        //private By home = By.XPath("//h1[contains(text(),'Home')]");
        private By logout = By.XPath("//span[contains(text(),'Log Out')]");
        
        #endregion

        #region Constants
        private string administrator = "Administrator";
        private string cfo = "MAW CFO";
        private string apClerk = "MAW AP Clerk";
        private string arClerk = "MAW AR Clerk";
        private string ceo = "MAW CEO";
        private string controller = "MAW Controller";
        private string grantAdministrator = "MAW Grant Administrator";
        private string programManager = "MAW Program Manager";
        private string sfsAPClerk = "MAW SFS AP Clerk";
        private string sfsARClerk = "MAW SFS AR Clerk";
        private string sfsAccountant = "MAW SFS Accountant";
        private string sfsSrAccountant = "MAW SFS Sr. Accountant";
        private string sfsManager = "MAW SFS Manager";
        private string handsOffAllChaptersViewOnly = "MAW Hands Off - All Chapter View Only";
        private string procurementAdmin = "MAW Procurement Admin";
        private string handsOffChapterViewOnly = "MAW Hands Off - Chapter View Only";
        private string fixedAssetManagement = "MAW Fixed Asset Management";
        #endregion


        #region Public Methods


        #region Corresponding Role Selection
        public void RoleSelection(string roleName)
        {
            try
            {
                //this.TESTREPORT.LogInfo("Selecting the Corresponding ROLE :  ", roleName.ToString());
                driver.FindElement(headerMenuUserRole).Click();
                driver.WaitElementExistsAndVisible(mawController);
                driver.FindElement(mawController).Click();
                driver.WaitForPageLoad(TimeSpan.FromSeconds(20));             
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("Role Selection Failed : ", "Failed to navigate to Role :" , EngineSetup.GetScreenShotPath());
            }
           
        }
        #endregion


        #region Click Logout
        public void Logout()
        {
            
            try
            {
                Actions action = new Actions(driver);
                action.MoveToElement(driver.FindElement(headerMenuUserRole)).Perform();
                driver.WaitForPageLoad(TimeSpan.FromSeconds(5));
                driver.ClickElementUsingWebElement(logout, "Logout");
                this.TESTREPORT.LogInfo("Logout is Success");                
                
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("Logout is not success", EngineSetup.GetScreenShotPath());
            }
            
        }
        #endregion


        #endregion

    }
}
