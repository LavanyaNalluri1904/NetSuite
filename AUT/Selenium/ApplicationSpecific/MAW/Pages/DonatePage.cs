using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.UIHandlers.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Engine.Setup;
using AUT.Selenium.CommonReUsablePages;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace AUT.Selenium.ApplicationSpecific.MAW.Pages
{
    public class DonatePage : AbstractTemplatePage
    {
        #region Constructors
        public DonatePage()
        {
            
        }
        public DonatePage(IWebDriver driver)
        {
            this.driver = driver;
        }
        #endregion 

        #region UI Object Repository

        private By rbtn50 =  By.CssSelector("input[data-amount='$50']");
        private By rbtn100 = By.CssSelector("input[data-amount='$100']");
        private By rbtn500 = By.CssSelector("input[data-amount='$500']");
        private By rbtnOther = By.Id("donation-level-other");
        private By rbtnOtherAmount = By.Id("donation-level-other-amount");

        private By chkDonateInHonorOrMemory = By.CssSelector("#honor-memorial-toggle");
        private By chkDonateMonthly = By.Id("donate-monthly-toggle");

        #region Card Type & Card Design
        private By rbtnCardTypeEcard = By.Id("honor-memorial-e-card-products");
        private By rbtnCardTypePaper = By.Id("honor-memorial-card-products");
        private By rbtnCardTypeCertificate = By.Id("honor-memorial-certificate-products");

        private By rbtnCardDesignEcard = By.XPath("//label[contains(text(),'General E-Card (Mahlia)')]//input");
        private By btnCardDesignEcardPreview = By.XPath("//label[contains(text(),'General E-Card (Mahlia)')]//button[contains(text(),'Preview')]");
        private By rbtnCardDesignMemorial = By.XPath("//label[contains(text(),'Memorial Postcard')]//input");
        private By btnCardDesignMemorialPreview = By.XPath("//label[contains(text(),'Memorial Postcard')]//button");
        private By imgCardDesignMemorial=By.XPath("//label[contains(text(),'Memorial Postcard')]//img");
        private By rbtnCardDesignTributePostCard = By.XPath("//label[contains(text(),'Tribute Postcard')]//input");
        private By btnCardDesignTributePostCardPreview = By.XPath("//label[contains(text(),'Tribute Postcard')]//button");
        private By imgCardDesignTribute = By.XPath("//label[contains(text(),'Tribute Postcard')]//img");
        private By rbtnCardDesignTributeCrt = By.XPath("//label[contains(text(),'Tribute Certificate')]//input");
        private By btnCardDesignTributeCrtPreview = By.XPath("//label[contains(text(),'Tribute Certificate')]//button");
        private By imgCardDesignTributeCrt = By.XPath("//label[contains(text(),'Tribute Certificate')]//img");

        private By txtHonoreeName = By.Name("honoree_full_name");
        private By txtECardHonoreeFirstName = By.CssSelector("input#donation-form__tribute_first_name--e-card");
        private By txtECardHonoreeLastName = By.CssSelector("input#donation-form__tribute_last_name--e-card");
        private By txtPaperCardHonoreeFirstName = By.CssSelector("input#donation-form__tribute_first_name--card");
        private By txtPaperCardHonoreeLastName = By.CssSelector("input#donation-form__tribute_last_name--card");
        private By txtPrintableCertHonoreeFirstName = By.CssSelector("input#donation-form__tribute_first_name--certificate");
        private By txtPrintableCertHonoreeLastName = By.CssSelector("input#donation-form__tribute_last_name--certificate");

        private By txtEmailAddress = By.Name("ecard.recipients");
        private By txtPersonalMessage = By.Id("donation-form__ecard-message");
        private By txtECardFromName = By.CssSelector("#donation-form__from_name--e-card");
        private By txtPaperCardFromName = By.CssSelector("#donation-form__from_name--card");
        private By txtPrintableCertFromName = By.CssSelector("#donation-form__from_name--certificate");

        private By txtCardFirstName = By.Name("send_to_first_name");
        private By txtCardLastName = By.Name("send_to_last_name");
        private By txtCardStreetAddress1 = By.Name("send_to_street_address_1");
        private By txtCardStreetAddress2 = By.Name("send_to_street_address_2");
        private By txtCardPostalCode = By.Name("send_to_postal_code");

        private By imgGeneralECardDesigns = By.XPath("//div[contains(@class,'honor-memorial-designs honor-memorial-designs--e-card')]//img");

        #endregion

        #region General E-Card popupWindow
        private By imgCardDesignEcardImage = By.XPath("//div[@id='flashContent']/p/img");
        private By txtCardDesignEcardTextPart1 = By.XPath("//div[@id='card-text']/p[1]");
        private By txtCardDesignEcardTextPart2 = By.XPath("//div[@id='card-text']/p[2] ");
        #endregion

        #region PaperCard CardDesigns  popupWindow
        private By rbtnPaperCardDesignImage = By.CssSelector("#zoom_preview");
        private By txtPaperCardDesignPreview = By.CssSelector(".fancybox-outer h1");
        private By txtPaperCardDesignHeading1 = By.CssSelector(".cardDescription>h3");
        private By txtPaperCardDesignHeading2 = By.XPath("//h4[1]");
        private By txtPaperCardDesignHeading3 = By.XPath("//h4[2]");
        private By imgPaperCardDesignFront = By.XPath("//div[@class='card-thumbs clearfix hide--xs']/img[contains(@src,'front')]");
        private By imgPaperCardDesignBack = By.XPath("//div[@class='card-thumbs clearfix hide--xs']/img[contains(@src,'back')]");
        private By btnPaperCardDesignClose = By.CssSelector(".fancybox-inner a");
        private By lnkPaperCardDesignPDFLink = By.XPath("//span[@title='Click here to view a test PDF document']");
        #endregion

        private By lnkLearnMoreMonthly = By.ClassName("js--donate-monthly-learn-more");
        private By txtDonateMonthlyInfo = By.XPath("//div[contains(text(),'Your first donation will be charged today')]"); // By.ClassName("js--donate-monthly-learn-more-cancel"); //By.XPath("//a[contains(text(),'Close')]");

        private By lblBenefitsOfMembership = By.XPath("//h2[contains(text(),'Benefits of membership')]");
        private By btnLearnMonthlyClose = By.XPath("//div[@class='fancybox-inner']//a[contains(text(),'Close')]"); // By.ClassName("js--donate-monthly-learn-more-cancel"); //By.XPath("//a[contains(text(),'Close')]");


        private By rbtnNational = By.Id("donor-intent-national");
        private By rbtnLocal = By.Id("donor-intent-local");
        private By rbtnInternational = By.Id("donor-intent-international");

        private By listLocalChapters = By.Id("donor-intent-local-chapter");
        private By listLocalChaptersText = By.Id("select2-donor-intent-local-chapter-container");
        private By txtSearchChapterZipOrName = By.ClassName("select2-search__field");
        private By listInternationalChapters = By.Id("donor-intent-international-chapter");
        private By txtInternationalChapterText = By.Id("select2-donor-intent-international-chapter-container");


        private By btnSaveAndContinue = By.ClassName("button-submit");
        private By btnCancel = By.XPath("//a[contains(text(),'Cancel')]");

        private By lblLocalChapterError = By.XPath("//div[contains(text(),'Please select a local chapter')]");
        private By lblOtherAmountError = By.XPath("//label[@id='donation-level-other-amount-error']");
        private By btnClearFormAndStartOver = By.XPath("//a[contains(text(),'Clear form and start over')]");

        #endregion

        #region Public Methods

        #region Validate elements on Donate page
        /// <summary>
        /// Validate elements on Donate page
        /// </summary>
        /// <param name="chapterName">chapter name</param>
        public void validateDonatePageElements(string chapterName="")
        {
            try
            {
                //Denominations
                driver.CheckElementExists(rbtn50, "$50");
                driver.CheckElementExists(rbtn100, "$100");
                driver.CheckElementExists(rbtn500, "$500");
                driver.CheckElementExists(rbtnOther, "OTHER");

                driver.VerifyInputRadioButtonStatus(rbtn100, "checked", "$100");
                driver.VerifyInputRadioButtonStatus(rbtn50, "unchecked", "$50");
                driver.VerifyInputRadioButtonStatus(rbtn500, "unchecked", "$500");
                driver.ScrollPage();

                //OPTIONS
                driver.CheckElementExists(chkDonateInHonorOrMemory, "Donate In Honor or Memory");
                driver.CheckElementExists(chkDonateMonthly, "Donate Monthly");
                driver.VerifyCheckboxStatus(chkDonateInHonorOrMemory, "unchecked", "Donate In Honor or Memory");
                driver.VerifyCheckboxStatus(chkDonateMonthly, "unchecked", "Donate Monthly");

                //DESIGNATIONS
                if (!chapterName.ToLower().Equals("chapter"))
                {
                    driver.CheckElementExists(rbtnNational, "NATIONAL");
                    driver.CheckElementExists(rbtnLocal, "LOCAL");
                    driver.CheckElementExists(rbtnInternational, "INTERNATIONAL");
                }
                if (chapterName.ToLower().Equals(""))
                {
                    driver.VerifyInputRadioButtonStatus(rbtnNational, "checked", "NATIONAL");
                    driver.VerifyInputRadioButtonStatus(rbtnLocal, "unchecked", "LOCAL");
                    driver.VerifyInputRadioButtonStatus(rbtnInternational, "unchecked", "INTERNATIONAL");
                }
                else if (chapterName.ToLower().Equals("local"))
                {
                    driver.VerifyInputRadioButtonStatus(rbtnNational, "unchecked", "NATIONAL");
                    driver.VerifyInputRadioButtonStatus(rbtnLocal, "checked", "LOCAL");
                    driver.VerifyInputRadioButtonStatus(rbtnInternational, "unchecked", "INTERNATIONAL");
                }
                else if (chapterName.ToLower().Equals("international"))
                {
                    driver.VerifyInputRadioButtonStatus(rbtnNational, "unchecked", "NATIONAL");
                    driver.VerifyInputRadioButtonStatus(rbtnLocal, "unchecked", "LOCAL");
                    driver.VerifyInputRadioButtonStatus(rbtnInternational, "checked", "INTERNATIONAL");
                }
                else if (chapterName.ToLower().Equals("chapter"))
                {
                    validateDesignationsNotDisplayed();
                }
                driver.CheckElementExists(btnSaveAndContinue, "SAVE AND CONTINUE");
                driver.CheckElementExists(btnCancel, "CANCEL");
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("ValidateDonatePageElements", "Error occurred while validating elements on donate page", EngineSetup.GetScreenShotPath());
            }

        }
        #endregion

        #region Navigate to donation review page
        /// <summary>
        /// Navigate to donation review page
        /// </summary>
        public void navigateToDonationReviewPage()
        {
            try
            {
                driver.ScrollToPageBottom();
                driver.ClickElement(btnSaveAndContinue, "Save and Continue");
                driver.WaitForPageLoad(TimeSpan.FromSeconds(60));
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("NavigateToDonationReviewPage", "Error occurred while navigating to donation review page", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Validate Donate Monthly option
        /// <summary>
        /// Method to validate donate monthly option
        /// </summary>
        public void validateDonateMonthly()
        {
            try
            {
                driver.ScrollPage();
                driver.ClickElement(chkDonateMonthly, "Donate Montly");
                driver.CheckElementExists(txtDonateMonthlyInfo, "Donate Monthly info");
                string nextMonthDate = DateTime.Now.AddMonths(1).ToString("M/dd/yyyy");
                driver.VerifyTextValue(txtDonateMonthlyInfo, string.Format("Your first donation will be charged today and then monthly starting on {0}.",nextMonthDate), "Donate Monthly Info");
                driver.ClickElement(lnkLearnMoreMonthly, "Learn more about our monthly donor program and benefits.");
                driver.CheckElementExists(lblBenefitsOfMembership, "Benefits of Membership");
                driver.CheckElementExists(btnLearnMonthlyClose, "Close");
                driver.ClickElement(btnLearnMonthlyClose, "Close");
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("ValidateDonateMonthly", "Error occurred while validating Donate Monthly option", EngineSetup.GetScreenShotPath());
            }
        }

        #endregion

        #region Validate Satellite Data collector value from page source
        /// <summary>
        /// Method to validate Satellite Data collector value from page source
        /// </summary>
        public void validateSatelliteDataCollectorValue()
        {
            try
            {
                driver.CheckTagInformationInSource("datacollector_0_AnalyticsTrackerContainer", "\"siteId\": '100-000'");
                driver.CheckTagInformationInSource("datacollector_0_AnalyticsTrackerContainer", "\"ThirdPartyId\": \"Convio\"");
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateSatelliteDataCollectorValue", "Error occurred while validating Satellite Data Collector values", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Validate Satellite footer script container data from page source 
        /// <summary>
        /// Method to validate Satellite footer script container data from page source
        /// </summary>
        public void validateSatelliteFooterScriptContainerValues()
        {
            try
            {
                driver.CheckTagInformationInSource("SatelliteFooterScriptContainer", "Google Code for Remarketing Tag");
                driver.CheckTagInformationInSource("SatelliteFooterScriptContainer", "Facebook Pixel Code");
                driver.CheckTagInformationInSource("SatelliteFooterScriptContainer", "DoubleClick DART");
                driver.CheckTagInformationInSource("SatelliteFooterScriptContainer", "Quantcast Tag");
                driver.CheckTagInformationInSource("SatelliteFooterScriptContainer", "WPMG ROI Tracking Script");
                driver.CheckTagInformationInSource("SatelliteFooterScriptContainer", "TnT Beacon Code");
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateSatelliteFooterScriptContainerValues", "Error occurred while validating Satellite Footer Script Container values", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Validate General E-Card image source is from products.json file
        /// <summary>
        /// Method to validate General E-Card image source is from products.json file
        /// </summary>
        /// <param name="jsonFilePath"></param>
        public void validateGeneralECardProductSource(string jsonFilePath)
        {
            try
            {
                driver.ClickElement(chkDonateInHonorOrMemory, "Select Donate In Honor Or Memory");
                driver.ScrollPage();
                driver.VerifyInputRadioButtonStatus(rbtnCardTypeEcard, "checked", "E-Card");
                driver.VerifyInputRadioButtonStatus(rbtnCardDesignEcard, "unchecked", "General E-Card (Mahlia)");
                driver.ScrollPage();
                driver.CheckElementExists(imgGeneralECardDesigns, "General E-Card (Mahlia) Card design");
              
                string imgSource = driver.GetElementAttribute(imgGeneralECardDesigns, "src");
                if (imgSource.Contains("https://secure2.wish.org"))
                    imgSource = imgSource.Replace("https://secure2.wish.org", "");
                if (imgSource.Contains("https://secure2.convio.net/wishdev"))
                    imgSource = imgSource.Replace("https://secure2.convio.net/wishdev", "");
                driver.VerifyDataInJsonFile(jsonFilePath, imgSource);

            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateProductSource", "Error occurred while validating product source", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Validate Paper Card-Memorial Post Card image source is from products.json file
        /// <summary>
        /// Method to validate Paper Card-Memorial Post Card image source is from products.json file
        /// </summary>
        /// <param name="jsonFilePath"></param>
        public void validateMemorialPostCardSource(string jsonFilePath)
        {
            try
            {
                driver.ClickElement(chkDonateInHonorOrMemory, "Select Donate In Honor Or Memory");
                driver.ScrollPage();
                driver.ClickElement(rbtnCardTypePaper, "Paper Card");
                driver.ClickElement(rbtnCardDesignMemorial, "Memorial Postcard (Kionna)");
                driver.VerifyInputRadioButtonStatus(rbtnCardDesignMemorial, "checked", "Memorial Postcard (Kionna)");
                driver.ScrollPage();
                driver.CheckElementExists(imgCardDesignMemorial, "Memorial Postcard (Kionna) Card design");

                string imgSource = driver.GetElementAttribute(imgCardDesignMemorial, "src");
                if (imgSource.Contains("https://secure2.wish.org"))
                    imgSource = imgSource.Replace("https://secure2.wish.org", "");
                if (imgSource.Contains("https://secure2.convio.net/wishdev"))
                    imgSource = imgSource.Replace("https://secure2.convio.net/wishdev", "");
                driver.VerifyDataInJsonFile(jsonFilePath, imgSource);

            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateProductSource", "Error occurred while validating product source", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Validate Paper Card-Tribute Post Card image source is from products.json file
        /// <summary>
        /// Method to validate Paper Card-Tribute Post Card image source is from products.json file
        /// </summary>
        /// <param name="jsonFilePath"></param>
        public void validateTributePostCardSource(string jsonFilePath)
        {
            try
            {
                driver.ClickElement(chkDonateInHonorOrMemory, "Select Donate In Honor Or Memory");
                driver.ScrollPage();
                driver.ClickElement(rbtnCardTypePaper, "Paper Card");
                driver.ClickElement(rbtnCardDesignTributePostCard, "Tribute Postcard (Gabe)");
                driver.VerifyInputRadioButtonStatus(rbtnCardDesignTributePostCard, "checked", "Tribute Postcard (Gabe)");
                driver.ScrollPage();
                driver.CheckElementExists(imgCardDesignTribute, "Tribute Postcard (Gabe) Card design");

                string imgSource = driver.GetElementAttribute(imgCardDesignTribute, "src");
                if (imgSource.Contains("https://secure2.wish.org"))
                    imgSource = imgSource.Replace("https://secure2.wish.org", "");
                if (imgSource.Contains("https://secure2.convio.net/wishdev"))
                    imgSource = imgSource.Replace("https://secure2.convio.net/wishdev", "");
                driver.VerifyDataInJsonFile(jsonFilePath, imgSource);

            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateProductSource", "Error occurred while validating product source", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Validate Paper Card-Tribute Post Card image source is from products.json file
        /// <summary>
        /// Method to validate Paper Card-Tribute Post Card image source is from products.json file
        /// </summary>
        /// <param name="jsonFilePath"></param>
        public void validateTributeCertPostCardSource(string jsonFilePath)
        {
            try
            {
                driver.ClickElement(chkDonateInHonorOrMemory, "Select Donate In Honor Or Memory");
                driver.ScrollPage();
                driver.ClickElement(rbtnCardTypeCertificate, "Printable Certificate");
                driver.ClickElement(rbtnCardDesignTributeCrt, "Tribute Certificate (Mahlia)");
                driver.VerifyInputRadioButtonStatus(rbtnCardDesignTributeCrt, "checked", "Tribute Certificate (Mahlia)");
                driver.ScrollPage();
                driver.CheckElementExists(imgCardDesignTributeCrt, "Tribute Certificate (Mahlia) Card design");

                string imgSource = driver.GetElementAttribute(imgCardDesignTributeCrt, "src");
                if (imgSource.Contains("https://secure2.wish.org"))
                    imgSource = imgSource.Replace("https://secure2.wish.org", "");
                if (imgSource.Contains("https://secure2.convio.net/wishdev"))
                    imgSource = imgSource.Replace("https://secure2.convio.net/wishdev","");
                driver.VerifyDataInJsonFile(jsonFilePath, imgSource);

            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateProductSource", "Error occurred while validating product source", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Re-usable method to select donation amount on Make-A-Wish donation page
        /// <summary>
        /// Method to select donation amount on Make-A-Wish donation page
        /// </summary>
        /// <param name="amount">amount</param>
        public void selectDonationAmount(string amount)
        {
            try
            {
                driver.ScrollPage();
                if (amount.Contains("$"))
                {
                    amount = amount.Replace("$", "");
                }
                switch (amount)
                {
                    case "50":
                        driver.ClickElement(rbtn50, amount);
                        break;
                    case "100":
                        driver.ClickElement(rbtn100, amount);
                        break;
                    case "500":
                        driver.ClickElement(rbtn500, amount);
                        break;
                    default:
                        driver.ClickElement(rbtnOther, "OTHER");
                        driver.SendKeysToElement(rbtnOtherAmount, amount, "OTHER");
                        break;
                }
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("selectDonationAmount", "Error occurred while selecting donation amount", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Re-usable method to select designation
        /// <summary>
        /// Method to select designation 
        /// </summary>
        /// <param name="designation">designation name</param>
        /// <param name="chapterName">chapter name</param>
        public void selectDesignation(string designation, string chapterName = "")
        {
            try
            {
                driver.ScrollPage();
                switch (designation.ToUpper())
                {
                    case "NATIONAL":
                        driver.ClickElement(rbtnNational, designation);
                        break;
                    case "LOCAL":
                        driver.ClickElement(rbtnLocal, designation);
                        if (chapterName != "")
                            driver.SelectDropdownItemByText(listLocalChapters, chapterName, "Local Chapter: " + chapterName);
                        break;
                    case "INTERNATIONAL":
                        driver.ClickElement(rbtnInternational, designation);
                        if (chapterName != "")
                            driver.SelectDropdownItemByText(listInternationalChapters, chapterName, "International Chapter: " + chapterName);
                        break;
                }
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("selectDesignation", "Error occurred while selecting designation", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Validate National chapter name resolves using zip code
        /// <summary>
        /// Method to validate National chapter name resolves using zip code
        /// </summary>
        /// <param name="zipCode">area zip code</param>
        /// <param name="chapterName">chapter name</param>
        public void validateNationalChapterUsingZipCode(string zipCode,string chapterName)
        {
            try
            {
                driver.ScrollPage();
                driver.ClickElement(rbtnLocal, "LOCAL");
                driver.ScrollPage();
                driver.ClickElement(listLocalChaptersText, "Local Chapters list");
                if (driver.CheckElementExists(txtSearchChapterZipOrName, "Chapter Name or Zip Code search text box"))
                    driver.SendKeysToElementAndPressEnter(txtSearchChapterZipOrName, zipCode, "Chapter Name or Zip Code search text box");
                driver.VerifyTextValue(listLocalChaptersText, chapterName, "Chapter Name List");
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateNationalChapterUsingZipCode", "Error occurred while validating national chapter name using zip code", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Validate chapter name resolves to proper chapter name
        /// <summary>
        /// Method to validate chapter name resolves to proper MAW chapter name 
        /// </summary>
        /// <param name="localAreaName">local chapter name</param>
        /// <param name="chapterName">MAW chapter name</param>
        public void validateNationalChapterUsingAreaName(string localAreaName, string chapterName)
        {
            try
            {
                driver.ScrollPage();
                driver.ClickElement(rbtnLocal, "LOCAL");
                driver.ScrollPage();
                driver.ClickElement(listLocalChaptersText, "Local Chapters list");
                if (driver.CheckElementExists(txtSearchChapterZipOrName, "Chapter Name or Zip Code search text box"))
                    driver.SendKeysToElementAndPressEnter(txtSearchChapterZipOrName, localAreaName, "Chapter Name or Zip Code search text box");
                driver.VerifyTextValue(listLocalChaptersText, chapterName, "Chapter Name List");
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateNationalChapterUsingAreaName", "Error occurred while validating National designation using local Area Name", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Validate international chapter name resolves using country name
        /// <summary>
        /// Method to validate international chapter name resolves using country name
        /// </summary>
        /// <param name="countryName"></param>
        /// <param name="intlChapterName"></param>
        public void validateInternationalChapterUsingCountryName(string countryName, string intlChapterName)
        {
            try
            {
                driver.ScrollPage();
                driver.ClickElement(rbtnInternational, "INTERNATIONAL");
                driver.ScrollPage();
                driver.ClickElement(txtInternationalChapterText, "Local Chapters list");
                if (driver.CheckElementExists(txtSearchChapterZipOrName, "Country Name search text box"))
                    driver.SendKeysToElementAndPressEnter(txtSearchChapterZipOrName, countryName, "Country Name search text box");
                driver.VerifyTextValue(txtInternationalChapterText, intlChapterName, "International Chapter List");
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateInternationalChapterUsingCountryName", "Error occurred while validating international designation using country name", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Validate card type and card designs
        /// <summary>
        /// Method to validate card type and card designs
        /// </summary>
        public void validateCardTypesAndDesigns()
        {
            try
            {
                driver.ClickElement(chkDonateInHonorOrMemory, "Select Donate In Honor Or Memory");
                driver.ScrollPage();
                driver.VerifyInputRadioButtonStatus(rbtnCardTypeEcard, "checked", "E-Card");
                driver.VerifyInputRadioButtonStatus(rbtnCardTypePaper, "unchecked", "Paper Card");
                driver.VerifyInputRadioButtonStatus(rbtnCardTypeCertificate, "unchecked", "Printable Certificate");
                driver.VerifyInputRadioButtonStatus(rbtnCardDesignEcard, "unchecked", "General E-Card (Mahlia)");
                driver.ClickElement(rbtnCardTypePaper, "Paper Card");
                driver.VerifyInputRadioButtonStatus(rbtnCardDesignMemorial, "unchecked", "Memorial Postcard (Kionna)");
                driver.VerifyInputRadioButtonStatus(rbtnCardDesignTributePostCard, "unchecked", "Tribute Postcard (Gabe)");
                driver.ClickElement(rbtnCardTypeCertificate, "Printable Certificate");
                driver.VerifyInputRadioButtonStatus(rbtnCardDesignTributeCrt, "unchecked", "Tribute Certificate (Mahlia)");
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateCardTypesAndDesigns", "Error occurred while validating section view for E-Card", EngineSetup.GetScreenShotPath());
            }

        }
        #endregion

        #region Validate sections of e-card
        /// <summary>
        /// Method to validate sections of E-Card 
        /// </summary>
        public void validateSectionViewForE_Card()
        {
            try
            {
                driver.ClickElement(chkDonateInHonorOrMemory, "Select Donate In Honor Or Memory");
                driver.ScrollPage();
                driver.VerifyInputRadioButtonStatus(rbtnCardTypeEcard, "checked", "E-Card");
                driver.VerifyInputRadioButtonStatus(rbtnCardDesignEcard, "unchecked", "General E-Card (Mahlia)");
                driver.ScrollPage();
                driver.CheckElementExists(txtHonoreeName, "Honoree Name");
                driver.CheckElementExists(txtCardFirstName, "Card FirstName");
                driver.CheckElementExists(txtCardLastName, "Card LastName");
                driver.CheckElementExists(txtEmailAddress, "EmailAddress");
                driver.CheckElementExists(txtPersonalMessage, "Personal Message");
                driver.CheckElementExists(txtECardFromName, "From Name");
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateSectionViewForE_Card", "Error occurred while validating section view for E-Card", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Validate card design for Genearal E-card
        /// <summary>
        /// Method to validate card design for General -E-card
        /// </summary>
        public void validateCardDesignForGeneralE_Card()
        {
            try
            {
                driver.ClickElement(chkDonateInHonorOrMemory, "Select Donate In Honor Or Memory");
                driver.ScrollPage();
                driver.VerifyInputRadioButtonStatus(rbtnCardTypeEcard, "checked", "E-Card");
                driver.ClickElement(rbtnCardDesignEcard, "General E-Card (Mahlia)");
                driver.VerifyInputRadioButtonStatus(rbtnCardDesignEcard, "checked", "General E-Card (Mahlia)");
                driver.ClickElement(btnCardDesignEcardPreview, "General E-Card (Mahlia) Preview");
                SimulateSendKeys("testdev");
                SimulateSendKeys("{ENTER}");

                driver.switchToNewWindow();
                if (driver.isAlertPresent())
                {
                    IAlert devAlert = driver.SwitchTo().Alert();
                    devAlert.SendKeys("testdev");
                    devAlert.Accept();
                }
                driver.CheckElementExists(imgCardDesignEcardImage, "General E-Card (Mahlia) Image Popup Window");
                driver.WaitElementTextEquals(txtCardDesignEcardTextPart1, "A donation has been made in your name to the Make-A-Wish Foundation®","E-Card Text 1");
                //driver.WaitElementTextEquals(rbtnCardDesignEcardTextPart2, "A donation has been made in your name to the Make-A-Wish Foundation®","E-Card Text 2");
                driver.switchBackToMainWindow();
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateCardDesignForGeneralE_Card", "Error occurred while validating CardDesign for General E_Card", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Validate sections of Paper card
        /// <summary>
        /// Method to validate sections of Paper card
        /// </summary>
        public void validateSectionViewForPaperCard()
        {
            try
            {
                driver.ClickElement(chkDonateInHonorOrMemory, "Select Donate In Honor Or Memory");
                driver.ScrollPage();
                driver.VerifyInputRadioButtonStatus(rbtnCardTypeEcard, "checked", "E-Card");
                driver.ClickElement(rbtnCardTypePaper, "Paper Card");
                driver.ScrollPage();
                driver.VerifyInputRadioButtonStatus(rbtnCardTypePaper, "checked", "Paper Card");
                driver.VerifyInputRadioButtonStatus(rbtnCardDesignMemorial, "unchecked", "Memorial Postcard (Kionna)");
                driver.VerifyInputRadioButtonStatus(rbtnCardDesignTributePostCard, "unchecked", "Tribute Postcard (Gabe)");
                driver.ScrollPage();

                driver.CheckElementExists(txtPaperCardHonoreeFirstName, "Honoree FirstName");
                driver.CheckElementExists(txtPaperCardHonoreeLastName, "Honoree LastName");
                driver.CheckElementExists(txtCardFirstName, "Card FirstName");
                driver.CheckElementExists(txtCardLastName, "Card LastName");
                driver.CheckElementExists(txtCardStreetAddress1, "Card StreetAddress1");
                driver.CheckElementExists(txtCardStreetAddress2, "Card StreetAddress2");
                driver.CheckElementExists(txtCardPostalCode, "Card PostalCode");
                driver.CheckElementExists(txtPaperCardFromName, "From Name");
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateSectionViewForPaperCard", "Error occurred while validating section view for PaperCard", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Valdiate card design for memorial post card
        /// <summary>
        /// Method to valdiate card design for memorial post card
        /// </summary>
        public void validateCardDesignForMemorialPostcard()
        {
            try
            {
                driver.ClickElement(chkDonateInHonorOrMemory, "Select Donate In Honor Or Memory");
                driver.ScrollPage();
                driver.VerifyInputRadioButtonStatus(rbtnCardTypeEcard, "checked", "E-Card");
                driver.ClickElement(rbtnCardTypePaper, "Paper Card");
                driver.VerifyInputRadioButtonStatus(rbtnCardTypePaper, "checked", "Paper Card");
                driver.ClickElement(rbtnCardDesignMemorial, "Memorial Postcard (Kionna)");
                driver.VerifyInputRadioButtonStatus(rbtnCardDesignMemorial, "checked", "Memorial Postcard (Kionna)");
                driver.ClickElement(btnCardDesignMemorialPreview, "Memorial Postcard (Kionna) Preview");

                driver.CheckElementExists(rbtnPaperCardDesignImage, "Zoom Preview Image");
                driver.WaitElementTextEquals(txtPaperCardDesignPreview, "PREVIEW", "PREVIEW");
                //driver.WaitElementTextEquals(txtPaperCardDesignHeading1, "MEMORIAL CARD\nSize: 5\" X 7\"");
                driver.WaitElementTextEquals(txtPaperCardDesignHeading2, "Front Sentiment", "Front Sentiment");
                driver.WaitElementTextEquals(txtPaperCardDesignHeading3, "Back Sentiment","Back Sentiment");
                driver.CheckElementExists(imgPaperCardDesignFront, "PaperCardDeisgn Image Front");
                driver.ClickElement(imgPaperCardDesignFront, "PaperCardDeisgn Image Front");
                driver.CheckElementExists(imgPaperCardDesignBack, "PaperCardDeisgn Image Back");
                driver.ClickElement(imgPaperCardDesignBack, "PaperCardDeisgn Image Back");
                driver.ClickElement(btnPaperCardDesignClose, "Close");
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateCardDesignForMemorialPostcard", "Error occurred while validating CardDesign for Memorial Postcard", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Validate card design for Tribute post card
        /// <summary>
        /// Method to validate card design for Tribute post card
        /// </summary>
        public void validateCardDesignForTributePostcard()
        {
            try
            {
                driver.ClickElement(chkDonateInHonorOrMemory, "Select Donate In Honor Or Memory");
                driver.ScrollPage();
                driver.VerifyInputRadioButtonStatus(rbtnCardTypeEcard, "checked", "E-Card");
                driver.ClickElement(rbtnCardTypePaper, "Paper Card");
                driver.VerifyInputRadioButtonStatus(rbtnCardTypePaper, "checked", "Paper Card");
                driver.ClickElement(rbtnCardDesignTributePostCard, "Tribute Postcard (Gabe)");
                driver.VerifyInputRadioButtonStatus(rbtnCardDesignTributePostCard, "checked", "Tribute Postcard (Gabe)");
                driver.ClickElement(btnCardDesignTributePostCardPreview, "Tribute Postcard (Gabe) Preview");

                driver.CheckElementExists(rbtnPaperCardDesignImage, "Zoom Preview Image");
                driver.WaitElementTextEquals(txtPaperCardDesignPreview, "PREVIEW","PREVIEW");
                driver.WaitElementTextEquals(txtPaperCardDesignHeading1, "TRIBUTE CARD\r\nSize: 5\" X 7\"","Tribute Card Size");
                driver.WaitElementTextEquals(txtPaperCardDesignHeading2, "Front Sentiment","Front Sentiment");
                driver.WaitElementTextEquals(txtPaperCardDesignHeading3, "Back Sentiment","Back Sentiment");
                driver.CheckElementExists(imgPaperCardDesignFront, "PaperCardDesign Image Front");
                driver.ClickElement(imgPaperCardDesignFront, "PaperCardDesign Image Front");
                driver.CheckElementExists(imgPaperCardDesignBack, "PaperCardDesign Image Back");
                driver.ClickElement(imgPaperCardDesignBack, "PaperCardDesign Image Back");
                driver.ClickElement(btnPaperCardDesignClose, "Close");
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateCardDesignForTributePostcard", "Error occurred while validating CardDesign for TributePostcard", EngineSetup.GetScreenShotPath());
            }

        }
        #endregion

        #region Validate sections for Printable Certificate
        /// <summary>
        /// Method to validate sections for Printable Certificate
        /// </summary>
        public void validateSectionViewForPrintableCertificate()
        {
            try
            {
                driver.ClickElement(chkDonateInHonorOrMemory, "Select Donate In Honor Or Memory");
                driver.ScrollPage();
                driver.VerifyInputRadioButtonStatus(rbtnCardTypeEcard, "checked", "E-Card");
                driver.VerifyInputRadioButtonStatus(rbtnCardDesignEcard, "unchecked", "General E-Card (Mahlia)");
                driver.ClickElement(rbtnCardTypeCertificate, "Printable Certificate");
                driver.ScrollPage();
                driver.VerifyInputRadioButtonStatus(rbtnCardTypeCertificate, "checked", "Printable Certificate");
                driver.VerifyInputRadioButtonStatus(rbtnCardDesignTributeCrt, "unchecked", " Tribute Certificate (Mahlia)");

                driver.ScrollPage();

                driver.CheckElementExists(txtPrintableCertHonoreeFirstName, "Honoree FirstName");
                driver.CheckElementExists(txtPrintableCertHonoreeLastName, "Honoree LastName");
                driver.CheckElementExists(txtPrintableCertFromName, "From Name");
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateSectionViewForPrintableCertificate", "Error occurred while validating section view for Printable Certificate", EngineSetup.GetScreenShotPath());
            }

        }
        #endregion

        #region Validate sections of Tribute Certificate
        /// <summary>
        /// Method to validate sections of Tribute Certificate
        /// </summary>
        public void validateSectionViewForTributeCertificate()
        {
            try
            {
                driver.ClickElement(chkDonateInHonorOrMemory, "Select Donate In Honor Or Memory");
                driver.ScrollPage();
                driver.VerifyInputRadioButtonStatus(rbtnCardTypeCertificate, "unchecked", "Printable Certificate");
                driver.ClickElement(rbtnCardTypeCertificate, "Printable Certificate");
                driver.VerifyInputRadioButtonStatus(rbtnCardTypeCertificate, "checked", "Printable Certificate");

                driver.ScrollPage();
                driver.VerifyInputRadioButtonStatus(rbtnCardDesignTributeCrt, "unchecked", "Tribute Certificate (Mahlia)");
                driver.ClickElement(rbtnCardDesignTributeCrt, "Tribute Certificate (Mahlia)");
                driver.VerifyInputRadioButtonStatus(rbtnCardDesignTributeCrt, "checked", " Tribute Certificate (Mahlia)");
                driver.ScrollPage();

                driver.ClickElement(btnCardDesignTributeCrtPreview, "Tribute Certificate (Mahlia) Preview");

                driver.CheckElementExists(rbtnPaperCardDesignImage, "Zoom Preview Image");
                driver.WaitElementTextEquals(txtPaperCardDesignPreview, "PREVIEW","PREVIEW");
                driver.WaitElementTextEquals(txtPaperCardDesignHeading1, "TRIBUTE CERTIFICATE\r\nSize: 8 1/2\" X 11\"", "TRIBUTE CERTIFICATE Size");
                driver.WaitElementTextEquals(txtPaperCardDesignHeading2, "Sentiment","Sentiment");
                driver.WaitElementTextEquals(txtPaperCardDesignHeading3, "Details","Details");
                driver.CheckElementExists(lnkPaperCardDesignPDFLink, "PDF Link");
                driver.ClickElement(btnPaperCardDesignClose, "Close");
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateSectionViewForTributeCertificate", "Error occurred while validating section view for Tribute Certificate", EngineSetup.GetScreenShotPath());
            }

        }
        #endregion

        #region Re-usable method to select a local designation
        /// <summary>
        /// Re-usable method to select a local designation
        /// </summary>
        /// <param name="chapterName"></param>
        public void selectLocalDesignation(String chapterName)
        {
            try
            {
                selectDesignation("LOCAL", chapterName);
                driver.ClickElement(btnSaveAndContinue, "Save and Continue");
                driver.WaitForPageLoad(TimeSpan.FromSeconds(60));
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("selectLocalDesignation", "Error occurred while navigating to donation review page", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Re-usable method to select a International designation
        /// <summary>
        /// Re-usable method to select a International designation
        /// </summary>
        /// <param name="chapterName"></param>
        public void selectInternationalDesignation(String chapterName)
        {
            try
            {
                selectDesignation("INTERNATIONAL", chapterName);
                driver.ClickElement(btnSaveAndContinue, "Save and Continue");
                driver.WaitForPageLoad(TimeSpan.FromSeconds(60));
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("selectInternationalDesignation", "Error occurred while selecting international designation", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Method to update e-card honoree information
        /// <summary>
        /// Method to update e-card honoree information
        /// </summary>
        /// <param name="honoreeName"></param>
        /// <param name="honoreeFirstName"></param>
        /// <param name="honoreeLastName"></param>
        /// <param name="emailAddress"></param>
        /// <param name="fromName"></param>
        /// <param name="personalMessage"></param>
        public void updateEcardHonoreeInformation(string honoreeName, string honoreeFirstName, string honoreeLastName, string emailAddress, string fromName, string personalMessage = "")
        {
            try
            {
                driver.ScrollPage();
                driver.ClickElement(chkDonateInHonorOrMemory, "Donor in Honor or Memory");
                driver.ClickElement(rbtnCardDesignEcard, "Card Design E-Card");
                driver.ScrollPage();
                driver.SendKeysToElement(txtHonoreeName, honoreeName,"Honoree Name");
                driver.SendKeysToElement(txtECardHonoreeFirstName, honoreeFirstName, "Honoree First Name");
                driver.SendKeysToElement(txtECardHonoreeLastName, honoreeLastName, "Honoree Last Name");
                driver.SendKeysToElement(txtEmailAddress, emailAddress, "Email Address");
                driver.ScrollPage();
                driver.SendKeysToElement(txtPersonalMessage, personalMessage, "Personal Message");
                driver.SendKeysToElement(txtECardFromName, fromName, "From Name");

            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("updateEcardHonoreeInformation", "Error occurred while updating Honoree Information", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Method to update Paper card memorial honoree information
        /// <summary>
        /// Method to update Paper card memorial honoree information
        /// </summary>
        /// <param name="honoreeFirstName"></param>
        /// <param name="honoreeLastName"></param>
        /// <param name="CardFirstName"></param>
        /// <param name="CardLastName"></param>
        /// <param name="CardStreetAdd1"></param>
        /// <param name="CardStreetAdd2"></param>
        /// <param name="CardPostalCode"></param>
        /// <param name="fromName"></param>
        public void updatePapercardMemorialHonoreeInformation(string honoreeFirstName, string honoreeLastName, string CardFirstName, string CardLastName, string CardStreetAdd1, string CardStreetAdd2, string CardPostalCode, string fromName)
        {
            try
            {
                Console.WriteLine("honoreeFirstName " + honoreeFirstName);
                driver.ScrollPage();
                driver.ClickElement(chkDonateInHonorOrMemory, "Donor in Honor or Memory");
                driver.ClickElement(rbtnCardTypePaper, " Paper Card");
                driver.ScrollPage();
                driver.ClickElement(rbtnCardDesignMemorial, "Card Design Memorial");
                driver.ScrollPage();
                driver.SendKeysToElement(txtPaperCardHonoreeFirstName, honoreeFirstName, "Honoree First Name");
                driver.SendKeysToElement(txtPaperCardHonoreeLastName, honoreeLastName, "Honoree Last Name");
                driver.ScrollPage();
                driver.SendKeysToElement(txtCardFirstName, CardFirstName, "Card FirstName");
                driver.SendKeysToElement(txtCardLastName, CardLastName, "Card LastName");
                driver.SendKeysToElement(txtCardStreetAddress1, CardStreetAdd1, "Card StreetAdd1");
                driver.SendKeysToElement(txtCardStreetAddress2, CardStreetAdd2, "Card StreetAdd2");
                driver.SendKeysToElement(txtCardPostalCode, CardPostalCode, "Card PostalCode");
                driver.ScrollPage();
                driver.SendKeysToElement(txtPaperCardFromName, fromName, "From Name");

            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("updatePapercardMemorialHonoreeInformation", "Error occurred while updating Honoree Information", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Method to update Paper card Tribute honoree information
        /// <summary>
        /// Method to update Paper card Tribute honoree information
        /// </summary>
        /// <param name="honoreeFirstName"></param>
        /// <param name="honoreeLastName"></param>
        /// <param name="CardFirstName"></param>
        /// <param name="CardLastName"></param>
        /// <param name="CardStreetAdd1"></param>
        /// <param name="CardStreetAdd2"></param>
        /// <param name="CardPostalCode"></param>
        /// <param name="fromName"></param>
        public void updatePapercardTributeHonoreeInformation(string honoreeFirstName, string honoreeLastName, string CardFirstName, string CardLastName, string CardStreetAdd1, string CardStreetAdd2, string CardPostalCode, string fromName)
        {
            try
            {
                driver.ScrollPage();
                driver.ClickElement(chkDonateInHonorOrMemory, "Donor in Honor or Memory");
                driver.ClickElement(rbtnCardTypePaper, " Paper Card");
                driver.ScrollPage();
                driver.ClickElement(rbtnCardDesignTributePostCard, "Card Design Tribute");
                driver.SendKeysToElement(txtPaperCardHonoreeFirstName, honoreeFirstName, "Honoree First Name");
                driver.SendKeysToElement(txtPaperCardHonoreeLastName, honoreeLastName, "Honoree Last Name");
                driver.ScrollPage();
                driver.SendKeysToElement(txtCardFirstName, CardFirstName, "Card FirstName");
                driver.SendKeysToElement(txtCardLastName, CardLastName, "Card LastName");
                driver.SendKeysToElement(txtCardStreetAddress1, CardStreetAdd1, "Card StreetAdd1");
                driver.SendKeysToElement(txtCardStreetAddress2, CardStreetAdd2, "Card StreetAdd2");
                driver.SendKeysToElement(txtCardPostalCode, CardPostalCode, "Card PostalCode");
                driver.ScrollPage();
                driver.SendKeysToElement(txtPaperCardFromName, fromName, "From Name");

            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("updatePapercardTributeHonoreeInformation", "Error occurred while updating Honoree Information", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Method to update Printable certificate tribute Honoree information
        /// <summary>
        /// Method to update Printable certificate tribute Honoree information
        /// </summary>
        /// <param name="honoreeFirstName"></param>
        /// <param name="honoreeLastName"></param>
        /// <param name="fromName"></param>
        public void updatePrintableCertTributeHonoreeInformation(string honoreeFirstName, string honoreeLastName, string fromName)
        {
            try
            {
                driver.ScrollPage();
                driver.ClickElement(chkDonateInHonorOrMemory, "Donor in Honor or Memory");
                driver.ClickElement(rbtnCardTypeCertificate, " Printable Certificate");
                driver.ScrollPage();
                driver.ClickElement(rbtnCardDesignTributeCrt, "Card Design - Tribute Certificate");
                driver.SendKeysToElement(txtPrintableCertHonoreeFirstName, honoreeFirstName, "Honoree First Name");
                driver.SendKeysToElement(txtPrintableCertHonoreeLastName, honoreeLastName, "Honoree Last Name");
                driver.ScrollPage();
                driver.SendKeysToElement(txtPrintableCertFromName, fromName, "From Name");

            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("updatePrintableCertTributeHonoreeInformation", "Error occurred while updating Honoree Information", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Method to validate Honoree information
        /// <summary>
        /// Method to validate Honoree information
        /// </summary>
        /// <param name="honoreeName"></param>
        /// <param name="honoreeFirstName"></param>
        /// <param name="honoreeLastName"></param>
        /// <param name="emailAddress"></param>
        /// <param name="fromName"></param>
        /// <param name="personalMessage"></param>
        public void validateHonoreeInformation(string honoreeName, string honoreeFirstName, string honoreeLastName, string emailAddress, string fromName, string personalMessage = "")
        {
            try
            {
                driver.ScrollPage();
                driver.VerifyCheckboxStatus(chkDonateInHonorOrMemory,"checked", "Donor in Honor or Memory");
                driver.VerifyInputRadioButtonStatus(rbtnCardDesignEcard,"checked", "Card Design E-Card");
                driver.ScrollPage();
                driver.VerifyTextValue(txtHonoreeName, honoreeName, "Honoree Name");
                driver.VerifyTextValue(txtECardHonoreeFirstName, honoreeFirstName, "Honoree First Name");
                driver.VerifyTextValue(txtECardHonoreeLastName, honoreeLastName, "Honoree Last Name");
                driver.VerifyTextValue(txtEmailAddress, emailAddress, "Email Address");
                driver.ScrollPage();
                driver.VerifyTextValue(txtPersonalMessage, personalMessage, "Personal Message");
                driver.VerifyTextValue(txtECardFromName, fromName, "From Name");

            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateHonoreeInformation", "Error occurred while validating Honoree Information", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Reusable method to validate other amount
        /// <summary>
        /// Reusable method to validate other amount
        /// </summary>
        /// <param name="amount"></param>
        public void validateOtherAmount(String amount)
        {
            try
            {
                selectDonationAmount(amount);
                driver.ClickElement(btnSaveAndContinue, "Save and Continue");
                int result = 0;
                if (int.TryParse(amount, out result) && result >= 5)
                {
                    driver.CheckElementDoesnotExists(lblOtherAmountError, "Donation amount error message");
                }
                else
                {
                    driver.VerifyTextValue(lblOtherAmountError, "Donation amount must be at least $5.", "Donation Error");
                }
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateOtherAmount", "Error occurred while validating Other amount", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Method to validate local chapter error
        /// <summary>
        /// Method to validate local chapter error
        /// </summary>
        public void validateLocalChapterError()
        {
            try
            {
                selectDesignation("LOCAL");
                driver.ClickElement(btnSaveAndContinue, "Save and Continue");
                driver.VerifyTextValue(lblLocalChapterError, "Please select a local chapter", "Local Chapter Error");
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("validateOtherAmount", "Error occurred while validating Other amount", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Method to click Donate Monthly option
        /// <summary>
        /// Method to click Donate Monthly option
        /// </summary>
        public void clickDonateMonthly()
        {
            try
            {
                driver.ScrollPage();
                driver.ClickElement(chkDonateMonthly, "Donate Monthly");
               
            }
            catch (Exception ex)
            {
                this.TESTREPORT.LogFailure("clickDonateMonthly", "Error occurred while click Donate Monthly option", EngineSetup.GetScreenShotPath());
            }
        }
        #endregion

        #region Validate Local donation after clearing form
        /// <summary>
        /// Method to validate local donation option after clearing form
        /// </summary>
        public void validateLocalDonationAfterClearForm()
        {
            driver.ClickElement(rbtn50, "$50");
            driver.ScrollPage();
            driver.ClickElement(rbtnLocal, "Local Donation");
            driver.ScrollPage();
            driver.ScrollPage();
            driver.ScrollPage();
            driver.ClickElement(btnSaveAndContinue, "Save And Continue");
            driver.CheckElementExists(lblLocalChapterError, "Local Donation Error Message");
            driver.SelectDropdownItemByText(listLocalChapters, "Make-A-Wish® Alabama", "Local Chapters List");
            driver.ClickElement(btnSaveAndContinue, "Save And Continue");
            driver.ScrollToPageBottom();
            driver.ClickElement(btnClearFormAndStartOver, "Clear Form And Start Over");
            driver.ScrollPage();
            driver.ScrollPage();
            driver.ScrollPage();
            driver.CheckElementExists(rbtnLocal, "Local Donation");

        }

        public void validateDesignationsNotDisplayed()
        {
            driver.CheckElementNotDisplayed(rbtnLocal, "Local Designation");
            driver.CheckElementNotDisplayed(rbtnNational, "National Designation");
            driver.CheckElementNotDisplayed(rbtnInternational, "International Designation");

        }
        #endregion
        #endregion
    }
}
