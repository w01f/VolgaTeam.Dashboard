using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlideTemplateViewer
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			var fileManagerPath = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "6ms.exe");
			if (!File.Exists(fileManagerPath)) return;
			try
			{
				var process = new Process();
				process.StartInfo.FileName = fileManagerPath;
				process.StartInfo.Arguments = "addslides";
				process.Start();
			}
			catch { }
		}
	}
}
