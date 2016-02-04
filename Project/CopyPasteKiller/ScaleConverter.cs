using System;
using System.Globalization;
using System.Windows.Data;

namespace CopyPasteKiller
{
	public class ScaleConverter : IValueConverter
	{
		public double OldMin { get; set; }

		public double OldMax { get; set; }

		public double NewMin { get; set; }

		public double NewMax { get; set; }

		public ScaleConverter(double oldMin, double oldMax, double newMin, double newMax)
		{
			OldMin = oldMin;
			OldMax = oldMax;
			NewMin = newMin;
			NewMax = newMax;
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			object result;

			if (value is double)
			{
				result = ScaleConverter.Scale((double)value, OldMin, OldMax, NewMin, NewMax);
			}
			else
			{
				result = null;
			}

			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		public static double Scale(double number, double oldMin, double oldMax, double newMin, double newMax)
		{
			double num = (number - oldMin) / (oldMax - oldMin);
			return (newMax - newMin) * num + newMin;
		}
	}
}