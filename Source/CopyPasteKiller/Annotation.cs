using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace CopyPasteKiller
{
	public class Annotation
	{
		public static double TextHeight;

		private static Brush brush_0;

		private static Brush brush_1;

		internal static double double_0;

		internal static double double_1;

		private double double_2;

		private CodeFile codeFile_0;

		private Similarity similarity_0;

		private double double_3;

		private double double_4;

		private double double_5;

		private Thickness thickness_0;

		private Brush brush_2;

		[CompilerGenerated]
		private static Func<Annotation, double> func_0;

		public CodeFile CodeFile
		{
			get
			{
				return this.codeFile_0;
			}
		}

		public Similarity Similarity
		{
			get
			{
				return this.similarity_0;
			}
		}

		public double Height
		{
			get
			{
				return this.double_3;
			}
		}

		public double Top
		{
			get
			{
				return this.double_4;
			}
		}

		public double Left
		{
			get
			{
				return this.double_5;
			}
		}

		public Thickness Margin
		{
			get
			{
				return this.thickness_0;
			}
		}

		public Brush Brush
		{
			get
			{
				return this.brush_2;
			}
		}

		public Annotation(CodeFile file, Similarity sim, IEnumerable<Annotation> existingAnnotations)
		{
			Func<Annotation, bool> func = null;
			this.double_5 = 0.0;
			base..ctor();
			this.codeFile_0 = file;
			this.similarity_0 = sim;
			this.double_3 = (double)sim.MyRange.Length * Annotation.TextHeight;
			this.double_4 = (double)sim.MyRange.Start * Annotation.TextHeight;
			this.double_2 = this.double_4 + this.double_3;
			if (file == sim.OtherFile)
			{
				this.brush_2 = Annotation.brush_1;
			}
			else
			{
				this.brush_2 = Annotation.brush_0;
			}
			if (func == null)
			{
				func = new Func<Annotation, bool>(this.method_0);
			}
			List<Annotation> list = existingAnnotations.Where(func).ToList<Annotation>();
			if (list.Count == 0)
			{
				this.double_5 = Annotation.double_1 + 2.0;
			}
			else
			{
				IEnumerable<Annotation> arg_EE_0 = list;
				if (Annotation.func_0 == null)
				{
					Annotation.func_0 = new Func<Annotation, double>(Annotation.smethod_0);
				}
				double num = arg_EE_0.Max(Annotation.func_0);
				this.double_5 = num + Annotation.double_1 + Annotation.double_0;
			}
			this.thickness_0 = new Thickness(this.double_5, this.double_4, 0.0, 0.0);
		}

		[CompilerGenerated]
		private bool method_0(Annotation annotation_0)
		{
			return this.double_4 <= annotation_0.double_2 && this.double_2 >= annotation_0.double_4;
		}

		[CompilerGenerated]
		private static double smethod_0(Annotation annotation_0)
		{
			return annotation_0.double_5;
		}

		static Annotation()
		{
			Annotation.TextHeight = 12.885;
			Annotation.brush_0 = new SolidColorBrush(Color.FromArgb(255, 17, 112, 189));
			Annotation.brush_1 = new SolidColorBrush(Color.FromArgb(255, 200, 44, 38));
			Annotation.double_0 = 5.0;
			Annotation.double_1 = 3.0;
		}
	}
}
