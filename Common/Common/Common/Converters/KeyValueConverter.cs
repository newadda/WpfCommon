using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Common.Common.Converters
{
    public class keyValue
    {
        public object Key { get; set; }
        public object Value { get; set; }
    }

    public class KeyValueConverter : IValueConverter
    {
        private List<keyValue> _Items = new List<keyValue>();
        public List<keyValue> Items
        {
            get { return _Items; }

            set
            {
                _Items = value;
            }

        }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (Items == null)
            {
                return value;
            }

            foreach (var item in Items)
            {
                if (value == null)
                {
                    if (item.Key == null)
                    {
                        return item.Value;
                    }

                }
                else if (item.Key.GetType() == value.GetType())
                {
                    if (item.Key.Equals(value))
                    {
                        return item.Value;
                    }
                }
            }

            return value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
            //return value;
        }
    }
}
