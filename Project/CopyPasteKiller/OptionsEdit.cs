using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Markup;

namespace CopyPasteKiller
{
	public class OptionsEdit : Window, IComponentConnector
	{
		private Options _options;

		internal DockPanel dockPanel;

		internal System.Windows.Controls.TextBox textBox;

		internal System.Windows.Controls.Button button2;

		private bool _isInitialized;

		public OptionsEdit(Options options)
		{
			InitializeComponent();
			_options = options;
			base.DataContext = _options;
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.ShowNewFolderButton = false;
			folderBrowserDialog.Description = "Pick Code Directory To Analyze";
			folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
			folderBrowserDialog.SelectedPath = _options.Directory;

			if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				_options.Directory = folderBrowserDialog.SelectedPath;
			}
		}

		private void button2_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(_options.Directory))
			{
				System.Windows.MessageBox.Show("You have not selected a directory to analyze", "Select Directory", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			else
			{
				string text = _options.ValidateRegexes();

				if (text != "")
				{
					System.Windows.MessageBox.Show("The following regexes are invalid: \r\n" + text, "Invalid Regex", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				else
				{
					base.DialogResult = new bool?(true);
					base.Close();
				}
			}
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_isInitialized)
			{
				_isInitialized = true;
				Uri resourceLocator = new Uri("/Atomiq;component/optionsedit.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				dockPanel = (DockPanel)target;
				break;
			case 2:
				((System.Windows.Controls.Button)target).Click += button1_Click;
				break;
			case 3:
				textBox = (System.Windows.Controls.TextBox)target;
				break;
			case 4:
				button2 = (System.Windows.Controls.Button)target;
				button2.Click += button2_Click;
				break;
			default:
				_isInitialized = true;
				break;
			}
		}
	}
}