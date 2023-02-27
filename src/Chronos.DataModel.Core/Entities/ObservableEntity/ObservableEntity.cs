// ************************************************
//      Project: Chronos.DataModel.Core
//         File: ObservableEntity.cs
//       Author:
// ************************************************

namespace Chronos.DataModel.Core
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class ObservableEntity<TEntityKey> : Entity<TEntityKey>, INotifyPropertyChanged 
        where TEntityKey : class
    {
       
        // ---- public properties ----
        public event PropertyChangedEventHandler PropertyChanged;

        // ---- constructor ----
        /// <summary>
        /// Initializes a new instance of <see cref="Entity{TEntityKey}"/>.
        /// </summary>
        /// <param name="key"></param>
        public ObservableEntity(TEntityKey key)
            : base(key)
        {
        }

        // ---- methods ----

        protected bool SetProperty<TProperty>(ref TProperty property, TProperty value, [CallerMemberName] string source = "")
        {
            bool hasChanged = false;

            if (property == null || !property.Equals(value))
            {
                hasChanged = true;

                property = value;

                this.RaisePropertyChanged(source);
            }

            return hasChanged;
        }


        /// <summary>
        /// Invokes the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
