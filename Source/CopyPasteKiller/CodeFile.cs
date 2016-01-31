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

		private string string_0;

		private string string_1;

		private string string_2;

		private string string_3;

		private int[] int_1;

		private string[] string_4;

		private int[] int_2;

		private string[] string_5;

		private int int_3;

		private List<Similarity> list_0 = new List<Similarity>();

		private Dictionary<int, string> dictionary_0 = new Dictionary<int, string>();

		private CodeDir codeDir_0;

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
				return this.string_1;
			}
			set
			{
				this.string_1 = value;
			}
		}

		public string Name
		{
			get
			{
				return System.IO.Path.GetFileName(this.string_0);
			}
		}

		public string Extension
		{
			get
			{
				return System.IO.Path.GetExtension(this.string_0);
			}
		}

		public string Code
		{
			get
			{
				return this.string_2;
			}
			set
			{
				this.string_2 = value;
			}
		}

		public string StrippedCode
		{
			get
			{
				return this.string_3;
			}
			set
			{
				this.string_3 = value;
			}
		}

		public int[] Hashes
		{
			get
			{
				return this.int_1;
			}
			set
			{
				this.int_1 = value;
			}
		}

		public string[] StrippedLines
		{
			get
			{
				return this.string_4;
			}
			set
			{
				this.string_4 = value;
			}
		}

		public int[] HashIndexToLineIndex
		{
			get
			{
				return this.int_2;
			}
			set
			{
				this.int_2 = value;
			}
		}

		public string[] Lines
		{
			get
			{
				return this.string_5;
			}
			set
			{
				this.string_5 = value;
			}
		}

		public int ProcessedLines
		{
			get
			{
				IEnumerable<Similarity> arg_23_0 = this.list_0;
				if (CodeFile.func_8 == null)
				{
					CodeFile.func_8 = new Func<Similarity, int>(CodeFile.smethod_9);
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
				IEnumerable<Similarity> arg_23_0 = this.list_0;
				if (CodeFile.func_9 == null)
				{
					CodeFile.func_9 = new Func<Similarity, int>(CodeFile.smethod_10);
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
				IEnumerable<Similarity> arg_23_0 = this.list_0;
				if (CodeFile.func_10 == null)
				{
					CodeFile.func_10 = new Func<Similarity, CodeFile>(CodeFile.smethod_11);
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

		public List<Similarity> Similarities
		{
			get
			{
				return this.list_0;
			}
			set
			{
				this.list_0 = value;
			}
		}

		public Dictionary<int, string> HashToLine
		{
			get
			{
				return this.dictionary_0;
			}
			set
			{
				this.dictionary_0 = value;
			}
		}

		public CodeDir DirectParent
		{
			get
			{
				return this.codeDir_0;
			}
		}

		public CodeFile(string path, string code, CodeDir directParent)
		{
			this.string_0 = path;
			this.string_2 = code;
			this.method_0(this.Code);
			this.codeDir_0 = directParent;
		}

		private static string smethod_0(Match match_0)
		{
			return Regex.Replace(match_0.Groups["comment"].Value, "[^\\r\\n]+", "");
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
					CodeFile.func_1 = new Func<string, string>(CodeFile.smethod_2);
				}
				func = CodeFile.func_1;
			}
			else if (this.Extension == ".java" || this.Extension == ".js" || this.Extension == ".as3")
			{
				if (CodeFile.func_2 == null)
				{
					CodeFile.func_2 = new Func<string, string>(CodeFile.smethod_3);
				}
				func = CodeFile.func_2;
			}
			else if (this.Extension == ".c" || this.Extension == ".cpp")
			{
				if (CodeFile.func_3 == null)
				{
					CodeFile.func_3 = new Func<string, string>(CodeFile.smethod_4);
				}
				func = CodeFile.func_3;
			}
			else if (this.Extension == ".vb")
			{
				if (CodeFile.func_4 == null)
				{
					CodeFile.func_4 = new Func<string, string>(CodeFile.smethod_5);
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
			if (this.list_0.Count == 0)
			{
				this.string_2 = null;
				this.string_3 = null;
				this.int_1 = null;
				this.string_4 = null;
				this.int_2 = null;
				this.string_5 = null;
			}
			this.dictionary_0 = null;
		}

		[CompilerGenerated]
		private static string smethod_1(string string_6)
		{
			return string_6;
		}

		[CompilerGenerated]
		private static string smethod_2(string string_6)
		{
			string_6 = CodeFile.regex_2.Replace(string_6, "");
			string_6 = CodeFile.regex_4.Replace(string_6, "");
			string_6 = CodeFile.regex_13.Replace(string_6, "");
			return string_6;
		}

		[CompilerGenerated]
		private static string smethod_3(string string_6)
		{
			string_6 = CodeFile.regex_2.Replace(string_6, "");
			string_6 = CodeFile.regex_5.Replace(string_6, "");
			string_6 = CodeFile.regex_13.Replace(string_6, "");
			return string_6;
		}

		[CompilerGenerated]
		private static string smethod_4(string string_6)
		{
			string_6 = CodeFile.regex_2.Replace(string_6, "");
			string_6 = CodeFile.regex_6.Replace(string_6, "");
			string_6 = CodeFile.regex_13.Replace(string_6, "");
			return string_6;
		}

		[CompilerGenerated]
		private static string smethod_5(string string_6)
		{
			string_6 = CodeFile.regex_3.Replace(string_6, "");
			string_6 = CodeFile.regex_7.Replace(string_6, "");
			string_6 = CodeFile.regex_8.Replace(string_6, "");
			string_6 = CodeFile.regex_9.Replace(string_6, "");
			string_6 = CodeFile.regex_10.Replace(string_6, "");
			return string_6;
		}

		[CompilerGenerated]
		private static string smethod_6(string string_6)
		{
			return string_6;
		}

		[CompilerGenerated]
		private static string smethod_7(string string_6)
		{
			return string_6;
		}

		[CompilerGenerated]
		private static string smethod_8(string string_6)
		{
			string_6 = CodeFile.regex_12.Replace(string_6, "");
			return string_6;
		}

		[CompilerGenerated]
		private static int smethod_9(Similarity similarity_0)
		{
			return similarity_0.MyHashIndexRange.Length;
		}

		[CompilerGenerated]
		private static int smethod_10(Similarity similarity_0)
		{
			return similarity_0.MyRange.Length;
		}

		[CompilerGenerated]
		private static CodeFile smethod_11(Similarity similarity_0)
		{
			return similarity_0.OtherFile;
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
