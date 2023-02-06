using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Crowdfunding.Domain;
using Crowdfunding.Domain.Entities;
using Crowdfunding.Domain.ViewModels;

namespace Crowdfunding.Service
{
    public class HomeInfoService : IHomeInfoService
    {
        private readonly IMapper _mapper;
        private readonly IDonationService _donationService;
        private readonly GloballAppConfig _globalSettings;
        private readonly IHomeInfoRepository _homeRepository;
        private readonly ICauseRepository _causeRepository;

        public HomeInfoService(IMapper mapper,
                               IDonationService donationService,
                               GloballAppConfig globalSettings,
                               IHomeInfoRepository homeRepository,
                               ICauseRepository causeRepotirory)
        {
            _mapper = mapper;
            _donationService = donationService;
            _homeRepository = homeRepository;
            _globalSettings = globalSettings;
            _causeRepository = causeRepotirory;
        }

        public async Task<HomeViewModel> RetrieveInitialDataHomeAsync()
        {
            var initialdataHome = await RetrieveTotalDataHome();

            var institutions = RetrieveCausesAsync();
            var donations = RetrieveDonorsAsync();

            var diffDate = _globalSettings.DateEndCampaign.Subtract(DateTime.Now);

            initialdataHome.TimeleftDays = diffDate.Days;
            initialdataHome.TimeleftHours = diffDate.Hours;
            initialdataHome.TimeleftMinutes = diffDate.Minutes;

            initialdataHome.TargetRemainingValue = _globalSettings.MetaCampaign - initialdataHome.TotalValueCollected;
            initialdataHome.PercentageTotalCollected = initialdataHome.TotalValueCollected * 100 / _globalSettings.MetaCampaign;

            await Task.WhenAll();
            initialdataHome.Donors = await donations;
            initialdataHome.Institutions = await institutions;

            return initialdataHome;
        }

        private async Task<HomeViewModel> RetrieveTotalDataHome()
        {
            return await _homeRepository.RetrieveinitialdataHomeAsync();
        }

        public async Task<IEnumerable<CauseViewModel>> RetrieveCausesAsync()
        {   
            var causes = await _causeRepository.RetrieveCauses();
            return _mapper.Map<IEnumerable<Cause>, IEnumerable<CauseViewModel>>(causes);
        }

        private async Task<IEnumerable<DonorViewModel>> RetrieveDonorsAsync()
        {
            return await _donationService.RetrieveDonorsAsync();
        }
    }
}