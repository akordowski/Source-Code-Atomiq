using System;
using System.Globalization;
using System.Windows.Data;

namespace CopyPasteKiller
{
	public class FoundConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			object result;

			if (!(value is int))
			{
				result = value;
			}
			else
			{
				int num = (int)value;
				result = num.NumShortener();
			}

			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}