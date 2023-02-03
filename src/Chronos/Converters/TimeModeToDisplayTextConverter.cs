using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.UI
{

    public class TimeModeToDisplayTextConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeMode timeMode && parameter is SourceControl source)
            {
                if (source == SourceControl.TrackButton)
                {

                    if (timeMode == TimeMode.Stopped)
                    {
                        return "Start";
                    }
                    else if (timeMode == TimeMode.Tracking)
                    {
                        return "Stop";
                    }
                }
                else
                {
                    if (timeMode == TimeMode.Tracking)
                    {
                        return "Pause";
                    }
                    else
                    {
                        return "End pause";
                    }
                }
            }

            return System.Convert.ToString(value);
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
