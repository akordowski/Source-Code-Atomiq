using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace CopyPasteKiller
{
	public class NoScrollTreeViewItem : TreeViewItem
	{
		[CompilerGenerated]
		private static RequestBringIntoViewEventHandler requestBringIntoViewEventHandler;

		public NoScrollTreeViewItem()
		{
			if (NoScrollTreeViewItem.requestBringIntoViewEventHandler == null)
			{
				NoScrollTreeViewItem.requestBringIntoViewEventHandler = new RequestBringIntoViewEventHandler(NoScrollTreeViewItem.smethod0);
			}
			base.RequestBringIntoView += NoScrollTreeViewItem.requestBringIntoViewEventHandler;
		}

		protected override DependencyObject GetContainerForItemOverride()
		{
			return new NoScrollTreeViewItem();
		}

		[CompilerGenerated]
		private static void smethod0(object sender, RequestBringIntoViewEventArgs e)
		{
			e.Handled = true;
		}
	}
}