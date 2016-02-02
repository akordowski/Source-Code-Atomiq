using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace CopyPasteKiller
{
	public class NoScrollTreeViewItem : TreeViewItem
	{
		[CompilerGenerated]
		private static RequestBringIntoViewEventHandler requestBringIntoViewEventHandler_0;

		public NoScrollTreeViewItem()
		{
			if (NoScrollTreeViewItem.requestBringIntoViewEventHandler_0 == null)
			{
				NoScrollTreeViewItem.requestBringIntoViewEventHandler_0 = new RequestBringIntoViewEventHandler(NoScrollTreeViewItem.smethod_0);
			}
			base.RequestBringIntoView += NoScrollTreeViewItem.requestBringIntoViewEventHandler_0;
		}

		protected override DependencyObject GetContainerForItemOverride()
		{
			return new NoScrollTreeViewItem();
		}

		[CompilerGenerated]
		private static void smethod_0(object sender, RequestBringIntoViewEventArgs e)
		{
			e.Handled = true;
		}
	}
}