using System.IO;
using System.Threading;
using Asa.Common.Core.OfficeInterops;
using Microsoft.Office.Core;

namespace Asa.Browser.Controls.InteropClasses
{
	public static class SalesLibraryPowerPointExtensions
	{
		public static void AppendSlidesFromFile(this PowerPointProcessor target, string filePath)
		{
			if (!File.Exists(filePath)) return;
			try
			{
				var thread = new Thread(delegate ()
				{
					var activeSlideIndex = target.GetActiveSlideIndex();
					var presentation = target.PowerPointObject.Presentations.Open(filePath, WithWindow: MsoTriState.msoFalse);
					target.AppendSlide(presentation, -1, indexToPaste: activeSlideIndex);
					presentation.Close();
				});
				thread.Start();

				while (thread.IsAlive)
					System.Windows.Forms.Application.DoEvents();
			}
			catch { }
		}
	}
}
