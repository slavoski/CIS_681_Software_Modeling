using System;
using System.Globalization;
using System.Windows.Data;

namespace CSE_681_Project_1.Converters
{
	public class ConvertDateToString : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var result = DateTime.Now;
			if (!string.IsNullOrEmpty(value?.ToString()))
			{
				result = DateTime.Parse(value.ToString());
			}
			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var result = "";
			if (!string.IsNullOrEmpty(value?.ToString()))
			{
				var dateTime = DateTime.Parse(value.ToString());
				result = dateTime.ToString("MMM dd, yyyy");
			}
			return result;
		}
	}
}