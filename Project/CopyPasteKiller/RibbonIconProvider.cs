using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CopyPasteKiller
{
	public class RibbonIconProvider
	{
		public ImageSource New { get; set; }

		public ImageSource Save { get; set; }

		public ImageSource Open { get; set; }

		public ImageSource Execute { get; set; }

		public ImageSource Import { get; set; }

		public ImageSource Export { get; set; }

		public ImageSource Reanalyze { get; set; }

		public RibbonIconProvider()
		{
			try
			{
				New = GetFile("filenew.png");
				Save = GetFile("filesave.png");
				Open = GetFile("fileopen.png");
				Execute = GetFile("1rightarrow.png");
				Import = GetFile("fileimport.png");
				Export = GetFile("fileexport.png");
				Reanalyze = GetFile("exec.png");
			}
			catch (Exception)
			{
			}
		}

		private ImageSource GetFile(string str)
		{
			ImageSource result;

			if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\CrystalIcons\\32x32\\" + str))
			{
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\CrystalIcons\\32x32\\" + str, UriKind.Absolute);
				bitmapImage.EndInit();
				result = bitmapImage;
			}
			else
			{
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.StreamSource = Application.GetResourceStream(new Uri("CrystalIcons/32x32/" + str, UriKind.RelativeOrAbsolute)).Stream;
				bitmapImage.EndInit();
				result = bitmapImage;
			}

			return result;
		}
	}
}