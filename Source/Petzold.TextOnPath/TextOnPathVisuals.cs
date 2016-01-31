using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Petzold.TextOnPath
{
	public class TextOnPathVisuals : TextOnPathBase
	{
		private Typeface typeface;

		protected VisualCollection visualChildren;

		protected List<FormattedText> formattedChars = new List<FormattedText>();

		protected double pathLength;

		protected double textLength;

		protected Rect boundingRect = default(Rect);

		protected override int VisualChildrenCount
		{
			get
			{
				return this.visualChildren.Count;
			}
		}

		public TextOnPathVisuals()
		{
			this.typeface = new Typeface(base.FontFamily, base.FontStyle, base.FontWeight, base.FontStretch);
			this.visualChildren = new VisualCollection(this);
		}

		protected override void OnPathPropertyChanged(DependencyPropertyChangedEventArgs args)
		{
			this.pathLength = TextOnPathBase.GetPathFigureLength(base.PathFigure);
			this.TransformVisualChildren();
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
				FormattedText formattedText = new FormattedText(text[i].ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, this.typeface, 100.0, base.Foreground);
				this.formattedChars.Add(formattedText);
				this.textLength += formattedText.WidthIncludingTrailingWhitespace;
			}
			this.GenerateVisualChildren();
		}

		protected virtual void GenerateVisualChildren()
		{
			this.visualChildren.Clear();
			foreach (FormattedText current in this.formattedChars)
			{
				DrawingVisual drawingVisual = new DrawingVisual();
				drawingVisual.Transform = new TransformGroup
				{
					Children = 
					{
						new ScaleTransform(),
						new RotateTransform(),
						new TranslateTransform()
					}
				};
				DrawingContext drawingContext = drawingVisual.RenderOpen();
				drawingContext.DrawText(current, new Point(0.0, 0.0));
				drawingContext.Close();
				this.visualChildren.Add(drawingVisual);
			}
			this.TransformVisualChildren();
		}

		protected virtual void TransformVisualChildren()
		{
			this.boundingRect = default(Rect);
			if (this.pathLength != 0.0 && this.textLength != 0.0)
			{
				if (this.formattedChars.Count == this.visualChildren.Count)
				{
					double num = this.pathLength / this.textLength;
					PathGeometry pathGeometry = new PathGeometry(new PathFigure[]
					{
						base.PathFigure
					});
					double num2 = 0.0;
					this.boundingRect = default(Rect);
					for (int i = 0; i < this.visualChildren.Count; i++)
					{
						FormattedText formattedText = this.formattedChars[i];
						double num3 = num * formattedText.WidthIncludingTrailingWhitespace;
						double num4 = num * formattedText.Baseline;
						num2 += num3 / 2.0 / this.pathLength;
						Point point;
						Point point2;
						pathGeometry.GetPointAtFractionLength(num2, out point, out point2);
						DrawingVisual drawingVisual = this.visualChildren[i] as DrawingVisual;
						TransformGroup transformGroup = drawingVisual.Transform as TransformGroup;
						ScaleTransform scaleTransform = transformGroup.Children[0] as ScaleTransform;
						RotateTransform rotateTransform = transformGroup.Children[1] as RotateTransform;
						TranslateTransform translateTransform = transformGroup.Children[2] as TranslateTransform;
						scaleTransform.ScaleX = num;
						scaleTransform.ScaleY = num;
						rotateTransform.Angle = Math.Atan2(point2.Y, point2.X) * 180.0 / 3.1415926535897931;
						rotateTransform.CenterX = num3 / 2.0;
						rotateTransform.CenterY = num4;
						translateTransform.X = point.X - num3 / 2.0;
						translateTransform.Y = point.Y - num4;
						Rect contentBounds = drawingVisual.ContentBounds;
						contentBounds.Transform(transformGroup.Value);
						this.boundingRect.Union(contentBounds);
						num2 += num3 / 2.0 / this.pathLength;
					}
					base.InvalidateMeasure();
				}
			}
		}

		protected override Visual GetVisualChild(int index)
		{
			if (index < 0 || index >= this.visualChildren.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return this.visualChildren[index];
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			return (Size)this.boundingRect.BottomRight;
		}
	}
}
