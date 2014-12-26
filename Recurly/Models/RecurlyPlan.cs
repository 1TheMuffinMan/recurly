using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp.Deserializers;

namespace Recurly
{
    [DeserializeAs(Name = "plan")]
    public class RecurlyPlan
    {
        public enum IntervalUnit
        {
            Days,
            Months
        }

        public string PlanCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }

        public bool? DisplayDonationAmounts { get; set; }
        public bool? DisplayQuantity { get; set; }
        public bool? DisplayPhoneNumber { get; set; }
        public bool? BypassHostedConfirmation { get; set; }

        public string UnitName { get; set; }
        public string PaymentPageTOSLink { get; set; }

        public int PlanIntervalLength { get; set; }
        public IntervalUnit PlanIntervalUnit { get; set; }

        public int TrialIntervalLength { get; set; }
        public IntervalUnit TrialIntervalUnit { get; set; }

        public string AccountingCode { get; set; }

        public DateTime CreatedAt { get; private set; }

        public int? TotalBillingCycles { get; set; }

        public bool? TaxExempt { get; set; }

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

        private Dictionary<string, int> _unitAmountInCents;
        /// <summary>
        /// A dictionary of currencies and values for the subscription amount
        /// </summary>
        public Dictionary<string, int> UnitAmountInCents
        {
            get { return _unitAmountInCents ?? (_unitAmountInCents = new Dictionary<string, int>()); }
        }

        private Dictionary<string, int> _setupFeeInCents;
        /// <summary>
        /// A dictionary of currency and values for the setup fee
        /// </summary>
        public Dictionary<string, int> SetupFeeInCents
        {
            get { return _setupFeeInCents ?? (_setupFeeInCents = new Dictionary<string, int>()); }
        }

        internal const string UrlPrefix = "/plans/";

        public RecurlyPlan() { }

        public RecurlyPlan(string planCode, string name)
        {
            PlanCode = planCode;
            Name = name;
        }
    }
}
