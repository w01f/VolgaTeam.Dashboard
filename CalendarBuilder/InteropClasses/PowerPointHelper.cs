using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using CalendarBuilder.ConfigurationClasses;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;

namespace CalendarBuilder.InteropClasses
{
	public partial class PowerPointHelper
	{
		private static readonly PowerPointHelper instance = new PowerPointHelper();

		private Presentation _activePresentation;
		private bool _is2003;
		private IntPtr _windowHandle = IntPtr.Zero;
		private Application _powerPointObject;
		private int _powerPointProcessId;

		private PowerPointHelper() { }

		public static PowerPointHelper Instance
		{
			get { return instance; }
		}

		public Application PowerPointObject
		{
			get { return _powerPointObject; }
		}

		public bool Foreground
		{
			get
			{
				if (_powerPointObject != null)
				{
					uint _powerPointProcessID = 0;
					WinAPIHelper.GetWindowThreadProcessId(new IntPtr(_powerPointObject.HWND), out _powerPointProcessID);

					uint _foregroundWindowProcessID = 0;
					WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out _foregroundWindowProcessID);

					return _foregroundWindowProcessID == _powerPointProcessID || _foregroundWindowProcessID == Process.GetCurrentProcess().Id;
				}
				return false;
			}
		}

		public bool Minimized
		{
			get { return _powerPointObject.WindowState == PpWindowState.ppWindowMinimized; }
		}

		public bool Is2003
		{
			get { return _is2003; }
		}

		public bool Connect()
		{
			bool result;
			try
			{
				MessageFilter.Register();
				try
				{
					_powerPointObject =
						Marshal.GetActiveObject("PowerPoint.Application") as Application;
				}
				catch
				{
					_powerPointObject = new Application();
					_powerPointObject.Visible = MsoTriState.msoCTrue;
				}
				_is2003 = _powerPointObject.Version.Equals("11.0");
				_windowHandle = new IntPtr(_powerPointObject.HWND);
				_powerPointObject.DisplayAlerts = PpAlertLevel.ppAlertsNone;
				GetActivePresentation();
				result = true;
			}
			catch
			{
				result = false;
			}
			finally
			{
				MessageFilter.Revoke();
			}
			return result;
		}

		public void Disconnect()
		{
			AppManager.ReleaseComObject(_powerPointObject);
			GC.Collect();
		}

		public void Close()
		{
			try
			{
				Process.GetProcessById(_powerPointProcessId).CloseMainWindow();
			}
			catch { }
		}

