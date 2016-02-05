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
		private Thread _thread;

		private bool bool0 = false;

		internal static int int0;

		private static int int1;

		[CompilerGenerated]
		private static Func<CodeDir, bool> func0;

		[CompilerGenerated]
		private static Func<CodeFile, bool> func1;

		[CompilerGenerated]
		private static Func<CodeFile, bool> func2;

		[CompilerGenerated]
		private static Func<CodeFile, int> func3;

		public string Path { get; set; }

		public Action Done { get; set; }

		public Action IncrementProgressValue { get; set; }

		public Action<string> AlertAction { get; set; }

		public Action<int, int, string> UpdateProgressAction { get; set; }

		public Action<int> UpdateProgressValue { get; set; }

		public ObservableCollection<CodeDir> RootDirectories { get; set; }

		public ObservableCollection<CodeFile> Files { get; private set; }

		public Options Options { get; set; }

		public Exception CaughtException { get; set; }

		public Analysis(string path)
		{
			Path = path;
		}

		internal void StartThread()
		{
			_thread = new Thread(new ThreadStart(method10));
			_thread.Start();
		}

		internal void AbortThread()
		{
			if (_thread != null && _thread.IsAlive)
			{
				_thread.Abort();
			}
		}

		private static int CompareSimilarities(Similarity similarity1, Similarity similarity2)
		{
			return similarity1.MyRange.Start.CompareTo(similarity2.MyRange.Start);
		}

		private void method2(CodeDir codeDir)
		{
			foreach (CodeDir dir in codeDir.Directories)
			{
				method2(dir);
			}

			IEnumerable<CodeDir> directories = codeDir.Directories;

			if (Analysis.func0 == null)
			{
				Analysis.func0 = new Func<CodeDir, bool>(Analysis.smethod5);
			}

			CodeDir[] array = directories.Where(Analysis.func0).ToArray<CodeDir>();

			for (int i = 0; i < array.Length; i++)
			{
				CodeDir dir = array[i];
				codeDir.Directories.Remove(dir);
				codeDir.ItemCollection.Remove(dir);
			}

			IEnumerable<CodeFile> files = codeDir.Files;

			if (Analysis.func1 == null)
			{
				Analysis.func1 = new Func<CodeFile, bool>(Analysis.smethod6);
			}

			CodeFile[] array2 = files.Where(Analysis.func1).ToArray<CodeFile>();

			for (int i = 0; i < array2.Length; i++)
			{
				CodeFile file = array2[i];
				codeDir.Files.Remove(file);
				codeDir.ItemCollection.Remove(file);
			}
		}

		private void method3()
		{
			ICollection<CodeFile> files = Files;

			if (Analysis.func2 == null)
			{
				Analysis.func2 = new Func<CodeFile, bool>(Analysis.smethod7);
			}

			files.RemoveWhere(Analysis.func2);
		}

		private CodeDir method4(DirectoryInfo directoryInfo, int int2, string string1)
		{
			CodeDir codeDir = new CodeDir
			{
				Path = directoryInfo.FullName,
				Name = directoryInfo.Name
			};

			string1 = string1 + "\\" + directoryInfo.Name;
			DirectoryInfo[] directories = directoryInfo.GetDirectories();

			for (int i = 0; i < directories.Length; i++)
			{
				DirectoryInfo directoryInfo_ = directories[i];
				CodeDir codeDir2 = method4(directoryInfo_, int2, string1);

				if (codeDir2 != null)
				{
					codeDir2.DirectParent = codeDir;
					codeDir.Directories.Add(codeDir2);
					codeDir.ItemCollection.Add(codeDir2);
				}
			}

			List<Regex> excludeRegexes = Options.GetExcludeRegexes();
			FileInfo[] files = directoryInfo.GetFiles(Options.FileSearchPattern, SearchOption.TopDirectoryOnly);

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
					IncrementProgressValue();
					string code = null;

					using (StreamReader streamReader = new StreamReader(fileInfo.FullName))
					{
						code = streamReader.ReadToEnd();
					}

					CodeFile codeFile = new CodeFile(fileInfo.FullName, code, codeDir);
					codeFile.ShortPath = string1;
					Files.Add(codeFile);
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

		internal bool method5()
		{
			return bool0;
		}

		internal void method6(bool value)
		{
			bool0 = value;
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

		internal static AllSequences smethod1(CodeFile codeFile1, CodeFile codeFile2, int int2)
		{
			AllSequences result;

			if (codeFile1 == codeFile2)
			{
				result = Analysis.smethod2(codeFile1, int2);
			}
			else
			{
				int[] hashes = codeFile1.Hashes;
				int[] hashes2 = codeFile2.Hashes;
				AllSequences allSequences = new AllSequences();

				if (hashes.Intersect(hashes2).Count<int>() == 0)
				{
					Analysis.int0++;
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

								if (array2[j] > int2)
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

		private static AllSequences smethod2(CodeFile codeFile, int int2)
		{
			int[] hashes = codeFile.Hashes;
			int[] hashes2 = codeFile.Hashes;
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

								if (array2[j] > int2)
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

		private static void smethod3(CodeFile myFile, CodeFile otherFile, int int2, List<int> sequenceBuilder, int myEnd, int otherEnd, int[,] int5)
		{
			if (sequenceBuilder.Count > int2)
			{
				int sequencesCount = sequenceBuilder.Count;
				int start = myEnd - int5[myEnd, otherEnd] + 1;
				int start2 = otherEnd - int5[myEnd, otherEnd] + 1;
				Similarity similarity = new Similarity();
				similarity.MyFile = myFile;
				similarity.OtherFile = otherFile;
				similarity.MyHashIndexRange = new HashIndexRange
				{
					Start = start,
					End = myEnd
				};
				similarity.OtherHashIndexRange = new HashIndexRange
				{
					Start = start2,
					End = otherEnd
				};
				similarity.SetLineRanges();

				myFile.Similarities.Add(similarity);

				if (myFile != otherFile)
				{
					Similarity similarity2 = new Similarity();
					similarity2.MyFile = otherFile;
					similarity2.OtherFile = myFile;
					similarity2.OtherHashIndexRange = new HashIndexRange
					{
						Start = start,
						End = myEnd
					};
					similarity2.MyHashIndexRange = new HashIndexRange
					{
						Start = start2,
						End = otherEnd
					};
					similarity2.SetLineRanges();
					otherFile.Similarities.Add(similarity2);
				}
			}
		}

		private static void smethod4(CodeFile myFile, CodeFile otherFile, Sequence sequence)
		{
			Similarity similarity = new Similarity();
			similarity.MyFile = myFile;
			similarity.OtherFile = otherFile;
			similarity.MyHashIndexRange = new HashIndexRange
			{
				Start = sequence.FirstCoord.I,
				End = sequence.LastCoord.I + 1
			};
			similarity.OtherHashIndexRange = new HashIndexRange
			{
				Start = sequence.FirstCoord.J,
				End = sequence.LastCoord.J + 1
			};
			similarity.SetLineRanges();
			similarity.UniqueId = Analysis.int1;
			myFile.Similarities.Add(similarity);
			Similarity similarity2 = new Similarity();
			similarity2.MyFile = otherFile;
			similarity2.OtherFile = myFile;
			similarity2.OtherHashIndexRange = new HashIndexRange
			{
				Start = sequence.FirstCoord.I,
				End = sequence.LastCoord.I + 1
			};
			similarity2.MyHashIndexRange = new HashIndexRange
			{
				Start = sequence.FirstCoord.J,
				End = sequence.LastCoord.J + 1
			};
			similarity2.SetLineRanges();
			similarity.UniqueId = Analysis.int1;
			otherFile.Similarities.Add(similarity2);
			similarity.CorrespondingSimilarity = similarity2;
			similarity2.CorrespondingSimilarity = similarity;
			Analysis.int1++;
		}

		internal void method7()
		{
			if (File.Exists("ignoreCode.txt"))
			{
				string str;

				using (StreamReader streamReader = new StreamReader("ignoreCode.txt"))
				{
					str = streamReader.ReadToEnd();
				}

				List<CodeFile> list = method8(str);

				foreach (CodeFile current in list)
				{
					foreach (CodeFile file in Files)
					{
						AllSequences allSequences = Analysis.smethod1(current, file, current.Hashes.Length - 1);
						method9(file, allSequences);
					}
				}

				method2(RootDirectories.First<CodeDir>());
				method3();
			}
		}

		private List<CodeFile> method8(string str)
		{
			string[] array = Regex.Split(str, "~~CPK\\sCode\\sChunk\\sDelimiter~~", RegexOptions.IgnoreCase);
			string arg = Options.FileSearchPattern.Substring(1, Options.FileSearchPattern.Length - 1);
			List<CodeFile> list = new List<CodeFile>();

			for (int i = 0; i < array.Length; i++)
			{
				if (!(array[i].Trim() == ""))
				{
					CodeFile item = new CodeFile("ignoreChunk" + i + arg, array[i], null);
					list.Add(item);
				}
			}

			IEnumerable<CodeFile> codeFiles = list;

			if (Analysis.func3 == null)
			{
				Analysis.func3 = new Func<CodeFile, int>(Analysis.GetHashesLength);
			}

			return codeFiles.OrderByDescending(Analysis.func3).ToList<CodeFile>();
		}

		private void method9(CodeFile codeFile, AllSequences allSequences)
		{
			if (allSequences.Sequences.Count != 0)
			{
				int i = 0;
				IL_A1:

				while (i < codeFile.Similarities.Count)
				{
					Similarity similarity = codeFile.Similarities[i];
					bool flag = false;
					int j = 0;

					while (j < allSequences.Sequences.Count)
					{
						Sequence sequence = allSequences.Sequences[j];

						if (sequence.FirstCoord.J < similarity.MyHashIndexRange.Start || sequence.LastCoord.J > similarity.MyHashIndexRange.End)
						{
							j++;
						}
						else
						{
							codeFile.Similarities.Remove(similarity);
							flag = true;
							//IL_9A:

							if (!flag)
							{
								i++;
								goto IL_A1;
							}
							goto IL_A1;
						}
					}
					//goto IL_9A;
				}
			}
		}

		[CompilerGenerated]
		private void method10()
		{
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Path);
				int minSimilarityLineLength = Options.MinSimilarityLineLength;
				FileInfo[] files = directoryInfo.GetFiles(Options.FileSearchPattern, SearchOption.AllDirectories);
				int num = 0;
				List<Regex> excludeRegexes = Options.GetExcludeRegexes();
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
							//IL_87:
							if (!flag)
							{
								num++;
							}
							i++;
							goto IL_98;
						}
					}
					//goto IL_87;
				}

				if (num == 0)
				{
					AlertAction("No " + Options.FileSearchPattern + " Files Found");
					Done();
				}
				else
				{
					UpdateProgressAction(0, num, "Loading Files...");
					CodeDir codeDir = method4(directoryInfo, Options.MinSimilarityLineLength, "");
					RootDirectories.Add(codeDir);
					ObservableCollection<CodeFile> codeFiles = Files;
					UpdateProgressAction(0, codeFiles.Count * codeFiles.Count / 2, "Comparing Files...");
					int num2 = 0;
					StringBuilder stringBuilder = new StringBuilder();

					for (int j = 0; j < codeFiles.Count; j++)
					{
						CodeFile codeFile = codeFiles[j];

						for (int k = j; k < codeFiles.Count; k++)
						{
							num2++;

							if (num2 % 500 == 0)
							{
								this.UpdateProgressValue(num2);
							}

							try
							{
								CodeFile codeFile_ = codeFiles[k];
								AllSequences allSequences = Analysis.smethod1(codeFile, codeFile_, minSimilarityLineLength);

								foreach (Sequence sequence in allSequences.Sequences)
								{
									Analysis.smethod4(codeFile, codeFile_, sequence);
								}
								goto IL_24D;
							}
							catch (Exception ex)
							{
								stringBuilder.AppendLine(codeFile.Name + " - " + codeFiles[k].Name);
								goto IL_24D;
							}
							break;
							IL_24D:;
						}

						codeFile.method1();
						codeFile.Similarities.Sort(new Comparison<Similarity>(Analysis.CompareSimilarities));
					}

					method2(codeDir);
					method3();
					method7();

					if (stringBuilder.Length > 0)
					{
						this.AlertAction("Atomiq experienced an error with the following file combinations: " + stringBuilder.ToString());
					}

					bool0 = true;
					Done();
				}
			}
			catch (Exception ex)
			{
				if (!(ex is ThreadAbortException))
				{
					CaughtException = ex;
				}

				Done();
			}
		}

		[CompilerGenerated]
		private static bool smethod5(CodeDir codeDir)
		{
			return codeDir.GetAllFilesCount() == 0;
		}

		[CompilerGenerated]
		private static bool smethod6(CodeFile codeFile)
		{
			return codeFile.Similarities.Count == 0;
		}

		[CompilerGenerated]
		private static bool smethod7(CodeFile codeFile)
		{
			return codeFile.Similarities.Count == 0;
		}

		[CompilerGenerated]
		private static int GetHashesLength(CodeFile codeFile)
		{
			return codeFile.Hashes.Length;
		}

		static Analysis()
		{
			Analysis.int0 = 0;
			Analysis.int1 = 0;
		}
	}
}