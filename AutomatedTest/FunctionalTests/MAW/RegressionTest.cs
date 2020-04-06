using System;
using RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AUT.Selenium.ApplicationSpecific.MAW.Pages;

namespace AutomatedTest.FunctionalTests.MAW
{
    [TestClass]
    public class RegressionTest: TestBaseTemplate
    {
        #region PageObject

        HomePage Home = new HomePage();
        DonatePage donate = new DonatePage();
        DonationReviewPage donationReview = new DonationReviewPage();
        ThankYouPage thankyou = new ThankYouPage();
        PayPalPage paypal = new PayPalPage();
        TransactionErrorPage errorpage = new TransactionErrorPage();
        #endregion

        [TestMethod]
        [TestCategory("Home Page UI")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TCVerifyHomePageSetup()
        {
            #region Initialization

            string isWhenYouDonate = string.Empty;

            bool isValid = false;

            string url = TestContext.DataRow["Url"].ToString();

            #endregion

            #region Test setup for Verify Donation page menu and common UI items.

          // this.TESTREPORT.LogInfo("Verify Donation page menu and common UI items.");
           
            Home.LunchPage(url);

            isValid =  Home.validateHomePageControls();

            if (isValid)
            {
                this.TESTREPORT.LogSuccess("Learn How Funds Are Used link verification.", "Clicking on Learn How Funds Are Used navigate to respective page.");
            }
            else
            {
                this.TESTREPORT.LogFailure("Learn How Funds Are Used link verification.", "Clicking on Learn How Funds Are Used do not navigate to respective page.");
            }

            Assert.IsTrue(isValid, "Donate By Email' Managing Our Funds section are not displayed.");
            this.TESTREPORT.UpdateTestCaseStatus();

            #endregion
        }


        [TestMethod]
        [TestCategory("Donation Page UI")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_002_Verify_Donation_Page_Setup()
        {

            #region TC_WEB_002 - Donation page default setup

            this.TESTREPORT.InitTestCase("TC_WEB_002 - Donation page default setup", "Verify Donation Page default set up");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateDonatePageElements();
            this.TESTREPORT.UpdateTestCaseStatus();
          
            #endregion

        }

        [TestMethod]
        [TestCategory("Donation Review Page UI")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_003_Verify_Donation_Review_Page_Setup()
        {

            #region TC_WEB_003 - Donation Review page default setup
            this.TESTREPORT.InitTestCase("TC_WEB_003 - Donation Review page default setup", "Verify Donation Review Page default set up");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.navigateToDonationReviewPage();
            donationReview.validateDonationReviewPageControls();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }


        [TestMethod]
        [TestCategory("Donate Page -Cards")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_004_Verify_CardTypes_Design()
        {

            #region TC_WEB_004 - Verify CardTypes and Card Design
            this.TESTREPORT.InitTestCase("TC_WEB_004 - Verify CardTypes and Card Design ", "Verify different card types and designs under 'Donate in Honor or Memory' section");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateCardTypesAndDesigns();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }
        
        [TestMethod]
        [TestCategory("Donate Page -Cards")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
         public void TC_WEB_005_Verify_Section_View_For_E_Card()
        {

            #region TC_WEB_005 - Verify Section View For E-Card
            this.TESTREPORT.InitTestCase("TC_WEB_005 - Verify Section View For E-Card", "Verify 'Donate in Honor or Memory' section view for E-Card");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateSectionViewForE_Card();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion
        }

        [TestMethod]
        [TestCategory("Donate Page -Cards")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_006_Verify_CardDesign_For_General_E_Card_Mahlia()
        {

            #region TC_WEB_006 - Verify Card Design For General E-Card (Mahlia)
            this.TESTREPORT.InitTestCase("TC_WEB_006 - Verify Card Design For General E-Card (Mahlia)", "Verify the Card Design details of General E-Card (Mahlia) for E-Card");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateCardDesignForGeneralE_Card();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion
        }

        [TestMethod]
        [TestCategory("Donate Page -Cards")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_007_Verify_Section_View_For_Paper_Card()
        {

            #region TC_WEB_007 - Verify Section View For Paper Card
            this.TESTREPORT.InitTestCase("TC_WEB_007 - Verify Section View For Paper Card", "Verify 'Donate in Honor or Memory' section view for Paper Card");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateSectionViewForPaperCard();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion
        }

        [TestMethod]
        [TestCategory("Donate Page -Cards")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_008_Verify_CardDesign_For_General_Memorial_Postcard()
        {

            #region TC_WEB_008 - Verify Card Design For Memorial Postcard
            this.TESTREPORT.InitTestCase("TC_WEB_008 - Verify Card Design For Memorial Postcard", "Verify the Card Design details of Memorial Postcard (Kionna) for Paper Card");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateCardDesignForMemorialPostcard();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion
        }

        [TestMethod]
        [TestCategory("Donate Page -Cards")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_009_Verify_CardDesign_For_General_Tribute_Postcard()
        {

            #region TC_WEB_009 - Verify Card Design For Tribute Postcard
            this.TESTREPORT.InitTestCase("TC_WEB_009 - Verify Card Design For Tribute Postcard", "Verify the Card Design details of Tribute Postcard (Gabe) for Paper Card");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateCardDesignForTributePostcard();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion
        }

        [TestMethod]
        [TestCategory("Donate Page -Cards")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_010_Verify_Section_View_For_Printable_Certificate()
        {

            #region TC_WEB_010 - Verify Section View For Printable Certificate
            this.TESTREPORT.InitTestCase("TC_WEB_010 - Verify Section View For Printable Certificate", "Verify 'Donate in Honor or Memory' section view for Printable Certificate");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateSectionViewForPrintableCertificate();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion
        }

        [TestMethod]
        [TestCategory("Donate Page -Cards")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_011_Verify_Tribute_Certificate_Card_Design()
        {

            #region TC_WEB_011 - Verify tribute certificate card design
            this.TESTREPORT.InitTestCase("TC_WEB_011 - Card Design details of Tribute Certificate", "Verify the Card Design details of Tribute Certificate (Mahlia) for Printable Certificate");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateSectionViewForTributeCertificate();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Monthly Donation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_012_Verify_Donate_Monthly_option()
        {

            #region TC_WEB_012 - Donate Monthly
            this.TESTREPORT.InitTestCase("TC_WEB_012 - Donate Monthly", "Verify 'Donate Monthly' option on the donation page");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateDonateMonthly(); 
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Data Collector Analytics")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_013_Verify_Satellite_Data_Collector_Values()
        {

            #region TC_WEB_013 - Satellite Data Collector values
            this.TESTREPORT.InitTestCase("TC_WEB_013 - Satellite Data Collector values", "Verify Satellite data collector values are populated correctly on the donation page");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateSatelliteDataCollectorValue();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Data Collector Analytics")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_014_Verify_Satellite_Footer_Script_Container_Values()
        {

            #region TC_WEB_014 - Satellite Footer Script Collector values
            this.TESTREPORT.InitTestCase("TC_WEB_014 - Satellite Footer Script Collector values", "Verify Satellite Footer Script Container values are populated correctly on the donation page");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateSatelliteFooterScriptContainerValues();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Image source JSON validation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_015_Verify_GeneralE_Card_Source()
        {

            #region TC_WEB_015 - General E-Card Source
            this.TESTREPORT.InitTestCase("TC_WEB_015 - General E-Card Source", "Verify that the [General E-Card] image source on 'Donate in Honor or Memory' section is from the products.json file");
            string url = TestContext.DataRow["Url"].ToString();
            string jsonFilePath = TestContext.DataRow["JsonFilePath"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateGeneralECardProductSource(jsonFilePath);
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Image source JSON validation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_016_Verify_PaperCard_Memorial_Source()
        {

            #region TC_WEB_016 - PaperCard Memorial Source
            this.TESTREPORT.InitTestCase("TC_WEB_016 - General Memorial PostCard Source", "Verify that the [Memorial PostCard] image source on 'Donate in Honor or Memory' section is from the products.json file");
            string url = TestContext.DataRow["Url"].ToString();
            string jsonFilePath = TestContext.DataRow["JsonFilePath"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateMemorialPostCardSource(jsonFilePath);
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Image source JSON validation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_017_Verify_PaperCard_Tribute_Source()
        {

            #region TC_WEB_017 - PaperCard Tribute Source
            this.TESTREPORT.InitTestCase("TC_WEB_017 - General Tribute PostCard Source", "Verify that the [Tribute PostCard] image source on 'Donate in Honor or Memory' section is from the products.json file");
            string url = TestContext.DataRow["Url"].ToString();
            string jsonFilePath = TestContext.DataRow["JsonFilePath"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateTributePostCardSource(jsonFilePath);
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Image source JSON validation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_018_Verify_PrintableCertificate_Memorial_Source()
        {

            #region TC_WEB_018 - Printable Certificate Memorial Source
            this.TESTREPORT.InitTestCase("TC_WEB_018 - Printable Certificate Memorial Source", "Verify that the [Printable Certificate] image source on 'Donate in Honor or Memory' section is from the products.json file");
            string url = TestContext.DataRow["Url"].ToString();
            string jsonFilePath = TestContext.DataRow["JsonFilePath"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateTributeCertPostCardSource(jsonFilePath);
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Designation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_019_Verify_Local_ZipCode_Resolves_ChapterName()
        {

            #region TC_WEB_019 - Verify Local Zip code resolves to chapter
            this.TESTREPORT.InitTestCase("TC_WEB_019 - Local Zip Code Resolves to Chapter Name", "Verify Local Zip code resolves to proper chapter on the donation page");
            string url = TestContext.DataRow["Url"].ToString();
            string zipCode = TestContext.DataRow["LocalZipCode"].ToString();
            string chapterName = TestContext.DataRow["LocalChapterName"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateNationalChapterUsingZipCode(zipCode,chapterName);
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Designation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_020_Verify_Local_ChapterName_Resolves_ChapterName()
        {

            #region TC_WEB_020 - Verify Local Chapter Name code resolves to appropriate chapter
            this.TESTREPORT.InitTestCase("TC_WEB_020 - Local Chapter Name Resolves to Chapter Name", "Verify Local chapter name resolves to proper chapter on the donation page");
            string url = TestContext.DataRow["Url"].ToString();
            string localAreaName = TestContext.DataRow["LocalAreaName"].ToString();
            string chapterName = TestContext.DataRow["LocalChapterName"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateNationalChapterUsingAreaName(localAreaName,chapterName);
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion
        }

        [TestMethod]
        [TestCategory("Designation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_021_Verify_CountryName_Resolves_International_ChapterName()
        {

            #region TC_WEB_021 - Verify Local Chapter Name code resolves to appropriate chapter
            this.TESTREPORT.InitTestCase("TC_WEB_021 - Country Name Resolves to International Chapter Name", "Verify that for International donation, the Country Name resolves to proper chapter");
            string url = TestContext.DataRow["Url"].ToString();
            string countryName = TestContext.DataRow["CountryName"].ToString();
            string intlChapterName = TestContext.DataRow["InternationalChapterName"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateInternationalChapterUsingCountryName(countryName, intlChapterName);
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion
        }

        [TestMethod]
        [TestCategory("Donation Amount")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_022_Verify_Single_Donation_section_50()
        {

            #region TC_WEB_022 - Single Donation section for $50
            this.TESTREPORT.InitTestCase("TC_WEB_022 - Single Donation section $50", "Verify the amount $50  appears in the Single Donation section ");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.selectDonationAmount("$50");
            donate.navigateToDonationReviewPage();
            donationReview.validateDonationAmount("$50");
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Donation Amount")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_023_Verify_Single_Donation_section_100()
        {

            #region TC_WEB_023 - Single Donation section for $100
            this.TESTREPORT.InitTestCase("TC_WEB_023 - Single Donation section $100", "Verify the amount $100  appears in the Single Donation section ");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.selectDonationAmount("$100");
            donate.navigateToDonationReviewPage();
            donationReview.validateDonationAmount("$100");
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Donation Amount")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_024_Verify_Single_Donation_section_500()
        {

            #region TC_WEB_024 - Single Donation section for $500
            this.TESTREPORT.InitTestCase("TC_WEB_024 - Single Donation section $500", "Verify the amount $500  appears in the Single Donation section ");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.selectDonationAmount("$500");
            donate.navigateToDonationReviewPage();
            donationReview.validateDonationAmount("$500");
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Donation Amount")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_025_Verify_Single_Donation_section_Other()
        {

            #region TC_WEB_025 - Single Donation section for Other amount
            this.TESTREPORT.InitTestCase("TC_WEB_025 - Single Donation section - Other", "Verify the amount in Other Option selected appears in the Single Donation values ");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.selectDonationAmount("$1000");
            donate.navigateToDonationReviewPage();
            donationReview.validateDonationAmount("$1,000");
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Designation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_026_Verify_default_benefitting_designation()
        {

            #region TC_WEB_026 - Default benefitting designation
            this.TESTREPORT.InitTestCase("TC_WEB_026 - Default donation designation - National", "Verify that the donation defaults to Make-A-Wish® America for NATIONAL designation");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.navigateToDonationReviewPage();
            donationReview.validateBenefittingChapter("Make-A-Wish® America");
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Designation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_027_Verify_LocalChapter_benefitting_designation()
        {

            #region TC_WEB_027 - Local chapter benefitting designation
            this.TESTREPORT.InitTestCase("TC_WEB_027 - LocalChapter donation benefitting designation ", "Verify that the donation defaults to Make-A-Wish® ChapterName for Local Chapter designation");
            string url = TestContext.DataRow["Url"].ToString();
            string localChapterName = TestContext.DataRow["LocalChapterName"].ToString();            
            Home.navigateToDonationPage(url);
            donate.selectLocalDesignation(localChapterName);
            donationReview.validateBenefittingChapter(localChapterName);
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Designation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_028_Verify_InternationalChapter_benefitting_designation()
        {

            #region TC_WEB_028 - International chapter benefitting designation
            this.TESTREPORT.InitTestCase("TC_WEB_028 - InternationalChapter donation benefitting designation", "Verify that the donation defaults to Make-A-Wish® ChapterName for International designation");
            string url = TestContext.DataRow["Url"].ToString();
            string internationalChapterName = TestContext.DataRow["InternationalChapterName"].ToString();
            Home.navigateToDonationPage(url);
            donate.selectInternationalDesignation(internationalChapterName);
            donationReview.validateBenefittingChapter(internationalChapterName);
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Review Information")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_029_Verify_Review_Edit_Donation()
        {

            #region TC_WEB_029 - Review/Edit Donation
            this.TESTREPORT.InitTestCase("TC_WEB_029 - REVIEW/EDIT Donation", "Verify that user is able to edit the donation information using the 'REVIEW/EDIT' button");
            #region Test Data
            string url = TestContext.DataRow["Url"].ToString();
            string honoreeName = TestContext.DataRow["HonoreeName"].ToString();
            string honoreeFirstName = TestContext.DataRow["HonoreeFirstName"].ToString();
            string honoreeLastName = TestContext.DataRow["HonoreeLastName"].ToString();
            string emailAddress = TestContext.DataRow["EmailAddress"].ToString();
            string fromName = TestContext.DataRow["FromName"].ToString();
            string personalMessage = TestContext.DataRow["PersonalMessage"].ToString();
            #endregion
            Home.navigateToDonationPage(url);
            donate.selectDonationAmount("$50");
            donate.updateEcardHonoreeInformation(honoreeName, honoreeFirstName, honoreeLastName, emailAddress, fromName, personalMessage);
            donate.navigateToDonationReviewPage();
            donationReview.reviewAndEditDonationInformation();
            donate.validateHonoreeInformation(honoreeName, honoreeFirstName, honoreeLastName, emailAddress, fromName, personalMessage);
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("End-To-End Donation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_030_Verify_Donation_With_Valid_CreditCard()
        {

            #region TC_WEB_030 - Donation with valid Credit Card information
            this.TESTREPORT.InitTestCase("TC_WEB_030 - Donation with valid Credit Card information", "Verify a donation with valid credit card details");
            #region Test Data
            string url = TestContext.DataRow["Url"].ToString();
            string yourFirstName = TestContext.DataRow["YourFirstName"].ToString();
            string yourLastName = TestContext.DataRow["YourLastName"].ToString();
            string emailAddress = TestContext.DataRow["YourEmailAddress"].ToString();
            string yourStreetAddress1 = TestContext.DataRow["YourStreetAddress1"].ToString();
            string yourStreetAddress2 = TestContext.DataRow["YourStreetAddress2"].ToString();
            string yourPostalCode = TestContext.DataRow["YourPostalCode"].ToString();
            string creditCardNumber = TestContext.DataRow["CreditCardNumber"].ToString();
            string expirationMonth = TestContext.DataRow["ExpirationMonth"].ToString();
            string expirationYear = TestContext.DataRow["ExpirationYear"].ToString();
            string cvv = TestContext.DataRow["CVV"].ToString();

            #endregion
            Home.navigateToDonationPage(url);
            donate.selectDonationAmount("$5");
            donate.navigateToDonationReviewPage();
            donationReview.updateYourInformation(yourFirstName, yourLastName, emailAddress, yourStreetAddress1, yourPostalCode, yourStreetAddress2);
            donationReview.updateBillingInformation("CREDIT CARD", creditCardNumber, expirationMonth, expirationYear, cvv);
            donationReview.submitDonation(); //uncomment for actual donation
            thankyou.validateThankYouPage("$5");
            thankyou.validateUserTransactionAndRefund();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("End-To-End Donation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_031_Verify_Donation_With_ECard_And_design_General_ECard()
        {

            #region TC_WEB_031 - Donation with ECard Type and card design General ECard
            this.TESTREPORT.InitTestCase("TC_WEB_031 - Donation with ECard Type and card design General ECard", "Verify a donation with ECard Type and card design General ECard");
            #region Test Data
            string url = TestContext.DataRow["Url"].ToString();
            string honoreeName = TestContext.DataRow["HonoreeName"].ToString();
            string honoreeFirstName = TestContext.DataRow["HonoreeFirstName"].ToString();
            string honoreeLastName = TestContext.DataRow["HonoreeLastName"].ToString();
            string cardDesignEmailAddress = TestContext.DataRow["EmailAddress"].ToString();
            string fromName = TestContext.DataRow["FromName"].ToString();
            string personalMessage = TestContext.DataRow["PersonalMessage"].ToString();
            string yourFirstName = TestContext.DataRow["YourFirstName"].ToString();
            string yourLastName = TestContext.DataRow["YourLastName"].ToString();
            string emailAddress = TestContext.DataRow["YourEmailAddress"].ToString();
            string yourStreetAddress1 = TestContext.DataRow["YourStreetAddress1"].ToString();
            string yourStreetAddress2 = TestContext.DataRow["YourStreetAddress2"].ToString();
            string yourPostalCode = TestContext.DataRow["YourPostalCode"].ToString();
            string creditCardNumber = TestContext.DataRow["CreditCardNumber"].ToString();
            string expirationMonth = TestContext.DataRow["ExpirationMonth"].ToString();
            string expirationYear = TestContext.DataRow["ExpirationYear"].ToString();
            string cvv = TestContext.DataRow["CVV"].ToString();

            #endregion
            Home.navigateToDonationPage(url);
            donate.selectDonationAmount("$5");
            donate.updateEcardHonoreeInformation(honoreeName, honoreeFirstName, honoreeLastName, cardDesignEmailAddress, fromName, personalMessage);
            donate.navigateToDonationReviewPage();
            donationReview.updateYourInformation(yourFirstName, yourLastName, emailAddress, yourStreetAddress1, yourPostalCode, yourStreetAddress2);
            donationReview.updateBillingInformation("CREDIT CARD", creditCardNumber, expirationMonth, expirationYear, cvv);
            donationReview.submitDonation(); //uncomment for actual donation
            thankyou.validateThankYouPage("$5");
            thankyou.validateUserTransactionAndRefund();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("End-To-End Donation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_032_Verify_Donation_With_PaperCard_And_design_MemorialPostCard()
        {

            #region TC_WEB_032 - Donation with PaperCard And design MemorialPostCard
            this.TESTREPORT.InitTestCase("TC_WEB_032 - Donation with PaperCard And design MemorialPostCard", "Verify a donation with PaperCard And design MemorialPostCard");
            #region Test Data
            string url = TestContext.DataRow["Url"].ToString();
            string honoreeFirstName = TestContext.DataRow["HonoreeFirstName"].ToString();
            string honoreeLastName = TestContext.DataRow["HonoreeLastName"].ToString();
            string cardFirstName = TestContext.DataRow["CardFirstName"].ToString();
            string cardLastName = TestContext.DataRow["CardLastName"].ToString();
            string cardStreetAdd1 = TestContext.DataRow["CardStreetAdd1"].ToString();
            string cardStreetAdd2 = TestContext.DataRow["CardStreetAdd2"].ToString();
            string cardPostalCode = TestContext.DataRow["CardPostalCode"].ToString();
            string fromName = TestContext.DataRow["FromName"].ToString();
            string yourFirstName = TestContext.DataRow["YourFirstName"].ToString();
            string yourLastName = TestContext.DataRow["YourLastName"].ToString();
            string emailAddress = TestContext.DataRow["YourEmailAddress"].ToString();
            string yourStreetAddress1 = TestContext.DataRow["YourStreetAddress1"].ToString();
            string yourStreetAddress2 = TestContext.DataRow["YourStreetAddress2"].ToString();
            string yourPostalCode = TestContext.DataRow["YourPostalCode"].ToString();
            string creditCardNumber = TestContext.DataRow["CreditCardNumber"].ToString();
            string expirationMonth = TestContext.DataRow["ExpirationMonth"].ToString();
            string expirationYear = TestContext.DataRow["ExpirationYear"].ToString();
            string cvv = TestContext.DataRow["CVV"].ToString();

            #endregion
            Home.navigateToDonationPage(url);
            donate.selectDonationAmount("$5");
            donate.updatePapercardMemorialHonoreeInformation(honoreeFirstName, honoreeLastName, cardFirstName, cardLastName, cardStreetAdd1, cardStreetAdd2, cardPostalCode, fromName);
            donate.navigateToDonationReviewPage();
            donationReview.updateYourInformation(yourFirstName, yourLastName, emailAddress, yourStreetAddress1, yourPostalCode, yourStreetAddress2);
            donationReview.updateBillingInformation("CREDIT CARD", creditCardNumber, expirationMonth, expirationYear, cvv);
            donationReview.submitDonation(); //uncomment for actual donation
            thankyou.validateThankYouPage("$5");
            thankyou.validateUserTransactionAndRefund();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("End-To-End Donation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_033_Verify_Donation_With_PaperCard_And_design_TributePostCard()
        {

            #region TC_WEB_033 - Donation with PaperCard And design TributePostCard
            this.TESTREPORT.InitTestCase("TC_WEB_033 - Donation with PaperCard And design TributePostCard", "Verify a donation with PaperCard And design TributePostCard");
            #region Test Data
            string url = TestContext.DataRow["Url"].ToString();
            string honoreeFirstName = TestContext.DataRow["HonoreeFirstName"].ToString();
            string honoreeLastName = TestContext.DataRow["HonoreeLastName"].ToString();
            string cardFirstName = TestContext.DataRow["CardFirstName"].ToString();
            string cardLastName = TestContext.DataRow["CardLastName"].ToString();
            string cardStreetAdd1 = TestContext.DataRow["CardStreetAdd1"].ToString();
            string cardStreetAdd2 = TestContext.DataRow["CardStreetAdd2"].ToString();
            string cardPostalCode = TestContext.DataRow["CardPostalCode"].ToString();
            string fromName = TestContext.DataRow["FromName"].ToString();
            string yourFirstName = TestContext.DataRow["YourFirstName"].ToString();
            string yourLastName = TestContext.DataRow["YourLastName"].ToString();
            string emailAddress = TestContext.DataRow["YourEmailAddress"].ToString();
            string yourStreetAddress1 = TestContext.DataRow["YourStreetAddress1"].ToString();
            string yourStreetAddress2 = TestContext.DataRow["YourStreetAddress2"].ToString();
            string yourPostalCode = TestContext.DataRow["YourPostalCode"].ToString();
            string creditCardNumber = TestContext.DataRow["CreditCardNumber"].ToString();
            string expirationMonth = TestContext.DataRow["ExpirationMonth"].ToString();
            string expirationYear = TestContext.DataRow["ExpirationYear"].ToString();
            string cvv = TestContext.DataRow["CVV"].ToString();

            #endregion
            Home.navigateToDonationPage(url);
            donate.selectDonationAmount("$5");
            donate.updatePapercardTributeHonoreeInformation(honoreeFirstName, honoreeLastName, cardFirstName, cardLastName, cardStreetAdd1, cardStreetAdd2, cardPostalCode, fromName);
            donate.navigateToDonationReviewPage();
            donationReview.updateYourInformation(yourFirstName, yourLastName, emailAddress, yourStreetAddress1, yourPostalCode, yourStreetAddress2);
            donationReview.updateBillingInformation("CREDIT CARD", creditCardNumber, expirationMonth, expirationYear, cvv);
            donationReview.submitDonation(); //uncomment for actual donation
            thankyou.validateThankYouPage("$5");
            thankyou.validateUserTransactionAndRefund();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("End-To-End Donation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_034_Verify_Donation_With_PrintableCert_And_design_TributeCert()
        {

            #region TC_WEB_034 - Donation with PrintableCertificate and design Tribute Certificate
            this.TESTREPORT.InitTestCase("TC_WEB_034 - Donation with PrintableCertificate and design Tribute Certificate", "Verify a donation with PrintableCertificate and design Tribute Certificate");
            #region Test Data
            string url = TestContext.DataRow["Url"].ToString();
            string honoreeFirstName = TestContext.DataRow["HonoreeFirstName"].ToString();
            string honoreeLastName = TestContext.DataRow["HonoreeLastName"].ToString();
            string fromName = TestContext.DataRow["FromName"].ToString();
            string yourFirstName = TestContext.DataRow["YourFirstName"].ToString();
            string yourLastName = TestContext.DataRow["YourLastName"].ToString();
            string emailAddress = TestContext.DataRow["YourEmailAddress"].ToString();
            string yourStreetAddress1 = TestContext.DataRow["YourStreetAddress1"].ToString();
            string yourStreetAddress2 = TestContext.DataRow["YourStreetAddress2"].ToString();
            string yourPostalCode = TestContext.DataRow["YourPostalCode"].ToString();
            string creditCardNumber = TestContext.DataRow["CreditCardNumber"].ToString();
            string expirationMonth = TestContext.DataRow["ExpirationMonth"].ToString();
            string expirationYear = TestContext.DataRow["ExpirationYear"].ToString();
            string cvv = TestContext.DataRow["CVV"].ToString();

            #endregion
            Home.navigateToDonationPage(url);
            donate.selectDonationAmount("$5");
            donate.updatePrintableCertTributeHonoreeInformation(honoreeFirstName, honoreeLastName, fromName);
            donate.navigateToDonationReviewPage();
            donationReview.updateYourInformation(yourFirstName, yourLastName, emailAddress, yourStreetAddress1, yourPostalCode, yourStreetAddress2);
            donationReview.updateBillingInformation("CREDIT CARD", creditCardNumber, expirationMonth, expirationYear, cvv);
            donationReview.submitDonation(); //uncomment for actual donation
            thankyou.validateThankYouPage("$5");
            thankyou.validateUserTransactionAndRefund();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }
        [TestMethod]
        [TestCategory("Donation Amount")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_035_Verify_Error_Message_Amount_LessThan_5()
        {

            #region TC_WEB_035 - Error message when amount is less than $5
            this.TESTREPORT.InitTestCase("TC_WEB_035 - Error Message when Amount < $5", "Verify that an error message is displayed if user enters less than $5 donation");
            string url = TestContext.DataRow["Url"].ToString();       
            Home.navigateToDonationPage(url);
            donate.validateOtherAmount("$4");
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Donation Amount")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_036_Verify_Error_Message_Amount_Negative_5()
        {

            #region TC_WEB_036 - Error message when amount is -$5
            this.TESTREPORT.InitTestCase("TC_WEB_036 - Error Message when Amount is -$5", "Verify that an error message is displayed if user enters negative numbers in OTHER amount text field (-$5) ");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateOtherAmount("-5");
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Donation Amount")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_037_Verify_Error_Message_Amount_SpecialChars()
        {

            #region TC_WEB_037 - Error message when amount has special chars
            this.TESTREPORT.InitTestCase("TC_WEB_037 - Error Message when Amount has Special Chars", "Verify that an error message is displayed if user enters special characters in OTHER amount text field ");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateOtherAmount("**");
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Donation Amount")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_038_Verify_Error_Message_When_Amount_Equals_5()
        {

            #region TC_WEB_038 - Error message when amount is equal to $5
            this.TESTREPORT.InitTestCase("TC_WEB_038 - Error Message when Amount = $5", "Verify that error message is not displayed if user enters $5 donation ");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateOtherAmount("5");
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Donation Amount")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_039_Verify_Error_Message_When_Amount_MoreThan_5()
        {

            #region TC_WEB_039 - Error message when amount is more than $5
            this.TESTREPORT.InitTestCase("TC_WEB_039 - Error Message when Amount > $5", "Verify that the OTHER amount text field accepts amount more than $5 ");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateOtherAmount("1000");
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Designation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_040_Verify_Local_Chapter_Error_Message()
        {

            #region TC_WEB_040 - Error message displayed when Local chapter is not selected
            this.TESTREPORT.InitTestCase("TC_WEB_040 - Error Message when Local Chapter is not selected", "Verify that an error is displayed when donating to a LOCAL chapter without selecting a chapter name");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.validateLocalChapterError();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }


        [TestMethod]
        [TestCategory("Designation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_041_Verify_Default_Designation_International()
        {

            #region TC_WEB_041 - Default benefitting designation - international
            this.TESTREPORT.InitTestCase("TC_WEB_041 - Default donation designation - International", "Verify that the donation defaults to Make-A-Wish® America for NATIONAL designation");
            string url = TestContext.DataRow["Url"].ToString();
            Home.navigateToDonationPage(url);
            donate.selectDesignation("International");
            donate.navigateToDonationReviewPage();
            donationReview.validateBenefittingChapter("Make-A-Wish® International");
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Billing Information")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_042_Verify_Donation_With_Invalid_CreditCard()
        {

            #region TC_WEB_042 - Donation with Invalid Credit Card information
            this.TESTREPORT.InitTestCase("TC_WEB_042 - Donation with invalid Credit Card information", "Verify a donation with invalid credit card details");
            #region Test Data
            string url = TestContext.DataRow["Url"].ToString();
            string yourFirstName = TestContext.DataRow["YourFirstName"].ToString();
            string yourLastName = TestContext.DataRow["YourLastName"].ToString();
            string emailAddress = TestContext.DataRow["YourEmailAddress"].ToString();
            string yourStreetAddress1 = TestContext.DataRow["YourStreetAddress1"].ToString();
            string yourStreetAddress2 = TestContext.DataRow["YourStreetAddress2"].ToString();
            string yourPostalCode = TestContext.DataRow["YourPostalCode"].ToString();
            string inValidCreditCardNumber = TestContext.DataRow["InvalidCreditCardNumber"].ToString();
            string expirationMonth = TestContext.DataRow["ExpirationMonth"].ToString();
            string expirationYear = TestContext.DataRow["ExpirationYear"].ToString();
            string inValidCvv = TestContext.DataRow["InValidCVV"].ToString();

            #endregion
            Home.navigateToDonationPage(url);
            donate.navigateToDonationReviewPage();
            donationReview.updateYourInformation(yourFirstName, yourLastName, emailAddress, yourStreetAddress1, yourPostalCode, yourStreetAddress2);
            donationReview.updateBillingInformation("CREDIT CARD", inValidCreditCardNumber, expirationMonth, expirationYear, inValidCvv);
            donationReview.submitDonation(); 
            donationReview.verifyErrorCreditCardNumber();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Billing Information")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_043_Verify_PaypalErrorMsg_With_CancelledDonation()
        {

            #region TC_WEB_043 - Paypal error when transaction is cancelled
            this.TESTREPORT.InitTestCase("TC_WEB_043 - Paypal Error when Donation Cancelled", "Verify that the PayPal error screen is displayed when donation through Paypal account is cancelled");
            #region Test Data
            string url = TestContext.DataRow["Url"].ToString();
            string yourFirstName = TestContext.DataRow["YourFirstName"].ToString();
            string yourLastName = TestContext.DataRow["YourLastName"].ToString();
            string emailAddress = TestContext.DataRow["YourEmailAddress"].ToString();
            string yourStreetAddress1 = TestContext.DataRow["YourStreetAddress1"].ToString();
            string yourStreetAddress2 = TestContext.DataRow["YourStreetAddress2"].ToString();
            string yourPostalCode = TestContext.DataRow["YourPostalCode"].ToString();
        

            #endregion
            Home.navigateToDonationPage(url);
            donate.navigateToDonationReviewPage();
            donationReview.updateYourInformation(yourFirstName, yourLastName, emailAddress, yourStreetAddress1, yourPostalCode, yourStreetAddress2);
            donationReview.updateBillingInformation("PAYPAL");
            donationReview.submitDonation();
            //paypal.verifyPayPalScreen("Make-A-Wish Foundation of America", "$100.00 USD");
            paypal.verifyPayPalScreen();
            paypal.cancelPaypalTransaction();
            errorpage.verifyPayPalErrorScreen();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }


        [TestMethod]
        [TestCategory("Sender Information")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_044_Verify_Donation_With_Blank_InformationFields()
        {

            #region TC_WEB_044 - Donation with blank information fields
            this.TESTREPORT.InitTestCase("TC_WEB_044 - Donation with Blank Information Fields", "Verify a donation by leaving the YOUR INFORMATION fields blank");
            #region Test Data
            string url = TestContext.DataRow["Url"].ToString();
            string creditCardNumber = TestContext.DataRow["CreditCardNumber"].ToString();
            string expirationMonth = TestContext.DataRow["ExpirationMonth"].ToString();
            string expirationYear = TestContext.DataRow["ExpirationYear"].ToString();
            string cvv = TestContext.DataRow["CVV"].ToString();

            #endregion
            Home.navigateToDonationPage(url);
            donate.navigateToDonationReviewPage();
            donationReview.updateBillingInformation("CREDIT CARD", creditCardNumber, expirationMonth, expirationYear, cvv);
            donationReview.submitDonation();
            donationReview.verifyYourInformationFieldErrors();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Billing Information")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_045_Verify_Donation_With_Blank_BillingFields()
        {

            #region TC_WEB_045 - Donation with blank Credit Card information
            this.TESTREPORT.InitTestCase("TC_WEB_045 - Donation with Blank Billing Fields", "Verify a donation by leaving the BILLING INFORMATION fields blank");
            #region Test Data
            string url = TestContext.DataRow["Url"].ToString();
            string yourFirstName = TestContext.DataRow["YourFirstName"].ToString();
            string yourLastName = TestContext.DataRow["YourLastName"].ToString();
            string emailAddress = TestContext.DataRow["YourEmailAddress"].ToString();
            string yourStreetAddress1 = TestContext.DataRow["YourStreetAddress1"].ToString();
            string yourStreetAddress2 = TestContext.DataRow["YourStreetAddress2"].ToString();
            string yourPostalCode = TestContext.DataRow["YourPostalCode"].ToString();
            string creditCardNumber = TestContext.DataRow["CreditCardNumber"].ToString();
            string expirationMonth = TestContext.DataRow["ExpirationMonth"].ToString();
            string expirationYear = TestContext.DataRow["ExpirationYear"].ToString();
            string cvv = TestContext.DataRow["CVV"].ToString();

            #endregion
            Home.navigateToDonationPage(url);
            donate.navigateToDonationReviewPage();
            donationReview.updateYourInformation(yourFirstName, yourLastName, emailAddress, yourStreetAddress1, yourPostalCode, yourStreetAddress2);
         
            donationReview.submitDonation(); 
            donationReview.VerifyBillingSectionFieldsErrors();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Clear & Start Over")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_046_Verify_ClearFormAndStartOverbutton_functionality()
        {

            #region TC_WEB_046 - Verify Clear form and start over button functionality
            this.TESTREPORT.InitTestCase("TC_WEB_046 - Clear Form And Start Over functionality", "Verify that all the fields in donation page are cleared when clicked on Clear form and start over button");
            #region Test Data
            string url = TestContext.DataRow["Url"].ToString();
            string honoreeName = TestContext.DataRow["HonoreeName"].ToString();
            string honoreeFirstName = TestContext.DataRow["HonoreeFirstName"].ToString();
            string honoreeLastName = TestContext.DataRow["HonoreeLastName"].ToString();
            string cardDesignEmailAddress = TestContext.DataRow["EmailAddress"].ToString();
            string fromName = TestContext.DataRow["FromName"].ToString();
            string personalMessage = TestContext.DataRow["PersonalMessage"].ToString();
            string yourFirstName = TestContext.DataRow["YourFirstName"].ToString();
            string yourLastName = TestContext.DataRow["YourLastName"].ToString();
            string emailAddress = TestContext.DataRow["YourEmailAddress"].ToString();
            string yourStreetAddress1 = TestContext.DataRow["YourStreetAddress1"].ToString();
            string yourStreetAddress2 = TestContext.DataRow["YourStreetAddress2"].ToString();
            string yourPostalCode = TestContext.DataRow["YourPostalCode"].ToString();
            string creditCardNumber = TestContext.DataRow["CreditCardNumber"].ToString();
            string expirationMonth = TestContext.DataRow["ExpirationMonth"].ToString();
            string expirationYear = TestContext.DataRow["ExpirationYear"].ToString();
            string cvv = TestContext.DataRow["CVV"].ToString();

            #endregion
            Home.navigateToDonationPage(url);
            donate.updateEcardHonoreeInformation(honoreeName, honoreeFirstName, honoreeLastName, cardDesignEmailAddress, fromName, personalMessage);
            donate.navigateToDonationReviewPage();
            donationReview.updateYourInformation(yourFirstName, yourLastName, emailAddress, yourStreetAddress1, yourPostalCode, yourStreetAddress2);
            donationReview.ClickClearformandstartoverbutton();
            donate.validateHonoreeInformation(honoreeName, honoreeFirstName, honoreeLastName, cardDesignEmailAddress, fromName, personalMessage);
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("End-To-End Donation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_047_Verify_Donation_Logging_in_LO()
        {

            #region TC_WEB_047 - Verify Donation Logging in LO
            this.TESTREPORT.InitTestCase("TC_WEB_047 - Transactions logged in LO", "Verify transactions are logged in LO after successful donation");
            #region Test Data
            string url = TestContext.DataRow["Url"].ToString();
            string yourFirstName = TestContext.DataRow["YourFirstName"].ToString();
            string yourLastName = TestContext.DataRow["YourLastName"].ToString();
            string emailAddress = TestContext.DataRow["YourEmailAddress"].ToString();
            string yourStreetAddress1 = TestContext.DataRow["YourStreetAddress1"].ToString();
            string yourStreetAddress2 = TestContext.DataRow["YourStreetAddress2"].ToString();
            string yourPostalCode = TestContext.DataRow["YourPostalCode"].ToString();
            string creditCardNumber = TestContext.DataRow["CreditCardNumber"].ToString();
            string expirationMonth = TestContext.DataRow["ExpirationMonth"].ToString();
            string expirationYear = TestContext.DataRow["ExpirationYear"].ToString();
            string cvv = TestContext.DataRow["CVV"].ToString();

            #endregion
            Home.navigateToDonationPage(url);
            donate.selectDonationAmount("$5");
            donate.navigateToDonationReviewPage();
            donationReview.updateYourInformation(yourFirstName, yourLastName, emailAddress, yourStreetAddress1, yourPostalCode, yourStreetAddress2);
            donationReview.updateBillingInformation("CREDIT CARD", creditCardNumber, expirationMonth, expirationYear, cvv);
            donationReview.submitDonation(); //uncomment for actual donation
            thankyou.validateThankYouPage("$5");
            thankyou.validateUserTransactionAndRefund();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("End-To-End Donation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_048_Verify_DonationMonthly_Logging_in_LO()
        {

            #region TC_WEB_048 - Verify Monthly Donation Logging in LO
            this.TESTREPORT.InitTestCase("TC_WEB_048 - Transactions for Donate Monthly in LO", "Verify that the donation frequency is marked as monthly in LO if user selects opts for monthly donation");
            #region Test Data
            string url = TestContext.DataRow["Url"].ToString();
            string yourFirstName = TestContext.DataRow["YourFirstName"].ToString();
            string yourLastName = TestContext.DataRow["YourLastName"].ToString();
            string emailAddress = TestContext.DataRow["YourEmailAddress"].ToString();
            string yourStreetAddress1 = TestContext.DataRow["YourStreetAddress1"].ToString();
            string yourStreetAddress2 = TestContext.DataRow["YourStreetAddress2"].ToString();
            string yourPostalCode = TestContext.DataRow["YourPostalCode"].ToString();
            string creditCardNumber = TestContext.DataRow["CreditCardNumber"].ToString();
            string expirationMonth = TestContext.DataRow["ExpirationMonth"].ToString();
            string expirationYear = TestContext.DataRow["ExpirationYear"].ToString();
            string cvv = TestContext.DataRow["CVV"].ToString();

            #endregion
            Home.navigateToDonationPage(url);
            donate.selectDonationAmount("$5");
            donate.clickDonateMonthly();
            donate.navigateToDonationReviewPage();
            donationReview.updateYourInformation(yourFirstName, yourLastName, emailAddress, yourStreetAddress1, yourPostalCode, yourStreetAddress2);
            donationReview.updateBillingInformation("CREDIT CARD", creditCardNumber, expirationMonth, expirationYear, cvv);
            donationReview.submitDonation(); //uncomment for actual donation
            thankyou.validateThankYouPage("$5");
            thankyou.validateUserTransactionAndRefund("RECURRING_GIFT");
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Chapterwise Donation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_049_Verify_Chapter_Specific_Home_Page()
        {

            #region TC_WEB_049 - Verify Chapter specific Make-A-Wish home page
            this.TESTREPORT.InitTestCase("TC_WEB_049 - Chapter Specific Home Page", "Verify home page setup for a chapter specific Make-A-Wish home page(http://[chapter].wish.org/)");
            #region Test Data
            string url = TestContext.DataRow["ChapterUrl"].ToString();
            string chapterName = TestContext.DataRow["ChapterName"].ToString();

            #endregion
            Home.NavigateToHome("http://illinois.wish.org/");
            Home.validateHomePageControls(chapterName);
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Chapterwise Donation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_050_Verify_Chapter_Specific_Donation_Page()
        {

            #region TC_WEB_050 - Verify Chapter specific Make-A-Wish donation page
            this.TESTREPORT.InitTestCase("TC_WEB_050 - Chapter Specific Donation Page", "Verify Donation page setup for a chapter specific Make-A-Wish home page(http://[chapter].wish.org/)");
            #region Test Data
            string url = TestContext.DataRow["ChapterUrl"].ToString();

            #endregion
            Home.navigateToDonationPage(url);
            donate.validateDonatePageElements("local");
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Chapterwise Donation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_051_Verify_International_Chapter_Donation()
        {

            #region TC_WEB_051 - Verify International chapter donation
            this.TESTREPORT.InitTestCase("TC_WEB_051 - International Chapter Donation", "Verify the successful donation to International chapter from Make-A-Wish National donation page");
            #region Test Data
            string url = TestContext.DataRow["Url"].ToString();
            string internationalChapterName = TestContext.DataRow["InternationalChapterName"].ToString();
            string yourFirstName = TestContext.DataRow["YourFirstName"].ToString();
            string yourLastName = TestContext.DataRow["YourLastName"].ToString();
            string emailAddress = TestContext.DataRow["YourEmailAddress"].ToString();
            string yourStreetAddress1 = TestContext.DataRow["YourStreetAddress1"].ToString();
            string yourStreetAddress2 = TestContext.DataRow["YourStreetAddress2"].ToString();
            string yourPostalCode = TestContext.DataRow["YourPostalCode"].ToString();
            string creditCardNumber = TestContext.DataRow["CreditCardNumber"].ToString();
            string expirationMonth = TestContext.DataRow["ExpirationMonth"].ToString();
            string expirationYear = TestContext.DataRow["ExpirationYear"].ToString();
            string cvv = TestContext.DataRow["CVV"].ToString();
            #endregion
            Home.navigateToDonationPage(url);
            donate.selectDonationAmount("$5");
            donate.selectInternationalDesignation(internationalChapterName);
            donationReview.validateBenefittingChapter(internationalChapterName);
            donationReview.updateYourInformation(yourFirstName, yourLastName, emailAddress, yourStreetAddress1, yourPostalCode, yourStreetAddress2);
            donationReview.updateBillingInformation("CREDIT CARD", creditCardNumber, expirationMonth, expirationYear, cvv);
            donationReview.submitDonation(); //uncomment for actual donation
            thankyou.validateThankYouPage("$5");
            thankyou.validateUserTransactionAndRefund();

            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Chapterwise Donation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_052_Verify_Local_Chapter_Donation()
        {

            #region TC_WEB_052 - Verify Local Chapter donation
            this.TESTREPORT.InitTestCase("TC_WEB_052 - Local Chapter Specific Donation", "Verify the successful donation for Local chapter from Make-A-Wish national donation page");
            #region Test Data
            string url = TestContext.DataRow["Url"].ToString();
            string localChapterName = TestContext.DataRow["LocalChapterName"].ToString();
            string yourFirstName = TestContext.DataRow["YourFirstName"].ToString();
            string yourLastName = TestContext.DataRow["YourLastName"].ToString();
            string emailAddress = TestContext.DataRow["YourEmailAddress"].ToString();
            string yourStreetAddress1 = TestContext.DataRow["YourStreetAddress1"].ToString();
            string yourStreetAddress2 = TestContext.DataRow["YourStreetAddress2"].ToString();
            string yourPostalCode = TestContext.DataRow["YourPostalCode"].ToString();
            string creditCardNumber = TestContext.DataRow["CreditCardNumber"].ToString();
            string expirationMonth = TestContext.DataRow["ExpirationMonth"].ToString();
            string expirationYear = TestContext.DataRow["ExpirationYear"].ToString();
            string cvv = TestContext.DataRow["CVV"].ToString();
            #endregion
            Home.navigateToDonationPage(url);
            donate.selectDonationAmount("$5");
            donate.selectLocalDesignation(localChapterName);
            donationReview.validateBenefittingChapter(localChapterName);
            donationReview.updateYourInformation(yourFirstName, yourLastName, emailAddress, yourStreetAddress1, yourPostalCode, yourStreetAddress2);
            donationReview.updateBillingInformation("CREDIT CARD", creditCardNumber, expirationMonth, expirationYear, cvv);
            donationReview.submitDonation(); //uncomment for actual donation
            thankyou.validateThankYouPage("$5");
            thankyou.validateUserTransactionAndRefund();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Chapterwise Donation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_053_Verify_Local_Chapter_Donation_With_ECard_And_design_General_ECard()
        {

            #region TC_WEB_053 - Local Chapter Donation with ECard Type and card design General ECard
            this.TESTREPORT.InitTestCase("TC_WEB_053 - Local Chapter Donation with ECard Type and card design General ECard", "Verify a donation for Local chapter specific Make-A-Wish Page with E-Card type of card design General E-Card (Mahlia)");
            #region Test Data
            string url = TestContext.DataRow["ChapterUrl"].ToString();
            string honoreeName = TestContext.DataRow["HonoreeName"].ToString();
            string honoreeFirstName = TestContext.DataRow["HonoreeFirstName"].ToString();
            string honoreeLastName = TestContext.DataRow["HonoreeLastName"].ToString();
            string cardDesignEmailAddress = TestContext.DataRow["EmailAddress"].ToString();
            string fromName = TestContext.DataRow["FromName"].ToString();
            string personalMessage = TestContext.DataRow["PersonalMessage"].ToString();
            string yourFirstName = TestContext.DataRow["YourFirstName"].ToString();
            string yourLastName = TestContext.DataRow["YourLastName"].ToString();
            string emailAddress = TestContext.DataRow["YourEmailAddress"].ToString();
            string yourStreetAddress1 = TestContext.DataRow["YourStreetAddress1"].ToString();
            string yourStreetAddress2 = TestContext.DataRow["YourStreetAddress2"].ToString();
            string yourPostalCode = TestContext.DataRow["YourPostalCode"].ToString();
            string creditCardNumber = TestContext.DataRow["CreditCardNumber"].ToString();
            string expirationMonth = TestContext.DataRow["ExpirationMonth"].ToString();
            string expirationYear = TestContext.DataRow["ExpirationYear"].ToString();
            string cvv = TestContext.DataRow["CVV"].ToString();

            #endregion
            Home.navigateToDonationPage(url);
            donate.selectDonationAmount("$5");
            donate.updateEcardHonoreeInformation(honoreeName, honoreeFirstName, honoreeLastName, cardDesignEmailAddress, fromName, personalMessage);
            donate.validateDesignationsNotDisplayed();
            donate.navigateToDonationReviewPage();
            donationReview.updateYourInformation(yourFirstName, yourLastName, emailAddress, yourStreetAddress1, yourPostalCode, yourStreetAddress2);
            donationReview.updateBillingInformation("CREDIT CARD", creditCardNumber, expirationMonth, expirationYear, cvv);
            donationReview.submitDonation(); //uncomment for actual donation
            thankyou.validateThankYouPage("$5");
            thankyou.validateUserTransactionAndRefund();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Chapterwise Donation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_054_Verify_Local_Chapter_Donation_With_PaperCard_And_design_MemorialPostCard()
        {

            #region TC_WEB_054 - Local Chapter Donation with PaperCard And design MemorialPostCard
            this.TESTREPORT.InitTestCase("TC_WEB_054 - Local Chapter Donation with PaperCard And design MemorialPostCard", "Verify a donation for Local chapter specific Make-A-Wish Page with Paper Card type of card design Memorial Postcard (Kionna)");
            #region Test Data
            string url = TestContext.DataRow["ChapterUrl"].ToString();
            string honoreeFirstName = TestContext.DataRow["HonoreeFirstName"].ToString();
            string honoreeLastName = TestContext.DataRow["HonoreeLastName"].ToString();
            string cardFirstName = TestContext.DataRow["CardFirstName"].ToString();
            string cardLastName = TestContext.DataRow["CardLastName"].ToString();
            string cardStreetAdd1 = TestContext.DataRow["CardStreetAdd1"].ToString();
            string cardStreetAdd2 = TestContext.DataRow["CardStreetAdd2"].ToString();
            string cardPostalCode = TestContext.DataRow["CardPostalCode"].ToString();
            string fromName = TestContext.DataRow["FromName"].ToString();
            string yourFirstName = TestContext.DataRow["YourFirstName"].ToString();
            string yourLastName = TestContext.DataRow["YourLastName"].ToString();
            string emailAddress = TestContext.DataRow["YourEmailAddress"].ToString();
            string yourStreetAddress1 = TestContext.DataRow["YourStreetAddress1"].ToString();
            string yourStreetAddress2 = TestContext.DataRow["YourStreetAddress2"].ToString();
            string yourPostalCode = TestContext.DataRow["YourPostalCode"].ToString();
            string creditCardNumber = TestContext.DataRow["CreditCardNumber"].ToString();
            string expirationMonth = TestContext.DataRow["ExpirationMonth"].ToString();
            string expirationYear = TestContext.DataRow["ExpirationYear"].ToString();
            string cvv = TestContext.DataRow["CVV"].ToString();

            #endregion
            Home.navigateToDonationPage(url);
            donate.selectDonationAmount("$5");
            donate.updatePapercardMemorialHonoreeInformation(honoreeFirstName, honoreeLastName, cardFirstName, cardLastName, cardStreetAdd1, cardStreetAdd2, cardPostalCode, fromName);
            donate.validateDesignationsNotDisplayed();
            donate.navigateToDonationReviewPage();
            donationReview.updateYourInformation(yourFirstName, yourLastName, emailAddress, yourStreetAddress1, yourPostalCode, yourStreetAddress2);
            donationReview.updateBillingInformation("CREDIT CARD", creditCardNumber, expirationMonth, expirationYear, cvv);
            donationReview.submitDonation(); //uncomment for actual donation
            thankyou.validateThankYouPage("$5");
            thankyou.validateUserTransactionAndRefund();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Chapterwise Donation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_055_Verify_Local_Chapter_Donation_With_PaperCard_And_design_TributePostCard()
        {

            #region TC_WEB_055 - Local Chapter Donation with PaperCard And design TributePostCard
            this.TESTREPORT.InitTestCase("TC_WEB_055 - Local Chapter Donation with PaperCard And design TributePostCard", "Verify a donation for Local chapter specific Make-A-Wish Page with Paper Card type of card design Tribute Postcard (Gabe)");
            #region Test Data
            string url = TestContext.DataRow["ChapterUrl"].ToString();
            string honoreeFirstName = TestContext.DataRow["HonoreeFirstName"].ToString();
            string honoreeLastName = TestContext.DataRow["HonoreeLastName"].ToString();
            string cardFirstName = TestContext.DataRow["CardFirstName"].ToString();
            string cardLastName = TestContext.DataRow["CardLastName"].ToString();
            string cardStreetAdd1 = TestContext.DataRow["CardStreetAdd1"].ToString();
            string cardStreetAdd2 = TestContext.DataRow["CardStreetAdd2"].ToString();
            string cardPostalCode = TestContext.DataRow["CardPostalCode"].ToString();
            string fromName = TestContext.DataRow["FromName"].ToString();
            string yourFirstName = TestContext.DataRow["YourFirstName"].ToString();
            string yourLastName = TestContext.DataRow["YourLastName"].ToString();
            string emailAddress = TestContext.DataRow["YourEmailAddress"].ToString();
            string yourStreetAddress1 = TestContext.DataRow["YourStreetAddress1"].ToString();
            string yourStreetAddress2 = TestContext.DataRow["YourStreetAddress2"].ToString();
            string yourPostalCode = TestContext.DataRow["YourPostalCode"].ToString();
            string creditCardNumber = TestContext.DataRow["CreditCardNumber"].ToString();
            string expirationMonth = TestContext.DataRow["ExpirationMonth"].ToString();
            string expirationYear = TestContext.DataRow["ExpirationYear"].ToString();
            string cvv = TestContext.DataRow["CVV"].ToString();

            #endregion
            Home.navigateToDonationPage(url);
            donate.selectDonationAmount("$5");
            donate.updatePapercardTributeHonoreeInformation(honoreeFirstName, honoreeLastName, cardFirstName, cardLastName, cardStreetAdd1, cardStreetAdd2, cardPostalCode, fromName);
            donate.validateDesignationsNotDisplayed();
            donate.navigateToDonationReviewPage();
            donationReview.updateYourInformation(yourFirstName, yourLastName, emailAddress, yourStreetAddress1, yourPostalCode, yourStreetAddress2);
            donationReview.updateBillingInformation("CREDIT CARD", creditCardNumber, expirationMonth, expirationYear, cvv);
            donationReview.submitDonation(); //uncomment for actual donation
            thankyou.validateThankYouPage("$5");
            thankyou.validateUserTransactionAndRefund();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Chapterwise Donation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_056_Verify_Local_Chapter_Donation_With_PrintableCert_And_design_TributeCert()
        {

            #region TC_WEB_056 - Local Chapter Donation with PrintableCertificate and design Tribute Certificate
            this.TESTREPORT.InitTestCase("TC_WEB_056 - Local Chapter Donation with PrintableCertificate and design Tribute Certificate", "Verify  a donation for Local chapter specific Make-A-Wish Page with Printable Certificate Card type of card design Tribute Certificate (Mahlia)");
            #region Test Data
            string url = TestContext.DataRow["ChapterUrl"].ToString();
            string honoreeFirstName = TestContext.DataRow["HonoreeFirstName"].ToString();
            string honoreeLastName = TestContext.DataRow["HonoreeLastName"].ToString();
            string fromName = TestContext.DataRow["FromName"].ToString();
            string yourFirstName = TestContext.DataRow["YourFirstName"].ToString();
            string yourLastName = TestContext.DataRow["YourLastName"].ToString();
            string emailAddress = TestContext.DataRow["YourEmailAddress"].ToString();
            string yourStreetAddress1 = TestContext.DataRow["YourStreetAddress1"].ToString();
            string yourStreetAddress2 = TestContext.DataRow["YourStreetAddress2"].ToString();
            string yourPostalCode = TestContext.DataRow["YourPostalCode"].ToString();
            string creditCardNumber = TestContext.DataRow["CreditCardNumber"].ToString();
            string expirationMonth = TestContext.DataRow["ExpirationMonth"].ToString();
            string expirationYear = TestContext.DataRow["ExpirationYear"].ToString();
            string cvv = TestContext.DataRow["CVV"].ToString();

            #endregion
            Home.navigateToDonationPage(url);
            donate.selectDonationAmount("$5");
            donate.updatePrintableCertTributeHonoreeInformation(honoreeFirstName, honoreeLastName, fromName);
            donate.validateDesignationsNotDisplayed();
            donate.navigateToDonationReviewPage();
            donationReview.updateYourInformation(yourFirstName, yourLastName, emailAddress, yourStreetAddress1, yourPostalCode, yourStreetAddress2);
            donationReview.updateBillingInformation("CREDIT CARD", creditCardNumber, expirationMonth, expirationYear, cvv);
            donationReview.submitDonation(); //uncomment for actual donation
            thankyou.validateThankYouPage("$5");
            thankyou.validateUserTransactionAndRefund();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Home Page UI")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_057_Verify_DonationPage_Navigation_Using_Button_On_Carousel()
        {

            #region TC_WEB_057 - Navigate to Donation page using Donate button on carousel
            this.TESTREPORT.InitTestCase("TC_WEB_057 - Navigate to DonationPage using Donate button on carousel", "Verify the navigation to Donate screen from Donate button present on Homepage carousel");
            #region Test Data
            string url = TestContext.DataRow["Url"].ToString();

            #endregion
            Home.navigateToDonationPageUsingCarousel("http://wish.org");
            donate.validateDonatePageElements();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Chapterwise Donation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_058_Verify_LocalChapter_DonationPage_Navigation_Using_Button_On_Carousel()
        {

            #region TC_WEB_058 - Navigate to Local Chapter Donation page using Donate button on carousel
            this.TESTREPORT.InitTestCase("TC_WEB_058 - Navigate to Local Chapter DonationPage using Donate button on carousel", "Verify the navigation to Donate screen from Donate present in Homepage carousel for Local chapter specific Make-A-Wish Page");
            #region Test Data
            string url = TestContext.DataRow["Url"].ToString();

            #endregion
            Home.navigateToDonationPageUsingCarousel("http://illinois.wish.org/");
            donate.validateDonatePageElements("chapter");
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("End-To-End Donation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_059_Verify_Donation_With_Paypal_Account()
        {

            #region TC_WEB_059 - Donation with PayPal account
            this.TESTREPORT.InitTestCase("TC_WEB_059 - Donation with PayPal account", "Verify the successful donation with PayPal option");
            #region Test Data
            string url = TestContext.DataRow["Url"].ToString();
            string yourFirstName = TestContext.DataRow["YourFirstName"].ToString();
            string yourLastName = TestContext.DataRow["YourLastName"].ToString();
            string emailAddress = TestContext.DataRow["YourEmailAddress"].ToString();
            string yourStreetAddress1 = TestContext.DataRow["YourStreetAddress1"].ToString();
            string yourStreetAddress2 = TestContext.DataRow["YourStreetAddress2"].ToString();
            string yourPostalCode = TestContext.DataRow["YourPostalCode"].ToString();
            string paypalEmailId = TestContext.DataRow["PaypalEmailId"].ToString();
            string paypalPassword = TestContext.DataRow["PaypalPassword"].ToString();
            #endregion
            Home.navigateToDonationPage(url);
            donate.selectDonationAmount("$5");
            donate.navigateToDonationReviewPage();
            donationReview.updateYourInformation(yourFirstName, yourLastName, emailAddress, yourStreetAddress1, yourPostalCode, yourStreetAddress2);
            donationReview.updateBillingInformation("PAYPAL");
            donationReview.submitDonation();
            paypal.verifyPayPalScreen();
            paypal.validatePaypalTransaction(paypalEmailId, paypalPassword);
            thankyou.validateThankYouPage("$5");
            thankyou.validateUserTransactionAndRefund("DONATION","$5");
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Chapterwise Donation")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_060_Verify_Local_Chapter_Donation_With_Paypal_Account()
        {

            #region TC_WEB_060 - Local Chapter Donation with PayPal account
            this.TESTREPORT.InitTestCase("TC_WEB_060 - Local Chapter Donation with PayPal account", "Verify  a donation for Local chapter specific Make-A-Wish Page with PayPal option");
            #region Test Data
            string url = TestContext.DataRow["Url"].ToString();
            string localChapterName = TestContext.DataRow["LocalChapterName"].ToString();
            string yourFirstName = TestContext.DataRow["YourFirstName"].ToString();
            string yourLastName = TestContext.DataRow["YourLastName"].ToString();
            string emailAddress = TestContext.DataRow["YourEmailAddress"].ToString();
            string yourStreetAddress1 = TestContext.DataRow["YourStreetAddress1"].ToString();
            string yourStreetAddress2 = TestContext.DataRow["YourStreetAddress2"].ToString();
            string yourPostalCode = TestContext.DataRow["YourPostalCode"].ToString();
            string paypalEmailId = TestContext.DataRow["PaypalEmailId"].ToString();
            string paypalPassword = TestContext.DataRow["PaypalPassword"].ToString();
            #endregion
            Home.navigateToDonationPage(url);
            donate.selectDonationAmount("$5");
            donate.selectLocalDesignation(localChapterName);
            donationReview.validateBenefittingChapter(localChapterName);
            donationReview.updateYourInformation(yourFirstName, yourLastName, emailAddress, yourStreetAddress1, yourPostalCode, yourStreetAddress2);
            donationReview.updateBillingInformation("PAYPAL");
            donationReview.submitDonation();
            paypal.verifyPayPalScreen();
            paypal.validatePaypalTransaction(paypalEmailId, paypalPassword);
            thankyou.validateThankYouPage("$5");
            thankyou.validateUserTransactionAndRefund("DONATION", "$5");
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }

        [TestMethod]
        [TestCategory("Clear & Start Over")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MAWTestData.csv", "MAWTestData#csv", DataAccessMethod.Sequential), DeploymentItem("MAWTestData.csv")]
        public void TC_WEB_061_Verify_Local_Donation_Clear_Form_And_Start_Over()
        {

            #region Validate Local Chapter donation by Clear form and start over 
            this.TESTREPORT.InitTestCase("TC_WEB_060 - Verify Local Donation during Clear form and Start Over", "Verify Local Chapter donation by Clear form and start over ");
            string url = TestContext.DataRow["Url"].ToString(); //readCSV("MAWTestData", "Url");
            Home.navigateToDonationPage(url);
            donate.validateLocalDonationAfterClearForm();
            this.TESTREPORT.UpdateTestCaseStatus();
            #endregion

        }
                
    }
}
