using System;

namespace CopyPasteKiller
{
	public class WheelFile
	{
		private WheelViewModel _wheelViewModel;

		public WheelFile(CodeFile file, WheelViewModel wheelViewModel)
		{
			_wheelViewModel = wheelViewModel;
		}
	}
}