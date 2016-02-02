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
		private static Regex regex_0;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex_1;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex_2;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex_3;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex_4;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex_5;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex_6;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex_7;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex_8;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex_9;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex_10;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex_11;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex_12;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex_13;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex_14;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex_15;

		private static int int_0;

		private int int_3;

		[CompilerGenerated]
		private static Func<string, string> func_0;

		[CompilerGenerated]
		private static Func<string, string> func_1;

		[CompilerGenerated]
		private static Func<string, string> func_2;

		[CompilerGenerated]
		private static Func<string, string> func_3;

		[CompilerGenerated]
		private static Func<string, string> func_4;

		[CompilerGenerated]
		private static Func<string, string> func_5;

		[CompilerGenerated]
		private static Func<string, string> func_6;

		[CompilerGenerated]
		private static Func<string, string> func_7;

		[CompilerGenerated]
		private static Func<Similarity, int> func_8;

		[CompilerGenerated]
		private static Func<Similarity, int> func_9;

		[CompilerGenerated]
		private static Func<Similarity, CodeFile> func_10;

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
				IEnumerable<Similarity> arg_23_0 = Similarities;
				if (CodeFile.func_8 == null)
				{
					CodeFile.func_8 = new Func<Similarity, int>(CodeFile.GetMyHashIndexRangeLength);
				}
				List<int> list = arg_23_0.Select(CodeFile.func_8).ToList<int>();
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
				IEnumerable<Similarity> arg_23_0 = Similarities;
				if (CodeFile.func_9 == null)
				{
					CodeFile.func_9 = new Func<Similarity, int>(CodeFile.GetMyRangeLength);
				}
				List<int> list = arg_23_0.Select(CodeFile.func_9).ToList<int>();
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
				IEnumerable<Similarity> arg_23_0 = Similarities;
				if (CodeFile.func_10 == null)
				{
					CodeFile.func_10 = new Func<Similarity, CodeFile>(CodeFile.GetOtherFile);
				}
				return arg_23_0.Select(CodeFile.func_10).Distinct<CodeFile>().Count<CodeFile>();
			}
		}

		public int Blocks
		{
			get
			{
				return this.Similarities.Count;
			}
		}

		public List<Similarity> Similarities { get; set; }

		public Dictionary<int, string> HashToLine { get; set; }

		public CodeDir DirectParent { get; private set; }

		public CodeFile(string path, string code, CodeDir directParent)
		{
			Path = path;
			Code = code;
			this.method_0(this.Code);
			DirectParent = directParent;
		}

		private static string smethod_0(Match match)
		{
			return Regex.Replace(match.Groups["comment"].Value, "[^\\r\\n]+", "");
		}

		private void method_0(string string_6)
		{
			List<string> list = new List<string>();
			List<int> list2 = new List<int>();
			List<string> list3 = new List<string>();
			List<int> list4 = new List<int>();
			if (this.Extension == ".cs" || this.Extension == ".java" || this.Extension == ".js" || this.Extension == ".c" || this.Extension == ".cpp" || this.Extension == ".as3")
			{
				string_6 = Regex.Replace(string_6, "/\\*(?<comment>.*?)\\*/", new MatchEvaluator(CodeFile.smethod_0), RegexOptions.Singleline);
			}
			if (this.Extension == ".xaml" || this.Extension == ".html" || this.Extension == ".aspx" || this.Extension == ".xml")
			{
				string_6 = Regex.Replace(string_6, "\\<!--(?<comment>.*?)--\\>", new MatchEvaluator(CodeFile.smethod_0), RegexOptions.Singleline);
			}
			if (this.Extension == ".cshtml")
			{
				string_6 = Regex.Replace(string_6, "\\<!--(?<comment>.*?)--\\>", new MatchEvaluator(CodeFile.smethod_0), RegexOptions.Singleline);
				string_6 = Regex.Replace(string_6, "@\\*(?<comment>.*?)\\*@", new MatchEvaluator(CodeFile.smethod_0), RegexOptions.Singleline);
			}
			if (this.Extension == ".rb" || this.Extension == ".py")
			{
				string text = "\"\"\"";
				string_6 = Regex.Replace(string_6, text + "(?<comment>.*?)" + text, new MatchEvaluator(CodeFile.smethod_0), RegexOptions.Singleline);
			}
			if (this.Extension == ".py")
			{
				string text2 = "'''";
				string_6 = Regex.Replace(string_6, text2 + "(?<comment>.*?)" + text2, new MatchEvaluator(CodeFile.smethod_0), RegexOptions.Singleline);
			}
			string[] array = Regex.Split(string_6, "\\r?\\n");
			if (CodeFile.func_0 == null)
			{
				CodeFile.func_0 = new Func<string, string>(CodeFile.smethod_1);
			}
			Func<string, string> func = CodeFile.func_0;
			if (this.Extension == ".cs")
			{
				if (CodeFile.func_1 == null)
				{
					CodeFile.func_1 = new Func<string, string>(CodeFile.Replace_1);
				}
				func = CodeFile.func_1;
			}
			else if (this.Extension == ".java" || this.Extension == ".js" || this.Extension == ".as3")
			{
				if (CodeFile.func_2 == null)
				{
					CodeFile.func_2 = new Func<string, string>(CodeFile.Replace_2);
				}
				func = CodeFile.func_2;
			}
			else if (this.Extension == ".c" || this.Extension == ".cpp")
			{
				if (CodeFile.func_3 == null)
				{
					CodeFile.func_3 = new Func<string, string>(CodeFile.Replace_3);
				}
				func = CodeFile.func_3;
			}
			else if (this.Extension == ".vb")
			{
				if (CodeFile.func_4 == null)
				{
					CodeFile.func_4 = new Func<string, string>(CodeFile.Replace_4);
				}
				func = CodeFile.func_4;
			}
			else if (this.Extension == ".xml" || this.Extension == ".xaml" || this.Extension == ".html" || this.Extension == ".aspx" || this.Extension == ".cshtml")
			{
				if (CodeFile.func_5 == null)
				{
					CodeFile.func_5 = new Func<string, string>(CodeFile.smethod_6);
				}
				func = CodeFile.func_5;
			}
			else if (this.Extension == ".css")
			{
				if (CodeFile.func_6 == null)
				{
					CodeFile.func_6 = new Func<string, string>(CodeFile.smethod_7);
				}
				func = CodeFile.func_6;
			}
			else if (this.Extension == ".py" || this.Extension == ".rb")
			{
				if (CodeFile.func_7 == null)
				{
					CodeFile.func_7 = new Func<string, string>(CodeFile.smethod_8);
				}
				func = CodeFile.func_7;
			}
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text3 = array2[i];
				string text4 = text3.ToLowerInvariant();
				text4 = func(text4);
				text4 = CodeFile.regex_14.Replace(text4, "");
				if (text4 != "")
				{
					int hashCode = text4.GetHashCode();
					list2.Add(hashCode);
					list3.Add(text4);
					list4.Add(list.Count);
					if (!this.HashToLine.ContainsKey(hashCode))
					{
						this.HashToLine.Add(hashCode, text4);
					}
				}
				list.Add(CodeFile.regex_15.Replace(text3, "    "));
			}
			list4.Add(list.Count);
			this.Hashes = list2.ToArray();
			this.StrippedLines = list3.ToArray();
			this.HashIndexToLineIndex = list4.ToArray();
			this.Lines = list.ToArray();
		}

		internal void method_1()
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
		private static string smethod_1(string str)
		{
			return str;
		}

		[CompilerGenerated]
		private static string Replace_1(string str)
		{
			str = CodeFile.regex_2.Replace(str, "");
			str = CodeFile.regex_4.Replace(str, "");
			str = CodeFile.regex_13.Replace(str, "");
			return str;
		}

		[CompilerGenerated]
		private static string Replace_2(string str)
		{
			str = CodeFile.regex_2.Replace(str, "");
			str = CodeFile.regex_5.Replace(str, "");
			str = CodeFile.regex_13.Replace(str, "");
			return str;
		}

		[CompilerGenerated]
		private static string Replace_3(string str)
		{
			str = CodeFile.regex_2.Replace(str, "");
			str = CodeFile.regex_6.Replace(str, "");
			str = CodeFile.regex_13.Replace(str, "");
			return str;
		}

		[CompilerGenerated]
		private static string Replace_4(string str)
		{
			str = CodeFile.regex_3.Replace(str, "");
			str = CodeFile.regex_7.Replace(str, "");
			str = CodeFile.regex_8.Replace(str, "");
			str = CodeFile.regex_9.Replace(str, "");
			str = CodeFile.regex_10.Replace(str, "");
			return str;
		}

		[CompilerGenerated]
		private static string smethod_6(string str)
		{
			return str;
		}

		[CompilerGenerated]
		private static string smethod_7(string str)
		{
			return str;
		}

		[CompilerGenerated]
		private static string smethod_8(string str)
		{
			str = CodeFile.regex_12.Replace(str, "");
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
			CodeFile.regex_0 = new Regex("@?\"[^\"]*\"", RegexOptions.Compiled);
			CodeFile.regex_1 = new Regex("'.'", RegexOptions.Compiled);
			CodeFile.regex_2 = new Regex("//.*$", RegexOptions.Compiled);
			CodeFile.regex_3 = new Regex("'.*$", RegexOptions.Compiled);
			CodeFile.regex_4 = new Regex("\\s*using\\s*[\\w\\.]+\\s*;\\s*", RegexOptions.Compiled);
			CodeFile.regex_5 = new Regex("\\s*import(s?)\\s*[\\w\\.*]+\\s*;?\\s*", RegexOptions.Compiled);
			CodeFile.regex_6 = new Regex("\\#include[^\\r\\n]+\\r?\\n", RegexOptions.Compiled);
			CodeFile.regex_7 = new Regex("\\s*imports\\s*[\\w\\.]+\\s*", RegexOptions.Compiled);
			CodeFile.regex_8 = new Regex("^\\s*next\\s*$", RegexOptions.Compiled);
			CodeFile.regex_9 = new Regex("^\\s*(g|s)et\\s*$", RegexOptions.Compiled);
			CodeFile.regex_10 = new Regex("^\\s*end\\s*(if|property|while|try)\\s*$", RegexOptions.Compiled);
			CodeFile.regex_11 = new Regex("\\s*imports\\s*[\\w\\.]+\\s*", RegexOptions.Compiled);
			CodeFile.regex_12 = new Regex("\\#.*$", RegexOptions.Compiled);
			CodeFile.regex_13 = new Regex("{|}", RegexOptions.Compiled);
			CodeFile.regex_14 = new Regex("\\s", RegexOptions.Compiled);
			CodeFile.regex_15 = new Regex("\\t", RegexOptions.Compiled);
			CodeFile.int_0 = 4;
		}
	}
}