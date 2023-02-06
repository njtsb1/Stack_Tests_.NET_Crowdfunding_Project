using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Crowdfunding.Domain;

namespace Crowdfunding.MVC.Controllers
{
    public class BaseController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly IDomainNotificationService _domainNotificationService;

        public BaseController(IDomainNotificationService domainNotificationService,
                              IToastNotification toastNotification)
        {
            _domainNotificationService = domainNotificationService;
            _toastNotification = toastNotification;
        }

        protected void AddNotificationOperationPerformedWithSuccess(string messageSuccess = null)
        {
            var sucessMessage = messageSuccess ?? "Operation performed successfully!";
            _toastNotification.AddSuccessToastMessage(sucessMessage);
        }

        protected void AddErrorsDominio()
        {
            var errorMessage = _domainNotificationService.HasErrors
                ? _domainNotificationService.RecoverErrorsDomainFormattedHtml()
                : null;

            if (!string.IsNullOrEmpty(errorMessage))
            {
                _toastNotification.AddErrorToastMessage(errorMessage);
            }
        }
    }
}