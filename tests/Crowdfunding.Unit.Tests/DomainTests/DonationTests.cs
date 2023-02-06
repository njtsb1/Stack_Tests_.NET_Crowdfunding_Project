using FluentAssertions;
using Xunit;
using Crowdfunding.Tests.Common.Fixtures;

namespace Crowdfunding.Unit.Tests.DomainTests
{
    [Collection(nameof(DonationFixtureCollection))]    
    public class DonationTests: IClassFixture<DonationFixture>, 
                              IClassFixture<AddressFixture>, 
                              IClassFixture<CardCreditFixture>
    {
        private readonly DonationFixture _donationFixture;
        private readonly AddressFixture _addressFixture;
        private readonly CardCreditFixture _cardCreditFixture;

        public DonationTests(DonationFixture donationFixture, AddressFixture eddressFixture, CardCreditFixture cardCreditFixture)
        {
            _donationFixture = donationFixture;
            _addressFixture = addressFixture;
            _cardCreditFixture = cardCreditFixture;
        }

        [Fact]
        [Trait("Donation", "Donation_CorrectlyFilledin_DonationValid")]
        public void Donation_CorrectlyFilledin_DonationValid()
        {           
            // Arrange
            var donation = _donationFixture.DonationValid();
            donation.AddAddressBilling(_addressFixture.AddressValid());
            donation.AddPaymentForm(_cardCreditFixture.CardCreditValid());

            // Act
            var valid = donation.Valid();

            // Assert
            valid.Should().BeTrue(because: "the fields have been filled in correctly");
            donation.ErrorMessages.Should().BeEmpty();
        }

        [Fact]
        [Trait("Donation", "Donation_PersonalDataInvalids_DonationInvalid")]
        public void Donation_PersonalDataInvalids_DonationInvalid()
        {
            // Arrange
            const bool EMAIL_INValid = true;
            var donation = _donationFixture.DonationValid(EMAIL_INValid);
            donation.AddAddressBilling(_addressFixture.AddressValid());
            donation.AddPaymentForm(_cardCreditFixture.CardCreditValid());

            // Act
            var valid = donation.Valid();

            // Assert
            valid.Should().BeFalse(because: "the email field is invalid");
            donation.ErrorMessages.Should().Contain("Email field is invalid.");
            donation.ErrorMessages.Should().HaveCount(1, because: "only the email field is invalid.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        [InlineData(-5)]
        [InlineData(-10.20)]
        [InlineData(-55.4)]
        [InlineData(-0.1)]
        [Trait("Donation", "Donation_ValuesDonationMinorEqualZero_DonationInvalid")]
        public void Donation_ValuesDonationMinorEqualZero_DonationInvalid(double valueDonation)
        {
            // Arrange            
            var donation = _donationFixture.DonationValid(false, valueDonation);
            donation.AddAddressBilling(_addressFixture.AddressValid());
            donation.AddPaymentForm(_cardCreditFixture.CardCreditValid());

            // Act
            var valid = donation.Valid();

            // Assert
            valid.Should().BeFalse(because: "the Value field is invalid");
            donation.ErrorMessages.Should().Contain("Minimum donation amount is $1.00");
            donation.ErrorMessages.Should().HaveCount(1, because: "only the Value field is invalid.");
        }

        [Theory]
        [InlineData(25000)]
        [InlineData(5500.50)]
        [InlineData(5000.1)]
        [InlineData(4505)]
        [InlineData(4500.1)]
        [Trait("Donation", "Donation_GreaterDonationValuesLimit_DonationInvalid")]
        public void Donation_GreaterDonationValuesLimit_DonationInvalid(double valueDonation)
        {
            // Arrange
            const bool EXCEDER_MAX_Value_Donation = true;
            var donation = _donationFixture.DonationValid(false, valueDonation);
            donation.AddAddressBilling(_addressFixture.AddressValid());
            donation.AddPaymentForm(_cardCreditFixture.CardCreditValid());

            // Act
            var valid = donation.Valid();

            // Assert
            valid.Should().BeFalse(because: "the Value field is invalid");
            donation.ErrorMessages.Should().Contain("Maximum amount for donation is $1,000.00");
            donation.ErrorMessages.Should().HaveCount(1, because: "only the Value field is invalid.");
        }

        [Fact]
        [Trait("Donation", "Donation_MessageSupportMaxlengthExecuted_DonationInvalid")]
        public void Donation_MessageSupportMaxlengthExecuted_DonationInvalid()
        {
            // Arrange
            const bool EXCEED_MAXIMUM_LENGTH_MESSAGE_SUPPORT = true;
            var donation = _donationFixture.DonationValid(false, null, EXCEED_MAXIMUM_LENGTH_MESSAGE_SUPPORT);
            donation.AddAddressBilling(_addressFixture.AddressValid());
            donation.AddPaymentForm(_cardCreditFixture.CardCreditValid());

            // Act
            var valid = donation.Valid();

            // Assert
            valid.Should().BeFalse(because: "The Support Message field has more characters than allowed");
            donation.ErrorMessages.Should().HaveCount(1, because: "only the Support Message field is invalid.");
            donation.ErrorMessages.Should().Contain("The Support Message field must have a maximum of 500 characters.");
        }

        [Fact]
        [Trait("Donation", "Donation_UninformedData_DonationInvalid")]
        public void Donation_UninformedData_DonationInvalid()
        {
            // Arrange
            var donation = _donationFixture.DonationInvalid(false);
            donation.AddAddressBilling(_addressFixture.AddressValid());
            donation.AddPaymentForm(_cardCreditFixture.CardCreditValid());

            // Act
            var valid = donation.Valid();

            // Assert
            valid.Should().BeFalse(because: "the donation fields were not informed");

            donation.ErrorMessages.Should().HaveCount(3, because: "The 3 mandatory fields of the donation were not filled out");

            donation.ErrorMessages.Should().Contain("Minimum donation amount is $1.00", because: "minimum donation amount not reached.");
            donation.ErrorMessages.Should().Contain("minimum donation amount not reached.", because: "Name field not provided.");
            donation.ErrorMessages.Should().Contain("Email field is required.", because: "the Email field was not informed.");            
        }

        [Fact]
        [Trait("Donation", "Donation_UninformedDataDonationAnonima_DonationInvalid")]
        public void Donation_UninformedDataDonationAnonima_DonationInvalid()
        {
            // Arrange
            var donation = _donationFixture.DonationInvalid(true);
            donation.AddAddressBilling(_addressFixture.AddressValid());
            donation.AddPaymentForm(_cardCreditFixture.CardCreditValid());

            // Act
            var valid = donation.Valid();

            // Assert
            valid.Should().BeFalse(because: "the donation fields were not informed");

            donation.ErrorMessages.Should().HaveCount(2, because: "The 2 mandatory fields for the donation were not filled out");

            donation.ErrorMessages.Should().Contain("Minimum donation amount is $1.00", because: "minimum donation amount not reached.");            
            donation.ErrorMessages.Should().Contain("Email field is required.", because: "the Email field was not informed.");            
        }

    }
}
