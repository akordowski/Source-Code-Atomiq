using System;
using System.Globalization;
using System.Windows.Data;

namespace CopyPasteKiller
{
	public class ScaleConverter : IValueConverter
	{
		private double double_0;

		private double double_1;

		private double double_2;

		private double double_3;

		public double OldMin
		{
			get
			{
				return this.double_0;
			}
			set
			{
				this.double_0 = value;
			}
		}

		public double OldMax
		{
			get
			{
				return this.double_1;
			}
			set
			{
				this.double_1 = value;
			}
		}

		public double NewMin
		{
			get
			{
				return this.double_2;
			}
			set
			{
				this.double_2 = value;
			}
		}

		public double NewMax
		{
			get
			{
				return this.double_3;
			}
			set
			{
				this.double_3 = value;
			}
		}

		public ScaleConverter(double oldMin, double oldMax, double newMin, double newMax)
		{
			this.double_0 = oldMin;
			this.double_1 = oldMax;
			this.double_2 = newMin;
			this.double_3 = newMax;
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			object result;
			if (value is double)
			{
				result = ScaleConverter.Scale((double)value, this.double_0, this.double_1, this.double_2, this.double_3);
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
