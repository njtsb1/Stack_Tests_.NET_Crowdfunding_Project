using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Crowdfunding.Domain;
using Crowdfunding.Repository;
using Crowdfunding.Repository.Context;
using Crowdfunding.Service;
using Crowdfunding.Service.AutoMapper;

namespace Crowdfunding.MVC.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<CrowdfundingOnlineDBContext>(opt => opt.UseInMemoryDatabase("CrowdfundingOnLineDIO"));

            return services;
        }

        public static IServiceCollection AddIocConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICauseService, CauseService>();
            services.AddScoped<IHomeInfoService, HomeInfoService>();

            services.AddScoped<IDomainNotificationService, DomainNotificationService>();
            services.AddScoped<IDonationService, DonationService>();
            services.AddScoped<IDonationRepository, DonationRepository>();

            services.AddScoped<ICauseRepository, CauseRepository>();
            services.AddScoped<IHomeInfoRepository, HomeInfoRepository>();

            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services, IConfiguration configuration)
        {
            var globalAppSettings = new GloballAppConfig();
            configuration.Bind("SettingsGeneralApplication", globalAppSettings);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CrowdfundingOnLineMappingProfile>();
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new GloballAppConfig();

            configuration.Bind("SettingsGeneralApplication", config);
            services.AddSingleton(config);

            return services;
        }
    }
}
