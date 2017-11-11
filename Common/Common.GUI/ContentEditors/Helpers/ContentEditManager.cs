using System;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.ContentEditors.Enums;
using Asa.Common.GUI.ContentEditors.Events;
using Asa.Common.GUI.ContentEditors.Interfaces;
using Asa.Common.GUI.ToolForms;

namespace Asa.Common.GUI.ContentEditors.Helpers
{
	public static class ContentEditManager<TChangeInfo>
		where TChangeInfo : BaseScheduleChangeInfo
	{
		public const string DefaultContentIdentifier = "Default";
		private static IContentController<TChangeInfo> _controller;

		public static event EventHandler<ScheduleSavingEventArgs> ScheduleSaving;
		public static event EventHandler<ScheduleSavingEventArgs> ScheduleSavingAs;
		public static event EventHandler<ScheduleSavingEventArgs> ScheduleSavingTemplate;

		public static void Init(IContentController<TChangeInfo> controller)
		{
			_controller = controller;

			foreach (var contentEditControl in _controller.ContentControls.OfType<IContentEditControl<TChangeInfo>>())
				contentEditControl.ContentChanged += OnContentChanged;

			_controller.ContentRibbon.Items.Clear();
			_controller.ContentRibbon.Items.AddRange(_controller.ContentControls.Select(c => c.TabPage).ToArray());

			ContentRibbonManager<TChangeInfo>.Init(_controller);
		}

		public static void OnScheduleOpened(object sender, EventArgs e)
		{
			var changeInfo = Activator.CreateInstance<TChangeInfo>();
			changeInfo.WholeScheduleChanged = true;
			OnContentChanged(
				null,
				new ContentSavedEventArgs<TChangeInfo>(
					DefaultContentIdentifier,
					changeInfo,
					ContentSavingReason.ScheduleChanged));
		}

		private static void OnContentChanged(object sender, ContentSavedEventArgs<TChangeInfo> e)
		{
			if (e.SavingReason != ContentSavingReason.ScheduleChanged)
				ScheduleSaving?.Invoke(null, new ScheduleSavingEventArgs());
			if (e.SavingReason == ContentSavingReason.TabChanged ||
				e.SavingReason == ContentSavingReason.ScheduleChanged ||
				e.SavingReason == ContentSavingReason.ScheduleSaved)
				foreach (var destinationEditorId in _controller.EditorRelations.Where(l => e.Source.Contains(l.Target)).SelectMany(l => l.Destrination))
				{
					var destinationEditor = _controller.ContentControls.OfType<IContentEditControl<TChangeInfo>>().FirstOrDefault(editor => editor.Identifier == destinationEditorId);
					destinationEditor?.OnRelatedContentChanged(e.ChangeInfo);
				}
		}

		public static void RaiseThemeChanged(IThemeManagementControl sender)
		{
			foreach (var contentOutputControl in _controller.ContentControls.OfType<IThemeManagementControl>().Where(c => c != sender))
				contentOutputControl.OnOuterThemeChanged();
		}

		public static void OnThemeChanged(object sender, EventArgs e)
		{
			RaiseThemeChanged(sender as IThemeManagementControl);
		}

		public static void ProcessContentEditChanges(IContentEditControl<TChangeInfo> contentEditor, ContentSavingEventArgs savingArgs)
		{
			if (contentEditor == null) return;
			contentEditor.Saving(savingArgs);
			if (savingArgs.Cancel)
			{
				if (savingArgs.ErrorMessages.Any() && savingArgs.SavingReason != ContentSavingReason.AppClosing)
					PopupMessageHelper.Instance.ShowWarning(String.Join(Environment.NewLine, savingArgs.ErrorMessages));
				return;
			}
			contentEditor.Save(savingArgs);
		}

		public static void SaveSchedule(bool silent = false)
		{
			if (_controller.ActiveEditor == null) return;
			var savingArgs = new ContentSavingEventArgs { SavingReason = ContentSavingReason.ScheduleSaved };
			ProcessContentEditChanges(_controller.ActiveEditor, savingArgs);
			if (!savingArgs.Cancel && !silent)
				PopupMessageHelper.Instance.ShowInformation("Data Saved");
		}

		public static void SaveScheduleAs()
		{
			if (_controller.ActiveEditor == null) return;
			var savingArgs = new ContentSavingEventArgs { SavingReason = ContentSavingReason.ScheduleSaved };
			_controller.ActiveEditor.Saving(savingArgs);
			if (savingArgs.Cancel)
			{
				if (savingArgs.ErrorMessages.Any() && savingArgs.SavingReason != ContentSavingReason.AppClosing)
					PopupMessageHelper.Instance.ShowWarning(String.Join(Environment.NewLine, savingArgs.ErrorMessages));
				return;
			}
			using (var form = new FormScheduleName(true))
			{
				if (form.ShowDialog() != DialogResult.OK) return;
				var scheduleSavingArgs = new ScheduleSavingEventArgs
				{
					Name = form.ScheduleName,
				};
				ScheduleSavingAs?.Invoke(null, scheduleSavingArgs);
				_controller.ActiveEditor.Save(savingArgs);
				if (form.checkEditSaveAsTemplate.Checked)
				{
					if (!FileStorageManager.Instance.UseLocalMode)
					{
						ScheduleSavingTemplate?.Invoke(null, scheduleSavingArgs);
						PopupMessageHelper.Instance.ShowInformation("Data saved to the cloud");
					}
					else
						PopupMessageHelper.Instance.ShowWarning("Cloud is not available");
				}
				else
					PopupMessageHelper.Instance.ShowInformation("Data Saved");
			}
		}

		public static void OutputPowerPoint()
		{
			if (_controller.ActiveOutputControl == null) return;
			SaveSchedule(true);
			_controller.ActiveOutputControl.OutputPowerPoint();
		}

		public static void OutputPdf()
		{
			if (_controller.ActiveOutputControl == null) return;
			SaveSchedule(true);
			_controller.ActiveOutputControl.OutputPdf();
		}

		public static void Preview()
		{
			if (_controller.ActiveOutputControl == null) return;
			SaveSchedule(true);
			_controller.ActiveOutputControl.Preview();
		}

		public static void Email()
		{
			if (_controller.ActiveOutputControl == null) return;
			SaveSchedule(true);
			_controller.ActiveOutputControl.Email();
		}
	}
}
