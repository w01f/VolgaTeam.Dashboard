using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Asa.Common.Core.Enums;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;

namespace Asa.Solutions.Dashboard.PresentationClasses
{
	[ToolboxItem(false)]
	public sealed partial class LeadoffStatementControl : DashboardSlideControl
	{
		private bool _allowToSave;
		public override SlideType SlideType => SlideType.LeadoffStatement;
		public override string SlideName => "B. Intro Slide";

		public LeadoffStatementControl(BaseDashboardContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();
			Text = SlideName;
			if ((CreateGraphics()).DpiX > 96)
			{
				ckA.Font = new Font(ckA.Font.FontFamily, ckA.Font.Size - 3, ckA.Font.Style);
				ckB.Font = new Font(ckB.Font.FontFamily, ckB.Font.Size - 3, ckB.Font.Style);
				ckC.Font = new Font(ckC.Font.FontFamily, ckC.Font.Size - 3, ckC.Font.Style);
			}
			UpdateEditState();
			comboBoxEditSlideHeader.EnableSelectAll();
			memoEditA.EnableSelectAll();
			memoEditB.EnableSelectAll();
			memoEditC.EnableSelectAll();

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.DashboardInfo.LeadoffStatementLists.Headers);

			pbSplash.Image = SlideContainer.DashboardInfo.LeadoffStatementSplashLogo;
		}

		private void UpdateEditState()
		{
			memoEditA.Enabled = ckA.Checked;
			memoEditB.Enabled = ckB.Checked;
			memoEditC.Enabled = ckC.Checked;
		}

		public override void LoadData()
		{
			_allowToSave = false;
			if (string.IsNullOrEmpty(SlideContainer.EditedContent.LeadoffStatementState.SlideHeader))
			{
				if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
					comboBoxEditSlideHeader.SelectedIndex = 0;
			}
			else
			{
				var index = comboBoxEditSlideHeader.Properties.Items.IndexOf(SlideContainer.EditedContent.LeadoffStatementState.SlideHeader);
				comboBoxEditSlideHeader.SelectedIndex = index >= 0 ? index : 0;
			}
			ckA.Checked = SlideContainer.EditedContent.LeadoffStatementState.ShowStatement1;
			ckB.Checked = SlideContainer.EditedContent.LeadoffStatementState.ShowStatement2;
			ckC.Checked = SlideContainer.EditedContent.LeadoffStatementState.ShowStatement3;
			memoEditA.EditValue = !String.IsNullOrEmpty(SlideContainer.EditedContent.LeadoffStatementState.Statement1) ? SlideContainer.EditedContent.LeadoffStatementState.Statement1 : (SlideContainer.DashboardInfo.LeadoffStatementLists.Statements.Count > 0 ? SlideContainer.DashboardInfo.LeadoffStatementLists.Statements[0] : string.Empty);
			memoEditB.EditValue = !String.IsNullOrEmpty(SlideContainer.EditedContent.LeadoffStatementState.Statement2) ? SlideContainer.EditedContent.LeadoffStatementState.Statement2 : (SlideContainer.DashboardInfo.LeadoffStatementLists.Statements.Count > 1 ? SlideContainer.DashboardInfo.LeadoffStatementLists.Statements[1] : string.Empty);
			memoEditC.EditValue = !String.IsNullOrEmpty(SlideContainer.EditedContent.LeadoffStatementState.Statement3) ? SlideContainer.EditedContent.LeadoffStatementState.Statement3 : (SlideContainer.DashboardInfo.LeadoffStatementLists.Statements.Count > 2 ? SlideContainer.DashboardInfo.LeadoffStatementLists.Statements[2] : string.Empty);

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			SlideContainer.EditedContent.LeadoffStatementState.SlideHeader = comboBoxEditSlideHeader.EditValue?.ToString() ?? string.Empty;
			SlideContainer.EditedContent.LeadoffStatementState.ShowStatement1 = ckA.Checked;
			SlideContainer.EditedContent.LeadoffStatementState.ShowStatement2 = ckB.Checked;
			SlideContainer.EditedContent.LeadoffStatementState.ShowStatement3 = ckC.Checked;
			SlideContainer.EditedContent.LeadoffStatementState.Statement1 = memoEditA.EditValue?.ToString() ?? string.Empty;
			SlideContainer.EditedContent.LeadoffStatementState.Statement2 = memoEditB.EditValue?.ToString() ?? string.Empty;
			SlideContainer.EditedContent.LeadoffStatementState.Statement3 = memoEditC.EditValue?.ToString() ?? string.Empty;
		}

		private void checkBoxes_CheckedChanged(object sender, EventArgs e)
		{
			UpdateEditState();
			if (_allowToSave)
				SlideContainer.RaiseDataChanged();
		}

		private void EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
				SlideContainer.RaiseDataChanged();
		}

		#region Output Staff
		public override bool ReadyForOutput => ckA.Checked || ckB.Checked || ckC.Checked;

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

		public string Title => comboBoxEditSlideHeader.EditValue?.ToString() ?? string.Empty;

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

		public override void GenerateOutput()
		{
			//SaveChanges();
			//FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			//FormProgress.ShowProgress();
			//AppManager.Instance.ShowFloater(() =>
			//{
			//	DashboardPowerPointHelper.Instance.AppendLeadoffStatements();
			//	FormProgress.CloseProgress();
			//});
		}

		public override PreviewGroup GeneratePreview()
		{
			throw new NotImplementedException();
			//SaveChanges();
			//FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			//FormProgress.ShowProgress();
			//var tempFileName = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			//DashboardPowerPointHelper.Instance.PrepareLeadoffStatements(tempFileName);
			//Utilities.ActivateForm(FormMain.Instance.Handle, false, false);
			//FormProgress.CloseProgress();
			//if (!File.Exists(tempFileName)) return;
			//using (var formPreview = new FormPreview(FormMain.Instance, DashboardPowerPointHelper.Instance, AppManager.Instance.HelpManager, AppManager.Instance.ShowFloater))
			//{
			//	formPreview.Text = "Preview Slides";
			//	formPreview.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
			//	RegistryHelper.MainFormHandle = formPreview.Handle;
			//	RegistryHelper.MaximizeMainForm = false;
			//	var previewResult = formPreview.ShowDialog();
			//	RegistryHelper.MaximizeMainForm = false;
			//	RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
			//	if (previewResult != DialogResult.OK)
			//		AppManager.Instance.ActivateMainForm();
			//}
		}
		#endregion
	}
}