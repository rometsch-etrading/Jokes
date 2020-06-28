using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Jokes.ViewModels
{
    /// <summary>
    /// Base Class for ViewModels
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// PropertyChanged Event Handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises a PropertyChanged Event
        /// </summary>
        /// <param name="property">Property Name (Caller Name if empty)</param>
        protected void RaisePropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        /// <summary>
        /// Convenience Method, sets value if changed and Raises PropertyChanged
        /// </summary>
        /// <typeparam name="T">Type of the Value</typeparam>
        /// <param name="backingField">Old Value</param>
        /// <param name="value">New Value</param>
        /// <param name="propertyName">Property Name</param>
        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return;

            backingField = value;

            RaisePropertyChanged(propertyName);
        }
    }
}