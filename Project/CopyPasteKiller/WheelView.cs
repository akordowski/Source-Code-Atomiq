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
		private WheelViewModel _wheelViewModel;

		private CodeDir _codeDir;

		private Style _style0;

		private Style _style1;

		private static Brush _brush0;

		private static Brush _brush1;

		private double double0 = 470.0;

		private double double1 = 470.0;

		private double double2 = 20.0;

		private double double3 = 5.0;

		private double double4 = 310.0;

		private double double5;

		private EventHandler<SimilaritySelectedEventArgs> _eventHandler0;

		private EventHandler<CodeFileSelectedEventArgs> _eventHandler1;

		internal Viewbox viewbox;

		internal Canvas canvas;

		private bool _isInitialized;

		public event EventHandler<SimilaritySelectedEventArgs> SimilaritySelected
		{
			add
			{
				EventHandler<SimilaritySelectedEventArgs> eventHandler = _eventHandler0;
				EventHandler<SimilaritySelectedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<SimilaritySelectedEventArgs> value2 = (EventHandler<SimilaritySelectedEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<SimilaritySelectedEventArgs>>(ref _eventHandler0, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler<SimilaritySelectedEventArgs> eventHandler = _eventHandler0;
				EventHandler<SimilaritySelectedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<SimilaritySelectedEventArgs> value2 = (EventHandler<SimilaritySelectedEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<SimilaritySelectedEventArgs>>(ref _eventHandler0, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		public event EventHandler<CodeFileSelectedEventArgs> CodeFileSelected
		{
			add
			{
				EventHandler<CodeFileSelectedEventArgs> eventHandler = _eventHandler1;
				EventHandler<CodeFileSelectedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<CodeFileSelectedEventArgs> value2 = (EventHandler<CodeFileSelectedEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<CodeFileSelectedEventArgs>>(ref _eventHandler1, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler<CodeFileSelectedEventArgs> eventHandler = _eventHandler1;
				EventHandler<CodeFileSelectedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<CodeFileSelectedEventArgs> value2 = (EventHandler<CodeFileSelectedEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<CodeFileSelectedEventArgs>>(ref _eventHandler1, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		public WheelView(IList<CodeFile> files, CodeDir rootDir)
		{
			InitializeComponent();
			_style0 = (Style)base.FindResource("FilePieceStyle");
			_style1 = (Style)base.FindResource("DirPieceStyle");
			_wheelViewModel = new WheelViewModel(files);
			base.DataContext = _wheelViewModel;
			_codeDir = rootDir;
			base.Loaded += WheelView_Loaded;
		}

		private void WheelView_Loaded(object sender, RoutedEventArgs e)
		{
			WheelViewLoaded();
		}

		internal void WheelViewLoaded()
		{
			double0 = double4 + (double)(_wheelViewModel.DeepestDir + 1) * double2;
			double1 = double0;
			base.Height = double1 * 2.0;
			base.Width = double0 * 2.0;
			canvas.Width = base.Width;
			canvas.Height = base.Height;
			Color.FromRgb(0, 102, 255);
			Color.FromRgb(255, 0, 0);
			Dictionary<CodeFile, PiePiece> dictionary = new Dictionary<CodeFile, PiePiece>();
			Dictionary<CodeDir, PiePiece> dictionary2 = new Dictionary<CodeDir, PiePiece>();
			Dictionary<PiePiece, double> dictionary3 = new Dictionary<PiePiece, double>();
			double5 = double4 + double2;
			Ellipse element = new Ellipse
			{
				Height = base.Height * 2.0,
				Width = base.Width * 2.0,
				StrokeThickness = base.Width - double4,
				Stroke = Brushes.Black
			};
			Canvas.SetTop(element, -1.0 * base.Height / 2.0);
			Canvas.SetLeft(element, -1.0 * base.Width / 2.0);
			Panel.SetZIndex(element, 5);
			canvas.Children.Add(element);
			double num = 0.0;
			double num2 = 360.0 / _wheelViewModel.TotalLineSize;

			foreach (CodeFile current in _wheelViewModel.CodeFiles)
			{
				PiePiece piePiece = new PiePiece();
				piePiece.Style = _style0;
				int num3 = _wheelViewModel.DeepestDir - current.DirectParent.Depth;
				piePiece.method11(double0);
				piePiece.method13(double1);
				piePiece.method1(double5 + (double)num3 * double2);
				piePiece.method5(double4);
				piePiece.method9(num);
				piePiece.method7(num2 * (double)current.ProcessedLines);
				num += piePiece.method6();
				piePiece.DataContext = current;
				piePiece.Tag = current;
				piePiece.Stroke = Brushes.Black;
				piePiece.Fill = WheelView.smethod0(piePiece);
				Panel.SetZIndex(piePiece, 10);
				piePiece.Cursor = Cursors.Hand;
				canvas.Children.Add(piePiece);
				dictionary.Add(current, piePiece);
				dictionary3.Add(piePiece, 0.0);
				method1(piePiece, System.IO.Path.GetFileNameWithoutExtension(current.Name), 1.0);
			}

			double[] array = new double[_wheelViewModel.DeepestDir + 1];

			foreach (CodeDir current2 in _codeDir.GetAllDirectories())
			{
				int processedLines = current2.ProcessedLines;
				int num3 = _wheelViewModel.DeepestDir - current2.Depth + 1;
				PiePiece piePiece = new PiePiece();
				piePiece.Style = this._style1;
				piePiece.method11(double0);
				piePiece.method13(double1);
				piePiece.method1(double5 + (double)num3 * double2);
				piePiece.method5(double4 + (double)num3 * double2);
				CodeFile firstDeepestFile = current2.FirstDeepestFile;
				PiePiece piePiece2 = dictionary[firstDeepestFile];
				piePiece.method9(piePiece2.method8());
				piePiece.method7(num2 * (double)processedLines);

				if (piePiece.method6() >= 360.0)
				{
					piePiece.method7(359.999);
				}

				array[num3] += piePiece.method6();
				piePiece.DataContext = current2;
				piePiece.Tag = current2;
				piePiece.Stroke = Brushes.Black;
				piePiece.Fill = WheelView.smethod0(piePiece);
				Panel.SetZIndex(piePiece, 10);
				piePiece.Cursor = Cursors.Hand;
				canvas.Children.Add(piePiece);
				dictionary2.Add(current2, piePiece);
				method1(piePiece, System.IO.Path.GetFileName(current2.Name), 0.8);
			}

			IEnumerable<Similarity> enumerable = _wheelViewModel.method1();

			foreach (Similarity current3 in enumerable)
			{
				CanonicalSpline canonicalSpline = new CanonicalSpline();
				canonicalSpline.Tolerance = 1.5;
				canonicalSpline.DataContext = current3;
				canonicalSpline.Points = new PointCollection();
				canonicalSpline.Cursor = Cursors.Hand;
				Panel.SetZIndex(canonicalSpline, 0);
				canonicalSpline.StrokeThickness = (double)current3.MyHashIndexRange.Length * (num2 / 360.0 * double4 * 2.0 * 3.14159);
				canonicalSpline.Stroke = (current3.SameFile ? WheelView._brush1 : WheelView._brush0);
				PiePiece piePiece3 = dictionary[current3.MyFile];
				PiePiece piePiece4 = dictionary[current3.OtherFile];
				double num4 = piePiece3.method8() + dictionary3[piePiece3] + (double)current3.MyHashIndexRange.Length * num2 / 2.0;
				Point point = Utils.ComputeCartesianCoordinate(num4, double4, double0, double1);
				Point value = Utils.ComputeCartesianCoordinate(num4, double4 + double4 / 3.0, double0, double1);
				Dictionary<PiePiece, double> dictionary4;
				PiePiece key;
				(dictionary4 = dictionary3)[key = piePiece3] = dictionary4[key] + (double)current3.MyHashIndexRange.Length * num2;
				double num5 = piePiece4.method8() + dictionary3[piePiece4] + (double)current3.OtherHashIndexRange.Length * num2 / 2.0;
				Point point2 = Utils.ComputeCartesianCoordinate(num5, double4, double0, double1);
				Point value2 = Utils.ComputeCartesianCoordinate(num5, double4 + double4 / 3.0, double0, double1);
				(dictionary4 = dictionary3)[key = piePiece4] = dictionary4[key] + (double)current3.OtherHashIndexRange.Length * num2;
				double num6 = method3(num4, num5);
				double num7 = double4 - num6 / 180.0 * double4;

				if (num7 > double4 / 1.2)
				{
					num7 = double4 / 1.2;
				}

				double angle = method2(num4, num5);
				Point value3 = Utils.ComputeCartesianCoordinate(angle, num7, double0, double1);
				canonicalSpline.Points.Add(value);
				canonicalSpline.Points.Add(point);
				canonicalSpline.Points.Add(value3);
				canonicalSpline.Points.Add(point2);
				canonicalSpline.Points.Add(value2);
				canonicalSpline.Stroke = new LinearGradientBrush(((SolidColorBrush)piePiece4.Fill).Color, ((SolidColorBrush)piePiece3.Fill).Color, point, point2)
				{
					MappingMode = BrushMappingMode.Absolute
				};

				canvas.Children.Add(canonicalSpline);
			}
		}

		private void method1(PiePiece piePiece, string str, double double6)
		{
			if (piePiece.method6() >= 6.0)
			{
				string text = str;
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
					double num2 = (double5 + double4 / 2.0) * 2.0 * 3.1415;
					num3 = num * 360.0 / num2;

					if (num3 + 1.5 <= piePiece.method6() || text.Length <= 1)
					{
						break;
					}

					text = text.Substring(0, text.Length - 1);
				}

				double num4 = piePiece.method8() + piePiece.method6() / 2.0;
				bool flag = false;

				if (num4 > 90.0 && num4 < 270.0)
				{
					flag = true;
				}

				if (flag)
				{
					textOnPathElement.PathFigure = piePiece.method20(double3, num3);
				}
				else
				{
					textOnPathElement.PathFigure = piePiece.method19(double3, num3);
				}

				canvas.Children.Add(textOnPathElement);
				Panel.SetZIndex(textOnPathElement, 15);
			}
		}

		private static Brush smethod0(PiePiece piePiece)
		{
			double h = piePiece.method8() / 360.0;
			return new SolidColorBrush(WheelView.HSL2RGB(h, 1.0, 0.5));
		}

		private double method2(double double6, double double7)
		{
			double num = method3(double6, double7);
			double num2 = num / 2.0;

			if (double6 > double7)
			{
				double num3 = double6;
				double6 = double7;
				double7 = num3;
			}

			double result;

			if (double7 - double6 > 180.0)
			{
				result = double6 - num2;
			}
			else
			{
				result = double6 + num2;
			}

			return result;
		}

		private double method3(double double6, double double7)
		{
			double num = Math.Abs(double6 - double7) % 360.0;

			if (num > 180.0)
			{
				num = 360.0 - num;
			}

			return num;
		}

		public void CanonicalSpline_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Point position = Mouse.GetPosition(canvas);
			CanonicalSpline canonicalSpline = (CanonicalSpline)sender;
			Similarity similarity = (Similarity)canonicalSpline.DataContext;
			double num = method4(canonicalSpline.Points[0], position);
			double num2 = method4(canonicalSpline.Points[2], position);

			if (_eventHandler0 != null)
			{
				if (num < num2)
				{
					_eventHandler0(this, new SimilaritySelectedEventArgs
					{
						Similarity = similarity
					});
				}
				else
				{
					_eventHandler0(this, new SimilaritySelectedEventArgs
					{
						Similarity = similarity.CorrespondingSimilarity
					});
				}
			}
		}

		private double method4(Point point0, Point point1)
		{
			return Math.Sqrt(Math.Pow(point0.X - point1.X, 2.0) + Math.Pow(point0.Y - point1.Y, 2.0));
		}

		public void PiePiece_MouseDown(object sender, MouseButtonEventArgs e)
		{
			FrameworkElement frameworkElement = (FrameworkElement)sender;
			CodeFile codeFile = frameworkElement.DataContext as CodeFile;

			if (codeFile != null && _eventHandler1 != null)
			{
				_eventHandler1(this, new CodeFileSelectedEventArgs
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
			if (!_isInitialized)
			{
				_isInitialized = true;
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
					viewbox = (Viewbox)target;
					break;
				case 2:
					canvas = (Canvas)target;
					break;
				default:
					_isInitialized = true;
					break;
			}
		}

		static WheelView()
		{
			WheelView._brush0 = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
			WheelView._brush1 = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
		}
	}
}