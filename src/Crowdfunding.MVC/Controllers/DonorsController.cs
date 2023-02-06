using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Crowdfunding.Domain;

namespace Crowdfunding.MVC.Controllers
{
    public class DonorsController : Controller
    {
        private readonly IDonationService _donationService;

        public DonorsController(IDonationService donationService)
        {
            _donationService = donationService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _donationService.RecoverDonorsAsync());
        }
    }
}
