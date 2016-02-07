using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using Asa.Common.Core.Objects.Images;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace Asa.Business.Calendar.Entities.NonPersistent
{
	public abstract class CalendarOutputData
	{
		protected readonly List<ImageSource> _dayLogosPaths = new List<ImageSource>();
		protected string _encodedLogo;

		public List<CalendarNote> Notes { get; private set; }

		public CalendarMonth Parent { get; private set; }

		#region Basic
		public bool ShowHeader { get; set; }
		public bool ShowBusinessName { get; set; }
		public bool ShowDecisionMaker { get; set; }

		public string Header { get; set; }
		public bool ApplyForAllBasic { get; set; }
		#endregion

		#region Notes
		public bool ShowCustomComment { get; set; }
		public string CustomComment { get; set; }
		public bool ApplyForAllCustomComment { get; set; }
		#endregion

		#region Style
		public string SlideColor { get; set; }
		public bool ApplyForAllThemeColor { get; set; }
		public bool ShowLogo { get; set; }
		public ImageSource Logo { get; set; }
		public bool ApplyForAllLogo { get; set; }
		public bool ShowBigDate { get; set; }
		#endregion

		#region Calculated Options
		public string SlideTitle
		{
			get { return ShowHeader ? Header : string.Empty; }
		}

		public string BusinessName
		{
			get { return !ShowBusinessName ? string.Empty : Parent.Parent.Settings.BusinessName; }
		}

		public string DecisionMaker
		{
			get { return !ShowDecisionMaker ? string.Empty : Parent.Parent.Settings.DecisionMaker; }
		}

		public int SlideRGB
		{
			get
			{
				int result = Color.Black.ToArgb();
				switch (SlideColor)
				{
					case "black":
						result = Information.RGB(0, 0, 0);
						break;
					case "blue":
						result = Information.RGB(0, 0, 102);
						break;
					case "gray":
						result = Information.RGB(0, 0, 0);
						break;
					case "green":
						result = Information.RGB(0, 51, 0);
						break;
					case "orange":
						result = Information.RGB(153, 0, 0);
						break;
					case "teal":
						result = Information.RGB(0, 51, 102);
						break;
				}
				return result;
			}
		}

		public string LogoFile
		{
			get
			{
				var result = string.Empty;
				if (!ShowLogo || Logo == null) return result;
				result = Path.GetTempFileName();
				try
				{
					File.Copy(Logo.FileName, result, true);
				}
				catch { }
				return result;
			}
		}

		public string MonthText
		{
			get { return Parent.Date.ToString("MMMM yyyy"); }
		}

		public string Comments
		{
			get
			{
				var result = string.Empty;
				if (ShowCustomComment)
					result = CustomComment;
				return result;
			}
		}

		public string[] DayOutput
		{
			get { return Parent.Days.Select(x => x.Summary).ToArray(); }
		}

		public List<ImageSource> DayLogoPaths
		{
			get { return _dayLogosPaths; }
		}

		public float FontSize
		{
			get { return 7; }
		}

		public virtual string TagA
		{
			get { return String.Empty; }
		}

		public virtual string TagB
		{
			get { return String.Empty; }
		}

		public virtual string TagC
		{
			get { return String.Empty; }
		}

		public virtual string TagD
		{
			get { return String.Empty; }
		}
		#endregion

		[JsonConstructor]
		protected CalendarOutputData() { }

		protected CalendarOutputData(CalendarMonth parent)
		{
			Parent = parent;
			Notes = new List<CalendarNote>();

			ShowCustomComment = false;
			ApplyForAllCustomComment = true;

			#region Basic
			ShowHeader = true;
			ShowBusinessName = true;
			ShowDecisionMaker = true;
			Header = String.Empty;
			ApplyForAllBasic = true;
			#endregion

			#region Style

			SlideColor = "gray";
			ApplyForAllThemeColor = true;

			ShowLogo = true;
			ApplyForAllLogo = true;

			ShowBigDate = false;

			#endregion
		}

		public virtual void Dispose()
		{
			if (Logo != null)
				Logo.Dispose();
			Logo = null;

			Notes.ForEach(n => n.Dispose());
			Notes.Clear();

			Parent = null;
		}

		public void PrepareDayLogoPaths()
		{
			_dayLogosPaths.Clear();
			foreach (var day in Parent.Days)
			{
				if (day.Logo.ContainsData)
				{
					day.Logo.PrepareOutputFile();
					_dayLogosPaths.Add(day.Logo);
				}
				else
					_dayLogosPaths.Add(new ImageSource());
			}
		}

		public void PrepareNotes()
		{
			Notes.Clear();
			Notes.AddRange(Parent.Parent.Notes.Where(x => x.StartDay >= Parent.DaysRangeBegin && x.FinishDay <= Parent.DaysRangeEnd));
		}
	}
}
