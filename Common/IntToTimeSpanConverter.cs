﻿using Microsoft.UI.Xaml.Data;
using System;
namespace vernard.Common
{
    internal class IntToTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return string.Format("{0:mm\\:ss}", TimeSpan.FromSeconds((int)value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException("IntToTimeSpanConverter only supports OneTime and OneWay binding modes");
        }
    }
}
