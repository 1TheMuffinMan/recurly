using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Recurly.Extensions;
using RestSharp;
using RestSharp.Serializers;

namespace Recurly
{
    public partial class RecurlyClient
    {
        public RecurlyCoupon GetCoupon(string couponCode)
        {
            var couponAsync = GetCouponAsync(couponCode);
            return couponAsync.Result;
        }

        public Task<RecurlyCoupon> GetCouponAsync(string couponCode, bool includeRedemptions = false)
        {
            var request = new RestRequest(RecurlyCoupon.UrlPrefix + "{couponCode}")
                .AddUrlSegment("couponCode", couponCode);

            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Parent task starting");
                var response = Execute<RecurlyCoupon>(request);

                if (includeRedemptions && !string.IsNullOrWhiteSpace(response.Data.RedemptionsUrl.Url))
                {
                    var result = GetResourceAsync<List<Redemption>>(response.Data.RedemptionsUrl.Url);
                    response.Data.Redemptions = result.Result.Data;
                }

                return response.Data;
            });
        }

        public List<RecurlyCoupon> ListCoupons(RecurlyCoupon.CouponState couponState = RecurlyCoupon.CouponState.All, bool includeRedemptions = false)
        {
            return ListCouponsAsync(couponState, includeRedemptions).Result;
        }

        public Task<List<RecurlyCoupon>> ListCouponsAsync(RecurlyCoupon.CouponState couponState = RecurlyCoupon.CouponState.All, bool includeRedemptions = false)
        {
            var request = new RestRequest(RecurlyCoupon.UrlPrefix);

            if (couponState != RecurlyCoupon.CouponState.All)
            {
                request.Resource += "?state={couponState}";
                request.AddUrlSegment("couponState", couponState.ToTransportCase());
            }

            return Task.Factory.StartNew(() =>
            {
                return Execute<List<RecurlyCoupon>>(request).Data;
            });
        }

        /// <summary>
        /// Creates a new coupon. Please note: coupons cannot be updated after being created.
        /// </summary>
        public RecurlyCoupon CreateCoupon(RecurlyCoupon coupon)
        {
            return CreateCouponAsync(coupon).Result;
        }

        /// <summary>
        /// Creates a new coupon. Please note: coupons cannot be updated after being created.
        /// </summary>
        public Task<RecurlyCoupon> CreateCouponAsync(RecurlyCoupon coupon)
        {
            var request = new RestRequest(RecurlyCoupon.UrlPrefix, Method.POST);
            request.XmlSerializer = new DotNetXmlSerializer("www.contoso.com");
            request.AddXmlBody(coupon);

            return ExecuteAsync<RecurlyCoupon>(request);
        }

        /// <summary>
        /// Deactivate the coupon so customers can no longer redeem the coupon.
        /// </summary>
        public void DeactivateCoupon(string couponCode)
        {
            DeactivateCouponAsync(couponCode).Wait();
        }

        /// <summary>
        /// Deactivate the coupon so customers can no longer redeem the coupon.
        /// </summary>
        public Task DeactivateCouponAsync(string couponCode)
        {
            var request = new RestRequest(RecurlyCoupon.UrlPrefix + "{couponCode}", Method.DELETE)
                .AddUrlSegment("couponCode", couponCode);

            return ExecuteAsync(request);
        }
    }
}