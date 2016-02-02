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
		private MainViewModel _mainViewModel = new MainViewModel();

		private static bool bool_0;

		private static string string_0;

		private static string string_1;

		private WheelView wheelView;

		internal ActiproSoftware.Windows.Controls.Ribbon.Controls.Button button_0;

		internal Group group;

		internal DockSite dockSite;

		internal ToolWindow toolWindow;

		internal NoScrollTreeView noScrollTreeView;

		internal ToolWindow toolWindow_1;

		internal ToolWindow toolWindow_2;

		internal ToolWindow toolWindow_3;

		private bool _isInitialized;

		public Window1()
		{
			InitializeComponent();
			base.DataContext = _mainViewModel;
			base.Closing += Window1_Closing;
			base.Loaded += Window1_Loaded;
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
				dockSiteLayoutSerializer.SaveToStream(isolatedStorageFileStream, this.dockSite);
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
						dockSiteLayoutSerializer.LoadFromStream(isolatedStorageFileStream, this.dockSite);
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
				dockSiteLayoutSerializer.LoadFromStream(manifestResourceStream, this.dockSite);
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
				dockSiteLayoutSerializer.SaveToFile(saveFileDialog.FileName, dockSite);
			}
		}

		private void method_3(object sender, RoutedEventArgs e)
		{
			_mainViewModel.Files.Clear();
			new OptionsEdit(_mainViewModel.Options)
			{
				DataContext = _mainViewModel.Options
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
				_mainViewModel.SelectedFile = (CodeFile)e.NewValue;
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
			if (_mainViewModel != null && _mainViewModel.SelectedSimilarity != null && _mainViewModel.SelectedSimilarity.MyFile != null && !string.IsNullOrEmpty(_mainViewModel.SelectedSimilarity.MyFile.Path))
			{
				if (!Window1.bool_0)
				{
					try
					{
						System.Diagnostics.Process.Start("\"" + _mainViewModel.SelectedSimilarity.MyFile.Path + "\"");
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
						System.Diagnostics.Process.Start("\"" + _mainViewModel.SelectedSimilarity.MyFile.Path + "\"");
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
					dTE.ItemOperations.OpenFile(_mainViewModel.SelectedSimilarity.MyFile.Path, "{7651A703-06E5-11D1-8EBD-00A0C90F26EA}");
					IEnumerable<Document> documents = dTE.Documents.Cast<Document>().ToList<Document>();
					foreach (Document current in documents)
					{
						Console.WriteLine(current.FullName);
					}
					IEnumerable<Document> arg_1E4_0 = documents;
					if (func == null)
					{
						func = new Func<Document, bool>(this.method_21);
					}
					Document document = arg_1E4_0.Where(func).First<Document>();
					TextSelection textSelection = (TextSelection)document.Selection;
					textSelection.GotoLine(_mainViewModel.SelectedSimilarity.MyRange.Start + 1, false);
					textSelection.LineDown(true, _mainViewModel.SelectedSimilarity.MyRange.Length - 1);
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
			if (_mainViewModel != null && _mainViewModel.SelectedSimilarity != null && _mainViewModel.SelectedSimilarity.MyFile != null && !string.IsNullOrEmpty(_mainViewModel.SelectedSimilarity.MyFile.Path))
			{
				System.Windows.Clipboard.SetText(_mainViewModel.SelectedSimilarity.MyTextNoLines);
			}
		}

		private void method_6(object sender, RoutedEventArgs e)
		{
			Func<Document, bool> func = null;
			if (_mainViewModel != null && _mainViewModel.SelectedSimilarity != null && _mainViewModel.SelectedSimilarity.OtherFile != null && !string.IsNullOrEmpty(_mainViewModel.SelectedSimilarity.OtherFile.Path))
			{
				if (!Window1.bool_0)
				{
					try
					{
						System.Diagnostics.Process.Start("\"" + _mainViewModel.SelectedSimilarity.OtherFile.Path + "\"");
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
						System.Diagnostics.Process.Start("\"" + _mainViewModel.SelectedSimilarity.OtherFile.Path + "\"");
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
					dTE.ItemOperations.OpenFile(_mainViewModel.SelectedSimilarity.OtherFile.Path, "{7651A703-06E5-11D1-8EBD-00A0C90F26EA}");
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
					textSelection.GotoLine(_mainViewModel.SelectedSimilarity.OtherRange.Start + 1, false);
					textSelection.LineDown(true, _mainViewModel.SelectedSimilarity.OtherRange.Length - 1);
					document.Activate();
				}
				catch
				{
				}
			}
		}

		private void method_7(object sender, RoutedEventArgs e)
		{
			if (_mainViewModel != null && _mainViewModel.SelectedSimilarity != null && _mainViewModel.SelectedSimilarity.OtherFile != null && !string.IsNullOrEmpty(_mainViewModel.SelectedSimilarity.OtherFile.Path))
			{
				System.Windows.Clipboard.SetText(_mainViewModel.SelectedSimilarity.OtherTextNoLines);
			}
		}

		public void AnnotationTextBox_SimilaritySelected(object sender, SimilaritySelectedEventArgs e)
		{
			_mainViewModel.method_0(e.Similarity.MyFile);
			_mainViewModel.SelectedSimilarity = e.Similarity;
		}

		private void method_8(object sender, RoutedEventArgs e)
		{
			using (StreamWriter streamWriter = new StreamWriter("ignoreCode.txt", true))
			{
				streamWriter.Write(_mainViewModel.SelectedSimilarity.MyTextNoLines);
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
					string value = XmlUtil.ConvertToXml(_mainViewModel.Options);
					streamWriter.Write(value);
				}
			}
		}

		private void method_13(object sender, ExecuteRoutedEventArgs e)
		{
			if (new OptionsEdit(_mainViewModel.Options).ShowDialog() == true)
			{
				this.method_14(_mainViewModel.Options);
			}
		}

		private void method_14(Options options)
		{
			AnalyzingWindow analyzingWindow = new AnalyzingWindow(options);
			analyzingWindow.ShowDialog();
			if (analyzingWindow.Analysis.method_5())
			{
				if (this.wheelView != null)
				{
					this.wheelView.Close();
				}
				_mainViewModel = new MainViewModel();
				_mainViewModel.Options = options;
				_mainViewModel.RootDirectories = analyzingWindow.Analysis.RootDirectories;
				_mainViewModel.Files = analyzingWindow.Analysis.Files;
				_mainViewModel.SelectedFile = _mainViewModel.Files.FirstOrDefault<CodeFile>();
				base.DataContext = _mainViewModel;
			}
			else if (analyzingWindow.Analysis.CaughtException != null)
			{
				string str = App.smethod_0(analyzingWindow.Analysis.CaughtException);
				System.Windows.MessageBox.Show("Atomiq has experienced a problem analyzing the directory. We'd really appreciate it if you could email \"" + str + "\" to support@nitriq.com", "Analysis Exception", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		private void method_15(object sender, ExecuteRoutedEventArgs e)
		{
			if (_mainViewModel.BlockCount > 1500)
			{
				MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Whoa, it looks like you have *a lot* of duplicate code there. Drawing this much duplication on the wheel may take a long time to draw and will make interaction with the wheel very very slow. Are you sure you want to continue?", "Continue?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
				if (messageBoxResult != MessageBoxResult.Yes)
				{
					return;
				}
			}
			if (wheelView == null)
			{
				WheelView wheelView = new WheelView(_mainViewModel.Files, _mainViewModel.RootDirectories.First<CodeDir>());
				wheelView.Closed += new EventHandler(this.method_18);
				wheelView.CodeFileSelected += new EventHandler<CodeFileSelectedEventArgs>(this.method_17);
				wheelView.SimilaritySelected += new EventHandler<SimilaritySelectedEventArgs>(this.method_16);
				wheelView.Show();
				this.wheelView = wheelView;
			}
			else
			{
				wheelView.Activate();
			}
		}

		private void method_16(object sender, SimilaritySelectedEventArgs e)
		{
			_mainViewModel.method_0(e.Similarity.MyFile);
			_mainViewModel.SelectedSimilarity = e.Similarity;
			base.Focus();
		}

		private void method_17(object sender, CodeFileSelectedEventArgs e)
		{
			_mainViewModel.SelectedFile = e.CodeFile;
			base.Focus();
		}

		private void method_18(object sender, EventArgs e)
		{
			WheelView wheelView = (WheelView)sender;
			wheelView.Closed -= new EventHandler(this.method_18);
			wheelView.CodeFileSelected -= new EventHandler<CodeFileSelectedEventArgs>(this.method_17);
			wheelView.SimilaritySelected -= new EventHandler<SimilaritySelectedEventArgs>(this.method_16);
			this.wheelView = null;
		}

		private void method_19(object sender, RoutedEventArgs e)
		{
			System.Diagnostics.Process.Start(new ProcessStartInfo("http://www.nimblepros.com/products/atomiq"));
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_isInitialized)
			{
				_isInitialized = true;
				Uri resourceLocator = new Uri("/Atomiq;component/window1.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		internal Delegate method_20(Type type, string str)
		{
			return Delegate.CreateDelegate(type, this, str);
		}

		[EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				button_0 = (ActiproSoftware.Windows.Controls.Ribbon.Controls.Button)target;
				button_0.Click += new EventHandler<ExecuteRoutedEventArgs>(this.method_9);
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
				group = (Group)target;
				break;
			case 7:
				((ActiproSoftware.Windows.Controls.Ribbon.Controls.Button)target).Click += new EventHandler<ExecuteRoutedEventArgs>(this.method_2);
				break;
			case 8:
				dockSite = (DockSite)target;
				break;
			case 9:
				toolWindow = (ToolWindow)target;
				break;
			case 10:
				noScrollTreeView = (NoScrollTreeView)target;
				break;
			case 11:
				toolWindow_1 = (ToolWindow)target;
				break;
			case 12:
				toolWindow_2 = (ToolWindow)target;
				break;
			case 13:
				toolWindow_3 = (ToolWindow)target;
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
				_isInitialized = true;
				break;
			}
		}

		[CompilerGenerated]
		private bool method_21(Document document)
		{
			return document.FullName.ToLowerInvariant() == _mainViewModel.SelectedSimilarity.MyFile.Path.ToLowerInvariant();
		}

		[CompilerGenerated]
		private bool method_22(Document document)
		{
			return document.FullName.ToLowerInvariant() == _mainViewModel.SelectedSimilarity.OtherFile.Path.ToLowerInvariant();
		}
	}
}