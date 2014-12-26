using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Xml;
using RestSharp.Serializers;

namespace Recurly
{
    [SerializeAs(Name = "billing_info")]
    public class RecurlyBillingInfo
    {
        public enum CreditCardType : short
        {
            Invalid,
            Visa,
            MasterCard,
            AmericanExpress,
            Discover,
            JCB
        }

        /// <summary>
        /// Account Code or unique ID for the account in Recurly
        /// </summary>
        [SerializeAs(Name = "account_code")]
        public string AccountCode { get; private set; }

        [SerializeAs(Name = "first_name")]
        public string FirstName { get; set; }

        [SerializeAs(Name = "last_name")]
        public string LastName { get; set; }

        [SerializeAs(Name = "address1")]
        public string Address1 { get; set; }

        [SerializeAs(Name = "address2")]
        public string Address2 { get; set; }

        [SerializeAs(Name = "city")]
        public string City { get; set; }

        /// <summary>
        /// 2-letter state or province preferred
        /// </summary>
        [SerializeAs(Name = "state")]
        public string State { get; set; }

        /// <summary>
        /// 2-letter ISO country code
        /// </summary>
        [SerializeAs(Name = "country")]
        public string Country { get; set; }

        /// <summary>
        /// Zip code or Postal code
        /// </summary>
        [SerializeAs(Name = "postal_code")]
        public string PostalCode { get; set; }

        [SerializeAs(Name = "phone_number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// VAT Numbers
        /// </summary>
        [SerializeAs(Name = "vat_number")]
        public string VatNumber { get; set; }

        [SerializeAs(Name = "ip_address")]
        public string IpAddress { get; set; }

        [SerializeAs(Name = "ip_address_country")]
        public string IpAddressCountry { get; private set; }

        /// <summary>
        /// Credit Card Number, first six digits
        /// </summary>
        [SerializeAs(Name = "first_six")]
        public string FirstSix { get; set; }

        /// <summary>
        /// Credit Card Number, last four digits
        /// </summary>
        [SerializeAs(Name = "last_four")]
        public string LastFour { get; set; }

        [SerializeAs(Name = "card_type")]
        public CreditCardType CardType { get; set; }

        [SerializeAs(Name = "expiration_month")]
        public int ExpirationMonth { get; set; }

        [SerializeAs(Name = "expiration_year")]
        public int ExpirationYear { get; set; }

        [SerializeAs(Name = "company")]
        public string Company { get; set; }

        /// <summary>
        /// Paypal Billing Agreement ID
        /// </summary>
        [SerializeAs(Name = "paypal_billing_agreement_id")]
        public string PaypalBillingAgreementId { get; set; }

        /// <summary>
        /// Amazon Billing Agreement ID
        /// </summary>
        [SerializeAs(Name = "amazon_billing_agreement_id")]
        public string AmazonBillingAgreementId { get; set; }

        private string _cardNumber;

        /// <summary>
        /// Credit card number
        /// </summary>
        [SerializeAs(Name = "credit_card_number")]
        public string CreditCardNumber
        {
            get { return _cardNumber; }
            set
            {
                //_cardNumber = value;
                //CreditCardType type;
                //if (value.IsValidCreditCardNumber(out type))
                //{
                //    var digits = value.Where(char.IsDigit).AsString();
                //    CardType = type;
                //    FirstSix = digits.Substring(0, 6);
                //    LastFour = digits.Last(4);
                //}
                //else
                //{
                //    CardType = CreditCardType.Invalid;
                //    FirstSix = LastFour = null;
                //}
            }
        }

        [SerializeAs(Name = "verification_value")]
        public string VerificationValue { get; set; }

        [SerializeAs(Name = "token_id")]
        public string TokenId { get; set; }

        private const string UrlPrefix = "/accounts/";
        private const string UrlPostfix = "/billing_info";

        public RecurlyBillingInfo() { }

        public RecurlyBillingInfo(string accountCode)
        {
            AccountCode = accountCode;
        }

        //public RecurlyBillingInfo(Account account)
        //    : this()
        //{
        //    AccountCode = account.AccountCode;
        //}

        //private RecurlyBillingInfo()
        //{
        //}

        ///// <summary>
        ///// Lookup a Recurly account's billing info
        ///// </summary>
        ///// <param name="accountCode"></param>
        ///// <returns></returns>
        //public static RecurlyBillingInfo Get(string accountCode)
        //{
        //    var billingInfo = new RecurlyBillingInfo();

        //    var statusCode = Client.Instance.PerformRequest(Client.HttpRequestMethod.Get,
        //        BillingInfoUrl(accountCode),
        //        billingInfo.ReadXml);

        //    return statusCode == HttpStatusCode.NotFound ? null : billingInfo;
        //}

        ///// <summary>
        ///// Update an account's billing info in Recurly
        ///// </summary>
        //public void Create()
        //{
        //    Update();
        //}

        ///// <summary>
        ///// Update an account's billing info in Recurly
        ///// </summary>
        //public void Update()
        //{
        //    Client.Instance.PerformRequest(Client.HttpRequestMethod.Put,
        //        BillingInfoUrl(AccountCode),
        //        WriteXml,
        //        ReadXml);
        //}

        private static string BillingInfoUrl(string accountCode)
        {
            return UrlPrefix + Uri.EscapeUriString(accountCode) + UrlPostfix;
        }
    }
}