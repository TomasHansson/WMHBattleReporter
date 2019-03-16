using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace WMHBattleReporter.View
{
    public class ListItemToPositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ListBoxItem item)
            {
                ListBox listBox = FindAncestor<ListBox>(item);
                if (listBox != null)
                {
                    int index = listBox.Items.IndexOf(item.Content);
                    return index + 1;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static T FindAncestor<T>(DependencyObject from) where T : class
        {
            if (from == null)
                return null;

            var candidate = from as T;
            return candidate ?? FindAncestor<T>(VisualTreeHelper.GetParent(from));
        }
    }
}
