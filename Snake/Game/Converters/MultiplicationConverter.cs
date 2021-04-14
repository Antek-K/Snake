﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Game
{
    public class MultiplicationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int && parameter is int)
            {
                return (int)value * (int)parameter;
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
