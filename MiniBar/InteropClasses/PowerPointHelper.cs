using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace MiniBar.InteropClasses
{
    public partial class PowerPointHelper
    {
        private static PowerPointHelper instance = new PowerPointHelper();

        private PowerPoint.Application _powerPointObject = null;
        private int _powerPointProcessId = 0;
        private PowerPoint.Presentation _activePresentation;
        private IntPtr _windowHandle = IntPtr.Zero;
        private bool _is2003 = false;

        private PowerPointHelper()
        {
        }

        public static PowerPointHelper Instance
        {
            get
            {
                return instance;
            }
        }

        public PowerPoint.Application PowerPointObject
        {
            get
            {
                return _powerPointObject;
            }
        }

        public bool IsMinimized
        {
            get
            {
                return WinAPIHelper.IsIconic(_windowHandle);
            }
        }

        public bool IsActive
        {
            get
            {
                return WinAPIHelper.IsWindowVisible(_windowHandle);
            }
        }

        public bool Is2003
        {
            get
            {
                return _is2003;
            }
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


        public bool Connect(bool start)
        {
            bool result = false;
            _containsPageNumbers = false;
            try
            {
                MessageFilter.Register();
                try
                {
                    if (_powerPointObject == null)
                    {
                        _powerPointObject =
                            System.Runtime.InteropServices.Marshal.GetActiveObject("PowerPoint.Application") as PowerPoint.Application;
                    }
                }
                catch
                {
                    if (start)
                    {
                        _powerPointObject = new Microsoft.Office.Interop.PowerPoint.Application();
                        _powerPointObject.Visible = Microsoft.Office.Core.MsoTriState.msoCTrue;
                    }
                }
                _is2003 = _powerPointObject.Version.Equals("11.0");
                uint lpdwProcessId = 0;
                WinAPIHelper.GetWindowThreadProcessId(new IntPtr(_powerPointObject.HWND), out lpdwProcessId);
                _powerPointProcessId = (int)lpdwProcessId;
                _windowHandle = new IntPtr(_powerPointObject.HWND);
                _powerPointObject.DisplayAlerts = Microsoft.Office.Interop.PowerPoint.PpAlertLevel.ppAlertsNone;
                GetActivePresentation();
                SearchPageNumbers();
                _powerPointObject.PresentationClose += new PowerPoint.EApplication_PresentationCloseEventHandler(_powerPointObject_PresentationClose);
                result = true;
            }
            catch
            {
                _powerPointObject = null;
                result = false;
            }
            finally
            {
                MessageFilter.Revoke();
            }
            return result;
        }

        private void _powerPointObject_PresentationClose(PowerPoint.Presentation Pres)
        {
            try
            {
                MessageFilter.Register();
                _powerPointObject.Quit();
            }
            catch
            {
                _activePresentation = null;
            }
            finally
            {
                MessageFilter.Revoke();
                AppManager.Instance.ReleaseComObject(_activePresentation);
                AppManager.Instance.ReleaseComObject(_powerPointObject);
                _powerPointObject = null;
                System.Threading.Thread.Sleep(500);
            }
        }

        public void ActivatePowerPoint()
        {
            if (_powerPointObject != null)
            {
                IntPtr powerPointHandle = new IntPtr(_powerPointObject.HWND);
                InteropClasses.WinAPIHelper.ShowWindow(powerPointHandle, InteropClasses.WindowShowStyle.ShowMaximized);
            }
        }

        public bool PowerPointDetected()
        {
            bool result = false;
            while (Process.GetProcesses().Where(x => x.ProcessName.Equals("POWERPNT")).Count() > 0)
            {
                PowerPoint.Application powerPoint = null;
                try
                {
                    powerPoint = System.Runtime.InteropServices.Marshal.GetActiveObject("PowerPoint.Application") as PowerPoint.Application;
                    if (powerPoint.Visible == Microsoft.Office.Core.MsoTriState.msoFalse)
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
                catch
                {
                }
                finally
                {
                    AppManager.Instance.ReleaseComObject(powerPoint);
                }
            }
            return result;
        }

        public void GetActivePresentation()
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
                        PowerPoint.Presentations presentations = _powerPointObject.Presentations;
                        _activePresentation = presentations.Add(Microsoft.Office.Core.MsoTriState.msoCTrue);
                        AppManager.Instance.ReleaseComObject(presentations);
                        if (ConfigurationClasses.SettingsManager.Instance.SlideTemplateEnabled && File.Exists(BusinessClasses.MasterWizardManager.Instance.SelectedWizard.CleanslateFile))
                        {
                            AppendCleanslate();
                        }
                        else
                        {
                            PowerPoint.Slides slides = _activePresentation.Slides;
                            slides.Add(1, Microsoft.Office.Interop.PowerPoint.PpSlideLayout.ppLayoutTitle);
                            AppManager.Instance.ReleaseComObject(slides);
                        }
                    }
                    else
                    {
                        PowerPoint.Presentations presentations = _powerPointObject.Presentations;
                        _activePresentation = presentations[1];
                        AppManager.Instance.ReleaseComObject(presentations);
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

        public void SetPresentationSettings()
        {
            try
            {
                MessageFilter.Register();
                if (_powerPointObject != null)
                {
                    if (_powerPointObject.ActivePresentation != null)
                    {
                        _powerPointObject.ActivePresentation.PageSetup.SlideWidth = (float)ConfigurationClasses.SettingsManager.Instance.SizeWidth * 72;
                        _powerPointObject.ActivePresentation.PageSetup.SlideHeight = (float)ConfigurationClasses.SettingsManager.Instance.SizeHeght * 72;

                        switch (ConfigurationClasses.SettingsManager.Instance.Orientation)
                        {
                            case "Landscape":
                                _powerPointObject.ActivePresentation.PageSetup.SlideOrientation = Microsoft.Office.Core.MsoOrientation.msoOrientationHorizontal;
                                break;
                            case "Portrait":
                                _powerPointObject.ActivePresentation.PageSetup.SlideOrientation = Microsoft.Office.Core.MsoOrientation.msoOrientationVertical;
                                break;
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                MessageFilter.Revoke();
            }
        }

        public void AppendSlide(PowerPoint.Presentation sourcePresentation, int slideIndex, bool firstSlide = false,int indexToPaste =0)
        {
            PowerPoint.Slide slide;
            PowerPoint.SlideRange pastedRange;
            PowerPoint.Design design;
            int currentSlideIndex = 0;
            Microsoft.Office.Core.MsoTriState masterShape;


            GetActivePresentation();
            if (indexToPaste == 0)
                indexToPaste = GetActiveSlideIndex();
            if (firstSlide || indexToPaste == 0)
                indexToPaste = 1;

            PowerPoint.Slides slides = sourcePresentation.Slides;
            for (int i = 1; i <= slides.Count; i++)
            {
                if ((i == slideIndex) || (slideIndex == -1))
                {
                    if (indexToPaste >= 0)
                    {
                        slide = slides[i];
                        slide.Copy();
                        PowerPoint.Slides activeSlides = _activePresentation.Slides;
                        pastedRange = activeSlides.Paste(indexToPaste);
                        indexToPaste++;
                        design = GetDesignFromSlide(slide, _activePresentation);
                        if (design != null)
                            pastedRange.Design = design;
                        else
                        {
                            PowerPoint.Design slideDesign = slide.Design;
                            pastedRange.Design = slideDesign;
                            AppManager.Instance.ReleaseComObject(slideDesign);
                        }
                        PowerPoint.ColorScheme colorScheme = slide.ColorScheme;
                        pastedRange.ColorScheme = colorScheme;
                        AppManager.Instance.ReleaseComObject(colorScheme);

                        if (slide.FollowMasterBackground == Microsoft.Office.Core.MsoTriState.msoFalse)
                        {
                            pastedRange.FollowMasterBackground = Microsoft.Office.Core.MsoTriState.msoFalse;
                            pastedRange.Background.Fill.Visible = slide.Background.Fill.Visible;
                            pastedRange.Background.Fill.ForeColor = slide.Background.Fill.ForeColor;
                            pastedRange.Background.Fill.BackColor = slide.Background.Fill.BackColor;

                            switch (slide.Background.Fill.Type)
                            {
                                case Microsoft.Office.Core.MsoFillType.msoFillTextured:
                                    switch (slide.Background.Fill.TextureType)
                                    {
                                        case Microsoft.Office.Core.MsoTextureType.msoTexturePreset:
                                            pastedRange.Background.Fill.PresetTextured(slide.Background.Fill.PresetTexture);
                                            break;
                                    }
                                    break;
                                case Microsoft.Office.Core.MsoFillType.msoFillSolid:
                                    pastedRange.Background.Fill.Transparency = 0;
                                    pastedRange.Background.Fill.Solid();
                                    break;
                                case Microsoft.Office.Core.MsoFillType.msoFillPicture:
                                    if (slide.Shapes.Count > 0)
                                        (slide.Shapes.Range(1)).Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                    masterShape = slide.DisplayMasterShapes;
                                    slide.DisplayMasterShapes = Microsoft.Office.Core.MsoTriState.msoFalse;
                                    slide.Export(Path.Combine(Path.GetTempPath(), slide.SlideID + ".png"), "PNG", -1, -1);
                                    pastedRange.Background.Fill.UserPicture(Path.Combine(Path.GetTempPath(), slide.SlideID + ".png"));
                                    FileInfo file = new FileInfo(Path.Combine(Path.GetTempPath(), slide.SlideID + ".png"));
                                    if (file.Exists)
                                        file.Delete();
                                    slide.DisplayMasterShapes = masterShape;
                                    if (slide.Shapes.Count > 0)
                                        (slide.Shapes.Range(1)).Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                    break;
                                case Microsoft.Office.Core.MsoFillType.msoFillPatterned:
                                    pastedRange.Background.Fill.Patterned(slide.Background.Fill.Pattern);
                                    break;
                                case Microsoft.Office.Core.MsoFillType.msoFillGradient:
                                    switch (slide.Background.Fill.GradientColorType)
                                    {
                                        case Microsoft.Office.Core.MsoGradientColorType.msoGradientTwoColors:
                                            pastedRange.Background.Fill.TwoColorGradient(slide.Background.Fill.GradientStyle, slide.Background.Fill.GradientVariant);
                                            break;
                                        case Microsoft.Office.Core.MsoGradientColorType.msoGradientPresetColors:
                                            pastedRange.Background.Fill.PresetGradient(slide.Background.Fill.GradientStyle, slide.Background.Fill.GradientVariant, slide.Background.Fill.PresetGradientType);
                                            break;
                                        case Microsoft.Office.Core.MsoGradientColorType.msoGradientOneColor:
                                            pastedRange.Background.Fill.OneColorGradient(slide.Background.Fill.GradientStyle, slide.Background.Fill.GradientVariant, slide.Background.Fill.GradientDegree);
                                            break;
                                    }
                                    break;
                            }
                        }
                        MakeDesignUnique(slide, pastedRange.Design);
                        activeSlides[indexToPaste - 1].Select();
                        currentSlideIndex = indexToPaste - 1;
                        AppManager.Instance.ReleaseComObject(pastedRange);
                        AppManager.Instance.ReleaseComObject(design);
                        AppManager.Instance.ReleaseComObject(slide);
                        AppManager.Instance.ReleaseComObject(activeSlides);
                    }
                }
            }
            AppManager.Instance.ReleaseComObject(slides);
        }

        private PowerPoint.Slide GetActiveSlide()
        {
            PowerPoint.Slide activeSlide = null;
            try
            {
                if (_powerPointObject.Windows.Count > 0)
                {
                    _powerPointObject.Activate();
                    if (_powerPointObject.ActiveWindow != null)
                    {
                        _powerPointObject.ActiveWindow.ViewType = Microsoft.Office.Interop.PowerPoint.PpViewType.ppViewNormal;
                        activeSlide = (PowerPoint.Slide)_powerPointObject.ActiveWindow.View.Slide;
                    }
                }
            }
            catch
            {
            }
            finally
            {
            }
            return activeSlide;
        }

        private int GetActiveSlideIndex()
        {
            int slideIndex = -1;
            try
            {
                _powerPointObject.Activate();
                PowerPoint.DocumentWindow activeWindow = _powerPointObject.ActiveWindow;
                if (activeWindow != null)
                {
                    PowerPoint.View view = activeWindow.View;
                    PowerPoint.Slide slide = (PowerPoint.Slide)view.Slide;
                    slideIndex = slide.SlideIndex;
                    AppManager.Instance.ReleaseComObject(slide);
                    AppManager.Instance.ReleaseComObject(view);
                }
                AppManager.Instance.ReleaseComObject(activeWindow);
            }
            catch
            {
            }
            finally
            {
            }
            return slideIndex + 1;
        }

        private PowerPoint.Design GetDesignFromSlide(PowerPoint.Slide slide, PowerPoint.Presentation presentation)
        {
            foreach (PowerPoint.Design design in presentation.Designs)
            {
                if (design.Name == slide.Design.Name)
                    return design;
            }
            return null;
        }

        private void MakeDesignUnique(PowerPoint.Slide slide, PowerPoint.Design design)
        {
            while (!(design.SlideMaster.Shapes.Count <= slide.Design.SlideMaster.Shapes.Count))
            {
                if (design.SlideMaster.Shapes.Count > 0)
                    design.SlideMaster.Shapes[design.SlideMaster.Shapes.Count].Delete();
                else
                    break;
            }
        }

        public void SavePDF(string fileName)
        {
            try
            {
                MessageFilter.Register();
                GetActivePresentation();
                if (_activePresentation != null)
                    _activePresentation.SaveAs(fileName, Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsPDF, Microsoft.Office.Core.MsoTriState.msoCTrue);
            }
            catch
            {
            }
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

        //
        // IOleMessageFilter functions.
        // Handle incoming thread requests.
        int IOleMessageFilter.HandleInComingCall(int dwCallType,
          System.IntPtr hTaskCaller, int dwTickCount, System.IntPtr
          lpInterfaceInfo)
        {
            //Return the flag SERVERCALL_ISHANDLED.
            return 0;
        }

        // Thread call was rejected, so try again.
        int IOleMessageFilter.RetryRejectedCall(System.IntPtr
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

        int IOleMessageFilter.MessagePending(System.IntPtr hTaskCallee,
          int dwTickCount, int dwPendingType)
        {
            //Return the flag PENDINGMSG_WAITDEFPROCESS.
            return 2;
        }

        // Implement the IOleMessageFilter interface.
        [DllImport("Ole32.dll")]
        private static extern int
          CoRegisterMessageFilter(IOleMessageFilter newFilter, out 
          IOleMessageFilter oldFilter);
    }

    [ComImport(), Guid("00000016-0000-0000-C000-000000000046"),
    InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    interface IOleMessageFilter
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




