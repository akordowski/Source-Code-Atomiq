using System;

namespace CopyPasteKiller
{
	public class Range
	{
		public int Start { get; set; }

		public int End { get; set; }

		public int Length
		{
			get
			{
				return End - Start;
			}
		}

		public string Text
		{
			get
			{
				return Start + "-" + End;
			}
		}
	}
}