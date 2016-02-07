using System;

namespace Asa.Business.Media.Entities.NonPersistent.Schedule
{
	public class Quarter
	{
		public DateTime DateAnchor { get; set; }
		public DateTime DateStart { get; set; }
		public DateTime DateEnd { get; set; }

		public int QuarterNumber
		{
			get
			{
				if (DateAnchor.Month >= 1 && DateAnchor.Month <= 3)
					return 1;
				if (DateAnchor.Month >= 4 && DateAnchor.Month <= 6)
					return 2;
				if (DateAnchor.Month >= 7 && DateAnchor.Month <= 9)
					return 3;
				if (DateAnchor.Month >= 10 && DateAnchor.Month <= 12)
					return 4;
				return 0;
			}
		}

		public override string ToString()
		{
			return String.Format("Q{0} {1}", QuarterNumber, DateAnchor.ToString("yy"));
		}
	}
}
