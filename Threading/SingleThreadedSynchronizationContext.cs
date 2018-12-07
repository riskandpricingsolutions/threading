using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace RiskAndPricingSolutions.Threading
{
    /// <summary>
    /// A simple implementation of a SynchronizationContext for educational purposes.
    /// </summary>
    public class SingleThreadedSynchronizationContext : SynchronizationContext
    {
        public SingleThreadedSynchronizationContext()
        {
            new Thread(() =>
            {
                _logger.Debug($"{nameof(SingleThreadedSynchronizationContext)} Starting single thread");
                while (true)
                {
                    try
                    {
                        _callbacks.Take()();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }) {Name = "SingleThreadedSynchronizationContext" }
            .Start();
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            _callbacks.Add(() => d(state));
        }

        public override void Send(SendOrPostCallback d, object state)
        {
            var tcs = new TaskCompletionSource<object>();

            _callbacks.Add(() =>
            {
                d(state);
                tcs.SetResult(null);
            });

            tcs.Task.Wait();
        }

        private readonly BlockingCollection<Action> _callbacks = 
            new BlockingCollection<Action>();

        private readonly ILog _logger = LogManager.GetLogger(typeof(SingleThreadedSynchronizationContext));
    }
}
