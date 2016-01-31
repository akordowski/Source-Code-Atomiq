using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Petzold.TextOnPath
{
	public class TextOnPathElement : TextOnPathBase
	{
		private Typeface typeface;

		private List<FormattedText> formattedChars = new List<FormattedText>();

		private double pathLength;

		private double textLength;

		public double TextLength
		{
			get
			{
				return this.textLength;
			}
		}

		public TextOnPathElement()
		{
			this.typeface = new Typeface(base.FontFamily, base.FontStyle, base.FontWeight, base.FontStretch);
		}

		protected override void OnFontPropertyChanged(DependencyPropertyChangedEventArgs args)
		{
			this.typeface = new Typeface(base.FontFamily, base.FontStyle, base.FontWeight, base.FontStretch);
			this.OnTextPropertyChanged(args);
		}

		protected override void OnForegroundPropertyChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnTextPropertyChanged(args);
		}

		protected override void OnTextPropertyChanged(DependencyPropertyChangedEventArgs args)
		{
			this.formattedChars.Clear();
			this.textLength = 0.0;
			string text = base.Text;
			for (int i = 0; i < text.Length; i++)
			{
				FormattedText formattedText = new FormattedText(text[i].ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, this.typeface, 16.0, base.Foreground);
				this.formattedChars.Add(formattedText);
				this.textLength += formattedText.WidthIncludingTrailingWhitespace;
			}
			base.InvalidateMeasure();
			base.InvalidateVisual();
		}

		protected override void OnPathPropertyChanged(DependencyPropertyChangedEventArgs args)
		{
			this.pathLength = TextOnPathBase.GetPathFigureLength(base.PathFigure);
			base.InvalidateMeasure();
			base.InvalidateVisual();
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			Size result;
			if (base.PathFigure == null)
			{
				result = this.MeasureOverride(availableSize);
			}
			else
			{
				result = (Size)new PathGeometry(new PathFigure[]
				{
					base.PathFigure
				}).Bounds.BottomRight;
			}
			return result;
		}

		protected override void OnRender(DrawingContext dc)
		{
			if (this.pathLength != 0.0 && this.textLength != 0.0)
			{
				double num = this.pathLength / this.textLength;
				num = 1.0;
				double num2 = 0.0;
				PathGeometry pathGeometry = new PathGeometry(new PathFigure[]
				{
					base.PathFigure
				});
				foreach (FormattedText current in this.formattedChars)
				{
					double num3 = num * current.WidthIncludingTrailingWhitespace;
					double num4 = num * current.Baseline;
					num2 += num3 / 2.0 / this.pathLength;
					Point point;
					Point point2;
					pathGeometry.GetPointAtFractionLength(num2, out point, out point2);
					dc.PushTransform(new TranslateTransform(point.X - num3 / 2.0, point.Y - num4));
					dc.PushTransform(new RotateTransform(Math.Atan2(point2.Y, point2.X) * 180.0 / 3.1415926535897931, num3 / 2.0, num4));
					dc.PushTransform(new ScaleTransform(num, num));
					dc.DrawText(current, new Point(0.0, 0.0));
					dc.Pop();
					dc.Pop();
					dc.Pop();
					num2 += num3 / 2.0 / this.pathLength;
				}
			}
		}
	}
}
