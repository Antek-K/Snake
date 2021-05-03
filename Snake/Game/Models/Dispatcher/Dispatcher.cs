using System;
using System.ComponentModel;
using System.Windows;

namespace Game
{
    /// <summary>
    /// Dispatcher to call delegate method on.
    /// </summary>
    public class Dispatcher : IDispatcher
    {
        public object Invoke(Delegate method, object caller, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            return Application.Current?.Dispatcher?.Invoke(method, caller, propertyChangedEventArgs);
        }
    }
}
