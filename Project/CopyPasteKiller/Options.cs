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
		private string string_0 = "[Tt]est\r\nAssemblyInfo\\.(vb|cs)$\r\n\\.g\\.(vb|cs)$\r\n\\.g\\.i\\.(vb|cs)$\r\n\\.Designer\\.(vb|cs)$";

		private ObservableCollection<string> observableCollection_0 = new ObservableCollection<string>
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

		private string string_1 = "*.cs";

		private string string_2 = "";

		private int int_0 = 10;

		[NonSerialized]
		private PropertyChangedEventHandler propertyChangedEventHandler_0;

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

		public string ExcludeFileRegexes
		{
			get
			{
				return this.string_0;
			}
			set
			{
				if (this.string_0 != value)
				{
					this.string_0 = value;
					this.method_0("ExcludeFileRegexes");
				}
			}
		}

		[XmlIgnore]
		public ObservableCollection<string> AvailableSearchPatterns
		{
			get
			{
				return this.observableCollection_0;
			}
		}

		public string FileSearchPattern
		{
			get
			{
				return this.string_1;
			}
			set
			{
				if (this.observableCollection_0.Contains(value) && this.string_1 != value)
				{
					this.string_1 = value;
					this.method_0("FileSearchPattern");
				}
			}
		}

		public string Directory
		{
			get
			{
				return this.string_2;
			}
			set
			{
				if (this.string_2 != value)
				{
					this.string_2 = value;
					this.method_0("Directory");
				}
			}
		}

		public int MinSimilarityLineLength
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
					this.method_0("MinSimilarityLineLength");
				}
			}
		}

		public string ValidateRegexes()
		{
			string[] array = Regex.Split(this.ExcludeFileRegexes, "\\r?\\n");
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

		private void method_0(string string_3)
		{
			if (this.propertyChangedEventHandler_0 != null)
			{
				this.propertyChangedEventHandler_0(this, new PropertyChangedEventArgs(string_3));
			}
		}
	}
}
