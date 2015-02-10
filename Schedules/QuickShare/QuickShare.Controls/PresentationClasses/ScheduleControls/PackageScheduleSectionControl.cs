using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.Utils;
using NewBizWiz.CommonGUI.Common;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.RetractableBar;
using NewBizWiz.CommonGUI.Themes;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.Core.QuickShare;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using NewBizWiz.MediaSchedule.Controls.PresentationClasses.ScheduleControls;
using NewBizWiz.QuickShare.Controls.InteropClasses;
using NewBizWiz.QuickShare.Controls.Properties;
using BusinessWrapper = NewBizWiz.QuickShare.Controls.BusinessClasses.BusinessWrapper;

namespace NewBizWiz.QuickShare.Controls.PresentationClasses.ScheduleControls
{
	public class PackageScheduleSectionControl : ScheduleSectionControl
	{
		private readonly SchedulePage _parent;

		protected PackageSchedule LocalSchedule
		{
			get { return _localSchedule as PackageSchedule; }
		}

		public override ButtonItem ThemeButton
		{
			get { return _parent.ThemeButton; }
		}
		public override ButtonItem PowerPointButton
		{
			get { return _parent.PowerPointButton; }
		}
		public override ButtonItem EmailButton
		{
			get { return _parent.EmailButton; }
		}
		public override ButtonItem PreviewButton
		{
			get { return _parent.PreviewButton; }
		}
		public override RibbonBar QuarterBar
		{
			get { return _parent.QuarterBar; }
		}
		public override ButtonItem QuarterButton
		{
			get { return _parent.QuarterButton; }
		}

