using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.RemoteStorage;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Application = Microsoft.Office.Interop.PowerPoint.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace Asa.Common.Core.OfficeInterops
{
	public abstract class PowerPointProcessor
	{
		protected bool _isFirstLaunch;
		private Application _powerPointObject;
		protected Presentation _activePresentation;
		private int _previouseSlideIndex;

		public Application PowerPointObject
		{
			get
			{
				if (!IsLinkedWithApplication)
					Connect();
				return _powerPointObject;
			}
		}

		public IntPtr WindowHandle => _powerPointObject != null ? new IntPtr(_powerPointObject.HWND) : IntPtr.Zero;

		public bool IsLinkedWithApplication
		{
			get
			{
				if (!Process.GetProcessesByName("POWERPNT").Any())
				{
					_powerPointObject = null;
					return false;
				}
				try
				{
					if (_powerPointObject == null)
						_powerPointObject = GetExistedPowerPoint();
					return _powerPointObject.Caption != "";
				}
				catch
				{
					_powerPointObject = null;
					return false;
				}
			}
		}

		public virtual bool Connect(bool forceNewObject = false)
		{
			try
			{
				MessageFilter.Register();
				_powerPointObject = GetExistedPowerPoint();
				if (forceNewObject && _powerPointObject == null)
				{
					_powerPointObject = CreateNewPowerPoint();
					_isFirstLaunch = true;
				}
				_powerPointObject.DisplayAlerts = PpAlertLevel.ppAlertsNone;
			}
			catch
			{
				_powerPointObject = null;
			}
			finally
			{
				MessageFilter.Revoke();
			}
			return _powerPointObject != null;
		}

		private Application CreateNewPowerPoint()
		{
			return new Application();
		}

		private Application GetExistedPowerPoint()
		{
			try
			{
				return Marshal.GetActiveObject("PowerPoint.Application") as Application;
			}
			catch
			{
				return null;
			}
		}

		public void Disconnect()
		{
			Utilities.ReleaseComObject(_powerPointObject);
			_powerPointObject = null;
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		protected void Close()
		{
			try
			{
				_powerPointObject.Quit();
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
			try
			{
				_activePresentation = _powerPointObject.ActivePresentation;
			}
			catch
			{
				_activePresentation = null;
				if (create)
				{
					try
					{
						MessageFilter.Register();
						if (_powerPointObject.Presentations.Count == 0)
						{
							var presentations = _powerPointObject.Presentations;
							_activePresentation = presentations.Add(MsoTriState.msoCTrue);
							Utilities.ReleaseComObject(presentations);
							Slides slides = _activePresentation.Slides;
							slides.Add(1, PpSlideLayout.ppLayoutTitle);
							Utilities.ReleaseComObject(slides);
						}
						else
						{
							var presentations = _powerPointObject.Presentations;
							_activePresentation = presentations[1];
							Utilities.ReleaseComObject(presentations);
						}
					}
					catch
					{
						_activePresentation = null;
					}
					finally
					{
						MessageFilter.Revoke();
					}
				}
			}
			return _activePresentation;
		}

		public int GetActiveSlideIndex()
		{
			var slideIndex = -1;
			try
			{
				MessageFilter.Register();
				_powerPointObject.Activate();
				var activeWindow = _powerPointObject.ActiveWindow;
				if (activeWindow != null)
				{
					var view = activeWindow.View;
					var slide = (Slide)view.Slide;
					slideIndex = slide.SlideIndex;
					Utilities.ReleaseComObject(slide);
					Utilities.ReleaseComObject(view);
				}
				Utilities.ReleaseComObject(activeWindow);
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
			return slideIndex;
		}

		public void ConvertToPDF(string originalFileName, string pdfFileName)
		{
			try
			{
				MessageFilter.Register();
				if (_powerPointObject == null) return;
				Presentation presentationObject = _powerPointObject.Presentations.Open(originalFileName, MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoFalse);
				presentationObject.SaveAs(pdfFileName, PpSaveAsFileType.ppSaveAsPDF, MsoTriState.msoCTrue);
				presentationObject.Close();
				Utilities.ReleaseComObject(presentationObject);
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void AppendSlidesFromFile(string filePath, bool firstSlide)
		{
			if (!File.Exists(filePath)) return;
			try
			{
				var thread = new Thread(delegate ()
				{
					MessageFilter.Register();
					var presentation = _powerPointObject.Presentations.Open(filePath, WithWindow: MsoTriState.msoFalse);
					AppendSlide(presentation, -1, null, firstSlide);
					presentation.Close();
				});
				thread.Start();

				while (thread.IsAlive)
					System.Windows.Forms.Application.DoEvents();
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void AppendSlide(Presentation sourcePresentation,
			int slideIndex,
			Presentation destinationPresentation = null,
			bool firstSlide = false,
			int indexToPaste = 0)
		{
			MessageFilter.Register();

			var tempPresentationPath = Path.GetTempFileName();
			sourcePresentation.SaveAs(tempPresentationPath);

			if (destinationPresentation == null)
			{
				GetActivePresentation();
				destinationPresentation = _activePresentation;
				indexToPaste = GetActiveSlideIndex();
			}
			else if (!firstSlide)
				indexToPaste = destinationPresentation.Slides.Count;

			if (firstSlide)
				indexToPaste = 0;

			Slide addedSlide = null;
			var slides = sourcePresentation.Slides;
			for (var i = 1; i <= slides.Count; i++)
			{
				if ((i != slideIndex) && (slideIndex != -1)) continue;
				var slide = slides[i];
				var activeSlides = destinationPresentation.Slides;
				activeSlides.InsertFromFile(tempPresentationPath, indexToPaste, i, i);
				indexToPaste++;
				addedSlide = activeSlides[indexToPaste];
				var design = GetDesignFromSlide(slide, destinationPresentation);
				if (design != null)
					addedSlide.Design = design;
				else
				{
					var slideDesign = sourcePresentation.SlideMaster.Design;
					addedSlide.Design = slideDesign;
					Utilities.ReleaseComObject(slideDesign);
				}
				var colorScheme = slide.ColorScheme;
				addedSlide.ColorScheme = colorScheme;
				Utilities.ReleaseComObject(colorScheme);
				Utilities.ReleaseComObject(design);
				Utilities.ReleaseComObject(slide);
				Utilities.ReleaseComObject(activeSlides);
			}
			if (addedSlide != null)
				addedSlide.Select();
			Utilities.ReleaseComObject(addedSlide);
			Utilities.ReleaseComObject(slides);
			MessageFilter.Revoke();
			try
			{
				File.Delete(tempPresentationPath);
			}
			catch { }
		}

		private Design GetDesignFromSlide(Slide slide, Presentation presentation)
		{
			return presentation.Designs.Cast<Design>().FirstOrDefault(design => design.Name == slide.Design.Name);
		}

		public void AppendSlideMaster(string presentationTemplatePath, Presentation destinationPresentation = null)
		{
			try
			{
				var thread = new Thread(delegate ()
				{
					MessageFilter.Register();
					var presentation = _powerPointObject.Presentations.Open(presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
					AppendSlide(presentation, -1, destinationPresentation);
					presentation.Close();
				});
				thread.Start();
				while (thread.IsAlive)
					System.Windows.Forms.Application.DoEvents();
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public SlideSettings GetActiveSlideSettings()
		{
			try
			{
				MessageFilter.Register();
				var settings = new SlideSettings();
				if (_powerPointObject?.ActivePresentation == null) return null;
				settings.SlideSize.Width = Math.Round(Convert.ToDecimal(_powerPointObject.ActivePresentation.PageSetup.SlideWidth / 72), 3);
				settings.SlideSize.Height = Math.Round(Convert.ToDecimal(_powerPointObject.ActivePresentation.PageSetup.SlideHeight / 72), 3);
				return settings;
			}
			catch
			{
				return null;
			}
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void SetSlideSettings(SlideSettings settings)
		{
			if (_powerPointObject?.ActivePresentation == null) return;
			SetSlideSettings(_powerPointObject.ActivePresentation, settings);
		}

		private void SetSlideSettings(Presentation presentation, SlideSettings settings)
		{
			try
			{
				MessageFilter.Register();
				presentation.PageSetup.SlideWidth = (float)settings.SlideSize.Width * 72;
				presentation.PageSetup.SlideHeight = (float)settings.SlideSize.Height * 72;

				switch (settings.SlideSize.Orientation)
				{
					case SlideOrientationEnum.Landscape:
						presentation.PageSetup.SlideOrientation = MsoOrientation.msoOrientationHorizontal;
						break;
					case SlideOrientationEnum.Portrait:
						presentation.PageSetup.SlideOrientation = MsoOrientation.msoOrientationVertical;
						break;
				}
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void MergeFiles(string mergedFileName, string[] filesToMerge)
		{
			PreparePresentation(mergedFileName, mergedPresentation =>
			{
				foreach (var file in filesToMerge)
				{
					var presentation = _powerPointObject.Presentations.Open(file, WithWindow: MsoTriState.msoFalse);
					AppendSlide(presentation, -1, mergedPresentation);
					presentation.Close();
					Utilities.ReleaseComObject(presentation);
				}
			});
		}

		protected void SavePrevSlideIndex()
		{
			_previouseSlideIndex = GetActiveSlideIndex();
		}

		protected void RestorePrevSlideIndex()
		{
			_powerPointObject.ActivePresentation.Slides[_previouseSlideIndex].Select();
		}

		public void PreparePresentation(string fileName, Action<Presentation> buildPresentation, bool generateImages = true)
		{
			try
			{
				var thread = new Thread(delegate ()
				{
					SavePrevSlideIndex();

					var presentation = _powerPointObject.Presentations.Open(SlideSettingsManager.Instance.GetLauncherTemplatePath(), MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoFalse);
					if (presentation.Slides.Count > 0)
						presentation.Slides[1].Delete();
					buildPresentation(presentation);
					MessageFilter.Register();
					presentation.SaveAs(fileName, PpSaveAsFileType.ppSaveAsOpenXMLPresentation, MsoTriState.msoCTrue);
					if (generateImages)
					{
						var destinationFolder = fileName.Replace(Path.GetExtension(fileName), string.Empty);
						if (!Directory.Exists(destinationFolder))
							Directory.CreateDirectory(destinationFolder);
						if (presentation.Slides.Count > 0)
							presentation.Export(destinationFolder, "PNG");
					}
					presentation.Close();
					Utilities.ReleaseComObject(presentation);
					RestorePrevSlideIndex();
				});
				thread.Start();
				while (thread.IsAlive)
					System.Windows.Forms.Application.DoEvents();
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void BuildPdf(string sourceFileName, string targetFileName)
		{
			BuildPdf(targetFileName, new[] { sourceFileName });
		}

		public void BuildPdf(string targetFileName, IEnumerable<string> presentationFiles)
		{
			try
			{
				var thread = new Thread(delegate ()
				{
					MessageFilter.Register();
					if (presentationFiles == null || !presentationFiles.Any()) return;
					var sourceFileName = String.Empty;
					if (presentationFiles.Count() > 1)
					{
						sourceFileName = Path.GetTempFileName();
						SavePrevSlideIndex();
						var presentations = _powerPointObject.Presentations;
						var presentation = presentations.Add(MsoTriState.msoFalse);
						SetSlideSettings(presentation, SlideSettingsManager.Instance.SlideSettings);
						var slideIndex = 0;
						foreach (var presentationFile in presentationFiles)
						{
							var sourcePresentation = _powerPointObject.Presentations.Open(presentationFile, WithWindow: MsoTriState.msoFalse);
							AppendSlide(sourcePresentation, -1, presentation, slideIndex == 0, slideIndex);
							sourcePresentation.Close();
							slideIndex++;
						}
						presentation.SaveAs(sourceFileName);
						Utilities.ReleaseComObject(presentation);
						Utilities.ReleaseComObject(presentations);
						RestorePrevSlideIndex();
					}
					else
						sourceFileName = presentationFiles.FirstOrDefault();
					if (!String.IsNullOrEmpty(sourceFileName))
						ConvertToPDF(sourceFileName, targetFileName);
				});
				thread.Start();
				while (thread.IsAlive)
					System.Windows.Forms.Application.DoEvents();
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void BuildPdf(string targetFileName, Action<Presentation> buildPresentation)
		{
			try
			{
				var thread = new Thread(delegate ()
				{
					SavePrevSlideIndex();

					var presentation = _powerPointObject.Presentations.Open(SlideSettingsManager.Instance.GetLauncherTemplatePath(), MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoFalse);
					if (presentation.Slides.Count > 0)
						presentation.Slides[1].Delete();
					buildPresentation(presentation);

					var sourceFileName = Path.GetTempFileName();
					presentation.SaveAs(sourceFileName);
					Utilities.ReleaseComObject(presentation);
					presentation.Close();

					ConvertToPDF(sourceFileName, targetFileName);

					Utilities.ReleaseComObject(presentation);

					RestorePrevSlideIndex();
				});
				thread.Start();
				while (thread.IsAlive)
					System.Windows.Forms.Application.DoEvents();
			}
			catch { }
		}

		public void FillContractInfo(Slide slide, ContractSettings contractSettings, StorageDirectory folder)
		{
			var templateFile = new StorageFile(folder.RelativePathParts.Merge(contractSettings.TemplateName));
			var templatePresentation = _powerPointObject.Presentations.Open(templateFile.LocalPath, WithWindow: MsoTriState.msoFalse);
			foreach (var shape in GetContractInfoShapes(contractSettings, templatePresentation))
			{
				shape.Copy();
				slide.Shapes.Paste();
			}
			templatePresentation.Close();
		}

		public void FillContractInfo(Design design, ContractSettings contractSettings, StorageDirectory folder)
		{
			var templateFile = new StorageFile(folder.RelativePathParts.Merge(contractSettings.TemplateName));
			var templatePresentation = _powerPointObject.Presentations.Open(Path.Combine(templateFile.LocalPath, contractSettings.TemplateName), WithWindow: MsoTriState.msoFalse);
			foreach (var shape in GetContractInfoShapes(contractSettings, templatePresentation))
			{
				shape.Copy();
				design.SlideMaster.Shapes.Paste();
			}
			templatePresentation.Close();
		}

		private IEnumerable<Shape> GetContractInfoShapes(ContractSettings contractSettings, Presentation templatePresentation)
		{
			var shapes = new List<Shape>();
			foreach (Slide slide in templatePresentation.Slides)
			{
				foreach (Shape shape in slide.Shapes)
				{
					for (var i = 1; i <= shape.Tags.Count; i++)
					{
						var addShape = false;
						var tagName = shape.Tags.Name(i);
						if ((tagName == "APPROVAL1" || tagName == "SIGLINE1") && contractSettings.ShowSignatureLine)
							addShape = true;
						else if (tagName == "RATESEXPIRE" && contractSettings.RateExpirationDate.HasValue)
						{
							shape.TextFrame.TextRange.Text = String.Format("{0} {1}", shape.TextFrame.TextRange.Text, contractSettings.RateExpirationDate.Value.ToString("MM/dd/yy"));
							addShape = true;
						}
						else if (tagName == "DISCLAMIER" && contractSettings.ShowDisclaimer)
							addShape = true;

						if (addShape)
							shapes.Add(shape);
					}
				}
			}
			return shapes;
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
						MessageFilter.Register();
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
					finally
					{
						MessageFilter.Revoke();
					}
				});
			}
		}
	}
}
