using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Petzold.TextOnPath
{
	public class SineCurve : Shape
	{
		private PolyLineSegment polyLineSegment;

		private PathGeometry pathGeometry;

		public static readonly DependencyProperty PeriodProperty = DependencyProperty.Register("Period", typeof(double), typeof(SineCurve), new FrameworkPropertyMetadata(360.0, new PropertyChangedCallback(SineCurve.OnRedrawPropertyChanged)));

		public static readonly DependencyProperty AmplitudeProperty = DependencyProperty.Register("Amplitude", typeof(double), typeof(SineCurve), new FrameworkPropertyMetadata(96.0, new PropertyChangedCallback(SineCurve.OnRedrawPropertyChanged)));

		public static readonly DependencyProperty PhaseProperty = DependencyProperty.Register("Phase", typeof(double), typeof(SineCurve), new FrameworkPropertyMetadata(0.0, new PropertyChangedCallback(SineCurve.OnRedrawPropertyChanged)));

		public static readonly DependencyProperty CyclesProperty = DependencyProperty.Register("Cycles", typeof(double), typeof(SineCurve), new FrameworkPropertyMetadata(1.0, new PropertyChangedCallback(SineCurve.OnRedrawPropertyChanged)));

		public static readonly DependencyProperty OriginProperty = DependencyProperty.Register("Origin", typeof(Point), typeof(SineCurve), new FrameworkPropertyMetadata(new Point(0.0, 96.0), new PropertyChangedCallback(SineCurve.OnRedrawPropertyChanged)));

		private static readonly DependencyPropertyKey PathFigureKey = DependencyProperty.RegisterReadOnly("PathFigure", typeof(PathFigure), typeof(SineCurve), new FrameworkPropertyMetadata(null));

		public static readonly DependencyProperty PathFigureProperty = SineCurve.PathFigureKey.DependencyProperty;

		public double Period
		{
			get
			{
				return (double)base.GetValue(SineCurve.PeriodProperty);
			}
			set
			{
				base.SetValue(SineCurve.PeriodProperty, value);
			}
		}

		public double Amplitude
		{
			get
			{
				return (double)base.GetValue(SineCurve.AmplitudeProperty);
			}
			set
			{
				base.SetValue(SineCurve.AmplitudeProperty, value);
			}
		}

		public double Phase
		{
			get
			{
				return (double)base.GetValue(SineCurve.PhaseProperty);
			}
			set
			{
				base.SetValue(SineCurve.PhaseProperty, value);
			}
		}

		public double Cycles
		{
			get
			{
				return (double)base.GetValue(SineCurve.CyclesProperty);
			}
			set
			{
				base.SetValue(SineCurve.CyclesProperty, value);
			}
		}

		public Point Origin
		{
			get
			{
				return (Point)base.GetValue(SineCurve.OriginProperty);
			}
			set
			{
				base.SetValue(SineCurve.OriginProperty, value);
			}
		}

		public PathFigure PathFigure
		{
			get
			{
				return (PathFigure)base.GetValue(SineCurve.PathFigureProperty);
			}
			protected set
			{
				base.SetValue(SineCurve.PathFigureKey, value);
			}
		}

		protected override Geometry DefiningGeometry
		{
			get
			{
				return this.pathGeometry;
			}
		}

		public SineCurve()
		{
			this.polyLineSegment = new PolyLineSegment();
			this.PathFigure = new PathFigure(default(Point), new PathSegment[]
			{
				this.polyLineSegment
			}, false);
			this.pathGeometry = new PathGeometry(new PathFigure[]
			{
				this.PathFigure
			});
			this.OnRedrawPropertyChanged(default(DependencyPropertyChangedEventArgs));
		}

		private static void OnRedrawPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			(obj as SineCurve).OnRedrawPropertyChanged(args);
		}

		private void OnRedrawPropertyChanged(DependencyPropertyChangedEventArgs args)
		{
			this.PathFigure.Segments.Clear();
			this.polyLineSegment.Points.Clear();
			for (double num = this.Origin.X; num <= this.Origin.X + this.Period * this.Cycles; num += 1.0)
			{
				double num2 = this.Phase + 360.0 * num / this.Period;
				double a = 3.1415926535897931 * num2 / 180.0;
				double y = this.Origin.Y - this.Amplitude * Math.Sin(a);
				if (num == this.Origin.X)
				{
					this.PathFigure.StartPoint = new Point(num, y);
				}
				else
				{
					this.polyLineSegment.Points.Add(new Point(num, y));
				}
			}
			this.PathFigure.Segments.Add(this.polyLineSegment);
		}
	}
}
