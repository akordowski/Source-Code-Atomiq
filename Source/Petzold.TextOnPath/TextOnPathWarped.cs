using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Petzold.TextOnPath
{
	public class TextOnPathWarped : TextOnPathBase
	{
		private delegate Point ProcessPoint(Point pointSrc);

		private Typeface typeface;

		private double pathLength;

		private double textLength;

		private double baseline;

		private PathGeometry flattenedTextPathGeometry;

		private PathGeometry warpedTextPathGeometry;

		public static readonly DependencyProperty ShiftToOriginProperty = DependencyProperty.Register("ShiftToOrigin", typeof(bool), typeof(TextOnPathWarped), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(TextOnPathWarped.OnShiftPropertyChanged)));

		public bool ShiftToOrigin
		{
			get
			{
				return (bool)base.GetValue(TextOnPathWarped.ShiftToOriginProperty);
			}
			set
			{
				base.SetValue(TextOnPathWarped.ShiftToOriginProperty, value);
			}
		}

		public TextOnPathWarped()
		{
			this.typeface = new Typeface(base.FontFamily, base.FontStyle, base.FontWeight, base.FontStretch);
		}

		protected override void OnPathPropertyChanged(DependencyPropertyChangedEventArgs args)
		{
			if (base.PathFigure != null)
			{
				this.pathLength = TextOnPathBase.GetPathFigureLength(base.PathFigure);
				this.GenerateWarpedGeometry();
			}
		}

		protected override void OnFontPropertyChanged(DependencyPropertyChangedEventArgs args)
		{
			this.typeface = new Typeface(base.FontFamily, base.FontStyle, base.FontWeight, base.FontStretch);
			this.OnTextPropertyChanged(args);
		}

		protected override void OnForegroundPropertyChanged(DependencyPropertyChangedEventArgs args)
		{
			base.InvalidateVisual();
		}

		protected override void OnTextPropertyChanged(DependencyPropertyChangedEventArgs args)
		{
			if (string.IsNullOrEmpty(base.Text))
			{
				this.flattenedTextPathGeometry = null;
			}
			else
			{
				FormattedText formattedText = new FormattedText(base.Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, this.typeface, 100.0, base.Foreground);
				this.textLength = formattedText.Width;
				this.baseline = formattedText.Baseline;
				Geometry geometry = formattedText.BuildGeometry(default(Point));
				this.flattenedTextPathGeometry = PathGeometry.CreateFromGeometry(geometry).GetFlattenedPathGeometry();
				this.warpedTextPathGeometry = this.flattenedTextPathGeometry.CloneCurrentValue();
				this.GenerateWarpedGeometry();
			}
		}

		private static void OnShiftPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			(obj as TextOnPathWarped).GenerateWarpedGeometry();
		}

		private void GenerateWarpedGeometry()
		{
			if (base.PathFigure != null && this.flattenedTextPathGeometry != null)
			{
				if (this.pathLength != 0.0 && this.textLength != 0.0)
				{
					this.WarpAllPoints(this.warpedTextPathGeometry, this.flattenedTextPathGeometry);
					base.InvalidateMeasure();
				}
			}
		}

		private void WarpAllPoints(PathGeometry warpedTextPathGeometry, PathGeometry flattenedTextPathGeometry)
		{
			PathGeometry pathGeometry = new PathGeometry(new PathFigure[]
			{
				base.PathFigure
			});
			double scalingFactor = this.pathLength / this.textLength;
			this.LoopThroughAllFlattenedPathPoints(warpedTextPathGeometry, flattenedTextPathGeometry, delegate(Point pointSrc)
			{
				double progress = Math.Max(0.0, Math.Min(1.0, pointSrc.X / this.textLength));
				double num = scalingFactor * (this.baseline - pointSrc.Y);
				Point point;
				Point point2;
				pathGeometry.GetPointAtFractionLength(progress, out point, out point2);
				double num2 = Math.Atan2(point2.Y, point2.X);
				return new Point
				{
					X = point.X + num * Math.Sin(num2),
					Y = point.Y - num * Math.Cos(num2)
				};
			});
			if (this.ShiftToOrigin)
			{
				Rect boundsRect = warpedTextPathGeometry.Bounds;
				this.LoopThroughAllFlattenedPathPoints(warpedTextPathGeometry, warpedTextPathGeometry, (Point pointSrc) => new Point
				{
					X = pointSrc.X - boundsRect.Left,
					Y = pointSrc.Y - boundsRect.Top
				});
			}
		}

		private void LoopThroughAllFlattenedPathPoints(PathGeometry pathGeometryDst, PathGeometry pathGeometrySrc, TextOnPathWarped.ProcessPoint callback)
		{
			for (int i = 0; i < pathGeometrySrc.Figures.Count; i++)
			{
				PathFigure pathFigure = pathGeometrySrc.Figures[i];
				PathFigure pathFigure2 = pathGeometryDst.Figures[i];
				pathFigure2.StartPoint = callback(pathFigure.StartPoint);
				for (int j = 0; j < pathFigure.Segments.Count; j++)
				{
					PathSegment pathSegment = pathFigure.Segments[j];
					PathSegment pathSegment2 = pathFigure2.Segments[j];
					if (pathSegment is LineSegment)
					{
						LineSegment lineSegment = pathSegment as LineSegment;
						LineSegment lineSegment2 = pathSegment2 as LineSegment;
						lineSegment2.Point = callback(lineSegment.Point);
					}
					else if (pathSegment is PolyLineSegment)
					{
						PointCollection points = (pathSegment as PolyLineSegment).Points;
						PointCollection points2 = (pathSegment2 as PolyLineSegment).Points;
						for (int k = 0; k < points.Count; k++)
						{
							points2[k] = callback(points[k]);
						}
					}
				}
			}
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			Size result;
			if (this.warpedTextPathGeometry != null)
			{
				result = (Size)this.warpedTextPathGeometry.Bounds.BottomRight;
			}
			else
			{
				result = this.MeasureOverride(availableSize);
			}
			return result;
		}

		protected override void OnRender(DrawingContext dc)
		{
			if (this.warpedTextPathGeometry != null)
			{
				dc.DrawGeometry(base.Foreground, null, this.warpedTextPathGeometry);
			}
		}
	}
}
