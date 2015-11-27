using Asa.Core.Common;

namespace Asa.MediaSchedule.Controls.PresentationClasses.Summary
{
	public interface ISummarySettingsEditorControl : IContentSettingsEditorControl
	{
		void LoadData(BaseSummarySettings dataSource);
	}

}
