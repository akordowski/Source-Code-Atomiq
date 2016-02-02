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
		private AnalyzingViewModel analyzingViewModel_0;

		private Analysis analysis_0;

		private bool bool_0;

		[CompilerGenerated]
		private static Action<string> action_0;

		public Analysis Analysis
		{
			get
			{
				return this.analysis_0;
			}
		}

		public AnalyzingWindow(Options options)
		{
			this.InitializeComponent();
			this.analyzingViewModel_0 = new AnalyzingViewModel();
			this.analysis_0 = new Analysis(options.Directory);
			this.analysis_0.Options = options;
			base.DataContext = this.analyzingViewModel_0;
			base.Loaded += new RoutedEventHandler(this.AnalyzingWindow_Loaded);
		}

		private void AnalyzingWindow_Loaded(object sender, RoutedEventArgs e)
		{
			this.analysis_0.UpdateProgressAction = new Action<int, int, string>(this.method_2);
			this.analysis_0.IncrementProgressValue = new Action(this.method_3);
			this.analysis_0.UpdateProgressValue = new Action<int>(this.method_4);
			Analysis arg_68_0 = this.analysis_0;
			if (AnalyzingWindow.action_0 == null)
			{
				AnalyzingWindow.action_0 = new Action<string>(AnalyzingWindow.smethod_0);
			}
			arg_68_0.AlertAction = AnalyzingWindow.action_0;
			this.analysis_0.Done = new Action(this.method_5);
			this.analysis_0.method_0();
		}

		private void method_0()
		{
			base.Close();
		}

		private void method_1(object sender, RoutedEventArgs e)
		{
			this.analysis_0.method_1();
			base.Close();
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!this.bool_0)
			{
				this.bool_0 = true;
				Uri resourceLocator = new Uri("/Atomiq;component/analyzingwindow.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId != 1)
			{
				this.bool_0 = true;
			}
			else
			{
				((Button)target).Click += new RoutedEventHandler(this.method_1);
			}
		}

		[CompilerGenerated]
		private void method_2(int int_0, int int_1, string string_0)
		{
			base.Dispatcher.Invoke(new Action(delegate
			{
				this.analyzingViewModel_0.Value = int_0;
				this.analyzingViewModel_0.Max = int_1;
				this.analyzingViewModel_0.Message = string_0;
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
				this.analyzingViewModel_0.Value = int_0;
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
			base.Dispatcher.Invoke(new Action(this.method_0), new object[0]);
		}

		[CompilerGenerated]
		private void method_6()
		{
			this.analyzingViewModel_0.Value++;
		}
	}
}
