using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace AdScheduleBuilder.InteropClasses
{
    public partial class PowerPointHelper
    {
        public void AppendBasicOverview(OutputClasses.OutputControls.PublicationBasicOverviewControl outputControl, PowerPoint.Presentation destinationPresentation = null)
        {
            if (Directory.Exists(BusinessClasses.OutputManager.Instance.BasicOverviewTemlatesFolderPath))
            {
                string presentationTemplatePath = Path.Combine(BusinessClasses.OutputManager.Instance.BasicOverviewTemlatesFolderPath, string.Format(BusinessClasses.OutputManager.BasicOverviewSlideTemplate, outputControl.OutputFileIndex));
                if (File.Exists(presentationTemplatePath))
                {
                    try
                    {
                        System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                        {
                            MessageFilter.Register();
                            PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                            foreach (PowerPoint.Slide slide in presentation.Slides)
                            {
                                foreach (PowerPoint.Shape shape in slide.Shapes)
                                {
                                    for (int i = 1; i <= shape.Tags.Count; i++)
                                    {
                                        switch (shape.Tags.Name(i))
                                        {
                                            case "PLOGO":
                                                if (!string.IsNullOrEmpty(outputControl.LogoFile))
                                                    slide.Shapes.AddPicture(FileName: outputControl.LogoFile, LinkToFile: Microsoft.Office.Core.MsoTriState.msoFalse, SaveWithDocument: Microsoft.Office.Core.MsoTriState.msoCTrue, Left: shape.Left, Top: shape.Top, Width: shape.Width, Height: shape.Height);
                                                shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                break;
                                            case "PUBTAG":
                                                shape.TextFrame.TextRange.Text = outputControl.PresentationName1;
                                                break;
                                            case "DATETAG":
                                                shape.TextFrame.TextRange.Text = outputControl.PresentationDate1;
                                                break;
                                            case "PUBTAG2":
                                                shape.TextFrame.TextRange.Text = outputControl.PresentationName2;
                                                break;
                                            case "DATETAG2":
                                                shape.TextFrame.TextRange.Text = outputControl.PresentationDate2;
                                                break;
                                            case "ADVERTISER":
                                                shape.TextFrame.TextRange.Text = outputControl.BusinessName;
                                                break;
                                            case "DECISIONMAKER":
                                                shape.TextFrame.TextRange.Text = outputControl.DecisionMaker;
                                                break;
                                            case "HEADER":
                                                shape.TextFrame.TextRange.Text = outputControl.Header;
                                                break;
                                            case "FLTDT1":
                                                shape.TextFrame.TextRange.Text = outputControl.FlightDates1.Trim();
                                                break;
                                            case "FLTDT2":
                                                shape.TextFrame.TextRange.Text = outputControl.FlightDates2.Trim();
                                                break;
                                            case "RUNDATES":
                                                shape.TextFrame.TextRange.Text = outputControl.RunDates;
                                                break;
                                            default:
                                                for (int j = 0; j < 5; j++)
                                                {
                                                    if (shape.Tags.Name(i).Equals(string.Format("ADSPEC{0}", j + 1)))
                                                    {
                                                        if (j < outputControl.AdSpecs.Length)
                                                            shape.TextFrame.TextRange.Text = outputControl.AdSpecs[j];
                                                        else
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                    }
                                                }
                                                for (int j = 0; j < 2; j++)
                                                {
                                                    if (shape.Tags.Name(i).Equals(string.Format("TOTALADS{0}", j + 1)))
                                                    {
                                                        if (j < outputControl.AdSummaries.Length)
                                                            shape.TextFrame.TextRange.Text = outputControl.AdSummaries[j];
                                                        else
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                    }
                                                }
                                                for (int j = 0; j < 4; j++)
                                                {
                                                    if (shape.Tags.Name(i).Equals(string.Format("INVTAG{0}", j + 1)))
                                                    {
                                                        if (j < outputControl.InvestmentDetails.Length)
                                                            shape.TextFrame.TextRange.Text = outputControl.InvestmentDetails[j];
                                                        else
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                    }
                                                }

                                                break;
                                        }
                                    }
                                }
                            }
                            AppendSlide(presentation, -1, destinationPresentation);
                            presentation.Close();
                        }));
                        thread.Start();

                        while (thread.IsAlive)
                            System.Windows.Forms.Application.DoEvents();
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
        }

        public void PrepareBasicOverviewEmail(string fileName, OutputClasses.OutputControls.PublicationBasicOverviewControl[] outputControls)
        {
            try
            {
                PowerPoint.Presentations presentations = _powerPointObject.Presentations;
                PowerPoint.Presentation presentation = presentations.Add(Microsoft.Office.Core.MsoTriState.msoFalse);
                presentation.PageSetup.SlideWidth = (float)ConfigurationClasses.SettingsManager.Instance.SizeWidth * 72;
                presentation.PageSetup.SlideHeight = (float)ConfigurationClasses.SettingsManager.Instance.SizeHeght * 72;
                switch (ConfigurationClasses.SettingsManager.Instance.Orientation)
                {
                    case "Landscape":
                        presentation.PageSetup.SlideOrientation = Microsoft.Office.Core.MsoOrientation.msoOrientationHorizontal;
                        break;
                    case "Portrait":
                        presentation.PageSetup.SlideOrientation = Microsoft.Office.Core.MsoOrientation.msoOrientationVertical;
                        break;
                }
                AppManager.ReleaseComObject(presentations);
                foreach(OutputClasses.OutputControls.PublicationBasicOverviewControl outputControl in outputControls)
                    AppendBasicOverview(outputControl,presentation);
                MessageFilter.Register();
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                {
                    presentation.SaveAs(FileName: fileName);
                    string destinationFolder = fileName.Replace(Path.GetExtension(fileName),string.Empty);
                    if(!Directory.Exists(destinationFolder))
                        Directory.CreateDirectory(destinationFolder);
                    presentation.Export(Path: destinationFolder, FilterName: "PNG");
                    presentation.Close();
                }));
                thread.Start();

                while (thread.IsAlive)
                    System.Windows.Forms.Application.DoEvents();

                AppManager.ReleaseComObject(presentation);
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
}
