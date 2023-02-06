using System.Collections.Generic;
using System.Threading.Tasks;
using Crowdfunding.Domain.Entities;

namespace Crowdfunding.Domain
{
    public interface ICauseRepository
    {
        Task<Cause> Add(Cause cause);
        Task<IEnumerable<Cause>> RecoverCauses();
    }
}
