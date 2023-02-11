using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Data;

namespace CSE_681_Project_1.Converters
{
	public class GetDescriptionFromPropertyAddColon : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				return Binding.DoNothing;

			string propertyName = (parameter as string).Split('.').Last();
			if (String.IsNullOrEmpty(propertyName))
				return new ArgumentNullException("parameter").ToString();

			Type type = typeof(GameInfo);

			PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
			if (property == null)
			{
				type = typeof(TeamInfo);
				property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
			}

			if (property == null)
				return new ArgumentOutOfRangeException("parameter", parameter,
					"Property \"" + propertyName + "\" not found in type \"" + type.Name + "\".").ToString();

			if (!property.IsDefined(typeof(DescriptionAttribute), true))
				return new ArgumentOutOfRangeException("parameter", parameter,
					"Property \"" + propertyName + "\" of type \"" + type.Name + "\"" +
					" has no associated Description attribute.").ToString();

			return ((DescriptionAttribute)property.GetCustomAttributes(typeof(DescriptionAttribute), true)[0]).Description + ":";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}