using System.Collections.Generic;
using System.Windows.Forms;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Common.GUI.ContentEditors.Objects;
using DevComponents.DotNetBar;

namespace Asa.Common.GUI.ContentEditors.Interfaces
{
	public interface IContentController<TChangeInfo>
		where TChangeInfo : BaseScheduleChangeInfo
	{
		IContentControl ActiveControl { get; set; }
		IContentEditControl<TChangeInfo> ActiveEditor { get; }
		IOutputControl ActiveOutputControl { get; }
		List<IContentControl> ContentControls { get; }
		List<ContentEditorRelation> EditorRelations { get; }
		RibbonControl ContentRibbon { get; }
		Panel MainPanel { get; }
		Panel EmptyPanel { get; }
	}
}
