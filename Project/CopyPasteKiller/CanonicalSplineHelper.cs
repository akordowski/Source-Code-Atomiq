using System;
using System.Windows;
using System.Windows.Media;

namespace CopyPasteKiller
{
	internal static class CanonicalSplineHelper
	{
		internal static PathGeometry smethod0(PointCollection pointCollection, double double0, DoubleCollection doubleCollection, bool bool0, bool bool1, double double1)
		{
			PathGeometry result;

			if (pointCollection == null || pointCollection.Count < 1)
			{
				result = null;
			}
			else
			{
				PolyLineSegment polyLineSegment = new PolyLineSegment();

				PathFigure pathFigure = new PathFigure();
				pathFigure.IsClosed = bool0;
				pathFigure.IsFilled = bool1;
				pathFigure.StartPoint = pointCollection[0];
				pathFigure.Segments.Add(polyLineSegment);

				PathGeometry pathGeometry = new PathGeometry();
				pathGeometry.Figures.Add(pathFigure);

				if (pointCollection.Count < 2)
				{
					result = pathGeometry;
				}
				else
				{
					if (pointCollection.Count == 2)
					{
						if (!bool0)
						{
							CanonicalSplineHelper.smethod1(polyLineSegment.Points, pointCollection[0], pointCollection[0], pointCollection[1], pointCollection[1], double0, double0, double1);
						}
						else
						{
							CanonicalSplineHelper.smethod1(polyLineSegment.Points, pointCollection[1], pointCollection[0], pointCollection[1], pointCollection[0], double0, double0, double1);
							CanonicalSplineHelper.smethod1(polyLineSegment.Points, pointCollection[0], pointCollection[1], pointCollection[0], pointCollection[1], double0, double0, double1);
						}
					}
					else
					{
						bool flag = doubleCollection != null && doubleCollection.Count > 0;

						for (int i = 0; i < pointCollection.Count; i++)
						{
							double double2 = flag ? doubleCollection[i % doubleCollection.Count] : double0;
							double double3 = flag ? doubleCollection[(i + 1) % doubleCollection.Count] : double0;

							if (i == 0)
							{
								CanonicalSplineHelper.smethod1(polyLineSegment.Points, bool0 ? pointCollection[pointCollection.Count - 1] : pointCollection[0], pointCollection[0], pointCollection[1], pointCollection[2], double2, double3, double1);
							}
							else if (i == pointCollection.Count - 2)
							{
								CanonicalSplineHelper.smethod1(polyLineSegment.Points, pointCollection[i - 1], pointCollection[i], pointCollection[i + 1], bool0 ? pointCollection[0] : pointCollection[i + 1], double2, double3, double1);
							}
							else if (i == pointCollection.Count - 1)
							{
								if (bool0)
								{
									CanonicalSplineHelper.smethod1(polyLineSegment.Points, pointCollection[i - 1], pointCollection[i], pointCollection[0], pointCollection[1], double2, double3, double1);
								}
							}
							else
							{
								CanonicalSplineHelper.smethod1(polyLineSegment.Points, pointCollection[i - 1], pointCollection[i], pointCollection[i + 1], pointCollection[i + 2], double2, double3, double1);
							}
						}
					}

					result = pathGeometry;
				}
			}

			return result;
		}

		private static void smethod1(PointCollection pointCollection, Point point0, Point point1, Point point2, Point point3, double double0, double double1, double double2)
		{
			double num = double0 * (point2.X - point0.X);
			double num2 = double0 * (point2.Y - point0.Y);
			double num3 = double1 * (point3.X - point1.X);
			double num4 = double1 * (point3.Y - point1.Y);
			double num5 = num + num3 + 2.0 * point1.X - 2.0 * point2.X;
			double num6 = num2 + num4 + 2.0 * point1.Y - 2.0 * point2.Y;
			double num7 = -2.0 * num - num3 - 3.0 * point1.X + 3.0 * point2.X;
			double num8 = -2.0 * num2 - num4 - 3.0 * point1.Y + 3.0 * point2.Y;
			double num9 = num;
			double num10 = num2;
			double x = point1.X;
			double y = point1.Y;
			int num11 = (int)((Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y)) / double2);

			for (int i = 1; i < num11; i++)
			{
				double num12 = (double)i / (double)(num11 - 1);
				Point value = new Point(num5 * num12 * num12 * num12 + num7 * num12 * num12 + num9 * num12 + x, num6 * num12 * num12 * num12 + num8 * num12 * num12 + num10 * num12 + y);
				pointCollection.Add(value);
			}
		}
	}
}