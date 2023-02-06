using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Threading.Tasks;
using Crowdfunding.Domain;
using Crowdfunding.Domain.ViewModels;

namespace Crowdfunding.MVC.Controllers
{
    public class DonationsController : BaseController
    {
        private readonly IDonationsService _donationsService;
        private readonly IDomainNotificationService _domainNotificationService;

        public DonationsController(IDonationsService donationsService,
                                 IDomainNotificationService domainNotificationService,
                                 IToastNotification toastNotification) : base(domainNotificationService, toastNotification)
        {
            _donationsService = donationsService;
            _domainNotificationService = domainNotificationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(nameof(Index), await _donationsService.RecoverDonorsAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DonationsViewModel model)
        {
            _donationsService.MakeDonationsAsync(model);

            if (HasErrorsDominio())
            {
                AddErrosDominio();
                return View(model);
            }

            AddNotificationOperationPerformedWithSuccess("Donation made successfully!<p>Thanks for supporting our cause :)</p>");
            return RedirectToAction("Index", "Home");
        }

        private bool HasErrorsDominio()
        {
            return _domainNotificationService.HasErrors;
        }
    }
}
