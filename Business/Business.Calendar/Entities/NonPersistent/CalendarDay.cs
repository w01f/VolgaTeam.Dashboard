using System;
using System.Text;
using Asa.Business.Calendar.Interfaces;
using Asa.Common.Core.Objects.Images;
using Newtonsoft.Json;

namespace Asa.Business.Calendar.Entities.NonPersistent
{
	public abstract class CalendarDay
	{
		protected string _userData;

		public ICalendarContent Parent { get; private set; }
		public DateTime Date { get; set; }
		public bool BelongsToSchedules { get; set; }
		public bool HasNotes { get; set; }
		public bool EditedByUser { get; private set; }
		public ImageSource Logo { get; set; }
		public virtual string ImportedData => String.Empty;

		[JsonIgnore]
		public string Comment
		{
			get { return !String.IsNullOrEmpty(_userData) ? _userData : ImportedData; }
			set
			{
				if (ImportedData != value && !String.IsNullOrEmpty(value))
				{
					_userData = value;
					EditedByUser = true;
				}
				else
				{
					_userData = null;
					EditedByUser = false;
				}
			}
		}

		public abstract bool IsMondatBased { get; }

		public string Summary
		{
			get
			{
				var result = new StringBuilder();

				if (!string.IsNullOrEmpty(Comment))
					result.AppendLine(Comment);
				return result.ToString();
			}
		}

		public bool ContainsData => !string.IsNullOrEmpty(Summary) || Logo.ContainsData;

		public abstract int WeekDayIndex { get; }

		[JsonConstructor]
		protected CalendarDay() { }

		protected CalendarDay(ICalendarContent parent)
		{
			Parent = parent;
			Logo = new ImageSource();
		}

		public void ClearData()
		{
			_userData = null;
			EditedByUser = false;
			Logo = new ImageSource();
		}

		public virtual void Dispose()
		{
			if (Logo != null)
				Logo.Dispose();
			Logo = null;
			Parent = null;
		}
	}
}
