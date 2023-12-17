using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using TodoList.Utils.Enums;

namespace TodoList.Utils.Converters
{
    public class CategoryToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ItemCategory category = (ItemCategory)value;

            try
            {
                if (Constants.CATEGORY_COLORS.ContainsKey(category))
                {
                    string colorHex = Constants.CATEGORY_COLORS[category];
                    return new BrushConverter().ConvertFrom(colorHex) as SolidColorBrush;
                }

                return Brushes.Black;
            }
            catch 
            {
                return Brushes.Black;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
