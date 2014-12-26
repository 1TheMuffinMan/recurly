using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Contrib;

namespace Recurly
{
    public partial class RecurlyClient
    {
        public RecurlyBillingInfo GetBillingInfo(string accountCode)
        {
            var response = GetBillingInfoAsync(accountCode);
            return response.Result;
        }

        public Task<RecurlyBillingInfo> GetBillingInfoAsync(string accountCode)
        {
            var request = new RestRequest("{prefix}{accountCode}billing_info")
                .AddUrlSegment("prefix", RecurlyAccount.UrlPrefix)
                .AddUrlSegment("accountCode", accountCode);

            return Task<RecurlyBillingInfo>.Factory.StartNew(() =>
            {
                var response = _client.Execute<RecurlyBillingInfo>(request);
                CheckForError(response);
                return response.Data;
            }, TaskCreationOptions.AttachedToParent);
        }
    }
}
