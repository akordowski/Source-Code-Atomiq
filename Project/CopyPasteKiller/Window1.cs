using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Controls.Docking.Serialization;
using ActiproSoftware.Windows.Controls.Ribbon.Controls;
using EnvDTE;
using EnvDTE80;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Markup;

namespace CopyPasteKiller
{
	public class Window1 : System.Windows.Window, IComponentConnector
	{
		private MainViewModel mainViewModel_0 = new MainViewModel();

		private static bool bool_0;

		private static string string_0;

		private static string string_1;

		private WheelView wheelView_0;

		internal ActiproSoftware.Windows.Controls.Ribbon.Controls.Button button_0;

		internal Group group_0;

		internal DockSite dockSite_0;

		internal ToolWindow toolWindow_0;

		internal NoScrollTreeView noScrollTreeView_0;

		internal ToolWindow toolWindow_1;

		internal ToolWindow toolWindow_2;

		internal ToolWindow toolWindow_3;

		private bool bool_1;

		public Window1()
		{
			this.InitializeComponent();
			base.DataContext = this.mainViewModel_0;
			base.Closing += new CancelEventHandler(this.Window1_Closing);
			base.Loaded += new RoutedEventHandler(this.Window1_Loaded);
		}

		private void Window1_Loaded(object sender, RoutedEventArgs e)
		{
			this.method_0();
			if (!string.IsNullOrEmpty(App.InitialProject) && File.Exists(App.InitialProject))
			{
				this.method_10(App.InitialProject);
			}
		}

		private void Window1_Closing(object sender, EventArgs e)
		{
			using (IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream("dock.site", FileMode.Create))
			{
				DockSiteLayoutSerializer dockSiteLayoutSerializer = new DockSiteLayoutSerializer();
				dockSiteLayoutSerializer.SaveToStream(isolatedStorageFileStream, this.dockSite_0);
			}
		}

		private void method_0()
		{
			try
			{
				using (IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream("dock.site", FileMode.OpenOrCreate))
				{
					if (isolatedStorageFileStream.Length == 0L)
					{
						this.method_1();
					}
					else
					{
						DockSiteLayoutSerializer dockSiteLayoutSerializer = new DockSiteLayoutSerializer();
						dockSiteLayoutSerializer.LoadFromStream(isolatedStorageFileStream, this.dockSite_0);
					}
				}
			}
			catch (Exception)
			{
				this.method_1();
			}
		}

		private void method_1()
		{
			try
			{
				Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CopyPasteKiller.Resources.dock.site");
				DockSiteLayoutSerializer dockSiteLayoutSerializer = new DockSiteLayoutSerializer();
				dockSiteLayoutSerializer.LoadFromStream(manifestResourceStream, this.dockSite_0);
			}
			catch (Exception)
			{
			}
		}

		private void method_2(object sender, RoutedEventArgs e)
		{
			System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			saveFileDialog.Title = "Save Tool Window Layout";
			saveFileDialog.DefaultExt = "site";
			saveFileDialog.Filter = "Dock Site (*.site)|*.site";
			saveFileDialog.RestoreDirectory = true;
			if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				DockSiteLayoutSerializer dockSiteLayoutSerializer = new DockSiteLayoutSerializer();
				dockSiteLayoutSerializer.SaveToFile(saveFileDialog.FileName, this.dockSite_0);
			}
		}

		private void method_3(object sender, RoutedEventArgs e)
		{
			this.mainViewModel_0.Files.Clear();
			new OptionsEdit(this.mainViewModel_0.Options)
			{
				DataContext = this.mainViewModel_0.Options
			}.ShowDialog();
			if (!(new RecentDirectories().ShowDialog() != true))
			{
			}
		}

