using FluentAssertions;
using Xunit;
using Crowdfunding.Domain;
using Crowdfunding.Domain.Entities;
using System.Linq;
using Crowdfunding.Tests.Common.Fixtures;

namespace Crowdfunding.Unit.Tests.DomainTests
{
    [Collection(nameof(PersonFixtureCollection))]
    public class DomainNotificationServiceTests: IClassFixture<PersonFixture>
    {
        private readonly PersonFixture _personFixture;
        private readonly IDomainNotificationService _domainNotificationService;

        public DomainNotificationServiceTests(PersonFixture fixture)
        {
            _personFixture = fixture;
            _domainNotificationService = new DomainNotificationService();
        }

        [Trait("DomainNotificationService", "DomainNotificationService_NewClass_ShouldntPossessNotifications")]
        [Fact]
        public void DomainNotificationService_NewClass_ShouldntPossessNotifications()
        {
            // Arrange & Act
            var domainNotification = new DomainNotificationService();

            // Assert
            domainNotification.HasErrors.Should().BeFalse(because:"no domino notifications have been added yet");
        }
        
        [Trait("DomainNotificationService", "DomainNotificationService_AddNotification_HasNotificationsTrue")]
        [Fact]
        public void DomainNotificationService_AddNotification_HasNotificationsTrue()
        {
            // Arrange
            var domainNotification = new DomainNotification("RequiredField", "The name field is required");

            // Act
            _domainNotificationService.Add(domainNotification);

            // Assert            
            _domainNotificationService.HasErrors.Should().BeTrue(because: "code notification was added RequiredField");

            var notifications = _domainNotificationService.RecuperarErrosDominio().Select(a => a.MensagemErro);
            notifications.Should().Contain("The name field is required", because: "code notification was added RequiredField");
        }

        [Trait("DomainNotificationService", "DomainNotificationService_AddEntity_HasNotificationsTrue")]
        [Fact]
        public void DomainNotificationService_AddEntity_HasNotificationsTrue()
        {
            // Arrange
            var person = _personFixture.PersonEmpty();
            person.Valid();

            // Act
            _domainNotificationService.Add(person);

            // Assert
            var notifications = _domainNotificationService.RecuperarErrosDominio().Select(a => a.MessageError);

            notifications.Should().HaveCount(2, because: "none of the 2 mandatory fields was informed.");
            notifications.Should().Contain("The name field is required.", because: "Name field not provided.");
            notifications.Should().Contain("Email field is required.", because: "the Email field was not informed.");

            _domainNotificationService.HasErrors.Should().BeTrue(because: "added invalid person entity");
        }
    }
}