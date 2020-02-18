using System;
using System.Globalization;
using Xamarin.Forms;

namespace SideProject.Converters
{
    public class StringToLabelStyleConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
           CultureInfo culture)
        {
            if (targetType != typeof(Style))
                throw new InvalidOperationException("The target must be a Style");

            var styleString = (string)value;
            if (!string.IsNullOrEmpty(styleString))
            {
                return Application.Current.Resources["styleString"] as Style;
            }

            return Application.Current.Resources["BaseLabel"] as Style;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
           CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
