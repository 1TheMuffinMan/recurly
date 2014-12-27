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
        public void ListPlans()
        {
            for (int i = 0; i < 10; i++)
            {
                var plan = new RecurlyPlan(GetMockPlanCode(), GetMockPlanName()) { Description = "Test Lookup" + i };
                plan.UnitAmountInCents.USD = 100;
                plan.TaxExempt = true;
                plan = Client.CreatePlan(plan);
                PlansToDeactivateOnDispose.Add(plan);
            }

            var plans = Client.ListPlans(perPage: 5);
            plans.Items.Count.Should().Be(5);
            plans.TotalItems.Should().BeGreaterOrEqualTo(10);
        }

        [Fact]
        public void LookupPlan()
        {
            var plan = new RecurlyPlan(GetMockPlanCode(), GetMockPlanName()) { Description = "Test Lookup" };
            plan.UnitAmountInCents.USD = 100;
            plan.TaxExempt = true;
            plan = Client.CreatePlan(plan);
            PlansToDeactivateOnDispose.Add(plan);

            plan.CreatedAt.Should().NotBe(default(DateTime));

            var fromService = Client.GetPlan(plan.PlanCode);
            fromService.PlanCode.Should().Be(plan.PlanCode);
            fromService.UnitAmountInCents.USD.Should().Be(100);
            fromService.Description.Should().Be("Test Lookup");
            Assert.True(plan.TaxExempt.GetValueOrDefault());
        }

        [Fact]
        public void CreatePlanSmall()
        {
            var plan = new RecurlyPlan(GetMockPlanCode(), GetMockPlanName());
            plan.UnitAmountInCents.USD = 100;
            plan = Client.CreatePlan(plan);
            PlansToDeactivateOnDispose.Add(plan);

            plan.CreatedAt.Should().NotBe(default(DateTime));
            plan.UnitAmountInCents.USD = 100;
        }

        [Fact]
        public void CreatePlan()
        {
            var plan = new RecurlyPlan(GetMockPlanCode(), GetMockPlanName())
            {
                AccountingCode = "accountingcode123",
                Description = "a test plan",
                DisplayDonationAmounts = true,
                DisplayPhoneNumber = false,
                DisplayQuantity = true,
                TotalBillingCycles = 5,
                TrialIntervalUnit = RecurlyPlan.IntervalUnit.Months,
                TrialIntervalLength = 1,
                PlanIntervalUnit = RecurlyPlan.IntervalUnit.Days,
                PlanIntervalLength = 180
            };
            plan.SetupFeeInCents.USD = 500;
            plan = Client.CreatePlan(plan);
            PlansToDeactivateOnDispose.Add(plan);

            plan.CreatedAt.Should().NotBe(default(DateTime));
            plan.AccountingCode.Should().Be("accountingcode123");
            plan.Description.Should().Be("a test plan");
            plan.DisplayDonationAmounts.Should().HaveValue().And.Be(true);
            plan.DisplayPhoneNumber.Should().HaveValue().And.Be(false);
            plan.DisplayQuantity.Should().HaveValue().And.Be(true);
            plan.TotalBillingCycles.Should().Be(5);
            plan.TrialIntervalUnit.Should().Be(RecurlyPlan.IntervalUnit.Months);
            plan.TrialIntervalLength.Should().Be(1);
            plan.PlanIntervalUnit.Should().Be(RecurlyPlan.IntervalUnit.Days);
            plan.PlanIntervalLength.Should().Be(180);
        }

        [Fact]
        public void UpdatePlan()
        {
            var plan = new RecurlyPlan(GetMockPlanCode(), GetMockPlanName()) { Description = "Test Update" };
            plan.UnitAmountInCents.USD=100;
            plan = Client.CreatePlan(plan);
            PlansToDeactivateOnDispose.Add(plan);

            plan.UnitAmountInCents.USD = 5000;
            plan.SetupFeeInCents.USD = 100;
            plan.TaxExempt = false;

            plan = Client.UpdatePlan(plan);
            plan.UnitAmountInCents.USD.Should().Be(5000);
            plan.SetupFeeInCents.USD.Should().Be(100);
            Assert.False(plan.TaxExempt.Value);
        }

        [Fact]
        public void DeactivatePlan()
        {
            // Arrange
            var plan = new RecurlyPlan(GetMockPlanCode(), GetMockPlanName()) { Description = "Test Delete" };
            plan.UnitAmountInCents.USD = 100;
            plan = Client.CreatePlan(plan);
            PlansToDeactivateOnDispose.Add(plan);

            plan.CreatedAt.Should().NotBe(default(DateTime));
            plan.UnitAmountInCents.USD.Should().Be(100);

            //Act
            Client.DeactivatePlan(plan.PlanCode);

            //Assert
            Action get = () => Client.GetPlan(plan.PlanCode);
            get.ShouldThrow<RecurlyNotFoundException>();
        }
    }
}
