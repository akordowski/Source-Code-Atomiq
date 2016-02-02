using System;
using System.Windows;
using System.Windows.Controls;

namespace CopyPasteKiller
{
	public class FileTemplateSelector : DataTemplateSelector
	{
		private DataTemplate dataTemplate_0;

		private DataTemplate dataTemplate_1;

		public DataTemplate CodeFileTemplate
		{
			get
			{
				return this.dataTemplate_0;
			}
			set
			{
				this.dataTemplate_0 = value;
			}
		}

		public DataTemplate CodeDirTemplate
		{
			get
			{
				return this.dataTemplate_1;
			}
			set
			{
				this.dataTemplate_1 = value;
			}
		}

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			DataTemplate result;
			if (item is CodeFile)
			{
				result = this.CodeFileTemplate;
			}
			else if (item is CodeDir)
			{
				result = this.CodeDirTemplate;
			}
			else
			{
				result = base.SelectTemplate(item, container);
			}
			return result;
		}
	}
}
