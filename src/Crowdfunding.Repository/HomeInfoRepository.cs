using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Crowdfunding.Domain;
using Crowdfunding.Domain.ViewModels;
using Crowdfunding.Repository.Context;

namespace Crowdfunding.Repository
{
    public class HomeInfoRepository : IHomeInfoRepository
    {
        private readonly CrowdfundingOnlineDBContext _dbContext;

        public HomeInfoRepository(CrowdfundingOnlineDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HomeViewModel> RetrieveInitialDataHomeAsync()
        {
            var totalDonors = _dbContext.Donations.CountAsync();
            var amount = _dbContext.Donations.SumAsync(a => a.Value);

            return new HomeViewModel
            {
                TotalValueCollected = await amount,
                QuantityDonors = await totalDonors
            };
        }
    }
}