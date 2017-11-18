using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RiskAndPricingSolutions.Threading.Examples.AsyncAwait
{
    /// <summary>
    /// Show how a very simple async/await method
    /// could be implemented by the compiler using
    /// awaiters
    /// </summary>
    public class HowAsyncAwaitWorks01
    {
        /// <summary>
        /// A simple method using async/await
        /// </summary>
        /// <returns></returns>
        public async Task MethodOneAsync()
        {
            try
            {
                await DoWorkAsync();
            }
            catch (ArgumentException e)
            {
            }
        }

        /// <summary>
        /// Implementing the same logic as the async/await method
        /// but using awaiters
        /// </summary>
        public void MethodOneAwaiter()
        {
            Task task = DoWorkAsync();
            TaskAwaiter awaiter = task.GetAwaiter();

            awaiter.OnCompleted(() =>
            {
                try
                {
                    awaiter.GetResult();
                }
                catch (ArgumentException e)
                {
                }
            });
        }

        public Task DoWorkAsync() => Task.Run(() => throw new ArgumentException("Bad Argument"));

    }
}