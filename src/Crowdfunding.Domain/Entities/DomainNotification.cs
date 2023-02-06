namespace Crowdfunding.Domain.Entities
{
    public class DomainNotification
    {
        public string Id { get; private set; }
        public string MessageError { get; private set; }

        public DomainNotification(string errorCode, string errorMessage)
        {
            Id = errorCode;
            MessageError = errorMessage;
        }
    }
}
