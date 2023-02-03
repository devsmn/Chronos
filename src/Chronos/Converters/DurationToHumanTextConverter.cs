// ************************************************
//      Project: Chronos.UI
//         File: DurationToHumanTextConverter.cs
//       Author: 
// ************************************************

namespace Chronos.UI
{
    using System;
    using System.Globalization;

    internal class DurationToHumanTextConverter : IValueConverter, IMarkupExtension
    {
        /// <summary>
        /// Converts the given <paramref name="value"/> to a human-readable string.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan timeSpan)
            {
                return $"{timeSpan.Hours}h {timeSpan.Minutes}m {timeSpan.Seconds}s";
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
