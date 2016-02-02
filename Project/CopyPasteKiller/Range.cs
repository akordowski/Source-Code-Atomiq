using System;

namespace CopyPasteKiller
{
	public class Range
	{
		private int int_0;

		private int int_1;

		public int Start
		{
			get
			{
				return this.int_0;
			}
			set
			{
				this.int_0 = value;
			}
		}

		public int End
		{
			get
			{
				return this.int_1;
			}
			set
			{
				this.int_1 = value;
			}
		}

		public int Length
		{
			get
			{
				return this.int_1 - this.int_0;
			}
		}

		public string Text
		{
			get
			{
				return this.int_0 + "-" + this.int_1;
			}
		}
	}
}
