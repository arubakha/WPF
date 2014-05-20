using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MultithreadingLesson.Business
{
    public enum Mode
    {
        LongCall,
        Delegate,
        Thread,
        AutoResetEvent
    }

    public class BoolToModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.Equals(parameter))
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value == true ? parameter : null;
        }
    }
}
