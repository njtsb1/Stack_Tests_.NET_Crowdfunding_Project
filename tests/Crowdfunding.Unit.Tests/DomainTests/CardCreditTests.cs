using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Crowdfunding.Tests.Common.Fixtures;

namespace Crowdfunding.Unit.Tests.DomainTests
{
    [Collection(nameof(CardCreditFixtureCollection))]
    public class CardCreditTests: IClassFixture<CardCreditFixture>
    {
        private readonly CardCreditFixture _fixture;

        public CardCreditTests(CardCreditFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        [Trait("CardCredit", "CardCredit_CorrectlyFilledin_CardCreditValid")]
        public void CardCredit_CorrectlyFilledin_CardCreditValid()
        {
            // Arrange
            var cardCredit = _fixture.CardCreditValid();

            // Act
            var valid = cardCredit.Valid();

            // Assert
            valid.Should().BeTrue(because: "the fields have been filled in correctly");
            cardCredit.ErrorMessages.Should().BeEmpty();
        }

        [Fact]
        [Trait("CardCredit", "CardCredit_NoDataFilled_CardCreditInvalid")]
        public void CardCredit_NoDataFilled_CardCreditInvalid()
        {
            // Arrange
            var cardCredit = _fixture.CardCreditEmpty();

            // Act
            var valid = cardCredit.Valid();

            // Assert
            valid.Should().BeFalse(because: "must have filling errors");
            cardCredit.ErrorMessages.Should().HaveCount(4, because: "none of the 4 mandatory fields were informed or are incorrect.");

            cardCredit.ErrorMessages.Should().Contain("The Name field must be filled in", because: "the Name Holder field is mandatory and has not been filled in.");
            cardCredit.ErrorMessages.Should().Contain("The Credit Card Number field must be filled in", because: "the Credit card number field is required and not filled in.");
            cardCredit.ErrorMessages.Should().Contain("The CVV field must be completed", because: "the CVV field is mandatory and has not been completed.");
            cardCredit.ErrorMessages.Should().Contain("The Validity field must be filled in", because: "The Credit Card Expiration Date field is required and not filled in.");
        }

        [Fact]
        [Trait("CardCredit", "CardCredit_ValidityNumberCVVInvalid_CardCreditInvalid")]
        public void CardCredit_ValidityNumberCVVInvalid_CardCreditInvalid()
        {
            // Arrange
            var cardCredit = _fixture.CardCreditValidityNumberCVVInvalid();

            // Act
            var valid = cardCredit.Valid();

            // Assert
            valid.Should().BeFalse(because: "must have validation errors");
            cardCredit.ErrorMessages.Should().HaveCount(3, because: "none of the 3 mandatory fields were informed or are incorrect.");

            cardCredit.ErrorMessages.Should().Contain("Invalid credit card number field", because: "the Name Holder field is mandatory and has not been filled in.");
            cardCredit.ErrorMessages.Should().Contain("Invalid CVV field", because: "the Name Holder field is mandatory and has not been filled in.");
            cardCredit.ErrorMessages.Should().Contain("Invalid credit card expiration date field", because: "the Name Holder field is mandatory and has not been filled in.");
        }

        [Fact]
        [Trait("CardCredit", "CardCredit_ValidityExpired_CardCreditInvalid")]
        public void CardCredit_ValidityExpired_CardCreditInvalid()
        {
            // Arrange
            var cardCredit = _fixture.CardCreditValidityExpired();

            // Act
            var valid = cardCredit.Valid();

            // Assert
            valid.Should().BeFalse(because: "must have validation errors");
            cardCredit.ErrorMessages.Should().HaveCount(1, because: "expired expiration date");

            cardCredit.ErrorMessages.Should().Contain("Expired credit card", because: "The Credit Card Expiration Date field is expired.");
        }

        [Fact]
        [Trait("CardCredit", "CardCredit_NameHolderMaxLength_CardCreditInvalid")]
        public void CardCredit_NameHolderMaxLength_CardCreditInvalid()
        {
            // Arrange
            var cardCredit = _fixture.CardCreditNameHolderMaxLengthInvalid();

            // Act
            var valid = cardCredit.Valid();

            // Assert
            valid.Should().BeFalse(because: "maximum size of fields reached");
            cardCredit.ErrorMessages.Should().HaveCount(1, because: "filling in 1 field exceeded the maximum allowed size.");

            cardCredit.ErrorMessages.Should().Contain("The Name Holder field must have a maximum of 150 characters", because: "the Name Holder field is mandatory and has not been filled in.");
        }

    }
}
