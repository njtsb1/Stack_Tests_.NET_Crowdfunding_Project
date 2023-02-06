using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Crowdfunding.Domain;
using Crowdfunding.Domain.Entities;
using Crowdfunding.Domain.ViewModels;

namespace Crowdfunding.Service
{
    public class CauseService : ICauseService
    {
        private readonly IMapper _mapper;
        private readonly ICauseRepository _causeRepository;

        public CauseService(ICauseRepository causeRepository,
                            IMapper mapper)
        {
            _mapper = mapper;
            _causeRepository = causeRepository;
        }

        public async Task Add(CauseViewModel model)
        {
            var cause = _mapper.Map<CauseViewModel, Cause>(model);
            await _causeRepository.Add(cause);
        }

        public async Task<IEnumerable<CauseViewModel>> RecoverCauses()
        {
            var causes = await _causeRepository.RecoverCauses();
            return _mapper.Map<IEnumerable<Cause>, IEnumerable<CauseViewModel>>(causes);
        }
    }
}