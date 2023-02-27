using Chronos.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.DataModel
{
    public abstract class TimeEntryRepository : ChronosRepository
    {
        public abstract void Save(IContext context, TimeEntry entry);
        public abstract TimeEntry Read(IContext context, int id);
        public abstract IEnumerable<TimeEntry> ReadAll(IContext context);
    }


}
