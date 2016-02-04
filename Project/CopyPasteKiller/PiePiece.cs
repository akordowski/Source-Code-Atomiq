using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CopyPasteKiller
{
	internal class PiePiece : Shape
	{
		public static readonly DependencyProperty dependencyProperty0;

		public static readonly DependencyProperty dependencyProperty1;

		public static readonly DependencyProperty dependencyProperty2;

		public static readonly DependencyProperty dependencyProperty3;

		public static readonly DependencyProperty dependencyProperty4;

		public static readonly DependencyProperty dependencyProperty5;

		public static readonly DependencyProperty dependencyProperty6;

		public static readonly DependencyProperty dependencyProperty7;

		public static readonly DependencyProperty dependencyProperty8;

		protected override Geometry DefiningGeometry
		{
			get
			{
				StreamGeometry streamGeometry = new StreamGeometry();
				streamGeometry.FillRule = FillRule.EvenOdd;

				using (StreamGeometryContext streamGeometryContext = streamGeometry.Open())
				{
					method18(streamGeometryContext);
				}

				streamGeometry.Freeze();

				return streamGeometry;
			}
		}

		public double method0()
		{
			return (double)base.GetValue(PiePiece.dependencyProperty0);
		}

		public void method1(double double0)
		{
			base.SetValue(PiePiece.dependencyProperty0, double0);
		}

		public double method2()
		{
			return (double)base.GetValue(PiePiece.dependencyProperty1);
		}

		public void method3(double double0)
		{
			base.SetValue(PiePiece.dependencyProperty1, double0);
		}

		public double method4()
		{
			return (double)base.GetValue(PiePiece.dependencyProperty2);
		}

		public void method5(double double0)
		{
			base.SetValue(PiePiece.dependencyProperty2, double0);
		}

		public double method6()
		{
			return (double)base.GetValue(PiePiece.dependencyProperty3);
		}

		public void method7(double double0)
		{
			base.SetValue(PiePiece.dependencyProperty3, double0);
			method15(double0 / 360.0);
		}

		public double method8()
		{
			return (double)base.GetValue(PiePiece.dependencyProperty4);
		}

		public void method9(double double0)
		{
			base.SetValue(PiePiece.dependencyProperty4, double0);
		}

		public double method10()
		{
			return (double)base.GetValue(PiePiece.dependencyProperty5);
		}

		public void method11(double double0)
		{
			base.SetValue(PiePiece.dependencyProperty5, double0);
		}

		public double method12()
		{
			return (double)base.GetValue(PiePiece.dependencyProperty6);
		}

		public void method13(double double0)
		{
			base.SetValue(PiePiece.dependencyProperty6, double0);
		}

		public double method14()
		{
			return (double)base.GetValue(PiePiece.dependencyProperty7);
		}

		private void method15(double double0)
		{
			base.SetValue(PiePiece.dependencyProperty7, double0);
		}

		public double method16()
		{
			return (double)base.GetValue(PiePiece.dependencyProperty8);
		}

		public void method17(double double0)
		{
			base.SetValue(PiePiece.dependencyProperty8, double0);
		}

		private void method18(StreamGeometryContext streamGeometryContext_0)
		{
			Point point = new Point(method10(), method12());
			Point point2 = Utils.ComputeCartesianCoordinate(method8(), method4());
			point2.Offset(method10(), method12());
			Point point3 = Utils.ComputeCartesianCoordinate(method8() + method6(), method4());
			point3.Offset(method10(), method12());
			Point point4 = Utils.ComputeCartesianCoordinate(method8(), method0());
			point4.Offset(method10(), method12());
			Point point5 = Utils.ComputeCartesianCoordinate(method8() + method6(), method0());
			point5.Offset(method10(), method12());
			bool isLargeArc = method6() > 180.0;

			if (method2() > 0.0)
			{
				Point point6 = Utils.ComputeCartesianCoordinate(method8() + method6() / 2.0, method2());
				point2.Offset(point6.X, point6.Y);
				point3.Offset(point6.X, point6.Y);
				point4.Offset(point6.X, point6.Y);
				point5.Offset(point6.X, point6.Y);
			}

			Size size = new Size(method0(), method0());
			Size size2 = new Size(method4(), method4());

			streamGeometryContext_0.BeginFigure(point2, true, true);
			streamGeometryContext_0.LineTo(point4, true, true);
			streamGeometryContext_0.ArcTo(point5, size, 0.0, isLargeArc, SweepDirection.Clockwise, true, true);
			streamGeometryContext_0.LineTo(point3, true, true);
			streamGeometryContext_0.ArcTo(point2, size2, 0.0, isLargeArc, SweepDirection.Counterclockwise, true, true);
		}

		public PathFigure method19(double double0, double double1)
		{
			double num = method4() + double0;
			Point start = Utils.ComputeCartesianCoordinate(method8() + (method6() - double1) / 2.0, num);
			start.Offset(method10(), method12());
			Point point = Utils.ComputeCartesianCoordinate(method8() + (method6() - double1) / 2.0 + double1, num);
			point.Offset(method10(), method12());
			Size size = new Size(num, num);
			bool isLargeArc = double1 > 180.0;

			List<PathSegment> segments = new List<PathSegment>
			{
				new ArcSegment(point, size, 0.0, isLargeArc, SweepDirection.Clockwise, true)
			};

			return new PathFigure(start, segments, false);
		}

		public PathFigure method20(double double0, double double1)
		{
			double num = method0() - double0;
			bool isLargeArc = double1 > 180.0;
			Point point = Utils.ComputeCartesianCoordinate(method8() + (method6() - double1) / 2.0, num);
			point.Offset(method10(), method12());
			Point start = Utils.ComputeCartesianCoordinate(method8() + (method6() - double1) / 2.0 + double1, num);
			start.Offset(method10(), method12());
			Size size = new Size(num, num);

			List<PathSegment> segments = new List<PathSegment>
			{
				new ArcSegment(point, size, 0.0, isLargeArc, SweepDirection.Counterclockwise, false)
			};

			return new PathFigure(start, segments, false);
		}

		static PiePiece()
		{
			PiePiece.dependencyProperty0 = DependencyProperty.Register("RadiusProperty", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
			PiePiece.dependencyProperty1 = DependencyProperty.Register("PushOutProperty", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
			PiePiece.dependencyProperty2 = DependencyProperty.Register("InnerRadiusProperty", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
			PiePiece.dependencyProperty3 = DependencyProperty.Register("WedgeAngleProperty", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
			PiePiece.dependencyProperty4 = DependencyProperty.Register("RotationAngleProperty", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
			PiePiece.dependencyProperty5 = DependencyProperty.Register("CentreXProperty", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
			PiePiece.dependencyProperty6 = DependencyProperty.Register("CentreYProperty", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
			PiePiece.dependencyProperty7 = DependencyProperty.Register("PercentageProperty", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0));
			PiePiece.dependencyProperty8 = DependencyProperty.Register("PieceValueProperty", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0));
		}
	}
}