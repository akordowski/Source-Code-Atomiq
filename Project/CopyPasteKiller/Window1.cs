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

		private static bool bool0;

		private static string string0;

		private static string string1;

		private WheelView _wheelView;

		internal ActiproSoftware.Windows.Controls.Ribbon.Controls.Button button0;

		internal Group group;

		internal DockSite dockSite;

		internal ToolWindow toolWindow;

		internal NoScrollTreeView noScrollTreeView;

		internal ToolWindow toolWindow1;

		internal ToolWindow toolWindow2;

		internal ToolWindow toolWindow3;

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
			method0();

			if (!string.IsNullOrEmpty(App.InitialProject) && File.Exists(App.InitialProject))
			{
				LoadAtomiqProject(App.InitialProject);
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

		private void method0()
		{
			try
			{
				using (IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream("dock.site", FileMode.OpenOrCreate))
				{
					if (isolatedStorageFileStream.Length == 0L)
					{
						method1();
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
				method1();
			}
		}

		private void method1()
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

		private void saveButton_Click(object sender, RoutedEventArgs e)
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

		private void method3(object sender, RoutedEventArgs e)
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
			Window1.bool0 = false;
			Window1.string0 = null;
			Window1.string1 = null;
		}

		private void method4(object sender, RoutedEventArgs e)
		{
			Func<Document, bool> func = null;

			if (_mainViewModel != null && _mainViewModel.SelectedSimilarity != null && _mainViewModel.SelectedSimilarity.MyFile != null && !string.IsNullOrEmpty(_mainViewModel.SelectedSimilarity.MyFile.Path))
			{
				if (!Window1.bool0)
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

						dTE = (DTE2)Marshal.GetActiveObject(Window1.string0);
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

						dTE = (DTE2)Marshal.GetActiveObject(Window1.string0);
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
						func = new Func<Document, bool>(method21);
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
		private static extern int SetForegroundWindow(IntPtr intptr);

		[DllImport("user32.dll")]
		private static extern int ShowWindow(IntPtr intptr, int int0);

		private void method5(object sender, RoutedEventArgs e)
		{
			if (_mainViewModel != null && _mainViewModel.SelectedSimilarity != null && _mainViewModel.SelectedSimilarity.MyFile != null && !string.IsNullOrEmpty(_mainViewModel.SelectedSimilarity.MyFile.Path))
			{
				System.Windows.Clipboard.SetText(_mainViewModel.SelectedSimilarity.MyTextNoLines);
			}
		}

		private void method6(object sender, RoutedEventArgs e)
		{
			Func<Document, bool> func = null;

			if (_mainViewModel != null && _mainViewModel.SelectedSimilarity != null && _mainViewModel.SelectedSimilarity.OtherFile != null && !string.IsNullOrEmpty(_mainViewModel.SelectedSimilarity.OtherFile.Path))
			{
				if (!Window1.bool0)
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

						dTE = (DTE2)Marshal.GetActiveObject(Window1.string0);
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

						dTE = (DTE2)Marshal.GetActiveObject(Window1.string0);
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
						func = new Func<Document, bool>(method22);
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

		private void method7(object sender, RoutedEventArgs e)
		{
			if (_mainViewModel != null && _mainViewModel.SelectedSimilarity != null && _mainViewModel.SelectedSimilarity.OtherFile != null && !string.IsNullOrEmpty(_mainViewModel.SelectedSimilarity.OtherFile.Path))
			{
				System.Windows.Clipboard.SetText(_mainViewModel.SelectedSimilarity.OtherTextNoLines);
			}
		}

		public void AnnotationTextBox_SimilaritySelected(object sender, SimilaritySelectedEventArgs e)
		{
			_mainViewModel.method0(e.Similarity.MyFile);
			_mainViewModel.SelectedSimilarity = e.Similarity;
		}

		private void method8(object sender, RoutedEventArgs e)
		{
			using (StreamWriter streamWriter = new StreamWriter("ignoreCode.txt", true))
			{
				streamWriter.Write(_mainViewModel.SelectedSimilarity.MyTextNoLines);
				streamWriter.Write("~~CPK Code Chunk Delimiter~~");
			}
		}

		private void method9(object sender, ExecuteRoutedEventArgs e)
		{
			Options options = new Options();
			if (new OptionsEdit(options).ShowDialog() == true)
			{
				this.Reanalyze(options);
			}
		}

		private void LoadAtomiqProject(string str)
		{
			string xml;

			using (StreamReader streamReader = new StreamReader(str))
			{
				xml = streamReader.ReadToEnd();
			}

			Options options = XmlUtil.FromXml<Options>(xml);

			if (new OptionsEdit(options).ShowDialog() == true)
			{
				Reanalyze(options);
			}
		}

		private void openButton_Click(object sender, ExecuteRoutedEventArgs e)
		{
			Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
			dialog.Title = "Load Atomiq Project";
			dialog.DefaultExt = "atomiqProj";
			dialog.Filter = "Atomiq Project (*.atomiqProj)|*.atomiqProj";
			dialog.RestoreDirectory = true;

			if (dialog.ShowDialog() == true)
			{
				LoadAtomiqProject(dialog.FileName);
			}
		}

		private void saveButton_Click(object sender, ExecuteRoutedEventArgs e)
		{
			Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
			dialog.Title = "Save Atomiq Project";
			dialog.DefaultExt = "atomiqProj";
			dialog.Filter = "Atomiq Project (*.atomiqProj)|*.atomiqProj";
			dialog.RestoreDirectory = true;

			if (dialog.ShowDialog() == true)
			{
				using (StreamWriter streamWriter = new StreamWriter(dialog.FileName))
				{
					string value = XmlUtil.ConvertToXml(_mainViewModel.Options);
					streamWriter.Write(value);
				}
			}
		}

		private void reanalyzeButton_Click(object sender, ExecuteRoutedEventArgs e)
		{
			if (new OptionsEdit(_mainViewModel.Options).ShowDialog() == true)
			{
				Reanalyze(_mainViewModel.Options);
			}
		}

		private void Reanalyze(Options options)
		{
			AnalyzingWindow analyzingWindow = new AnalyzingWindow(options);
			analyzingWindow.ShowDialog();

			if (analyzingWindow.Analysis.method5())
			{
				if (_wheelView != null)
				{
					_wheelView.Close();
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
				string str = App.smethod0(analyzingWindow.Analysis.CaughtException);
				System.Windows.MessageBox.Show("Atomiq has experienced a problem analyzing the directory. We'd really appreciate it if you could email \"" + str + "\" to support@nitriq.com", "Analysis Exception", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		private void wheelButton_Click(object sender, ExecuteRoutedEventArgs e)
		{
			if (_mainViewModel.BlockCount > 1500)
			{
				MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Whoa, it looks like you have *a lot* of duplicate code there. Drawing this much duplication on the wheel may take a long time to draw and will make interaction with the wheel very very slow. Are you sure you want to continue?", "Continue?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
				if (messageBoxResult != MessageBoxResult.Yes)
				{
					return;
				}
			}

			if (_wheelView == null)
			{
				WheelView wheelView = new WheelView(_mainViewModel.Files, _mainViewModel.RootDirectories.First<CodeDir>());
				wheelView.Closed += new EventHandler(wheelView_Closed);
				wheelView.CodeFileSelected += new EventHandler<CodeFileSelectedEventArgs>(wheelView_CodeFileSelected);
				wheelView.SimilaritySelected += new EventHandler<SimilaritySelectedEventArgs>(wheelView_SimilaritySelected);
				wheelView.Show();
				this._wheelView = wheelView;
			}
			else
			{
				_wheelView.Activate();
			}
		}

		private void wheelView_SimilaritySelected(object sender, SimilaritySelectedEventArgs e)
		{
			_mainViewModel.method0(e.Similarity.MyFile);
			_mainViewModel.SelectedSimilarity = e.Similarity;
			base.Focus();
		}

		private void wheelView_CodeFileSelected(object sender, CodeFileSelectedEventArgs e)
		{
			_mainViewModel.SelectedFile = e.CodeFile;
			base.Focus();
		}

		private void wheelView_Closed(object sender, EventArgs e)
		{
			WheelView wheelView = (WheelView)sender;
			wheelView.Closed -= new EventHandler(this.wheelView_Closed);
			wheelView.CodeFileSelected -= new EventHandler<CodeFileSelectedEventArgs>(this.wheelView_CodeFileSelected);
			wheelView.SimilaritySelected -= new EventHandler<SimilaritySelectedEventArgs>(this.wheelView_SimilaritySelected);
			_wheelView = null;
		}

		private void method19(object sender, RoutedEventArgs e)
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
		internal Delegate method20(Type type, string str)
		{
			return Delegate.CreateDelegate(type, this, str);
		}

		[EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
				case 1:
					button0 = (ActiproSoftware.Windows.Controls.Ribbon.Controls.Button)target;
					button0.Click += new EventHandler<ExecuteRoutedEventArgs>(method9);
					break;
				case 2:
					((ActiproSoftware.Windows.Controls.Ribbon.Controls.Button)target).Click += new EventHandler<ExecuteRoutedEventArgs>(this.openButton_Click);
					break;
				case 3:
					((ActiproSoftware.Windows.Controls.Ribbon.Controls.Button)target).Click += new EventHandler<ExecuteRoutedEventArgs>(this.saveButton_Click);
					break;
				case 4:
					((ActiproSoftware.Windows.Controls.Ribbon.Controls.Button)target).Click += new EventHandler<ExecuteRoutedEventArgs>(this.reanalyzeButton_Click);
					break;
				case 5:
					((ActiproSoftware.Windows.Controls.Ribbon.Controls.Button)target).Click += new EventHandler<ExecuteRoutedEventArgs>(this.wheelButton_Click);
					break;
				case 6:
					group = (Group)target;
					break;
				case 7:
					((ActiproSoftware.Windows.Controls.Ribbon.Controls.Button)target).Click += new EventHandler<ExecuteRoutedEventArgs>(this.saveButton_Click);
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
					toolWindow1 = (ToolWindow)target;
					break;
				case 12:
					toolWindow2 = (ToolWindow)target;
					break;
				case 13:
					toolWindow3 = (ToolWindow)target;
					break;
				case 14:
					((System.Windows.Controls.Button)target).Click += new RoutedEventHandler(method4);
					break;
				case 15:
					((System.Windows.Controls.Button)target).Click += new RoutedEventHandler(method5);
					break;
				case 16:
					((System.Windows.Controls.Button)target).Click += new RoutedEventHandler(method8);
					break;
				case 17:
					((System.Windows.Controls.Button)target).Click += new RoutedEventHandler(method6);
					break;
				case 18:
					((System.Windows.Controls.Button)target).Click += new RoutedEventHandler(method7);
					break;
				default:
					_isInitialized = true;
					break;
			}
		}

		[CompilerGenerated]
		private bool method21(Document document)
		{
			return document.FullName.ToLowerInvariant() == _mainViewModel.SelectedSimilarity.MyFile.Path.ToLowerInvariant();
		}

		[CompilerGenerated]
		private bool method22(Document document)
		{
			return document.FullName.ToLowerInvariant() == _mainViewModel.SelectedSimilarity.OtherFile.Path.ToLowerInvariant();
		}
	}
}