//
//
//
//

namespace Chronos.DataModel.Core
{
    using System;
    using System.ComponentModel;

    public class Entity<TEntityKey> where TEntityKey : EntityKey
    {
        // ---- public properties ----
        public DateTime CreationDate { get; set; }

        public EntityKey Key { get; private set; }

        // ---- constructor ----

        /// <summary>
        /// Initializes a new instance of <see cref="Entity{TEntityKey}"/>.
        /// </summary>
        /// <param name="key"></param>
        public Entity(EntityKey key)
        {
            this.Key = key;
        }
    }
}
