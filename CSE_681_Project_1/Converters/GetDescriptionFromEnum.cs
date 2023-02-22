using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace CSE_681_Project_1.Converters
{
	public class GetDescriptionFromEnum : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var description = "";

			if (value != null)
			{
				FieldInfo field = value.GetType().GetField(value.ToString());

				if (field != null)
				{
					description = (field.GetCustomAttribute(typeof(DescriptionAttribute), false) as DescriptionAttribute).Description ?? "";
				}
			}
			return description;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}