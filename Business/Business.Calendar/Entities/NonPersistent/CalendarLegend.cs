using Asa.Common.Core.Interfaces;

namespace Asa.Business.Calendar.Entities.NonPersistent
{
	public class CalendarLegend : IJsonCloneable<CalendarLegend>
	{
		public string Code { get; set; }
		public string Description { get; set; }
		public bool Visible { get; set; }

		public string StringRepresentation => Code + " = " + Description;

		public CalendarLegend()
		{
			Code = string.Empty;
			Description = string.Empty;
			Visible = true;
		}

		public void AfterClone(CalendarLegend source, bool fullClone = true) { }
	}
}
