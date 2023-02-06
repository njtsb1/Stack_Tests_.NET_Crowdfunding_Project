using System;

namespace Crowdfunding.Domain
{
    public class GloballAppConfig
    {
        public int MetaCampaign { get; set; }
        public DateTime DateEndCampaign { get; set; }     
       
        public RetryPolicy Polly { get; set; }        
    }

    public class RetryPolicy
    {
        public int AmountRetry { get; set; }
        public int WaitTimeInSeconds { get; set; }
    }
}