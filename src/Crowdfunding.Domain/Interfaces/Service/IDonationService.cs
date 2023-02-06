using System.Collections.Generic;
using System.Threading.Tasks;
using Crowdfunding.Domain.ViewModels;

namespace Crowdfunding.Domain
{
    public interface IDonationService
    {
        Task MakeDonationAsync(DonationViewModel model);
        Task<IEnumerable<DonorViewModel>> RecoverDonorsAsync(int pageIndex = 0);
    }
}
