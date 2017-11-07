using System.Collections.Generic;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Common.GUI.ContentEditors.Objects;
using DevComponents.DotNetBar;
using DevExpress.XtraLayout;

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
		LayoutControlItem MainPanel { get; }
		LayoutControlItem EmptyPanel { get; }
	}
}
