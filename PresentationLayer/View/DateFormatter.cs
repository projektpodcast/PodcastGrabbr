using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PresentationLayer.View
{
    //public class DateFormatter : IValueConverter
    //{
    //    public async Task<bool> CheckUri(string path)
    //    {
    //        Uri uri = new Uri(path);
    //        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);


    //        try
    //        {
    //            await request.GetResponseAsync();
    //            return true;
    //        }
    //        catch (Exception)
    //        {

    //            return false;
    //        }

    //        //if (await request.GetResponseAsync() != null)
    //        //{
    //        //    return true;
    //        //}
    //        //else
    //        //{
    //        //    return false;
    //        //}
    //    }

    //    public object Convert(object value, Type targetType, object parameter, CultureInfo language)
    //    {
    //        //object x = await CheckUri(value as string);
    //        var task = CheckUri("s");
    //        var result = task.RunSynchronously;
    //        //await CheckUri("a");
    //        //if (await CheckUri(value as string))
    //        //{

    //        //}
    //        return null;
    //        //return GetVisibility(!(bool)value);
    //    }
    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    //public async object Convert(object value, Type targetType, object parameter, CultureInfo language)
    //    //{
    //    //    string path = value as string;
    //    //    Uri uri = new Uri(path);
    //    //    //try do download
    //    //    Windows.Web.Http.HttpClient httpClient = new Window.Web.Http.HttpClient();
    //    //    try
    //    //    {
    //    //        var image = await httpClient.GetAsync(uri);
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        return "defaultImage.png";
    //    //    }

    //    //    return path;
    //    //}

    //    //public object ConvertBack(object value, Type targetType, object parameter, string language)
    //    //{
    //    //    throw new NotImplementedException();
    //    //}
    //}
}
