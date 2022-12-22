using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Project.WPF
{
    public class EnumConverter : IValueConverter
    {
        public string GetDescription(object obj)
        {
            Type type = obj.GetType();
            var member = type.GetMembers().FirstOrDefault(member => obj.ToString() == member.Name);
            var descrAttribute = member.GetCustomAttributes(typeof(DescriptionAttribute), true).Cast<DescriptionAttribute>().Select(x => x.Description).ToList();
            if (descrAttribute.Count > 0)
                return descrAttribute[0];
            else
                return "";
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "";
            foreach (var one in Enum.GetValues(parameter as Type))
            {
                if (value.Equals(one))
                    return GetDescription(one);
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            foreach (var one in Enum.GetValues(parameter as Type))
            {
                if (value.ToString() == GetDescription(one))
                    return one;
            }
            return null;
        }
    }
}
