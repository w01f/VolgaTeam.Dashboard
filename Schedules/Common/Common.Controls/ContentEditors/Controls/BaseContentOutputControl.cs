using System;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Schedules.Common.Controls.ContentEditors.Events;
using Asa.Schedules.Common.Controls.ContentEditors.Interfaces;
using Asa.Schedules.Common.Controls.ContentEditors.Objects;

namespace Asa.Schedules.Common.Controls.ContentEditors.Controls
{
	public abstract class BaseContentOutputControl<TChangeInfo> : BaseContentEditControl<TChangeInfo>, IThemeManagementControl, IOutputControl
		where TChangeInfo : BaseScheduleChangeInfo
	{
		protected ThemeUpdateInfo ThemeUpdateInfo { get; }

		protected abstract void UpdateMenuOutputButtons();


		public virtual void EditSettings() { throw new NotImplementedException(); }
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
			if (!ContentUpdateInfo.NeedToUpdate)
				UpdateMenuOutputButtons();
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
