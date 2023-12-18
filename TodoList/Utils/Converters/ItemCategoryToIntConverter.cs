using System.Globalization;
using System.Windows.Data;
using TodoList.Utils.Enums;

namespace TodoList.Utils.Converters
{
    public class ItemCategoryToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ItemCategory category)
            {
                return (int)category;
            }

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
