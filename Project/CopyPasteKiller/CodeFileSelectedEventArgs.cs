using System;

namespace CopyPasteKiller
{
	public class CodeFileSelectedEventArgs : EventArgs
	{
		public CodeFile CodeFile { get; set; }
	}
}