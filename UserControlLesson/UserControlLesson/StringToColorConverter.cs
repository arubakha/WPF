using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Reflection;
using System.Windows;

namespace UserControlLesson
{
    public class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (Color)ColorConverter.ConvertFromString((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var names = typeof(Colors).GetProperties().Where(c => (Color)c.GetValue(null, null) == (Color)value).Select(c => c.Name);
            return names.Count() > 0 ? names.First() : DependencyProperty.UnsetValue;
        }
    }
}
