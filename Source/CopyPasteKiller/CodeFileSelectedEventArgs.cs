using System;

namespace CopyPasteKiller
{
	public class CodeFileSelectedEventArgs : EventArgs
	{
		private CodeFile codeFile_0;

		public CodeFile CodeFile
		{
			get
			{
				return this.codeFile_0;
			}
			set
			{
				this.codeFile_0 = value;
			}
		}
	}
}
