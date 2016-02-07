using System;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Common.Interfaces;
using Asa.Common.GUI.ContentEditors.Events;
using Asa.Common.GUI.ContentEditors.Interfaces;
using Asa.Common.GUI.ContentEditors.Objects;

namespace Asa.Common.GUI.ContentEditors.Controls
{
	public abstract class BasePartitionEditControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo> : BaseContentEditControl<TChangeInfo>, IPartitionEditControl<TChangeInfo>, IOutputControl
		where TPartitionContet : BaseSchedulePartitionContent<TSchedule, TScheduleSettings>
		where TSchedule : ISchedule<TScheduleSettings>
		where TScheduleSettings : IBaseScheduleSettings
		where TChangeInfo : BaseScheduleChangeInfo
	{
		protected bool IsThemeChanged { get; set; }
		protected ThemeUpdateInfo ThemeUpdateInfo { get; private set; }
		public TPartitionContet EditedContent { get; set; }

		public event EventHandler<EventArgs> ThemeChanged;

		public abstract void OutputPowerPoint();
		public abstract void OutputPdf();
		public abstract void Preview();
		public abstract void Email();

		protected BasePartitionEditControl()
		{
			ThemeUpdateInfo = new ThemeUpdateInfo();
		}

		public override void ShowControl()
		{
			base.ShowControl();
			if (ThemeUpdateInfo.NeedToUpdate)
				LoadThemes();
		}

		public override void Save(ContentSavingEventArgs savingArgs)
		{
			base.Save(savingArgs);
			if (IsThemeChanged)
				OnThemeChanged();
		}

		protected virtual void LoadThemes()
		{
			ThemeUpdateInfo.NeedToUpdate = false;
		}

		protected virtual void OnThemeChanged()
		{
			var handler = ThemeChanged;
			if (handler != null) handler(this, EventArgs.Empty);
		}

		public virtual void OnOuterThemeChanged()
		{
			ThemeUpdateInfo.NeedToUpdate = true;
			if (IsActive)
				LoadThemes();
		}
	}
}
