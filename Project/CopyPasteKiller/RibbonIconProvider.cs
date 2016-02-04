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
				New = GetImage("filenew.png");
				Save = GetImage("filesave.png");
				Open = GetImage("fileopen.png");
				Execute = GetImage("1rightarrow.png");
				Import = GetImage("fileimport.png");
				Export = GetImage("fileexport.png");
				Reanalyze = GetImage("exec.png");
			}
			catch (Exception)
			{
			}
		}

		private ImageSource GetImage(string fileName)
		{
			ImageSource result;

			if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\CrystalIcons\\32x32\\" + fileName))
			{
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\CrystalIcons\\32x32\\" + fileName, UriKind.Absolute);
				bitmapImage.EndInit();
				result = bitmapImage;
			}
			else
			{
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.StreamSource = Application.GetResourceStream(new Uri("CrystalIcons/32x32/" + fileName, UriKind.RelativeOrAbsolute)).Stream;
				bitmapImage.EndInit();
				result = bitmapImage;
			}

			return result;
		}
	}
}