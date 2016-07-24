using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Asa.Business.Dashboard.Dictionaries;
using Asa.Business.Dashboard.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.ToolForms;
using DevComponents.DotNetBar;
using Asa.Dashboard.InteropClasses;

namespace Asa.Dashboard.TabHomeForms
{
	[ToolboxItem(false)]
	public sealed partial class SlideClientGoalsControl : SlideBaseControl
	{
		private bool _allowToSave;
		private readonly SuperTooltipInfo _toolTipLoad = new SuperTooltipInfo("Needs Analysis Slides", "", "Open previously-saved CNA slide data files", null, null, eTooltipColor.Gray);
		private readonly SuperTooltipInfo _toolTipHelp = new SuperTooltipInfo("HELP", "", "Help me with the Client Needs Analysis Slide", null, null, eTooltipColor.Gray);

		public SlideClientGoalsControl()
			: base()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AppManager.Instance.SetClickEventHandler(this);
			if ((CreateGraphics()).DpiX > 96)
			{

				laGoal1.Font = new Font(laGoal1.Font.FontFamily, laGoal1.Font.Size - 3, laGoal1.Font.Style);
				laGoal2.Font = new Font(laGoal2.Font.FontFamily, laGoal2.Font.Size - 3, laGoal2.Font.Style);
				laGoal3.Font = new Font(laGoal3.Font.FontFamily, laGoal3.Font.Size - 3, laGoal3.Font.Style);
				laGoal4.Font = new Font(laGoal4.Font.FontFamily, laGoal4.Font.Size - 3, laGoal4.Font.Style);
				laGoal5.Font = new Font(laGoal5.Font.FontFamily, laGoal5.Font.Size - 3, laGoal5.Font.Style);
			}
			comboBoxEditGoal1.EnableSelectAll();
			comboBoxEditGoal2.EnableSelectAll();
			comboBoxEditGoal3.EnableSelectAll();
			comboBoxEditGoal4.EnableSelectAll();
			comboBoxEditGoal5.EnableSelectAll();

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(ListManager.Instance.ClientGoalsLists.Headers);

			comboBoxEditGoal1.Properties.Items.Clear();
			comboBoxEditGoal1.Properties.Items.AddRange(ListManager.Instance.ClientGoalsLists.Goals);

			comboBoxEditGoal2.Properties.Items.Clear();
			comboBoxEditGoal2.Properties.Items.AddRange(ListManager.Instance.ClientGoalsLists.Goals);

			comboBoxEditGoal3.Properties.Items.Clear();
			comboBoxEditGoal3.Properties.Items.AddRange(ListManager.Instance.ClientGoalsLists.Goals);

			comboBoxEditGoal4.Properties.Items.Clear();
			comboBoxEditGoal4.Properties.Items.AddRange(ListManager.Instance.ClientGoalsLists.Goals);

			comboBoxEditGoal5.Properties.Items.Clear();
			comboBoxEditGoal5.Properties.Items.AddRange(ListManager.Instance.ClientGoalsLists.Goals);

			checkEditSolutionNew.EditValueChanged += EditValueChanged;

			FormMain.Instance.FormClosed += (sender1, e1) =>
			{
				if (!SettingsNotSaved) return;
				SaveState();
				ViewSettingsManager.Instance.ClientGoalsState.Save();
			};

			LoadSavedState();
		}

		public override string SlideName => "Needs Analysis";

		public override SuperTooltipInfo TooltipLoad => _toolTipLoad;

		public override SuperTooltipInfo TooltipHelp => _toolTipHelp;

		public override ButtonItem ThemeButton => FormMain.Instance.buttonItemHomeThemeClientGoals;

