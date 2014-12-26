using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recurly
{
    public class RecurlySubscription
    {
        [Flags]
        // The currently valid Subscription States
        public enum SubscriptionState : short
        {
            All = 0,
            Active = 1,
            Canceled = 2,
            Expired = 4,
            Future = 8,
            InTrial = 16,
            Live = 32,
            PastDue = 64,
            Pending = 128
        }

        public enum ChangeTimeframe : short
        {
            Now,
            Renewal
        }

        public enum RefundType : short
        {
            Full,
            Partial,
            None
        }

        public string Uuid { get; private set; }

        public SubscriptionState State { get; private set; }

        /// <summary>
        /// Unit amount per quantity.  Leave null to keep as is. Set to override plan's default amount.
        /// </summary>
        public int? UnitAmountInCents { get; set; }

        public string Currency { get; set; }
        public int Quantity { get; set; }

        /// <summary>
        /// Date the subscription started.
        /// </summary>
        public DateTime? ActivatedAt { get; private set; }
        /// <summary>
        /// If set, the date the subscriber canceled their subscription.
        /// </summary>
        public DateTime? CanceledAt { get; private set; }
        /// <summary>
        /// If set, the subscription will expire/terminate on this date.
        /// </summary>
        public DateTime? ExpiresAt { get; private set; }
        /// <summary>
        /// Date the current invoice period started.
        /// </summary>
        public DateTime? CurrentPeriodStartedAt { get; private set; }
        /// <summary>
        /// The subscription is paid until this date. Next invoice date.
        /// </summary>
        public DateTime? CurrentPeriodEndsAt { get; private set; }
        /// <summary>
        /// Date the trial started, if the subscription has a trial.
        /// </summary>
        public DateTime? TrialPeriodStartedAt { get; private set; }

        /// <summary>
        /// Date the trial ends, if the subscription has/had a trial.
        /// 
        /// This may optionally be set on new subscriptions to specify an exact time for the 
        /// subscription to commence.  The subscription will be active and in "trial" until
        /// this date.
        /// </summary>
        public DateTime? TrialPeriodEndsAt
        {
            get { return _trialPeriodEndsAt; }
            set
            {
                if (ActivatedAt.HasValue)
                    throw new InvalidOperationException("Cannot set TrialPeriodEndsAt on existing subscriptions.");
                if (value.HasValue && (value < DateTime.UtcNow))
                    throw new ArgumentException("TrialPeriodEndsAt must occur in the future.");

                _trialPeriodEndsAt = value;
            }
        }
        private DateTime? _trialPeriodEndsAt;

        /// <summary>
        /// If set, the subscription will begin in the future on this date. 
        /// The subscription will apply the setup fee and trial period, unless the plan has no trial.
        /// </summary>
        public DateTime? StartsAt { get; set; }

        /// <summary>
        /// Represents pending changes to the subscription
        /// </summary>
        public RecurlySubscription PendingSubscription { get; private set; }

        /// <summary>
        /// If true, this is a "pending subscription" object and no changes are allowed
        /// </summary>
        private bool IsPendingSubscription { get; set; }

        /// <summary>
        /// Optional coupon for the subscription
        /// </summary>
        public RecurlyCoupon Coupon { get; set; }

        /// <summary>
        /// List of add ons for this subscription
        /// </summary>
        //public SubscriptionAddOnList AddOns
        //{
        //    get { return _addOns ?? (_addOns = new SubscriptionAddOnList(this)); }
        //    set { _addOns = value; }
        //}
        //private SubscriptionAddOnList _addOns;

        public int? TotalBillingCycles { get; set; }
        public DateTime? FirstRenewalDate { get; set; }

        internal const string UrlPrefix = "/subscriptions/";

        public string CollectionMethod { get; set; }
        public int? NetTerms { get; set; }
        public string PoNumber { get; set; }

        /// <summary>
        /// Amount of tax or VAT within the transaction, in cents.
        /// </summary>
        public int? TaxInCents { get; private set; }

        /// <summary>
        /// Tax type as "vat" for VAT or "usst" for US Sales Tax.
        /// </summary>
        public string TaxType { get; private set; }

        /// <summary>
        /// Tax rate that will be applied to this subscription.
        /// </summary>
        public decimal? TaxRate { get; private set; }
    }
}
