using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.DataModel
{
    public abstract class ChronosRepository
    {
        public abstract Task<bool> InitializeAsync();
    }
}
