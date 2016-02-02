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
		[NonSerialized]
		private PropertyChangedEventHandler PropertyChangedEventHandler;

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
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChangedEventHandler;
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

		public CodeDir DirectParent { get; set; }

		public int Depth
		{
			get
			{
				int result;
				if (DirectParent == null)
				{
					result = 1;
				}
				else
				{
					result = DirectParent.Depth + 1;
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

		public string Path { get; set; }

		public string ShortPath
		{
			get
			{
				string result;
				if (DirectParent == null)
				{
					result = "";
				}
				else
				{
					result = DirectParent.ShortPath + "\\" + DirectParent.Name;
				}
				return result;
			}
		}

		public string Name { get; set; }

		public ObservableCollection<object> ItemCollection { get; set; }

		public ObservableCollection<CodeDir> Directories { get; set; }

		public ObservableCollection<CodeFile> Files { get; set; }

		public int RawLines
		{
			get
			{
				if (CodeDir.func_0 == null)
				{
					CodeDir.func_0 = new Func<CodeFile, int>(CodeDir.GetRawLines);
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
					CodeDir.func_1 = new Func<CodeFile, int>(CodeDir.GetProcessedLines);
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
				IEnumerable<CodeFile> codeFiles = hashSet;
				if (CodeDir.func_2 == null)
				{
					CodeDir.func_2 = new Func<CodeFile, int>(CodeDir.GetSimilaritiesCount);
				}
				return codeFiles.Sum(CodeDir.func_2);
			}
		}

		public int TotalLines
		{
			get
			{
				if (CodeDir.func_3 == null)
				{
					CodeDir.func_3 = new Func<CodeFile, int>(CodeDir.GetHashesLength);
				}
				return this.method_2(CodeDir.func_3);
			}
		}

		public IEnumerable<CodeDir> GetAllDirectories()
		{
			//CodeDir.<GetAllDirectories>d__0 <GetAllDirectories>d__ = new CodeDir.<GetAllDirectories>d__0(-2);
			//<GetAllDirectories>d__.<>4__this = this;
			//return <GetAllDirectories>d__;

			return null;
		}

		public IEnumerable<CodeFile> GetAllFiles()
		{
			//CodeDir.<GetAllFiles>d__9 <GetAllFiles>d__ = new CodeDir.<GetAllFiles>d__9(-2);
			//<GetAllFiles>d__.<>4__this = this;
			//return <GetAllFiles>d__;

			return null;
		}

		public int GetAllFilesCount()
		{
			int num = Files.Count;
			foreach (CodeDir codeDir in Directories)
			{
				num += codeDir.GetAllFilesCount();
			}
			return num;
		}

		private HashSet<CodeFile> method_0()
		{
			HashSet<CodeFile> hashSet = new HashSet<CodeFile>();
			IEnumerable<CodeFile> files = Files;
			if (CodeDir.func_4 == null)
			{
				CodeDir.func_4 = new Func<CodeFile, IEnumerable<Similarity>>(CodeDir.GetSimilarities);
			}
			IEnumerable<Similarity> arg_4B_0 = files.SelectMany(CodeDir.func_4);
			if (CodeDir.func_5 == null)
			{
				CodeDir.func_5 = new Func<Similarity, CodeFile>(CodeDir.GetOtherFile);
			}
			IEnumerable<CodeFile> range = arg_4B_0.Select(CodeDir.func_5);
			hashSet.AddRange(range);
			foreach (CodeDir current in Directories)
			{
				range = current.method_0();
				hashSet.AddRange(range);
			}
			return hashSet;
		}

		private HashSet<CodeFile> method_1()
		{
			HashSet<CodeFile> hashSet = new HashSet<CodeFile>();
			hashSet.AddRange(Files);

			foreach (CodeDir current in Directories)
			{
				HashSet<CodeFile> range = current.method_1();
				hashSet.AddRange(range);
			}

			return hashSet;
		}

		private int method_2(Func<CodeFile, int> sumFunc)
		{
			int num = Files.Sum(sumFunc);
			foreach (CodeDir current in Directories)
			{
				num += current.method_2(sumFunc);
			}
			return num;
		}

		private void OnPropertyChanged(string str)
		{
			if (this.PropertyChangedEventHandler != null)
			{
				this.PropertyChangedEventHandler(this, new PropertyChangedEventArgs(str));
			}
		}

		[CompilerGenerated]
		private static int GetRawLines(CodeFile codeFile)
		{
			return codeFile.RawLines;
		}

		[CompilerGenerated]
		private static int GetProcessedLines(CodeFile codeFile)
		{
			return codeFile.ProcessedLines;
		}

		[CompilerGenerated]
		private static int GetSimilaritiesCount(CodeFile codeFile)
		{
			return codeFile.Similarities.Count<Similarity>();
		}

		[CompilerGenerated]
		private static int GetHashesLength(CodeFile codeFile)
		{
			return codeFile.Hashes.Length;
		}

		[CompilerGenerated]
		private static IEnumerable<Similarity> GetSimilarities(CodeFile codeFile)
		{
			return codeFile.Similarities;
		}

		[CompilerGenerated]
		private static CodeFile GetOtherFile(Similarity similarity)
		{
			return similarity.OtherFile;
		}
	}
}