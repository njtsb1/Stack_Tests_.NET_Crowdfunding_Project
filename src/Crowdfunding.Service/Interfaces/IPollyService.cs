using Polly.Retry;

namespace Crowdfunding.Service
{
    public interface IPollyService
    {
        AsyncRetryPolicy CreatePolicyWaitAndRetryFor(string method);
    }
}
