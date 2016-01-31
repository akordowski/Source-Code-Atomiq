using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace CopyPasteKiller
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private string string_0;

		private string string_1;

		private int int_0;

		private RibbonIconProvider ribbonIconProvider_0 = new RibbonIconProvider();

		private ObservableCollection<CodeDir> observableCollection_0 = new ObservableCollection<CodeDir>();

		private ObservableCollection<CodeFile> observableCollection_1 = new ObservableCollection<CodeFile>();

		private CodeFile codeFile_0;

		private Similarity similarity_0;

		private Options options_0;

		private bool bool_0;

		[NonSerialized]
		private PropertyChangedEventHandler propertyChangedEventHandler_0;

		[CompilerGenerated]
		private static Func<CodeFile, IEnumerable<int>> func_0;

		[CompilerGenerated]
		private static Func<Similarity, int> func_1;

		[CompilerGenerated]
		private static Func<CodeFile, IEnumerable<int>> func_2;

		[CompilerGenerated]
		private static Func<Similarity, int> func_3;

		public event PropertyChangedEventHandler PropertyChanged
		{
			add
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.propertyChangedEventHandler_0;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Combine(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.propertyChangedEventHandler_0, value2, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
			remove
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.propertyChangedEventHandler_0;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Remove(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.propertyChangedEventHandler_0, value2, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
		}

		public string LicenseName
		{
			get
			{
				return this.string_0;
			}
			set
			{
				this.string_0 = value;
			}
		}

		public string LicenseCompany
		{
			get
			{
				return this.string_1;
			}
			set
			{
				this.string_1 = value;
			}
		}

		public int TrialDays
		{
			get
			{
				return this.int_0;
			}
			set
			{
				if (this.int_0 != value)
				{
					this.int_0 = value;
					this.method_1("TrialDays");
				}
			}
		}

		public RibbonIconProvider RibbonIcons
		{
			get
			{
				return this.ribbonIconProvider_0;
			}
		}

		public ObservableCollection<CodeDir> RootDirectories
		{
			get
			{
				return this.observableCollection_0;
			}
			set
			{
				this.observableCollection_0 = value;
			}
		}

		public ObservableCollection<CodeFile> Files
		{
			get
			{
				return this.observableCollection_1;
			}
			set
			{
				this.observableCollection_1 = value;
			}
		}

		public CodeFile SelectedFile
		{
			get
			{
				return this.codeFile_0;
			}
			set
			{
				if (this.codeFile_0 != value)
				{
					this.codeFile_0 = value;
					this.method_1("SelectedFile");
					this.SelectedSimilarity = this.codeFile_0.Similarities.First<Similarity>();
				}
			}
		}

		public Similarity SelectedSimilarity
		{
			get
			{
				return this.similarity_0;
			}
			set
			{
				this.similarity_0 = value;
				this.method_1("SelectedSimilarity");
			}
		}

		public Options Options
		{
			get
			{
				return this.options_0;
			}
			set
			{
				if (this.options_0 != value)
				{
					this.options_0 = value;
					this.method_1("Options");
					this.method_1("SaveEnabled");
				}
			}
		}

		public bool SaveEnabled
		{
			get
			{
				return this.options_0 != null;
			}
		}

		public int BlockCount
		{
			get
			{
				IEnumerable<CodeFile> arg_23_0 = this.observableCollection_1;
				if (MainViewModel.func_0 == null)
				{
					MainViewModel.func_0 = new Func<CodeFile, IEnumerable<int>>(MainViewModel.smethod_0);
				}
				List<int> list = arg_23_0.SelectMany(MainViewModel.func_0).ToList<int>();
				int result;
				if (list.Count == 0)
				{
					result = 0;
				}
				else
				{
					result = list.Count / 2;
				}
				return result;
			}
		}

		public int LineLengthCount
		{
			get
			{
				IEnumerable<CodeFile> arg_23_0 = this.observableCollection_1;
				if (MainViewModel.func_2 == null)
				{
					MainViewModel.func_2 = new Func<CodeFile, IEnumerable<int>>(MainViewModel.smethod_2);
				}
				List<int> list = arg_23_0.SelectMany(MainViewModel.func_2).ToList<int>();
				int result;
				if (list.Count == 0)
				{
					result = 0;
				}
				else
				{
					result = list.Sum() / 2;
				}
				return result;
			}
		}

		public string Version
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}

		internal void method_0(CodeFile codeFile_1)
		{
			if (this.codeFile_0 != codeFile_1)
			{
				this.codeFile_0 = codeFile_1;
				this.method_1("SelectedFile");
			}
		}

		private void method_1(string string_2)
		{
			if (this.propertyChangedEventHandler_0 != null)
			{
				this.propertyChangedEventHandler_0(this, new PropertyChangedEventArgs(string_2));
			}
		}

		[CompilerGenerated]
		private static IEnumerable<int> smethod_0(CodeFile codeFile_1)
		{
			IEnumerable<Similarity> arg_23_0 = codeFile_1.Similarities;
			if (MainViewModel.func_1 == null)
			{
				MainViewModel.func_1 = new Func<Similarity, int>(MainViewModel.smethod_1);
			}
			return arg_23_0.Select(MainViewModel.func_1);
		}

		[CompilerGenerated]
		private static int smethod_1(Similarity similarity_1)
		{
			return similarity_1.MyRange.Length;
		}

		[CompilerGenerated]
		private static IEnumerable<int> smethod_2(CodeFile codeFile_1)
		{
			IEnumerable<Similarity> arg_23_0 = codeFile_1.Similarities;
			if (MainViewModel.func_3 == null)
			{
				MainViewModel.func_3 = new Func<Similarity, int>(MainViewModel.smethod_3);
			}
			return arg_23_0.Select(MainViewModel.func_3);
		}

		[CompilerGenerated]
		private static int smethod_3(Similarity similarity_1)
		{
			return similarity_1.MyHashIndexRange.Length;
		}
	}
}