		public void GetActivePresentation()
		{
			try
			{
				MessageFilter.Register();
				_activePresentation = _powerPointObject.ActivePresentation;
			}
			catch
			{
				try
				{
					MessageFilter.Register();
					if (_powerPointObject.Presentations.Count == 0)
					{
						Presentations presentations = _powerPointObject.Presentations;
						_activePresentation = presentations.Add(MsoTriState.msoCTrue);
						AppManager.ReleaseComObject(presentations);
						Slides slides = _activePresentation.Slides;
						slides.Add(1, PpSlideLayout.ppLayoutTitle);
					}
					else
					{
						Presentations presentations = _powerPointObject.Presentations;
						_activePresentation = presentations[1];
						AppManager.ReleaseComObject(presentations);
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

		public int GetActiveSlideIndex()
		{
			int slideIndex = -1;
			try
			{
				MessageFilter.Register();
				_powerPointObject.Activate();
				DocumentWindow activeWindow = _powerPointObject.ActiveWindow;
				if (activeWindow != null)
				{
					View view = activeWindow.View;
					var slide = (Slide)view.Slide;
					slideIndex = slide.SlideIndex;
					AppManager.ReleaseComObject(slide);
					AppManager.ReleaseComObject(view);
				}
				AppManager.ReleaseComObject(activeWindow);
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
			return slideIndex + 1;
		}

		public void CreateLockedPresentation(string sourceFolderPathName, string destinationFileName)
		{
			try
			{
				MessageFilter.Register();
				if (Directory.Exists(sourceFolderPathName))
				{
					Presentations presentations = _powerPointObject.Presentations;
					Presentation presentation = presentations.Add(MsoTriState.msoFalse);
					AppManager.ReleaseComObject(presentations);
					Slides slides = presentation.Slides;

					string[] previewImages = Directory.GetFiles(sourceFolderPathName, "*.png");
					Array.Sort(previewImages, (x, y) => WinAPIHelper.StrCmpLogicalW(x, y));

					for (int i = 0; i < previewImages.Length; i++)
					{
						Slide slide = slides.Add(i + 1, PpSlideLayout.ppLayoutBlank);
						slide.Shapes.AddPicture(previewImages[i], MsoTriState.msoFalse, MsoTriState.msoCTrue, 0, 0, presentation.SlideMaster.Width, presentation.SlideMaster.Height);
						AppManager.ReleaseComObject(slide);
					}
					AppManager.ReleaseComObject(slides);
					presentation.SaveAs(destinationFileName);
					presentation.Close();
					AppManager.ReleaseComObject(presentation);
				}
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void ConvertToPDF(string originalFileName, string pdfFileName)
		{
			try
			{
				MessageFilter.Register();
				if (_powerPointObject != null)
				{
					Presentation presentationObject = _powerPointObject.Presentations.Open(originalFileName, MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoFalse);
					presentationObject.SaveAs(pdfFileName, PpSaveAsFileType.ppSaveAsPDF, MsoTriState.msoCTrue);
					presentationObject.Close();
					AppManager.ReleaseComObject(presentationObject);
				}
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void AppendSlidesFromFile(string filePath)
		{
			if (File.Exists(filePath))
			{
				try
				{
					var thread = new Thread(delegate()
												{
													MessageFilter.Register();
													Presentation presentation = _powerPointObject.Presentations.Open(FileName: filePath, WithWindow: MsoTriState.msoFalse);
													AppendSlide(presentation, -1);
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
		}

		public void AppendSlide(Presentation sourcePresentation, int slideIndex, Presentation destinationPresentation = null)
		{
			Slide slide;
			SlideRange pastedRange;
			Design design;
			int currentSlideIndex = 0;
			int indexToPaste;
			MsoTriState masterShape;

			MessageFilter.Register();
			if (destinationPresentation == null)
			{
				GetActivePresentation();
				destinationPresentation = _activePresentation;
				indexToPaste = GetActiveSlideIndex();
				if (indexToPaste == 0)
					indexToPaste = 1;
			}
			else
				indexToPaste = destinationPresentation.Slides.Count + 1;
			Slides slides = sourcePresentation.Slides;
			for (int i = 1; i <= slides.Count; i++)
			{
				if ((i == slideIndex) || (slideIndex == -1))
				{
					slide = slides[i];
					slide.Copy();
					Slides activeSlides = destinationPresentation.Slides;
					pastedRange = activeSlides.Paste(indexToPaste);
					indexToPaste++;
					design = GetDesignFromSlide(slide, destinationPresentation);
					if (design != null)
					{
						pastedRange.Design = design;
					}
					else
					{
						Design slideDesign = sourcePresentation.SlideMaster.Design;
						pastedRange.Design = slideDesign;
						AppManager.ReleaseComObject(slideDesign);
					}
					ColorScheme colorScheme = slide.ColorScheme;
					pastedRange.ColorScheme = colorScheme;
					AppManager.ReleaseComObject(colorScheme);

					if (slide.FollowMasterBackground == MsoTriState.msoFalse)
					{
						pastedRange.FollowMasterBackground = MsoTriState.msoFalse;
						pastedRange.Background.Fill.Visible = slide.Background.Fill.Visible;
						pastedRange.Background.Fill.ForeColor = slide.Background.Fill.ForeColor;
						pastedRange.Background.Fill.BackColor = slide.Background.Fill.BackColor;

						switch (slide.Background.Fill.Type)
						{
							case MsoFillType.msoFillTextured:
								switch (slide.Background.Fill.TextureType)
								{
									case MsoTextureType.msoTexturePreset:
										pastedRange.Background.Fill.PresetTextured(slide.Background.Fill.PresetTexture);
										break;
								}
								break;
							case MsoFillType.msoFillSolid:
								pastedRange.Background.Fill.Transparency = 0;
								pastedRange.Background.Fill.Solid();
								break;
							case MsoFillType.msoFillPicture:
								if (slide.Shapes.Count > 0)
									(slide.Shapes.Range(1)).Visible = MsoTriState.msoFalse;
								masterShape = slide.DisplayMasterShapes;
								slide.DisplayMasterShapes = MsoTriState.msoFalse;
								slide.Export(Path.Combine(Path.GetTempPath(), slide.SlideID + ".png"), "PNG", -1, -1);
								pastedRange.Background.Fill.UserPicture(Path.Combine(Path.GetTempPath(), slide.SlideID + ".png"));
								var file = new FileInfo(Path.Combine(Path.GetTempPath(), slide.SlideID + ".png"));
								if (file.Exists)
									file.Delete();
								slide.DisplayMasterShapes = masterShape;
								if (slide.Shapes.Count > 0)
									(slide.Shapes.Range(1)).Visible = MsoTriState.msoFalse;
								break;
							case MsoFillType.msoFillPatterned:
								pastedRange.Background.Fill.Patterned(slide.Background.Fill.Pattern);
								break;
							case MsoFillType.msoFillGradient:
								switch (slide.Background.Fill.GradientColorType)
								{
									case MsoGradientColorType.msoGradientTwoColors:
										pastedRange.Background.Fill.TwoColorGradient(slide.Background.Fill.GradientStyle, slide.Background.Fill.GradientVariant);
										break;
									case MsoGradientColorType.msoGradientPresetColors:
										pastedRange.Background.Fill.PresetGradient(slide.Background.Fill.GradientStyle, slide.Background.Fill.GradientVariant, slide.Background.Fill.PresetGradientType);
										break;
									case MsoGradientColorType.msoGradientOneColor:
										pastedRange.Background.Fill.OneColorGradient(slide.Background.Fill.GradientStyle, slide.Background.Fill.GradientVariant, slide.Background.Fill.GradientDegree);
										break;
								}
								break;
						}
					}
					MakeDesignUnique(slide, pastedRange.Design);
					activeSlides[indexToPaste - 1].Select();
					currentSlideIndex = indexToPaste - 1;
					AppManager.ReleaseComObject(pastedRange);
					AppManager.ReleaseComObject(design);
					AppManager.ReleaseComObject(slide);
					AppManager.ReleaseComObject(activeSlides);
				}
			}
			AppManager.ReleaseComObject(slides);
			MessageFilter.Revoke();
		}

		private Design GetDesignFromSlide(Slide slide, Presentation presentation)
		{
			foreach (Design design in presentation.Designs)
			{
				if (design.Name == slide.Design.Name)
					return design;
			}
			return null;
		}

		private void MakeDesignUnique(Slide slide, Design design)
		{
			while (!(design.SlideMaster.Shapes.Count <= slide.Design.SlideMaster.Shapes.Count))
			{
				if (design.SlideMaster.Shapes.Count > 0)
					design.SlideMaster.Shapes[design.SlideMaster.Shapes.Count].Delete();
				else
					break;
			}
		}

		public void SetPresentationSettings()
		{
			if (_powerPointObject != null)
			{
				if (_powerPointObject.ActivePresentation != null)
				{
					_powerPointObject.ActivePresentation.PageSetup.SlideWidth = (float)SettingsManager.Instance.SizeWidth * 72;
					_powerPointObject.ActivePresentation.PageSetup.SlideHeight = (float)SettingsManager.Instance.SizeHeght * 72;

					switch (SettingsManager.Instance.Orientation)
					{
						case "Landscape":
							_powerPointObject.ActivePresentation.PageSetup.SlideOrientation = MsoOrientation.msoOrientationHorizontal;
							break;
						case "Portrait":
							_powerPointObject.ActivePresentation.PageSetup.SlideOrientation = MsoOrientation.msoOrientationVertical;
							break;
					}
				}
			}
		}
	}

	public class MessageFilter : IOleMessageFilter
	{
		//
		// Class containing the IOleMessageFilter
		// thread error-handling functions.

		// Start the filter.

		//
		// IOleMessageFilter functions.
		// Handle incoming thread requests.

		#region IOleMessageFilter Members
		int IOleMessageFilter.HandleInComingCall(int dwCallType,
												 IntPtr hTaskCaller, int dwTickCount, IntPtr
																						  lpInterfaceInfo)
		{
			//Return the flag SERVERCALL_ISHANDLED.
			return 0;
		}

		// Thread call was rejected, so try again.
		int IOleMessageFilter.RetryRejectedCall(IntPtr
													hTaskCallee, int dwTickCount, int dwRejectType)
		{
			if (dwRejectType == 2)
			// flag = SERVERCALL_RETRYLATER.
			{
				// Retry the thread call immediately if return >=0 & 
				// <100.
				return 99;
			}
			// Too busy; cancel call.
			return -1;
		}

		int IOleMessageFilter.MessagePending(IntPtr hTaskCallee,
											 int dwTickCount, int dwPendingType)
		{
			//Return the flag PENDINGMSG_WAITDEFPROCESS.
			return 2;
		}
		#endregion

		public static void Register()
		{
			IOleMessageFilter newFilter = new MessageFilter();
			IOleMessageFilter oldFilter = null;
			CoRegisterMessageFilter(newFilter, out oldFilter);
		}

		// Done with the filter, close it.
		public static void Revoke()
		{
			IOleMessageFilter oldFilter = null;
			CoRegisterMessageFilter(null, out oldFilter);
		}

		// Implement the IOleMessageFilter interface.
		[DllImport("Ole32.dll")]
		private static extern int
			CoRegisterMessageFilter(IOleMessageFilter newFilter, out
				                                                     IOleMessageFilter oldFilter);
	}

	[ComImport, Guid("00000016-0000-0000-C000-000000000046"),
	 InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IOleMessageFilter
	{
		[PreserveSig]
		int HandleInComingCall(
			int dwCallType,
			IntPtr hTaskCaller,
			int dwTickCount,
			IntPtr lpInterfaceInfo);

		[PreserveSig]
		int RetryRejectedCall(
			IntPtr hTaskCallee,
			int dwTickCount,
			int dwRejectType);

		[PreserveSig]
		int MessagePending(
			IntPtr hTaskCallee,
			int dwTickCount,
			int dwPendingType);
	}
}