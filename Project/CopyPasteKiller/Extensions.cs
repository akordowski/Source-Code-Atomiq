using System;
using System.Collections.Generic;
using System.Linq;

namespace CopyPasteKiller
{
	public static class Extensions
	{
		public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> range)
		{
			foreach (T current in range)
			{
				hashSet.Add(current);
			}
		}

		public static void RemoveWhere<T>(this ICollection<T> collection, Func<T, bool> removalPredicate)
		{
			T[] array = collection.Where(removalPredicate).ToArray<T>();
			T[] array2 = array;

			for (int i = 0; i < array2.Length; i++)
			{
				T item = array2[i];
				collection.Remove(item);
			}
		}

		public static string NumShortener(this int num)
		{
			string result;

			if (num >= 1000000)
			{
				double value = (double)num / 1000000.0;
				result = Math.Round(value, 0).ToString() + "M";
			}
			else if (num >= 1000)
			{
				double value = (double)num / 1000.0;
				result = Math.Round(value, 1).ToString() + "K";
			}
			else
			{
				result = num.ToString();
			}

			return result;
		}
	}
}