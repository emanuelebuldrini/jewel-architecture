namespace JewelArchitecture.Examples.SmartCharging.WebApiTest.Mocks
{
    public class ConcurrencySynchronizer(int concurrentTasks = 2, int syncMsTimeout = 2000) : IDisposable
    {
        private SemaphoreSlim? _synchronizer = new(0, concurrentTasks);
        private int waitingThreadCount = 0;
        private readonly object lockObj = new();

        public bool IsDisposed { get => _synchronizer == null; }

        public async Task WaitAsync()
        {
            if (_synchronizer == null)
            {
                return;
            }

            lock (lockObj)
            {
                waitingThreadCount++; // Track the number of threads waiting
            }

            var synchronized=await _synchronizer.WaitAsync(TimeSpan.FromMilliseconds(syncMsTimeout));

            if (!synchronized)
            {
                throw new TimeoutException($"Failed to synchronize tasks within a timeout of '{syncMsTimeout}' ms.");
            }
        }
        public void ReleaseAll()
        {
            _synchronizer?.Release(waitingThreadCount);
            waitingThreadCount = 0;

            Dispose(); // After the first release the purpose has been accomplished.
            _synchronizer = null;
        }

        public void Dispose()
        {
            _synchronizer?.Dispose();
        }
    }
}
