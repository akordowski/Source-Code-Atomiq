using System;
using System.Windows;
using System.Windows.Controls;

namespace CopyPasteKiller
{
	public class FileTemplateSelector : DataTemplateSelector
	{
		public DataTemplate CodeFileTemplate { get; set; }

		public DataTemplate CodeDirTemplate { get; set; }

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			DataTemplate result;
			if (item is CodeFile)
			{
				result = CodeFileTemplate;
			}
			else if (item is CodeDir)
			{
				result = CodeDirTemplate;
			}
			else
			{
				result = base.SelectTemplate(item, container);
			}
			return result;
		}
	}
}