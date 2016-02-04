using System;
using System.Collections.ObjectModel;
using System.IO;

namespace CopyPasteKiller
{
	public class RecentViewModel
	{
		public ObservableCollection<string> RecentDirectories { get; private set; }

		public string OpenDirectory { get; private set; }

		public RecentViewModel()
		{
			if (File.Exists("RecentDirectories.txt"))
			{
				using (StreamReader streamReader = new StreamReader("RecentDirectories.txt"))
				{
					while (!streamReader.EndOfStream)
					{
						string item = streamReader.ReadLine();
						RecentDirectories.Add(item);
					}
				}
			}
		}

		public void Open(string directory)
		{
			try
			{
				OpenDirectory = directory;
				int num = 0;

				using (StreamWriter streamWriter = new StreamWriter("RecentDirectories.txt"))
				{
					streamWriter.WriteLine(OpenDirectory);

					foreach (string current in RecentDirectories)
					{
						if (!(OpenDirectory == current))
						{
							streamWriter.WriteLine(current);

							if (num++ >= 10)
							{
								break;
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}
	}
}