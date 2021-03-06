﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Hirakata.Converter
{
    public class VisibilityConverterReverse : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isHD = (bool)value;
            if (isHD) return System.Windows.Visibility.Hidden;
            return System.Windows.Visibility.Visible;
        }


        /// <summary>
        /// Not used, due to Readonly
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
