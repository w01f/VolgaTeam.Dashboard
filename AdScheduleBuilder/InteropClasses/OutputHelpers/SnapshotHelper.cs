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
        public void AppendSnapshot(PowerPoint.Presentation destinationPresentation = null)
        {
            if (Directory.Exists(BusinessClasses.OutputManager.Instance.SnapshotTemlatesFolderPath))
            {
                string presentationTemplatePath = Path.Combine(BusinessClasses.OutputManager.Instance.SnapshotTemlatesFolderPath, string.Format(BusinessClasses.OutputManager.SnapshotSlideTemplate, OutputClasses.OutputControls.OutputSnapshotControl.Instance.OutputFileIndex));
                if (File.Exists(presentationTemplatePath))
                {
                    try
                    {
                        System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                        {
                            MessageFilter.Register();

                            int publicationsCount = OutputClasses.OutputControls.OutputSnapshotControl.Instance.PublicationNames.Length;
                            for (int k = 0; k < publicationsCount; k += 6)
                            {
                                PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                                foreach (PowerPoint.Slide slide in presentation.Slides)
                                {
                                    foreach (PowerPoint.Shape shape in slide.Shapes)
                                    {
                                        for (int i = 1; i <= shape.Tags.Count; i++)
                                        {
                                            switch (shape.Tags.Name(i))
                                            {
                                                case "ADVERTISER":
                                                    shape.TextFrame.TextRange.Text = OutputClasses.OutputControls.OutputSnapshotControl.Instance.BusinessName;
                                                    break;
                                                case "DATETAG":
                                                    shape.TextFrame.TextRange.Text = OutputClasses.OutputControls.OutputSnapshotControl.Instance.Date;
                                                    break;
                                                case "DECISIONMAKER":
                                                    shape.TextFrame.TextRange.Text = OutputClasses.OutputControls.OutputSnapshotControl.Instance.DecisionMaker;
                                                    break;
                                                case "HEADER":
                                                    shape.TextFrame.TextRange.Text = OutputClasses.OutputControls.OutputSnapshotControl.Instance.Header;
                                                    break;
                                                case "FLTDT1":
                                                    shape.TextFrame.TextRange.Text = OutputClasses.OutputControls.OutputSnapshotControl.Instance.FlightDates;
                                                    break;
                                                default:
                                                    for (int j = 0; j < 6; j++)
                                                    {
                                                        if (shape.Tags.Name(i).Equals(string.Format("PUBLOGO{0}", AppManager.GetLetterByDigit(j + 1))))
                                                        {
                                                            if ((k + j) < OutputClasses.OutputControls.OutputSnapshotControl.Instance.LogoFiles.Length)
                                                                if (!string.IsNullOrEmpty(OutputClasses.OutputControls.OutputSnapshotControl.Instance.LogoFiles[k + j]))
                                                                    slide.Shapes.AddPicture(FileName: OutputClasses.OutputControls.OutputSnapshotControl.Instance.LogoFiles[k + j], LinkToFile: Microsoft.Office.Core.MsoTriState.msoFalse, SaveWithDocument: Microsoft.Office.Core.MsoTriState.msoCTrue, Left: shape.Left, Top: shape.Top, Width: shape.Width, Height: shape.Height);
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                        }
                                                        else if (shape.Tags.Name(i).Equals(string.Format("PUB{0}", AppManager.GetLetterByDigit(j + 1))))
                                                        {
                                                            if ((k + j) < OutputClasses.OutputControls.OutputSnapshotControl.Instance.PublicationNames.Length)
                                                            {
                                                                shape.TextFrame.TextRange.Text = OutputClasses.OutputControls.OutputSnapshotControl.Instance.PublicationNames[k + j];
                                                            }
                                                            else
                                                                shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                        }
                                                        else
                                                        {
                                                            for (int l = 0; l < 6; l++)
                                                            {
                                                                if (shape.Tags.Name(i).Equals(string.Format("ADSPEC{0}{1}", new object[] { (l + 1), AppManager.GetLetterByDigit(j + 1) })))
                                                                {
                                                                    if ((k + j) < OutputClasses.OutputControls.OutputSnapshotControl.Instance.AdSpecs.Length)
                                                                    {
                                                                        if (l < OutputClasses.OutputControls.OutputSnapshotControl.Instance.AdSpecs[k + j].Length)
                                                                            shape.TextFrame.TextRange.Text = OutputClasses.OutputControls.OutputSnapshotControl.Instance.AdSpecs[k + j][l];
                                                                        else
                                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                                    }
                                                                    else
                                                                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                }
                                AppendSlide(presentation, -1, destinationPresentation);
                                presentation.Close();
                            }
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
        public void PrepareSnapshotEmail(string fileName)
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
                AppendSnapshot(presentation);
                MessageFilter.Register();
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                {
                    presentation.SaveAs(FileName: fileName);
                    string destinationFolder = fileName.Replace(Path.GetExtension(fileName), string.Empty);
                    if (!Directory.Exists(destinationFolder))
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
