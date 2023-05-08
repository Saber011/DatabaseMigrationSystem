namespace DatabaseMigrationSystem.Utils
{
    public static class RetryHelper
    {
        public static void Do(
            Action action,
            Action<Exception, int> actionOnRetry,
            int retryCount,
            TimeSpan retryInterval,
            int retryIntervalFactor)
        {
            Do<object>(() =>
            {
                action();
                return null;
            }, actionOnRetry, retryCount, retryInterval, retryIntervalFactor);
        }

        public static T Do<T>(
            Func<T> action,
            Action<Exception, int> actionOnRetry,
            int retryCount,
            TimeSpan retryInterval,
            int retryIntervalFactor)
        {
            var exceptions = new List<Exception>();

            for (int retry = 0; retry < retryCount; retry++)
            {
                try
                {
                    if (retry > 0)
                    {
                        Thread.Sleep(retryInterval);
                        retryInterval *= retryIntervalFactor;
                    }
                    return action();
                }
                catch (Exception ex)
                {
                    actionOnRetry(ex, retry + 1);
                    exceptions.Add(ex);
                }
            }

            throw new AggregateException(exceptions);
        }
    }
}

