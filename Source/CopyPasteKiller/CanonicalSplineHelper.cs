using System;
using System.Windows;
using System.Windows.Media;

namespace CopyPasteKiller
{
	internal static class CanonicalSplineHelper
	{
		internal static PathGeometry smethod_0(PointCollection pointCollection_0, double double_0, DoubleCollection doubleCollection_0, bool bool_0, bool bool_1, double double_1)
		{
			PathGeometry result;
			if (pointCollection_0 == null || pointCollection_0.Count < 1)
			{
				result = null;
			}
			else
			{
				PolyLineSegment polyLineSegment = new PolyLineSegment();
				PathFigure pathFigure = new PathFigure();
				pathFigure.IsClosed = bool_0;
				pathFigure.IsFilled = bool_1;
				pathFigure.StartPoint = pointCollection_0[0];
				pathFigure.Segments.Add(polyLineSegment);
				PathGeometry pathGeometry = new PathGeometry();
				pathGeometry.Figures.Add(pathFigure);
				if (pointCollection_0.Count < 2)
				{
					result = pathGeometry;
				}
				else
				{
					if (pointCollection_0.Count == 2)
					{
						if (!bool_0)
						{
							CanonicalSplineHelper.smethod_1(polyLineSegment.Points, pointCollection_0[0], pointCollection_0[0], pointCollection_0[1], pointCollection_0[1], double_0, double_0, double_1);
						}
						else
						{
							CanonicalSplineHelper.smethod_1(polyLineSegment.Points, pointCollection_0[1], pointCollection_0[0], pointCollection_0[1], pointCollection_0[0], double_0, double_0, double_1);
							CanonicalSplineHelper.smethod_1(polyLineSegment.Points, pointCollection_0[0], pointCollection_0[1], pointCollection_0[0], pointCollection_0[1], double_0, double_0, double_1);
						}
					}
					else
					{
						bool flag = doubleCollection_0 != null && doubleCollection_0.Count > 0;
						for (int i = 0; i < pointCollection_0.Count; i++)
						{
							double double_2 = flag ? doubleCollection_0[i % doubleCollection_0.Count] : double_0;
							double double_3 = flag ? doubleCollection_0[(i + 1) % doubleCollection_0.Count] : double_0;
							if (i == 0)
							{
								CanonicalSplineHelper.smethod_1(polyLineSegment.Points, bool_0 ? pointCollection_0[pointCollection_0.Count - 1] : pointCollection_0[0], pointCollection_0[0], pointCollection_0[1], pointCollection_0[2], double_2, double_3, double_1);
							}
							else if (i == pointCollection_0.Count - 2)
							{
								CanonicalSplineHelper.smethod_1(polyLineSegment.Points, pointCollection_0[i - 1], pointCollection_0[i], pointCollection_0[i + 1], bool_0 ? pointCollection_0[0] : pointCollection_0[i + 1], double_2, double_3, double_1);
							}
							else if (i == pointCollection_0.Count - 1)
							{
								if (bool_0)
								{
									CanonicalSplineHelper.smethod_1(polyLineSegment.Points, pointCollection_0[i - 1], pointCollection_0[i], pointCollection_0[0], pointCollection_0[1], double_2, double_3, double_1);
								}
							}
							else
							{
								CanonicalSplineHelper.smethod_1(polyLineSegment.Points, pointCollection_0[i - 1], pointCollection_0[i], pointCollection_0[i + 1], pointCollection_0[i + 2], double_2, double_3, double_1);
							}
						}
					}
					result = pathGeometry;
				}
			}
			return result;
		}

		private static void smethod_1(PointCollection pointCollection_0, Point point_0, Point point_1, Point point_2, Point point_3, double double_0, double double_1, double double_2)
		{
			double num = double_0 * (point_2.X - point_0.X);
			double num2 = double_0 * (point_2.Y - point_0.Y);
			double num3 = double_1 * (point_3.X - point_1.X);
			double num4 = double_1 * (point_3.Y - point_1.Y);
			double num5 = num + num3 + 2.0 * point_1.X - 2.0 * point_2.X;
			double num6 = num2 + num4 + 2.0 * point_1.Y - 2.0 * point_2.Y;
			double num7 = -2.0 * num - num3 - 3.0 * point_1.X + 3.0 * point_2.X;
			double num8 = -2.0 * num2 - num4 - 3.0 * point_1.Y + 3.0 * point_2.Y;
			double num9 = num;
			double num10 = num2;
			double x = point_1.X;
			double y = point_1.Y;
			int num11 = (int)((Math.Abs(point_1.X - point_2.X) + Math.Abs(point_1.Y - point_2.Y)) / double_2);
			for (int i = 1; i < num11; i++)
			{
				double num12 = (double)i / (double)(num11 - 1);
				Point value = new Point(num5 * num12 * num12 * num12 + num7 * num12 * num12 + num9 * num12 + x, num6 * num12 * num12 * num12 + num8 * num12 * num12 + num10 * num12 + y);
				pointCollection_0.Add(value);
			}
		}
	}
}
