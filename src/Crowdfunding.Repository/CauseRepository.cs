using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Crowdfunding.Domain;
using Crowdfunding.Domain.Entities;
using Crowdfunding.Repository.Context;

namespace Crowdfunding.Repository
{
    public class CauseRepository : ICauseRepository
    {
        private readonly CrowdfundingOnlineDBContext _dbContext;

        public CauseRepository(CrowdfundingOnlineDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Cause> Add(Cause cause)
        {
            await _dbContext.AddAsync(cause);
            await _dbContext.SaveChangesAsync();

            return cause;
        }

        public async Task<IEnumerable<Cause>> RecoverCauses()
        {
            return await _dbContext.Causes.ToListAsync();
        }
    }
}