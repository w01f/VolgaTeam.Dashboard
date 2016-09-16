using System;
using Asa.Common.GUI.ContentEditors.Events;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.ContentEditors
{
	class SnapshotOpenEventArgs : ContentOpenEventArgs
	{
		public Guid SnapshotId { get; set; }
		public SnapshotEditorType EditorType { get; set; }
	}
}
