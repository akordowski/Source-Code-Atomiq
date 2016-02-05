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
		private RecentViewModel _recentViewModel;

		private bool _isInitialize;

		public string SelectedPath
		{
			get
			{
				return _recentViewModel.OpenDirectory;
			}
		}

		public RecentDirectories()
		{
			InitializeComponent();
			base.Loaded += RecentDirectories_Loaded;
		}

		private void RecentDirectories_Loaded(object sender, RoutedEventArgs e)
		{
			_recentViewModel = new RecentViewModel();
			base.DataContext = _recentViewModel;
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.ShowNewFolderButton = false;
			folderBrowserDialog.Description = "Pick Code Directory To Analyze";

			if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				_recentViewModel.Open(folderBrowserDialog.SelectedPath);
				base.DialogResult = new bool?(true);
				base.Close();
			}
		}

		private void method1(object sender, RoutedEventArgs e)
		{
			FrameworkElement frameworkElement = sender as FrameworkElement;
			_recentViewModel.Open(frameworkElement.DataContext.ToString());
			base.DialogResult = new bool?(true);
			base.Close();
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_isInitialize)
			{
				_isInitialize = true;
				Uri resourceLocator = new Uri("/Atomiq;component/recentdirectories.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId != 1)
			{
				_isInitialize = true;
			}
			else
			{
				((System.Windows.Controls.Button)target).Click += button_Click;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 2)
			{
				((System.Windows.Controls.Button)target).Click += new RoutedEventHandler(method1);
			}
		}
	}
}