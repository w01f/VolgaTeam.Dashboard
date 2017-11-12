using System;
using Asa.Schedules.Common.Controls.ContentEditors.Events;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.ContentEditors
{
	class OptionsSetOpenEventArgs : ContentOpenEventArgs
	{
		public Guid OptionsSetId { get; set; }
		public OptionEditorType EditorType { get; set; }
	}
}
