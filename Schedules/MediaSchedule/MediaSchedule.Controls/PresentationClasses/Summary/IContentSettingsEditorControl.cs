using System;

namespace Asa.MediaSchedule.Controls.PresentationClasses.Summary
{
	public interface IContentSettingsEditorControl
	{
		event EventHandler<EventArgs> DataChanged;
	}
}
