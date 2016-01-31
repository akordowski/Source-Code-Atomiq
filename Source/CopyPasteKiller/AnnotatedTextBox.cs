using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace CopyPasteKiller
{
	public class AnnotatedTextBox : UserControl, IComponentConnector, IStyleConnector
	{
		private AnnotatedTextViewModel annotatedTextViewModel_0 = new AnnotatedTextViewModel();

		public static readonly DependencyProperty CodeFileProperty;

		public static readonly DependencyProperty SimilarityProperty;

		private EventHandler<SimilaritySelectedEventArgs> eventHandler_0;

		internal Grid grid_0;

		internal TextBox textBox_0;

		internal TextBox textBox_1;

		private bool bool_0;

		public event EventHandler<SimilaritySelectedEventArgs> SimilaritySelected
		{
			add
			{
				EventHandler<SimilaritySelectedEventArgs> eventHandler = this.eventHandler_0;
				EventHandler<SimilaritySelectedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<SimilaritySelectedEventArgs> value2 = (EventHandler<SimilaritySelectedEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<SimilaritySelectedEventArgs>>(ref this.eventHandler_0, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler<SimilaritySelectedEventArgs> eventHandler = this.eventHandler_0;
				EventHandler<SimilaritySelectedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<SimilaritySelectedEventArgs> value2 = (EventHandler<SimilaritySelectedEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<SimilaritySelectedEventArgs>>(ref this.eventHandler_0, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		public CodeFile CodeFile
		{
			get
			{
				return (CodeFile)base.GetValue(AnnotatedTextBox.CodeFileProperty);
			}
			set
			{
				base.SetValue(AnnotatedTextBox.CodeFileProperty, value);
			}
		}

		public Similarity Similarity
		{
			get
			{
				return (Similarity)base.GetValue(AnnotatedTextBox.SimilarityProperty);
			}
			set
			{
				base.SetValue(AnnotatedTextBox.SimilarityProperty, value);
			}
		}

		public AnnotatedTextBox()
		{
			this.InitializeComponent();
			this.grid_0.DataContext = this.annotatedTextViewModel_0;
		}

		private static object smethod_0(DependencyObject dependencyObject_0, CodeFile codeFile_0)
		{
			AnnotatedTextBox annotatedTextBox = dependencyObject_0 as AnnotatedTextBox;
			object result;
			if (annotatedTextBox != null)
			{
				result = annotatedTextBox.OnCoerceCodeFile((CodeFile)codeFile_0);
			}
			else
			{
				result = codeFile_0;
			}
			return result;
		}

		private static void smethod_1(DependencyObject dependencyObject_0, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs_0)
		{
			AnnotatedTextBox annotatedTextBox = dependencyObject_0 as AnnotatedTextBox;
			if (annotatedTextBox != null)
			{
				annotatedTextBox.OnCodeFileChanged((CodeFile)dependencyPropertyChangedEventArgs_0.OldValue, (CodeFile)dependencyPropertyChangedEventArgs_0.NewValue);
			}
		}

		protected virtual CodeFile OnCoerceCodeFile(CodeFile value)
		{
			return value;
		}

		protected virtual void OnCodeFileChanged(CodeFile oldValue, CodeFile newValue)
		{
			this.annotatedTextViewModel_0.CodeFile = newValue;
		}

		private static object smethod_2(DependencyObject dependencyObject_0, Similarity similarity_0)
		{
			AnnotatedTextBox annotatedTextBox = dependencyObject_0 as AnnotatedTextBox;
			object result;
			if (annotatedTextBox != null)
			{
				result = annotatedTextBox.OnCoerceSimilarity((Similarity)similarity_0);
			}
			else
			{
				result = similarity_0;
			}
			return result;
		}

		private static void smethod_3(DependencyObject dependencyObject_0, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs_0)
		{
			AnnotatedTextBox annotatedTextBox = dependencyObject_0 as AnnotatedTextBox;
			if (annotatedTextBox != null)
			{
				annotatedTextBox.OnSimilarityChanged((Similarity)dependencyPropertyChangedEventArgs_0.OldValue, (Similarity)dependencyPropertyChangedEventArgs_0.NewValue);
			}
		}

		protected virtual Similarity OnCoerceSimilarity(Similarity value)
		{
			if (value == null)
			{
				this.method_1(0);
			}
			else
			{
				this.method_1(value.MyRange.Start);
			}
			return value;
		}

		protected virtual void OnSimilarityChanged(Similarity oldValue, Similarity newValue)
		{
		}

		private void method_0(object sender, MouseButtonEventArgs e)
		{
			FrameworkElement frameworkElement = (FrameworkElement)sender;
			Annotation annotation = (Annotation)frameworkElement.DataContext;
			if (this.eventHandler_0 != null)
			{
				this.eventHandler_0(this, new SimilaritySelectedEventArgs
				{
					Similarity = annotation.Similarity.CorrespondingSimilarity
				});
			}
		}

		internal void method_1(int int_0)
		{
			try
			{
				this.textBox_1.ScrollToVerticalOffset((double)int_0 * Annotation.TextHeight);
			}
			catch (Exception)
			{
			}
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!this.bool_0)
			{
				this.bool_0 = true;
				Uri resourceLocator = new Uri("/Atomiq;component/annotatedtextbox.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.grid_0 = (Grid)target;
				return;
			case 3:
				this.textBox_0 = (TextBox)target;
				return;
			case 4:
				this.textBox_1 = (TextBox)target;
				return;
			}
			this.bool_0 = true;
		}

		[EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 2)
			{
				((Rectangle)target).MouseDown += new MouseButtonEventHandler(this.method_0);
			}
		}

		static AnnotatedTextBox()
		{
			AnnotatedTextBox.CodeFileProperty = DependencyProperty.Register("CodeFile", typeof(CodeFile), typeof(AnnotatedTextBox), new UIPropertyMetadata(null, new PropertyChangedCallback(AnnotatedTextBox.smethod_1), new CoerceValueCallback(AnnotatedTextBox.smethod_0)));
			AnnotatedTextBox.SimilarityProperty = DependencyProperty.Register("Similarity", typeof(Similarity), typeof(AnnotatedTextBox), new UIPropertyMetadata(null, new PropertyChangedCallback(AnnotatedTextBox.smethod_3), new CoerceValueCallback(AnnotatedTextBox.smethod_2)));
		}
	}
}
