using System;
using System.ComponentModel;
using System.Windows;

namespace Game
{
    public class Dispatcher : IDispatcher
    {
        public object Invoke(Delegate method, object caller, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            return Application.Current?.Dispatcher?.Invoke(method, caller, propertyChangedEventArgs);
        }
    }
}
