using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace CopyPasteKiller
{
	public class AnnotatedTextViewModel : INotifyPropertyChanged
	{
		private CodeFile codeFile_0;

		private string string_0;

		private double double_0;

		private double double_1;

		private ObservableCollection<Annotation> observableCollection_0 = new ObservableCollection<Annotation>();

		[NonSerialized]
		private PropertyChangedEventHandler propertyChangedEventHandler_0;

		[CompilerGenerated]
		private static Func<Annotation, double> func_0;

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

		public CodeFile CodeFile
		{
			get
			{
				return this.codeFile_0;
			}
			set
			{
				if (this.codeFile_0 != value)
				{
					this.codeFile_0 = value;
					this.method_0();
					this.method_1();
					this.method_2("CodeFile");
				}
			}
		}

		public string LineNumbers
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
					this.method_2("LineNumbers");
				}
			}
		}

		public double AnnotationWidth
		{
			get
			{
				return this.double_0;
			}
			set
			{
				if (this.double_0 != value)
				{
					this.double_0 = value;
					this.method_2("AnnotationWidth");
				}
			}
		}

		public double AnnotationHeight
		{
			get
			{
				return this.double_1;
			}
			set
			{
				if (this.double_1 != value)
				{
					this.double_1 = value;
					this.method_2("AnnotationHeight");
				}
			}
		}

		public ObservableCollection<Annotation> Annotations
		{
			get
			{
				return this.observableCollection_0;
			}
		}

		private void method_0()
		{
			this.observableCollection_0.Clear();
			this.AnnotationHeight = 0.0;
			if (this.codeFile_0 != null)
			{
				this.AnnotationHeight = Annotation.TextHeight * (double)(this.codeFile_0.Lines.Length + 1);
				foreach (Similarity current in this.codeFile_0.Similarities)
				{
					this.observableCollection_0.Add(new Annotation(this.codeFile_0, current, this.observableCollection_0));
				}
				if (this.observableCollection_0.Count > 0)
				{
					IEnumerable<Annotation> arg_D0_0 = this.observableCollection_0;
					if (AnnotatedTextViewModel.func_0 == null)
					{
						AnnotatedTextViewModel.func_0 = new Func<Annotation, double>(AnnotatedTextViewModel.smethod_0);
					}
					this.AnnotationWidth = arg_D0_0.Max(AnnotatedTextViewModel.func_0);
				}
				else
				{
					this.AnnotationWidth = Annotation.double_0;
				}
			}
		}

		private void method_1()
		{
			this.LineNumbers = null;
			if (this.codeFile_0 != null)
			{
				Builder builder = new Builder("\r\n");
				for (int i = 0; i < this.codeFile_0.Lines.Length; i++)
				{
					builder.Append(i);
				}
				this.LineNumbers = builder.ToString();
			}
		}

		private void method_2(string string_1)
		{
			if (this.propertyChangedEventHandler_0 != null)
			{
				this.propertyChangedEventHandler_0(this, new PropertyChangedEventArgs(string_1));
			}
		}

		[CompilerGenerated]
		private static double smethod_0(Annotation annotation_0)
		{
			return annotation_0.Left + Annotation.double_1 * 2.0 + Annotation.double_0;
		}
	}
}
