using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.DataModel
{
    public class TaskHelper
    {
        public static TResult RunAsSync<TResult>(Func<Task<TResult>> task)
        { 
            return task().ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        public static Task RunAsSync(Func<Task> task)
        {
            return Task.FromResult(task);
        }
    }
}
