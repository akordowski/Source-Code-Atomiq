using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CopyPasteKiller
{
	public class CanonicalSpline : Shape
	{
		private PathGeometry _definingGeometry;

		public static readonly DependencyProperty PointsProperty;

		public static readonly DependencyProperty TensionProperty;

		public static readonly DependencyProperty TensionsProperty;

		public static readonly DependencyProperty IsClosedProperty;

		public static readonly DependencyProperty IsFilledProperty;

		public static readonly DependencyProperty FillRuleProperty;

		public static readonly DependencyProperty ToleranceProperty;

		public PointCollection Points
		{
			get
			{
				return (PointCollection)base.GetValue(CanonicalSpline.PointsProperty);
			}
			set
			{
				base.SetValue(CanonicalSpline.PointsProperty, value);
			}
		}

		public double Tension
		{
			get
			{
				return (double)base.GetValue(CanonicalSpline.TensionProperty);
			}
			set
			{
				base.SetValue(CanonicalSpline.TensionProperty, value);
			}
		}

		public DoubleCollection Tensions
		{
			get
			{
				return (DoubleCollection)base.GetValue(CanonicalSpline.TensionsProperty);
			}
			set
			{
				base.SetValue(CanonicalSpline.TensionsProperty, value);
			}
		}

		public bool IsClosed
		{
			get
			{
				return (bool)base.GetValue(CanonicalSpline.IsClosedProperty);
			}
			set
			{
				base.SetValue(CanonicalSpline.IsClosedProperty, value);
			}
		}

		public bool IsFilled
		{
			get
			{
				return (bool)base.GetValue(CanonicalSpline.IsFilledProperty);
			}
			set
			{
				base.SetValue(CanonicalSpline.IsFilledProperty, value);
			}
		}

		public FillRule FillRule
		{
			get
			{
				return (FillRule)base.GetValue(CanonicalSpline.FillRuleProperty);
			}
			set
			{
				base.SetValue(CanonicalSpline.FillRuleProperty, value);
			}
		}

		public double Tolerance
		{
			get
			{
				return (double)base.GetValue(CanonicalSpline.ToleranceProperty);
			}
			set
			{
				base.SetValue(CanonicalSpline.ToleranceProperty, value);
			}
		}

		protected override Geometry DefiningGeometry
		{
			get
			{
				return _definingGeometry;
			}
		}

		private static void smethod0(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
		{
			(dependencyObject as CanonicalSpline).method0(eventArgs);
		}

		private void method0(DependencyPropertyChangedEventArgs eventArgs)
		{
			_definingGeometry = CanonicalSplineHelper.smethod0(Points, Tension, Tensions, IsClosed, IsFilled, Tolerance);
			base.InvalidateMeasure();
			method1(eventArgs);
		}

		private static void smethod1(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
		{
			(dependencyObject as CanonicalSpline).method1(eventArgs);
		}

		private void method1(DependencyPropertyChangedEventArgs eventArgs)
		{
			if (_definingGeometry != null)
			{
				_definingGeometry.FillRule = FillRule;
			}

			base.InvalidateVisual();
		}

		static CanonicalSpline()
		{
			CanonicalSpline.PointsProperty = Polyline.PointsProperty.AddOwner(typeof(CanonicalSpline), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(CanonicalSpline.smethod0)));
			CanonicalSpline.TensionProperty = DependencyProperty.Register("Tension", typeof(double), typeof(CanonicalSpline), new FrameworkPropertyMetadata(0.5, new PropertyChangedCallback(CanonicalSpline.smethod0)));
			CanonicalSpline.TensionsProperty = DependencyProperty.Register("Tensions", typeof(DoubleCollection), typeof(CanonicalSpline), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(CanonicalSpline.smethod0)));
			CanonicalSpline.IsClosedProperty = PathFigure.IsClosedProperty.AddOwner(typeof(CanonicalSpline), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(CanonicalSpline.smethod0)));
			CanonicalSpline.IsFilledProperty = PathFigure.IsFilledProperty.AddOwner(typeof(CanonicalSpline), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(CanonicalSpline.smethod0)));
			CanonicalSpline.FillRuleProperty = Polyline.FillRuleProperty.AddOwner(typeof(CanonicalSpline), new FrameworkPropertyMetadata(FillRule.EvenOdd, new PropertyChangedCallback(CanonicalSpline.smethod1)));
			CanonicalSpline.ToleranceProperty = DependencyProperty.Register("Tolerance", typeof(double), typeof(CanonicalSpline), new FrameworkPropertyMetadata(Geometry.StandardFlatteningTolerance, new PropertyChangedCallback(CanonicalSpline.smethod0)));
		}
	}
}