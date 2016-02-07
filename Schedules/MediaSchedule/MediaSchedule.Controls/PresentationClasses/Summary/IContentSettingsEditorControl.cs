using System;

namespace Asa.Media.Controls.PresentationClasses.Summary
{
	public interface IContentSettingsEditorControl
	{
		event EventHandler<EventArgs> DataChanged;
	}
}
