using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Common.Enums;
using Asa.Business.Common.Interfaces;
using Asa.Common.Core.Dictionaries;
using Asa.Common.Core.Interfaces;

namespace Asa.Business.Common.Entities.NonPersistent.Schedule
{
	public abstract class BaseScheduleSettings : SettingsContainer, IBaseScheduleSettings, IJsonCloneable<BaseScheduleSettings>
	{
		public ScheduleEditMode EditMode { get; set; }
		public string BusinessName { get; set; }
		public string DecisionMaker { get; set; }
		public string Status { get; set; }
		public DateTime? PresentationDate { get; set; }
		public DateTime? FlightDateStart { get; set; }
		public DateTime? FlightDateEnd { get; set; }

		public abstract string FlightDates { get; }

		public virtual bool IsNew => (EditMode == ScheduleEditMode.Regular && 
									(String.IsNullOrEmpty(BusinessName) ||
									String.IsNullOrEmpty(DecisionMaker) ||
									!PresentationDate.HasValue)) ||
									!FlightDateStart.HasValue ||
									!FlightDateEnd.HasValue;

		public int TotalWeeks => GetWeeks().Count();

		public IEnumerable<DateRange> GetWeeks()
		{
			var weeks = new List<DateRange>();
			if (FlightDateStart.HasValue && FlightDateEnd.HasValue)
			{
				var startDate = FlightDateStart.Value;
				while (startDate < FlightDateEnd)
				{
					weeks.Add(new DateRange()
					{
						StartDate = startDate,
						FinishDate = startDate.AddDays(6)
					});
					startDate = startDate.AddDays(7);
				}
			}
			return weeks;
		}

		public virtual void AfterClone(BaseScheduleSettings source, bool fullClone = true)
		{
			AfterCreate();
		}

		public void UpdateDictionaries()
		{
			ListManager.Instance.Advertisers.Add(BusinessName);
			ListManager.Instance.DecisionMakers.Add(DecisionMaker);
		}

		public virtual void Dispose()
		{
			Parent = null;
		}
	}
}
