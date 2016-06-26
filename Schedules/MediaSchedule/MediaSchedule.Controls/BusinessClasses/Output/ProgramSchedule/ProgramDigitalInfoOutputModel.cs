using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Media.Controls.BusinessClasses.Output.DigitalInfo;

namespace Asa.Media.Controls.BusinessClasses.Output.ProgramSchedule
{
	public class ProgramDigitalInfoOutputModel : BaseDigitalInfoOutputModel
	{
		public ProgramDigitalInfoOutputModel(ScheduleSection parent) : base(parent.DigitalInfo) { }
	}
}
