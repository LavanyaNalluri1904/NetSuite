using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUT.Selenium.ApplicationSpecific.MAW.Pages
{
    public class UserTransaction
    {
        public class Amount
        {
            public string formatted { get; set; }
            public string @decimal { get; set; }
        }

        public class BillingAddress
        {
            public string zip { get; set; }
            public string country { get; set; }
            public object street3 { get; set; }
            public string city { get; set; }
            public object county { get; set; }
            public string street1 { get; set; }
            public string street2 { get; set; }
            public string state { get; set; }
        }

        public class Form
        {
            public object description { get; set; }
            public string id { get; set; }
            public string title { get; set; }
        }

        public class Description
        {
        }

        public class Level
        {
            public Description description { get; set; }
            public string id { get; set; }
            public string title { get; set; }
        }

        public class Campaign
        {
            public object description { get; set; }
            public string id { get; set; }
            public string title { get; set; }
        }

        public class ValueOfGoods
        {
            public string formatted { get; set; }
            public string @decimal { get; set; }
        }

        public class TaxDeductibleAmount
        {
            public string formatted { get; set; }
            public string @decimal { get; set; }
        }

        public class DonationDetail
        {
            public Form form { get; set; }
            public Level level { get; set; }
            public Campaign campaign { get; set; }
            public ValueOfGoods value_of_goods { get; set; }
            public TaxDeductibleAmount tax_deductible_amount { get; set; }
        }

        public class BillingName
        {
            public object middle { get; set; }
            public object prof_suffix { get; set; }
            public string last { get; set; }
            public object title { get; set; }
            public object suffix { get; set; }
            public string first { get; set; }
        }

        public class RootObject
        {
            public Amount amount { get; set; }
            public string credit_card_type { get; set; }
            public string tender_type { get; set; }
            public string credit_card_number { get; set; }
            public string confirmation_code { get; set; }
            public string credit_card_expiration { get; set; }
            public BillingAddress billing_address { get; set; }
            public DonationDetail donation_detail { get; set; }
            public string pay_method { get; set; }
            public string transaction_type { get; set; }
            public BillingName billing_name { get; set; }
            public string timestamp { get; set; }
        }
    }
}