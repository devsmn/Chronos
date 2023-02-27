//
//
//
//

namespace Chronos.DataModel.Core
{
    using System;
    using System.ComponentModel;

    public class Entity<TKey> where TKey : class
    {
        // ---- public properties ----
        public DateTime CreationDate { get; set; }

        public TKey Key { get; private set; }

        // ---- constructor ----

        /// <summary>
        /// Initializes a new instance of <see cref="Entity{TEntityKey}"/>.
        /// </summary>
        /// <param name="key"></param>
        public Entity(TKey key)
        {
            this.Key = key;
        }

        // ---- methods ----
        public virtual void SetKey(TKey key)
        {
            this.Key = key;
        }

    }
}
