using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Dashboard.InteropClasses;
using Asa.Solutions.Dashboard.PresentationClasses.Output;
using DevExpress.Skins;

namespace Asa.Solutions.Dashboard.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class LeadoffStatementControl : DashboardSlideControl, ILeadoffStatementOutputData, IDashboardSlide
	{
		private bool _allowToSave;
		public override SlideType SlideType => SlideType.LeadoffStatement;
		public string SlideName => SlideContainer.DashboardInfo.LeadoffStatementTitle;

		public LeadoffStatementControl(BaseDashboardContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();
			Text = SlideName;
			
			UpdateEditState();

			comboBoxEditSlideHeader.EnableSelectAll();
			memoEditA.EnableSelectAll();
			memoEditB.EnableSelectAll();
			memoEditC.EnableSelectAll();

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.DashboardInfo.LeadoffStatementLists.Headers);

			pictureEditSplash.Image = SlideContainer.DashboardInfo.LeadoffStatementSplashLogo;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);
			layoutControlItemAToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAToggle.MaxSize, scaleFactor);
			layoutControlItemAToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemAToggle.MinSize, scaleFactor);
			layoutControlItemAValue.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAValue.MaxSize, scaleFactor);
			layoutControlItemAValue.MinSize = RectangleHelper.ScaleSize(layoutControlItemAValue.MinSize, scaleFactor);
			layoutControlItemBToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemBToggle.MaxSize, scaleFactor);
			layoutControlItemBToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemBToggle.MinSize, scaleFactor);
			layoutControlItemBValue.MaxSize = RectangleHelper.ScaleSize(layoutControlItemBValue.MaxSize, scaleFactor);
			layoutControlItemBValue.MinSize = RectangleHelper.ScaleSize(layoutControlItemBValue.MinSize, scaleFactor);
			layoutControlItemCToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCToggle.MaxSize, scaleFactor);
			layoutControlItemCToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemCToggle.MinSize, scaleFactor);
			layoutControlItemCValue.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCValue.MaxSize, scaleFactor);
			layoutControlItemCValue.MinSize = RectangleHelper.ScaleSize(layoutControlItemCValue.MinSize, scaleFactor);
		}

		private void UpdateEditState()
		{
			layoutControlItemAValue.Enabled = checkEditA.Checked;
			layoutControlItemBValue.Enabled = checkEditB.Checked;
			layoutControlItemCValue.Enabled = checkEditC.Checked;
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
			checkEditA.Checked = SlideContainer.EditedContent.LeadoffStatementState.ShowStatement1;
			checkEditB.Checked = SlideContainer.EditedContent.LeadoffStatementState.ShowStatement2;
			checkEditC.Checked = SlideContainer.EditedContent.LeadoffStatementState.ShowStatement3;
			memoEditA.EditValue = !String.IsNullOrEmpty(SlideContainer.EditedContent.LeadoffStatementState.Statement1) ? SlideContainer.EditedContent.LeadoffStatementState.Statement1 : (SlideContainer.DashboardInfo.LeadoffStatementLists.Statements.Count > 0 ? SlideContainer.DashboardInfo.LeadoffStatementLists.Statements[0] : string.Empty);
			memoEditB.EditValue = !String.IsNullOrEmpty(SlideContainer.EditedContent.LeadoffStatementState.Statement2) ? SlideContainer.EditedContent.LeadoffStatementState.Statement2 : (SlideContainer.DashboardInfo.LeadoffStatementLists.Statements.Count > 1 ? SlideContainer.DashboardInfo.LeadoffStatementLists.Statements[1] : string.Empty);
			memoEditC.EditValue = !String.IsNullOrEmpty(SlideContainer.EditedContent.LeadoffStatementState.Statement3) ? SlideContainer.EditedContent.LeadoffStatementState.Statement3 : (SlideContainer.DashboardInfo.LeadoffStatementLists.Statements.Count > 2 ? SlideContainer.DashboardInfo.LeadoffStatementLists.Statements[2] : string.Empty);

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			SlideContainer.EditedContent.LeadoffStatementState.SlideHeader = comboBoxEditSlideHeader.EditValue?.ToString() ?? string.Empty;
			SlideContainer.EditedContent.LeadoffStatementState.ShowStatement1 = checkEditA.Checked;
			SlideContainer.EditedContent.LeadoffStatementState.ShowStatement2 = checkEditB.Checked;
			SlideContainer.EditedContent.LeadoffStatementState.ShowStatement3 = checkEditC.Checked;
			SlideContainer.EditedContent.LeadoffStatementState.Statement1 = memoEditA.EditValue?.ToString() ?? string.Empty;
			SlideContainer.EditedContent.LeadoffStatementState.Statement2 = memoEditB.EditValue?.ToString() ?? string.Empty;
			SlideContainer.EditedContent.LeadoffStatementState.Statement3 = memoEditC.EditValue?.ToString() ?? string.Empty;
		}

		private void OnCheckedChanged(object sender, EventArgs e)
		{
			UpdateEditState();
			OnEditValueChanged(sender, e);
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
				SlideContainer.RaiseDataChanged();
		}

		#region Output Staff
		public override bool ReadyForOutput => checkEditA.Checked || checkEditB.Checked || checkEditC.Checked;

		public int StatementsCount => SelectedStatements.Length;

		public string Title => comboBoxEditSlideHeader.EditValue?.ToString() ?? string.Empty;

		public string[] SelectedStatements
		{
			get
			{
				var result = new List<string>();
				if (checkEditA.Checked && memoEditA.EditValue != null)
					if (!string.IsNullOrEmpty(memoEditA.EditValue.ToString().Trim()))
						result.Add(memoEditA.EditValue.ToString());
				if (checkEditB.Checked && memoEditB.EditValue != null)
					if (!string.IsNullOrEmpty(memoEditB.EditValue.ToString().Trim()))
						result.Add(memoEditB.EditValue.ToString());
				if (checkEditC.Checked && memoEditC.EditValue != null)
					if (!string.IsNullOrEmpty(memoEditC.EditValue.ToString().Trim()))
						result.Add(memoEditC.EditValue.ToString());
				return result.ToArray();
			}
		}

		public void GenerateOutput()
		{
			SlideContainer.PowerPointProcessor.AppendLeadoffStatements(this);
		}

		public PreviewGroup GeneratePreview()
		{
			var tempFileName = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			SlideContainer.PowerPointProcessor.PrepareLeadoffStatements(this, tempFileName);
			return new PreviewGroup { Name = SlideName, PresentationSourcePath = tempFileName };
		}
		#endregion
	}
}