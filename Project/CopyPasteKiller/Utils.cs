using System;
using System.Windows;

namespace CopyPasteKiller
{
	public static class Utils
	{
		public static Point ComputeCartesianCoordinate(double angle, double radius)
		{
			double num = 0.017453292519943295 * (angle - 90.0);
			double x = radius * Math.Cos(num);
			double y = radius * Math.Sin(num);

			return new Point(x, y);
		}

		public static Point ComputeCartesianCoordinate(double angle, double radius, double centerX, double centerY)
		{
			Point result = Utils.ComputeCartesianCoordinate(angle, radius);
			Point point = new Point(centerX, centerY);
			result.Offset(point.X, point.Y);

			return result;
		}
	}
}