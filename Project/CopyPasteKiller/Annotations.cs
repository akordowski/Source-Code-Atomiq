using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace CopyPasteKiller
{
	public class Annotations : UserControl, IComponentConnector
	{
		private bool bool_0;

		public Annotations()
		{
			this.InitializeComponent();
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!this.bool_0)
			{
				this.bool_0 = true;
				Uri resourceLocator = new Uri("/Atomiq;component/annotations.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			this.bool_0 = true;
		}
	}
}
