using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Crowdfunding.Tests.Common.Fixtures;
namespace Crowdfunding.Unit.Tests.DomainTests
{
    [Collection(nameof(AddressFixtureCollection))]
    public class AddressTests: IClassFixture<AddressFixture>
    {
        private readonly AddressFixture _fixture;

        public AddressTests(EddressFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        [Trait("Address", "Address_CorrectlyFilledin_AddressValid")]
        public void Address_CorrectlyFilledin_AddressValid()
        {
            // Arrange
            var address = _fixture.AddressValid();

            // Act
            var valid = address.Valid();

            // Assert
            valid.Should().BeTrue(because: "the fields have been filled in correctly");
            address.ErrorMessages.Should().BeEmpty();
        }

        [Fact]
        [Trait("Address", "Address_NoDataFilled_AddressInvalid")]
        public void Address_NoDataFilled_AddressInvalid()
        {
            // Arrange
            var address = _fixture.AddressEmpty();

            // Act
            var valid = address.Valid();

            // Assert
            valid.Should().BeFalse(because: "must have filling errors");
            address.ErrorMessages.Should().HaveCount(6, because: "none of the 6 mandatory fields were informed or are incorrect.");

            address.ErrorMessages.Should().Contain("City field must be filled in", because: "City field is required and not filled in.");
            address.ErrorMessages.Should().Contain("The Address field must be filled in", because: "the Address field is required and not filled in.");
            address.ErrorMessages.Should().Contain("The zip code field must be filled in", because: "CEP field is required and not filled in.");
            address.ErrorMessages.Should().Contain("Invalid State field", because: "the State field was not filled in as expected.");
            address.ErrorMessages.Should().Contain("The Phone field must be filled in", because: "Phone field not filled in as expected.");
            address.ErrorMessages.Should().Contain("The Number field must be filled in", because: "the Number field was not filled in as expected.");
        }

        [Fact]
        [Trait("Address", "Address_ZIPCodePhoneStatusInvalid_AddressInvalid")]
        public void Address_ZIPCodePhoneStatusInvalid_AddressInvalid()
        {
            // Arrange
            var address = _fixture.AddressZIPCodePhone StatusInvalid();

            // Act
            var valid = address.Valid();

            // Assert
            valid.Should().BeFalse(because: "must have validation errors");
            address.ErrorMessages.Should().HaveCount(3, because: "filling in 3 fields was not done as expected.");
            
            address.ErrorMessages.Should().Contain("Invalid zip code field", because: "zip code field was not filled in as expected.");
            address.ErrorMessages.Should().Contain("Invalid State field", because: "the State field was not filled in as expected.");
            address.ErrorMessages.Should().Contain("Invalid phone field", because: "Phone field not filled in as expected.");            
        }

        [Fact]
        [Trait("Address", "Address_AddressCityComplementMaxLength_AddressInvalid")]
        public void Address_AddressCityComplementMaxLength_AddressInvalid()
        {
            // Arrange
            var address = _fixture.AddressMaxLength();

            // Act
            var valid = address.Valid();

            // Assert
            valid.Should().BeFalse(because: "maximum size of fields reached");
            address.ErrorMessages.Should().HaveCount(4, because: "the filling of 4 fields exceeded the maximum allowed size.");

            address.ErrorMessages.Should().Contain("The Address field must have a maximum of 250 characters", because: "the Address field has exceeded the maximum allowed size.");
            address.ErrorMessages.Should().Contain("The City field must have a maximum of 150 characters", because: "City field exceeded maximum allowed size.");
            address.ErrorMessages.Should().Contain("The Complement field must have a maximum of 250 characters", because: "the Add-on field has exceeded the maximum allowed size.");
            address.ErrorMessages.Should().Contain("The Number field must have a maximum of 6 characters", because: "the Add-on field has exceeded the maximum allowed size.");
        }
        

    }
}
