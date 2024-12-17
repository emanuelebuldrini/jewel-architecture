using Polly.Retry;
using Polly;
using JewelArchitecture.Core.Infrastructure.Resilience.Exceptions;

namespace JewelArchitecture.Core.Infrastructure.Resilience
{
    public static class PollyHttpRequestHelper
    {
        public static AsyncRetryPolicy CreatePolicy(BackoffStrategy strategy, int retryCount,
            double baseDelaySeconds, Action<Exception, TimeSpan, int, Context> onRetryAction) =>
            Policy.Handle<HttpRetryableException>() // e.g., the external API is down or there is a network issue.                
                .WaitAndRetryAsync(
                    retryCount,
                    attempt => CalculateDelay(strategy, baseDelaySeconds, attempt),
                    onRetry: onRetryAction);

        private static TimeSpan CalculateDelay(BackoffStrategy strategy, double baseDelaySeconds, int attempt)
        {
            return strategy switch
            {
                BackoffStrategy.Linear => TimeSpan.FromSeconds(baseDelaySeconds * attempt),

                BackoffStrategy.Exponential => TimeSpan.FromSeconds(Math.Pow(baseDelaySeconds, attempt)),

                BackoffStrategy.Constant => TimeSpan.FromSeconds(baseDelaySeconds),

                _ => throw new ArgumentException($"Unknown retry strategy: {strategy}")
            };
        }
    }
}
