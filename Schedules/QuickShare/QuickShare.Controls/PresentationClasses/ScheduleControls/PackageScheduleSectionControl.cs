using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
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

			_parent.QuarterButton.CheckedChanged += QuarterCheckedChanged;
			_parent.SaveButton.Click += Save_Click;
			_parent.SaveAsButton.Click += SaveAs_Click;
			_parent.HelpButton.Click += Help_Click;
			_parent.PowerPointButton.Click += PowerPoint_Click;
			_parent.EmailButton.Click += Email_Click;
			_parent.PreviewButton.Click += Preview_Click;
			_parent.ProgramAddButton.Click += AddProgram_Click;
			_parent.ProgramDeleteButton.Click += DeleteProgram_Click;
			BusinessWrapper.Instance.PackageManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			});
			var buttonInfos = new List<ButtonInfo>();
			buttonInfos.Add(new ButtonInfo { Logo = Resources.SectionSettingsShare, Tooltip = "Open My Share Info", Action = () => { xtraTabControlOptions.SelectedTabPage = xtraTabPageOptionsQuickShare; } });
			buttonInfos.Add(new ButtonInfo { Logo = MediaMetaData.Instance.DataType == MediaDataType.TV ? MediaSchedule.Controls.Properties.Resources.SectionSettingsTV : MediaSchedule.Controls.Properties.Resources.SectionSettingsRadio, Tooltip = String.Format("Open {0} Schedule Settings", MediaMetaData.Instance.DataTypeString), Action = () => { xtraTabControlOptions.SelectedTabPage = xtraTabPageOptionsLine; } });
			buttonInfos.Add(new ButtonInfo { Logo = MediaSchedule.Controls.Properties.Resources.SectionSettingsInfo, Tooltip = "Open Schedule Info", Action = () => { xtraTabControlOptions.SelectedTabPage = xtraTabPageOptionsTotals; } });
			buttonInfos.Add(new ButtonInfo { Logo = MediaSchedule.Controls.Properties.Resources.SectionSettingsOptions, Tooltip = "Open Options", Action = () => { xtraTabControlOptions.SelectedTabPage = xtraTabPageOptionsStyle; } });
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
			laScheduleInfo.Text = String.Format("{0} ({1}){4}{2} ({3})",
				LocalSchedule.BusinessName,
				LocalSchedule.Parent.Name,
				LocalSchedule.FlightDates,
				String.Format("{0} {1}s", ScheduleSection.TotalPeriods, SpotTitle),
				Environment.NewLine);
			base.LoadSchedule(quickLoad);
			xtraScrollableControlColors.Controls.Clear();
			var selectedColor = BusinessWrapper.Instance.OutputManager.AvailableColors.FirstOrDefault(c => c.Name.Equals(MediaMetaData.Instance.SettingsManager.SelectedColor)) ?? BusinessWrapper.Instance.OutputManager.AvailableColors.FirstOrDefault();
			var topPosition = 20;
			foreach (var color in BusinessWrapper.Instance.OutputManager.AvailableColors)
			{
				var button = new ButtonX();
				button.Height = 50;
				button.Width = pnColors.Width - 40;
				button.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
				button.TextAlignment = eButtonTextAlignment.Center;
				button.ColorTable = eButtonColor.OrangeWithBackground;
				button.Style = eDotNetBarStyle.StyleManagerControlled;
				button.Image = color.Logo;
				button.Tag = color;
				button.Checked = color.Name.Equals(selectedColor.Name);
				button.Click += buttonColor_Click;
				button.CheckedChanged += colorButton_CheckedChanged;
				xtraScrollableControlColors.Controls.Add(button);
				button.Location = new Point(20, topPosition);
				topPosition += (button.Height + 20);
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
			using (var form = new FormNewSchedule())
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

		protected override void PowerPointInternal(IEnumerable<OutputScheduleGridBased> outputPages)
		{
			if (outputPages == null || !outputPages.Any()) return;
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.Show();
					QuickSharePowerPointHelper.Instance.AppendOneSheetTableBased(outputPages, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
					TrackOutput();
					formProgress.Close();
				});
			}
		}

		protected override void PreviewInternal(IEnumerable<OutputScheduleGridBased> outputPages)
		{
			if (outputPages == null || !outputPages.Any()) return;
			var tempFileName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				QuickSharePowerPointHelper.Instance.PrepareOneSheetEmailTableBased(tempFileName, outputPages, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
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

		protected override void EmailInternal(IEnumerable<OutputScheduleGridBased> outputPages)
		{
			if (outputPages == null || !outputPages.Any()) return;
			var tempFileName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				QuickSharePowerPointHelper.Instance.PrepareOneSheetEmailTableBased(tempFileName, outputPages, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
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
