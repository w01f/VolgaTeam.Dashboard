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
	public abstract class BaseContentEditControl<TChangeInfo> : PartitionEditTemplateControl, IContentEditControl<TChangeInfo>
		where TChangeInfo : BaseScheduleChangeInfo
	{
		protected ContentUpdateInfo<TChangeInfo> ContentUpdateInfo { get; private set; }
		public bool SettingsNotSaved { get; set; }
		public bool IsActive { get; set; }
		public abstract string Identifier { get; }
		public abstract RibbonTabItem TabPage { get; }
		protected TChangeInfo ChangeInfo { get; private set; }

		public event EventHandler<ContentSavedEventArgs<TChangeInfo>> ContentChanged;

		protected abstract void UpdateEditedContet();
		protected abstract void ApplyChanges();
		public abstract void GetHelp();

		protected BaseContentEditControl()
		{
			ContentUpdateInfo = new ContentUpdateInfo<TChangeInfo>();
			ChangeInfo = Activator.CreateInstance<TChangeInfo>();
		}

		public virtual void InitControl()
		{
			TabPage.Tag = Identifier;
		}

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
			if (handler != null)
				handler(this, e);
		}
	}
}
