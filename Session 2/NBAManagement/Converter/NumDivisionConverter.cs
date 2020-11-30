using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NBAManagement.Converter
{
    public class NumDivisionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double numValue = (double)value;
            int numParameter = System.Convert.ToInt32(parameter);

            return (numValue / numParameter) * 0.925;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int numValue = (int)value;
            int numParameter = (int)parameter;

            return numValue / 0.925 * numParameter;
        }
    }
}
