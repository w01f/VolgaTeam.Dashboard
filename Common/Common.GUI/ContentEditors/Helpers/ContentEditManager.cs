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

		public static void Init(IContentController<TChangeInfo> controller)
		{
			_controller = controller;

			foreach (var contentEditControl in _controller.ContentControls.OfType<IContentEditControl<TChangeInfo>>())
				contentEditControl.ContentChanged += OnContentChanged;

			foreach (var partitionEditControl in _controller.ContentControls.OfType<IPartitionEditControl<TChangeInfo>>())
				partitionEditControl.ThemeChanged += OnThemeChanged;

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
			if (e.SavingReason != ContentSavingReason.ScheduleChanged && ScheduleSaving != null)
				ScheduleSaving(null, new ScheduleSavingEventArgs());
			if (e.SavingReason == ContentSavingReason.TabChanged ||
				e.SavingReason == ContentSavingReason.ScheduleChanged ||
				e.SavingReason == ContentSavingReason.ScheduleSaved)
				foreach (var destinationEditorId in _controller.EditorRelations.Where(l => e.Source.Contains(l.Target)).SelectMany(l => l.Destrination))
				{
					var destinationEditor = _controller.ContentControls.OfType<IContentEditControl<TChangeInfo>>().FirstOrDefault(editor => editor.Identifier == destinationEditorId);
					if (destinationEditor == null) continue;
					destinationEditor.OnRelatedContentChanged(e.ChangeInfo);
				}
		}

		private static void OnThemeChanged(object sender, EventArgs e)
		{
			foreach (var partitionEditControl in _controller.ContentControls.OfType<IPartitionEditControl<TChangeInfo>>().Where(c => c != sender))
				partitionEditControl.OnOuterThemeChanged();
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
			using (var form = new FormScheduleName())
			{
				form.Text = "Save Schedule";
				form.laLogo.Text = "Please set a new name for your Schedule:";
				if (form.ShowDialog() != DialogResult.OK) return;
				if (ScheduleSavingAs != null)
					ScheduleSavingAs(null, new ScheduleSavingEventArgs() { Name = form.ScheduleName });
				_controller.ActiveEditor.Save(savingArgs);
				PopupMessageHelper.Instance.ShowInformation("Data Saved");
			}
		}

		public static void OutputPowerPoint()
		{
			var outputControl = _controller.ActiveControl as IOutputControl;
			if (outputControl == null) return;
			SaveSchedule(true);
			outputControl.OutputPowerPoint();
		}

		public static void OutputPdf()
		{
			var outputControl = _controller.ActiveControl as IOutputControl;
			if (outputControl == null) return;
			SaveSchedule(true);
			outputControl.OutputPdf();
		}

		public static void Preview()
		{
			var outputControl = _controller.ActiveControl as IOutputControl;
			if (outputControl == null) return;
			SaveSchedule(true);
			outputControl.Preview();
		}

		public static void Email()
		{
			var outputControl = _controller.ActiveControl as IOutputControl;
			if (outputControl == null) return;
			SaveSchedule(true);
			outputControl.Email();
		}
	}
}
