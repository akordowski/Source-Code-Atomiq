using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Markup;

namespace CopyPasteKiller
{
	public class RecentDirectories : Window, IComponentConnector, IStyleConnector
	{
		private RecentViewModel recentViewModel_0;

		private bool bool_0;

		public string SelectedPath
		{
			get
			{
				return this.recentViewModel_0.OpenDirectory;
			}
		}

		public RecentDirectories()
		{
			this.InitializeComponent();
			base.Loaded += new RoutedEventHandler(this.RecentDirectories_Loaded);
		}

		private void RecentDirectories_Loaded(object sender, RoutedEventArgs e)
		{
			this.recentViewModel_0 = new RecentViewModel();
			base.DataContext = this.recentViewModel_0;
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.ShowNewFolderButton = false;
			folderBrowserDialog.Description = "Pick Code Directory To Analyze";
			if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.recentViewModel_0.Open(folderBrowserDialog.SelectedPath);
				base.DialogResult = new bool?(true);
				base.Close();
			}
		}

		private void method_1(object sender, RoutedEventArgs e)
		{
			FrameworkElement frameworkElement = sender as FrameworkElement;
			this.recentViewModel_0.Open(frameworkElement.DataContext.ToString());
			base.DialogResult = new bool?(true);
			base.Close();
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!this.bool_0)
			{
				this.bool_0 = true;
				Uri resourceLocator = new Uri("/Atomiq;component/recentdirectories.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
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
				((System.Windows.Controls.Button)target).Click += new RoutedEventHandler(this.method_0);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 2)
			{
				((System.Windows.Controls.Button)target).Click += new RoutedEventHandler(this.method_1);
			}
		}
	}
}
