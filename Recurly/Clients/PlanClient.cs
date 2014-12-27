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
        /// <summary>
        /// Lists all your active subscription plans.
        /// </summary>
        /// <param name="cursor">Splits records across pages. Leave blank to return the first page. 
        /// Supply the URI in the first page's Link property to fetch the next page.</param>
        /// <param name="perPage">Number of records to return per page, up to a maximum of 200. Defaults to 50</param>
        public Task<RecurlyPagedSet<RecurlyPlan>> ListPlansAsync(string cursor = null, uint perPage = 50)
        {
            if (perPage > 200) throw new ArgumentOutOfRangeException("perPage", "perPage cannot exceed 200.");

            var request = new RestRequest(RecurlyPlan.UrlPrefix + "?per_page={perPage}")
                .AddUrlSegment("perPage", perPage.ToString());

            if (!string.IsNullOrWhiteSpace(cursor))
                request.Resource += string.Format("&cursor={0}", cursor);

            return Task<RecurlyPagedSet<RecurlyPlan>>.Factory.StartNew(() =>
            {
                var response = Execute<List<RecurlyPlan>>(request);

                var h = response.Headers.Cast<Parameter>().SingleOrDefault(x => x.Name == "X-Records");
                return new RecurlyPagedSet<RecurlyPlan>(response.Data, Convert.ToInt32(h.Value), null);
            });
        }

        /// <summary>
        /// Lists all your active subscription plans.
        /// </summary>
        /// <param name="cursor">Splits records across pages. Leave blank to return the first page. 
        /// Supply the URI in the first page's Link property to fetch the next page.</param>
        /// <param name="perPage">Number of records to return per page, up to a maximum of 200. Defaults to 50</param>
        public RecurlyPagedSet<RecurlyPlan> ListPlans(string cursor = null, uint perPage = 50)
        {
            return ListPlansAsync(cursor, perPage).Result;
        }

        /// <summary>
        /// Lookup a plan's details.
        /// </summary>
        /// <param name="planCode">The unique plan code.</param>
        public Task<RecurlyPlan> GetPlanAsync(string planCode)
        {
            var request = new RestRequest(RecurlyPlan.UrlPrefix + "{planCode}")
                .AddUrlSegment("planCode", planCode);

            return ExecuteAsync<RecurlyPlan>(request);
        }

        /// <summary>
        /// Lookup a plan's details.
        /// </summary>
        /// <param name="planCode">The unique plan code.</param>
        public RecurlyPlan GetPlan(string planCode)
        {
            return GetPlanAsync(planCode).Result;
        }

        /// <summary>
        /// Create a new subscription plan.
        /// </summary>
        public Task<RecurlyPlan> CreatePlanAsync(RecurlyPlan plan)
        {
            var request = new RestRequest(RecurlyPlan.UrlPrefix, Method.POST);
            request.XmlSerializer = new DotNetXmlSerializer("www.contoso.com");
            request.AddXmlBody(plan);

            return ExecuteAsync<RecurlyPlan>(request);
        }

        /// <summary>
        /// Create a new subscription plan.
        /// </summary>
        public RecurlyPlan CreatePlan(RecurlyPlan plan)
        {
            return CreatePlanAsync(plan).Result;
        }

        /// <summary>
        /// Update the pricing or details for a plan. Existing subscriptions will remain at the previous renewal amounts.
        /// </summary>
        public Task<RecurlyPlan> UpdatePlanAsync(RecurlyPlan plan)
        {
            var request = new RestRequest(RecurlyPlan.UrlPrefix + "{planCode}", Method.PUT)
                .AddUrlSegment("planCode", plan.PlanCode);
            request.XmlSerializer = new DotNetXmlSerializer("www.contoso.com");
            request.AddXmlBody(plan);

            return ExecuteAsync<RecurlyPlan>(request);
        }

        /// <summary>
        /// Update the pricing or details for a plan. Existing subscriptions will remain at the previous renewal amounts.
        /// </summary>
        public RecurlyPlan UpdatePlan(RecurlyPlan plan)
        {
            return UpdatePlanAsync(plan).Result;
        }

        /// <summary>
        /// Deleting a plan makes it inactive. New accounts cannot be created on the plan.
        /// </summary>
        /// <param name="planCode">The unique plan code.</param>
        public Task DeactivatePlanAsync(string planCode)
        {
            var request = new RestRequest(RecurlyPlan.UrlPrefix + "{planCode}", Method.DELETE)
                .AddUrlSegment("planCode", planCode);

            return ExecuteAsync(request);
        }

        /// <summary>
        /// Deleting a plan makes it inactive. New accounts cannot be created on the plan.
        /// </summary>
        /// <param name="planCode">The unique plan code.</param>
        public void DeactivatePlan(string planCode)
        {
            DeactivatePlanAsync(planCode).Wait();
        }
    }
}
