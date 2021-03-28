using System;
using System.ComponentModel;

namespace Game
{
    public interface IDispatcher
    {
        object Invoke(Delegate method, object caller, PropertyChangedEventArgs propertyChangedEventArgs);
    }
}
