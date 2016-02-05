using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace CopyPasteKiller
{
	public class AnalyzingWindow : Window, IComponentConnector
	{
		private AnalyzingViewModel _analyzingViewModel;

		private bool _isInitialized;

		[CompilerGenerated]
		private static Action<string> action0;

		public Analysis Analysis { get; private set; }

		public AnalyzingWindow(Options options)
		{
			InitializeComponent();
			_analyzingViewModel = new AnalyzingViewModel();
			Analysis = new Analysis(options.Directory);
			Analysis.Options = options;

			base.DataContext = _analyzingViewModel;
			base.Loaded += new RoutedEventHandler(AnalyzingWindow_Loaded);
		}

		private void AnalyzingWindow_Loaded(object sender, RoutedEventArgs e)
		{
			Analysis.UpdateProgressAction = new Action<int, int, string>(method2);
			Analysis.IncrementProgressValue = new Action(method3);
			Analysis.UpdateProgressValue = new Action<int>(method4);

			Analysis analysis = Analysis;

			if (AnalyzingWindow.action0 == null)
			{
				AnalyzingWindow.action0 = new Action<string>(AnalyzingWindow.ShowMessageBox);
			}

			analysis.AlertAction = AnalyzingWindow.action0;
			Analysis.Done = new Action(method5);
			Analysis.StartThread();
		}

		private void Close()
		{
			base.Close();
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			Analysis.AbortThread();
			base.Close();
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_isInitialized)
			{
				_isInitialized = true;
				Uri resourceLocator = new Uri("/Atomiq;component/analyzingwindow.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId != 1)
			{
				_isInitialized = true;
			}
			else
			{
				((Button)target).Click += button_Click;
			}
		}

		[CompilerGenerated]
		private void method2(int int1, int int2, string str)
		{
			base.Dispatcher.Invoke(new Action(delegate
			{
				this._analyzingViewModel.Value = int1;
				this._analyzingViewModel.Max = int2;
				this._analyzingViewModel.Message = str;
			}), new object[0]);
		}

		[CompilerGenerated]
		private void method3()
		{
			base.Dispatcher.Invoke(new Action(method6), new object[0]);
		}

		[CompilerGenerated]
		private void method4(int value)
		{
			base.Dispatcher.Invoke(new Action(delegate
			{
				_analyzingViewModel.Value = value;
			}), new object[0]);
		}

		[CompilerGenerated]
		private static void ShowMessageBox(string text)
		{
			MessageBox.Show(text);
		}

		[CompilerGenerated]
		private void method5()
		{
			base.Dispatcher.Invoke(new Action(Close), new object[0]);
		}

		[CompilerGenerated]
		private void method6()
		{
			_analyzingViewModel.Value++;
		}
	}
}