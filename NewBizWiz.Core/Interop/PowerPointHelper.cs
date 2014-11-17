using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Win32;
using NewBizWiz.Core.Common;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace NewBizWiz.Core.Interop
{
	public interface IPowerPointHelper
	{
		Application PowerPointObject { get; }
		bool Foreground { get; }
		bool IsMinimized { get; }
		bool IsActive { get; }
		bool Is2003 { get; }
		string ActiveFileName { get; }
		string ActiveFilePath { get; }
		bool Connect(bool run = true);
		void Disconnect(bool closeIfFirstLaunch = true);
		void Close();
		bool PowerPointDetected();
		Presentation GetActivePresentation();
		int GetActiveSlideIndex();
		void CreateLockedPresentation(string sourceFolderPathName, string destinationFileName);
		void ConvertToPDF(string originalFileName, string pdfFileName);
		void AppendSlidesFromFile(string filePath, bool firstSlide);
		void AppendSlide(Presentation sourcePresentation, int slideIndex, Presentation destinationPresentation = null, bool firstSlide = false, int indexToPaste = 0);
		void AppendSlideMaster(string presentationTemplatePath, Presentation destinationPresentation = null);
		void SetPresentationSettings();
		void SavePDF(string fileName);
		void MergeFiles(string mergedFileName, string[] filesToMerge);
		void PreparePresentation(string fileName, Action<Presentation> buildPresentation, bool generateImages = true);
	}

	public class PowerPointHelper<T> : IPowerPointHelper where T : class,new()
	{
		protected static T _instance;

		protected Presentation _activePresentation;
		protected bool _containsPageNumbers = false;
		private bool _is2003;
		private bool _isFirstLaunch;
		protected Application _powerPointObject;
		private int _powerPointProcessId;
		private IntPtr _windowHandle = IntPtr.Zero;
		private int previouseSlideIndex;

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

		public bool IsMinimized
		{
			get
			{
				try
				{
					return WinAPIHelper.IsIconic(_windowHandle);
				}
				catch
				{
					return true;
				}
			}
		}

		public bool IsActive
		{
			get
			{
				try
				{
					return WinAPIHelper.IsWindowVisible(_windowHandle) || IsMinimized;
				}
				catch
				{
					return true;
				}
			}
		}

		public bool Is2003
		{
			get { return _is2003; }
		}

		public string ActiveFileName
		{
			get
			{
				try
				{
					return _powerPointObject.ActivePresentation.Name;
				}
				catch
				{
					return string.Empty;
				}
			}
		}

		public string ActiveFilePath
		{
			get
			{
				try
				{
					return _powerPointObject.ActivePresentation.FullName;
				}
				catch
				{
					return string.Empty;
				}
			}
		}

		public static string Version
		{
			get
			{
				string regKey = @"Software\Microsoft\Windows\CurrentVersion\App Paths";
				string key = "powerpnt.exe";
				string powerPointPath = string.Empty;

				//looks inside CURRENT_USER:
				RegistryKey mainKey = Registry.CurrentUser;
				try
				{
					mainKey = mainKey.OpenSubKey(regKey + @"\" + key, false);
					if (mainKey != null)
					{
						powerPointPath = mainKey.GetValue(string.Empty).ToString();
					}
				}
				catch { }

				//if not found, looks inside LOCAL_MACHINE:
				mainKey = Registry.LocalMachine;
				if (string.IsNullOrEmpty(powerPointPath))
				{
					try
					{
						mainKey = mainKey.OpenSubKey(regKey + @"\" + key, false);
						if (mainKey != null)
						{
							powerPointPath = mainKey.GetValue(string.Empty).ToString();
						}
					}
					catch { }
				}

				//closing the handle:
				if (mainKey != null)
					mainKey.Close();

				int majorVersion = 0;
				try
				{
					FileVersionInfo fileVersion = FileVersionInfo.GetVersionInfo(powerPointPath);
					majorVersion = fileVersion.FileMajorPart;
				}
				catch { }

				switch (majorVersion)
				{
					case 11:
						return "2003";
					case 12:
						return "2007";
					case 14:
						string bitness = string.Empty;
						try
						{
							bitness = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Office\14.0\Outlook", false).GetValue("Bitness").ToString();
						}
						catch { }
						return string.Format("2010{0}", bitness);
					default:
						return "Unknown";
				}
			}
		}

		public bool Connect(bool run = true)
		{
			bool result = false;
			try
			{
				MessageFilter.Register();
				try
				{
					if (_powerPointObject == null || run)
						_powerPointObject =
							Marshal.GetActiveObject("PowerPoint.Application") as Application;
				}
				catch
				{
					if (run)
						_powerPointObject = new Application();
					_powerPointObject.Visible = MsoTriState.msoCTrue;
					_isFirstLaunch = true;
				}
				_is2003 = _powerPointObject.Version.Equals("11.0");
				_windowHandle = new IntPtr(_powerPointObject.HWND);
				uint lpdwProcessId = 0;
				WinAPIHelper.GetWindowThreadProcessId(_windowHandle, out lpdwProcessId);
				_powerPointProcessId = (int)lpdwProcessId;
				_powerPointObject.DisplayAlerts = PpAlertLevel.ppAlertsNone;
				GetActivePresentation();
				SearchPageNumbers();
				result = true;
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

		public void Disconnect(bool closeIfFirstLaunch = true)
		{
			if (_isFirstLaunch && closeIfFirstLaunch)
			{
				Close();
				_isFirstLaunch = false;
			}
			Utilities.Instance.ReleaseComObject(_powerPointObject);
			_powerPointObject = null;
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

		public bool PowerPointDetected()
		{
			bool result = false;
			while (Process.GetProcesses().Any(x => x.ProcessName.Equals("POWERPNT")))
			{
				Application powerPoint = null;
				try
				{
					powerPoint = Marshal.GetActiveObject("PowerPoint.Application") as Application;
					if (powerPoint.Visible == MsoTriState.msoFalse)
					{
						uint lpdwProcessId = 0;
						WinAPIHelper.GetWindowThreadProcessId(new IntPtr(powerPoint.HWND), out lpdwProcessId);
						Process.GetProcessById((int)lpdwProcessId).Kill();
					}
					else
					{
						result = true;
						break;
					}
				}
				catch { }
				finally
				{
					Utilities.Instance.ReleaseComObject(powerPoint);
				}
			}
			if (!result)
				_powerPointObject = null;
			return result;
		}

		public Presentation GetActivePresentation()
		{
			try
			{
				_activePresentation = _powerPointObject.ActivePresentation;
			}
			catch
			{
				try
				{
					MessageFilter.Register();
					if (_powerPointObject.Presentations.Count == 0)
					{
						var presentations = _powerPointObject.Presentations;
						_activePresentation = presentations.Add(MsoTriState.msoCTrue);
						Utilities.Instance.ReleaseComObject(presentations);
						Slides slides = _activePresentation.Slides;
						slides.Add(1, PpSlideLayout.ppLayoutTitle);
						Utilities.Instance.ReleaseComObject(slides);
					}
					else
					{
						var presentations = _powerPointObject.Presentations;
						_activePresentation = presentations[1];
						Utilities.Instance.ReleaseComObject(presentations);
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
					Utilities.Instance.ReleaseComObject(slide);
					Utilities.Instance.ReleaseComObject(view);
				}
				Utilities.Instance.ReleaseComObject(activeWindow);
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
			return slideIndex;
		}

		public void CreateLockedPresentation(string sourceFolderPathName, string destinationFileName)
		{
			try
			{
				MessageFilter.Register();
				if (!Directory.Exists(sourceFolderPathName)) return;
				var presentations = _powerPointObject.Presentations;
				var presentation = presentations.Add(MsoTriState.msoFalse);
				Utilities.Instance.ReleaseComObject(presentations);
				var slides = presentation.Slides;

				string[] previewImages = Directory.GetFiles(sourceFolderPathName, "*.png");
				Array.Sort(previewImages, WinAPIHelper.StrCmpLogicalW);

				for (int i = 0; i < previewImages.Length; i++)
				{
					Slide slide = slides.Add(i + 1, PpSlideLayout.ppLayoutBlank);
					slide.Shapes.AddPicture(previewImages[i], MsoTriState.msoFalse, MsoTriState.msoCTrue, 0, 0, presentation.SlideMaster.Width, presentation.SlideMaster.Height);
					Utilities.Instance.ReleaseComObject(slide);
				}
				Utilities.Instance.ReleaseComObject(slides);
				presentation.SaveAs(destinationFileName);
				presentation.Close();
				Utilities.Instance.ReleaseComObject(presentation);
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
				if (_powerPointObject == null) return;
				Presentation presentationObject = _powerPointObject.Presentations.Open(originalFileName, MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoFalse);
				presentationObject.SaveAs(pdfFileName, PpSaveAsFileType.ppSaveAsPDF, MsoTriState.msoCTrue);
				presentationObject.Close();
				Utilities.Instance.ReleaseComObject(presentationObject);
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
				var thread = new Thread(delegate()
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

		public void AppendSlide(Presentation sourcePresentation, int slideIndex, Presentation destinationPresentation = null, bool firstSlide = false, int indexToPaste = 0)
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
			else
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
					Utilities.Instance.ReleaseComObject(slideDesign);
				}
				var colorScheme = slide.ColorScheme;
				addedSlide.ColorScheme = colorScheme;
				Utilities.Instance.ReleaseComObject(colorScheme);
				Utilities.Instance.ReleaseComObject(design);
				Utilities.Instance.ReleaseComObject(slide);
				Utilities.Instance.ReleaseComObject(activeSlides);
			}
			if (addedSlide != null)
				addedSlide.Select();
			Utilities.Instance.ReleaseComObject(addedSlide);
			Utilities.Instance.ReleaseComObject(slides);
			MessageFilter.Revoke();
			try
			{
				File.Delete(tempPresentationPath);
			}
			catch {}
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
				if (_powerPointObject.Windows.Count > 0)
				{
					_powerPointObject.Activate();
					if (_powerPointObject.ActiveWindow != null)
					{
						_powerPointObject.ActiveWindow.ViewType = PpViewType.ppViewNormal;
						activeSlide = (Slide)_powerPointObject.ActiveWindow.View.Slide;
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
				var thread = new Thread(delegate()
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

		public void SetPresentationSettings()
		{
			try
			{
				MessageFilter.Register();
				if (_powerPointObject == null) return;
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
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}

		private void SearchPageNumbers()
		{
			if (_activePresentation != null)
			{
				foreach (Slide slide in _activePresentation.Slides)
				{
					foreach (Shape shape in slide.Shapes)
					{
						for (int i = 1; i <= shape.Tags.Count && !_containsPageNumbers; i++)
						{
							switch (shape.Tags.Name(i))
							{
								case "NEWPAGENUMBER":
								case "PAGE_NUMBER":
									_containsPageNumbers = true;
									break;
							}
						}
						if (_containsPageNumbers)
							break;
					}
				}
			}
		}

		public void SavePDF(string fileName)
		{
			try
			{
				MessageFilter.Register();
				GetActivePresentation();
				if (_activePresentation != null)
					_activePresentation.SaveAs(fileName, PpSaveAsFileType.ppSaveAsPDF, MsoTriState.msoCTrue);
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
					Utilities.Instance.ReleaseComObject(presentation);
				}
			});
		}

		protected void SavePrevSlideIndex()
		{
			previouseSlideIndex = GetActiveSlideIndex();
		}

		protected void RestorePrevSlideIndex()
		{
			_powerPointObject.ActivePresentation.Slides[previouseSlideIndex].Select();
		}

		public void PreparePresentation(string fileName, Action<Presentation> buildPresentation, bool generateImages = true)
		{
			try
			{
				SavePrevSlideIndex();
				var presentations = _powerPointObject.Presentations;
				var presentation = presentations.Add(MsoTriState.msoFalse);
				presentation.PageSetup.SlideWidth = (float)SettingsManager.Instance.SizeWidth * 72;
				presentation.PageSetup.SlideHeight = (float)SettingsManager.Instance.SizeHeght * 72;
				switch (SettingsManager.Instance.Orientation)
				{
					case "Landscape":
						presentation.PageSetup.SlideOrientation = MsoOrientation.msoOrientationHorizontal;
						break;
					case "Portrait":
						presentation.PageSetup.SlideOrientation = MsoOrientation.msoOrientationVertical;
						break;
				}
				Utilities.Instance.ReleaseComObject(presentations);
				buildPresentation(presentation);
				MessageFilter.Register();
				var thread = new Thread(delegate()
				{
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
				});
				thread.Start();

				while (thread.IsAlive)
					System.Windows.Forms.Application.DoEvents();

				Utilities.Instance.ReleaseComObject(presentation);
				RestorePrevSlideIndex();
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
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