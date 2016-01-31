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

		private static Dictionary<ScrollViewer, string> dictionary_0;

		private static Dictionary<string, double> dictionary_1;

		private static Dictionary<string, double> dictionary_2;

		public static void SetScrollGroup(DependencyObject obj, string scrollGroup)
		{
			obj.SetValue(ScrollSynchronizer.ScrollGroupProperty, scrollGroup);
		}

		public static string GetScrollGroup(DependencyObject obj)
		{
			return (string)obj.GetValue(ScrollSynchronizer.ScrollGroupProperty);
		}

		private static void smethod_0(DependencyObject dependencyObject_0, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs_0)
		{
			ScrollViewer scrollViewer = dependencyObject_0 as ScrollViewer;
			if (scrollViewer != null)
			{
				if (!string.IsNullOrEmpty((string)dependencyPropertyChangedEventArgs_0.OldValue) && ScrollSynchronizer.dictionary_0.ContainsKey(scrollViewer))
				{
					scrollViewer.ScrollChanged -= new ScrollChangedEventHandler(ScrollSynchronizer.smethod_1);
					ScrollSynchronizer.dictionary_0.Remove(scrollViewer);
				}
				if (!string.IsNullOrEmpty((string)dependencyPropertyChangedEventArgs_0.NewValue))
				{
					if (ScrollSynchronizer.dictionary_1.Keys.Contains((string)dependencyPropertyChangedEventArgs_0.NewValue))
					{
						scrollViewer.ScrollToHorizontalOffset(ScrollSynchronizer.dictionary_1[(string)dependencyPropertyChangedEventArgs_0.NewValue]);
					}
					else
					{
						ScrollSynchronizer.dictionary_1.Add((string)dependencyPropertyChangedEventArgs_0.NewValue, scrollViewer.HorizontalOffset);
					}
					if (ScrollSynchronizer.dictionary_2.Keys.Contains((string)dependencyPropertyChangedEventArgs_0.NewValue))
					{
						scrollViewer.ScrollToVerticalOffset(ScrollSynchronizer.dictionary_2[(string)dependencyPropertyChangedEventArgs_0.NewValue]);
					}
					else
					{
						ScrollSynchronizer.dictionary_2.Add((string)dependencyPropertyChangedEventArgs_0.NewValue, scrollViewer.VerticalOffset);
					}
					ScrollSynchronizer.dictionary_0.Add(scrollViewer, (string)dependencyPropertyChangedEventArgs_0.NewValue);
					scrollViewer.ScrollChanged += new ScrollChangedEventHandler(ScrollSynchronizer.smethod_1);
				}
			}
		}

		private static void smethod_1(object sender, ScrollChangedEventArgs e)
		{
			if (e.VerticalChange != 0.0 || e.HorizontalChange != 0.0)
			{
				ScrollViewer scrollViewer_ = sender as ScrollViewer;
				ScrollSynchronizer.smethod_2(scrollViewer_);
			}
		}

		private static void smethod_2(ScrollViewer scrollViewer_0)
		{
			string group = ScrollSynchronizer.dictionary_0[scrollViewer_0];
			ScrollSynchronizer.dictionary_2[group] = scrollViewer_0.VerticalOffset;
			ScrollSynchronizer.dictionary_1[group] = scrollViewer_0.HorizontalOffset;
			foreach (KeyValuePair<ScrollViewer, string> current in from s in ScrollSynchronizer.dictionary_0
			where s.Value == @group && s.Key != scrollViewer_0
			select s)
			{
				if (current.Key.VerticalOffset != scrollViewer_0.VerticalOffset)
				{
					current.Key.ScrollToVerticalOffset(scrollViewer_0.VerticalOffset);
				}
				if (current.Key.HorizontalOffset != scrollViewer_0.HorizontalOffset)
				{
					current.Key.ScrollToHorizontalOffset(scrollViewer_0.HorizontalOffset);
				}
			}
		}

		static ScrollSynchronizer()
		{
			ScrollSynchronizer.ScrollGroupProperty = DependencyProperty.RegisterAttached("ScrollGroup", typeof(string), typeof(ScrollSynchronizer), new PropertyMetadata(new PropertyChangedCallback(ScrollSynchronizer.smethod_0)));
			ScrollSynchronizer.dictionary_0 = new Dictionary<ScrollViewer, string>();
			ScrollSynchronizer.dictionary_1 = new Dictionary<string, double>();
			ScrollSynchronizer.dictionary_2 = new Dictionary<string, double>();
		}
	}
}
