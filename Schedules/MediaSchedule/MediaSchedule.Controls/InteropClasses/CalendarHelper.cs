using System;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.VisualBasic;
using Asa.Media.Controls.BusinessClasses;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Common.Core.OfficeInterops;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Enums;

namespace Asa.Media.Controls.InteropClasses
{
	public partial class MediaSchedulePowerPointHelper<T> where T : class,new()
	{
		public void AppendCalendar(CalendarOutputData[] monthOutputDatas, Presentation destinationPresentation = null)
		{
			foreach (var monthOutputData in monthOutputDatas)
			{
				var presentationTemplatePath = BusinessObjects.Instance.OutputManager.GetCalendarFile(monthOutputData.ShowLogo, monthOutputData.DayOutput.Length);
				if (String.IsNullOrEmpty(presentationTemplatePath) || !File.Exists(presentationTemplatePath)) return;
				try
				{
					var thread = new Thread(delegate()
					{
						monthOutputData.PrepareDayLogoPaths();
						var daysCount = monthOutputData.DayOutput.Length;
						MessageFilter.Register();
						var presentation = PowerPointObject.Presentations.Open(presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
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
											{
												var logoShape = slide.Shapes.AddPicture(monthOutputData.LogoFile, MsoTriState.msoFalse, MsoTriState.msoCTrue, shape.Left, shape.Top, shape.Width, shape.Height);
												if (PowerPointManager.Instance.SlideSettings.Orientation == SlideOrientationEnum.Portrait)
													logoShape.Rotation = 90;
											}
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
										case "1-1":
											if (daysCount > 0)
												SetDayRecordTagValue(monthOutputData, slide, shape, 1);
											break;
										case "DAY2":
										case "2-1":
											if (daysCount > 1)
												SetDayRecordTagValue(monthOutputData, slide, shape, 2);
											break;
										case "DAY3":
										case "3-1":
											if (daysCount > 2)
												SetDayRecordTagValue(monthOutputData, slide, shape, 3);
											break;
										case "DAY4":
										case "4-1":
											if (daysCount > 3)
												SetDayRecordTagValue(monthOutputData, slide, shape, 4);
											break;
										case "DAY5":
										case "5-1":
											if (daysCount > 4)
												SetDayRecordTagValue(monthOutputData, slide, shape, 5);
											break;
										case "DAY6":
										case "6-1":
											if (daysCount > 5)
												SetDayRecordTagValue(monthOutputData, slide, shape, 6);
											break;
										case "DAY7":
										case "7-1":
											if (daysCount > 6)
												SetDayRecordTagValue(monthOutputData, slide, shape, 7);
											break;
										case "DAY8":
										case "8-1":
											if (daysCount > 7)
												SetDayRecordTagValue(monthOutputData, slide, shape, 8);
											break;
										case "DAY9":
										case "9-1":
											if (daysCount > 8)
												SetDayRecordTagValue(monthOutputData, slide, shape, 9);
											break;
										case "DAY10":
										case "10-1":
											if (daysCount > 9)
												SetDayRecordTagValue(monthOutputData, slide, shape, 10);
											break;
										case "DAY11":
										case "11-1":
											if (daysCount > 10)
												SetDayRecordTagValue(monthOutputData, slide, shape, 11);
											break;
										case "DAY12":
										case "12-1":
											if (daysCount > 11)
												SetDayRecordTagValue(monthOutputData, slide, shape, 12);
											break;
										case "DAY13":
										case "13-1":
											if (daysCount > 12)
												SetDayRecordTagValue(monthOutputData, slide, shape, 13);
											break;
										case "DAY14":
										case "14-1":
											if (daysCount > 13)
												SetDayRecordTagValue(monthOutputData, slide, shape, 14);
											break;
										case "DAY15":
										case "15-1":
											if (daysCount > 14)
												SetDayRecordTagValue(monthOutputData, slide, shape, 15);
											break;
										case "DAY16":
										case "16-1":
											if (daysCount > 15)
												SetDayRecordTagValue(monthOutputData, slide, shape, 16);
											break;
										case "DAY17":
										case "17-1":
											if (daysCount > 16)
												SetDayRecordTagValue(monthOutputData, slide, shape, 17);
											break;
										case "DAY18":
										case "18-1":
											if (daysCount > 17)
												SetDayRecordTagValue(monthOutputData, slide, shape, 18);
											break;
										case "DAY19":
										case "19-1":
											if (daysCount > 18)
												SetDayRecordTagValue(monthOutputData, slide, shape, 19);
											break;
										case "DAY20":
										case "20-1":
											if (daysCount > 19)
												SetDayRecordTagValue(monthOutputData, slide, shape, 20);
											break;
										case "DAY21":
										case "21-1":
											if (daysCount > 20)
												SetDayRecordTagValue(monthOutputData, slide, shape, 21);
											break;
										case "DAY22":
										case "22-1":
											if (daysCount > 21)
												SetDayRecordTagValue(monthOutputData, slide, shape, 22);
											break;
										case "DAY23":
										case "23-1":
											if (daysCount > 22)
												SetDayRecordTagValue(monthOutputData, slide, shape, 23);
											break;
										case "DAY24":
										case "24-1":
											if (daysCount > 23)
												SetDayRecordTagValue(monthOutputData, slide, shape, 24);
											break;
										case "DAY25":
										case "25-1":
											if (daysCount > 24)
												SetDayRecordTagValue(monthOutputData, slide, shape, 25);
											break;
										case "DAY26":
										case "26-1":
											if (daysCount > 25)
												SetDayRecordTagValue(monthOutputData, slide, shape, 26);
											break;
										case "DAY27":
										case "27-1":
											if (daysCount > 26)
												SetDayRecordTagValue(monthOutputData, slide, shape, 27);
											break;
										case "DAY28":
										case "28-1":
											if (daysCount > 27)
												SetDayRecordTagValue(monthOutputData, slide, shape, 28);
											break;
										case "DAY29":
										case "29-1":
											if (daysCount > 28)
												SetDayRecordTagValue(monthOutputData, slide, shape, 29);
											break;
										case "DAY30":
										case "30-1":
											if (daysCount > 29)
												SetDayRecordTagValue(monthOutputData, slide, shape, 30);
											break;
										case "DAY31":
										case "31-1":
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
								Shape noteShape = null;
								if (PowerPointManager.Instance.SlideSettings.Orientation == SlideOrientationEnum.Portrait)
									noteShape = slide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationVertical, note.Left, note.Top, note.StaticWidth, note.Bottom - note.Top);
								else
									noteShape = slide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, note.Left, note.Top, note.Right - note.Left, note.StaticHeight);

								noteShape.Fill.Visible = MsoTriState.msoTrue;
								noteShape.Fill.Solid();
								noteShape.Fill.ForeColor.RGB = Information.RGB(note.BackgroundColor.R, note.BackgroundColor.G, note.BackgroundColor.B);
								noteShape.Fill.Transparency = 0;

								noteShape.Line.Visible = MsoTriState.msoTrue;
								noteShape.Line.ForeColor.SchemeColor = PpColorSchemeIndex.ppForeground;
								noteShape.Line.BackColor.RGB = Information.RGB(0, 0, 0);

								noteShape.Shadow.Visible = MsoTriState.msoTrue;
								noteShape.Shadow.Type = MsoShadowType.msoShadow14;

								noteShape.TextFrame.TextRange.Font.Color.RGB = Information.RGB(note.ForeColor.R, note.ForeColor.G, note.ForeColor.B);
								noteShape.TextFrame.TextRange.Font.Name = "Calibri";
								noteShape.TextFrame.TextRange.Font.Size = 8;
								note.Note.AddTextRange(noteShape.TextFrame.TextRange);
							}
						}

						var backgroundFilePath = BusinessObjects.Instance.OutputManager.GetCalendarBackgroundFile(monthOutputData.SlideColor, monthOutputData.Parent.Date, monthOutputData.ShowBigDate);
						if (!String.IsNullOrEmpty(backgroundFilePath) && File.Exists(backgroundFilePath))
						{
							var backgroundShape = presentation.SlideMaster.Shapes.AddPicture(backgroundFilePath, MsoTriState.msoFalse, MsoTriState.msoCTrue, 0, 0);
							if (PowerPointManager.Instance.SlideSettings.Orientation == SlideOrientationEnum.Portrait)
							{
								backgroundShape.Height = presentation.SlideMaster.Width;
								backgroundShape.Width = presentation.SlideMaster.Height;
								backgroundShape.Top = (presentation.SlideMaster.Height - backgroundShape.Height) / 2;
								backgroundShape.Left = (presentation.SlideMaster.Width - backgroundShape.Width) / 2; ;
								backgroundShape.Rotation = 90;
							}
							else
							{
								backgroundShape.Top = (presentation.SlideMaster.Height - backgroundShape.Height) / 2;
								backgroundShape.Left = (presentation.SlideMaster.Width - backgroundShape.Width) / 2;
							}
						}

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

		private string GetSlideMasterName(CalendarOutputData monthOutputData)
		{
			return String.Format("{0}{1}{2}", monthOutputData.Parent.Date.ToString("MMMyy").ToLower(), monthOutputData.SlideColor.ToLower(), monthOutputData.ShowBigDate ? "L" : "t");
		}

		private static void SetDayRecordTagValue(CalendarOutputData monthOutputData, Slide slide, Shape shape, int dayNumber)
		{
			try
			{
				var day = monthOutputData.Parent.Days[dayNumber - 1];
				var hasNote = false;
				shape.TextFrame.TextRange.Font.Size = monthOutputData.FontSize;
				foreach (var note in monthOutputData.Notes.Where(note => note.StartDay.Date == day.Date.Date))
				{
					if (PowerPointManager.Instance.SlideSettings.Orientation == SlideOrientationEnum.Portrait)
					{
						note.Left = shape.Left + shape.Height - 15;
						note.Top = shape.Top - ((shape.Width - shape.Height) / 2) + 5;

						shape.Left -= (note.StaticWidth + 10);
					}
					else
					{
						note.Left = shape.Left + 5;
						note.Top = shape.Top + 5;
						shape.Top += (note.StaticHeight + 10);
						shape.Height -= (note.StaticHeight + 10);
					}

					hasNote = true;
				}
				foreach (var note in monthOutputData.Notes.Where(note => note.FinishDay.Date == day.Date.Date))
				{
					if (PowerPointManager.Instance.SlideSettings.Orientation == SlideOrientationEnum.Portrait)
					{
						note.Bottom = shape.Top - ((shape.Width - shape.Height) / 2) + shape.Width - 10;
					}
					else
					{
						note.Right = shape.Left + shape.Width - 10;
					}
				}

				var middleNote = monthOutputData.Notes.FirstOrDefault(note => note.StartDay.Date < day.Date.Date && note.FinishDay.Date >= day.Date.Date);
				if (middleNote != null)
				{
					if (PowerPointManager.Instance.SlideSettings.Orientation == SlideOrientationEnum.Portrait)
					{
						shape.Left -= (middleNote.StaticWidth + 10);
					}
					else
					{
						shape.Top += (middleNote.StaticHeight + 10);
						shape.Height -= (middleNote.StaticHeight + 10);
					}
					hasNote = true;
				}

				Shape imageShape = null;
				var dayText = monthOutputData.DayOutput[dayNumber - 1];
				var dayLogo = monthOutputData.DayLogoPaths[dayNumber - 1];
				if (dayLogo.ContainsData)
				{
					if (PowerPointManager.Instance.SlideSettings.Orientation == SlideOrientationEnum.Portrait)
					{
						imageShape = slide.Shapes.AddPicture(dayLogo.OutputFilePath, MsoTriState.msoFalse, MsoTriState.msoCTrue, shape.Left + shape.Width - ((shape.Width - shape.Height) / 2) - dayLogo.XtraTinyImage.Width + ((dayLogo.XtraTinyImage.Width - dayLogo.XtraTinyImage.Height) / 2), shape.Top + (shape.Height - dayLogo.XtraTinyImage.Height) / 2, dayLogo.XtraTinyImage.Width, dayLogo.XtraTinyImage.Height);
						imageShape.Rotation = 90;
					}
					else
					{
						var heightOffset = ((!String.IsNullOrEmpty(dayText) || hasNote) ? 0 : ((shape.Height - dayLogo.XtraTinyImage.Height) / 2)) + 5;
						imageShape = slide.Shapes.AddPicture(dayLogo.OutputFilePath, MsoTriState.msoFalse, MsoTriState.msoCTrue, shape.Left + (shape.Width - dayLogo.XtraTinyImage.Width) / 2, shape.Top + heightOffset, dayLogo.XtraTinyImage.Width, dayLogo.XtraTinyImage.Height);
					}
				}
				if (!String.IsNullOrEmpty(dayText))
				{
					shape.TextFrame.TextRange.Text = dayText;
					if (imageShape != null)
					{
						if (PowerPointManager.Instance.SlideSettings.Orientation == SlideOrientationEnum.Portrait)
						{
							shape.Left -= imageShape.Height;
						}
						else
						{
							shape.Top = imageShape.Top + imageShape.Height;
							shape.Height -= imageShape.Height;
						}
					}
				}
				else
					shape.Visible = MsoTriState.msoFalse;
			}
			catch { }
		}

		public void PrepareCalendarEmail(string fileName, CalendarOutputData[] monthOutputData)
		{
			PreparePresentation(fileName, presentation => AppendCalendar(monthOutputData, presentation));
		}
	}
}