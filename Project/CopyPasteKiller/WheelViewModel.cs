using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CopyPasteKiller
{
	public class WheelViewModel
	{
		private SortedDictionary<string, int> sortedDictionary_0 = new SortedDictionary<string, int>();

		private Dictionary<string, int> dictionary_0 = new Dictionary<string, int>();

		private int int_0;

		private IList<CodeFile> ilist_0;

		private double double_0;

		private double double_1 = 600.0;

		[CompilerGenerated]
		private static Func<char, bool> func_0;

		[CompilerGenerated]
		private static Func<char, bool> func_1;

		public SortedDictionary<string, int> DirectoryToLoc
		{
			get
			{
				return this.sortedDictionary_0;
			}
		}

		public Dictionary<string, int> DirectoryToLevel
		{
			get
			{
				return this.dictionary_0;
			}
		}

		public int DeepestDir
		{
			get
			{
				return this.int_0;
			}
		}

		public IList<CodeFile> CodeFiles
		{
			get
			{
				return this.ilist_0;
			}
		}

		public double TotalLineSize
		{
			get
			{
				return this.double_0;
			}
			set
			{
				this.double_0 = value;
			}
		}

		public double Diameter
		{
			get
			{
				return this.double_1;
			}
			set
			{
				this.double_1 = value;
			}
		}

		public WheelViewModel(IList<CodeFile> files)
		{
			int num = 0;
			foreach (CodeFile current in files)
			{
				this.method_0(current);
				num += current.ProcessedLines;
			}
			this.double_0 = (double)num;
			this.ilist_0 = files;
		}

		private void method_0(CodeFile codeFile_0)
		{
			IEnumerable<char> arg_23_0 = codeFile_0.ShortPath;
			if (WheelViewModel.func_0 == null)
			{
				WheelViewModel.func_0 = new Func<char, bool>(WheelViewModel.smethod_0);
			}
			int num = arg_23_0.Count(WheelViewModel.func_0);
			if (this.int_0 < num)
			{
				this.int_0 = num;
			}
			string text = codeFile_0.ShortPath.TrimStart(new char[]
			{
				'\\'
			});
			while (text.Length > 0)
			{
				if (!this.sortedDictionary_0.ContainsKey(text))
				{
					this.sortedDictionary_0.Add(text, 0);
					Dictionary<string, int> arg_A3_0 = this.dictionary_0;
					string arg_A3_1 = text;
					IEnumerable<char> arg_9C_0 = text;
					if (WheelViewModel.func_1 == null)
					{
						WheelViewModel.func_1 = new Func<char, bool>(WheelViewModel.smethod_1);
					}
					arg_A3_0.Add(arg_A3_1, arg_9C_0.Count(WheelViewModel.func_1) + 1);
				}
				SortedDictionary<string, int> sortedDictionary;
				string key;
				(sortedDictionary = this.sortedDictionary_0)[key = text] = sortedDictionary[key] + codeFile_0.Hashes.Length;
				text = Path.GetDirectoryName(text);
			}
		}

		internal IEnumerable<Similarity> method_1()
		{
			Dictionary<int, Similarity> dictionary = new Dictionary<int, Similarity>();
			foreach (CodeFile current in this.ilist_0)
			{
				foreach (Similarity current2 in current.Similarities)
				{
					if (!dictionary.ContainsKey(current2.UniqueId))
					{
						dictionary.Add(current2.UniqueId, current2);
					}
				}
			}
			return dictionary.Values;
		}

		[CompilerGenerated]
		private static bool smethod_0(char char_0)
		{
			return char_0 == '\\';
		}

		[CompilerGenerated]
		private static bool smethod_1(char char_0)
		{
			return char_0 == '\\';
		}
	}
}
