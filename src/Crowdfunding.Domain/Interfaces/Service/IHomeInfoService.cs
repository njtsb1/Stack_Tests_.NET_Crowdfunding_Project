using System.Collections.Generic;
using System.Threading.Tasks;
using Crowdfunding.Domain.ViewModels;

namespace Crowdfunding.Domain
{
    public interface IHomeInfoService
    {
        Task<HomeViewModel> RetrieveInitialDataHomeAsync();        
        Task<IEnumerable<CausaViewModel>> RecoverCausesAsync();
    }
}
