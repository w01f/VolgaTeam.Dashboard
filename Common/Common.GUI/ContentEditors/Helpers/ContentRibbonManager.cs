using System;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Common.GUI.ContentEditors.Enums;
using Asa.Common.GUI.ContentEditors.Events;
using Asa.Common.GUI.ContentEditors.Interfaces;

namespace Asa.Common.GUI.ContentEditors.Helpers
{
	public static class ContentRibbonManager<TChangeInfo>
		where TChangeInfo : BaseScheduleChangeInfo
	{
		private static IContentController<TChangeInfo> _controller;
		private static bool _tabChangeInProgress;

		public static void Init(IContentController<TChangeInfo> controller)
		{
			_controller = controller;
			_controller.ContentRibbon.SelectedRibbonTabChanged += OnSelectedRibbonTabChanged;
		}

		public static void RaiseTabChanged()
		{
			OnSelectedRibbonTabChanged(null, EventArgs.Empty);
		}

		private static void OnSelectedRibbonTabChanged(object sender, EventArgs e)
		{
			if (_tabChangeInProgress) return;
			try
			{
				_tabChangeInProgress = true;

				_controller.ContentRibbon.Enabled = false;

				var activeEditor = _controller.ActiveControl as IContentEditControl<TChangeInfo>;
				if (activeEditor != null)
				{
					var savingArgs = new ContentSavingEventArgs { SavingReason = ContentSavingReason.TabChanged };
					ContentEditManager<TChangeInfo>.ProcessContentEditChanges(activeEditor, savingArgs);
					if (savingArgs.Cancel)
					{
						_controller.ContentRibbon.SelectedRibbonTabItem = _controller.ActiveControl.TabPage;
						_controller.ContentRibbon.Enabled = true;
						return;
					}
				}
				_controller.EmptyPanel.BringToFront();
				foreach (var contentControl in _controller.ContentControls)
					contentControl.IsActive = false;
				_controller.ActiveControl = _controller.ContentControls
					.First(c => c.Identifier == (String)_controller.ContentRibbon.SelectedRibbonTabItem.Tag);
				if (!_controller.MainPanel.Controls.Contains((Control) _controller.ActiveControl))
				{
					_controller.MainPanel.Controls.Add((Control) _controller.ActiveControl);
					_controller.ActiveControl.InitControl();
				}
				_controller.ActiveControl.ShowControl();
				((Control)_controller.ActiveControl).BringToFront();
				_controller.MainPanel.BringToFront();

				_controller.ContentRibbon.Enabled = true;
			}
			finally
			{
				_tabChangeInProgress = false;
			}
		}
	}
}
