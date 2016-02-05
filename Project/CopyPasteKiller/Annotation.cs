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

		private static Brush brush1;

		private static Brush brush2;

		internal static double double1;

		internal static double double2;

		private double double3;

		[CompilerGenerated]
		private static Func<Annotation, double> func0;

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
			//base..ctor();
			CodeFile = file;
			Similarity = sim;
			Height = (double)sim.MyRange.Length * Annotation.TextHeight;
			Top = (double)sim.MyRange.Start * Annotation.TextHeight;
			double3 = Top + Height;

			if (file == sim.OtherFile)
			{
				Brush = Annotation.brush2;
			}
			else
			{
				Brush = Annotation.brush1;
			}

			if (func == null)
			{
				func = new Func<Annotation, bool>(method0);
			}

			List<Annotation> list = existingAnnotations.Where(func).ToList<Annotation>();

			if (list.Count == 0)
			{
				Left = Annotation.double2 + 2.0;
			}
			else
			{
				IEnumerable<Annotation> annotations = list;

				if (Annotation.func0 == null)
				{
					Annotation.func0 = new Func<Annotation, double>(Annotation.smethod0);
				}

				double num = annotations.Max(Annotation.func0);
				Left = num + Annotation.double2 + Annotation.double1;
			}

			Margin = new Thickness(Left, Top, 0.0, 0.0);
		}

		[CompilerGenerated]
		private bool method0(Annotation annotation)
		{
			return Top <= annotation.double3 && double3 >= annotation.Top;
		}

		[CompilerGenerated]
		private static double smethod0(Annotation annotation)
		{
			return annotation.Left;
		}

		static Annotation()
		{
			Annotation.TextHeight = 12.885;
			Annotation.brush1 = new SolidColorBrush(Color.FromArgb(255, 17, 112, 189));
			Annotation.brush2 = new SolidColorBrush(Color.FromArgb(255, 200, 44, 38));
			Annotation.double1 = 5.0;
			Annotation.double2 = 3.0;
		}
	}
}