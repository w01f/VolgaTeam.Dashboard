using System;
using Asa.Schedules.Common.Controls.ContentEditors.Events;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.ContentEditors
{
	class SnapshotOpenEventArgs : ContentOpenEventArgs
	{
		public Guid SnapshotId { get; set; }
		public SnapshotEditorType EditorType { get; set; }
	}
}