		public PackageScheduleSectionControl(SchedulePage parent)
		{
			_parent = parent;
			_localSchedule = _parent.Schedule;
			xtraTabPageOptionsDigital.PageVisible = false;
			xtraTabControlData.ShowTabHeader = DefaultBoolean.True;
			xtraTabPageDataSchedule.Text = String.Format("Schedule - {0}", _localSchedule.Name);
			_parent.QuarterButton.CheckedChanged += QuarterCheckedChanged;
			_parent.SaveButton.Click += Save_Click;
			_parent.SaveAsButton.Click += SaveAs_Click;
			_parent.HelpButton.Click += Help_Click;
			_parent.PowerPointButton.AddEventHandler(Controller.Instance.CheckPowerPointRunning, PowerPoint_Click);
			_parent.EmailButton.AddEventHandler(Controller.Instance.CheckPowerPointRunning, Email_Click);
			_parent.PreviewButton.AddEventHandler(Controller.Instance.CheckPowerPointRunning, Preview_Click);
			_parent.ProgramAddButton.Click += AddProgram_Click;
			_parent.ProgramDeleteButton.Click += DeleteProgram_Click;
			BusinessWrapper.Instance.PackageManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.BeginInvoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			});
			var buttonInfos = new List<ButtonInfo>
			{
				new ButtonInfo { 
					Logo = Resources.SectionSettingsShare, 
					Tooltip = "Open My Share Info", 
					Action = () => { xtraTabControlOptions.SelectedTabPage = xtraTabPageOptionsQuickShare; } 
				}, 
				new ButtonInfo { 
					Logo = MediaMetaData.Instance.DataType == MediaDataType.TV ? MediaSchedule.Controls.Properties.Resources.SectionSettingsTV : MediaSchedule.Controls.Properties.Resources.SectionSettingsRadio, 
					Tooltip = String.Format("Open {0} Schedule Settings", MediaMetaData.Instance.DataTypeString), 
					Action = () => { xtraTabControlOptions.SelectedTabPage = xtraTabPageOptionsLine; } 
				}, 
				new ButtonInfo
				{
					Logo = MediaSchedule.Controls.Properties.Resources.SectionSettingsInfo, 
					Tooltip = "Open Schedule Info", 
					Action = () => { xtraTabControlOptions.SelectedTabPage = xtraTabPageOptionsTotals; }
				}, 
				new ButtonInfo
				{
					Logo = MediaSchedule.Controls.Properties.Resources.SectionSettingsOptions, 
					Tooltip = "Open Options", 
					Action = () => { xtraTabControlOptions.SelectedTabPage = xtraTabPageOptionsStyle; }
				}
			};
			retractableBarControl.AddButtons(buttonInfos);
		}

		#region Methods
		public override void LoadSchedule(bool quickLoad)
		{
			FormThemeSelector.Link(ThemeButton, BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType), MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType), (t =>
			{
				MediaMetaData.Instance.SettingsManager.SetSelectedTheme(SlideType, t.Name);
				MediaMetaData.Instance.SettingsManager.SaveSettings();
				SettingsNotSaved = true;
			}));
			_parent.PowerPointBar.RecalcLayout();
			_parent.Panel.PerformLayout();
			labelControlScheduleInfo.Text = String.Format("{0} ({1}){4}<color=gray><i>{2} ({3})</i></color>",
				LocalSchedule.BusinessName,
				LocalSchedule.Parent.Name,
				LocalSchedule.FlightDates,
				String.Format("{0} {1}s", ScheduleSection.TotalPeriods, SpotTitle),
				Environment.NewLine);
			base.LoadSchedule(quickLoad);
			if (!quickLoad)
			{
				outputColorSelector.InitData(BusinessWrapper.Instance.OutputManager.ScheduleColors, MediaMetaData.Instance.SettingsManager.SelectedColor);
				outputColorSelector.ColorChanged += OnColorChanged;
			}
		}

		protected override bool SaveSchedule(string scheduleName = "")
		{
			var nameChanged = !string.IsNullOrEmpty(scheduleName);
			if (nameChanged)
				LocalSchedule.Parent.Name = scheduleName;
			var result = base.SaveSchedule(scheduleName);
			Controller.Instance.SavePackage(LocalSchedule.Parent, nameChanged, false, this);
			return result;
		}

		protected override void AddActivity(UserActivity activity)
		{
			BusinessWrapper.Instance.ActivityManager.AddActivity(activity);
		}
		#endregion

		#region Ribbon Operations Events
		public override void Help_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink(_helpKey);
		}

		public override void Save_Click(object sender, EventArgs e)
		{
			if (SaveSchedule())
				Utilities.Instance.ShowInformation("Schedule Saved");
		}

		public override void SaveAs_Click(object sender, EventArgs e)
		{
			using (var form = new FormNewSchedule(PackageManager.GetShortPackageList().Select(p => p.ShortFileName)))
			{
				form.Text = "Save Schedule";
				form.laLogo.Text = "Please set a new name for your Schedule:";
				if (form.ShowDialog() != DialogResult.OK) return;
				if (!string.IsNullOrEmpty(form.ScheduleName))
				{
					if (SaveSchedule(form.ScheduleName))
						Utilities.Instance.ShowInformation("Schedule was saved");
				}
				else
					Utilities.Instance.ShowWarning("Schedule Name can't be empty");
			}
		}
		#endregion

		#region Output Staff
		public override Theme SelectedTheme
		{
			get { return BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType).FirstOrDefault(t => t.Name.Equals(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType)) || String.IsNullOrEmpty(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType))); }
		}

		protected override void PowerPointInternal(IEnumerable<OutputSchedule> outputPages)
		{
			if (outputPages == null || !outputPages.Any()) return;
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.Show();
					QuickSharePowerPointHelper.Instance.AppendOneSheet(outputPages, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
					TrackOutput();
					formProgress.Close();
				});
			}
		}

		protected override void PreviewInternal(IEnumerable<OutputSchedule> outputPages)
		{
			if (outputPages == null || !outputPages.Any()) return;
			var tempFileName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				QuickSharePowerPointHelper.Instance.PrepareOneSheetEmail(tempFileName, outputPages, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				formProgress.Close();
			}
			if (!File.Exists(tempFileName)) return;
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, QuickSharePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater, TrackPreview))
			{
				formPreview.Text = "Preview Schedule";
				formPreview.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
			}
		}

		protected override void EmailInternal(IEnumerable<OutputSchedule> outputPages)
		{
			if (outputPages == null || !outputPages.Any()) return;
			var tempFileName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				QuickSharePowerPointHelper.Instance.PrepareOneSheetEmail(tempFileName, outputPages, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
				formProgress.Close();
			}
			if (!File.Exists(tempFileName)) return;
			using (var formEmail = new FormEmail(QuickSharePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager))
			{
				formEmail.Text = "Email this Schedule";
				formEmail.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				RegistryHelper.MainFormHandle = formEmail.Handle;
				RegistryHelper.MaximizeMainForm = false;
				formEmail.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
			}
		}
		#endregion
	}
}
