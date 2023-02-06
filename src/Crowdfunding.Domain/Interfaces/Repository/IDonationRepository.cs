using System.Collections.Generic;
using System.Threading.Tasks;
using Crowdfunding.Domain.Entities;

namespace Crowdfunding.Domain
{
    public interface IDonationRepository
    {
        Task AddAsync(Donation model);
        Task<IEnumerable<Donation>> RecoverDonorsAsync(int pageIndex = 0);
    }
}
