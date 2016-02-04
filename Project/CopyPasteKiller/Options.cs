using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Serialization;

namespace CopyPasteKiller
{
	public class Options : INotifyPropertyChanged
	{
		private string _excludeFileRegexes = "[Tt]est\r\nAssemblyInfo\\.(vb|cs)$\r\n\\.g\\.(vb|cs)$\r\n\\.g\\.i\\.(vb|cs)$\r\n\\.Designer\\.(vb|cs)$";

		private ObservableCollection<string> _availableSearchPatterns = new ObservableCollection<string>
		{
			"*.cs",
			"*.vb",
			"*.aspx",
			"*.cshtml",
			"*.xaml",
			"*.py",
			"*.rb",
			"*.java",
			"*.js",
			"*.c",
			"*.cpp",
			"*.as3",
			"*.*"
		};

		private string _fileSearchPattern = "*.cs";

		private string _directory = "";

		private int _minSimilarityLineLength = 10;

		[NonSerialized]
		private PropertyChangedEventHandler propertyChangedEventHandler;

		public event PropertyChangedEventHandler PropertyChanged
		{
			add
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.propertyChangedEventHandler;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Combine(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.propertyChangedEventHandler, value2, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
			remove
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.propertyChangedEventHandler;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Remove(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.propertyChangedEventHandler, value2, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
		}

		public string ExcludeFileRegexes
		{
			get
			{
				return _excludeFileRegexes;
			}
			set
			{
				if (_excludeFileRegexes != value)
				{
					_excludeFileRegexes = value;
					OnPropertyChanged("ExcludeFileRegexes");
				}
			}
		}

		[XmlIgnore]
		public ObservableCollection<string> AvailableSearchPatterns
		{
			get
			{
				return _availableSearchPatterns;
			}
		}

		public string FileSearchPattern
		{
			get
			{
				return _fileSearchPattern;
			}
			set
			{
				if (_availableSearchPatterns.Contains(value) && _fileSearchPattern != value)
				{
					_fileSearchPattern = value;
					OnPropertyChanged("FileSearchPattern");
				}
			}
		}

		public string Directory
		{
			get
			{
				return _directory;
			}
			set
			{
				if (_directory != value)
				{
					_directory = value;
					OnPropertyChanged("Directory");
				}
			}
		}

		public int MinSimilarityLineLength
		{
			get
			{
				return _minSimilarityLineLength;
			}
			set
			{
				if (_minSimilarityLineLength != value)
				{
					_minSimilarityLineLength = value;
					OnPropertyChanged("MinSimilarityLineLength");
				}
			}
		}

		public string ValidateRegexes()
		{
			string[] array = Regex.Split(ExcludeFileRegexes, "\\r?\\n");
			StringBuilder stringBuilder = new StringBuilder();
			string[] array2 = array;

			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];

				if (!(text.Trim() == ""))
				{
					try
					{
						new Regex(text, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
					}
					catch (Exception)
					{
						if (stringBuilder.Length > 0)
						{
							stringBuilder.AppendLine();
						}
						stringBuilder.Append(text);
					}
				}
			}

			return stringBuilder.ToString();
		}

		public List<Regex> GetExcludeRegexes()
		{
			List<Regex> list = new List<Regex>();
			string[] array = Regex.Split(this.ExcludeFileRegexes, "\\r?\\n");
			string[] array2 = array;

			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];

				if (!(text.Trim() == ""))
				{
					Regex item = new Regex(text, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
					list.Add(item);
				}
			}

			return list;
		}

		private void OnPropertyChanged(string str)
		{
			if (propertyChangedEventHandler != null)
			{
				propertyChangedEventHandler(this, new PropertyChangedEventArgs(str));
			}
		}
	}
}