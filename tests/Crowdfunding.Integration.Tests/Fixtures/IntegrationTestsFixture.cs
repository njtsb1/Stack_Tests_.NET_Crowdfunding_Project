using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;
using Crowdfunding.Domain;
using Crowdfunding.Integration.Tests.Config;
using Crowdfunding.MVC;
using Xunit;

namespace Crowdfunding.Integration.Tests.Fixtures
{
    [CollectionDefinition(nameof(IntegrationWebTestsFixtureCollection))]
    public class IntegrationWebTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupWebTests>>
    {
    }

    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public HttpClient Client;
        public IConfigurationRoot Configuration;
        public GloballAppConfig GeneralConfigurationApplication;
        public readonly CrowdfundingAppFactory<TStartup> Factory;

        public IntegrationTestsFixture()
        {
            var clientOption = new WebApplicationFactoryClientOptions
            {
            };

            Factory = new CrowdfundingAppFactory<TStartup>();
            Client = Factory.CreateClient(clientOption);
            Configuration = GetConfiguration();

            GeneralConfigurationApplication = BuildGlobalAppConfiguration();
        }

        private GloballAppConfig BuildGlobalAppConfiguration()
        {   
            var globalAppSettings = new GloballAppConfig();
            Configuration.Bind("SettingsGeneralApplication", globalAppSettings);

            return globalAppSettings;
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }

        private IConfigurationRoot GetConfiguration()
        {
            var workingDir = Directory.GetCurrentDirectory();

            return new ConfigurationBuilder()
                      .SetBasePath(workingDir)
                      .AddJsonFile("appsettings.json")
                      .AddJsonFile("appsettings.Testing.json")
                      .Build();
        }
    }
}
