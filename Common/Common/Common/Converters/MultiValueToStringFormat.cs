using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WPFCommon.Common.Converters
{
    /// <summary>
    /// 여러 MultiBinding 값을 staring format 으로 변환한다.
    /// </summary>
    public class MultiValueToStringFormat : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values==null)
            {
                return null;
            }

            String format=parameter as String;
            if(format==null)
            {
                return null;
            }

            return String.Format(format, values);


        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

   
}
