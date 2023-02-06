using System.Collections.Generic;
using System.ComponentModel;

namespace Crowdfunding.Domain.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Donors = new List<DonorViewModel>();
            Institutions = new List<CauseViewModel>();
        }

        [DisplayName("How much is left to collect?")]
        public double TargetRemainingValue { get; set; }

        [DisplayName("How much did we raise?")]
        public double TotalValueCollected { get; set; }

        [DisplayName("Percentage raised")]
        public double PercentageTotalCollected { get; set; }

        [DisplayName("Number of Donors")]
        public int QuantityDonors { get; set; }

        [DisplayName("Days Remaining")]
        public int TimeRemainingDays { get; set; }

        [DisplayName("Hours left")]
        public int TimeRemainingHours { get; set; }

        [DisplayName("Remaining Minutes")]
        public int TimeRemainingMinutes { get; set; }

        public IEnumerable<DonorViewModel> Donors { get; set; }
        public IEnumerable<CauseViewModel> Institutions { get; set; }
    }
}
