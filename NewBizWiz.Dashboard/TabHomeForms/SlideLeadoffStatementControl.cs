using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Dashboard;
using NewBizWiz.Dashboard.InteropClasses;
using ListManager = NewBizWiz.Core.Dashboard.ListManager;

namespace NewBizWiz.Dashboard.TabHomeForms
{
	[ToolboxItem(false)]
	public sealed partial class SlideLeadoffStatementControl : SlideBaseControl
	{
		private bool _allowToSave;
		private readonly SuperTooltipInfo _toolTipLoad = new SuperTooltipInfo("Intro Slides", "", "Open previously-saved Intro slide data files", null, null, eTooltipColor.Gray);
		private readonly SuperTooltipInfo _toolTipHelp = new SuperTooltipInfo("HELP", "", "Help me with the Introduction Slide", null, null, eTooltipColor.Gray);

		public SlideLeadoffStatementControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AppManager.Instance.SetClickEventHandler(this);
			if ((CreateGraphics()).DpiX > 96)
			{
				ckA.Font = new Font(ckA.Font.FontFamily, ckA.Font.Size - 3, ckA.Font.Style);
				ckB.Font = new Font(ckB.Font.FontFamily, ckB.Font.Size - 3, ckB.Font.Style);
				ckC.Font = new Font(ckC.Font.FontFamily, ckC.Font.Size - 3, ckC.Font.Style);
			}
			UpdateEditState();
			comboBoxEditSlideHeader.MouseUp += Utilities.Instance.Editor_MouseUp;
			comboBoxEditSlideHeader.MouseDown += Utilities.Instance.Editor_MouseDown;
			comboBoxEditSlideHeader.Enter += Utilities.Instance.Editor_Enter;
			memoEditA.MouseUp += Utilities.Instance.Editor_MouseUp;
			memoEditA.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEditA.Enter += Utilities.Instance.Editor_Enter;
			memoEditB.MouseUp += Utilities.Instance.Editor_MouseUp;
			memoEditB.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEditB.Enter += Utilities.Instance.Editor_Enter;
			memoEditC.MouseUp += Utilities.Instance.Editor_MouseUp;
			memoEditC.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEditC.Enter += Utilities.Instance.Editor_Enter;

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(ListManager.Instance.LeadoffStatementLists.Headers);

			checkEditSolutionNew.EditValueChanged += EditValueChanged;

			FormMain.Instance.FormClosed += (sender1, e1) =>
			{
				if (SettingsNotSaved)
				{
					SaveState();
					ViewSettingsManager.Instance.LeadoffStatementState.Save();
				}
			};

			LoadSavedState();
		}

		public override string SlideName
		{
			get { return "Introduction"; }
		}

		public override SuperTooltipInfo TooltipLoad
		{
			get { return _toolTipLoad; }
		}

		public override SuperTooltipInfo TooltipHelp
		{
			get { return _toolTipHelp; }
		}

		public override ButtonItem ThemeButton
		{
			get { return FormMain.Instance.buttonItemHomeThemeLeadoff; }
		}

		private void UpdateEditState()
		{
			memoEditA.Enabled = ckA.Checked;
			memoEditB.Enabled = ckB.Checked;
			memoEditC.Enabled = ckC.Checked;
		}

		private void LoadSavedState()
		{
			_allowToSave = false;
			checkEditSolutionNew.Checked = ViewSettingsManager.Instance.LeadoffStatementState.IsNewSolution;
			if (string.IsNullOrEmpty(ViewSettingsManager.Instance.LeadoffStatementState.SlideHeader))
			{
				if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
					comboBoxEditSlideHeader.SelectedIndex = 0;
			}
			else
			{
				var index = comboBoxEditSlideHeader.Properties.Items.IndexOf(ViewSettingsManager.Instance.LeadoffStatementState.SlideHeader);
				comboBoxEditSlideHeader.SelectedIndex = index >= 0 ? index : 0;
			}
			ckA.Checked = ViewSettingsManager.Instance.LeadoffStatementState.ShowStatement1;
			ckB.Checked = ViewSettingsManager.Instance.LeadoffStatementState.ShowStatement2;
			ckC.Checked = ViewSettingsManager.Instance.LeadoffStatementState.ShowStatement3;
			memoEditA.EditValue = !String.IsNullOrEmpty(ViewSettingsManager.Instance.LeadoffStatementState.Statement1) ? ViewSettingsManager.Instance.LeadoffStatementState.Statement1 : (ListManager.Instance.LeadoffStatementLists.Statements.Count > 0 ? ListManager.Instance.LeadoffStatementLists.Statements[0] : string.Empty);
			memoEditB.EditValue = !String.IsNullOrEmpty(ViewSettingsManager.Instance.LeadoffStatementState.Statement2) ? ViewSettingsManager.Instance.LeadoffStatementState.Statement2 : (ListManager.Instance.LeadoffStatementLists.Statements.Count > 1 ? ListManager.Instance.LeadoffStatementLists.Statements[1] : string.Empty);
			memoEditC.EditValue = !String.IsNullOrEmpty(ViewSettingsManager.Instance.LeadoffStatementState.Statement3) ? ViewSettingsManager.Instance.LeadoffStatementState.Statement3 : (ListManager.Instance.LeadoffStatementLists.Statements.Count > 2 ? ListManager.Instance.LeadoffStatementLists.Statements[2] : string.Empty);

			_allowToSave = true;
			SettingsNotSaved = false;

			UpdateOutputState();
			UpdateSavedFilesState();
		}

