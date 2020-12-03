using NBAManagement.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NBAManagement.Converter
{
    public class MatchupTypeEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var seasonTypeValue = (MatchupTypeEnum)value;
            switch(seasonTypeValue)
            {
            case MatchupTypeEnum.Preseason: return "Preseason Season";
            case MatchupTypeEnum.Regular:   return "Regular Season";
            case MatchupTypeEnum.Post:      return "Post Season";
            default: return new ArgumentException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = (string)value;
            switch(strValue)
            {
            case "Preseason Season": return MatchupTypeEnum.Preseason;
            case "Regular Season":   return MatchupTypeEnum.Regular;
            case "Post Season":      return MatchupTypeEnum.Post;
            default: return new ArgumentException();
            }
        }
    }
}
