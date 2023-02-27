using Chronos.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.DataModel
{
    public partial class TimeEntry
    {
        /// <summary>
        /// Saves this <see cref="TimeEntry"/> instance.
        /// </summary>
        /// <param name="context"></param>
        public void Save(IContext context)
        {
            DataStore.Resolve<TimeEntryRepository>()?.Save(context, this);
        }

        /// <summary>
        /// Reads the <see cref="TimeEntry"/> with the given <paramref name="key"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TimeEntry Read(IContext context, int key)
        {
            return DataStore.Resolve<TimeEntryRepository>()?.Read(context, key);
        }

        /// <summary>
        /// Reads all <see cref="TimeEntry"/> objects.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IEnumerable<TimeEntry> ReadAll(IContext context)
        {
            return DataStore.Resolve<TimeEntryRepository>()?.ReadAll(context);
        }
    }
}
