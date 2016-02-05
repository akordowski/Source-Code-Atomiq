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
		private PropertyChangedEventHandler _propertyChangedEventHandler;

		[CompilerGenerated]
		private static Func<CodeFile, int> func0;

		[CompilerGenerated]
		private static Func<CodeFile, int> func1;

		[CompilerGenerated]
		private static Func<CodeFile, int> func2;

		[CompilerGenerated]
		private static Func<CodeFile, int> func3;

		[CompilerGenerated]
		private static Func<CodeFile, IEnumerable<Similarity>> func4;

		[CompilerGenerated]
		private static Func<Similarity, CodeFile> func5;

		public event PropertyChangedEventHandler PropertyChanged
		{
			add
			{
				PropertyChangedEventHandler propertyChangedEventHandler = _propertyChangedEventHandler;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Combine(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref _propertyChangedEventHandler, value2, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
			remove
			{
				PropertyChangedEventHandler propertyChangedEventHandler = _propertyChangedEventHandler;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Remove(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref _propertyChangedEventHandler, value2, propertyChangedEventHandler2);
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

				if (Directories.Count > 0)
				{
					result = Directories.First<CodeDir>().FirstDeepestFile;
				}
				else
				{
					result = Files.First<CodeFile>();
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
				if (CodeDir.func0 == null)
				{
					CodeDir.func0 = new Func<CodeFile, int>(CodeDir.GetRawLines);
				}

				return method2(CodeDir.func0);
			}
		}

		public int ProcessedLines
		{
			get
			{
				if (CodeDir.func1 == null)
				{
					CodeDir.func1 = new Func<CodeFile, int>(CodeDir.GetProcessedLines);
				}

				return method2(CodeDir.func1);
			}
		}

		public int FilesWithSharedSimilarities
		{
			get
			{
				HashSet<CodeFile> hashSet = method0();
				return hashSet.Count;
			}
		}

		public int Blocks
		{
			get
			{
				HashSet<CodeFile> hashSet = method1();
				IEnumerable<CodeFile> codeFiles = hashSet;

				if (CodeDir.func2 == null)
				{
					CodeDir.func2 = new Func<CodeFile, int>(CodeDir.GetSimilaritiesCount);
				}

				return codeFiles.Sum(CodeDir.func2);
			}
		}

		public int TotalLines
		{
			get
			{
				if (CodeDir.func3 == null)
				{
					CodeDir.func3 = new Func<CodeFile, int>(CodeDir.GetHashesLength);
				}

				return method2(CodeDir.func3);
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

			foreach (CodeDir current in Directories)
			{
				num += current.GetAllFilesCount();
			}

			return num;
		}

		private HashSet<CodeFile> method0()
		{
			HashSet<CodeFile> hashSet = new HashSet<CodeFile>();
			IEnumerable<CodeFile> codeFiles = Files;

			if (CodeDir.func4 == null)
			{
				CodeDir.func4 = new Func<CodeFile, IEnumerable<Similarity>>(CodeDir.GetSimilarities);
			}

			IEnumerable<Similarity> similarites = codeFiles.SelectMany(CodeDir.func4);

			if (CodeDir.func5 == null)
			{
				CodeDir.func5 = new Func<Similarity, CodeFile>(CodeDir.GetOtherFile);
			}

			IEnumerable<CodeFile> range = similarites.Select(CodeDir.func5);
			hashSet.AddRange(range);

			foreach (CodeDir current in Directories)
			{
				range = current.method0();
				hashSet.AddRange(range);
			}

			return hashSet;
		}

		private HashSet<CodeFile> method1()
		{
			HashSet<CodeFile> hashSet = new HashSet<CodeFile>();
			hashSet.AddRange(Files);

			foreach (CodeDir directory in Directories)
			{
				HashSet<CodeFile> range = directory.method1();
				hashSet.AddRange(range);
			}

			return hashSet;
		}

		private int method2(Func<CodeFile, int> sumFunc)
		{
			int num = Files.Sum(sumFunc);

			foreach (CodeDir directory in Directories)
			{
				num += directory.method2(sumFunc);
			}

			return num;
		}

		private void OnPropertyChanged(string str)
		{
			if (_propertyChangedEventHandler != null)
			{
				_propertyChangedEventHandler(this, new PropertyChangedEventArgs(str));
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