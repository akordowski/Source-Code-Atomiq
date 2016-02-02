using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace CopyPasteKiller
{
	public class Analysis
	{
		private Thread thread_0;

		private string string_0;

		private Action action_0;

		private Action action_1;

		private Action<string> action_2;

		private Action<int, int, string> action_3;

		private Action<int> action_4;

		private ObservableCollection<CodeDir> observableCollection_0 = new ObservableCollection<CodeDir>();

		private ObservableCollection<CodeFile> observableCollection_1 = new ObservableCollection<CodeFile>();

		private Options options_0;

		private bool bool_0 = false;

		private Exception exception_0;

		internal static int int_0;

		private static int int_1;

		[CompilerGenerated]
		private static Func<CodeDir, bool> func_0;

		[CompilerGenerated]
		private static Func<CodeFile, bool> func_1;

		[CompilerGenerated]
		private static Func<CodeFile, bool> func_2;

		[CompilerGenerated]
		private static Func<CodeFile, int> func_3;

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

		public Action Done
		{
			get
			{
				return this.action_0;
			}
			set
			{
				this.action_0 = value;
			}
		}

		public Action IncrementProgressValue
		{
			get
			{
				return this.action_1;
			}
			set
			{
				this.action_1 = value;
			}
		}

		public Action<string> AlertAction
		{
			get
			{
				return this.action_2;
			}
			set
			{
				this.action_2 = value;
			}
		}

		public Action<int, int, string> UpdateProgressAction
		{
			get
			{
				return this.action_3;
			}
			set
			{
				this.action_3 = value;
			}
		}

		public Action<int> UpdateProgressValue
		{
			get
			{
				return this.action_4;
			}
			set
			{
				this.action_4 = value;
			}
		}

		public ObservableCollection<CodeDir> RootDirectories
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

		public ObservableCollection<CodeFile> Files
		{
			get
			{
				return this.observableCollection_1;
			}
		}

		public Options Options
		{
			get
			{
				return this.options_0;
			}
			set
			{
				this.options_0 = value;
			}
		}

		public Exception CaughtException
		{
			get
			{
				return this.exception_0;
			}
			set
			{
				this.exception_0 = value;
			}
		}

		public Analysis(string path)
		{
			this.string_0 = path;
		}

		internal void method_0()
		{
			this.thread_0 = new Thread(new ThreadStart(this.method_10));
			this.thread_0.Start();
		}

		internal void method_1()
		{
			if (this.thread_0 != null && this.thread_0.IsAlive)
			{
				this.thread_0.Abort();
			}
		}

		private static int smethod_0(Similarity similarity_0, Similarity similarity_1)
		{
			return similarity_0.MyRange.Start.CompareTo(similarity_1.MyRange.Start);
		}

		private void method_2(CodeDir codeDir_0)
		{
			foreach (CodeDir codeDir in codeDir_0.Directories)
			{
				this.method_2(codeDir);
			}
			IEnumerable<CodeDir> arg_59_0 = codeDir_0.Directories;
			if (Analysis.func_0 == null)
			{
				Analysis.func_0 = new Func<CodeDir, bool>(Analysis.smethod_5);
			}
			CodeDir[] array = arg_59_0.Where(Analysis.func_0).ToArray<CodeDir>();
			for (int i = 0; i < array.Length; i++)
			{
				CodeDir codeDir = array[i];
				codeDir_0.Directories.Remove(codeDir);
				codeDir_0.ItemCollection.Remove(codeDir);
			}
			IEnumerable<CodeFile> arg_BB_0 = codeDir_0.Files;
			if (Analysis.func_1 == null)
			{
				Analysis.func_1 = new Func<CodeFile, bool>(Analysis.smethod_6);
			}
			CodeFile[] array2 = arg_BB_0.Where(Analysis.func_1).ToArray<CodeFile>();
			for (int i = 0; i < array2.Length; i++)
			{
				CodeFile item = array2[i];
				codeDir_0.Files.Remove(item);
				codeDir_0.ItemCollection.Remove(item);
			}
		}

		private void method_3()
		{
			ICollection<CodeFile> arg_23_0 = this.Files;
			if (Analysis.func_2 == null)
			{
				Analysis.func_2 = new Func<CodeFile, bool>(Analysis.smethod_7);
			}
			arg_23_0.RemoveWhere(Analysis.func_2);
		}

		private CodeDir method_4(DirectoryInfo directoryInfo_0, int int_2, string string_1)
		{
			CodeDir codeDir = new CodeDir
			{
				Path = directoryInfo_0.FullName,
				Name = directoryInfo_0.Name
			};
			string_1 = string_1 + "\\" + directoryInfo_0.Name;
			DirectoryInfo[] directories = directoryInfo_0.GetDirectories();
			for (int i = 0; i < directories.Length; i++)
			{
				DirectoryInfo directoryInfo_ = directories[i];
				CodeDir codeDir2 = this.method_4(directoryInfo_, int_2, string_1);
				if (codeDir2 != null)
				{
					codeDir2.DirectParent = codeDir;
					codeDir.Directories.Add(codeDir2);
					codeDir.ItemCollection.Add(codeDir2);
				}
			}
			List<Regex> excludeRegexes = this.options_0.GetExcludeRegexes();
			FileInfo[] files = directoryInfo_0.GetFiles(this.options_0.FileSearchPattern, SearchOption.TopDirectoryOnly);
			for (int i = 0; i < files.Length; i++)
			{
				FileInfo fileInfo = files[i];
				bool flag = false;
				for (int j = 0; j < excludeRegexes.Count; j++)
				{
					if (excludeRegexes[j].IsMatch(fileInfo.FullName))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					this.IncrementProgressValue();
					string code = null;
					using (StreamReader streamReader = new StreamReader(fileInfo.FullName))
					{
						code = streamReader.ReadToEnd();
					}
					CodeFile codeFile = new CodeFile(fileInfo.FullName, code, codeDir);
					codeFile.ShortPath = string_1;
					this.observableCollection_1.Add(codeFile);
					codeDir.Files.Add(codeFile);
					codeDir.ItemCollection.Add(codeFile);
				}
			}
			CodeDir result;
			if (codeDir.GetAllFilesCount() == 0)
			{
				result = null;
			}
			else
			{
				result = codeDir;
			}
			return result;
		}

		internal bool method_5()
		{
			return this.bool_0;
		}

		internal void method_6(bool value)
		{
			this.bool_0 = value;
		}

		public static string FindLcs(string str1, string str2)
		{
			string result;
			if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
			{
				result = "";
			}
			else
			{
				int[,] array = new int[str1.Length, str2.Length];
				int num = 0;
				int num2 = 0;
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < str1.Length; i++)
				{
					for (int j = 0; j < str2.Length; j++)
					{
						if (str1[i] != str2[j])
						{
							array[i, j] = 0;
						}
						else
						{
							if (i == 0 || j == 0)
							{
								array[i, j] = 1;
							}
							else
							{
								array[i, j] = 1 + array[i - 1, j - 1];
							}
							if (array[i, j] > num)
							{
								num = array[i, j];
								int num3 = i - array[i, j] + 1;
								if (num2 == num3)
								{
									stringBuilder.Append(str1[i]);
								}
								else
								{
									num2 = num3;
									stringBuilder.Remove(0, stringBuilder.Length);
									stringBuilder.Append(str1.Substring(num2, i + 1 - num2));
								}
							}
						}
					}
				}
				Builder builder = new Builder("\r\n");
				Builder builder2 = new Builder(".");
				for (int i = 0; i < str1.Length; i++)
				{
					builder2.Clear();
					for (int j = 0; j < str2.Length; j++)
					{
						builder2.Append(array[i, j].ToString().PadLeft(2, ' '));
					}
					builder.Append(builder2);
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		internal static AllSequences smethod_1(CodeFile codeFile_0, CodeFile codeFile_1, int int_2)
		{
			AllSequences result;
			if (codeFile_0 == codeFile_1)
			{
				result = Analysis.smethod_2(codeFile_0, int_2);
			}
			else
			{
				int[] hashes = codeFile_0.Hashes;
				int[] hashes2 = codeFile_1.Hashes;
				AllSequences allSequences = new AllSequences();
				if (hashes.Intersect(hashes2).Count<int>() == 0)
				{
					Analysis.int_0++;
					result = allSequences;
				}
				else if (hashes.Length == 0 || hashes2.Length == 0)
				{
					result = allSequences;
				}
				else
				{
					int[] array = new int[hashes2.Length];
					int[] array2 = new int[hashes2.Length];
					for (int i = 0; i < hashes.Length; i++)
					{
						for (int j = 0; j < hashes2.Length; j++)
						{
							if (hashes[i] != hashes2[j])
							{
								array2[j] = 0;
							}
							else
							{
								if (i == 0 || j == 0)
								{
									array2[j] = 1;
								}
								else
								{
									array2[j] = 1 + array[j - 1];
								}
								if (array2[j] > int_2)
								{
									allSequences.AddCoordToAppropriateSequence(new Coord
									{
										I = i,
										J = j,
										Size = array2[j]
									});
								}
							}
						}
						int[] array3 = array;
						array = array2;
						array2 = array3;
					}
					result = allSequences;
				}
			}
			return result;
		}

		private static AllSequences smethod_2(CodeFile codeFile_0, int int_2)
		{
			int[] hashes = codeFile_0.Hashes;
			int[] hashes2 = codeFile_0.Hashes;
			AllSequences allSequences = new AllSequences();
			AllSequences result;
			if (hashes.Length == 0)
			{
				result = allSequences;
			}
			else
			{
				int[] array = new int[hashes2.Length];
				int[] array2 = new int[hashes2.Length];
				for (int i = 0; i < hashes.Length; i++)
				{
					for (int j = i; j < hashes2.Length; j++)
					{
						if (i != j)
						{
							if (hashes[i] != hashes2[j])
							{
								array2[j] = 0;
							}
							else
							{
								if (i == 0 || j == 0)
								{
									array2[j] = 1;
								}
								else
								{
									array2[j] = 1 + array[j - 1];
								}
								if (array2[j] > int_2)
								{
									allSequences.AddCoordToAppropriateSequence(new Coord
									{
										I = i,
										J = j,
										Size = array2[j]
									});
								}
							}
						}
					}
					int[] array3 = array;
					array = array2;
					array2 = array3;
				}
				result = allSequences;
			}
			return result;
		}

		private static void smethod_3(CodeFile codeFile_0, CodeFile codeFile_1, int int_2, List<int> sequenceBuilder, int int_3, int int_4, int[,] int_5)
		{
			if (sequenceBuilder.Count > int_2)
			{
				int arg_17_0 = sequenceBuilder.Count;
				int start = int_3 - int_5[int_3, int_4] + 1;
				int start2 = int_4 - int_5[int_3, int_4] + 1;
				Similarity similarity = new Similarity();
				similarity.MyFile = codeFile_0;
				similarity.OtherFile = codeFile_1;
				similarity.MyHashIndexRange = new HashIndexRange
				{
					Start = start,
					End = int_3
				};
				similarity.OtherHashIndexRange = new HashIndexRange
				{
					Start = start2,
					End = int_4
				};
				similarity.SetLineRanges();
				codeFile_0.Similarities.Add(similarity);
				if (codeFile_0 != codeFile_1)
				{
					Similarity similarity2 = new Similarity();
					similarity2.MyFile = codeFile_1;
					similarity2.OtherFile = codeFile_0;
					similarity2.OtherHashIndexRange = new HashIndexRange
					{
						Start = start,
						End = int_3
					};
					similarity2.MyHashIndexRange = new HashIndexRange
					{
						Start = start2,
						End = int_4
					};
					similarity2.SetLineRanges();
					codeFile_1.Similarities.Add(similarity2);
				}
			}
		}

		private static void smethod_4(CodeFile codeFile_0, CodeFile codeFile_1, Sequence sequence_0)
		{
			Similarity similarity = new Similarity();
			similarity.MyFile = codeFile_0;
			similarity.OtherFile = codeFile_1;
			similarity.MyHashIndexRange = new HashIndexRange
			{
				Start = sequence_0.FirstCoord.I,
				End = sequence_0.LastCoord.I + 1
			};
			similarity.OtherHashIndexRange = new HashIndexRange
			{
				Start = sequence_0.FirstCoord.J,
				End = sequence_0.LastCoord.J + 1
			};
			similarity.SetLineRanges();
			similarity.UniqueId = Analysis.int_1;
			codeFile_0.Similarities.Add(similarity);
			Similarity similarity2 = new Similarity();
			similarity2.MyFile = codeFile_1;
			similarity2.OtherFile = codeFile_0;
			similarity2.OtherHashIndexRange = new HashIndexRange
			{
				Start = sequence_0.FirstCoord.I,
				End = sequence_0.LastCoord.I + 1
			};
			similarity2.MyHashIndexRange = new HashIndexRange
			{
				Start = sequence_0.FirstCoord.J,
				End = sequence_0.LastCoord.J + 1
			};
			similarity2.SetLineRanges();
			similarity.UniqueId = Analysis.int_1;
			codeFile_1.Similarities.Add(similarity2);
			similarity.CorrespondingSimilarity = similarity2;
			similarity2.CorrespondingSimilarity = similarity;
			Analysis.int_1++;
		}

		internal void method_7()
		{
			if (File.Exists("ignoreCode.txt"))
			{
				string string_;
				using (StreamReader streamReader = new StreamReader("ignoreCode.txt"))
				{
					string_ = streamReader.ReadToEnd();
				}
				List<CodeFile> list = this.method_8(string_);
				foreach (CodeFile current in list)
				{
					foreach (CodeFile current2 in this.observableCollection_1)
					{
						AllSequences allSequences_ = Analysis.smethod_1(current, current2, current.Hashes.Length - 1);
						this.method_9(current2, allSequences_);
					}
				}
				this.method_2(this.observableCollection_0.First<CodeDir>());
				this.method_3();
			}
		}

		private List<CodeFile> method_8(string string_1)
		{
			string[] array = Regex.Split(string_1, "~~CPK\\sCode\\sChunk\\sDelimiter~~", RegexOptions.IgnoreCase);
			string arg = this.options_0.FileSearchPattern.Substring(1, this.options_0.FileSearchPattern.Length - 1);
			List<CodeFile> list = new List<CodeFile>();
			for (int i = 0; i < array.Length; i++)
			{
				if (!(array[i].Trim() == ""))
				{
					CodeFile item = new CodeFile("ignoreChunk" + i + arg, array[i], null);
					list.Add(item);
				}
			}
			IEnumerable<CodeFile> arg_A0_0 = list;
			if (Analysis.func_3 == null)
			{
				Analysis.func_3 = new Func<CodeFile, int>(Analysis.smethod_8);
			}
			return arg_A0_0.OrderByDescending(Analysis.func_3).ToList<CodeFile>();
		}

		private void method_9(CodeFile codeFile_0, AllSequences allSequences_0)
		{
			if (allSequences_0.Sequences.Count != 0)
			{
				int i = 0;
				IL_A1:
				while (i < codeFile_0.Similarities.Count)
				{
					Similarity similarity = codeFile_0.Similarities[i];
					bool flag = false;
					int j = 0;
					while (j < allSequences_0.Sequences.Count)
					{
						Sequence sequence = allSequences_0.Sequences[j];
						if (sequence.FirstCoord.J < similarity.MyHashIndexRange.Start || sequence.LastCoord.J > similarity.MyHashIndexRange.End)
						{
							j++;
						}
						else
						{
							codeFile_0.Similarities.Remove(similarity);
							flag = true;
							IL_9A:
							if (!flag)
							{
								i++;
								goto IL_A1;
							}
							goto IL_A1;
						}
					}
					goto IL_9A;
				}
			}
		}

		[CompilerGenerated]
		private void method_10()
		{
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(this.string_0);
				int minSimilarityLineLength = this.options_0.MinSimilarityLineLength;
				FileInfo[] files = directoryInfo.GetFiles(this.options_0.FileSearchPattern, SearchOption.AllDirectories);
				int num = 0;
				List<Regex> excludeRegexes = this.options_0.GetExcludeRegexes();
				FileInfo[] array = files;
				int i = 0;
				IL_98:
				while (i < array.Length)
				{
					FileInfo fileInfo = array[i];
					bool flag = false;
					for (int j = 0; j < excludeRegexes.Count; j++)
					{
						if (excludeRegexes[j].IsMatch(fileInfo.FullName))
						{
							flag = true;
							IL_87:
							if (!flag)
							{
								num++;
							}
							i++;
							goto IL_98;
						}
					}
					goto IL_87;
				}
				if (num == 0)
				{
					this.AlertAction("No " + this.options_0.FileSearchPattern + " Files Found");
					this.Done();
				}
				else
				{
					this.UpdateProgressAction(0, num, "Loading Files...");
					CodeDir codeDir = this.method_4(directoryInfo, this.options_0.MinSimilarityLineLength, "");
					this.observableCollection_0.Add(codeDir);
					ObservableCollection<CodeFile> observableCollection = this.observableCollection_1;
					this.UpdateProgressAction(0, observableCollection.Count * observableCollection.Count / 2, "Comparing Files...");
					int num2 = 0;
					StringBuilder stringBuilder = new StringBuilder();
					for (int j = 0; j < observableCollection.Count; j++)
					{
						CodeFile codeFile = observableCollection[j];
						for (int k = j; k < observableCollection.Count; k++)
						{
							num2++;
							if (num2 % 500 == 0)
							{
								this.UpdateProgressValue(num2);
							}
							try
							{
								CodeFile codeFile_ = observableCollection[k];
								AllSequences allSequences = Analysis.smethod_1(codeFile, codeFile_, minSimilarityLineLength);
								foreach (Sequence current in allSequences.Sequences)
								{
									Analysis.smethod_4(codeFile, codeFile_, current);
								}
								goto IL_24D;
							}
							catch (Exception ex)
							{
								stringBuilder.AppendLine(codeFile.Name + " - " + observableCollection[k].Name);
								goto IL_24D;
							}
							break;
							IL_24D:;
						}
						codeFile.method_1();
						codeFile.Similarities.Sort(new Comparison<Similarity>(Analysis.smethod_0));
					}
					this.method_2(codeDir);
					this.method_3();
					this.method_7();
					if (stringBuilder.Length > 0)
					{
						this.AlertAction("Atomiq experienced an error with the following file combinations: " + stringBuilder.ToString());
					}
					this.bool_0 = true;
					this.Done();
				}
			}
			catch (Exception ex)
			{
				if (!(ex is ThreadAbortException))
				{
					this.CaughtException = ex;
				}
				this.Done();
			}
		}

		[CompilerGenerated]
		private static bool smethod_5(CodeDir codeDir_0)
		{
			return codeDir_0.GetAllFilesCount() == 0;
		}

		[CompilerGenerated]
		private static bool smethod_6(CodeFile codeFile_0)
		{
			return codeFile_0.Similarities.Count == 0;
		}

		[CompilerGenerated]
		private static bool smethod_7(CodeFile codeFile_0)
		{
			return codeFile_0.Similarities.Count == 0;
		}

		[CompilerGenerated]
		private static int smethod_8(CodeFile codeFile_0)
		{
			return codeFile_0.Hashes.Length;
		}

		static Analysis()
		{
			Analysis.int_0 = 0;
			Analysis.int_1 = 0;
		}
	}
}
