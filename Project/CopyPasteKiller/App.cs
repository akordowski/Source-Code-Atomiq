using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace CopyPasteKiller
{
	public class App : Application
	{
		private class ExpiredLicenseException : Exception
		{
		}

		public static string InitialProject;

		private bool _isInitialiezed;

		private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			string text = "exception " + DateTime.Now.ToString("yyyyMMdd HH mm ss") + ".log";
			MessageBox.Show("Oh Noes! Atomiq experienced an unhandled exception. We'd really appreciate it if you could email \"" + text + "\" to support@nitriq.com", "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Hand);

			using (StreamWriter streamWriter = new StreamWriter(text))
			{
				streamWriter.Write(e.Exception.ToString());
			}
		}

		internal static string smethod0(Exception ex)
		{
			string text = "exception " + DateTime.Now.ToString("yyyyMMdd HH mm ss") + ".log";
			using (StreamWriter streamWriter = new StreamWriter(text))
			{
				streamWriter.Write(ex.ToString());
			}
			return text;
		}

		private void App_Startup(object sender, StartupEventArgs e)
		{
			if (e.Args.Length == 1 && e.Args[0].EndsWith(".atomiqProj") && File.Exists(e.Args[0]))
			{
				App.InitialProject = e.Args[0];
			}
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_isInitialiezed)
			{
				_isInitialiezed = true;
				base.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(this.App_DispatcherUnhandledException);
				base.Startup += new StartupEventHandler(this.App_Startup);
				base.StartupUri = new Uri("Window1.xaml", UriKind.Relative);
				Uri resourceLocator = new Uri("/Atomiq;component/app.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode, STAThread]
		public static void Main()
		{
			App app = new App();
			app.InitializeComponent();
			app.Run();
		}
	}
}