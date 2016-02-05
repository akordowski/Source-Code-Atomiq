using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace CopyPasteKiller
{
	public class NoScrollTreeViewItem : TreeViewItem
	{
		[CompilerGenerated]
		private static RequestBringIntoViewEventHandler _requestBringIntoViewEventHandler;

		public NoScrollTreeViewItem()
		{
			if (NoScrollTreeViewItem._requestBringIntoViewEventHandler == null)
			{
				NoScrollTreeViewItem._requestBringIntoViewEventHandler = new RequestBringIntoViewEventHandler(NoScrollTreeViewItem.BringIntoView);
			}

			base.RequestBringIntoView += NoScrollTreeViewItem._requestBringIntoViewEventHandler;
		}

		protected override DependencyObject GetContainerForItemOverride()
		{
			return new NoScrollTreeViewItem();
		}

		[CompilerGenerated]
		private static void BringIntoView(object sender, RequestBringIntoViewEventArgs e)
		{
			e.Handled = true;
		}
	}
}