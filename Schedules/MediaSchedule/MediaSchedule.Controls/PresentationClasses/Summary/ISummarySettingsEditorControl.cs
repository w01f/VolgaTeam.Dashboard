using Asa.Business.Common.Entities.NonPersistent.Summary;

namespace Asa.Media.Controls.PresentationClasses.Summary
{
	public interface ISummarySettingsEditorControl : IContentSettingsEditorControl
	{
		void LoadData(BaseSummarySettings dataSource);
	}

}
