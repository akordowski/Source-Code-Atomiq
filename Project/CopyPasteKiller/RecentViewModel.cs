using System;
using System.Collections.ObjectModel;
using System.IO;

namespace CopyPasteKiller
{
	public class RecentViewModel
	{
		private ObservableCollection<string> observableCollection_0 = new ObservableCollection<string>();

		private string string_0;

		public ObservableCollection<string> RecentDirectories
		{
			get
			{
				return this.observableCollection_0;
			}
		}

		public string OpenDirectory
		{
			get
			{
				return this.string_0;
			}
		}

		public RecentViewModel()
		{
			if (File.Exists("RecentDirectories.txt"))
			{
				using (StreamReader streamReader = new StreamReader("RecentDirectories.txt"))
				{
					while (!streamReader.EndOfStream)
					{
						string item = streamReader.ReadLine();
						this.RecentDirectories.Add(item);
					}
				}
			}
		}

		public void Open(string directory)
		{
			try
			{
				this.string_0 = directory;
				int num = 0;
				using (StreamWriter streamWriter = new StreamWriter("RecentDirectories.txt"))
				{
					streamWriter.WriteLine(this.string_0);
					foreach (string current in this.observableCollection_0)
					{
						if (!(this.string_0 == current))
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
