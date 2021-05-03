using System;
using System.Globalization;
using System.Windows.Data;

namespace Game
{
    /// <summary>
    /// Provides one way multiplication converter for int values.
    /// </summary>
    public class MultiplicationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue && parameter is int intParameter)
            {
                return intValue * intParameter;
            }
            if(value is int)
            {
                return value;
            }
            return 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
