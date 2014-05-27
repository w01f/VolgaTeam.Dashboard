using System;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.VisualBasic;
using NewBizWiz.Calendar.Controls.BusinessClasses;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Interop;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace NewBizWiz.Calendar.Controls.InteropClasses
{
	public partial class CalendarPowerPointHelper
	{
		public void AppendCalendar(CommonCalendarOutputData[] monthOutputDatas, Presentation destinationPresentation = null)
		{
			if (!Directory.Exists(BusinessWrapper.Instance.OutputManager.CalendarTemlatesFolderPath)) return;
			foreach (var monthOutputData in monthOutputDatas)
			{
				var presentationTemplatePath = Path.Combine(BusinessWrapper.Instance.OutputManager.CalendarTemlatesFolderPath, BusinessWrapper.Instance.OutputManager.TemplatesManager.GetSlideName(monthOutputData));
				if (!File.Exists(presentationTemplatePath)) return;
				try
				{
					var thread = new Thread(delegate()
					{
						monthOutputData.PrepareDayLogoPaths();
						var daysCount = monthOutputData.DayOutput.Length;
						MessageFilter.Register();
						var presentation = _powerPointObject.Presentations.Open(presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
						foreach (Slide slide in presentation.Slides)
						{
							foreach (Shape shape in slide.Shapes)
							{
								if (shape.HasTextFrame == MsoTriState.msoTrue)
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
												slide.Shapes.AddPicture(monthOutputData.LogoFile, MsoTriState.msoFalse, MsoTriState.msoCTrue, shape.Left, shape.Top, shape.Width, shape.Height);
											shape.Visible = MsoTriState.msoFalse;
											break;
										case "HEADERTAG":
											shape.TextFrame.TextRange.Text = monthOutputData.SlideTitle;
											break;
										case "PREPAREDFOR":
											if (string.IsNullOrEmpty(monthOutputData.BusinessName) && string.IsNullOrEmpty(monthOutputData.DecisionMaker))
												shape.Visible = MsoTriState.msoFalse;
											else
												shape.Visible = MsoTriState.msoCTrue;
											break;
										case "ADVORDEC1":
											if (string.IsNullOrEmpty(monthOutputData.BusinessName) && string.IsNullOrEmpty(monthOutputData.DecisionMaker))
												shape.Visible = MsoTriState.msoFalse;
											else
											{
												shape.Visible = MsoTriState.msoCTrue;
												if (string.IsNullOrEmpty(monthOutputData.BusinessName))
													shape.TextFrame.TextRange.Text = monthOutputData.DecisionMaker;
												else
													shape.TextFrame.TextRange.Text = monthOutputData.BusinessName;
											}
											break;
										case "DECNAME2":
											if (string.IsNullOrEmpty(monthOutputData.DecisionMaker))
												shape.Visible = MsoTriState.msoFalse;
											else
											{
												shape.Visible = MsoTriState.msoCTrue;
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
											shape.TextFrame.TextRange.Text = String.Empty;
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
							foreach (var note in monthOutputData.Notes)
							{
								var noteShape = slide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, note.Left, note.Top, note.Right - note.Left, note.Height);
								noteShape.Fill.Visible = MsoTriState.msoTrue;
								noteShape.Fill.Solid();
								noteShape.Fill.ForeColor.RGB = Information.RGB(note.BackgroundColor.R, note.BackgroundColor.G, note.BackgroundColor.B);
								noteShape.Fill.Transparency = 0;

								noteShape.Line.Visible = MsoTriState.msoTrue;
								noteShape.Line.ForeColor.SchemeColor = PpColorSchemeIndex.ppForeground;
								noteShape.Line.BackColor.RGB = Information.RGB(0, 0, 0);
								;

								noteShape.Shadow.Visible = MsoTriState.msoTrue;
								noteShape.Shadow.Type = MsoShadowType.msoShadow14;

								noteShape.TextFrame.TextRange.Font.Color.RGB = Information.RGB(note.ForeColor.R, note.ForeColor.G, note.ForeColor.B);
								noteShape.TextFrame.TextRange.Font.Name = "Calibri";
								noteShape.TextFrame.TextRange.Font.Size = 8;
								noteShape.TextFrame.TextRange.Text = note.Note.SimpleText;
							}
						}

						var backgroundFilePath = Path.Combine(BusinessWrapper.Instance.OutputManager.CalendarBackgroundFolderPath, String.Format(OutputManager.BackgroundFilePath, monthOutputData.SlideColor, monthOutputData.Parent.Date.ToString("yyyy")), monthOutputData.BackgroundFileName);
						if (File.Exists(backgroundFilePath))
							presentation.SlideMaster.Shapes.AddPicture(backgroundFilePath, MsoTriState.msoFalse, MsoTriState.msoCTrue, 0, 0, presentation.SlideMaster.Width, presentation.SlideMaster.Height);
						presentation.SlideMaster.Design.Name = BusinessWrapper.Instance.OutputManager.TemplatesManager.GetSlideMasterName(monthOutputData);
						AppendSlide(presentation, -1, destinationPresentation);
						presentation.Close();
					});
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

		private static void SetDayRecordTagValue(CommonCalendarOutputData monthOutputData, Slide slide, Shape shape, int dayNumber)
		{
			try
			{
				foreach (var note in monthOutputData.Notes.Where(x => x.StartDay.Day == dayNumber))
				{
					note.Left = shape.Left + 10;
					note.Top = shape.Top + 10;
					shape.Top += (note.Height + 15);
					shape.Height -= (note.Height + 15);
				}
				foreach (var note in monthOutputData.Notes.Where(x => x.FinishDay.Day == dayNumber))
				{
					note.Right = shape.Left + shape.Width - 10;
				}

				var midleNote = monthOutputData.Notes.FirstOrDefault(x => x.StartDay.Day < dayNumber && x.FinishDay.Day >= dayNumber);
				if (midleNote != null)
				{
					shape.Top += (midleNote.Height + 15);
					shape.Height -= (midleNote.Height + 15);
				}

				Shape imageShape = null;
				if (!string.IsNullOrEmpty(monthOutputData.DayLogoPaths[dayNumber - 1]))
				{
					imageShape = slide.Shapes.AddPicture(monthOutputData.DayLogoPaths[dayNumber - 1], MsoTriState.msoFalse, MsoTriState.msoCTrue, shape.Left + 3, shape.Top + 3);
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
					shape.Visible = MsoTriState.msoFalse;
			}
			catch { }
		}

		public void PrepareCalendarEmail(string fileName, CommonCalendarOutputData[] monthOutputData)
		{
			PreparePresentation(fileName, presentation => AppendCalendar(monthOutputData, presentation));
		}
	}
}