		private void LoadSavedState()
		{
			_allowToSave = false;
			checkEditSolutionNew.Checked = ViewSettingsManager.Instance.ClientGoalsState.IsNewSolution;
			if (string.IsNullOrEmpty(ViewSettingsManager.Instance.ClientGoalsState.SlideHeader))
			{
				if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
					comboBoxEditSlideHeader.SelectedIndex = 0;
			}
			else
			{
				var index = comboBoxEditSlideHeader.Properties.Items.IndexOf(ViewSettingsManager.Instance.ClientGoalsState.SlideHeader);
				comboBoxEditSlideHeader.SelectedIndex = index >= 0 ? index : 0;
			}
			comboBoxEditGoal1.EditValue = !string.IsNullOrEmpty(ViewSettingsManager.Instance.ClientGoalsState.Goal1) ? ViewSettingsManager.Instance.ClientGoalsState.Goal1 : null;
			comboBoxEditGoal2.EditValue = !string.IsNullOrEmpty(ViewSettingsManager.Instance.ClientGoalsState.Goal2) ? ViewSettingsManager.Instance.ClientGoalsState.Goal2 : null;
			comboBoxEditGoal3.EditValue = !string.IsNullOrEmpty(ViewSettingsManager.Instance.ClientGoalsState.Goal3) ? ViewSettingsManager.Instance.ClientGoalsState.Goal3 : null;
			comboBoxEditGoal4.EditValue = !string.IsNullOrEmpty(ViewSettingsManager.Instance.ClientGoalsState.Goal4) ? ViewSettingsManager.Instance.ClientGoalsState.Goal4 : null;
			comboBoxEditGoal5.EditValue = !string.IsNullOrEmpty(ViewSettingsManager.Instance.ClientGoalsState.Goal5) ? ViewSettingsManager.Instance.ClientGoalsState.Goal5 : null;

			_allowToSave = true;
			SettingsNotSaved = false;

			UpdateSavedFilesState();
		}

		private void SaveState()
		{
			ViewSettingsManager.Instance.ClientGoalsState.IsNewSolution = checkEditSolutionNew.Checked;
			ViewSettingsManager.Instance.ClientGoalsState.SlideHeader = comboBoxEditSlideHeader.EditValue != null ? comboBoxEditSlideHeader.EditValue.ToString() : string.Empty;
			ViewSettingsManager.Instance.ClientGoalsState.Goal1 = comboBoxEditGoal1.EditValue != null ? comboBoxEditGoal1.EditValue.ToString() : string.Empty;
			ViewSettingsManager.Instance.ClientGoalsState.Goal2 = comboBoxEditGoal2.EditValue != null ? comboBoxEditGoal2.EditValue.ToString() : string.Empty;
			ViewSettingsManager.Instance.ClientGoalsState.Goal3 = comboBoxEditGoal3.EditValue != null ? comboBoxEditGoal3.EditValue.ToString() : string.Empty;
			ViewSettingsManager.Instance.ClientGoalsState.Goal4 = comboBoxEditGoal4.EditValue != null ? comboBoxEditGoal4.EditValue.ToString() : string.Empty;
			ViewSettingsManager.Instance.ClientGoalsState.Goal5 = comboBoxEditGoal5.EditValue != null ? comboBoxEditGoal5.EditValue.ToString() : string.Empty;
			SettingsNotSaved = false;
		}

		public override void LoadClick()
		{
			using (var form = new FormSavedClentGoals())
			{
				if (form.ShowDialog() == DialogResult.OK && !String.IsNullOrEmpty(form.SelectedFile))
				{
					ViewSettingsManager.Instance.ClientGoalsState.Load(form.SelectedFile);
					LoadSavedState();
				}
			}
			base.LoadClick();
		}

		private void EditValueChanged(object sender, EventArgs e)
		{
			UpdateOutputState();
			if (_allowToSave)
				SettingsNotSaved = true;
		}

		#region Output Staff
		public int GoalsCount
		{
			get
			{
				int result = 0;
				if (comboBoxEditGoal1.EditValue != null)
					if (!string.IsNullOrEmpty(comboBoxEditGoal1.EditValue.ToString().Trim()))
						result++;
				if (comboBoxEditGoal2.EditValue != null)
					if (!string.IsNullOrEmpty(comboBoxEditGoal2.EditValue.ToString().Trim()))
						result++;
				if (comboBoxEditGoal3.EditValue != null)
					if (!string.IsNullOrEmpty(comboBoxEditGoal3.EditValue.ToString().Trim()))
						result++;
				if (comboBoxEditGoal4.EditValue != null)
					if (!string.IsNullOrEmpty(comboBoxEditGoal4.EditValue.ToString().Trim()))
						result++;
				if (comboBoxEditGoal5.EditValue != null)
					if (!string.IsNullOrEmpty(comboBoxEditGoal5.EditValue.ToString().Trim()))
						result++;
				return result;
			}
		}

