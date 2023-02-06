using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using NToastNotify;
using Crowdfunding.Domain;
using Crowdfunding.Domain.Entities;
using Crowdfunding.Domain.ViewModels;
using Crowdfunding.MVC.Controllers;
using Crowdfunding.Service;
using Crowdfunding.Tests.Common.Fixtures;
using Xunit;

namespace Crowdfunding.Unit.Tests.ControllerTests
{
    [Collection(nameof(DonationFixtureCollection))]
    public class DonationControllerTests : IClassFixture<DonationFixture>,
                                        IClassFixture<EddressFixture>,
                                        IClassFixture<CardCreditFixture>
    {
        private readonly Mock<IDonationRepository> _donationRepository = new Mock<IDonationRepository>();        
        private readonly Mock<GloballAppConfig> _globallAppConfig = new Mock<GloballAppConfig>();

        private readonly DonationFixture _donationFixture;
        private readonly AddressFixture _eddressFixture;
        private readonly CardCreditFixture _cardCreditFixture;

        private DonationsController _donationController;
        private readonly IDonationService _donationService;

        private Mock<IMapper> _mapper;
        private Mock<IPaymentService> _polenService = new Mock<IPaymentService>();
        private Mock<ILogger<DonationsController>> _logger = new Mock<ILogger<DonationsController>>();

        private IDomainNotificationService _domainNotificationService = new DomainNotificationService();

        private Mock<IToastNotification> _toastNotification = new Mock<IToastNotification>();

        private readonly Donation _donationValid;
        private readonly DonationViewModel _donationModelValid;

        public DonationControllerTests(DonationFixture donationFixture, AddressFixture addressFixture, CardCreditFixture cardCreditFixture)
        {
            _donationFixture = donationFixture;
            _addressFixture = addressFixture;
            _cardCreditFixture = cardCreditFixture;

            _mapper = new Mock<IMapper>();

            _donationValid = donationFixture.DonationValid();
            _donationValid.AddAddressBilling(addressFixture.AddressValid());
            _donationValid.AddPaymentMethod(cardCreditFixture.CardCreditValid());

            _donationModelValid = donationFixture.DonationModelValid();
            _donationModelValid.AddressBilling = addressFixture.AddressModelValid();
            _donationModelValid.PaymentMethod = cardCreditFixture.CardCreditModelValid();

            _mapper.Setup(a => a.Map<DonationViewModel, Donation>(_donationModelValid)).Returns(_donationValid);

            _donationService = new DonationService(_mapper.Object, _donationRepository.Object, _domainNotificationService);
        }

        #region HTTPPOST

        [Trait("DonationController", "DonationController_Add_ReturnDataWithSuccess")]
        [Fact]
        public void DonationController_Add_ReturnDataWithSuccess()
        {
            // Arrange            
            _donationController = new DonationsController(_donationService, _domainNotificationService, _toastNotification.Object);

            // Act
            var return = _donationController.Create(_donationModelValid);

            _mapper.Verify(a => a.Map<DonationViewModel, Donation>(_donationModelValid), Times.Once);
            _toastNotification.Verify(a => a.AddSuccessToastMessage(It.IsAny<string>(), It.IsAny<LibraryOptions>()), Times.Once);

            return.Should().BeOfType<RedirectToActionResult>();

            ((RedirectToActionResult)return).ActionName.Should().Be("Index");
            ((RedirectToActionResult)return).ControllerName.Should().Be("Home");
        }

        [Trait("DonationController", "DonationController_AddDataInvalids_BadRequest")]
        [Fact]
        public void DonationController_AddDataInvalids_BadRequest()
        {
            // Arrange          
            var donation = _donationFixture.DonationInvalid();
            var donationModelInvalid = new DonationViewModel();
            _mapper.Setup(a => a.Map<DonationViewModel, Donation>(donationModelInvalid)).Returns(donation);

            _donationController = new DonationsController(_donationService, _domainNotificationService, _toastNotification.Object);

            // Act
            var return = _donationController.Create(donationModelInvalid);

            // Assert                   
            return.Should().BeOfType<ViewResult>();

            _mapper.Verify(a => a.Map<DonationViewModel, Donation>(donationModelInvalid), Times.Once);
            _donationRepository.Verify(a => a.AddAsync(donation), Times.Never);
            _toastNotification.Verify(a => a.AddErrorToastMessage(It.IsAny<string>(), It.IsAny<LibraryOptions>()), Times.Once);

            var viewResult = ((ViewResult)return);

            viewResult.Model.Should().BeOfType<DonationViewModel>();

            ((DonationViewModel)viewResult.Model).Should().Be(donationModelInvalid);
        }

        #endregion
    }
}

