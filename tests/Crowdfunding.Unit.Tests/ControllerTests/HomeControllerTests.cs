using Microsoft.Extensions.Logging;
using Moq;
using Crowdfunding.MVC.Controllers;
using Crowdfunding.Domain;

namespace Crowdfunding.Unit.Tests.ControllerTests
{
    public class HomeControllerTests
    {
        private readonly IHomeInfoService _homeInfoService;
        private readonly Mock<ILogger<HomeController>> _logger;

        public HomeControllerTests()
        {

        }
    }
}
