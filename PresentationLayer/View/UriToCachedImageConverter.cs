using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PresentationLayer.View
{
    [Obsolete]
    public class UriToCachedImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            try
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.DecodePixelHeight = 100;
                    bi.DecodePixelWidth = 100;
                    bi.UriSource = new Uri(value.ToString());

                    bi.EndInit();

                    return bi;
                    
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }



        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("Two way conversion is not supported.");
        }
    }
}
