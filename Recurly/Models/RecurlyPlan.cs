using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace Recurly
{
    [XmlRoot("plan")]
    [DeserializeAs(Name = "plan")]
    public class RecurlyPlan
    {
        public enum IntervalUnit
        {
            [XmlEnum("days")]
            Days,
            [XmlEnum("months")]
            Months
        }

        [XmlElement("plan_code")]
        public string PlanCode { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("success_url")]
        public string SuccessUrl { get; set; }

        [XmlElement("cancel_url")]
        public string CancelUrl { get; set; }

        [XmlElement("display_donation_amounts")]
        public bool? DisplayDonationAmounts { get; set; }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool DisplayDonationAmountsSpecified { get { return DisplayDonationAmounts.HasValue; } }


        [XmlElement("display_quantity")]
        public bool? DisplayQuantity { get; set; }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool DisplayQuantitySpecified { get { return DisplayQuantity.HasValue; } }

        [XmlElement("display_phone_number")]
        public bool? DisplayPhoneNumber { get; set; }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool DisplayPhoneNumberSpecified { get { return DisplayPhoneNumber.HasValue; } }

        [XmlElement("bypass_hosted_confirmation")]
        public bool? BypassHostedConfirmation { get; set; }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool BypassHostedConfirmationSpecified { get { return BypassHostedConfirmation.HasValue; } }

        [XmlElement("unit_name")]
        public string UnitName { get; set; }

        [XmlElement("payment_page_tos_link")]
        public string PaymentPageTOSLink { get; set; }

        [XmlElement("plan_interval_length")]
        public int PlanIntervalLength { get; set; }

        [XmlElement("plan_interval_unit")]
        public IntervalUnit PlanIntervalUnit { get; set; }

        [XmlElement("trial_interval_length")]
        public int TrialIntervalLength { get; set; }

        [XmlElement("trial_interval_unit")]
        public IntervalUnit TrialIntervalUnit { get; set; }

        [XmlElement("accounting_code")]
        public string AccountingCode { get; set; }

        [XmlIgnore]
        public DateTime CreatedAt { get; set; }

        [XmlElement("total_billing_cycles")]
        public int? TotalBillingCycles { get; set; }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool TotalBillingCyclesSpecified { get { return TotalBillingCycles.HasValue; } }

        [XmlElement("tax_exempt")]
        public bool? TaxExempt { get; set; }

        [XmlElement("tax_code")]
        public string TaxCode { get; set; }

        //private AddOnList _addOns;

        //public RecurlyList<AddOn> AddOns
        //{
        //    get
        //    {
        //        if (_addOns == null)
        //        {
        //            var url = UrlPrefix + Uri.EscapeUriString(PlanCode) + "/add_ons/";
        //            _addOns = new AddOnList(url);
        //        }
        //        return _addOns;
        //    }
        //}

        /// <summary>
        /// Currencies and values for the subscription amount
        /// </summary>
        [XmlElement("unit_amount_in_cents")]
        public RecurlyCurrency UnitAmountInCents { get; set; }
        
        /// <summary>
        /// Currencies and values for the setup fee
        /// </summary>
        [XmlElement("setup_fee_in_cents", IsNullable = false)]
        public RecurlyCurrency SetupFeeInCents { get; set; }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool SetupFeeInCentsSpecified { get { return SetupFeeInCents.AnySpecified; } }

        internal const string UrlPrefix = "/plans/";

        public RecurlyPlan(string planCode, string name) : this()
        {
            PlanCode = planCode;
            Name = name;
        }

        public RecurlyPlan()
        {
            SetupFeeInCents = new RecurlyCurrency();
            UnitAmountInCents = new RecurlyCurrency();
            PlanIntervalLength = 1;
        }
    }
}
