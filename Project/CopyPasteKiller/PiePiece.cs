using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CopyPasteKiller
{
	internal class PiePiece : Shape
	{
		public static readonly DependencyProperty dependencyProperty_0;

		public static readonly DependencyProperty dependencyProperty_1;

		public static readonly DependencyProperty dependencyProperty_2;

		public static readonly DependencyProperty dependencyProperty_3;

		public static readonly DependencyProperty dependencyProperty_4;

		public static readonly DependencyProperty dependencyProperty_5;

		public static readonly DependencyProperty dependencyProperty_6;

		public static readonly DependencyProperty dependencyProperty_7;

		public static readonly DependencyProperty dependencyProperty_8;

		protected override Geometry DefiningGeometry
		{
			get
			{
				StreamGeometry streamGeometry = new StreamGeometry();
				streamGeometry.FillRule = FillRule.EvenOdd;
				using (StreamGeometryContext streamGeometryContext = streamGeometry.Open())
				{
					this.method_18(streamGeometryContext);
				}
				streamGeometry.Freeze();
				return streamGeometry;
			}
		}

		public double method_0()
		{
			return (double)base.GetValue(PiePiece.dependencyProperty_0);
		}

		public void method_1(double double_0)
		{
			base.SetValue(PiePiece.dependencyProperty_0, double_0);
		}

		public double method_2()
		{
			return (double)base.GetValue(PiePiece.dependencyProperty_1);
		}

		public void method_3(double double_0)
		{
			base.SetValue(PiePiece.dependencyProperty_1, double_0);
		}

		public double method_4()
		{
			return (double)base.GetValue(PiePiece.dependencyProperty_2);
		}

		public void method_5(double double_0)
		{
			base.SetValue(PiePiece.dependencyProperty_2, double_0);
		}

		public double method_6()
		{
			return (double)base.GetValue(PiePiece.dependencyProperty_3);
		}

		public void method_7(double double_0)
		{
			base.SetValue(PiePiece.dependencyProperty_3, double_0);
			this.method_15(double_0 / 360.0);
		}

		public double method_8()
		{
			return (double)base.GetValue(PiePiece.dependencyProperty_4);
		}

		public void method_9(double double_0)
		{
			base.SetValue(PiePiece.dependencyProperty_4, double_0);
		}

		public double method_10()
		{
			return (double)base.GetValue(PiePiece.dependencyProperty_5);
		}

		public void method_11(double double_0)
		{
			base.SetValue(PiePiece.dependencyProperty_5, double_0);
		}

		public double method_12()
		{
			return (double)base.GetValue(PiePiece.dependencyProperty_6);
		}

		public void method_13(double double_0)
		{
			base.SetValue(PiePiece.dependencyProperty_6, double_0);
		}

		public double method_14()
		{
			return (double)base.GetValue(PiePiece.dependencyProperty_7);
		}

		private void method_15(double double_0)
		{
			base.SetValue(PiePiece.dependencyProperty_7, double_0);
		}

		public double method_16()
		{
			return (double)base.GetValue(PiePiece.dependencyProperty_8);
		}

		public void method_17(double double_0)
		{
			base.SetValue(PiePiece.dependencyProperty_8, double_0);
		}

		private void method_18(StreamGeometryContext streamGeometryContext_0)
		{
			Point point = new Point(this.method_10(), this.method_12());
			Point point2 = Utils.ComputeCartesianCoordinate(this.method_8(), this.method_4());
			point2.Offset(this.method_10(), this.method_12());
			Point point3 = Utils.ComputeCartesianCoordinate(this.method_8() + this.method_6(), this.method_4());
			point3.Offset(this.method_10(), this.method_12());
			Point point4 = Utils.ComputeCartesianCoordinate(this.method_8(), this.method_0());
			point4.Offset(this.method_10(), this.method_12());
			Point point5 = Utils.ComputeCartesianCoordinate(this.method_8() + this.method_6(), this.method_0());
			point5.Offset(this.method_10(), this.method_12());
			bool isLargeArc = this.method_6() > 180.0;
			if (this.method_2() > 0.0)
			{
				Point point6 = Utils.ComputeCartesianCoordinate(this.method_8() + this.method_6() / 2.0, this.method_2());
				point2.Offset(point6.X, point6.Y);
				point3.Offset(point6.X, point6.Y);
				point4.Offset(point6.X, point6.Y);
				point5.Offset(point6.X, point6.Y);
			}
			Size size = new Size(this.method_0(), this.method_0());
			Size size2 = new Size(this.method_4(), this.method_4());
			streamGeometryContext_0.BeginFigure(point2, true, true);
			streamGeometryContext_0.LineTo(point4, true, true);
			streamGeometryContext_0.ArcTo(point5, size, 0.0, isLargeArc, SweepDirection.Clockwise, true, true);
			streamGeometryContext_0.LineTo(point3, true, true);
			streamGeometryContext_0.ArcTo(point2, size2, 0.0, isLargeArc, SweepDirection.Counterclockwise, true, true);
		}

		public PathFigure method_19(double double_0, double double_1)
		{
			double num = this.method_4() + double_0;
			Point start = Utils.ComputeCartesianCoordinate(this.method_8() + (this.method_6() - double_1) / 2.0, num);
			start.Offset(this.method_10(), this.method_12());
			Point point = Utils.ComputeCartesianCoordinate(this.method_8() + (this.method_6() - double_1) / 2.0 + double_1, num);
			point.Offset(this.method_10(), this.method_12());
			Size size = new Size(num, num);
			bool isLargeArc = double_1 > 180.0;
			List<PathSegment> segments = new List<PathSegment>
			{
				new ArcSegment(point, size, 0.0, isLargeArc, SweepDirection.Clockwise, true)
			};
			return new PathFigure(start, segments, false);
		}

		public PathFigure method_20(double double_0, double double_1)
		{
			double num = this.method_0() - double_0;
			bool isLargeArc = double_1 > 180.0;
			Point point = Utils.ComputeCartesianCoordinate(this.method_8() + (this.method_6() - double_1) / 2.0, num);
			point.Offset(this.method_10(), this.method_12());
			Point start = Utils.ComputeCartesianCoordinate(this.method_8() + (this.method_6() - double_1) / 2.0 + double_1, num);
			start.Offset(this.method_10(), this.method_12());
			Size size = new Size(num, num);
			List<PathSegment> segments = new List<PathSegment>
			{
				new ArcSegment(point, size, 0.0, isLargeArc, SweepDirection.Counterclockwise, false)
			};
			return new PathFigure(start, segments, false);
		}

		static PiePiece()
		{
			PiePiece.dependencyProperty_0 = DependencyProperty.Register("RadiusProperty", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
			PiePiece.dependencyProperty_1 = DependencyProperty.Register("PushOutProperty", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
			PiePiece.dependencyProperty_2 = DependencyProperty.Register("InnerRadiusProperty", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
			PiePiece.dependencyProperty_3 = DependencyProperty.Register("WedgeAngleProperty", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
			PiePiece.dependencyProperty_4 = DependencyProperty.Register("RotationAngleProperty", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
			PiePiece.dependencyProperty_5 = DependencyProperty.Register("CentreXProperty", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
			PiePiece.dependencyProperty_6 = DependencyProperty.Register("CentreYProperty", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
			PiePiece.dependencyProperty_7 = DependencyProperty.Register("PercentageProperty", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0));
			PiePiece.dependencyProperty_8 = DependencyProperty.Register("PieceValueProperty", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0));
		}
	}
}
