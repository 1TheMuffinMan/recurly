using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Recurly;

namespace TestBench
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RecurlyClient();
          //  var coupon = client.GetCouponAsync("35off", true);
         //   var coupons = client.ListCouponsAsync();
          //  var account = client.GetAccountAsync("auth0|542e31d8fcc9436ae5001cbd", false);
           // var t = account.Result;
            //   var billing = client.GetBillingInfo("auth0|542e31d8fcc9436ae5001cbd");
            var coupon =
                client.CreateCouponAsync(new RecurlyCoupon("test1", "Test 1", 20)
                {
                    DiscountPercent = 5,
                });

            //var t = client.DeactivateCouponAsync("test2");
            Task.WaitAll(coupon);
          //  Console.WriteLine(account.Result.AccountCode);
            //   var t = billing;
        }
    }
}
