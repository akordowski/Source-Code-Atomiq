using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CopyPasteKiller
{
	public class WheelViewModel
	{
		[CompilerGenerated]
		private static Func<char, bool> func0;

		[CompilerGenerated]
		private static Func<char, bool> func1;

		public SortedDictionary<string, int> DirectoryToLoc { get; private set; }

		public Dictionary<string, int> DirectoryToLevel { get; private set; }

		public int DeepestDir { get; private set; }

		public IList<CodeFile> CodeFiles { get; private set; }

		public double TotalLineSize { get; private set; }

		public double Diameter { get; private set; }

		public WheelViewModel(IList<CodeFile> files)
		{
			Diameter = 600;

			int num = 0;

			foreach (CodeFile current in files)
			{
				method0(current);
				num += current.ProcessedLines;
			}

			TotalLineSize = (double)num;
			CodeFiles = files;
		}

		private void method0(CodeFile codeFile)
		{
			IEnumerable<char> shortPaths = codeFile.ShortPath;

			if (WheelViewModel.func0 == null)
			{
				WheelViewModel.func0 = new Func<char, bool>(WheelViewModel.smethod0);
			}

			int num = shortPaths.Count(WheelViewModel.func0);

			if (DeepestDir < num)
			{
				DeepestDir = num;
			}
			string text = codeFile.ShortPath.TrimStart(new char[]
			{
				'\\'
			});

			while (text.Length > 0)
			{
				if (!DirectoryToLoc.ContainsKey(text))
				{
					DirectoryToLoc.Add(text, 0);
					Dictionary<string, int> arg_A3_0 = DirectoryToLevel;
					string arg_A3_1 = text;
					IEnumerable<char> arg_9C_0 = text;
					if (WheelViewModel.func1 == null)
					{
						WheelViewModel.func1 = new Func<char, bool>(WheelViewModel.smethod1);
					}
					arg_A3_0.Add(arg_A3_1, arg_9C_0.Count(WheelViewModel.func1) + 1);
				}
				SortedDictionary<string, int> sortedDictionary;
				string key;
				(sortedDictionary = DirectoryToLoc)[key = text] = sortedDictionary[key] + codeFile.Hashes.Length;
				text = Path.GetDirectoryName(text);
			}
		}

		internal IEnumerable<Similarity> method1()
		{
			Dictionary<int, Similarity> dictionary = new Dictionary<int, Similarity>();

			foreach (CodeFile current in CodeFiles)
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
		private static bool smethod0(char char0)
		{
			return char0 == '\\';
		}

		[CompilerGenerated]
		private static bool smethod1(char char0)
		{
			return char0 == '\\';
		}
	}
}