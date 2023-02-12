using System;
using System.Globalization;
using System.Windows.Data;

namespace CSE_681_Project_1.Converters
{
	internal class ConvertNumberToTime : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var result = new TimeSpan();
			var timeNeeded = 0;
			if (!string.IsNullOrEmpty(value?.ToString()))
			{
				result = TimeSpan.FromSeconds(Double.Parse(value.ToString()));
			}

			if (!string.IsNullOrEmpty(parameter?.ToString()))
			{
				timeNeeded = Int32.Parse(parameter.ToString());
			}

			return timeNeeded == 0 ? result.Hours : timeNeeded == 1 ? result.Minutes : timeNeeded == 4 ? result.TotalSeconds : result.Seconds;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var result = new TimeSpan();

			if (!string.IsNullOrEmpty(value?.ToString()))
			{
				var timeNeeded = 0;
				if (!string.IsNullOrEmpty(parameter?.ToString()))
				{
					timeNeeded = Int32.Parse(parameter.ToString());
				}

				var time = Double.Parse(value.ToString());

				time = timeNeeded == 0 ? time * 360 : timeNeeded == 1 ? time * 60 : time;

				result = TimeSpan.FromSeconds(time);
			}

			return result.TotalSeconds.ToString();
		}
	}
}