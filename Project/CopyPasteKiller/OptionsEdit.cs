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
		private Options options;

		internal DockPanel dockPanel;

		internal System.Windows.Controls.TextBox textBox;

		internal System.Windows.Controls.Button button;

		private bool bool0;

		public OptionsEdit(Options options)
		{
			this.InitializeComponent();
			this.options = options;
			base.DataContext = this.options;
		}

		private void method0(object sender, RoutedEventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.ShowNewFolderButton = false;
			folderBrowserDialog.Description = "Pick Code Directory To Analyze";
			folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
			folderBrowserDialog.SelectedPath = this.options.Directory;

			if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				options.Directory = folderBrowserDialog.SelectedPath;
			}
		}

		private void button_0_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(options.Directory))
			{
				System.Windows.MessageBox.Show("You have not selected a directory to analyze", "Select Directory", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			else
			{
				string text = options.ValidateRegexes();

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
			if (!bool0)
			{
				bool0 = true;
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
					((System.Windows.Controls.Button)target).Click += new RoutedEventHandler(method0);
					break;
				case 3:
					textBox = (System.Windows.Controls.TextBox)target;
					break;
				case 4:
					button = (System.Windows.Controls.Button)target;
					button.Click += new RoutedEventHandler(this.button_0_Click);
					break;
				default:
					bool0 = true;
					break;
			}
		}
	}
}