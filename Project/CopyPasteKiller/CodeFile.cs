using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace CopyPasteKiller
{
	[DebuggerDisplay("CodeFile: {Name}")]
	public class CodeFile
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex0;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex1;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex2;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex3;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex4;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex5;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex6;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex7;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex8;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex9;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex10;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex11;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex12;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex13;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex14;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex15;

		private static int int0;

		private int int3;

		[CompilerGenerated]
		private static Func<string, string> func0;

		[CompilerGenerated]
		private static Func<string, string> func1;

		[CompilerGenerated]
		private static Func<string, string> func2;

		[CompilerGenerated]
		private static Func<string, string> func3;

		[CompilerGenerated]
		private static Func<string, string> func4;

		[CompilerGenerated]
		private static Func<string, string> func5;

		[CompilerGenerated]
		private static Func<string, string> func6;

		[CompilerGenerated]
		private static Func<string, string> func7;

		[CompilerGenerated]
		private static Func<Similarity, int> func8;

		[CompilerGenerated]
		private static Func<Similarity, int> func9;

		[CompilerGenerated]
		private static Func<Similarity, CodeFile> func10;

		public string Path { get; set; }

		public string ShortPath { get; set; }

		public string Name
		{
			get
			{
				return System.IO.Path.GetFileName(Path);
			}
		}

		public string Extension
		{
			get
			{
				return System.IO.Path.GetExtension(Path);
			}
		}

		public string Code { get; set; }

		public string StrippedCode { get; set; }

		public int[] Hashes { get; set; }

		public string[] StrippedLines { get; set; }

		public int[] HashIndexToLineIndex { get; set; }

		public string[] Lines { get; set; }

		public int ProcessedLines
		{
			get
			{
				IEnumerable<Similarity> similarities = Similarities;

				if (CodeFile.func8 == null)
				{
					CodeFile.func8 = new Func<Similarity, int>(CodeFile.GetMyHashIndexRangeLength);
				}

				List<int> list = similarities.Select(CodeFile.func8).ToList<int>();
				int result;

				if (list.Count == 0)
				{
					result = 0;
				}
				else
				{
					result = list.Sum();
				}

				return result;
			}
		}

		public int RawLines
		{
			get
			{
				IEnumerable<Similarity> similarities = Similarities;

				if (CodeFile.func9 == null)
				{
					CodeFile.func9 = new Func<Similarity, int>(CodeFile.GetMyRangeLength);
				}

				List<int> list = similarities.Select(CodeFile.func9).ToList<int>();
				int result;

				if (list.Count == 0)
				{
					result = 0;
				}
				else
				{
					result = list.Sum();
				}

				return result;
			}
		}

		public int FilesWithSharedSimilarities
		{
			get
			{
				IEnumerable<Similarity> similarities = Similarities;

				if (CodeFile.func10 == null)
				{
					CodeFile.func10 = new Func<Similarity, CodeFile>(CodeFile.GetOtherFile);
				}

				return similarities.Select(CodeFile.func10).Distinct<CodeFile>().Count<CodeFile>();
			}
		}

		public int Blocks
		{
			get
			{
				return Similarities.Count;
			}
		}

		public List<Similarity> Similarities { get; set; }

		public Dictionary<int, string> HashToLine { get; set; }

		public CodeDir DirectParent { get; private set; }

		public CodeFile(string path, string code, CodeDir directParent)
		{
			Path = path;
			Code = code;
			method0(Code);
			DirectParent = directParent;
		}

		private static string smethod0(Match match)
		{
			return Regex.Replace(match.Groups["comment"].Value, "[^\\r\\n]+", "");
		}

		private void method0(string string6)
		{
			List<string> list = new List<string>();
			List<int> list2 = new List<int>();
			List<string> list3 = new List<string>();
			List<int> list4 = new List<int>();

			if (Extension == ".cs" || Extension == ".java" || Extension == ".js" || Extension == ".c" || Extension == ".cpp" || Extension == ".as3")
			{
				string6 = Regex.Replace(string6, "/\\*(?<comment>.*?)\\*/", new MatchEvaluator(CodeFile.smethod0), RegexOptions.Singleline);
			}

			if (Extension == ".xaml" || Extension == ".html" || Extension == ".aspx" || Extension == ".xml")
			{
				string6 = Regex.Replace(string6, "\\<!--(?<comment>.*?)--\\>", new MatchEvaluator(CodeFile.smethod0), RegexOptions.Singleline);
			}

			if (Extension == ".cshtml")
			{
				string6 = Regex.Replace(string6, "\\<!--(?<comment>.*?)--\\>", new MatchEvaluator(CodeFile.smethod0), RegexOptions.Singleline);
				string6 = Regex.Replace(string6, "@\\*(?<comment>.*?)\\*@", new MatchEvaluator(CodeFile.smethod0), RegexOptions.Singleline);
			}

			if (Extension == ".rb" || Extension == ".py")
			{
				string text = "\"\"\"";
				string6 = Regex.Replace(string6, text + "(?<comment>.*?)" + text, new MatchEvaluator(CodeFile.smethod0), RegexOptions.Singleline);
			}

			if (Extension == ".py")
			{
				string text2 = "'''";
				string6 = Regex.Replace(string6, text2 + "(?<comment>.*?)" + text2, new MatchEvaluator(CodeFile.smethod0), RegexOptions.Singleline);
			}

			string[] array = Regex.Split(string6, "\\r?\\n");

			if (CodeFile.func0 == null)
			{
				CodeFile.func0 = new Func<string, string>(CodeFile.smethod1);
			}

			Func<string, string> func = CodeFile.func0;

			if (Extension == ".cs")
			{
				if (CodeFile.func1 == null)
				{
					CodeFile.func1 = new Func<string, string>(CodeFile.smethod2);
				}

				func = CodeFile.func1;
			}
			else if (Extension == ".java" || Extension == ".js" || Extension == ".as3")
			{
				if (CodeFile.func2 == null)
				{
					CodeFile.func2 = new Func<string, string>(CodeFile.smethod3);
				}

				func = CodeFile.func2;
			}
			else if (Extension == ".c" || Extension == ".cpp")
			{
				if (CodeFile.func3 == null)
				{
					CodeFile.func3 = new Func<string, string>(CodeFile.smethod4);
				}

				func = CodeFile.func3;
			}
			else if (Extension == ".vb")
			{
				if (CodeFile.func4 == null)
				{
					CodeFile.func4 = new Func<string, string>(CodeFile.smethod5);
				}

				func = CodeFile.func4;
			}
			else if (Extension == ".xml" || Extension == ".xaml" || Extension == ".html" || Extension == ".aspx" || Extension == ".cshtml")
			{
				if (CodeFile.func5 == null)
				{
					CodeFile.func5 = new Func<string, string>(CodeFile.smethod6);
				}

				func = CodeFile.func5;
			}
			else if (Extension == ".css")
			{
				if (CodeFile.func6 == null)
				{
					CodeFile.func6 = new Func<string, string>(CodeFile.smethod7);
				}

				func = CodeFile.func6;
			}
			else if (Extension == ".py" || Extension == ".rb")
			{
				if (CodeFile.func7 == null)
				{
					CodeFile.func7 = new Func<string, string>(CodeFile.smethod8);
				}

				func = CodeFile.func7;
			}

			string[] array2 = array;

			for (int i = 0; i < array2.Length; i++)
			{
				string text3 = array2[i];
				string text4 = text3.ToLowerInvariant();
				text4 = func(text4);
				text4 = CodeFile.regex14.Replace(text4, "");

				if (text4 != "")
				{
					int hashCode = text4.GetHashCode();
					list2.Add(hashCode);
					list3.Add(text4);
					list4.Add(list.Count);

					if (!HashToLine.ContainsKey(hashCode))
					{
						HashToLine.Add(hashCode, text4);
					}
				}

				list.Add(CodeFile.regex15.Replace(text3, "    "));
			}

			list4.Add(list.Count);
			Hashes = list2.ToArray();
			StrippedLines = list3.ToArray();
			HashIndexToLineIndex = list4.ToArray();
			Lines = list.ToArray();
		}

		internal void method1()
		{
			if (Similarities.Count == 0)
			{
				Code = null;
				StrippedCode = null;
				Hashes = null;
				StrippedLines = null;
				HashIndexToLineIndex = null;
				Lines = null;
			}

			HashToLine = null;
		}

		[CompilerGenerated]
		private static string smethod1(string str)
		{
			return str;
		}

		[CompilerGenerated]
		private static string smethod2(string str)
		{
			str = CodeFile.regex2.Replace(str, "");
			str = CodeFile.regex4.Replace(str, "");
			str = CodeFile.regex13.Replace(str, "");

			return str;
		}

		[CompilerGenerated]
		private static string smethod3(string str)
		{
			str = CodeFile.regex2.Replace(str, "");
			str = CodeFile.regex5.Replace(str, "");
			str = CodeFile.regex13.Replace(str, "");

			return str;
		}

		[CompilerGenerated]
		private static string smethod4(string str)
		{
			str = CodeFile.regex2.Replace(str, "");
			str = CodeFile.regex6.Replace(str, "");
			str = CodeFile.regex13.Replace(str, "");

			return str;
		}

		[CompilerGenerated]
		private static string smethod5(string str)
		{
			str = CodeFile.regex3.Replace(str, "");
			str = CodeFile.regex7.Replace(str, "");
			str = CodeFile.regex8.Replace(str, "");
			str = CodeFile.regex9.Replace(str, "");
			str = CodeFile.regex10.Replace(str, "");

			return str;
		}

		[CompilerGenerated]
		private static string smethod6(string str)
		{
			return str;
		}

		[CompilerGenerated]
		private static string smethod7(string str)
		{
			return str;
		}

		[CompilerGenerated]
		private static string smethod8(string str)
		{
			str = CodeFile.regex12.Replace(str, "");
			return str;
		}

		[CompilerGenerated]
		private static int GetMyHashIndexRangeLength(Similarity similarity)
		{
			return similarity.MyHashIndexRange.Length;
		}

		[CompilerGenerated]
		private static int GetMyRangeLength(Similarity similarity)
		{
			return similarity.MyRange.Length;
		}

		[CompilerGenerated]
		private static CodeFile GetOtherFile(Similarity similarity)
		{
			return similarity.OtherFile;
		}

		static CodeFile()
		{
			CodeFile.regex0 = new Regex("@?\"[^\"]*\"", RegexOptions.Compiled);
			CodeFile.regex1 = new Regex("'.'", RegexOptions.Compiled);
			CodeFile.regex2 = new Regex("//.*$", RegexOptions.Compiled);
			CodeFile.regex3 = new Regex("'.*$", RegexOptions.Compiled);
			CodeFile.regex4 = new Regex("\\s*using\\s*[\\w\\.]+\\s*;\\s*", RegexOptions.Compiled);
			CodeFile.regex5 = new Regex("\\s*import(s?)\\s*[\\w\\.*]+\\s*;?\\s*", RegexOptions.Compiled);
			CodeFile.regex6 = new Regex("\\#include[^\\r\\n]+\\r?\\n", RegexOptions.Compiled);
			CodeFile.regex7 = new Regex("\\s*imports\\s*[\\w\\.]+\\s*", RegexOptions.Compiled);
			CodeFile.regex8 = new Regex("^\\s*next\\s*$", RegexOptions.Compiled);
			CodeFile.regex9 = new Regex("^\\s*(g|s)et\\s*$", RegexOptions.Compiled);
			CodeFile.regex10 = new Regex("^\\s*end\\s*(if|property|while|try)\\s*$", RegexOptions.Compiled);
			CodeFile.regex11 = new Regex("\\s*imports\\s*[\\w\\.]+\\s*", RegexOptions.Compiled);
			CodeFile.regex12 = new Regex("\\#.*$", RegexOptions.Compiled);
			CodeFile.regex13 = new Regex("{|}", RegexOptions.Compiled);
			CodeFile.regex14 = new Regex("\\s", RegexOptions.Compiled);
			CodeFile.regex15 = new Regex("\\t", RegexOptions.Compiled);
			CodeFile.int0 = 4;
		}
	}
}