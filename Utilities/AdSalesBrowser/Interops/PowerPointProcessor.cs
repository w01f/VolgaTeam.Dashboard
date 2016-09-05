using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using AdSalesBrowser.SalesLibraryExtensions.LinkViewContent;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Application = Microsoft.Office.Interop.PowerPoint.Application;

namespace AdSalesBrowser.Interops
{
	public abstract class PowerPointProcessor
	{
		private bool _isFirstLaunch;
		public Application PowerPointObject { get; private set; }
		public Presentation SlideSourcePresentation { get; private set; }
		public SlideShowWindow SlideShowWindow { get; private set; }

		public IntPtr WindowHandle => PowerPointObject != null ? new IntPtr(PowerPointObject.HWND) : IntPtr.Zero;

		public bool IsLinkedWithApplication
		{
			get
			{
				if (!Process.GetProcessesByName("POWERPNT").Any())
				{
					PowerPointObject = null;
					return false;
				}
				try
				{
					if (PowerPointObject == null)
						PowerPointObject = GetExistedPowerPoint();
					return PowerPointObject.Caption != "";
				}
				catch
				{
					PowerPointObject = null;
					return false;
				}
			}
		}

		public virtual bool Connect(bool forceNewObject = false)
		{
			try
			{
				if (forceNewObject)
				{
					_isFirstLaunch = GetExistedPowerPoint() == null;
					PowerPointObject = CreateNewPowerPoint();
				}
				else
					PowerPointObject = GetExistedPowerPoint();
				PowerPointObject.DisplayAlerts = PpAlertLevel.ppAlertsNone;
			}
			catch
			{
				PowerPointObject = null;
			}
			finally
			{
			}
			return PowerPointObject != null;
		}

		private Application CreateNewPowerPoint()
		{
			return new Application();
		}

		private Application GetExistedPowerPoint()
		{
			try
			{
				_isFirstLaunch = false;
				return Marshal.GetActiveObject("PowerPoint.Application") as Application;
			}
			catch
			{
				return null;
			}
		}

