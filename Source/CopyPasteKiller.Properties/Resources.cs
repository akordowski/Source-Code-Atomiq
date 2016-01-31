using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace CopyPasteKiller.Properties
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
	internal class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("CopyPasteKiller.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		internal static Icon atomiq
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("atomiq", Resources.resourceCulture);
				return (Icon)@object;
			}
		}

		internal static Icon atomiq1
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("atomiq1", Resources.resourceCulture);
				return (Icon)@object;
			}
		}

		internal static Bitmap CPKIcon
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("CPKIcon", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Icon CPKIcon1
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("CPKIcon1", Resources.resourceCulture);
				return (Icon)@object;
			}
		}

		internal Resources()
		{
		}
	}
}
