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
		private CodeFile _codeFile;

		private string _lineNumbers;

		private double _annotationWidth;

		private double _annotationHeight;

		private ObservableCollection<Annotation> _annotations = new ObservableCollection<Annotation>();

		[NonSerialized]
		private PropertyChangedEventHandler propertyChangedEventHandler;

		[CompilerGenerated]
		private static Func<Annotation, double> func_0;

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

		public CodeFile CodeFile
		{
			get
			{
				return _codeFile;
			}
			set
			{
				if (_codeFile != value)
				{
					_codeFile = value;
					this.method_0();
					this.method_1();
					OnPropertyChanged("CodeFile");
				}
			}
		}

		public string LineNumbers
		{
			get
			{
				return _lineNumbers;
			}
			set
			{
				if (_lineNumbers != value)
				{
					_lineNumbers = value;
					OnPropertyChanged("LineNumbers");
				}
			}
		}

		public double AnnotationWidth
		{
			get
			{
				return _annotationWidth;
			}
			set
			{
				if (_annotationWidth != value)
				{
					_annotationWidth = value;
					OnPropertyChanged("AnnotationWidth");
				}
			}
		}

		public double AnnotationHeight
		{
			get
			{
				return _annotationHeight;
			}
			set
			{
				if (_annotationHeight != value)
				{
					_annotationHeight = value;
					OnPropertyChanged("AnnotationHeight");
				}
			}
		}

		public ObservableCollection<Annotation> Annotations
		{
			get
			{
				return _annotations;
			}
		}

		private void method_0()
		{
			_annotations.Clear();
			AnnotationHeight = 0.0;

			if (_codeFile != null)
			{
				AnnotationHeight = Annotation.TextHeight * (double)(_codeFile.Lines.Length + 1);

				foreach (Similarity current in _codeFile.Similarities)
				{
					_annotations.Add(new Annotation(_codeFile, current, _annotations));
				}

				if (_annotations.Count > 0)
				{
					IEnumerable<Annotation> annotations = this._annotations;

					if (AnnotatedTextViewModel.func_0 == null)
					{
						AnnotatedTextViewModel.func_0 = new Func<Annotation, double>(AnnotatedTextViewModel.smethod_0);
					}

					AnnotationWidth = annotations.Max(AnnotatedTextViewModel.func_0);
				}
				else
				{
					AnnotationWidth = Annotation.double_0;
				}
			}
		}

		private void method_1()
		{
			LineNumbers = null;

			if (_codeFile != null)
			{
				Builder builder = new Builder("\r\n");

				for (int i = 0; i < _codeFile.Lines.Length; i++)
				{
					builder.Append(i);
				}

				LineNumbers = builder.ToString();
			}
		}

		private void OnPropertyChanged(string str)
		{
			if (propertyChangedEventHandler != null)
			{
				propertyChangedEventHandler(this, new PropertyChangedEventArgs(str));
			}
		}

		[CompilerGenerated]
		private static double smethod_0(Annotation annotation)
		{
			return annotation.Left + Annotation.double_1 * 2.0 + Annotation.double_0;
		}
	}
}