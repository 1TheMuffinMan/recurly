using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using RestSharp.Deserializers;
using RestSharp.Serializers;
[assembly: InternalsVisibleTo("TestBench")]
namespace Recurly
{
    [DeserializeAs(Name = "coupon")]
    [SerializeAs(Name = "coupon")]
    [XmlRoot("coupon")]
    public class RecurlyCoupon
    {
        public enum CouponState : short
        {
            All = 0,
            Redeemable,
            Expired,
            Inactive,
            MaxedOut
        }

        public enum CouponDiscountType
        {
            [XmlEnum("percent")]
            Percent,
            [XmlEnum("dollars")]
            Dollars
        }

        internal const string UrlPrefix = "coupons/";

        [XmlIgnore]
        public List<Redemption> Redemptions { get; internal set; }

        [DeserializeAs(Name = "redemptions")]
        [XmlIgnore]
        public RecurlyRedemptionUrl RedemptionsUrl { get; internal set; }

        /// <summary>
        /// Unique code to identify and redeem the coupon. This code may only contain the following characters: [a-z A-Z 0-9 @ - _ .]. Max of 50 characters.
        /// </summary>
        [XmlElement("coupon_code")]
        public string CouponCode { get; set; }

        /// <summary>
        /// Coupon name
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Description of the coupon on the hosted payment pages
        /// </summary>
        [XmlElement("hosted_description")]
        public string HostedDescription { get; set; }

        /// <summary>
        /// Description of the coupon on the invoice
        /// </summary>
        [XmlElement("invoice_description")]
        public string InvoiceDescription { get; set; }

        /// <summary>
        /// Last date to redeem the coupon, defaults to no date
        /// </summary>
        [XmlElement("redeem_by_date")]
        public DateTime? RedeemByDate { get; set; }

        public bool RedeemByDateSpecified { get { return RedeemByDate.HasValue; } }

        /// <summary>
        /// If true, the coupon applies to the first invoice only
        /// </summary>
        [XmlElement("single_use")]
        public bool SingleUse { get; set; }

        /// <summary>
        /// Number of months after redemption that the coupon is valid, defaults to no date
        /// </summary>
        [XmlElement("applies_for_months")]
        public int? AppliesForMonths { get; set; }

        public bool AppliesForMonthsSpecified { get { return AppliesForMonths.HasValue; } }

        /// <summary>
        /// Maximum number of accounts that may use the coupon before it can no longer be redeemed
        /// </summary>
        [XmlElement("max_redemptions")]
        public int? MaxRedemptions { get; set; }

        /// <summary>
        /// The coupon is valid for all plans if true, defaults to true
        /// </summary>
        [XmlElement("applies_to_all_plans")]
        public bool AppliesToAllPlans { get; set; }

        [XmlElement("discount_type")]
        public CouponDiscountType DiscountType { get; set; }

        [XmlElement("state")]
        [XmlIgnore]
        public CouponState State { get; internal set; }

        /// <summary>
        /// A dictionary of currencies and discounts
        /// </summary>
        [XmlElement("discount_in_cents")]
        public RecurlyCurrency DiscountInCents { get; set; }

        /// <summary>
        /// Discount percentage if discount_type is "percent"
        /// </summary>
        [XmlElement("discount_percent")]
        public int? DiscountPercent { get; set; }
        public bool DiscountPercentSpecified { get { return DiscountPercent.HasValue; } }

        /// <summary>
        /// A list of plans to limit the coupon
        /// </summary>
        [XmlElement("plan_codes", IsNullable = true)]
        public List<string> Plans { get; set; }

        [DeserializeAs(Name = "created_at")]
        [XmlIgnore]
        public DateTime CreatedAt { get; internal set; }

        public RecurlyCoupon(string code, string name, RecurlyCurrency discounts)
        {
            Plans = new List<string>();
            Redemptions = new List<Redemption>();
            CouponCode = code;
            Name = name;
            DiscountInCents = discounts;
            DiscountType = CouponDiscountType.Dollars;
            AppliesToAllPlans = true;
        }

        public RecurlyCoupon(string code, string name, int discountPercent)
        {
            Plans = new List<string>();
            Redemptions = new List<Redemption>();

            CouponCode = code;
            Name = name;
            DiscountPercent = discountPercent;
            DiscountType = CouponDiscountType.Percent;
            AppliesToAllPlans = true;

        }

        public RecurlyCoupon() : this(null, null, 0) { }
    }
}
