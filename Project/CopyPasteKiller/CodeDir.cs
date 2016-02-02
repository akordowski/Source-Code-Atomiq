using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace CopyPasteKiller
{
	public class CodeDir : INotifyPropertyChanged
	{
		private CodeDir codeDir_0;

		private string string_0;

		private string string_1;

		private ObservableCollection<object> observableCollection_0 = new ObservableCollection<object>();

		private ObservableCollection<CodeDir> observableCollection_1 = new ObservableCollection<CodeDir>();

		private ObservableCollection<CodeFile> observableCollection_2 = new ObservableCollection<CodeFile>();

		[NonSerialized]
		private PropertyChangedEventHandler propertyChangedEventHandler_0;

		[CompilerGenerated]
		private static Func<CodeFile, int> func_0;

		[CompilerGenerated]
		private static Func<CodeFile, int> func_1;

		[CompilerGenerated]
		private static Func<CodeFile, int> func_2;

		[CompilerGenerated]
		private static Func<CodeFile, int> func_3;

		[CompilerGenerated]
		private static Func<CodeFile, IEnumerable<Similarity>> func_4;

		[CompilerGenerated]
		private static Func<Similarity, CodeFile> func_5;

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

		public CodeDir DirectParent
		{
			get
			{
				return this.codeDir_0;
			}
			set
			{
				this.codeDir_0 = value;
			}
		}

		public int Depth
		{
			get
			{
				int result;
				if (this.codeDir_0 == null)
				{
					result = 1;
				}
				else
				{
					result = this.codeDir_0.Depth + 1;
				}
				return result;
			}
		}

		public CodeFile FirstDeepestFile
		{
			get
			{
				CodeFile result;
				if (this.Directories.Count > 0)
				{
					result = this.Directories.First<CodeDir>().FirstDeepestFile;
				}
				else
				{
					result = this.Files.First<CodeFile>();
				}
				return result;
			}
		}

		public string Path
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

		public string ShortPath
		{
			get
			{
				string result;
				if (this.codeDir_0 == null)
				{
					result = "";
				}
				else
				{
					result = this.codeDir_0.ShortPath + "\\" + this.codeDir_0.Name;
				}
				return result;
			}
		}

		public string Name
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

		public ObservableCollection<object> ItemCollection
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

		public ObservableCollection<CodeDir> Directories
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

		public ObservableCollection<CodeFile> Files
		{
			get
			{
				return this.observableCollection_2;
			}
			set
			{
				this.observableCollection_2 = value;
			}
		}

		public int RawLines
		{
			get
			{
				if (CodeDir.func_0 == null)
				{
					CodeDir.func_0 = new Func<CodeFile, int>(CodeDir.smethod_0);
				}
				return this.method_2(CodeDir.func_0);
			}
		}

		public int ProcessedLines
		{
			get
			{
				if (CodeDir.func_1 == null)
				{
					CodeDir.func_1 = new Func<CodeFile, int>(CodeDir.smethod_1);
				}
				return this.method_2(CodeDir.func_1);
			}
		}

		public int FilesWithSharedSimilarities
		{
			get
			{
				HashSet<CodeFile> hashSet = this.method_0();
				return hashSet.Count;
			}
		}

		public int Blocks
		{
			get
			{
				HashSet<CodeFile> hashSet = this.method_1();
				IEnumerable<CodeFile> arg_25_0 = hashSet;
				if (CodeDir.func_2 == null)
				{
					CodeDir.func_2 = new Func<CodeFile, int>(CodeDir.smethod_2);
				}
				return arg_25_0.Sum(CodeDir.func_2);
			}
		}

		public int TotalLines
		{
			get
			{
				if (CodeDir.func_3 == null)
				{
					CodeDir.func_3 = new Func<CodeFile, int>(CodeDir.smethod_3);
				}
				return this.method_2(CodeDir.func_3);
			}
		}

		public IEnumerable<CodeDir> GetAllDirectories()
		{
			CodeDir.<GetAllDirectories>d__0 <GetAllDirectories>d__ = new CodeDir.<GetAllDirectories>d__0(-2);
			<GetAllDirectories>d__.<>4__this = this;
			return <GetAllDirectories>d__;
		}

		public IEnumerable<CodeFile> GetAllFiles()
		{
			CodeDir.<GetAllFiles>d__9 <GetAllFiles>d__ = new CodeDir.<GetAllFiles>d__9(-2);
			<GetAllFiles>d__.<>4__this = this;
			return <GetAllFiles>d__;
		}

		public int GetAllFilesCount()
		{
			int num = this.observableCollection_2.Count;
			foreach (CodeDir current in this.observableCollection_1)
			{
				num += current.GetAllFilesCount();
			}
			return num;
		}

		private HashSet<CodeFile> method_0()
		{
			HashSet<CodeFile> hashSet = new HashSet<CodeFile>();
			IEnumerable<CodeFile> arg_29_0 = this.observableCollection_2;
			if (CodeDir.func_4 == null)
			{
				CodeDir.func_4 = new Func<CodeFile, IEnumerable<Similarity>>(CodeDir.smethod_4);
			}
			IEnumerable<Similarity> arg_4B_0 = arg_29_0.SelectMany(CodeDir.func_4);
			if (CodeDir.func_5 == null)
			{
				CodeDir.func_5 = new Func<Similarity, CodeFile>(CodeDir.smethod_5);
			}
			IEnumerable<CodeFile> range = arg_4B_0.Select(CodeDir.func_5);
			hashSet.AddRange(range);
			foreach (CodeDir current in this.observableCollection_1)
			{
				range = current.method_0();
				hashSet.AddRange(range);
			}
			return hashSet;
		}

		private HashSet<CodeFile> method_1()
		{
			HashSet<CodeFile> hashSet = new HashSet<CodeFile>();
			hashSet.AddRange(this.observableCollection_2);
			foreach (CodeDir current in this.observableCollection_1)
			{
				HashSet<CodeFile> range = current.method_1();
				hashSet.AddRange(range);
			}
			return hashSet;
		}

		private int method_2(Func<CodeFile, int> sumFunc)
		{
			int num = this.observableCollection_2.Sum(sumFunc);
			foreach (CodeDir current in this.observableCollection_1)
			{
				num += current.method_2(sumFunc);
			}
			return num;
		}

		private void method_3(string string_2)
		{
			if (this.propertyChangedEventHandler_0 != null)
			{
				this.propertyChangedEventHandler_0(this, new PropertyChangedEventArgs(string_2));
			}
		}

		[CompilerGenerated]
		private static int smethod_0(CodeFile codeFile_0)
		{
			return codeFile_0.RawLines;
		}

		[CompilerGenerated]
		private static int smethod_1(CodeFile codeFile_0)
		{
			return codeFile_0.ProcessedLines;
		}

		[CompilerGenerated]
		private static int smethod_2(CodeFile codeFile_0)
		{
			return codeFile_0.Similarities.Count<Similarity>();
		}

		[CompilerGenerated]
		private static int smethod_3(CodeFile codeFile_0)
		{
			return codeFile_0.Hashes.Length;
		}

		[CompilerGenerated]
		private static IEnumerable<Similarity> smethod_4(CodeFile codeFile_0)
		{
			return codeFile_0.Similarities;
		}

		[CompilerGenerated]
		private static CodeFile smethod_5(Similarity similarity_0)
		{
			return similarity_0.OtherFile;
		}
	}
}
