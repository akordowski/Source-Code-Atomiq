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

			foreach (Sequence sequence in Sequences)
			{
				if (sequence.LastCoord.I + 1 == coord.I && sequence.LastCoord.J + 1 == coord.J)
				{
					sequence.LastCoord = coord;
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