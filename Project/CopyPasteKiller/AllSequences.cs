using System;
using System.Collections.Generic;

namespace CopyPasteKiller
{
	public class AllSequences
	{
		public List<Sequence> Sequences = new List<Sequence>();

		public void AddCoordToAppropriateSequence(Coord coord)
		{
			bool flag = false;

			foreach (Sequence current in Sequences)
			{
				if (current.LastCoord.I + 1 == coord.I && current.LastCoord.J + 1 == coord.J)
				{
					current.LastCoord = coord;
					flag = true;
					break;
				}
			}

			if (!flag)
			{
				Sequences.Add(new Sequence
				{
					FirstCoord = new Coord
					{
						I = coord.I - coord.Size + 1,
						J = coord.J - coord.Size + 1,
						Size = 1
					},
					LastCoord = coord
				});
			}
		}
	}
}