		public string Title => comboBoxEditSlideHeader.EditValue == null ? string.Empty : comboBoxEditSlideHeader.EditValue.ToString();

		public string[] SelectedGoals
		{
			get
			{
				var result = new List<string>();
				if (comboBoxEditGoal1.EditValue != null)
					if (!string.IsNullOrEmpty(comboBoxEditGoal1.EditValue.ToString().Trim()))
						result.Add(comboBoxEditGoal1.EditValue.ToString());
				if (comboBoxEditGoal2.EditValue != null)
					if (!string.IsNullOrEmpty(comboBoxEditGoal2.EditValue.ToString().Trim()))
						result.Add(comboBoxEditGoal2.EditValue.ToString());
				if (comboBoxEditGoal3.EditValue != null)
					if (!string.IsNullOrEmpty(comboBoxEditGoal3.EditValue.ToString().Trim()))
						result.Add(comboBoxEditGoal3.EditValue.ToString());
				if (comboBoxEditGoal4.EditValue != null)
					if (!string.IsNullOrEmpty(comboBoxEditGoal4.EditValue.ToString().Trim()))
						result.Add(comboBoxEditGoal4.EditValue.ToString());
				if (comboBoxEditGoal5.EditValue != null)
					if (!string.IsNullOrEmpty(comboBoxEditGoal5.EditValue.ToString().Trim()))
						result.Add(comboBoxEditGoal5.EditValue.ToString());
				return result.ToArray();
			}
		}

		protected override void SaveChanges(string fileName = "")
		{
			if (!SettingsNotSaved) return;
			SaveState();
			ViewSettingsManager.Instance.ClientGoalsState.Save(fileName);
			UpdateSavedFilesState();
		}

		public void Output()
		{
			SaveChanges();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			FormProgress.ShowProgress();
			AppManager.Instance.ShowFloater(() =>
			{
				DashboardPowerPointHelper.Instance.AppendClientGoals();
				FormProgress.CloseProgress();
			});
		}

		public void Preview()
		{
			SaveChanges();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			var tempFileName = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			DashboardPowerPointHelper.Instance.PrepareClientGoals(tempFileName);
			Utilities.ActivateForm(FormMain.Instance.Handle, false, false);
			FormProgress.CloseProgress();
			if (!File.Exists(tempFileName)) return;
			using (var formPreview = new FormPreview(FormMain.Instance, DashboardPowerPointHelper.Instance, AppManager.Instance.HelpManager, AppManager.Instance.ShowFloater))
			{
				formPreview.Text = "Preview Slides";
				formPreview.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = false;
				RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
				if (previewResult != DialogResult.OK)
					AppManager.Instance.ActivateMainForm();
			}
		}

		public void UpdateOutputState()
		{
			var result = false;
			if (comboBoxEditGoal1.EditValue != null)
			{
				if (!string.IsNullOrEmpty(comboBoxEditGoal1.EditValue.ToString().Trim()))
					result = true;
			}
			if (comboBoxEditGoal2.EditValue != null)
			{
				if (!string.IsNullOrEmpty(comboBoxEditGoal2.EditValue.ToString().Trim()))
					result = true;
			}
			if (comboBoxEditGoal3.EditValue != null)
			{
				if (!string.IsNullOrEmpty(comboBoxEditGoal3.EditValue.ToString().Trim()))
					result = true;
			}
			if (comboBoxEditGoal4.EditValue != null)
			{
				if (!string.IsNullOrEmpty(comboBoxEditGoal4.EditValue.ToString().Trim()))
					result = true;
			}
			if (comboBoxEditGoal5.EditValue != null)
			{
				if (!string.IsNullOrEmpty(comboBoxEditGoal5.EditValue.ToString().Trim()))
					result = true;
			}
			SetOutputState(result);
		}

		protected override void UpdateSavedFilesState()
		{
			SetLoadState(ViewSettingsManager.Instance.ClientGoalsState.AllowToLoad());
		}
		#endregion
	}
}