using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Recurly.Test
{
    public class PlanTest : BaseTest
    {
        [Fact]
        public void CreatePlanSmall()
        {
            var plan = new RecurlyPlan(GetMockPlanCode(), GetMockPlanName());
            plan.SetupFeeInCents.Add("USD", 100);
            plan = Client.CreatePlan(plan);
            PlansToDeactivateOnDispose.Add(plan);

            plan.CreatedAt.Should().NotBe(default(DateTime));
            plan.SetupFeeInCents.Should().Contain("USD", 100);
        }
    }
}
