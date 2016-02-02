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
		private bool _isInitialized;

		public Annotations()
		{
			InitializeComponent();
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_isInitialized)
			{
				_isInitialized = true;
				Uri resourceLocator = new Uri("/Atomiq;component/annotations.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			_isInitialized = true;
		}
	}
}