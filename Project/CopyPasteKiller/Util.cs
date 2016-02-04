using System;
using System.Diagnostics;
using System.Windows.Media;

namespace CopyPasteKiller
{
	public static class Util
	{
		private const int int0 = 299;

		private const int int1 = 587;

		private const int int2 = 114;

		private const int int3 = 255;

		private const int int4 = 127;

		public static int count;

		public static Stopwatch stopwatch;

		public static Color CalculateForeColor(Color backColor)
		{
			int num = ((int)backColor.R * 299 + (int)backColor.G * 587 + (int)(backColor.B * 114)) / 1000;
			Color result;

			if (num <= 127)
			{
				result = Colors.White;
			}
			else
			{
				result = Colors.Black;
			}

			return result;
		}

		public static Color CalculateGradientColor(double percent, Color minColor, Color maxColor)
		{
			return Util.CalculateGradientColor(percent, 0.0, 100.0, minColor, maxColor);
		}

		public static Color CalculateGradientColor(double value, double min, double max, Color minColor, Color maxColor)
		{
			Util.count++;
			Util.stopwatch.Start();
			Color result;

			if (value < min)
			{
				result = minColor;
			}
			else if (value >= max)
			{
				result = maxColor;
			}
			else
			{
				int num = (int)(maxColor.R - minColor.R);
				int num2 = (int)(maxColor.G - minColor.G);
				int num3 = (int)(maxColor.B - minColor.B);
				double num4 = (value - min) / (max - min);
				byte r = (byte)(num4 * (double)num + (double)minColor.R);
				byte g = (byte)(num4 * (double)num2 + (double)minColor.G);
				byte b = (byte)(num4 * (double)num3 + (double)minColor.B);

				result = Color.FromArgb(255, r, g, b);
			}

			return result;
		}

		static Util()
		{
			Util.count = 0;
			Util.stopwatch = new Stopwatch();
		}
	}
}