using System;
using System.Windows.Forms;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Common.GUI.ContentEditors.Events;
using Asa.Common.GUI.ContentEditors.Interfaces;
using Asa.Common.GUI.ContentEditors.Objects;
using Asa.Common.GUI.ToolForms;
using DevComponents.DotNetBar;

namespace Asa.Common.GUI.ContentEditors.Controls
{
	public abstract class BaseContentEditControl<TChangeInfo> : UserControl, IContentEditControl<TChangeInfo>
		where TChangeInfo : BaseScheduleChangeInfo
	{
		protected ContentUpdateInfo<TChangeInfo> ContentUpdateInfo { get; }
		public bool SettingsNotSaved { get; set; }
		public bool IsActive { get; set; }
		public abstract string Identifier { get; }
		public abstract RibbonTabItem TabPage { get; }
		protected TChangeInfo ChangeInfo { get; }

		public event EventHandler<ContentSavedEventArgs<TChangeInfo>> ContentChanged;

		protected abstract void UpdateEditedContet();
		protected abstract void ApplyChanges();
		public abstract void GetHelp();

		protected BaseContentEditControl()
		{
			Dock = DockStyle.Fill;
			ContentUpdateInfo = new ContentUpdateInfo<TChangeInfo>();
			ChangeInfo = Activator.CreateInstance<TChangeInfo>();
		}

		public virtual void InitMetaData()
		{
			TabPage.Tag = Identifier;
		}

		public virtual void InitControl() { }

		public virtual void ShowControl()
		{
			IsActive = true;
			if (ContentUpdateInfo.NeedToUpdate)
				LoadData();
		}

		public virtual void Saving(ContentSavingEventArgs savingArgs)
		{
			if (SettingsNotSaved)
			{
				ApplyChanges();
				ValidateChanges(savingArgs);
			}
		}

		public virtual void Save(ContentSavingEventArgs savingArgs)
		{
			if (SettingsNotSaved)
			{
				Application.DoEvents();
				FormProgress.ShowProgress("Saving Data...", SaveData);
				SettingsNotSaved = false;
				OnContentChanged(new ContentSavedEventArgs<TChangeInfo>(Identifier, ChangeInfo, savingArgs.SavingReason));
			}
		}

		private void LoadData()
		{
			Application.DoEvents();
			FormProgress.ShowProgress("Loading Data...", () =>
			{
				ChangeInfo.Reset();
				UpdateEditedContet();
			});
			ContentUpdateInfo.NeedToUpdate = false;
		}

		protected virtual void SaveData() { }

		protected virtual void ValidateChanges(ContentSavingEventArgs savingArgs)
		{
			savingArgs.Cancel = false;
		}

		public virtual void OnRelatedContentChanged(TChangeInfo changeInfo)
		{
			ContentUpdateInfo.NeedToUpdate = true;
			ContentUpdateInfo.ChangeInfo.Merge(changeInfo);
			if (IsActive)
				LoadData();
		}

		protected virtual void OnContentChanged(ContentSavedEventArgs<TChangeInfo> e)
		{
			var handler = ContentChanged;
			handler?.Invoke(this, e);
		}
	}
}
