using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using Crowdfunding.Domain.Base;
using Crowdfunding.Domain.Entities;

namespace Crowdfunding.Domain
{
    public interface IDomainNotificationService
    {
        bool HasErrors { get; }
        IEnumerable<DomainNotification> RecoverErrorsDomain();
        string RecoverErrorsDomainFormattedHtml();

        void Add<T>(T entity) where T : Entity;

        void Add(ValidationResult validationResult);
        void Add(DomainNotification domainNotification);
    }

    public class DomainNotificationService : IDomainNotificationService
    {
        private readonly List<DomainNotification> _notifications;

        public bool HasErrors => _notifications.Any();

        public DomainNotificationService()
        {
            _notifications = new List<DomainNotification>();
        }

        public IEnumerable<DomainNotification> RecoverErrorsDomain()
        {
            return _notifications;
        }

        public void Add(DomainNotification domainNotification)
        {
            _notifications.Add(domainNotification);
        }

        public void Add<T>(T entity) where T : Entity
        {
            var notifications = entity.ValidationResult.Errors.Select(a => new DomainNotification(a.ErrorCode, a.ErrorMessage));
            _notifications.AddRange(notifications);
        }

        public void Add(ValidationResult validationResult)
        {
            if (validationResult == null) return;

            var notifications = validationResult.Errors.Select(a => new DomainNotification(a.ErrorCode, a.ErrorMessage));
            _notifications.AddRange(notifications);
        }

        private static bool ThereisErrorForProperty(string field)
        {
            return !string.IsNullOrEmpty(field);
        }

        private void AddErrorDomain(DomainNotification notification)
        {
            if (FieldFilled(notification) && !ErrorExists(notification.MessageError))
            {
                _notifications.Add(notification);
            }
        }

        private bool ErrorExists(string field)
        {
            return _notifications.Any(a => a.MessageError == field);
        }

        private static bool FieldFilled(DomainNotification notification)
        {
            return !string.IsNullOrEmpty(notification.MessageError);
        }

        public string RecoverErrorsDomainFormatadoHtml()
        {
            var errors = string.Join("", RecoverErrorsDomain().Select(a => $"<li>{a.MessageError}</li>").ToArray());
            return $"<ul>{errors}</ul>";
        }
    }
}
