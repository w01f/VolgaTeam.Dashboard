using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace TVScheduleBuilder.InteropClasses
{
    public partial class PowerPointHelper
    {
        private static PowerPointHelper instance = new PowerPointHelper();

        private PowerPoint.Application _powerPointObject;
        private PowerPoint.Presentation _activePresentation;
        private int _powerPointProcessId = 0;
        private bool _is2003 = false;

        private bool _isFirstLaunch = false;

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
            get
            {
                return _powerPointObject.WindowState == PowerPoint.PpWindowState.ppWindowMinimized;
            }
        }


        public bool Is2003
        {
            get
            {
                return _is2003;
            }
        }

        public bool Connect()
        {
            bool result = false;
            try
            {
                MessageFilter.Register();
                _powerPointObject =
                    System.Runtime.InteropServices.Marshal.GetActiveObject("PowerPoint.Application") as PowerPoint.Application;
                _is2003 = _powerPointObject.Version.Equals("11.0");

                uint lpdwProcessId = 0;
                WinAPIHelper.GetWindowThreadProcessId(new IntPtr(_powerPointObject.HWND), out lpdwProcessId);
                _powerPointProcessId = (int)lpdwProcessId;

                _powerPointObject.DisplayAlerts = Microsoft.Office.Interop.PowerPoint.PpAlertLevel.ppAlertsNone;

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
            if (_isFirstLaunch)
            {
                Close();
                _isFirstLaunch = false;
            }
            AppManager.ReleaseComObject(_powerPointObject);
            GC.Collect();
        }

        public void Close()
        {
            try
            {
                Process.GetProcessById(_powerPointProcessId).CloseMainWindow();
            }
            catch
            {
            }
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
                        PowerPoint.Presentations presentations = _powerPointObject.Presentations;
                        _activePresentation = presentations.Add(Microsoft.Office.Core.MsoTriState.msoCTrue);
                        AppManager.ReleaseComObject(presentations);
                        PowerPoint.Slides slides = _activePresentation.Slides;
                        slides.Add(1, Microsoft.Office.Interop.PowerPoint.PpSlideLayout.ppLayoutTitle);
                    }
                    else
                    {
                        PowerPoint.Presentations presentations = _powerPointObject.Presentations;
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
                PowerPoint.DocumentWindow activeWindow = _powerPointObject.ActiveWindow;
                if (activeWindow != null)
                {
                    PowerPoint.View view = activeWindow.View;
                    PowerPoint.Slide slide = (PowerPoint.Slide)view.Slide;
                    slideIndex = slide.SlideIndex;
                    AppManager.ReleaseComObject(slide);
                    AppManager.ReleaseComObject(view);
                }
                AppManager.ReleaseComObject(activeWindow);
            }
            catch
            {
            }
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
                    PowerPoint.Presentations presentations = _powerPointObject.Presentations;
                    PowerPoint.Presentation presentation = presentations.Add(Microsoft.Office.Core.MsoTriState.msoFalse);
                    AppManager.ReleaseComObject(presentations);
                    PowerPoint.Slides slides = presentation.Slides;

                    string[] previewImages = Directory.GetFiles(sourceFolderPathName, "*.png");
                    Array.Sort(previewImages, (x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x, y));

                    for (int i = 0; i < previewImages.Length; i++)
                    {
                        PowerPoint.Slide slide = slides.Add(i + 1, Microsoft.Office.Interop.PowerPoint.PpSlideLayout.ppLayoutBlank);
                        slide.Shapes.AddPicture(previewImages[i], Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 0, 0, presentation.SlideMaster.Width, presentation.SlideMaster.Height);
                        AppManager.ReleaseComObject(slide);
                    }
                    AppManager.ReleaseComObject(slides);
                    presentation.SaveAs(destinationFileName);
                    presentation.Close();
                    AppManager.ReleaseComObject(presentation);
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

        public void ConvertToPDF(string originalFileName, string pdfFileName)
        {
            try
            {
                MessageFilter.Register();
                if (_powerPointObject != null)
                {
                    PowerPoint.Presentation presentationObject = _powerPointObject.Presentations.Open(originalFileName, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoFalse);
                    presentationObject.SaveAs(pdfFileName, Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsPDF, Microsoft.Office.Core.MsoTriState.msoCTrue);
                    presentationObject.Close();
                    AppManager.ReleaseComObject(presentationObject);
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

        public void AppendSlide(PowerPoint.Presentation sourcePresentation, int slideIndex, PowerPoint.Presentation destinationPresentation = null)
        {
            PowerPoint.Slide slide;
            PowerPoint.SlideRange pastedRange;
            PowerPoint.Design design;
            int currentSlideIndex = 0;
            int indexToPaste;
            Microsoft.Office.Core.MsoTriState masterShape;

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
            PowerPoint.Slides slides = sourcePresentation.Slides;
            for (int i = 1; i <= slides.Count; i++)
            {
                if ((i == slideIndex) || (slideIndex == -1))
                {
                    slide = slides[i];
                    slide.Copy();
                    PowerPoint.Slides activeSlides = destinationPresentation.Slides;
                    pastedRange = activeSlides.Paste(indexToPaste);
                    indexToPaste++;
                    design = GetDesignFromSlide(slide, destinationPresentation);
                    if (design != null)
                    {
                        pastedRange.Design = design;
                    }
                    else
                    {
                        PowerPoint.Design slideDesign = sourcePresentation.SlideMaster.Design;
                        pastedRange.Design = slideDesign;
                        AppManager.ReleaseComObject(slideDesign);
                    }
                    PowerPoint.ColorScheme colorScheme = slide.ColorScheme;
                    pastedRange.ColorScheme = colorScheme;
                    AppManager.ReleaseComObject(colorScheme);

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
                    AppManager.ReleaseComObject(pastedRange);
                    AppManager.ReleaseComObject(design);
                    AppManager.ReleaseComObject(slide);
                    AppManager.ReleaseComObject(activeSlides);
                }
            }
            AppManager.ReleaseComObject(slides);
            MessageFilter.Revoke();
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
