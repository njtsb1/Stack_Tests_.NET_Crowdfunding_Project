using System.Threading.Tasks;
using Crowdfunding.Domain.ViewModels;

namespace Crowdfunding.Domain
{
    public interface IHomeInfoRepository
    {
        Task<HomeViewModel> RetrieveInitialDataHomeAsync();
    }
}