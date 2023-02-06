using System.Collections.Generic;
using System.Threading.Tasks;
using Crowdfunding.Domain.ViewModels;

namespace Crowdfunding.Domain
{
    public interface IPaymentService
    {
        Task<IEnumerable<CauseViewModel>> RecoverInstitutionsAsync(int page = 0);
        Task AddDonationAsync(DonationViewModel donation);
    }
}
