using FluentAssertions;
using Xunit;
using Crowdfunding.Tests.Common.Fixtures;

namespace Crowdfunding.Unit.Tests.DomainTests
{
    [Collection(nameof(PersonFixtureCollection))]
    public class PersonTests : IClassFixture<PersonFixture>
    {
        private readonly PersonFixture _fixture;

        public PersonTests(PersonFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        [Trait("Person", "Person_CorrectlyFilledin_PersonValid")]
        public void Person_CorrectlyFilledin_PersonValid()
        {
            // Arrange
            var person = _fixture.PersonValid();
            
            // Act
            var valid = person.Valid();

            // Assert
            valid.Should().BeTrue(because: "the fields have been filled in correctly");
            person.ErrorMessages.Should().BeEmpty();            
        }

        [Fact]
        [Trait("Person", "Person_NoDataFilled_PersonInvalid")]
        public void Person_NoDataFilled_PersonInvalid()
        {
            // Arrange
            var person = _fixture.PersonEmpty();

            // Act
            var valid = person.Valid();

            // Assert
            valid.Should().BeFalse(because: "must have validation errors");

            person.ErrorMessages.Should().HaveCount(2, because: "none of the 2 mandatory fields was informed.");

            person.ErrorMessages.Should().Contain("The name field is required.", because: "Name field not provided.");
            person.ErrorMessages.Should().Contain("Email field is required.", because: "the Email field was not informed.");
        }

        [Fact]
        [Trait("Person", "Person_EmailInvalid_PersonInvalid")]
        public void Person_EmailInvalid_PersonInvalid()
        {
            // Arrange
            const bool EMAIL_INValid = true;
            var person = _fixture.PersonValid(EMAIL_INValid);

            // Act
            var valid = person.Valid();

            // Assert
            valid.Should().BeFalse(because: "");
            person.ErrorMessages.Should().HaveCount(1, because: "only the email field is invalid.");

            person.ErrorMessages.Should().Contain("Email field is invalid.");
        }

        [Fact]
        [Trait("Person", "Person_CamposMaxLenghtExcedidos_PersonInvalid")]
        public void Person_CamposMaxLenghtExcedidos_PersonInvalid()
        {
            // Arrange            
            var person = _fixture.PersonMaxLenth();

            // Act
            var valid = person.Valid();

            // Assert
            valid.Should().BeFalse(because: "name and email fields have more characters than allowed.");
            person.ErrorMessages.Should().HaveCount(2, because: "the data is invalid.");

            person.ErrorMessages.Should().Contain("The Name field must have a maximum of 150 characters.");
            person.ErrorMessages.Should().Contain("The Email field must have a maximum of 150 characters.");
        }
    }
}