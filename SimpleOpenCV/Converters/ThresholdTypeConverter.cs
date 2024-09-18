using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SimpleOpenCV.Converters
{
    public class ThresholdTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object ret = ThresholdConverter(value, targetType, parameter, culture);
            if (ret != null)
                return ret;
            else
                return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object ret = ThresholdConverter(value,targetType,parameter,culture);
            if (ret != null)
                return ret;
            else
                return Binding.DoNothing;
        }
        private static object ThresholdConverter(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(string))
            {
                if (value is ThresholdTypes thresholdType)
                {
                    return thresholdType.ToString();
                }
                return string.Empty;
            }
            if (targetType == typeof(ThresholdTypes))
            {
                if (value is string strValue && Enum.TryParse(strValue, true, out ThresholdTypes result))
                {
                    return result;
                }
                return ThresholdTypes.Binary;
            }

            return null;
        }
    }
}
