using System;
using System.IO;
using System.Runtime.InteropServices;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace CustomSlidesAddIn.InteropClasses
{
    public class PowerPointHelper
    {
        private static PowerPointHelper instance = new PowerPointHelper();

        private PowerPoint.Application _powerPointObject = null;
        private PowerPoint.Presentation _activePresentation = null;

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

        public void Connect(PowerPoint.Application powerPointObject)
        {
            _powerPointObject = powerPointObject;
        }

        public void Disconnect()
        {
            BusinessClasses.CommonMethods.ReleaseComObject(_activePresentation);
            BusinessClasses.CommonMethods.ReleaseComObject(_powerPointObject);
        }

        public void GetActivePresentation()
        {
            _activePresentation = _powerPointObject.ActivePresentation;
        }

        public void SetPresentationSettings()
        {
            try
            {
                MessageFilter.Register();
                GetActivePresentation();
                if (_powerPointObject != null)
                {
                    if (_activePresentation != null && ConfigurationClasses.SettingsManager.Instance.ChangeSizeAutomatically)
                    {
                        _activePresentation.PageSetup.SlideWidth = (float)ConfigurationClasses.SettingsManager.Instance.SizeWidth * 72;
                        _activePresentation.PageSetup.SlideHeight = (float)ConfigurationClasses.SettingsManager.Instance.SizeHeght * 72;

                        switch (ConfigurationClasses.SettingsManager.Instance.Orientation)
                        {
                            case "Landscape":
                                _activePresentation.PageSetup.SlideOrientation = Microsoft.Office.Core.MsoOrientation.msoOrientationHorizontal;
                                break;
                            case "Portrait":
                                _activePresentation.PageSetup.SlideOrientation = Microsoft.Office.Core.MsoOrientation.msoOrientationVertical;
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

        public void AppendSlide(PowerPoint.Presentation sourcePresentation, int slideIndex, bool firstSlide = false, int indexToPaste = 0)
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
                            BusinessClasses.CommonMethods.ReleaseComObject(slideDesign);
                        }
                        PowerPoint.ColorScheme colorScheme = slide.ColorScheme;
                        pastedRange.ColorScheme = colorScheme;
                        BusinessClasses.CommonMethods.ReleaseComObject(colorScheme);

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
                        BusinessClasses.CommonMethods.ReleaseComObject(pastedRange);
                        BusinessClasses.CommonMethods.ReleaseComObject(design);
                        BusinessClasses.CommonMethods.ReleaseComObject(slide);
                        BusinessClasses.CommonMethods.ReleaseComObject(activeSlides);
                    }
                }
            }
            BusinessClasses.CommonMethods.ReleaseComObject(slides);
        }

        public void AppendCustomSlide(string slidePath)
        {
            if (File.Exists(slidePath))
            {
                try
                {
                    using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                    {
                        form.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
                        form.TopMost = true;
                        System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                        {
                            MessageFilter.Register();
                            PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: slidePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                            AppendSlide(presentation, -1);
                            presentation.Close();
                            BusinessClasses.CommonMethods.ReleaseComObject(presentation);
                        }));

                        form.Show();
                        System.Windows.Forms.Application.DoEvents();

                        thread.Start();
                        while (thread.IsAlive)
                            System.Windows.Forms.Application.DoEvents();

                        form.Close();
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
                    BusinessClasses.CommonMethods.ReleaseComObject(slide);
                    BusinessClasses.CommonMethods.ReleaseComObject(view);
                }
                BusinessClasses.CommonMethods.ReleaseComObject(activeWindow);
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
