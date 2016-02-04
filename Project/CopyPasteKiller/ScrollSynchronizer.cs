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

		private static Dictionary<ScrollViewer, string> dictionary0;

		private static Dictionary<string, double> dictionary1;

		private static Dictionary<string, double> dictionary2;

		public static void SetScrollGroup(DependencyObject obj, string scrollGroup)
		{
			obj.SetValue(ScrollSynchronizer.ScrollGroupProperty, scrollGroup);
		}

		public static string GetScrollGroup(DependencyObject obj)
		{
			return (string)obj.GetValue(ScrollSynchronizer.ScrollGroupProperty);
		}

		private static void smethod0(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
		{
			ScrollViewer scrollViewer = dependencyObject as ScrollViewer;

			if (scrollViewer != null)
			{
				if (!string.IsNullOrEmpty((string)dependencyPropertyChangedEventArgs.OldValue) && ScrollSynchronizer.dictionary0.ContainsKey(scrollViewer))
				{
					scrollViewer.ScrollChanged -= new ScrollChangedEventHandler(ScrollSynchronizer.smethod1);
					ScrollSynchronizer.dictionary0.Remove(scrollViewer);
				}

				if (!string.IsNullOrEmpty((string)dependencyPropertyChangedEventArgs.NewValue))
				{
					if (ScrollSynchronizer.dictionary1.Keys.Contains((string)dependencyPropertyChangedEventArgs.NewValue))
					{
						scrollViewer.ScrollToHorizontalOffset(ScrollSynchronizer.dictionary1[(string)dependencyPropertyChangedEventArgs.NewValue]);
					}
					else
					{
						ScrollSynchronizer.dictionary1.Add((string)dependencyPropertyChangedEventArgs.NewValue, scrollViewer.HorizontalOffset);
					}
					if (ScrollSynchronizer.dictionary2.Keys.Contains((string)dependencyPropertyChangedEventArgs.NewValue))
					{
						scrollViewer.ScrollToVerticalOffset(ScrollSynchronizer.dictionary2[(string)dependencyPropertyChangedEventArgs.NewValue]);
					}
					else
					{
						ScrollSynchronizer.dictionary2.Add((string)dependencyPropertyChangedEventArgs.NewValue, scrollViewer.VerticalOffset);
					}
					ScrollSynchronizer.dictionary0.Add(scrollViewer, (string)dependencyPropertyChangedEventArgs.NewValue);
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
			string group = ScrollSynchronizer.dictionary0[scrollViewer];
			ScrollSynchronizer.dictionary2[group] = scrollViewer.VerticalOffset;
			ScrollSynchronizer.dictionary1[group] = scrollViewer.HorizontalOffset;
			foreach (KeyValuePair<ScrollViewer, string> current in from s in ScrollSynchronizer.dictionary0
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
			ScrollSynchronizer.dictionary0 = new Dictionary<ScrollViewer, string>();
			ScrollSynchronizer.dictionary1 = new Dictionary<string, double>();
			ScrollSynchronizer.dictionary2 = new Dictionary<string, double>();
		}
	}
}