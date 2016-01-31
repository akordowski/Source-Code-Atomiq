using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.TextOnPath
{
	public class TextOnPathControl : UserControl
	{
		private const double FONTSIZE = 100.0;

		private Panel mainPanel;

		public static readonly DependencyProperty PathFigureProperty;

		public static readonly DependencyProperty TextProperty;

		public PathFigure PathFigure
		{
			get
			{
				return (PathFigure)base.GetValue(TextOnPathControl.PathFigureProperty);
			}
			set
			{
				base.SetValue(TextOnPathControl.PathFigureProperty, value);
			}
		}

		public string Text
		{
			get
			{
				return (string)base.GetValue(TextOnPathControl.TextProperty);
			}
			set
			{
				base.SetValue(TextOnPathControl.TextProperty, value);
			}
		}

		static TextOnPathControl()
		{
			TextOnPathControl.PathFigureProperty = DependencyProperty.Register("PathFigure", typeof(PathFigure), typeof(TextOnPathControl), new FrameworkPropertyMetadata(new PropertyChangedCallback(TextOnPathControl.OnPathPropertyChanged)));
			TextOnPathControl.TextProperty = TextBlock.TextProperty.AddOwner(typeof(TextOnPathControl), new FrameworkPropertyMetadata(new PropertyChangedCallback(TextOnPathControl.OnTextPropertyChanged)));
			Control.FontFamilyProperty.OverrideMetadata(typeof(TextOnPathControl), new FrameworkPropertyMetadata(new PropertyChangedCallback(TextOnPathControl.OnFontPropertyChanged)));
			Control.FontStyleProperty.OverrideMetadata(typeof(TextOnPathControl), new FrameworkPropertyMetadata(new PropertyChangedCallback(TextOnPathControl.OnFontPropertyChanged)));
			Control.FontWeightProperty.OverrideMetadata(typeof(TextOnPathControl), new FrameworkPropertyMetadata(new PropertyChangedCallback(TextOnPathControl.OnFontPropertyChanged)));
			Control.FontStretchProperty.OverrideMetadata(typeof(TextOnPathControl), new FrameworkPropertyMetadata(new PropertyChangedCallback(TextOnPathControl.OnFontPropertyChanged)));
		}

		public TextOnPathControl()
		{
			this.mainPanel = new Canvas();
			base.Content = this.mainPanel;
		}

		private static void OnFontPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			(obj as TextOnPathControl).OrientTextOnPath();
		}

		private static void OnPathPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			(obj as TextOnPathControl).OrientTextOnPath();
		}

		private static void OnTextPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			TextOnPathControl textOnPathControl = obj as TextOnPathControl;
			textOnPathControl.mainPanel.Children.Clear();
			if (!string.IsNullOrEmpty(textOnPathControl.Text))
			{
				string text = textOnPathControl.Text;
				for (int i = 0; i < text.Length; i++)
				{
					char c = text[i];
					TextBlock textBlock = new TextBlock();
					textBlock.Text = c.ToString();
					textBlock.FontSize = 100.0;
					textOnPathControl.mainPanel.Children.Add(textBlock);
				}
				textOnPathControl.OrientTextOnPath();
			}
		}

		private void OrientTextOnPath()
		{
			double pathFigureLength = TextOnPathBase.GetPathFigureLength(this.PathFigure);
			double num = 0.0;
			foreach (UIElement uIElement in this.mainPanel.Children)
			{
				uIElement.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
				num += uIElement.DesiredSize.Width;
			}
			if (pathFigureLength != 0.0 && num != 0.0)
			{
				double num2 = pathFigureLength / num;
				PathGeometry pathGeometry = new PathGeometry(new PathFigure[]
				{
					this.PathFigure
				});
				double num3 = num2 * 100.0 * base.FontFamily.Baseline;
				double num4 = 0.0;
				foreach (UIElement uIElement in this.mainPanel.Children)
				{
					double num5 = num2 * uIElement.DesiredSize.Width;
					num4 += num5 / 2.0 / pathFigureLength;
					Point point;
					Point point2;
					pathGeometry.GetPointAtFractionLength(num4, out point, out point2);
					uIElement.RenderTransform = new TransformGroup
					{
						Children = 
						{
							new ScaleTransform(num2, num2),
							new RotateTransform(Math.Atan2(point2.Y, point2.X) * 180.0 / 3.1415926535897931, num5 / 2.0, num3),
							new TranslateTransform(point.X - num5 / 2.0, point.Y - num3)
						}
					};
					num4 += num5 / 2.0 / pathFigureLength;
				}
			}
		}
	}
}
