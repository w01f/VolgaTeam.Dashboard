using System.IO;
using System.Threading;
using Asa.Common.Core.OfficeInterops;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;

namespace Asa.Browser.Controls.InteropClasses
{
	public static class SaleslIbraryPowerPointExtensions
	{
		public static bool InsertVideoIntoActivePresentation(this PowerPointProcessor target, string videoFile)
		{
			bool result;
			try
			{
				var thread = new Thread(delegate ()
				{
					var newFile = Path.Combine(Path.GetDirectoryName(target.GetActivePresentation().FullName), Path.GetFileName(videoFile));
					File.Copy(videoFile, newFile, true);
					var slide = target.GetActiveSlide();
					if (slide != null)
					{
						var shape = slide.Shapes.AddMediaObject2(newFile);
						shape.AnimationSettings.PlaySettings.PlayOnEntry = MsoTriState.msoTrue;
						shape.AnimationSettings.AdvanceTime = 0;
						float maxWidth = (target.GetActivePresentation().PageSetup.SlideWidth / 10) * 9; // 5% border
						if (maxWidth < shape.Width)
						{
							shape.LockAspectRatio = MsoTriState.msoTrue;
							shape.Width = maxWidth;
						}
						shape.Left = (target.GetActivePresentation().PageSetup.SlideWidth - shape.Width) / 2;
						shape.Top = (target.GetActivePresentation().PageSetup.SlideHeight - shape.Height) / 2;
					}
				});
				thread.Start();

				while (thread.IsAlive)
					System.Windows.Forms.Application.DoEvents();


				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

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
			finally
			{
			}
		}

		private static Slide GetActiveSlide(this PowerPointProcessor target)
		{
			if (target.PowerPointObject.Windows.Count <= 0) return null;
			target.PowerPointObject.Activate();
			if (target.PowerPointObject.ActiveWindow == null) return null;
			target.PowerPointObject.ActiveWindow.ViewType = PpViewType.ppViewNormal;
			return (Slide)target.PowerPointObject.ActiveWindow.View.Slide;
		}
	}
}
