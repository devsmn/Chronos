using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.DataModel.Core
{
    public abstract class EntityKey<TValue>
    {
        public TValue Id { get; set; }

        public EntityKey(TValue value)
        {
            this.Id = value;
        }
    }
}
