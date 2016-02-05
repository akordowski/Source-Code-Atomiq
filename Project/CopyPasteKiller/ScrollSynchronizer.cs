using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CopyPasteKiller
{
	public class ScrollSynchronizer : DependencyObject
	{
		public static readonly DependencyProperty ScrollGroupProperty;

		private static Dictionary<ScrollViewer, string> _dictionary0;

		private static Dictionary<string, double> _dictionary1;

		private static Dictionary<string, double> _dictionary2;

		public static void SetScrollGroup(DependencyObject obj, string scrollGroup)
		{
			obj.SetValue(ScrollSynchronizer.ScrollGroupProperty, scrollGroup);
		}

		public static string GetScrollGroup(DependencyObject obj)
		{
			return (string)obj.GetValue(ScrollSynchronizer.ScrollGroupProperty);
		}

		private static void smethod0(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
		{
			ScrollViewer scrollViewer = dependencyObject as ScrollViewer;

			if (scrollViewer != null)
			{
				if (!string.IsNullOrEmpty((string)eventArgs.OldValue) && ScrollSynchronizer._dictionary0.ContainsKey(scrollViewer))
				{
					scrollViewer.ScrollChanged -= new ScrollChangedEventHandler(ScrollSynchronizer.smethod1);
					ScrollSynchronizer._dictionary0.Remove(scrollViewer);
				}

				if (!string.IsNullOrEmpty((string)eventArgs.NewValue))
				{
					if (ScrollSynchronizer._dictionary1.Keys.Contains((string)eventArgs.NewValue))
					{
						scrollViewer.ScrollToHorizontalOffset(ScrollSynchronizer._dictionary1[(string)eventArgs.NewValue]);
					}
					else
					{
						ScrollSynchronizer._dictionary1.Add((string)eventArgs.NewValue, scrollViewer.HorizontalOffset);
					}

					if (ScrollSynchronizer._dictionary2.Keys.Contains((string)eventArgs.NewValue))
					{
						scrollViewer.ScrollToVerticalOffset(ScrollSynchronizer._dictionary2[(string)eventArgs.NewValue]);
					}
					else
					{
						ScrollSynchronizer._dictionary2.Add((string)eventArgs.NewValue, scrollViewer.VerticalOffset);
					}

					ScrollSynchronizer._dictionary0.Add(scrollViewer, (string)eventArgs.NewValue);
					scrollViewer.ScrollChanged += new ScrollChangedEventHandler(ScrollSynchronizer.smethod1);
				}
			}
		}

		private static void smethod1(object sender, ScrollChangedEventArgs e)
		{
			if (e.VerticalChange != 0.0 || e.HorizontalChange != 0.0)
			{
				ScrollViewer scrollViewer = sender as ScrollViewer;
				ScrollSynchronizer.smethod2(scrollViewer);
			}
		}

		private static void smethod2(ScrollViewer scrollViewer)
		{
			string group = ScrollSynchronizer._dictionary0[scrollViewer];
			ScrollSynchronizer._dictionary2[group] = scrollViewer.VerticalOffset;
			ScrollSynchronizer._dictionary1[group] = scrollViewer.HorizontalOffset;

			foreach (KeyValuePair<ScrollViewer, string> current in from s in ScrollSynchronizer._dictionary0
			where s.Value == @group && s.Key != scrollViewer
			select s)
			{
				if (current.Key.VerticalOffset != scrollViewer.VerticalOffset)
				{
					current.Key.ScrollToVerticalOffset(scrollViewer.VerticalOffset);
				}
				if (current.Key.HorizontalOffset != scrollViewer.HorizontalOffset)
				{
					current.Key.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset);
				}
			}
		}

		static ScrollSynchronizer()
		{
			ScrollSynchronizer.ScrollGroupProperty = DependencyProperty.RegisterAttached("ScrollGroup", typeof(string), typeof(ScrollSynchronizer), new PropertyMetadata(new PropertyChangedCallback(ScrollSynchronizer.smethod0)));
			ScrollSynchronizer._dictionary0 = new Dictionary<ScrollViewer, string>();
			ScrollSynchronizer._dictionary1 = new Dictionary<string, double>();
			ScrollSynchronizer._dictionary2 = new Dictionary<string, double>();
		}
	}
}