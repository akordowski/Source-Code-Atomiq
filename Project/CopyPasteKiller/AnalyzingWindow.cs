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
		private static Action<string> action;

		public Analysis Analysis { get; private set; }

		public AnalyzingWindow(Options options)
		{
			InitializeComponent();
			_analyzingViewModel = new AnalyzingViewModel();
			Analysis = new Analysis(options.Directory);
			Analysis.Options = options;
			base.DataContext = this._analyzingViewModel;
			base.Loaded += AnalyzingWindow_Loaded;
		}

		private void AnalyzingWindow_Loaded(object sender, RoutedEventArgs e)
		{
			Analysis.UpdateProgressAction = new Action<int, int, string>(this.method_2);
			Analysis.IncrementProgressValue = new Action(this.method_3);
			Analysis.UpdateProgressValue = new Action<int>(this.method_4);
			Analysis analysis = Analysis;

			if (AnalyzingWindow.action == null)
			{
				AnalyzingWindow.action = new Action<string>(AnalyzingWindow.smethod_0);
			}

			analysis.AlertAction = AnalyzingWindow.action;

			Analysis.Done = new Action(this.method_5);
			Analysis.method_0();
		}

		private void Close()
		{
			base.Close();
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			Analysis.method_1();
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
		private void method_2(int int_0, int int_1, string str)
		{
			base.Dispatcher.Invoke(new Action(delegate
			{
				_analyzingViewModel.Value = int_0;
				_analyzingViewModel.Max = int_1;
				_analyzingViewModel.Message = str;
			}), new object[0]);
		}

		[CompilerGenerated]
		private void method_3()
		{
			base.Dispatcher.Invoke(new Action(this.method_6), new object[0]);
		}

		[CompilerGenerated]
		private void method_4(int int_0)
		{
			base.Dispatcher.Invoke(new Action(delegate
			{
				this._analyzingViewModel.Value = int_0;
			}), new object[0]);
		}

		[CompilerGenerated]
		private static void smethod_0(string string_0)
		{
			MessageBox.Show(string_0);
		}

		[CompilerGenerated]
		private void method_5()
		{
			base.Dispatcher.Invoke(new Action(this.Close), new object[0]);
		}

		[CompilerGenerated]
		private void method_6()
		{
			_analyzingViewModel.Value++;
		}
	}
}