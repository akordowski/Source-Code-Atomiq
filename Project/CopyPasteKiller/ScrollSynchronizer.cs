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

		private static void smethod_0(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
		{
			ScrollViewer scrollViewer = dependencyObject as ScrollViewer;
			if (scrollViewer != null)
			{
				if (!string.IsNullOrEmpty((string)dependencyPropertyChangedEventArgs.OldValue) && ScrollSynchronizer.dictionary_0.ContainsKey(scrollViewer))
				{
					scrollViewer.ScrollChanged -= new ScrollChangedEventHandler(ScrollSynchronizer.smethod_1);
					ScrollSynchronizer.dictionary_0.Remove(scrollViewer);
				}
				if (!string.IsNullOrEmpty((string)dependencyPropertyChangedEventArgs.NewValue))
				{
					if (ScrollSynchronizer.dictionary_1.Keys.Contains((string)dependencyPropertyChangedEventArgs.NewValue))
					{
						scrollViewer.ScrollToHorizontalOffset(ScrollSynchronizer.dictionary_1[(string)dependencyPropertyChangedEventArgs.NewValue]);
					}
					else
					{
						ScrollSynchronizer.dictionary_1.Add((string)dependencyPropertyChangedEventArgs.NewValue, scrollViewer.HorizontalOffset);
					}
					if (ScrollSynchronizer.dictionary_2.Keys.Contains((string)dependencyPropertyChangedEventArgs.NewValue))
					{
						scrollViewer.ScrollToVerticalOffset(ScrollSynchronizer.dictionary_2[(string)dependencyPropertyChangedEventArgs.NewValue]);
					}
					else
					{
						ScrollSynchronizer.dictionary_2.Add((string)dependencyPropertyChangedEventArgs.NewValue, scrollViewer.VerticalOffset);
					}
					ScrollSynchronizer.dictionary_0.Add(scrollViewer, (string)dependencyPropertyChangedEventArgs.NewValue);
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

		private static void smethod_2(ScrollViewer scrollViewer)
		{
			string group = ScrollSynchronizer.dictionary_0[scrollViewer];
			ScrollSynchronizer.dictionary_2[group] = scrollViewer.VerticalOffset;
			ScrollSynchronizer.dictionary_1[group] = scrollViewer.HorizontalOffset;
			foreach (KeyValuePair<ScrollViewer, string> current in from s in ScrollSynchronizer.dictionary_0
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
			ScrollSynchronizer.ScrollGroupProperty = DependencyProperty.RegisterAttached("ScrollGroup", typeof(string), typeof(ScrollSynchronizer), new PropertyMetadata(new PropertyChangedCallback(ScrollSynchronizer.smethod_0)));
			ScrollSynchronizer.dictionary_0 = new Dictionary<ScrollViewer, string>();
			ScrollSynchronizer.dictionary_1 = new Dictionary<string, double>();
			ScrollSynchronizer.dictionary_2 = new Dictionary<string, double>();
		}
	}
}