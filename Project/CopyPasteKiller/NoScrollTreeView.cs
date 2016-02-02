using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CopyPasteKiller
{
	public class NoScrollTreeView : TreeView
	{
		protected override DependencyObject GetContainerForItemOverride()
		{
			return new NoScrollTreeViewItem();
		}

		protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
		{
			base.OnMouseDoubleClick(e);
		}
	}
}