using Microsoft.Maui.Controls.Xaml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.UI
{
    public class TimeModeToIsVisibleConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeMode timeMode && parameter is SourceControl source)
            {
                if (source == SourceControl.PauseButton)
                {
                    return timeMode != TimeMode.Stopped;
                }
                else
                {
                    return timeMode != TimeMode.Paused;
                }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}

