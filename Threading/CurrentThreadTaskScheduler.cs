using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskAndPricingSolutions.Threading
{
    /// <summary>
    /// A TaskScheduler than executes tasks immediately
    /// on whatever threads its methods are invoked on
    /// </summary>
    public class CurrentThreadTaskScheduler : TaskScheduler
    {
        protected override void QueueTask(Task task)
        {
            TryExecuteTask(task);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return TryExecuteTask(task);
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return Enumerable.Empty<Task>();
        }
    }
}

