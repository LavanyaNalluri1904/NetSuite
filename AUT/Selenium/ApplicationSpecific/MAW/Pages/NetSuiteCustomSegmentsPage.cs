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
    public class NetSuiteCustomSegmentsPage : AbstractTemplatePage
    {
        #region Constructors
        public NetSuiteCustomSegmentsPage()
        { 
        }

        public NetSuiteCustomSegmentsPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        #endregion

        #region UI Object Repository
        private By customization = By.XPath("//body[contains(@class,'ext-webkit ext-chrome')]/div[@id='pageContainer']/div[@id='div__header']/div[@id='ns_navigation']/ul[@id='ns-header-menu-main']/li[@id='ns-header-menu-main-item7']/a/span[1]");
        private By listRecordsFields = By.XPath("//td[contains(text(),'Lists, Records, & Fields')]");
        private By customSegments = By.XPath("//a[contains(text(),'Custom Segments')]");
        private By regionLabel = By.XPath("//a[contains(text(),'Region')]");
        private By regionLabelInRegionPage = By.XPath("//span[@class='uir-field inputreadonly'][contains(text(),'Region')]");
        private By customRecordTypeInRegionPage = By.XPath("//a[@class='dottedlink']");
        private By southWestElement = By.XPath("//div[@id='valuestab_wrapper']//tr//tr[2]//td[1]");
        private By northernNevadaChapterElement = By.XPath("//td[contains(text(),'Northern Nevada Chapter')]");
        private By grantCustomSegment = By.XPath("//tr[@id='row1']//td[3]");
        private By grantLableInGrantPage = By.XPath("//tr[@id='tr_fg_primaryfieldgroup']//table[contains(@class,'table_fields')]//tbody//tr//td//span[contains(@class,'uir-field inputreadonly')][contains(text(),'Grant')]");
        private By customRecordTypeInGrantPage = By.XPath("//a[contains(text(),'Grant')]");
        private By grantTestCompanyInGrantPage = By.XPath("//td[contains(text(),'Grant Test Company One')]");
        #endregion

        #region Constants
        string cusSegmentConst = "Custom Segments";
        string customizationConst = "Customization";
        string listRecordsFieldsConst = "Lists, Records, & Fields";
        string regionConst = "Region";
        string southWestConst = "Southwest";
        string northernNevadaChapterConst = "Northern Nevada Chapter";
        string grantCustomSegmentConst = "Grant";
        string grantTestCompanyValueInGrantPage = "Grant Test Company One";
        #endregion

        #region Public Methods

        #region Segmentation
        public void CustomSegments()
        {
            try
            {
                this.TESTREPORT.LogInfo("Entered into Custom Segments Page");
                driver.ClickElementUsingWebElement(customization, customizationConst);
                driver.WaitForPageLoad(TimeSpan.FromSeconds(5));
                driver.ClickElementUsingWebElement(listRecordsFields, listRecordsFieldsConst);
                driver.ClickElementUsingWebElement(customSegments, cusSegmentConst);
                driver.WaitForPageLoad(TimeSpan.FromSeconds(30));
                this.TESTREPORT.LogSuccess("Navigation to Custom Segment is success", EngineSetup.GetScreenShotPath());
            }
            catch(Exception ex)
            {
                this.TESTREPORT.LogFailure("Navigation to Custom Segment Page is not success ", EngineSetup.GetScreenShotPath());
            }

        }
        #endregion

        #region Custom Segments Region Value
        public void CustomSegmentsRegionValue()
        {
            try
            {
                driver.SwitchTo().Frame(1);
                this.TESTREPORT.LogInfo("Entered into Custom Segments - REGION Page");
                driver.ClickElementUsingWebElement(regionLabel, regionConst);
                driver.WaitForPageLoad(TimeSpan.FromSeconds(8));
                driver.CheckElementExists(regionLabelInRegionPage, regionConst);
                driver.CheckElementExists(customRecordTypeInRegionPage, regionConst);
                driver.ScrollPage();
                driver.ScrollPage();
                driver.CheckElementExists(southWestElement, southWestConst);
                driver.CheckElementExists(northernNevadaChapterElement, northernNevadaChapterConst);
                driver.SwitchTo().DefaultContent();
                driver.ClickElementUsingWebElement(customSegments, cusSegmentConst);
                
            }
            catch(Exception ex)
            {
                this.TESTREPORT.LogFailure("Navigation to Custom Segment- REGION Page is not success ", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Custom Segments Grant Value
        public void CustomSegmentsGrantValue()
        {
            driver.SwitchTo().Frame(1);
            this.TESTREPORT.LogInfo("Entered into Custom Segments - GRANT Page");
            driver.ClickElementUsingWebElement(grantCustomSegment, grantCustomSegmentConst);
            driver.WaitForPageLoad(TimeSpan.FromSeconds(8));
            driver.CheckElementExists(grantLableInGrantPage, grantCustomSegmentConst);
            driver.CheckElementExists(customRecordTypeInGrantPage, grantCustomSegmentConst);
            driver.ScrollPage();
            driver.ScrollPage();
            driver.CheckElementExists(grantTestCompanyInGrantPage, grantTestCompanyValueInGrantPage);
            driver.SwitchTo().DefaultContent();
            driver.ClickElementUsingWebElement(customSegments, cusSegmentConst);
            
        }
        #endregion

        #endregion
    }
}
