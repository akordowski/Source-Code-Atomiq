using System;
using System.Diagnostics;

namespace CopyPasteKiller
{
	[DebuggerDisplay("Sequence: {FirstCoord.I}-{LastCoord.I}, {FirstCoord.J}-{LastCoord.J}")]
	public class Sequence
	{
		public Coord FirstCoord;

		public Coord LastCoord;
	}
}