using System;
using System.ComponentModel;
using System.Threading;

namespace CopyPasteKiller
{
	public class AnalyzingViewModel : INotifyPropertyChanged
	{
		private int int_0;

		private int int_1;

		private int int_2;

		private string string_0;

		[NonSerialized]
		private PropertyChangedEventHandler propertyChangedEventHandler_0;

		public event PropertyChangedEventHandler PropertyChanged
		{
			add
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.propertyChangedEventHandler_0;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Combine(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.propertyChangedEventHandler_0, value2, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
			remove
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.propertyChangedEventHandler_0;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Remove(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.propertyChangedEventHandler_0, value2, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
		}

		public int Min
		{
			get
			{
				return this.int_0;
			}
			set
			{
				if (this.int_0 != value)
				{
					this.int_0 = value;
					this.method_0("Min");
				}
			}
		}

		public int Max
		{
			get
			{
				return this.int_1;
			}
			set
			{
				if (this.int_1 != value)
				{
					this.int_1 = value;
					this.method_0("Max");
				}
			}
		}

		public int Value
		{
			get
			{
				return this.int_2;
			}
			set
			{
				if (this.int_2 != value)
				{
					this.int_2 = value;
					this.method_0("Value");
				}
			}
		}

		public string Message
		{
			get
			{
				return this.string_0;
			}
			set
			{
				if (this.string_0 != value)
				{
					this.string_0 = value;
					this.method_0("Message");
				}
			}
		}

		private void method_0(string string_1)
		{
			if (this.propertyChangedEventHandler_0 != null)
			{
				this.propertyChangedEventHandler_0(this, new PropertyChangedEventArgs(string_1));
			}
		}
	}
}
