using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Recurly.Test
{
    public class CouponTest : BaseTest
    {
        [Fact]
        public void ListCoupons()
        {
            CreateNewCoupon(1);
            CreateNewCoupon(2);

            var coupons = Client.ListCoupons();
            coupons.Should().NotBeEmpty();
        }

        [Fact]
        public void ListCouponsRedeemable()
        {
            var coupon1 = CreateNewCoupon(1);
            Client.DeactivateCoupon(coupon1.CouponCode);
            CreateNewCoupon(2);

            var coupons = Client.ListCoupons(RecurlyCoupon.CouponState.Redeemable);
            coupons.Should().NotBeEmpty();
        }

        [Fact]
        public void CouponsCanBeCreated()
        {
            var discounts = new RecurlyCurrency { USD = 100 };
            var coupon = new RecurlyCoupon(GetMockCouponCode(), GetMockCouponName(), discounts)
            {
                MaxRedemptions = 1
            };
            coupon = Client.CreateCoupon(coupon);
            coupon.CreatedAt.Should().NotBe(default(DateTime));
        }

        ///// <summary>
        ///// This test isn't constructed as expected, because the service apparently marks expired or maxed
        ///// out coupons as "Inactive" rather than "MaxedOut" or "Expired".
        ///// </summary>
        //[Fact]
        //public void ListCouponsExpired()
        //{
        //    var coupon = new Coupon(GetMockCouponCode(), GetMockCouponName("Expired test"), 10)
        //    {
        //        MaxRedemptions = 1
        //    };
        //    coupon.Create();
        //    coupon.CreatedAt.Should().NotBe(default(DateTime));

        //    var account = CreateNewAccountWithBillingInfo();

        //    var redemption = account.RedeemCoupon(coupon.CouponCode, "USD");
        //    redemption.CreatedAt.Should().NotBe(default(DateTime));

        //    var fromService = Coupons.Get(coupon.CouponCode);
        //    fromService.Should().NotBeNull();

        //    var expiredCoupons = Coupons.List(Coupon.CouponState.Expired);
        //    expiredCoupons.Should().NotContain(coupon,
        //            "the Recurly service marks this expired coupon as \"Inactive\", which cannot be searched for.");
        //}

        ///// <summary>
        ///// This test isn't constructed as expected, because the service apparently marks expired or maxed
        ///// out coupons as "Inactive" rather than "MaxedOut" or "Expired".
        ///// </summary>
        //[Fact]
        //public void ListCouponsMaxedOut()
        //{
        //    var coupon = new Coupon(GetMockCouponCode(), GetMockCouponName("Maxed Out test"), 10)
        //    {
        //        MaxRedemptions = 1
        //    };
        //    coupon.Create();
        //    coupon.CreatedAt.Should().NotBe(default(DateTime));

        //    var account = CreateNewAccountWithBillingInfo();

        //    var redemption = account.RedeemCoupon(coupon.CouponCode, "USD");
        //    redemption.CreatedAt.Should().NotBe(default(DateTime));

        //    var fromService = Coupons.Get(coupon.CouponCode);
        //    fromService.Should().NotBeNull();

        //    var expiredCoupons = Coupons.List(Coupon.CouponState.Expired);
        //    expiredCoupons.Should().NotContain(coupon,
        //            "the Recurly service marks this expired coupon as \"Inactive\", which cannot be searched for.");
        //}

        [Fact]
        public void CreateCouponPercent()
        {
            var coupon = new RecurlyCoupon(GetMockCouponCode(), GetMockCouponName(), 10);
            coupon = Client.CreateCoupon(coupon);

            coupon.CreatedAt.Should().NotBe(default(DateTime));

            coupon = Client.GetCoupon(coupon.CouponCode);

            coupon.Should().NotBeNull();
            coupon.DiscountPercent.Should().Be(10);
            coupon.DiscountType.Should().Be(RecurlyCoupon.CouponDiscountType.Percent);
        }

        [Fact]
        public void CreateCouponDollars()
        {
            var discounts = new RecurlyCurrency { USD = 100, EUR = 50 };
            var coupon = new RecurlyCoupon(GetMockCouponCode(), GetMockCouponName(), discounts);

            coupon = Client.CreateCoupon(coupon);
            coupon.CreatedAt.Should().NotBe(default(DateTime));

            coupon = Client.GetCoupon(coupon.CouponCode);

            coupon.Should().NotBeNull();
            coupon.DiscountInCents.USD.Should().Equals(discounts.USD);
            coupon.DiscountInCents.EUR.Should().Equals(discounts.EUR);
            coupon.DiscountType.Should().Be(RecurlyCoupon.CouponDiscountType.Dollars);
        }

        //[Fact]
        //public void CreateCouponPlan()
        //{
        //    var plan = new Plan(GetMockPlanCode("coupon plan"), "Coupon Test");
        //    plan.SetupFeeInCents.Add("USD", 500);
        //    plan.UnitAmountInCents.Add("USD", 5000);
        //    plan.Create();
        //    PlansToDeactivateOnDispose.Add(plan);

        //    var coupon = new Coupon(GetMockCouponCode(), GetMockCouponName(), new Dictionary<string, int>());
        //    coupon.DiscountInCents.Add("USD", 100);
        //    coupon.Plans.Add(plan.PlanCode);

        //    Action a = coupon.Create;
        //    a.ShouldNotThrow();
        //    Assert.Equal(1, coupon.Plans.Count);

        //    //plan.Deactivate(); BaseTest.Dispose() handles this
        //}

        //[Fact]
        //public void Coupon_plan_must_exist()
        //{
        //    var coupon = new Coupon(GetMockCouponCode(), GetMockCouponName(), 10);
        //    coupon.Plans.Add("notrealplan");

        //    Action create = coupon.Create;
        //    create.ShouldThrow<ValidationException>();
        //}

        [Fact]
        public void DeactivateCoupon()
        {
            var discounts = new RecurlyCurrency {USD = 100, EUR = 50};
            var coupon = new RecurlyCoupon(GetMockCouponCode(), GetMockCouponName(), discounts);
            coupon = Client.CreateCoupon(coupon);
            coupon.CreatedAt.Should().NotBe(default(DateTime));

            Client.DeactivateCoupon(coupon.CouponCode);

            coupon = Client.GetCoupon(coupon.CouponCode);
            coupon.Should().NotBeNull();
            coupon.State.Should().Be(RecurlyCoupon.CouponState.Inactive);
        }
    }
}
