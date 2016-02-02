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

		[CompilerGenerated]
		private static Func<Annotation, double> func_0;

		public CodeFile CodeFile { get; private set; }

		public Similarity Similarity { get; private set; }

		public double Height { get; private set; }

		public double Top { get; private set; }

		public double Left { get; private set; }

		public Thickness Margin { get; private set; }

		public Brush Brush { get; private set; }

		public Annotation(CodeFile file, Similarity sim, IEnumerable<Annotation> existingAnnotations)
		{
			Func<Annotation, bool> func = null;
			Left = 0.0;
			CodeFile = file;
			Similarity = sim;
			Height = (double)sim.MyRange.Length * Annotation.TextHeight;
			Top = (double)sim.MyRange.Start * Annotation.TextHeight;
			this.double_2 = Top + Height;
			if (file == sim.OtherFile)
			{
				Brush = Annotation.brush_1;
			}
			else
			{
				Brush = Annotation.brush_0;
			}
			if (func == null)
			{
				func = new Func<Annotation, bool>(this.method_0);
			}
			List<Annotation> list = existingAnnotations.Where(func).ToList<Annotation>();
			if (list.Count == 0)
			{
				Left = Annotation.double_1 + 2.0;
			}
			else
			{
				IEnumerable<Annotation> arg_EE_0 = list;
				if (Annotation.func_0 == null)
				{
					Annotation.func_0 = new Func<Annotation, double>(Annotation.smethod_0);
				}
				double num = arg_EE_0.Max(Annotation.func_0);
				Left = num + Annotation.double_1 + Annotation.double_0;
			}
			Margin = new Thickness(Left, Top, 0.0, 0.0);
		}

		[CompilerGenerated]
		private bool method_0(Annotation annotation)
		{
			return Top <= annotation.double_2 && double_2 >= annotation.Top;
		}

		[CompilerGenerated]
		private static double smethod_0(Annotation annotation)
		{
			return annotation.Left;
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