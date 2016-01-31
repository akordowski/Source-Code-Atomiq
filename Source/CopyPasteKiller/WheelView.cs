using Petzold.TextOnPath;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CopyPasteKiller
{
	public class WheelView : Window, IComponentConnector
	{
		private WheelViewModel wheelViewModel_0;

		private CodeDir codeDir_0;

		private Style style_0;

		private Style style_1;

		private static Brush brush_0;

		private static Brush brush_1;

		private double double_0 = 470.0;

		private double double_1 = 470.0;

		private double double_2 = 20.0;

		private double double_3 = 5.0;

		private double double_4 = 310.0;

		private double double_5;

		private EventHandler<SimilaritySelectedEventArgs> eventHandler_0;

		private EventHandler<CodeFileSelectedEventArgs> eventHandler_1;

		internal Viewbox viewbox_0;

		internal Canvas canvas_0;

		private bool bool_0;

		public event EventHandler<SimilaritySelectedEventArgs> SimilaritySelected
		{
			add
			{
				EventHandler<SimilaritySelectedEventArgs> eventHandler = this.eventHandler_0;
				EventHandler<SimilaritySelectedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<SimilaritySelectedEventArgs> value2 = (EventHandler<SimilaritySelectedEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<SimilaritySelectedEventArgs>>(ref this.eventHandler_0, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler<SimilaritySelectedEventArgs> eventHandler = this.eventHandler_0;
				EventHandler<SimilaritySelectedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<SimilaritySelectedEventArgs> value2 = (EventHandler<SimilaritySelectedEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<SimilaritySelectedEventArgs>>(ref this.eventHandler_0, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		public event EventHandler<CodeFileSelectedEventArgs> CodeFileSelected
		{
			add
			{
				EventHandler<CodeFileSelectedEventArgs> eventHandler = this.eventHandler_1;
				EventHandler<CodeFileSelectedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<CodeFileSelectedEventArgs> value2 = (EventHandler<CodeFileSelectedEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<CodeFileSelectedEventArgs>>(ref this.eventHandler_1, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler<CodeFileSelectedEventArgs> eventHandler = this.eventHandler_1;
				EventHandler<CodeFileSelectedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<CodeFileSelectedEventArgs> value2 = (EventHandler<CodeFileSelectedEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<CodeFileSelectedEventArgs>>(ref this.eventHandler_1, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		public WheelView(IList<CodeFile> files, CodeDir rootDir)
		{
			this.InitializeComponent();
			this.style_0 = (Style)base.FindResource("FilePieceStyle");
			this.style_1 = (Style)base.FindResource("DirPieceStyle");
			this.wheelViewModel_0 = new WheelViewModel(files);
			base.DataContext = this.wheelViewModel_0;
			this.codeDir_0 = rootDir;
			base.Loaded += new RoutedEventHandler(this.WheelView_Loaded);
		}

		private void WheelView_Loaded(object sender, RoutedEventArgs e)
		{
			this.method_0();
		}

		internal void method_0()
		{
			this.double_0 = this.double_4 + (double)(this.wheelViewModel_0.DeepestDir + 1) * this.double_2;
			this.double_1 = this.double_0;
			base.Height = this.double_1 * 2.0;
			base.Width = this.double_0 * 2.0;
			this.canvas_0.Width = base.Width;
			this.canvas_0.Height = base.Height;
			Color.FromRgb(0, 102, 255);
			Color.FromRgb(255, 0, 0);
			Dictionary<CodeFile, PiePiece> dictionary = new Dictionary<CodeFile, PiePiece>();
			Dictionary<CodeDir, PiePiece> dictionary2 = new Dictionary<CodeDir, PiePiece>();
			Dictionary<PiePiece, double> dictionary3 = new Dictionary<PiePiece, double>();
			this.double_5 = this.double_4 + this.double_2;
			Ellipse element = new Ellipse
			{
				Height = base.Height * 2.0,
				Width = base.Width * 2.0,
				StrokeThickness = base.Width - this.double_4,
				Stroke = Brushes.Black
			};
			Canvas.SetTop(element, -1.0 * base.Height / 2.0);
			Canvas.SetLeft(element, -1.0 * base.Width / 2.0);
			Panel.SetZIndex(element, 5);
			this.canvas_0.Children.Add(element);
			double num = 0.0;
			double num2 = 360.0 / this.wheelViewModel_0.TotalLineSize;
			foreach (CodeFile current in this.wheelViewModel_0.CodeFiles)
			{
				PiePiece piePiece = new PiePiece();
				piePiece.Style = this.style_0;
				int num3 = this.wheelViewModel_0.DeepestDir - current.DirectParent.Depth;
				piePiece.method_11(this.double_0);
				piePiece.method_13(this.double_1);
				piePiece.method_1(this.double_5 + (double)num3 * this.double_2);
				piePiece.method_5(this.double_4);
				piePiece.method_9(num);
				piePiece.method_7(num2 * (double)current.ProcessedLines);
				num += piePiece.method_6();
				piePiece.DataContext = current;
				piePiece.Tag = current;
				piePiece.Stroke = Brushes.Black;
				piePiece.Fill = WheelView.smethod_0(piePiece);
				Panel.SetZIndex(piePiece, 10);
				piePiece.Cursor = Cursors.Hand;
				this.canvas_0.Children.Add(piePiece);
				dictionary.Add(current, piePiece);
				dictionary3.Add(piePiece, 0.0);
				this.method_1(piePiece, System.IO.Path.GetFileNameWithoutExtension(current.Name), 1.0);
			}
			double[] array = new double[this.wheelViewModel_0.DeepestDir + 1];
			foreach (CodeDir current2 in this.codeDir_0.GetAllDirectories())
			{
				int processedLines = current2.ProcessedLines;
				int num3 = this.wheelViewModel_0.DeepestDir - current2.Depth + 1;
				PiePiece piePiece = new PiePiece();
				piePiece.Style = this.style_1;
				piePiece.method_11(this.double_0);
				piePiece.method_13(this.double_1);
				piePiece.method_1(this.double_5 + (double)num3 * this.double_2);
				piePiece.method_5(this.double_4 + (double)num3 * this.double_2);
				CodeFile firstDeepestFile = current2.FirstDeepestFile;
				PiePiece piePiece2 = dictionary[firstDeepestFile];
				piePiece.method_9(piePiece2.method_8());
				piePiece.method_7(num2 * (double)processedLines);
				if (piePiece.method_6() >= 360.0)
				{
					piePiece.method_7(359.999);
				}
				array[num3] += piePiece.method_6();
				piePiece.DataContext = current2;
				piePiece.Tag = current2;
				piePiece.Stroke = Brushes.Black;
				piePiece.Fill = WheelView.smethod_0(piePiece);
				Panel.SetZIndex(piePiece, 10);
				piePiece.Cursor = Cursors.Hand;
				this.canvas_0.Children.Add(piePiece);
				dictionary2.Add(current2, piePiece);
				this.method_1(piePiece, System.IO.Path.GetFileName(current2.Name), 0.8);
			}
			IEnumerable<Similarity> enumerable = this.wheelViewModel_0.method_1();
			foreach (Similarity current3 in enumerable)
			{
				CanonicalSpline canonicalSpline = new CanonicalSpline();
				canonicalSpline.Tolerance = 1.5;
				canonicalSpline.DataContext = current3;
				canonicalSpline.Points = new PointCollection();
				canonicalSpline.Cursor = Cursors.Hand;
				Panel.SetZIndex(canonicalSpline, 0);
				canonicalSpline.StrokeThickness = (double)current3.MyHashIndexRange.Length * (num2 / 360.0 * this.double_4 * 2.0 * 3.14159);
				canonicalSpline.Stroke = (current3.SameFile ? WheelView.brush_1 : WheelView.brush_0);
				PiePiece piePiece3 = dictionary[current3.MyFile];
				PiePiece piePiece4 = dictionary[current3.OtherFile];
				double num4 = piePiece3.method_8() + dictionary3[piePiece3] + (double)current3.MyHashIndexRange.Length * num2 / 2.0;
				Point point = Utils.ComputeCartesianCoordinate(num4, this.double_4, this.double_0, this.double_1);
				Point value = Utils.ComputeCartesianCoordinate(num4, this.double_4 + this.double_4 / 3.0, this.double_0, this.double_1);
				Dictionary<PiePiece, double> dictionary4;
				PiePiece key;
				(dictionary4 = dictionary3)[key = piePiece3] = dictionary4[key] + (double)current3.MyHashIndexRange.Length * num2;
				double num5 = piePiece4.method_8() + dictionary3[piePiece4] + (double)current3.OtherHashIndexRange.Length * num2 / 2.0;
				Point point2 = Utils.ComputeCartesianCoordinate(num5, this.double_4, this.double_0, this.double_1);
				Point value2 = Utils.ComputeCartesianCoordinate(num5, this.double_4 + this.double_4 / 3.0, this.double_0, this.double_1);
				(dictionary4 = dictionary3)[key = piePiece4] = dictionary4[key] + (double)current3.OtherHashIndexRange.Length * num2;
				double num6 = this.method_3(num4, num5);
				double num7 = this.double_4 - num6 / 180.0 * this.double_4;
				if (num7 > this.double_4 / 1.2)
				{
					num7 = this.double_4 / 1.2;
				}
				double angle = this.method_2(num4, num5);
				Point value3 = Utils.ComputeCartesianCoordinate(angle, num7, this.double_0, this.double_1);
				canonicalSpline.Points.Add(value);
				canonicalSpline.Points.Add(point);
				canonicalSpline.Points.Add(value3);
				canonicalSpline.Points.Add(point2);
				canonicalSpline.Points.Add(value2);
				canonicalSpline.Stroke = new LinearGradientBrush(((SolidColorBrush)piePiece4.Fill).Color, ((SolidColorBrush)piePiece3.Fill).Color, point, point2)
				{
					MappingMode = BrushMappingMode.Absolute
				};
				this.canvas_0.Children.Add(canonicalSpline);
			}
		}

		private void method_1(PiePiece piePiece_0, string string_0, double double_6)
		{
			if (piePiece_0.method_6() >= 6.0)
			{
				string text = string_0;
				TextOnPathElement textOnPathElement;
				double num3;
				while (true)
				{
					textOnPathElement = new TextOnPathElement
					{
						Text = text,
						IsHitTestVisible = false,
						FontWeight = FontWeights.Bold
					};
					double num = textOnPathElement.TextLength * 1.5;
					double num2 = (this.double_5 + this.double_4 / 2.0) * 2.0 * 3.1415;
					num3 = num * 360.0 / num2;
					if (num3 + 1.5 <= piePiece_0.method_6() || text.Length <= 1)
					{
						break;
					}
					text = text.Substring(0, text.Length - 1);
				}
				double num4 = piePiece_0.method_8() + piePiece_0.method_6() / 2.0;
				bool flag = false;
				if (num4 > 90.0 && num4 < 270.0)
				{
					flag = true;
				}
				if (flag)
				{
					textOnPathElement.PathFigure = piePiece_0.method_20(this.double_3, num3);
				}
				else
				{
					textOnPathElement.PathFigure = piePiece_0.method_19(this.double_3, num3);
				}
				this.canvas_0.Children.Add(textOnPathElement);
				Panel.SetZIndex(textOnPathElement, 15);
			}
		}

		private static Brush smethod_0(PiePiece piePiece_0)
		{
			double h = piePiece_0.method_8() / 360.0;
			return new SolidColorBrush(WheelView.HSL2RGB(h, 1.0, 0.5));
		}

		private double method_2(double double_6, double double_7)
		{
			double num = this.method_3(double_6, double_7);
			double num2 = num / 2.0;
			if (double_6 > double_7)
			{
				double num3 = double_6;
				double_6 = double_7;
				double_7 = num3;
			}
			double result;
			if (double_7 - double_6 > 180.0)
			{
				result = double_6 - num2;
			}
			else
			{
				result = double_6 + num2;
			}
			return result;
		}

		private double method_3(double double_6, double double_7)
		{
			double num = Math.Abs(double_6 - double_7) % 360.0;
			if (num > 180.0)
			{
				num = 360.0 - num;
			}
			return num;
		}

		public void CanonicalSpline_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Point position = Mouse.GetPosition(this.canvas_0);
			CanonicalSpline canonicalSpline = (CanonicalSpline)sender;
			Similarity similarity = (Similarity)canonicalSpline.DataContext;
			double num = this.method_4(canonicalSpline.Points[0], position);
			double num2 = this.method_4(canonicalSpline.Points[2], position);
			if (this.eventHandler_0 != null)
			{
				if (num < num2)
				{
					this.eventHandler_0(this, new SimilaritySelectedEventArgs
					{
						Similarity = similarity
					});
				}
				else
				{
					this.eventHandler_0(this, new SimilaritySelectedEventArgs
					{
						Similarity = similarity.CorrespondingSimilarity
					});
				}
			}
		}

		private double method_4(Point point_0, Point point_1)
		{
			return Math.Sqrt(Math.Pow(point_0.X - point_1.X, 2.0) + Math.Pow(point_0.Y - point_1.Y, 2.0));
		}

		public void PiePiece_MouseDown(object sender, MouseButtonEventArgs e)
		{
			FrameworkElement frameworkElement = (FrameworkElement)sender;
			CodeFile codeFile = frameworkElement.DataContext as CodeFile;
			if (codeFile != null && this.eventHandler_1 != null)
			{
				this.eventHandler_1(this, new CodeFileSelectedEventArgs
				{
					CodeFile = codeFile
				});
			}
		}

		public static Color HSL2RGB(double h, double sl, double l)
		{
			double num = l;
			double num2 = l;
			double num3 = l;
			double num4 = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);
			if (num4 > 0.0)
			{
				double num5 = l + l - num4;
				double num6 = (num4 - num5) / num4;
				h *= 6.0;
				int num7 = (int)h;
				double num8 = h - (double)num7;
				double num9 = num4 * num6 * num8;
				double num10 = num5 + num9;
				double num11 = num4 - num9;
				switch (num7)
				{
				case 0:
					num = num4;
					num2 = num10;
					num3 = num5;
					break;
				case 1:
					num = num11;
					num2 = num4;
					num3 = num5;
					break;
				case 2:
					num = num5;
					num2 = num4;
					num3 = num10;
					break;
				case 3:
					num = num5;
					num2 = num11;
					num3 = num4;
					break;
				case 4:
					num = num10;
					num2 = num5;
					num3 = num4;
					break;
				case 5:
					num = num4;
					num2 = num5;
					num3 = num11;
					break;
				}
			}
			return Color.FromRgb(Convert.ToByte(num * 255.0), Convert.ToByte(num2 * 255.0), Convert.ToByte(num3 * 255.0));
		}

		public void CanonicalSpline_MouseEnter(object sender, MouseEventArgs e)
		{
			UIElement element = sender as UIElement;
			Panel.SetZIndex(element, 1);
		}

		public void CanonicalSpline_MouseLeave(object sender, MouseEventArgs e)
		{
			UIElement element = sender as UIElement;
			Panel.SetZIndex(element, 0);
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!this.bool_0)
			{
				this.bool_0 = true;
				Uri resourceLocator = new Uri("/Atomiq;component/wheelview.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.viewbox_0 = (Viewbox)target;
				break;
			case 2:
				this.canvas_0 = (Canvas)target;
				break;
			default:
				this.bool_0 = true;
				break;
			}
		}

		static WheelView()
		{
			WheelView.brush_0 = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
			WheelView.brush_1 = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
		}
	}
}
