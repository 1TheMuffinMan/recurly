using System;
using System.Collections.Generic;
using System.Linq;

namespace Recurly.Test
{
    public abstract class BaseTest : IDisposable
    {
        protected const string NullString = null;
        protected const string EmptyString = "";

        protected readonly List<RecurlyPlan> PlansToDeactivateOnDispose;
        public static RecurlyClient Client;

        protected BaseTest()
        {
            Client = new RecurlyClient();
            PlansToDeactivateOnDispose = new List<RecurlyPlan>();
        }

        protected RecurlyAccount CreateNewAccount()
        {
            var account = new RecurlyAccount(GetUniqueAccountCode());
            //
            return account;
        }

        protected RecurlyAccount CreateNewAccountWithBillingInfo()
        {
            var account = NewAccountWithBillingInfo();
            //account.Create();

            return account;
        }

        protected RecurlyAccount NewAccountWithBillingInfo()
        {
            var code = GetUniqueAccountCode();
            var account = new RecurlyAccount(code, NewBillingInfo(code));
            return account;
        }

        protected RecurlyCoupon CreateNewCoupon(int discountPercent)
        {
            var coupon = new RecurlyCoupon(GetMockCouponCode(), GetMockCouponName(), discountPercent);

            Client.CreateCoupon(coupon);
            return coupon;
        }

        public string GetUniqueAccountCode()
        {
            return Guid.NewGuid().ToString();
        }

        public string GetMockAccountName(string name = "Test Account")
        {
            return name + " " + DateTime.Now.ToString("yyyyMMddhhmmFFFFFFF");
        }

        public string GetMockCouponCode(string name = "cc")
        {
            return name + DateTime.Now.ToString("yyyyMMddhhmmFFFFFFF");
        }

        public string GetMockCouponName(string name = "Test Coupon")
        {
            return name + " " + DateTime.Now.ToString("yyyyMMddhhmmFFFFFFF");
        }

        public string GetMockPlanCode(string name = "pc")
        {
            return name.Replace(" ", "") + DateTime.Now.ToString("yyyyMMddhhmmFFFFFFF");
        }

        public string GetMockPlanName(string name = "Test Plan")
        {
            return name + " " + DateTime.Now.ToString("yyyyMMddhhmmFFFFFFF");
        }

        public RecurlyBillingInfo NewBillingInfo(RecurlyAccount account)
        {
            var billingInfo = new RecurlyBillingInfo(account.AccountCode)
            {
                FirstName = account.FirstName,
                LastName = account.LastName,
                Address1 = "123 Test St",
                City = "San Francisco",
                State = "CA",
                Country = "US",
                PostalCode = "94105",
                ExpirationMonth = DateTime.Now.Month,
                ExpirationYear = DateTime.Now.Year + 1,
                CreditCardNumber = TestCreditCardNumbers.Visa1,
                VerificationValue = "123"
            };
            return billingInfo;
        }

        public RecurlyBillingInfo NewBillingInfo(string accountCode)
        {
            return new RecurlyBillingInfo(accountCode)
            {
                FirstName = "John",
                LastName = "Smith",
                Address1 = "123 Test St",
                City = "San Francisco",
                State = "CA",
                Country = "US",
                PostalCode = "94105",
                ExpirationMonth = DateTime.Now.Month,
                ExpirationYear = DateTime.Now.Year + 1,
                CreditCardNumber = TestCreditCardNumbers.Visa1,
                VerificationValue = "123"
            };
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (!PlansToDeactivateOnDispose.Any()) return;

            foreach (var plan in PlansToDeactivateOnDispose)
            {
                try
                {
                  //  plan.Deactivate();
                }
                catch (RecurlyException)
                {
                }
            }
        }
    }
}
