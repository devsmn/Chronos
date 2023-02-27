using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.DataModel.Core
{
    public class ChronosAppContext : IContext
    {
        public CancellationToken CancellationToken { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Guid CorrelactionId => throw new NotImplementedException();

        public int ThreadId => throw new NotImplementedException();
    }
}

