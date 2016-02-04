using System;
using System.ComponentModel;
using System.Threading;

namespace CopyPasteKiller
{
	public class AnalyzingViewModel : INotifyPropertyChanged
	{
		private int _min;

		private int _max;

		private int _value;

		private string _message;

		[NonSerialized]
		private PropertyChangedEventHandler propertyChangedEventHandler;

		public event PropertyChangedEventHandler PropertyChanged
		{
			add
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.propertyChangedEventHandler;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Combine(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.propertyChangedEventHandler, value2, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
			remove
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.propertyChangedEventHandler;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Remove(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.propertyChangedEventHandler, value2, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
		}

		public int Min
		{
			get
			{
				return _min;
			}
			set
			{
				if (_min != value)
				{
					_min = value;
					OnPropertyChanged("Min");
				}
			}
		}

		public int Max
		{
			get
			{
				return _max;
			}
			set
			{
				if (_max != value)
				{
					_max = value;
					OnPropertyChanged("Max");
				}
			}
		}

		public int Value
		{
			get
			{
				return _value;
			}
			set
			{
				if (_value != value)
				{
					_value = value;
					OnPropertyChanged("Value");
				}
			}
		}

		public string Message
		{
			get
			{
				return _message;
			}
			set
			{
				if (_message != value)
				{
					_message = value;
					OnPropertyChanged("Message");
				}
			}
		}

		private void OnPropertyChanged(string str)
		{
			if (propertyChangedEventHandler != null)
			{
				propertyChangedEventHandler(this, new PropertyChangedEventArgs(str));
			}
		}
	}
}