using System;

namespace Asa.Business.Common.Entities.NonPersistent.Common
{
	public class DateRange
	{
		public DateTime? StartDate { get; set; }
		public DateTime? FinishDate { get; set; }

		public string Range
		{
			get
			{
				if (!(StartDate.HasValue && FinishDate.HasValue)) return String.Empty;
				return String.Format("{0}-{1}", StartDate.Value.ToString("MM/dd/yy"), FinishDate.Value.ToString("MM/dd/yy"));
			}
		}
	}
}
