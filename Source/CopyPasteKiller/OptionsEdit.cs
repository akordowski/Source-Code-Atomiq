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
		private Options options_0;

		internal DockPanel dockPanel_0;

		internal System.Windows.Controls.TextBox textBox_0;

		internal System.Windows.Controls.Button button_0;

		private bool bool_0;

		public OptionsEdit(Options options)
		{
			this.InitializeComponent();
			this.options_0 = options;
			base.DataContext = this.options_0;
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.ShowNewFolderButton = false;
			folderBrowserDialog.Description = "Pick Code Directory To Analyze";
			folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
			folderBrowserDialog.SelectedPath = this.options_0.Directory;
			if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.options_0.Directory = folderBrowserDialog.SelectedPath;
			}
		}

		private void button_0_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(this.options_0.Directory))
			{
				System.Windows.MessageBox.Show("You have not selected a directory to analyze", "Select Directory", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			else
			{
				string text = this.options_0.ValidateRegexes();
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
			if (!this.bool_0)
			{
				this.bool_0 = true;
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
				this.dockPanel_0 = (DockPanel)target;
				break;
			case 2:
				((System.Windows.Controls.Button)target).Click += new RoutedEventHandler(this.method_0);
				break;
			case 3:
				this.textBox_0 = (System.Windows.Controls.TextBox)target;
				break;
			case 4:
				this.button_0 = (System.Windows.Controls.Button)target;
				this.button_0.Click += new RoutedEventHandler(this.button_0_Click);
				break;
			default:
				this.bool_0 = true;
				break;
			}
		}
	}
}
