using DevComponents.DotNetBar;

namespace Asa.Schedules.Common.Controls.ContentEditors.Interfaces
{
	public interface IMultipleSlidesOutputControl : IOutputControl
	{
		void OutputPowerPointBeforePopup(PopupOpenEventArgs e);
		void OutputPowerPointAll();
	}
}
