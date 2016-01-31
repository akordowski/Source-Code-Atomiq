using System;

namespace CopyPasteKiller
{
	public class SimilaritySelectedEventArgs : EventArgs
	{
		private Similarity similarity_0;

		public Similarity Similarity
		{
			get
			{
				return this.similarity_0;
			}
			set
			{
				this.similarity_0 = value;
			}
		}
	}
}
