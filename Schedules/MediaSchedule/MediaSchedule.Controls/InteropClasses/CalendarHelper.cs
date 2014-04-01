using System;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.VisualBasic;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Interop;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace NewBizWiz.MediaSchedule.Controls.InteropClasses
{
	public partial class MediaSchedulePowerPointHelper
	{
		public void AppendCalendar(BroadcastCalendarOutputData[] monthOutputDatas, Presentation destinationPresentation = null)
		{
			if (!Directory.Exists(BusinessWrapper.Instance.OutputManager.CalendarTemlatesFolderPath)) return;
			foreach (var monthOutputData in monthOutputDatas)
			{
				var presentationTemplatePath = Path.Combine(BusinessWrapper.Instance.OutputManager.CalendarTemlatesFolderPath,
					String.Format(OutputManager.CalendarSlideTemplate,
					monthOutputData.ShowLogo ? "logo" : "no_logo",
					monthOutputData.DayOutput.Length,
					Core.Common.SettingsManager.Instance.SlideFolder.Replace("Slides", "")));
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
									var shapeName = shape.Tags.Name(i).Trim();
									switch (shapeName)
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
										case "DAY1":
											if (daysCount > 0)
												SetDayRecordTagValue(monthOutputData, slide, shape, 1);
											break;
										case "DAY2":
											if (daysCount > 1)
												SetDayRecordTagValue(monthOutputData, slide, shape, 2);
											break;
										case "DAY3":
											if (daysCount > 2)
												SetDayRecordTagValue(monthOutputData, slide, shape, 3);
											break;
										case "DAY4":
											if (daysCount > 3)
												SetDayRecordTagValue(monthOutputData, slide, shape, 4);
											break;
										case "DAY5":
											if (daysCount > 4)
												SetDayRecordTagValue(monthOutputData, slide, shape, 5);
											break;
										case "DAY6":
											if (daysCount > 5)
												SetDayRecordTagValue(monthOutputData, slide, shape, 6);
											break;
										case "DAY7":
											if (daysCount > 6)
												SetDayRecordTagValue(monthOutputData, slide, shape, 7);
											break;
										case "DAY8":
											if (daysCount > 7)
												SetDayRecordTagValue(monthOutputData, slide, shape, 8);
											break;
										case "DAY9":
											if (daysCount > 8)
												SetDayRecordTagValue(monthOutputData, slide, shape, 9);
											break;
										case "DAY10":
											if (daysCount > 9)
												SetDayRecordTagValue(monthOutputData, slide, shape, 10);
											break;
										case "DAY11":
											if (daysCount > 10)
												SetDayRecordTagValue(monthOutputData, slide, shape, 11);
											break;
										case "DAY12":
											if (daysCount > 11)
												SetDayRecordTagValue(monthOutputData, slide, shape, 12);
											break;
										case "DAY13":
											if (daysCount > 12)
												SetDayRecordTagValue(monthOutputData, slide, shape, 13);
											break;
										case "DAY14":
											if (daysCount > 13)
												SetDayRecordTagValue(monthOutputData, slide, shape, 14);
											break;
										case "DAY15":
											if (daysCount > 14)
												SetDayRecordTagValue(monthOutputData, slide, shape, 15);
											break;
										case "DAY16":
											if (daysCount > 15)
												SetDayRecordTagValue(monthOutputData, slide, shape, 16);
											break;
										case "DAY17":
											if (daysCount > 16)
												SetDayRecordTagValue(monthOutputData, slide, shape, 17);
											break;
										case "DAY18":
											if (daysCount > 17)
												SetDayRecordTagValue(monthOutputData, slide, shape, 18);
											break;
										case "DAY19":
											if (daysCount > 18)
												SetDayRecordTagValue(monthOutputData, slide, shape, 19);
											break;
										case "DAY20":
											if (daysCount > 19)
												SetDayRecordTagValue(monthOutputData, slide, shape, 20);
											break;
										case "DAY21":
											if (daysCount > 20)
												SetDayRecordTagValue(monthOutputData, slide, shape, 21);
											break;
										case "DAY22":
											if (daysCount > 21)
												SetDayRecordTagValue(monthOutputData, slide, shape, 22);
											break;
										case "DAY23":
											if (daysCount > 22)
												SetDayRecordTagValue(monthOutputData, slide, shape, 23);
											break;
										case "DAY24":
											if (daysCount > 23)
												SetDayRecordTagValue(monthOutputData, slide, shape, 24);
											break;
										case "DAY25":
											if (daysCount > 24)
												SetDayRecordTagValue(monthOutputData, slide, shape, 25);
											break;
										case "DAY26":
											if (daysCount > 25)
												SetDayRecordTagValue(monthOutputData, slide, shape, 26);
											break;
										case "DAY27":
											if (daysCount > 26)
												SetDayRecordTagValue(monthOutputData, slide, shape, 27);
											break;
										case "DAY28":
											if (daysCount > 27)
												SetDayRecordTagValue(monthOutputData, slide, shape, 28);
											break;
										case "DAY29":
											if (daysCount > 28)
												SetDayRecordTagValue(monthOutputData, slide, shape, 29);
											break;
										case "DAY30":
											if (daysCount > 29)
												SetDayRecordTagValue(monthOutputData, slide, shape, 30);
											break;
										case "DAY31":
											if (daysCount > 30)
												SetDayRecordTagValue(monthOutputData, slide, shape, 31);
											break;
										case "DAY32":
											if (daysCount > 31)
												SetDayRecordTagValue(monthOutputData, slide, shape, 32);
											break;
										case "DAY33":
											if (daysCount > 32)
												SetDayRecordTagValue(monthOutputData, slide, shape, 33);
											break;
										case "DAY34":
											if (daysCount > 33)
												SetDayRecordTagValue(monthOutputData, slide, shape, 34);
											break;
										case "DAY35":
											if (daysCount > 34)
												SetDayRecordTagValue(monthOutputData, slide, shape, 35);
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
								noteShape.TextFrame.TextRange.Text = note.Note;
							}
						}

						var backgroundFilePath = Path.Combine(BusinessWrapper.Instance.OutputManager.CalendarBackgroundFolderPath, String.Format(OutputManager.BackgroundFilePath, monthOutputData.SlideColor, monthOutputData.Parent.Date.ToString("yyyy")), monthOutputData.BackgroundFileName);
						if (File.Exists(backgroundFilePath))
							presentation.SlideMaster.Shapes.AddPicture(backgroundFilePath, MsoTriState.msoFalse, MsoTriState.msoCTrue, 0, 0, presentation.SlideMaster.Width, presentation.SlideMaster.Height);
						presentation.SlideMaster.Design.Name = GetSlideMasterName(monthOutputData);
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

		private string GetSlideMasterName(BroadcastCalendarOutputData monthOutputData)
		{
			return String.Format("{0}{1}{2}", monthOutputData.Parent.Date.ToString("MMMyy").ToLower(), monthOutputData.SlideColor.ToLower(), monthOutputData.ShowBigDate ? "L" : "t");
		}

		private void SetDayRecordTagValue(BroadcastCalendarOutputData monthOutputData, Slide slide, Shape shape, int dayNumber)
		{
			try
			{
				var day = monthOutputData.Parent.Days[dayNumber - 1];
				foreach (var note in monthOutputData.Notes.Where(x => x.StartDay.Date == day.Date.Date))
				{
					note.Left = shape.Left + 10;
					note.Top = shape.Top + 10;
					shape.Top += (note.Height + 15);
					shape.Height -= (note.Height + 15);
				}
				foreach (var note in monthOutputData.Notes.Where(x => x.FinishDay.Date == day.Date.Date))
				{
					note.Right = shape.Left + shape.Width - 10;
				}

				var midleNote = monthOutputData.Notes.FirstOrDefault(x => x.StartDay.Date < day.Date.Date && x.FinishDay.Date > day.Date.Date);
				if (midleNote != null)
				{
					shape.Top += (midleNote.Height + 15);
					shape.Height -= (midleNote.Height + 15);
				}

				Shape imageShape = null;
				if (!String.IsNullOrEmpty(monthOutputData.DayLogoPaths[dayNumber - 1]))
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

		public void PrepareCalendarEmail(string fileName, BroadcastCalendarOutputData[] monthOutputData)
		{
			PreparePresentation(fileName, presentation => AppendCalendar(monthOutputData, presentation));
		}
	}
}