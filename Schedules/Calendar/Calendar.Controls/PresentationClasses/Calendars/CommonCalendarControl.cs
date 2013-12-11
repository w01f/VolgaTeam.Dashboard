﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.Calendar.Controls.BusinessClasses;
using NewBizWiz.Calendar.Controls.InteropClasses;
using NewBizWiz.Calendar.Controls.PresentationClasses.SlideInfo;
using NewBizWiz.Calendar.Controls.PresentationClasses.Views.GridView;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;
using SettingsManager = NewBizWiz.Core.Calendar.SettingsManager;

namespace NewBizWiz.Calendar.Controls.PresentationClasses.Calendars
{
	public class CommonCalendarControl : BaseCalendarControl
	{
		protected Schedule _localSchedule = null;

		public CommonCalendarControl()
			: base()
		{
			InitSlideInfo<SlideInfoControl>();
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadCalendar(e.QuickSave);
			});
		}

		public override List<ImageSource> DayImages
		{
			get { return Core.AdSchedule.ListManager.Instance.Images; }
		}

		public override ISchedule Schedule
		{
			get { return _localSchedule; }
		}

		public override Form FormMain
		{
			get { return Controller.Instance.FormMain; }
		}

		public override RibbonControl Ribbon
		{
			get { return Controller.Instance.Ribbon; }
		}

		public override ImageListBoxControl MonthList
		{
			get { return Controller.Instance.CalendarVisualizer.MonthsListBoxControl; }
		}

		public override ButtonItem SlideInfoButton
		{
			get { return Controller.Instance.CalendarVisualizer.SlideInfoButtonItem; }
		}

		public override ButtonItem PreviewButton
		{
			get { return Controller.Instance.CalendarVisualizer.PreviewButtonItem; }
		}

		public override ButtonItem EmailButton
		{
			get { return Controller.Instance.CalendarVisualizer.EmailButtonItem; }
		}

		public override ButtonItem PowerPointButton
		{
			get { return Controller.Instance.CalendarVisualizer.PowerPointButtonItem; }
		}

		public override ButtonItem ThemeButton
		{
			get { throw new NotImplementedException(); }
		}

		public override ButtonItem CopyButton
		{
			get { return Controller.Instance.CalendarVisualizer.CopyButtonItem; }
		}

		public override ButtonItem PasteButton
		{
			get { return Controller.Instance.CalendarVisualizer.PasteButtonItem; }
		}

		public override ButtonItem CloneButton
		{
			get { return Controller.Instance.CalendarVisualizer.CloneButtonItem; }
		}

		public override Core.Calendar.Calendar CalendarData
		{
			get
			{
				return _localSchedule.GraphicCalendar;
			}
		}

		public override CalendarSettings CalendarSettings
		{
			get { return SettingsManager.Instance.ViewSettings.GraphicCalendarSettings; }
		}

		public override void LoadCalendar(bool quickLoad)
		{
			_localSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			base.LoadCalendar(quickLoad);
		}

		public override bool SaveCalendarData(string scheduleName = "")
		{
			var result = base.SaveCalendarData(scheduleName);
			Controller.Instance.SaveSchedule(_localSchedule, true, this);
			return result;
		}

		public override void OpenHelp()
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink(SelectedView.GetType() == typeof(GridViewControl) ? "list" : "ninja");
		}

		public override void SaveSettings()
		{
			SettingsManager.Instance.ViewSettings.Save();
		}

		protected override void PowerPointInternal(IEnumerable<CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			using (var formProgress = new FormProgress())
			{
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.TopMost = true;
					formProgress.laProgress.Text = outputData.Count() == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Calendar slides…\nThis will take a few minutes…";
					formProgress.Show();
					Enabled = false;
					CalendarPowerPointHelper.Instance.AppendCalendar(outputData.ToArray());
					Enabled = true;
					formProgress.Close();
				});
			}
		}

		protected override void EmailInternal(IEnumerable<CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			var tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Calendar for Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				Enabled = false;
				CalendarPowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, outputData.ToArray());
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				Enabled = true;
				formProgress.Close();
			}
			if (!File.Exists(tempFileName)) return;
			using (var formEmail = new FormEmail(FormMain, CalendarPowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager))
			{
				formEmail.Text = "Email this Calendar";
				formEmail.PresentationFile = tempFileName;
				RegistryHelper.MainFormHandle = formEmail.Handle;
				RegistryHelper.MaximizeMainForm = false;
				formEmail.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
			}
		}

		protected override void PreviewInternal(IEnumerable<CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			var tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Calendar for Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				Enabled = false;
				CalendarPowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, outputData.ToArray());
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				Enabled = true;
				formProgress.Close();
			}
			if (!File.Exists(tempFileName)) return;
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, CalendarPowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater))
			{
				formPreview.Text = "Preview this Calendar";
				formPreview.PresentationFile = tempFileName;
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
			}
		}
	}
}
