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
		private bool bool0;

		[NonSerialized]
		private PropertyChangedEventHandler PropertyChangedEventHandler;

		[CompilerGenerated]
		private static Func<CodeFile, IEnumerable<int>> func0;

		[CompilerGenerated]
		private static Func<Similarity, int> func1;

		[CompilerGenerated]
		private static Func<CodeFile, IEnumerable<int>> func2;

		[CompilerGenerated]
		private static Func<Similarity, int> func3;

		public event PropertyChangedEventHandler PropertyChanged
		{
			add
			{
				PropertyChangedEventHandler propertyChangedEventHandler = PropertyChangedEventHandler;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Combine(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChangedEventHandler, value2, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
			remove
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChangedEventHandler;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Remove(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChangedEventHandler, value2, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
		}

		public string LicenseName { get; set; }

		public string LicenseCompany { get; set; }

		private int _trialDays;

		public int TrialDays
		{
			get
			{
				return _trialDays;
			}
			set
			{
				if (_trialDays != value)
				{
					_trialDays = value;
					OnPropertyChanged("TrialDays");
				}
			}
		}

		public RibbonIconProvider RibbonIcons { get; private set; }

		public ObservableCollection<CodeDir> RootDirectories { get; set; }

		public ObservableCollection<CodeFile> Files { get; set; }

		private CodeFile _selectedFile;

		public CodeFile SelectedFile
		{
			get
			{
				return _selectedFile;
			}
			set
			{
				if (_selectedFile != value)
				{
					_selectedFile = value;
					OnPropertyChanged("SelectedFile");
					SelectedSimilarity = _selectedFile.Similarities.First<Similarity>();
				}
			}
		}

		private Similarity _selectedSimilarity;

		public Similarity SelectedSimilarity
		{
			get
			{
				return _selectedSimilarity;
			}
			set
			{
				_selectedSimilarity = value;
				OnPropertyChanged("SelectedSimilarity");
			}
		}

		private Options _options;

		public Options Options
		{
			get
			{
				return _options;
			}
			set
			{
				if (_options != value)
				{
					_options = value;
					OnPropertyChanged("Options");
					OnPropertyChanged("SaveEnabled");
				}
			}
		}

		public bool SaveEnabled
		{
			get
			{
				return _options != null;
			}
		}

		public int BlockCount
		{
			get
			{
				IEnumerable<CodeFile> files = Files;

				if (MainViewModel.func0 == null)
				{
					MainViewModel.func0 = new Func<CodeFile, IEnumerable<int>>(MainViewModel.smethod0);
				}

				List<int> list = files.SelectMany(MainViewModel.func0).ToList<int>();
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
				IEnumerable<CodeFile> files = Files;

				if (MainViewModel.func2 == null)
				{
					MainViewModel.func2 = new Func<CodeFile, IEnumerable<int>>(MainViewModel.smethod2);
				}

				List<int> list = files.SelectMany(MainViewModel.func2).ToList<int>();
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

		internal void method0(CodeFile codeFile)
		{
			if (_selectedFile != codeFile)
			{
				_selectedFile = codeFile;
				OnPropertyChanged("SelectedFile");
			}
		}

		private void OnPropertyChanged(string str)
		{
			if (PropertyChangedEventHandler != null)
			{
				PropertyChangedEventHandler(this, new PropertyChangedEventArgs(str));
			}
		}

		[CompilerGenerated]
		private static IEnumerable<int> smethod0(CodeFile codeFile)
		{
			IEnumerable<Similarity> similarities = codeFile.Similarities;

			if (MainViewModel.func1 == null)
			{
				MainViewModel.func1 = new Func<Similarity, int>(MainViewModel.GetMyRangeLength);
			}

			return similarities.Select(MainViewModel.func1);
		}

		[CompilerGenerated]
		private static int GetMyRangeLength(Similarity similarity)
		{
			return similarity.MyRange.Length;
		}

		[CompilerGenerated]
		private static IEnumerable<int> smethod2(CodeFile codeFile)
		{
			IEnumerable<Similarity> similarities = codeFile.Similarities;

			if (MainViewModel.func3 == null)
			{
				MainViewModel.func3 = new Func<Similarity, int>(MainViewModel.GetMyHashIndexRangeLength);
			}

			return similarities.Select(MainViewModel.func3);
		}

		[CompilerGenerated]
		private static int GetMyHashIndexRangeLength(Similarity similarity)
		{
			return similarity.MyHashIndexRange.Length;
		}
	}
}