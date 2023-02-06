using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Crowdfunding.Domain;
using Crowdfunding.Domain.Entities;
using Crowdfunding.Repository.Context;

namespace Crowdfunding.Repository
{
    public class DonationRepository : IDonationRepository
    {
        private readonly ILogger<DonationRepository> _logger;
        private readonly GloballAppConfig _globalSettings;
        private readonly CrowdfundingOnlineDBContext _CrowdfundingOnlineDBContext;

        public DonationRepository(GloballAppConfig globalSettings,
                                CrowdfundingOnlineDBContext CrowdfundingDbContext,
                                ILogger<DonationRepository> logger)
        {
            _globalSettings = globalSettings;
            _CrowdfundingOnlineDBContext = CrowdfundingDbContext;
            _logger = logger;
        }

        public async Task AddAsync(Donation model)
        {
            await _CrowdfundingOnlineDBContext.Donations.AddAsync(model);
            await _CrowdfundingOnlineDBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Donation>> RetrieveDonationsAsync(int pageIndex = 0)
        {
            return await _CrowdfundingOnlineDBContext.Donations.Include("Personaldata").ToListAsync();
        }
    }
}