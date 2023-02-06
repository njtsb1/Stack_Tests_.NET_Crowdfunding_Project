using FluentAssertions;
using System.Threading.Tasks;
using Crowdfunding.Domain.Extensions;
using Crowdfunding.Integration.Tests.Fixtures;
using Crowdfunding.MVC;
using Xunit;

namespace Crowdfunding.Integration.Tests
{
    [Collection(nameof(IntegrationWebTestsFixtureCollection))]
    public class HomeTests
    {
        private readonly IntegrationTestsFixture<StartupWebTests> _integrationTestsFixture;

        public HomeTests(IntegrationTestsFixture<StartupWebTests> integrationTestsFixture)
        {
            _integrationTestsFixture = integrationTestsFixture;
        }

        [Trait("HomeControllerIntegrationTests", "HomeController_LoadInitialPage_TotalDonorsAndTotalAmountRaisedMustBeZero")]
        [Fact]
        public async Task HomeController_LoadInitialPage_TotalDonorsAndTotalAmountRaisedMustBeZero()
        {
            // Arrange & Act
            var home = await _integrationTestsFixture.Client.GetAsync("Home");

            // Assert
            home.EnsureSuccessStatusCode();
            var dataHome = await home.Content.ReadAsStringAsync();

            var totalcollected = 0.ToMoneyBrString();
            var metaCampaign = _integrationTestsFixture.GeneralConfigurationApplication.MetaCampaign.ToMoneyBrString();

            // Donation total data
            dataHome.Should().Contain(expected: "How much did we raise?");
            dataHome.Should().Contain(expected: totalcollected);

            dataHome.Should().Contain(expected: "How much is left to collect?");
            dataHome.Should().Contain(expected: metaCampaign);
        }
    }
}