		public void tree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			if (e.NewValue is CodeFile)
			{
				Console.WriteLine(((CodeFile)e.NewValue).Name);
				this.mainViewModel_0.SelectedFile = (CodeFile)e.NewValue;
			}
		}

		static Window1()
		{
			Window1.bool_0 = false;
			Window1.string_0 = null;
			Window1.string_1 = null;
		}

		private void method_4(object sender, RoutedEventArgs e)
		{
			Func<Document, bool> func = null;
			if (this.mainViewModel_0 != null && this.mainViewModel_0.SelectedSimilarity != null && this.mainViewModel_0.SelectedSimilarity.MyFile != null && !string.IsNullOrEmpty(this.mainViewModel_0.SelectedSimilarity.MyFile.Path))
			{
				if (!Window1.bool_0)
				{
					try
					{
						System.Diagnostics.Process.Start("\"" + this.mainViewModel_0.SelectedSimilarity.MyFile.Path + "\"");
						return;
					}
					catch (Exception)
					{
						System.Windows.MessageBox.Show("Couldn't open the file");
						return;
					}
				}
				try
				{
					DTE2 dTE = null;
					try
					{
						System.Diagnostics.Process[] processesByName = System.Diagnostics.Process.GetProcessesByName("devenv");
						if (processesByName.Length > 0)
						{
							Window1.ShowWindow(processesByName[processesByName.Length - 1].MainWindowHandle, 4);
							Window1.SetForegroundWindow(processesByName[processesByName.Length - 1].MainWindowHandle);
						}
						dTE = (DTE2)Marshal.GetActiveObject(Window1.string_0);
					}
					catch (Exception)
					{
						System.Diagnostics.Process.Start("\"" + this.mainViewModel_0.SelectedSimilarity.MyFile.Path + "\"");
					}
					if (dTE != null)
					{
						System.Diagnostics.Process[] processesByName = System.Diagnostics.Process.GetProcessesByName("devenv");
						if (processesByName.Length > 0)
						{
							Window1.ShowWindow(processesByName[processesByName.Length - 1].MainWindowHandle, 4);
							Window1.SetForegroundWindow(processesByName[processesByName.Length - 1].MainWindowHandle);
						}
						dTE = (DTE2)Marshal.GetActiveObject(Window1.string_0);
					}
					dTE.ItemOperations.OpenFile(this.mainViewModel_0.SelectedSimilarity.MyFile.Path, "{7651A703-06E5-11D1-8EBD-00A0C90F26EA}");
					IEnumerable<Document> enumerable = dTE.Documents.Cast<Document>().ToList<Document>();
					foreach (Document current in enumerable)
					{
						Console.WriteLine(current.FullName);
					}
					IEnumerable<Document> arg_1E4_0 = enumerable;
					if (func == null)
					{
						func = new Func<Document, bool>(this.method_21);
					}
					Document document = arg_1E4_0.Where(func).First<Document>();
					TextSelection textSelection = (TextSelection)document.Selection;
					textSelection.GotoLine(this.mainViewModel_0.SelectedSimilarity.MyRange.Start + 1, false);
					textSelection.LineDown(true, this.mainViewModel_0.SelectedSimilarity.MyRange.Length - 1);
					document.Activate();
				}
				catch
				{
				}
			}
		}

		[DllImport("user32.dll")]
		private static extern int SetForegroundWindow(IntPtr intptr_0);

		[DllImport("user32.dll")]
		private static extern int ShowWindow(IntPtr intptr_0, int int_0);

		private void method_5(object sender, RoutedEventArgs e)
		{
			if (this.mainViewModel_0 != null && this.mainViewModel_0.SelectedSimilarity != null && this.mainViewModel_0.SelectedSimilarity.MyFile != null && !string.IsNullOrEmpty(this.mainViewModel_0.SelectedSimilarity.MyFile.Path))
			{
				System.Windows.Clipboard.SetText(this.mainViewModel_0.SelectedSimilarity.MyTextNoLines);
			}
		}

		private void method_6(object sender, RoutedEventArgs e)
		{
			Func<Document, bool> func = null;
			if (this.mainViewModel_0 != null && this.mainViewModel_0.SelectedSimilarity != null && this.mainViewModel_0.SelectedSimilarity.OtherFile != null && !string.IsNullOrEmpty(this.mainViewModel_0.SelectedSimilarity.OtherFile.Path))
			{
				if (!Window1.bool_0)
				{
					try
					{
						System.Diagnostics.Process.Start("\"" + this.mainViewModel_0.SelectedSimilarity.OtherFile.Path + "\"");
						return;
					}
					catch (Exception)
					{
						System.Windows.MessageBox.Show("Couldn't open the file");
						return;
					}
				}
				try
				{
					DTE2 dTE = null;
					try
					{
						System.Diagnostics.Process[] processesByName = System.Diagnostics.Process.GetProcessesByName("devenv");
						if (processesByName.Length > 0)
						{
							Window1.ShowWindow(processesByName[processesByName.Length - 1].MainWindowHandle, 4);
							Window1.SetForegroundWindow(processesByName[processesByName.Length - 1].MainWindowHandle);
						}
						dTE = (DTE2)Marshal.GetActiveObject(Window1.string_0);
					}
					catch (Exception)
					{
						System.Diagnostics.Process.Start("\"" + this.mainViewModel_0.SelectedSimilarity.OtherFile.Path + "\"");
					}
					if (dTE != null)
					{
						System.Diagnostics.Process[] processesByName = System.Diagnostics.Process.GetProcessesByName("devenv");
						if (processesByName.Length > 0)
						{
							Window1.ShowWindow(processesByName[processesByName.Length - 1].MainWindowHandle, 4);
							Window1.SetForegroundWindow(processesByName[processesByName.Length - 1].MainWindowHandle);
						}
						dTE = (DTE2)Marshal.GetActiveObject(Window1.string_0);
					}
					dTE.ItemOperations.OpenFile(this.mainViewModel_0.SelectedSimilarity.OtherFile.Path, "{7651A703-06E5-11D1-8EBD-00A0C90F26EA}");
					IEnumerable<Document> enumerable = dTE.Documents.Cast<Document>().ToList<Document>();
					foreach (Document current in enumerable)
					{
						Console.WriteLine(current.FullName);
					}
					IEnumerable<Document> arg_1E4_0 = enumerable;
					if (func == null)
					{
						func = new Func<Document, bool>(this.method_22);
					}
					Document document = arg_1E4_0.Where(func).First<Document>();
					TextSelection textSelection = (TextSelection)document.Selection;
					textSelection.GotoLine(this.mainViewModel_0.SelectedSimilarity.OtherRange.Start + 1, false);
					textSelection.LineDown(true, this.mainViewModel_0.SelectedSimilarity.OtherRange.Length - 1);
					document.Activate();
				}
				catch
				{
				}
			}
		}

		private void method_7(object sender, RoutedEventArgs e)
		{
			if (this.mainViewModel_0 != null && this.mainViewModel_0.SelectedSimilarity != null && this.mainViewModel_0.SelectedSimilarity.OtherFile != null && !string.IsNullOrEmpty(this.mainViewModel_0.SelectedSimilarity.OtherFile.Path))
			{
				System.Windows.Clipboard.SetText(this.mainViewModel_0.SelectedSimilarity.OtherTextNoLines);
			}
		}

		public void AnnotationTextBox_SimilaritySelected(object sender, SimilaritySelectedEventArgs e)
		{
			this.mainViewModel_0.method_0(e.Similarity.MyFile);
			this.mainViewModel_0.SelectedSimilarity = e.Similarity;
		}

		private void method_8(object sender, RoutedEventArgs e)
		{
			using (StreamWriter streamWriter = new StreamWriter("ignoreCode.txt", true))
			{
				streamWriter.Write(this.mainViewModel_0.SelectedSimilarity.MyTextNoLines);
				streamWriter.Write("~~CPK Code Chunk Delimiter~~");
			}
		}

		private void method_9(object sender, ExecuteRoutedEventArgs e)
		{
			Options options = new Options();
			if (new OptionsEdit(options).ShowDialog() == true)
			{
				this.method_14(options);
			}
		}

		private void method_10(string string_2)
		{
			string xml;
			using (StreamReader streamReader = new StreamReader(string_2))
			{
				xml = streamReader.ReadToEnd();
			}
			Options options = XmlUtil.FromXml<Options>(xml);
			if (new OptionsEdit(options).ShowDialog() == true)
			{
				this.method_14(options);
			}
		}

		private void method_11(object sender, ExecuteRoutedEventArgs e)
		{
			Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
			openFileDialog.Title = "Load Atomiq Project";
			openFileDialog.DefaultExt = "atomiqProj";
			openFileDialog.Filter = "Atomiq Project (*.atomiqProj)|*.atomiqProj";
			openFileDialog.RestoreDirectory = true;
			if (openFileDialog.ShowDialog() == true)
			{
				this.method_10(openFileDialog.FileName);
			}
		}

		private void method_12(object sender, ExecuteRoutedEventArgs e)
		{
			Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
			saveFileDialog.Title = "Save Atomiq Project";
			saveFileDialog.DefaultExt = "atomiqProj";
			saveFileDialog.Filter = "Atomiq Project (*.atomiqProj)|*.atomiqProj";
			saveFileDialog.RestoreDirectory = true;
			if (saveFileDialog.ShowDialog() == true)
			{
				using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName))
				{
					string value = XmlUtil.ConvertToXml(this.mainViewModel_0.Options);
					streamWriter.Write(value);
				}
			}
		}

		private void method_13(object sender, ExecuteRoutedEventArgs e)
		{
			if (new OptionsEdit(this.mainViewModel_0.Options).ShowDialog() == true)
			{
				this.method_14(this.mainViewModel_0.Options);
			}
		}

		private void method_14(Options options_0)
		{
			AnalyzingWindow analyzingWindow = new AnalyzingWindow(options_0);
			analyzingWindow.ShowDialog();
			if (analyzingWindow.Analysis.method_5())
			{
				if (this.wheelView_0 != null)
				{
					this.wheelView_0.Close();
				}
				this.mainViewModel_0 = new MainViewModel();
				this.mainViewModel_0.Options = options_0;
				this.mainViewModel_0.RootDirectories = analyzingWindow.Analysis.RootDirectories;
				this.mainViewModel_0.Files = analyzingWindow.Analysis.Files;
				this.mainViewModel_0.SelectedFile = this.mainViewModel_0.Files.FirstOrDefault<CodeFile>();
				base.DataContext = this.mainViewModel_0;
			}
			else if (analyzingWindow.Analysis.CaughtException != null)
			{
				string str = App.smethod_0(analyzingWindow.Analysis.CaughtException);
				System.Windows.MessageBox.Show("Atomiq has experienced a problem analyzing the directory. We'd really appreciate it if you could email \"" + str + "\" to support@nitriq.com", "Analysis Exception", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		private void method_15(object sender, ExecuteRoutedEventArgs e)
		{
			if (this.mainViewModel_0.BlockCount > 1500)
			{
				MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Whoa, it looks like you have *a lot* of duplicate code there. Drawing this much duplication on the wheel may take a long time to draw and will make interaction with the wheel very very slow. Are you sure you want to continue?", "Continue?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
				if (messageBoxResult != MessageBoxResult.Yes)
				{
					return;
				}
			}
			if (this.wheelView_0 == null)
			{
				WheelView wheelView = new WheelView(this.mainViewModel_0.Files, this.mainViewModel_0.RootDirectories.First<CodeDir>());
				wheelView.Closed += new EventHandler(this.method_18);
				wheelView.CodeFileSelected += new EventHandler<CodeFileSelectedEventArgs>(this.method_17);
				wheelView.SimilaritySelected += new EventHandler<SimilaritySelectedEventArgs>(this.method_16);
				wheelView.Show();
				this.wheelView_0 = wheelView;
			}
			else
			{
				this.wheelView_0.Activate();
			}
		}

		private void method_16(object sender, SimilaritySelectedEventArgs e)
		{
			this.mainViewModel_0.method_0(e.Similarity.MyFile);
			this.mainViewModel_0.SelectedSimilarity = e.Similarity;
			base.Focus();
		}

		private void method_17(object sender, CodeFileSelectedEventArgs e)
		{
			this.mainViewModel_0.SelectedFile = e.CodeFile;
			base.Focus();
		}

		private void method_18(object sender, EventArgs e)
		{
			WheelView wheelView = (WheelView)sender;
			wheelView.Closed -= new EventHandler(this.method_18);
			wheelView.CodeFileSelected -= new EventHandler<CodeFileSelectedEventArgs>(this.method_17);
			wheelView.SimilaritySelected -= new EventHandler<SimilaritySelectedEventArgs>(this.method_16);
			this.wheelView_0 = null;
		}

		private void method_19(object sender, RoutedEventArgs e)
		{
			System.Diagnostics.Process.Start(new ProcessStartInfo("http://www.nimblepros.com/products/atomiq"));
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!this.bool_1)
			{
				this.bool_1 = true;
				Uri resourceLocator = new Uri("/Atomiq;component/window1.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		internal Delegate method_20(Type type_0, string string_2)
		{
			return Delegate.CreateDelegate(type_0, this, string_2);
		}

		[EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.button_0 = (ActiproSoftware.Windows.Controls.Ribbon.Controls.Button)target;
				this.button_0.Click += new EventHandler<ExecuteRoutedEventArgs>(this.method_9);
				break;
			case 2:
				((ActiproSoftware.Windows.Controls.Ribbon.Controls.Button)target).Click += new EventHandler<ExecuteRoutedEventArgs>(this.method_11);
				break;
			case 3:
				((ActiproSoftware.Windows.Controls.Ribbon.Controls.Button)target).Click += new EventHandler<ExecuteRoutedEventArgs>(this.method_12);
				break;
			case 4:
				((ActiproSoftware.Windows.Controls.Ribbon.Controls.Button)target).Click += new EventHandler<ExecuteRoutedEventArgs>(this.method_13);
				break;
			case 5:
				((ActiproSoftware.Windows.Controls.Ribbon.Controls.Button)target).Click += new EventHandler<ExecuteRoutedEventArgs>(this.method_15);
				break;
			case 6:
				this.group_0 = (Group)target;
				break;
			case 7:
				((ActiproSoftware.Windows.Controls.Ribbon.Controls.Button)target).Click += new EventHandler<ExecuteRoutedEventArgs>(this.method_2);
				break;
			case 8:
				this.dockSite_0 = (DockSite)target;
				break;
			case 9:
				this.toolWindow_0 = (ToolWindow)target;
				break;
			case 10:
				this.noScrollTreeView_0 = (NoScrollTreeView)target;
				break;
			case 11:
				this.toolWindow_1 = (ToolWindow)target;
				break;
			case 12:
				this.toolWindow_2 = (ToolWindow)target;
				break;
			case 13:
				this.toolWindow_3 = (ToolWindow)target;
				break;
			case 14:
				((System.Windows.Controls.Button)target).Click += new RoutedEventHandler(this.method_4);
				break;
			case 15:
				((System.Windows.Controls.Button)target).Click += new RoutedEventHandler(this.method_5);
				break;
			case 16:
				((System.Windows.Controls.Button)target).Click += new RoutedEventHandler(this.method_8);
				break;
			case 17:
				((System.Windows.Controls.Button)target).Click += new RoutedEventHandler(this.method_6);
				break;
			case 18:
				((System.Windows.Controls.Button)target).Click += new RoutedEventHandler(this.method_7);
				break;
			default:
				this.bool_1 = true;
				break;
			}
		}

		[CompilerGenerated]
		private bool method_21(Document document_0)
		{
			return document_0.FullName.ToLowerInvariant() == this.mainViewModel_0.SelectedSimilarity.MyFile.Path.ToLowerInvariant();
		}

		[CompilerGenerated]
		private bool method_22(Document document_0)
		{
			return document_0.FullName.ToLowerInvariant() == this.mainViewModel_0.SelectedSimilarity.OtherFile.Path.ToLowerInvariant();
		}
	}
}
