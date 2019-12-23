using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PresentationLayer.View.Helper
{

    public class InvertedBoolToVisibilityConverter : IValueConverter
    {

        private object GetVisibility(object value)
        {
            if (!(value is bool))
            {
                return Visibility.Collapsed;
            }

            bool isVisible = (bool)value;
            if (isVisible)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            return GetVisibility(!(bool)value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            throw new NotImplementedException();
        }

    }
}
