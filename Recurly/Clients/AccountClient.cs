using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace Recurly
{
    public partial class RecurlyClient
    {
        public RecurlyAccount GetAccount(string accountCode, bool includeBillingInfo = false)
        {
            var task = GetAccountAsync(accountCode, includeBillingInfo);
            task.Wait();
            return task.Result;
        }

        public Task<RecurlyAccount> GetAccountAsync(string accountCode, bool includeBillingInfo = false)
        {
            if (string.IsNullOrWhiteSpace(accountCode)) throw new ArgumentNullException("accountCode");

            var request = new RestRequest(RecurlyAccount.UrlPrefix + "{accountCode}")
                .AddUrlSegment("accountCode", accountCode);

            return Task.Factory.StartNew(() =>
            {
                var account = ExecuteAsync<RecurlyAccount>(request);
                if (includeBillingInfo)
                {
                   var billingTask = GetBillingInfoAsync(accountCode);
                    account.Result.BillingInfo = billingTask.Result;
                }

                return account.Result;
            });

        }
    }
}
