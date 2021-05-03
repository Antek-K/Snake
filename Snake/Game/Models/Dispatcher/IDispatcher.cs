using System;
using System.ComponentModel;

namespace Game
{
    /// <summary>
    /// Interface for dispatcher to make possible to select dispatcher to call delegate method on.
    /// </summary>
    public interface IDispatcher
    {
        object Invoke(Delegate method, object caller, PropertyChangedEventArgs propertyChangedEventArgs);
    }
}