		public void Disconnect(bool closeIfFirstLaunch = false)
		{
			if (_isFirstLaunch && closeIfFirstLaunch)
			{
				Close();
				_isFirstLaunch = false;
			}
			Utilities.ReleaseComObject(PowerPointObject);
			PowerPointObject = null;
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		protected void Close()
		{
			try
			{
				PowerPointObject.Quit();
				uint lpdwProcessId;
				WinAPIHelper.GetWindowThreadProcessId(WindowHandle, out lpdwProcessId);
				var powerPointProcessId = (int)lpdwProcessId;
				Process.GetProcessById(powerPointProcessId).CloseMainWindow();
			}
			catch { }
			finally
			{
				try
				{
					Process.GetProcessesByName("POWERPNT").ToList().ForEach(p => p.Kill());
				}
				catch { }
			}
		}

		public Presentation GetActivePresentation(bool create = true)
		{
			Presentation presentation;
			try
			{
				presentation = PowerPointObject.ActivePresentation;
			}
			catch
			{
				presentation = null;
				if (create)
				{
					try
					{
						if (PowerPointObject.Presentations.Count == 0)
						{
							var presentations = PowerPointObject.Presentations;
							presentation = presentations.Add(MsoTriState.msoCTrue);
							Utilities.ReleaseComObject(presentations);
							var slides = presentation.Slides;
							slides.Add(1, PpSlideLayout.ppLayoutTitle);
							Utilities.ReleaseComObject(slides);
						}
						else
						{
							var presentations = PowerPointObject.Presentations;
							presentation = presentations[1];
							Utilities.ReleaseComObject(presentations);
						}
					}
					catch
					{
						presentation = null;
					}
				}
			}
			return presentation;
		}

		public int GetActiveSlideIndex()
		{
			var slideIndex = -1;
			try
			{
				slideIndex = ((Slide)PowerPointObject.ActiveWindow.View.Slide).SlideIndex;
			}
			catch
			{
				try
				{
					PowerPointObject.Activate();
					slideIndex = ((Slide)PowerPointObject.ActiveWindow.View.Slide).SlideIndex;
				}
				catch { }
			}
			return slideIndex;
		}

		public Slide GetActiveSlide()
		{
			if (PowerPointObject.Windows.Count <= 0) return null;
			PowerPointObject.Activate();
			if (PowerPointObject.ActiveWindow == null) return null;
			PowerPointObject.ActiveWindow.ViewType = PpViewType.ppViewNormal;
			return (Slide)PowerPointObject.ActiveWindow.View.Slide;
		}

		public bool InsertVideoIntoActivePresentation(string videoFile)
		{
			bool result;
			try
			{
				var thread = new Thread(delegate ()
				{
					var newFile = Path.Combine(Path.GetDirectoryName(GetActivePresentation().FullName), Path.GetFileName(videoFile));
					File.Copy(videoFile, newFile, true);
					var slide = GetActiveSlide();
					if (slide != null)
					{
						var shape = slide.Shapes.AddMediaObject2(newFile);
						shape.AnimationSettings.PlaySettings.PlayOnEntry = MsoTriState.msoTrue;
						shape.AnimationSettings.AdvanceTime = 0;
						float maxWidth = (GetActivePresentation().PageSetup.SlideWidth / 10) * 9; // 5% border
						if (maxWidth < shape.Width)
						{
							shape.LockAspectRatio = MsoTriState.msoTrue;
							shape.Width = maxWidth;
						}
						shape.Left = (GetActivePresentation().PageSetup.SlideWidth - shape.Width) / 2;
						shape.Top = (GetActivePresentation().PageSetup.SlideHeight - shape.Height) / 2;
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

		public void AppendSlidesFromFile(string filePath)
		{
			if (!File.Exists(filePath)) return;
			try
			{
				var thread = new Thread(delegate ()
				{
					var presentation = PowerPointObject.Presentations.Open(filePath, WithWindow: MsoTriState.msoFalse);
					AppendSlide(presentation);
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

		public void AppendSlide(Presentation sourcePresentation,
			Presentation destinationPresentation = null)
		{
			if (destinationPresentation == null)
				destinationPresentation = GetActivePresentation();

			for (var i = 1; i <= sourcePresentation.Slides.Count; i++)
			{
				sourcePresentation.Slides[i].Copy();
				try
				{
					destinationPresentation.Application.CommandBars.ExecuteMso("PasteSourceFormatting");
				}
				catch { }
			}
			destinationPresentation.Slides[destinationPresentation.Slides.Count].Select();
		}

		public void PrintPresentation(string presentationPath, int currentSlideIndex, Action<Action> printActionWrapper)
		{
			using (var dlg = new PrintDialog
			{
				AllowCurrentPage = true,
				AllowPrintToFile = false,
				AllowSelection = false,
				AllowSomePages = true,
				ShowNetwork = true,
				UseEXDialog = true
			})
			{
				dlg.PrinterSettings.FromPage = currentSlideIndex;
				dlg.PrinterSettings.ToPage = currentSlideIndex;
				if (dlg.ShowDialog() != DialogResult.OK) return;

				var fromPage = dlg.PrinterSettings.FromPage;
				var toPage = 1;
				var collate = dlg.PrinterSettings.Collate;
				var copies = dlg.PrinterSettings.Copies;
				var printRange = dlg.PrinterSettings.PrintRange;
				var printerName = dlg.PrinterSettings.PrinterName;

				printActionWrapper(() =>
				{
					try
					{
						var presentation = PowerPointObject.Presentations.Open(presentationPath, WithWindow: MsoTriState.msoFalse);
						switch (printRange)
						{
							case System.Drawing.Printing.PrintRange.AllPages:
								fromPage = 1;
								toPage = presentation.Slides.Count;
								break;
							case System.Drawing.Printing.PrintRange.CurrentPage:
								fromPage = currentSlideIndex;
								toPage = currentSlideIndex;
								break;
							case System.Drawing.Printing.PrintRange.SomePages:
								if (fromPage < 1)
									fromPage = 1;
								toPage = currentSlideIndex;
								if (toPage > presentation.Slides.Count)
									toPage = presentation.Slides.Count;
								break;
						}
						presentation.PrintOptions.PrintInBackground = MsoTriState.msoFalse;
						presentation.PrintOptions.ActivePrinter = printerName;
						presentation.PrintOptions.NumberOfCopies = copies;
						presentation.PrintOut(
							fromPage,
							toPage,
							String.Empty,
							copies,
							collate ? MsoTriState.msoTrue : MsoTriState.msoFalse);
						presentation.Close();
						Utilities.ReleaseComObject(presentationPath);
					}
					catch { }
				});
			}
		}

		public SlideSettings GetSlideSettings()
		{
			try
			{
				var settings = new SlideSettings();
				if (PowerPointObject?.ActivePresentation == null) return null;
				settings.SizeWidth = PowerPointObject.ActivePresentation.PageSetup.SlideWidth / 72;
				settings.SizeHeght = PowerPointObject.ActivePresentation.PageSetup.SlideHeight / 72;
				switch (PowerPointObject.ActivePresentation.PageSetup.SlideOrientation)
				{
					case MsoOrientation.msoOrientationHorizontal:
						settings.Orientation = SlideOrientationEnum.Landscape;
						break;
					case MsoOrientation.msoOrientationVertical:
						settings.Orientation = SlideOrientationEnum.Portrait;
						break;
				}
				if (settings.SizeWidth == 10 && settings.SizeHeght == 5.625)
				{
					settings.SizeWidth = 13;
					settings.SizeHeght = 7.32;
				}
				return settings;
			}
			catch
			{
				return null;
			}
			finally
			{
			}
		}
	}
}
