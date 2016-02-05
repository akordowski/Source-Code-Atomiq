using System;
using System.Text;

namespace CopyPasteKiller
{
	public class Builder
	{
		private string _delimiter;

		private StringBuilder _stringBuilder = new StringBuilder();

		private bool _isCleared = true;

		public Builder()
			: this(", ")
		{
		}

		public Builder(string delimiter)
		{
			_delimiter = delimiter;
		}

		public void Clear()
		{
			_stringBuilder.Remove(0, _stringBuilder.Length);
			_isCleared = true;
		}

		public void Append(object obj)
		{
			Append(obj.ToString());
		}

		public void Append(string text)
		{
			if (_isCleared)
			{
				_isCleared = false;
			}
			else
			{
				_stringBuilder.Append(_delimiter);
			}

			_stringBuilder.Append(text.ToString());
		}

		public override string ToString()
		{
			return _stringBuilder.ToString();
		}
	}
}