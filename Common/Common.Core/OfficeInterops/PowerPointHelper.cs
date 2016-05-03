﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.RemoteStorage;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace Asa.Common.Core.OfficeInterops
{
	public interface IPowerPointHelper
	{
		Application PowerPointObject { get; }
		bool IsLinkedWithApplication { get; }
		bool Connect(bool run = true);
		void Disconnect();
		Presentation GetActivePresentation(bool create = true);
		int GetActiveSlideIndex();
		void ConvertToPDF(string originalFileName, string pdfFileName);
		void AppendSlidesFromFile(string filePath, bool firstSlide);
		void AppendSlide(Presentation sourcePresentation, int slideIndex, Presentation destinationPresentation = null, bool firstSlide = false, int indexToPaste = 0);
		void AppendSlideMaster(string presentationTemplatePath, Presentation destinationPresentation = null);
		SlideSettings GetSlideSettings();
		void SetSlideSettings(SlideSettings settings);
		void SavePDF(string fileName);
		void MergeFiles(string mergedFileName, string[] filesToMerge);
		void PreparePresentation(string fileName, Action<Presentation> buildPresentation, bool generateImages = true);
		void BuildPdf(string sourceFileName, string targetFileName);
		void BuildPdf(string targetFileName, IEnumerable<string> presentationFiles);
	}

	public class PowerPointHelper<T> : IPowerPointHelper where T : class, new()
	{
		protected static T _instance;

		protected Presentation _activePresentation;
		private Application _powerPointObject;
		private int _previouseSlideIndex;

		protected PowerPointHelper() { }

		public static T Instance
		{
			get
			{
				if (_instance == null)
					_instance = new T();
				return _instance;
			}
		}

		public Application PowerPointObject
		{
			get
			{
				if (!IsLinkedWithApplication)
					Connect(false);
				return _powerPointObject;
			}
		}

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
						Connect(false);
					return _powerPointObject.Caption != "";
				}
				catch
				{
					_powerPointObject = null;
					return false;
				}
			}
		}

		public bool Connect(bool run = true)
		{
			bool result;
			try
			{
				MessageFilter.Register();
				try
				{
					_powerPointObject = Marshal.GetActiveObject("PowerPoint.Application") as Application;
				}
				catch
				{
					if (run)
					{
						_powerPointObject = new Application();
						_powerPointObject.Visible = MsoTriState.msoCTrue;
					}
				}
				_powerPointObject.DisplayAlerts = PpAlertLevel.ppAlertsNone;
				result = GetActivePresentation(run) != null;
			}
			catch
			{
				result = false;
				_powerPointObject = null;
			}
			finally
			{
				MessageFilter.Revoke();
			}
			return result;
		}

		public void Disconnect()
		{
			Utilities.ReleaseComObject(_powerPointObject);
			_powerPointObject = null;
			GC.Collect();
		}

		public Presentation GetActivePresentation(bool create = true)
		{
			try
			{
				_activePresentation = PowerPointObject.ActivePresentation;
			}
			catch
			{
				_activePresentation = null;
				if (create)
				{
					try
					{
						MessageFilter.Register();
						if (PowerPointObject.Presentations.Count == 0)
						{
							var presentations = PowerPointObject.Presentations;
							_activePresentation = presentations.Add(MsoTriState.msoCTrue);
							Utilities.ReleaseComObject(presentations);
							Slides slides = _activePresentation.Slides;
							slides.Add(1, PpSlideLayout.ppLayoutTitle);
							Utilities.ReleaseComObject(slides);
						}
						else
						{
							var presentations = PowerPointObject.Presentations;
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
				PowerPointObject.Activate();
				var activeWindow = PowerPointObject.ActiveWindow;
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
				if (PowerPointObject == null) return;
				Presentation presentationObject = PowerPointObject.Presentations.Open(originalFileName, MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoFalse);
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
					var presentation = PowerPointObject.Presentations.Open(filePath, WithWindow: MsoTriState.msoFalse);
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

		protected Slide GetActiveSlide()
		{
			Slide activeSlide = null;
			try
			{
				if (PowerPointObject.Windows.Count > 0)
				{
					PowerPointObject.Activate();
					if (PowerPointObject.ActiveWindow != null)
					{
						PowerPointObject.ActiveWindow.ViewType = PpViewType.ppViewNormal;
						activeSlide = (Slide)PowerPointObject.ActiveWindow.View.Slide;
					}
				}
			}
			catch { }
			return activeSlide;
		}

		public void AppendSlideMaster(string presentationTemplatePath, Presentation destinationPresentation = null)
		{
			try
			{
				var thread = new Thread(delegate ()
				{
					MessageFilter.Register();
					var presentation = PowerPointObject.Presentations.Open(presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
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

		public SlideSettings GetSlideSettings()
		{
			try
			{
				MessageFilter.Register();
				var settings = new SlideSettings();
				if (PowerPointObject?.ActivePresentation == null) return null;
				settings.SlideSize.Width = Math.Round(Convert.ToDecimal(PowerPointObject.ActivePresentation.PageSetup.SlideWidth / 72), 3);
				settings.SlideSize.Height = Math.Round(Convert.ToDecimal(PowerPointObject.ActivePresentation.PageSetup.SlideHeight / 72), 3);
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
			if (PowerPointObject?.ActivePresentation == null) return;
			SetSlideSettings(PowerPointObject.ActivePresentation, settings);
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

		public void SavePDF(string fileName)
		{
			try
			{
				MessageFilter.Register();
				GetActivePresentation();
				_activePresentation?.SaveAs(fileName, PpSaveAsFileType.ppSaveAsPDF, MsoTriState.msoCTrue);
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
					var presentation = PowerPointObject.Presentations.Open(file, WithWindow: MsoTriState.msoFalse);
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
			PowerPointObject.ActivePresentation.Slides[_previouseSlideIndex].Select();
		}

		public void PreparePresentation(string fileName, Action<Presentation> buildPresentation, bool generateImages = true)
		{
			try
			{
				var thread = new Thread(delegate ()
				{
					SavePrevSlideIndex();

					var presentation = PowerPointObject.Presentations.Open(PowerPointManager.Instance.GetLauncherTemplatePath(), MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoFalse);
					if (presentation.Slides.Count > 0)
						presentation.Slides[1].Delete();
					buildPresentation(presentation);
					MessageFilter.Register();
					presentation.SaveAs(fileName);
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
						var presentations = PowerPointObject.Presentations;
						var presentation = presentations.Add(MsoTriState.msoFalse);
						SetSlideSettings(presentation, PowerPointManager.Instance.SlideSettings);
						var slideIndex = 0;
						foreach (var presentationFile in presentationFiles)
						{
							var sourcePresentation = PowerPointObject.Presentations.Open(presentationFile, WithWindow: MsoTriState.msoFalse);
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

		protected void FillContractInfo(Slide slide, ContractSettings contractSettings, StorageDirectory folder)
		{
			var templateFile = new StorageFile(folder.RelativePathParts.Merge(contractSettings.TemplateName));
			var templatePresentation = PowerPointObject.Presentations.Open(templateFile.LocalPath, WithWindow: MsoTriState.msoFalse);
			foreach (var shape in GetContractInfoShapes(contractSettings, templatePresentation))
			{
				shape.Copy();
				slide.Shapes.Paste();
			}
			templatePresentation.Close();
		}

		protected void FillContractInfo(Design design, ContractSettings contractSettings, StorageDirectory folder)
		{
			var templateFile = new StorageFile(folder.RelativePathParts.Merge(contractSettings.TemplateName));
			var templatePresentation = PowerPointObject.Presentations.Open(Path.Combine(templateFile.LocalPath, contractSettings.TemplateName), WithWindow: MsoTriState.msoFalse);
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
	}
}
