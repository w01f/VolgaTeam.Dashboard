using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using NewBizWiz.MediaSchedule.Controls.InteropClasses;
using NewBizWiz.MediaSchedule.Controls.Properties;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.ScheduleControls
{
	public class RegularScheduleSectionControl : ScheduleSectionControl
	{
		protected RegularSchedule LocalSchedule
		{
			get { return _localSchedule as RegularSchedule; }
		}

		public RegularScheduleSectionControl()
		{
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.BeginInvoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			});
			digitalInfoControl.RequestDefaultInfo += (o, args) =>
			{
				args.Editor.EditValue = LocalSchedule.GetDigitalInfo(args);
				args.Editor.Tag = args.Editor.EditValue;
			};
			digitalInfoControl.SettingsChanged += (o, args) =>
			{
				TrackOptionChanged();
				SettingsNotSaved = true;
			};
			xtraTabPageOptionsQuickShare.PageVisible = false;
		}

		#region Methods
		public override void LoadSchedule(bool quickLoad)
		{
			_localSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			FormThemeSelector.Link(ThemeButton, BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType), MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType), (t =>
			{
				MediaMetaData.Instance.SettingsManager.SetSelectedTheme(SlideType, t.Name);
				MediaMetaData.Instance.SettingsManager.SaveSettings();
				SettingsNotSaved = true;
			}));
			labelControlScheduleInfo.Text = String.Format("{0}{3}<color=gray><i>{1} ({2})</i></color>",
				_localSchedule.BusinessName,
				_localSchedule.FlightDates,
				String.Format("{0} {1}s", ScheduleSection.TotalPeriods, SpotTitle),
				Environment.NewLine);
			base.LoadSchedule(quickLoad);

			if (!quickLoad)
			{
				xtraTabPageOptionsStyle.PageVisible = BusinessWrapper.Instance.OutputManager.ScheduleColors.Items.Count > 1;
				outputColorSelector.InitData(BusinessWrapper.Instance.OutputManager.ScheduleColors, MediaMetaData.Instance.SettingsManager.SelectedColor);
				outputColorSelector.ColorChanged += OnColorChanged;
			}

			xtraTabPageOptionsDigital.PageEnabled = LocalSchedule.DigitalProducts.Any();
			digitalInfoControl.InitData(ScheduleSection.DigitalLegend);

			var buttonInfos = new List<ButtonInfo>();
			buttonInfos.Add(new ButtonInfo { Logo = MediaMetaData.Instance.DataType == MediaDataType.TV ? Resources.SectionSettingsTV : Resources.SectionSettingsRadio, Tooltip = String.Format("Open {0} Schedule Settings", MediaMetaData.Instance.DataTypeString), Action = () => { xtraTabControlOptions.SelectedTabPage = xtraTabPageOptionsLine; } });
			if (LocalSchedule.DigitalProducts.Any())
				buttonInfos.Add(new ButtonInfo { Logo = Resources.SectionSettingsDigital, Tooltip = "Open Digital Settings", Action = () => { xtraTabControlOptions.SelectedTabPage = xtraTabPageOptionsDigital; } });
			buttonInfos.Add(new ButtonInfo { Logo = Resources.SectionSettingsInfo, Tooltip = "Open Schedule Info", Action = () => { xtraTabControlOptions.SelectedTabPage = xtraTabPageOptionsTotals; } });
			if (BusinessWrapper.Instance.OutputManager.ScheduleColors.Items.Count > 1)
				buttonInfos.Add(new ButtonInfo { Logo = Resources.SectionSettingsOptions, Tooltip = "Open Slide Style", Action = () => { xtraTabControlOptions.SelectedTabPage = xtraTabPageOptionsStyle; } });
			retractableBarControl.AddButtons(buttonInfos);
		}

		protected override bool SaveSchedule(string scheduleName = "")
		{
			var quickLoad = !SettingsNotSaved && LocalSchedule.BroadcastCalendar.DataSourceType == BroadcastDataTypeEnum.Snapshots;
			LocalSchedule.BroadcastCalendar.UpdateDataSource();
			var nameChanged = !string.IsNullOrEmpty(scheduleName);
			if (nameChanged)
				_localSchedule.Name = scheduleName;
			var result = base.SaveSchedule(scheduleName);
			digitalInfoControl.SaveData();
			Controller.Instance.SaveSchedule(LocalSchedule, nameChanged, quickLoad, false, false, this);
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
			using (var form = new FormNewSchedule(ScheduleManager.GetShortScheduleList().Select(s => s.ShortFileName)))
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
		public override string DigitalLegend
		{
			get
			{
				if (!ScheduleSection.DigitalLegend.Enabled) return String.Empty;
				var requestOptions = ScheduleSection.DigitalLegend.RequestOptions;
				if (!ScheduleSection.DigitalLegend.AllowEdit)
				{
					requestOptions.Separator = " ";
					return String.Format("Digital Product Info: {0}{1}{2}", LocalSchedule.GetDigitalInfo(requestOptions), requestOptions.Separator, ScheduleSection.DigitalLegend.GetAdditionalData(" "));
				}
				if (!String.IsNullOrEmpty(ScheduleSection.DigitalLegend.CompiledInfo))
					return String.Format("Digital Product Info: {0}{1}{2}", ScheduleSection.DigitalLegend.CompiledInfo, requestOptions.Separator, ScheduleSection.DigitalLegend.GetAdditionalData(" "));
				return String.Empty;
			}
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
					RegularMediaSchedulePowerPointHelper.Instance.AppendOneSheet(outputPages, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
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
				RegularMediaSchedulePowerPointHelper.Instance.PrepareOneSheetEmail(tempFileName, outputPages, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				formProgress.Close();
			}
			if (!File.Exists(tempFileName)) return;
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, RegularMediaSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater, TrackPreview))
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

		protected override void PdfInternal(IEnumerable<OutputSchedule> outputPages)
		{
			if (outputPages == null || !outputPages.Any()) return;
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.Show();
					var pdfFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}-{1}.pdf", _localSchedule.Name, DateTime.Now.ToString("MM-dd-yy-hmmss")));
					RegularMediaSchedulePowerPointHelper.Instance.PrepareOneSheetPdf(pdfFileName, outputPages, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
					if (File.Exists(pdfFileName))
						try
						{
							Process.Start(pdfFileName);
						}
						catch { }
					TrackOutput();
					formProgress.Close();
				});
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
				RegularMediaSchedulePowerPointHelper.Instance.PrepareOneSheetEmail(tempFileName, outputPages, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
				formProgress.Close();
			}
			if (!File.Exists(tempFileName)) return;
			using (var formEmail = new FormEmail(RegularMediaSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager))
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
