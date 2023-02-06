using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using Crowdfunding.Domain;

namespace Crowdfunding.Service
{
    public class PollyService : IPollyService
    {
        private GloballAppConfig _globallAppConfig;
        private readonly ILogger<PollyService> _logger;

        public PollyService(ILogger<PollyService> logger, GloballAppConfig globallAppConfig)
        {
            _logger = logger;
            _globallAppConfig = globallAppConfig;
        }

        public AsyncRetryPolicy CreatePolicyWaitAndRetryFor(string method)
        {
            var policy = Policy.Handle<Exception>().WaitAndRetryAsync(_globallAppConfig.Polly.AmountRetry,
                attempt => TimeSpan.FromSeconds(_globallAppConfig.Polly.WaitTimeInSeconds),
                (exception, calculatedWaitDuration) =>
                {
                    _logger.LogError($"Error calling method {method}. Total retry time so far: {calculatedWaitDuration}s");
                });

            return policy;
        }
    }
}
