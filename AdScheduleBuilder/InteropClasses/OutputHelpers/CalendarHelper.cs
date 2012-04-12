using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Excel = Microsoft.Office.Interop.Excel;

namespace AdScheduleBuilder.InteropClasses
{
    public partial class PowerPointHelper
    {
        public void AppendCalendar(OutputClasses.OutputControls.MonthViewControl monthView, PowerPoint.Presentation destinationPresentation = null)
        {
            if (Directory.Exists(BusinessClasses.OutputManager.Instance.CalendarTemlatesFolderPath))
            {

                string presentationTemplatePath = Path.Combine(BusinessClasses.OutputManager.Instance.CalendarTemlatesFolderPath, monthView.SlideName);
                if (File.Exists(presentationTemplatePath))
                {
                    try
                    {
                        System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                        {
                            int daysCount = monthView.DayOutput.Count;
                            MessageFilter.Register();

                            ExcelHelper excelHelper = new ExcelHelper();

                            PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                            foreach (PowerPoint.Slide slide in presentation.Slides)
                            {
                                foreach (PowerPoint.Shape shape in slide.Shapes)
                                {
                                    if (shape.HasTextFrame == Microsoft.Office.Core.MsoTriState.msoTrue)
                                    {
                                        shape.TextFrame.TextRange.Font.Color.RGB = monthView.SlideRGB;
                                        shape.TextFrame.TextRange.Paragraphs().Font.Color.RGB = monthView.SlideRGB;
                                    }

                                    for (int i = 1; i <= shape.Tags.Count; i++)
                                    {
                                        switch (shape.Tags.Name(i))
                                        {
                                            case "LOGO":
                                                if (!string.IsNullOrEmpty(monthView.LogoFile))
                                                    slide.Shapes.AddPicture(FileName: monthView.LogoFile, LinkToFile: Microsoft.Office.Core.MsoTriState.msoFalse, SaveWithDocument: Microsoft.Office.Core.MsoTriState.msoCTrue, Left: shape.Left, Top: shape.Top, Width: shape.Width, Height: shape.Height);
                                                shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                break;
                                            case "HEADERTAG":
                                                shape.TextFrame.TextRange.Text = monthView.SlideTitle;
                                                break;
                                            case "PREPAREDFOR":
                                                if (string.IsNullOrEmpty(monthView.BusinessName) && string.IsNullOrEmpty(monthView.DecisionMaker))
                                                    shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                else
                                                    shape.Visible = Microsoft.Office.Core.MsoTriState.msoCTrue;
                                                break;
                                            case "ADVORDEC1":
                                                if (string.IsNullOrEmpty(monthView.BusinessName) && string.IsNullOrEmpty(monthView.DecisionMaker))
                                                    shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                else
                                                {
                                                    shape.Visible = Microsoft.Office.Core.MsoTriState.msoCTrue;
                                                    if (string.IsNullOrEmpty(monthView.BusinessName))
                                                        shape.TextFrame.TextRange.Text = monthView.DecisionMaker;
                                                    else
                                                        shape.TextFrame.TextRange.Text = monthView.BusinessName;
                                                }
                                                break;
                                            case "DECNAME2":
                                                if (string.IsNullOrEmpty(monthView.DecisionMaker))
                                                    shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                else
                                                {
                                                    shape.Visible = Microsoft.Office.Core.MsoTriState.msoCTrue;
                                                    shape.TextFrame.TextRange.Text = monthView.DecisionMaker;
                                                }
                                                break;
                                            case "MONTHYEAR":
                                                shape.TextFrame.TextRange.Text = monthView.MonthText;
                                                break;
                                            case "COMMENTS":
                                                shape.TextFrame.TextRange.Text = monthView.Comments;
                                                break;
                                            case "TAGA":
                                                shape.TextFrame.TextRange.Text = monthView.TagA;
                                                break;
                                            case "TAGB":
                                                shape.TextFrame.TextRange.Text = monthView.TagB;
                                                break;
                                            case "TAGC":
                                                shape.TextFrame.TextRange.Text = monthView.TagC;
                                                break;
                                            case "TAGD":
                                                shape.TextFrame.TextRange.Text = monthView.TagD;
                                                break;
                                            case "CODES":
                                                shape.TextFrame.TextRange.Text = monthView.Legend;
                                                break;
                                            case "1-1":
                                                if (daysCount > 0)
                                                    SetDayRecordTagValue(monthView, shape, 1);
                                                break;
                                            case "2-1":
                                                if (daysCount > 1)
                                                    SetDayRecordTagValue(monthView, shape, 2);
                                                break;
                                            case "3-1":
                                                if (daysCount > 2)
                                                    SetDayRecordTagValue(monthView, shape, 3);
                                                break;
                                            case "4-1":
                                                if (daysCount > 3)
                                                    SetDayRecordTagValue(monthView, shape, 4);
                                                break;
                                            case "5-1":
                                                if (daysCount > 4)
                                                    SetDayRecordTagValue(monthView, shape, 5);
                                                break;
                                            case "6-1":
                                                if (daysCount > 5)
                                                    SetDayRecordTagValue(monthView, shape, 6);
                                                break;
                                            case "7-1":
                                                if (daysCount > 6)
                                                    SetDayRecordTagValue(monthView, shape, 7);
                                                break;
                                            case "8-1":
                                                if (daysCount > 7)
                                                    SetDayRecordTagValue(monthView, shape, 8);
                                                break;
                                            case "9-1":
                                                if (daysCount > 8)
                                                    SetDayRecordTagValue(monthView, shape, 9);
                                                break;
                                            case "10-1":
                                                if (daysCount > 9)
                                                    SetDayRecordTagValue(monthView, shape, 10);
                                                break;
                                            case "11-1":
                                                if (daysCount > 10)
                                                    SetDayRecordTagValue(monthView, shape, 11);
                                                break;
                                            case "12-1":
                                                if (daysCount > 11)
                                                    SetDayRecordTagValue(monthView, shape, 12);
                                                break;
                                            case "13-1":
                                                if (daysCount > 12)
                                                    SetDayRecordTagValue(monthView, shape, 13);
                                                break;
                                            case "14-1":
                                                if (daysCount > 13)
                                                    SetDayRecordTagValue(monthView, shape, 14);
                                                break;
                                            case "15-1":
                                                if (daysCount > 14)
                                                    SetDayRecordTagValue(monthView, shape, 15);
                                                break;
                                            case "16-1":
                                                if (daysCount > 15)
                                                    SetDayRecordTagValue(monthView, shape, 16);
                                                break;
                                            case "17-1":
                                                if (daysCount > 16)
                                                    SetDayRecordTagValue(monthView, shape, 17);
                                                break;
                                            case "18-1":
                                                if (daysCount > 17)
                                                    SetDayRecordTagValue(monthView, shape, 18);
                                                break;
                                            case "19-1":
                                                if (daysCount > 18)
                                                    SetDayRecordTagValue(monthView, shape, 19);
                                                break;
                                            case "20-1":
                                                if (daysCount > 19)
                                                    SetDayRecordTagValue(monthView, shape, 20);
                                                break;
                                            case "21-1":
                                                if (daysCount > 20)
                                                    SetDayRecordTagValue(monthView, shape, 21);
                                                break;
                                            case "22-1":
                                                if (daysCount > 21)
                                                    SetDayRecordTagValue(monthView, shape, 22);
                                                break;
                                            case "23-1":
                                                if (daysCount > 22)
                                                    SetDayRecordTagValue(monthView, shape, 23);
                                                break;
                                            case "24-1":
                                                if (daysCount > 23)
                                                    SetDayRecordTagValue(monthView, shape, 24);
                                                break;
                                            case "25-1":
                                                if (daysCount > 24)
                                                    SetDayRecordTagValue(monthView, shape, 25);
                                                break;
                                            case "26-1":
                                                if (daysCount > 25)
                                                    SetDayRecordTagValue(monthView, shape, 26);
                                                break;
                                            case "27-1":
                                                if (daysCount > 26)
                                                    SetDayRecordTagValue(monthView, shape, 27);
                                                break;
                                            case "28-1":
                                                if (daysCount > 27)
                                                    SetDayRecordTagValue(monthView, shape, 28);
                                                break;
                                            case "29-1":
                                                if (daysCount > 28)
                                                    SetDayRecordTagValue(monthView, shape, 29);
                                                break;
                                            case "30-1":
                                                if (daysCount > 29)
                                                    SetDayRecordTagValue(monthView, shape, 30);
                                                break;
                                            case "31-1":
                                                if (daysCount > 30)
                                                    SetDayRecordTagValue(monthView, shape, 31);
                                                break;
                                        }
                                    }
                                }
                            }
                            if (excelHelper.Connect())
                            {
                                if (excelHelper.GetCalendarBackground(monthView))
                                {
                                    PowerPoint.ShapeRange shapeRange = presentation.SlideMaster.Shapes.PasteSpecial(PowerPoint.PpPasteDataType.ppPasteMetafilePicture, Microsoft.Office.Core.MsoTriState.msoFalse, "", 0, "", Microsoft.Office.Core.MsoTriState.msoFalse);
                                    // Resize selection
                                    shapeRange.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoFalse;
                                    shapeRange.Height = presentation.SlideMaster.Height;
                                    shapeRange.Width = presentation.SlideMaster.Width;
                                    shapeRange.Top = 0;
                                    shapeRange.Left = 0;

                                }
                                excelHelper.Disconnect();
                            }

                            presentation.SlideMaster.Design.Name = monthView.SlideMasterName;
                            AppendSlide(presentation, -1, destinationPresentation);
                            presentation.Close();
                        }));
                        thread.Start();
                        while (thread.IsAlive)
                            Application.DoEvents();
                    }
                    finally
                    {
                        MessageFilter.Revoke();
                    }
                }
            }
        }

        private void SetDayRecordTagValue(OutputClasses.OutputControls.MonthViewControl monthView, PowerPoint.Shape shape, int dayNumber)
        {
            try
            {
                if (!string.IsNullOrEmpty(monthView.DayOutput[dayNumber - 1].RecordsText))
                {
                    shape.TextFrame.TextRange.Text = monthView.DayOutput[dayNumber - 1].RecordsText;
                    shape.TextFrame.TextRange.Font.Size = monthView.DayOutput[dayNumber - 1].FontSize;
                }
                else
                    shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
            }
            catch
            {
            }
        }

        public void PrepareCalendarEmail(string fileName, OutputClasses.OutputControls.MonthViewControl[] outputControls)
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
                foreach (OutputClasses.OutputControls.MonthViewControl outputControl in outputControls)
                {
                    outputControl.PrepareOutput();
                    AppendCalendar(outputControl, presentation);
                }
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

    public partial class ExcelHelper
    {
        public bool GetCalendarBackground(OutputClasses.OutputControls.MonthViewControl monthView)
        {
            bool result = false;
            try
            {
                Excel.Workbook workBook = _excelObject.Workbooks.Open(Path.Combine(BusinessClasses.OutputManager.Instance.CalendarBackgroundFolderPath, string.Format(BusinessClasses.OutputManager.ExcelBackgroundFileName, new object[] { monthView.SlideColor, monthView.Month.ToString("yyyy") })));
                workBook.Sheets[monthView.BackgroundSheetName].Range["A1:N37"].Copy();
                result = true;
            }
            catch
            {
            }
            return result;
        }
    }
}


