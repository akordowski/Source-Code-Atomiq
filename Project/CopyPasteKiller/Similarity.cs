using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace CopyPasteKiller
{
	[DebuggerDisplay("Similarity: {MyFile.Name} - {OtherFile.Name}")]
	public class Similarity
	{
		private CodeFile codeFile_0;

		private CodeFile codeFile_1;

		private LineRange lineRange_0 = new LineRange();

		private LineRange lineRange_1 = new LineRange();

		private HashIndexRange hashIndexRange_0;

		private HashIndexRange hashIndexRange_1;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Regex regex_0;

		private int int_0;

		private Similarity similarity_0;

		public CodeFile MyFile
		{
			get
			{
				return this.codeFile_0;
			}
			set
			{
				this.codeFile_0 = value;
			}
		}

		public CodeFile OtherFile
		{
			get
			{
				return this.codeFile_1;
			}
			set
			{
				this.codeFile_1 = value;
			}
		}

		public bool SameFile
		{
			get
			{
				return this.codeFile_0 == this.codeFile_1;
			}
		}

		public LineRange MyRange
		{
			get
			{
				return this.lineRange_0;
			}
		}

		public LineRange OtherRange
		{
			get
			{
				return this.lineRange_1;
			}
		}

		public HashIndexRange MyHashIndexRange
		{
			get
			{
				return this.hashIndexRange_0;
			}
			set
			{
				this.hashIndexRange_0 = value;
			}
		}

		public HashIndexRange OtherHashIndexRange
		{
			get
			{
				return this.hashIndexRange_1;
			}
			set
			{
				this.hashIndexRange_1 = value;
			}
		}

		public string MyText
		{
			get
			{
				return this.GetText(this.codeFile_0, this.lineRange_0);
			}
		}

		public string OtherText
		{
			get
			{
				return this.GetText(this.codeFile_1, this.lineRange_1);
			}
		}

		public string MyTextNoLines
		{
			get
			{
				return this.GetText(this.codeFile_0, this.lineRange_0, false);
			}
		}

		public string OtherTextNoLines
		{
			get
			{
				return this.GetText(this.codeFile_1, this.lineRange_1, false);
			}
		}

		public string MyStrictText
		{
			get
			{
				return this.method_1(this.codeFile_0, this.hashIndexRange_0);
			}
		}

		public string OtherStrictText
		{
			get
			{
				return this.method_1(this.codeFile_1, this.hashIndexRange_1);
			}
		}

		public int UniqueId
		{
			get
			{
				return this.int_0;
			}
			set
			{
				this.int_0 = value;
			}
		}

		public Similarity CorrespondingSimilarity
		{
			get
			{
				return this.similarity_0;
			}
			set
			{
				this.similarity_0 = value;
			}
		}

		public void SetLineRanges()
		{
			this.lineRange_0 = this.method_0(this.MyFile, this.hashIndexRange_0, this.lineRange_0);
			this.lineRange_1 = this.method_0(this.OtherFile, this.hashIndexRange_1, this.lineRange_1);
		}

		private LineRange method_0(CodeFile codeFile_2, HashIndexRange hashIndexRange_2, LineRange lineRange_2)
		{
			lineRange_2.Start = codeFile_2.HashIndexToLineIndex[hashIndexRange_2.Start];
			if (codeFile_2.HashIndexToLineIndex.Length <= hashIndexRange_2.End)
			{
				lineRange_2.End = codeFile_2.HashIndexToLineIndex[codeFile_2.HashIndexToLineIndex.Length - 1];
			}
			else
			{
				lineRange_2.End = codeFile_2.HashIndexToLineIndex[hashIndexRange_2.End];
			}
			return lineRange_2;
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

		private string method_1(CodeFile codeFile_2, HashIndexRange hashIndexRange_2)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = hashIndexRange_2.Start; i < hashIndexRange_2.End; i++)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.AppendLine();
				}
				if (i >= codeFile_2.Hashes.Length)
				{
					break;
				}
				stringBuilder.Append(codeFile_2.Hashes[i].ToString().PadLeft(13, ' ') + "  " + codeFile_2.HashToLine[codeFile_2.Hashes[i]]);
			}
			return stringBuilder.ToString();
		}

		public string GetSimilarText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = this.lineRange_0.Start; i < this.lineRange_0.End; i++)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.AppendLine();
				}
				stringBuilder.Append(this.codeFile_0.Lines[i]);
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("********************");
			stringBuilder.AppendLine();
			for (int i = this.lineRange_1.Start; i < this.lineRange_1.End; i++)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.AppendLine();
				}
				stringBuilder.Append(this.codeFile_1.Lines[i]);
			}
			return stringBuilder.ToString();
		}

		static Similarity()
		{
			Similarity.regex_0 = new Regex("^\\s*$", RegexOptions.Compiled);
		}
	}
}
