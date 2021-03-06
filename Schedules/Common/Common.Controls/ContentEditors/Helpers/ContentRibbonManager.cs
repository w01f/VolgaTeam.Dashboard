﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Schedules.Common.Controls.ContentEditors.Enums;
using Asa.Schedules.Common.Controls.ContentEditors.Events;
using Asa.Schedules.Common.Controls.ContentEditors.Interfaces;
using DevExpress.XtraLayout.Utils;

namespace Asa.Schedules.Common.Controls.ContentEditors.Helpers
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

		public static void ShowRibbonTab(string contentIdentifier, ContentOpenEventArgs args = null)
		{
			var tabPage = _controller.ContentControls.FirstOrDefault(c => c.Identifier == contentIdentifier)?.TabPage;
			_controller.ContentRibbon.SelectedRibbonTabItem = tabPage;
		}

		private static void OnSelectedRibbonTabChanged(object sender, EventArgs e)
		{
			if (_tabChangeInProgress) return;
			try
			{
				_tabChangeInProgress = true;

				_controller.ContentRibbon.Enabled = false;

				var nextControl = _controller.ContentControls
					.First(c => c.Identifier == (String)_controller.ContentRibbon.SelectedRibbonTabItem.Tag);

				if (_controller.ActiveControl is IContentEditControl<TChangeInfo> activeEditor)
				{
					var savingArgs = new ContentSavingEventArgs { SavingReason = ContentSavingReason.TabChanged, RequreScheduleInfoValidation = nextControl.RequreScheduleInfo };
					ContentEditManager<TChangeInfo>.ProcessContentEditChanges(activeEditor, savingArgs);
					if (savingArgs.Cancel)
					{
						_controller.ContentRibbon.SelectedRibbonTabItem = _controller.ActiveControl.TabPage;
						_controller.ContentRibbon.Enabled = true;
						return;
					}
				}
				_controller.MainPanelContainer.Visibility = LayoutVisibility.Never;
				_controller.EmptyPanel.Visibility = LayoutVisibility.Always;
				foreach (var contentControl in _controller.ContentControls)
				{
					contentControl.IsActive = false;
					((Control)contentControl).Visible = false;
				}
				_controller.ActiveControl = nextControl;
				if (!_controller.MainPanel.Control.Controls.Contains((Control)_controller.ActiveControl))
				{
					_controller.ActiveControl.InitBusinessObjects();
					_controller.MainPanel.Control.Controls.Add((Control)_controller.ActiveControl);
					_controller.ActiveControl.InitControl();
				}
				_controller.ActiveControl.ShowControl(e as ContentOpenEventArgs);
				((Control)_controller.ActiveControl).Visible = true;
				((Control)_controller.ActiveControl).BringToFront();
				_controller.EmptyPanel.Visibility = LayoutVisibility.Never;
				_controller.MainPanelContainer.Visibility = LayoutVisibility.Always;

				_controller.ContentRibbon.Enabled = true;
			}
			finally
			{
				_tabChangeInProgress = false;
			}
		}
	}
}
