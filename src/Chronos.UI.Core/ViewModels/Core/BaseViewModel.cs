// ************************************************
//      Project: Chronos.UI.Core
//         File: BaseViewModel.cs
//       Author:
// ************************************************

namespace Chronos.UI.Core
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class BaseViewModel : INotifyPropertyChanged
    {
        // ---- public properties ----
        public event PropertyChangedEventHandler PropertyChanged;

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
