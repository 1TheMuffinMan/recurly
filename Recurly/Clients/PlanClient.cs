using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Serializers;

namespace Recurly
{
    public partial class RecurlyClient
    {
        public Task<RecurlyPagedSet<RecurlyPlan>> ListPlansAsync(string cursor, uint perPage = 50)
        {
            if (perPage > 200) throw new ArgumentOutOfRangeException("perPage", "perPage cannot exceed 200.");

            var request = new RestRequest(RecurlyPlan.UrlPrefix + "?per_page={perPage}");
            if (!string.IsNullOrWhiteSpace(cursor))
                request.Resource += string.Format("&cursor={0}", cursor);

            return Task<RecurlyPagedSet<RecurlyPlan>>.Factory.StartNew(() =>
            {
                var response = Execute<List<RecurlyPlan>>(request);

                var h = response.Headers.Cast<Parameter>().SingleOrDefault(x => x.Name == "X-Records");
                return new RecurlyPagedSet<RecurlyPlan>(response.Data, (int)h.Value, null);
            });
        }

        public Task<RecurlyPlan> CreatePlanAsync(RecurlyPlan plan)
        {
            var request = new RestRequest(RecurlyPlan.UrlPrefix);
            request.XmlSerializer = new DotNetXmlSerializer("www.contoso.com");

            return ExecuteAsync<RecurlyPlan>(request);
        }

        public RecurlyPlan CreatePlan(RecurlyPlan plan)
        {
            return CreatePlanAsync(plan).Result;
        }
    }
}
