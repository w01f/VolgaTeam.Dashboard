using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Excel = Microsoft.Office.Interop.Excel;

namespace CalendarBuilder.InteropClasses
{
    public partial class PowerPointHelper
    {
        public void AppendCalendar(BusinessClasses.CalendarOutputData monthOutputData, PowerPoint.Presentation destinationPresentation = null)
        {
            if (Directory.Exists(BusinessClasses.OutputManager.Instance.CalendarTemlatesFolderPath))
            {
                string presentationTemplatePath = Path.Combine(BusinessClasses.OutputManager.Instance.CalendarTemlatesFolderPath, monthOutputData.SlideName);
                if (File.Exists(presentationTemplatePath))
                {
                    try
                    {
                        System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                        {
                            monthOutputData.PrepareDayLogoPaths();
                            monthOutputData.PrepareNotes();
                            int daysCount = monthOutputData.DayOutput.Length;
                            MessageFilter.Register();

                            ExcelHelper excelHelper = new ExcelHelper();

                            PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                            foreach (PowerPoint.Slide slide in presentation.Slides)
                            {
                                foreach (PowerPoint.Shape shape in slide.Shapes)
                                {
                                    if (shape.HasTextFrame == Microsoft.Office.Core.MsoTriState.msoTrue)
                                    {
                                        shape.TextFrame.TextRange.Font.Color.RGB = monthOutputData.SlideRGB;
                                        shape.TextFrame.TextRange.Paragraphs().Font.Color.RGB = monthOutputData.SlideRGB;
                                    }

                                    for (int i = 1; i <= shape.Tags.Count; i++)
                                    {
                                        switch (shape.Tags.Name(i))
                                        {
                                            case "LOGO":
                                                if (!string.IsNullOrEmpty(monthOutputData.LogoFile))
                                                    slide.Shapes.AddPicture(FileName: monthOutputData.LogoFile, LinkToFile: Microsoft.Office.Core.MsoTriState.msoFalse, SaveWithDocument: Microsoft.Office.Core.MsoTriState.msoCTrue, Left: shape.Left, Top: shape.Top, Width: shape.Width, Height: shape.Height);
                                                shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                break;
                                            case "HEADERTAG":
                                                shape.TextFrame.TextRange.Text = monthOutputData.SlideTitle;
                                                break;
                                            case "PREPAREDFOR":
                                                if (string.IsNullOrEmpty(monthOutputData.BusinessName) && string.IsNullOrEmpty(monthOutputData.DecisionMaker))
                                                    shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                else
                                                    shape.Visible = Microsoft.Office.Core.MsoTriState.msoCTrue;
                                                break;
                                            case "ADVORDEC1":
                                                if (string.IsNullOrEmpty(monthOutputData.BusinessName) && string.IsNullOrEmpty(monthOutputData.DecisionMaker))
                                                    shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                else
                                                {
                                                    shape.Visible = Microsoft.Office.Core.MsoTriState.msoCTrue;
                                                    if (string.IsNullOrEmpty(monthOutputData.BusinessName))
                                                        shape.TextFrame.TextRange.Text = monthOutputData.DecisionMaker;
                                                    else
                                                        shape.TextFrame.TextRange.Text = monthOutputData.BusinessName;
                                                }
                                                break;
                                            case "DECNAME2":
                                                if (string.IsNullOrEmpty(monthOutputData.DecisionMaker))
                                                    shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                else
                                                {
                                                    shape.Visible = Microsoft.Office.Core.MsoTriState.msoCTrue;
                                                    shape.TextFrame.TextRange.Text = monthOutputData.DecisionMaker;
                                                }
                                                break;
                                            case "MONTHYEAR":
                                                shape.TextFrame.TextRange.Text = monthOutputData.MonthText;
                                                break;
                                            case "COMMENTS":
                                                shape.TextFrame.TextRange.Text = monthOutputData.Comments;
                                                break;
                                            case "TAGA":
                                                shape.TextFrame.TextRange.Text = monthOutputData.TagA;
                                                break;
                                            case "TAGB":
                                                shape.TextFrame.TextRange.Text = monthOutputData.TagB;
                                                break;
                                            case "TAGC":
                                                shape.TextFrame.TextRange.Text = monthOutputData.TagC;
                                                break;
                                            case "TAGD":
                                                shape.TextFrame.TextRange.Text = monthOutputData.TagD;
                                                break;
                                            case "CODES":
                                                shape.TextFrame.TextRange.Text = monthOutputData.LegendString;
                                                break;
                                            case "1-1":
                                                if (daysCount > 0)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 1);
                                                break;
                                            case "2-1":
                                                if (daysCount > 1)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 2);
                                                break;
                                            case "3-1":
                                                if (daysCount > 2)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 3);
                                                break;
                                            case "4-1":
                                                if (daysCount > 3)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 4);
                                                break;
                                            case "5-1":
                                                if (daysCount > 4)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 5);
                                                break;
                                            case "6-1":
                                                if (daysCount > 5)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 6);
                                                break;
                                            case "7-1":
                                                if (daysCount > 6)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 7);
                                                break;
                                            case "8-1":
                                                if (daysCount > 7)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 8);
                                                break;
                                            case "9-1":
                                                if (daysCount > 8)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 9);
                                                break;
                                            case "10-1":
                                                if (daysCount > 9)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 10);
                                                break;
                                            case "11-1":
                                                if (daysCount > 10)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 11);
                                                break;
                                            case "12-1":
                                                if (daysCount > 11)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 12);
                                                break;
                                            case "13-1":
                                                if (daysCount > 12)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 13);
                                                break;
                                            case "14-1":
                                                if (daysCount > 13)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 14);
                                                break;
                                            case "15-1":
                                                if (daysCount > 14)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 15);
                                                break;
                                            case "16-1":
                                                if (daysCount > 15)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 16);
                                                break;
                                            case "17-1":
                                                if (daysCount > 16)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 17);
                                                break;
                                            case "18-1":
                                                if (daysCount > 17)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 18);
                                                break;
                                            case "19-1":
                                                if (daysCount > 18)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 19);
                                                break;
                                            case "20-1":
                                                if (daysCount > 19)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 20);
                                                break;
                                            case "21-1":
                                                if (daysCount > 20)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 21);
                                                break;
                                            case "22-1":
                                                if (daysCount > 21)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 22);
                                                break;
                                            case "23-1":
                                                if (daysCount > 22)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 23);
                                                break;
                                            case "24-1":
                                                if (daysCount > 23)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 24);
                                                break;
                                            case "25-1":
                                                if (daysCount > 24)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 25);
                                                break;
                                            case "26-1":
                                                if (daysCount > 25)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 26);
                                                break;
                                            case "27-1":
                                                if (daysCount > 26)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 27);
                                                break;
                                            case "28-1":
                                                if (daysCount > 27)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 28);
                                                break;
                                            case "29-1":
                                                if (daysCount > 28)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 29);
                                                break;
                                            case "30-1":
                                                if (daysCount > 29)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 30);
                                                break;
                                            case "31-1":
                                                if (daysCount > 30)
                                                    SetDayRecordTagValue(monthOutputData, slide, shape, 31);
                                                break;
                                        }
                                    }
                                }
                                foreach (BusinessClasses.CalendarNote note in monthOutputData.Notes)
                                {
                                    PowerPoint.Shape noteShape = slide.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, note.Left, note.Top, note.Right - note.Left, note.Height);
                                    noteShape.Fill.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                    noteShape.Fill.Solid();
                                    noteShape.Fill.ForeColor.RGB = Microsoft.VisualBasic.Information.RGB(note.BackgroundColor.R, note.BackgroundColor.G, note.BackgroundColor.B);
                                    noteShape.Fill.Transparency = 0;

                                    noteShape.Line.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                    noteShape.Line.ForeColor.SchemeColor = PowerPoint.PpColorSchemeIndex.ppForeground;
                                    noteShape.Line.BackColor.RGB = Microsoft.VisualBasic.Information.RGB(0, 0, 0); ;

                                    noteShape.Shadow.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                    noteShape.Shadow.Type = Microsoft.Office.Core.MsoShadowType.msoShadow14;

                                    noteShape.TextFrame.TextRange.Font.Color.RGB = Microsoft.VisualBasic.Information.RGB(note.ForeColor.R, note.ForeColor.G, note.ForeColor.B);
                                    noteShape.TextFrame.TextRange.Font.Name = "Calibri";
                                    noteShape.TextFrame.TextRange.Font.Size = 8;
                                    noteShape.TextFrame.TextRange.Text = note.Note;
                                }

                            }
                            if (excelHelper.Connect())
                            {
                                if (excelHelper.GetCalendarBackground(monthOutputData))
                                {
                                    PowerPoint.ShapeRange shapeRange = presentation.SlideMaster.Shapes.PasteSpecial(PowerPoint.PpPasteDataType.ppPasteEnhancedMetafile, Microsoft.Office.Core.MsoTriState.msoFalse, "", 0, "", Microsoft.Office.Core.MsoTriState.msoFalse);
                                    // Resize selection
                                    shapeRange.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoFalse;
                                    shapeRange.Height = presentation.SlideMaster.Height;
                                    shapeRange.Width = presentation.SlideMaster.Width;
                                    shapeRange.Top = 0;
                                    shapeRange.Left = 0;

                                }
                                excelHelper.Disconnect();
                            }



                            presentation.SlideMaster.Design.Name = monthOutputData.SlideMasterName;
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

        private void SetDayRecordTagValue(BusinessClasses.CalendarOutputData monthOutputData, PowerPoint.Slide slide, PowerPoint.Shape shape, int dayNumber)
        {
            try
            {
                foreach (BusinessClasses.CalendarNote note in monthOutputData.Notes.Where(x => x.StartDay.Day == dayNumber))
                {
                    note.Left = shape.Left + 10;
                    note.Top = shape.Top + 10;
                    shape.Top += (note.Height + 15);
                    shape.Height -= (note.Height + 15);
                }
                foreach (BusinessClasses.CalendarNote note in monthOutputData.Notes.Where(x => x.FinishDay.Day == dayNumber))
                {
                    note.Right = shape.Left + shape.Width - 10;
                }

                BusinessClasses.CalendarNote midleNote = monthOutputData.Notes.Where(x => x.StartDay.Day < dayNumber && x.FinishDay.Day >= dayNumber).FirstOrDefault();
                if (midleNote != null)
                {
                    shape.Top += (midleNote.Height + 15);
                    shape.Height -= (midleNote.Height + 15);
                }

                PowerPoint.Shape imageShape = null;
                if (!string.IsNullOrEmpty(monthOutputData.DayLogoPaths[dayNumber - 1]))
                {
                    imageShape = slide.Shapes.AddPicture(FileName: monthOutputData.DayLogoPaths[dayNumber - 1], LinkToFile: Microsoft.Office.Core.MsoTriState.msoFalse, SaveWithDocument: Microsoft.Office.Core.MsoTriState.msoCTrue, Left: shape.Left + 3, Top: shape.Top + 3);
                }
                if (!string.IsNullOrEmpty(monthOutputData.DayOutput[dayNumber - 1]))
                {
                    shape.TextFrame.TextRange.Text = monthOutputData.DayOutput[dayNumber - 1];
                    shape.TextFrame.TextRange.Font.Size = monthOutputData.FontSize;
                    if (imageShape != null)
                    {
                        shape.Top = imageShape.Top + imageShape.Height;
                        shape.Height -= imageShape.Height;
                    }
                }
                else
                    shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
            }
            catch
            {
            }
        }

        public void PrepareCalendarEmail(string fileName, BusinessClasses.CalendarOutputData[] monthOutputData)
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
                foreach (BusinessClasses.CalendarOutputData month in monthOutputData)
                {
                    AppendCalendar(month, presentation);
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
        public bool GetCalendarBackground(BusinessClasses.CalendarOutputData monthOutputData)
        {
            bool result = false;
            try
            {
                Excel.Workbook workBook = _excelObject.Workbooks.Open(Path.Combine(BusinessClasses.OutputManager.Instance.CalendarBackgroundFolderPath, string.Format(BusinessClasses.OutputManager.ExcelBackgroundFileName, new object[] { monthOutputData.SlideColor, monthOutputData.Parent.StartDate.ToString("yyyy") })));
                workBook.Sheets[monthOutputData.BackgroundSheetName].Range["A1:N37"].Copy();
                result = true;
            }
            catch
            {
            }
            return result;
        }
    }
}


