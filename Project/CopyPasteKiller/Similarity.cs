using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace CopyPasteKiller
{
	[DebuggerDisplay("Similarity: {MyFile.Name} - {OtherFile.Name}")]
	public class Similarity
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex_0;

		public CodeFile MyFile { get; set; }

		public CodeFile OtherFile { get; set; }

		public bool SameFile
		{
			get
			{
				return MyFile == OtherFile;
			}
		}

		public LineRange MyRange { get; private set; }

		public LineRange OtherRange { get; private set; }

		public HashIndexRange MyHashIndexRange { get; set; }

		public HashIndexRange OtherHashIndexRange { get; set; }

		public string MyText
		{
			get
			{
				return GetText(MyFile, MyRange);
			}
		}

		public string OtherText
		{
			get
			{
				return GetText(OtherFile, OtherRange);
			}
		}

		public string MyTextNoLines
		{
			get
			{
				return GetText(MyFile, MyRange, false);
			}
		}

		public string OtherTextNoLines
		{
			get
			{
				return GetText(OtherFile, OtherRange, false);
			}
		}

		public string MyStrictText
		{
			get
			{
				return this.method_1(MyFile, MyHashIndexRange);
			}
		}

		public string OtherStrictText
		{
			get
			{
				return this.method_1(OtherFile, OtherHashIndexRange);
			}
		}

		public int UniqueId { get; set; }

		public Similarity CorrespondingSimilarity { get; set; }

		public void SetLineRanges()
		{
			MyRange = this.method_0(MyFile, MyHashIndexRange, MyRange);
			OtherRange = this.method_0(OtherFile, OtherHashIndexRange, OtherRange);
		}

		private LineRange method_0(CodeFile codeFile, HashIndexRange hashIndexRange, LineRange lineRange)
		{
			lineRange.Start = codeFile.HashIndexToLineIndex[hashIndexRange.Start];
			if (codeFile.HashIndexToLineIndex.Length <= hashIndexRange.End)
			{
				lineRange.End = codeFile.HashIndexToLineIndex[codeFile.HashIndexToLineIndex.Length - 1];
			}
			else
			{
				lineRange.End = codeFile.HashIndexToLineIndex[hashIndexRange.End];
			}
			return lineRange;
		}

		public string GetText(CodeFile file, LineRange range)
		{
			return this.GetText(file, range, true);
		}

		public string GetText(CodeFile file, LineRange range, bool includeLineNumbers)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 1000;
			for (int i = range.Start; i < range.End; i++)
			{
				if (!Similarity.regex_0.IsMatch(file.Lines[i]))
				{
					string text = file.Lines[i];
					int num2 = 0;
					while (num2 < text.Length && text[num2] == ' ')
					{
						num2++;
					}
					if (num > num2)
					{
						num = num2;
					}
				}
			}
			for (int i = range.Start; i < range.End; i++)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.AppendLine();
				}
				if (includeLineNumbers)
				{
					if (file.Lines[i].Length <= num)
					{
						stringBuilder.Append(i.ToString().PadLeft(4, ' '));
					}
					else
					{
						stringBuilder.Append(i.ToString().PadLeft(4, ' ') + "\t" + file.Lines[i].Substring(num));
					}
				}
				else if (file.Lines[i].Length <= num)
				{
					stringBuilder.Append("");
				}
				else
				{
					stringBuilder.Append("\t" + file.Lines[i].Substring(num));
				}
			}
			return stringBuilder.ToString();
		}

		private string method_1(CodeFile codeFile, HashIndexRange hashIndexRange)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = hashIndexRange.Start; i < hashIndexRange.End; i++)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.AppendLine();
				}
				if (i >= codeFile.Hashes.Length)
				{
					break;
				}
				stringBuilder.Append(codeFile.Hashes[i].ToString().PadLeft(13, ' ') + "  " + codeFile.HashToLine[codeFile.Hashes[i]]);
			}
			return stringBuilder.ToString();
		}

		public string GetSimilarText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = MyRange.Start; i < MyRange.End; i++)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.AppendLine();
				}
				stringBuilder.Append(MyFile.Lines[i]);
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("********************");
			stringBuilder.AppendLine();
			for (int i = OtherRange.Start; i < OtherRange.End; i++)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.AppendLine();
				}
				stringBuilder.Append(OtherFile.Lines[i]);
			}
			return stringBuilder.ToString();
		}

		static Similarity()
		{
			Similarity.regex_0 = new Regex("^\\s*$", RegexOptions.Compiled);
		}
	}
}