using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Dashboard.InteropClasses;
using Asa.Solutions.Dashboard.PresentationClasses.Output;

namespace Asa.Solutions.Dashboard.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class LeadoffStatementControl : DashboardSlideControl, ILeadoffStatementOutputData, IDashboardSlide
	{
		private bool _allowToSave;
		public override SlideType SlideType => SlideType.LeadoffStatement;
		public string SlideName => "B. Intro Slide";

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
				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.LeadoffStatementState.SlideHeader;
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

		public Theme SelectedTheme => SlideContainer.GetSelectedTheme(SlideType.LeadoffStatement);

		public int StatementsCount => SelectedStatements.Length;

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

		public void GenerateOutput()
		{
			SolutionDashboardPowerPointHelper.Instance.AppendLeadoffStatements(this);
		}

		public PreviewGroup GeneratePreview()
		{
			var tempFileName = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			SolutionDashboardPowerPointHelper.Instance.PrepareLeadoffStatements(this, tempFileName);
			return new PreviewGroup { Name = SlideName, PresentationSourcePath = tempFileName };
		}
		#endregion
	}
}