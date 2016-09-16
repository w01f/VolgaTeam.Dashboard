using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Common.GUI.ContentEditors.Events;
using Asa.Common.GUI.ContentEditors.Interfaces;
using Asa.Common.GUI.ContentEditors.Objects;

namespace Asa.Common.GUI.ContentEditors.Controls
{
	public abstract class BaseContentOutputControl<TChangeInfo> : BaseContentEditControl<TChangeInfo>, IThemeManagementControl, IOutputControl
		where TChangeInfo : BaseScheduleChangeInfo
	{
		protected ThemeUpdateInfo ThemeUpdateInfo { get; }

		public abstract void OutputPowerPoint();
		public abstract void OutputPdf();
		public abstract void Preview();
		public abstract void Email();

		protected BaseContentOutputControl()
		{
			ThemeUpdateInfo = new ThemeUpdateInfo();
		}

		public override void ShowControl(ContentOpenEventArgs args = null)
		{
			base.ShowControl(args);
			if (ThemeUpdateInfo.NeedToUpdate)
				LoadThemes();
		}

		protected virtual void LoadThemes()
		{
			ThemeUpdateInfo.NeedToUpdate = false;
		}

		public virtual void OnOuterThemeChanged()
		{
			ThemeUpdateInfo.NeedToUpdate = true;
			if (IsActive)
				LoadThemes();
		}
	}
}