		private void SaveState()
		{
			ViewSettingsManager.Instance.LeadoffStatementState.IsNewSolution = checkEditSolutionNew.Checked;
			ViewSettingsManager.Instance.LeadoffStatementState.SlideHeader = comboBoxEditSlideHeader.EditValue != null ? comboBoxEditSlideHeader.EditValue.ToString() : string.Empty;
			ViewSettingsManager.Instance.LeadoffStatementState.ShowStatement1 = ckA.Checked;
			ViewSettingsManager.Instance.LeadoffStatementState.ShowStatement2 = ckB.Checked;
			ViewSettingsManager.Instance.LeadoffStatementState.ShowStatement3 = ckC.Checked;
			ViewSettingsManager.Instance.LeadoffStatementState.Statement1 = memoEditA.EditValue != null ? memoEditA.EditValue.ToString() : string.Empty;
			ViewSettingsManager.Instance.LeadoffStatementState.Statement2 = memoEditB.EditValue != null ? memoEditB.EditValue.ToString() : string.Empty;
			ViewSettingsManager.Instance.LeadoffStatementState.Statement3 = memoEditC.EditValue != null ? memoEditC.EditValue.ToString() : string.Empty;
			SettingsNotSaved = false;
		}

		public override void LoadClick()
		{
			using (var form = new FormSavedLeadoffStatement())
			{
				if (form.ShowDialog() == DialogResult.OK && !String.IsNullOrEmpty(form.SelectedFile))
				{
					ViewSettingsManager.Instance.LeadoffStatementState.Load(form.SelectedFile);
					LoadSavedState();
				}
			}
			base.LoadClick();
		}

		private void checkBoxes_CheckedChanged(object sender, EventArgs e)
		{
			UpdateEditState();
			UpdateOutputState();
			if (_allowToSave)
				SettingsNotSaved = true;
		}

		private void EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
				SettingsNotSaved = true;
		}

		#region Output Staff
		public int StatementsCount
		{
			get
			{
				var result = 0;
				if (ckA.Checked && memoEditA.EditValue != null)
					if (!string.IsNullOrEmpty(memoEditA.EditValue.ToString().Trim()))
						result++;
				if (ckB.Checked && memoEditB.EditValue != null)
					if (!string.IsNullOrEmpty(memoEditB.EditValue.ToString().Trim()))
						result++;
				if (ckC.Checked && memoEditC.EditValue != null)
					if (!string.IsNullOrEmpty(memoEditC.EditValue.ToString().Trim()))
						result++;
				return result;
			}
		}

		public string Title
		{
			get { return comboBoxEditSlideHeader.EditValue == null ? string.Empty : comboBoxEditSlideHeader.EditValue.ToString(); }
		}

		public string[] SelectedStatements
		{
			get
			{
				var result = new List<string>();
				if (ckA.Checked && memoEditA.EditValue != null)
					if (!string.IsNullOrEmpty(memoEditA.EditValue.ToString().Trim()))
						result.Add(memoEditA.EditValue.ToString());
				if (ckB.Checked && memoEditB.EditValue != null)
					if (!string.IsNullOrEmpty(memoEditB.EditValue.ToString().Trim()))
						result.Add(memoEditB.EditValue.ToString());
				if (ckC.Checked && memoEditC.EditValue != null)
					if (!string.IsNullOrEmpty(memoEditC.EditValue.ToString().Trim()))
						result.Add(memoEditC.EditValue.ToString());
				return result.ToArray();
			}
		}

		public void UpdateOutputState()
		{
			SetOutputState(ckA.Checked || ckB.Checked || ckC.Checked);
		}

		protected override void UpdateSavedFilesState()
		{
			SetLoadState(ViewSettingsManager.Instance.LeadoffStatementState.AllowToLoad());
		}

		protected override void SaveChanges(string fileName = "")
		{
			if (!SettingsNotSaved) return;
			SaveState();
			ViewSettingsManager.Instance.LeadoffStatementState.Save(fileName);
			UpdateSavedFilesState();
		}

		public void TrackOutput()
		{
			var otherOptions = new Dictionary<string, object>();
			otherOptions.Add("IsNewSolution", ViewSettingsManager.Instance.LeadoffStatementState.IsNewSolution);
			AppManager.Instance.ActivityManager.AddActivity(new OutputActivity(SlideName, null, null, otherOptions));
		}

		public void Output()
		{
			SaveChanges();
			TrackOutput();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			FormProgress.ShowProgress();
			AppManager.Instance.ShowFloater(() =>
			{
				DashboardPowerPointHelper.Instance.AppendLeadoffStatements();
				FormProgress.CloseProgress();
			});
		}

		public void Preview()
		{
			SaveChanges();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			var tempFileName = Path.Combine(Core.Common.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			DashboardPowerPointHelper.Instance.PrepareLeadoffStatements(tempFileName);
			Utilities.Instance.ActivateForm(FormMain.Instance.Handle, false, false);
			FormProgress.CloseProgress();
			if (!File.Exists(tempFileName)) return;
			using (var formPreview = new FormPreview(FormMain.Instance, DashboardPowerPointHelper.Instance, AppManager.Instance.HelpManager, AppManager.Instance.ShowFloater, TrackOutput))
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
		#endregion
	}
}