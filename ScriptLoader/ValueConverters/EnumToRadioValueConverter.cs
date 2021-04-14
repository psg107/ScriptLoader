using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ScriptLoader.ValueConverters
{
    public class EnumToRadioValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enumType = value.GetType();
            var enumValue = value.ToString();
            var enumValueParameter = parameter.ToString();

            var actualEnum = Enum.Parse(enumType, enumValue);
            var expectedEnum = Enum.Parse(enumType, enumValueParameter);

            if (actualEnum.ToString() == expectedEnum.ToString())
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;
        }
    }
}
