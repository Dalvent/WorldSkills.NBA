using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NBAManagement.Converter
{
    public class GameStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int numValue = (int)value;
            switch(numValue)
            {
            case -1: return "Not start";
            case 0:  return "Running";
            case 1:  return "Finished";
            default: return new ArgumentException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = (string)value;
            switch(strValue)
            {
            case "Not start": return -1;
            case "Running": return 0;
            case "Finished": return 1;
            default: return new ArgumentException();
            }
        }
    }
}
