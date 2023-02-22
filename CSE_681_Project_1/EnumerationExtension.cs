using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CSE_681_Project_1.Extensions
{
    public static class EnumerationExtension
    {
        public static string Description(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetTypeInfo().GetDeclaredField(value.ToString());
            DescriptionAttribute[] attributes = fieldInfo != null ? fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[] : new DescriptionAttribute[] { };
            return (attributes.Length > 0) ? attributes.First().Description : value.ToString();
        }
    }
}