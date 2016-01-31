using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Petzold.TextOnPath
{
	public abstract class TextOnPathBase : FrameworkElement
	{
		public static readonly DependencyProperty FontFamilyProperty = TextElement.FontFamilyProperty.AddOwner(typeof(TextOnPathBase), new FrameworkPropertyMetadata(new PropertyChangedCallback(TextOnPathBase.OnFontPropertyChanged)));

		public static readonly DependencyProperty FontStyleProperty = TextElement.FontStyleProperty.AddOwner(typeof(TextOnPathBase), new FrameworkPropertyMetadata(new PropertyChangedCallback(TextOnPathBase.OnFontPropertyChanged)));

		public static readonly DependencyProperty FontWeightProperty = TextElement.FontWeightProperty.AddOwner(typeof(TextOnPathBase), new FrameworkPropertyMetadata(new PropertyChangedCallback(TextOnPathBase.OnFontPropertyChanged)));

		public static readonly DependencyProperty FontStretchProperty = TextElement.FontStretchProperty.AddOwner(typeof(TextOnPathBase), new FrameworkPropertyMetadata(new PropertyChangedCallback(TextOnPathBase.OnFontPropertyChanged)));

		public static readonly DependencyProperty ForegroundProperty = TextElement.ForegroundProperty.AddOwner(typeof(TextOnPathBase), new FrameworkPropertyMetadata(new PropertyChangedCallback(TextOnPathBase.OnForegroundPropertyChanged)));

		public static readonly DependencyProperty TextProperty = TextBlock.TextProperty.AddOwner(typeof(TextOnPathBase), new FrameworkPropertyMetadata(new PropertyChangedCallback(TextOnPathBase.OnTextPropertyChanged)));

		public static readonly DependencyProperty PathFigureProperty = DependencyProperty.Register("PathFigure", typeof(PathFigure), typeof(TextOnPathBase), new FrameworkPropertyMetadata(new PropertyChangedCallback(TextOnPathBase.OnPathPropertyChanged)));

		public FontFamily FontFamily
		{
			get
			{
				return (FontFamily)base.GetValue(TextOnPathBase.FontFamilyProperty);
			}
			set
			{
				base.SetValue(TextOnPathBase.FontFamilyProperty, value);
			}
		}

		public FontStyle FontStyle
		{
			get
			{
				return (FontStyle)base.GetValue(TextOnPathBase.FontStyleProperty);
			}
			set
			{
				base.SetValue(TextOnPathBase.FontStyleProperty, value);
			}
		}

		public FontWeight FontWeight
		{
			get
			{
				return (FontWeight)base.GetValue(TextOnPathBase.FontWeightProperty);
			}
			set
			{
				base.SetValue(TextOnPathBase.FontWeightProperty, value);
			}
		}

		public FontStretch FontStretch
		{
			get
			{
				return (FontStretch)base.GetValue(TextOnPathBase.FontStretchProperty);
			}
			set
			{
				base.SetValue(TextOnPathBase.FontStretchProperty, value);
			}
		}

		public Brush Foreground
		{
			get
			{
				return (Brush)base.GetValue(TextOnPathBase.ForegroundProperty);
			}
			set
			{
				base.SetValue(TextOnPathBase.ForegroundProperty, value);
			}
		}

		public string Text
		{
			get
			{
				return (string)base.GetValue(TextOnPathBase.TextProperty);
			}
			set
			{
				base.SetValue(TextOnPathBase.TextProperty, value);
			}
		}

		public PathFigure PathFigure
		{
			get
			{
				return (PathFigure)base.GetValue(TextOnPathBase.PathFigureProperty);
			}
			set
			{
				base.SetValue(TextOnPathBase.PathFigureProperty, value);
			}
		}

		private static void OnFontPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			(obj as TextOnPathBase).OnFontPropertyChanged(args);
		}

		protected abstract void OnFontPropertyChanged(DependencyPropertyChangedEventArgs args);

		private static void OnForegroundPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			(obj as TextOnPathBase).OnForegroundPropertyChanged(args);
		}

		protected abstract void OnForegroundPropertyChanged(DependencyPropertyChangedEventArgs args);

		private static void OnTextPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			(obj as TextOnPathBase).OnTextPropertyChanged(args);
		}

		protected abstract void OnTextPropertyChanged(DependencyPropertyChangedEventArgs args);

		private static void OnPathPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			(obj as TextOnPathBase).OnPathPropertyChanged(args);
		}

		protected abstract void OnPathPropertyChanged(DependencyPropertyChangedEventArgs args);

		public static double GetPathFigureLength(PathFigure pathFigure)
		{
			double result;
			if (pathFigure == null)
			{
				result = 0.0;
			}
			else
			{
				bool flag = true;
				foreach (PathSegment current in pathFigure.Segments)
				{
					if (!(current is PolyLineSegment) && !(current is LineSegment))
					{
						flag = false;
						break;
					}
				}
				PathFigure pathFigure2 = flag ? pathFigure : pathFigure.GetFlattenedPathFigure();
				double num = 0.0;
				Point point = pathFigure2.StartPoint;
				foreach (PathSegment current in pathFigure2.Segments)
				{
					if (current is LineSegment)
					{
						Point point2 = (current as LineSegment).Point;
						num += (point2 - point).Length;
						point = point2;
					}
					else if (current is PolyLineSegment)
					{
						PointCollection points = (current as PolyLineSegment).Points;
						foreach (Point point2 in points)
						{
							num += (point2 - point).Length;
							point = point2;
						}
					}
				}
				result = num;
			}
			return result;
		}
	}
}
