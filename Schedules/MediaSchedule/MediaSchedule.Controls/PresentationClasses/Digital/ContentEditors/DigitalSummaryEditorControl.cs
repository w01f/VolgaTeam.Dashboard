using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Preview;
using Asa.Media.Controls.PresentationClasses.Digital.Output;
using Asa.Media.Controls.PresentationClasses.Digital.Settings;
using Asa.Online.Controls.InteropClasses;
using Asa.Online.Controls.PresentationClasses.Products;
using Asa.Online.Controls.PresentationClasses.Summary;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.Digital.ContentEditors
{
	[ToolboxItem(false)]
	//public partial class DigitalSummaryControl : UserControl, IDigitalSlideControl
	public partial class DigitalSummaryEditorControl : XtraTabPage, IDigitalEditor, IDigitalSummaryContainerControl, IDigitalOutputContainer, IDigitalOutputItem
	{
		private bool _allowToSave;
		private bool _needToReload;
		private readonly DigitalEditorsContainer _container;
		private readonly List<DigitalProductSummaryControl> _summaryControls = new List<DigitalProductSummaryControl>();
		public DigitalEditorType EditorType => DigitalEditorType.Summary;
		public string HelpTag => "digitalsl";
		public event EventHandler<DataChangedEventArgs> DataChanged;

		public DigitalSummaryEditorControl(DigitalEditorsContainer container)
		{
			InitializeComponent();
			Text = "Digital Summary";
			_container = container;
			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;
			}
		}

		public void LoadData()
		{
			if (!_needToReload) return;

			_allowToSave = false;

			_summaryControls.ForEach(s => s.Release());
			_summaryControls.Clear();
			xtraScrollableControl.Controls.Clear();
			_summaryControls.AddRange(_container.EditedContent.DigitalProducts.Select(product =>
			{
				var summaryControl = new DigitalProductSummaryControl();
				summaryControl.LoadData(product);
				return summaryControl;
			}));
			if (_summaryControls.Any())
			{
				xtraScrollableControl.Controls.AddRange(_summaryControls.OfType<Control>().Reverse().ToArray());
				xtraScrollableControl.ScrollControlIntoView(_summaryControls[0]);
				SetFocus();
			}

			checkEditStatement.Checked = !String.IsNullOrEmpty(_container.EditedContent.DigitalProductSummary.Statement);
			memoEditStatement.EditValue = _container.EditedContent.DigitalProductSummary.Statement;
			checkEditMonthlyInvestment.Checked = _container.EditedContent.DigitalProductSummary.MonthlyInvestment.HasValue;
			spinEditMonthlyInvestment.EditValue = _container.EditedContent.DigitalProductSummary.MonthlyInvestment;
			checkEditTotalInvestment.Checked = _container.EditedContent.DigitalProductSummary.TotalInvestment.HasValue;
			spinEditTotalInvestment.EditValue = _container.EditedContent.DigitalProductSummary.TotalInvestment;

			_allowToSave = true;

			_needToReload = false;
		}

		public void RequestReload()
		{
			_needToReload = true;
		}

		public void SaveData()
		{
			_summaryControls.ForEach(sc => sc.SaveData());
			_container.EditedContent.DigitalProductSummary.Statement = checkEditStatement.Checked ? memoEditStatement.EditValue as String : null;
			_container.EditedContent.DigitalProductSummary.MonthlyInvestment = checkEditMonthlyInvestment.Checked ? spinEditMonthlyInvestment.EditValue as decimal? : null;
			_container.EditedContent.DigitalProductSummary.TotalInvestment = checkEditTotalInvestment.Checked ? spinEditTotalInvestment.EditValue as decimal? : null;
		}

		public void UpdateAccordingSettings(SettingsChangedEventArgs e) { }

		public void SetFocus()
		{
			if (_summaryControls.Any())
				_summaryControls[0].FocusControl();
		}

		private void checkEditStatement_CheckedChanged(object sender, EventArgs e)
		{
			memoEditStatement.Enabled = checkEditStatement.Checked;
			if (!checkEditStatement.Checked)
				memoEditStatement.EditValue = null;
			EditValueChanged(sender, e);
		}

		private void checkEditMonthlyInvestment_CheckedChanged(object sender, EventArgs e)
		{
			spinEditMonthlyInvestment.Enabled = checkEditMonthlyInvestment.Checked;
			if (!checkEditMonthlyInvestment.Checked)
				spinEditMonthlyInvestment.EditValue = null;
			EditValueChanged(sender, e);
		}

		private void checkEditTotalInvestment_CheckedChanged(object sender, EventArgs e)
		{
			spinEditTotalInvestment.Enabled = checkEditTotalInvestment.Checked;
			if (!checkEditTotalInvestment.Checked)
				spinEditTotalInvestment.EditValue = null;
			EditValueChanged(sender, e);
		}

		private void EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
				DataChanged?.Invoke(this, new DataChangedEventArgs { ChangedEditorType = EditorType });
		}

		#region Output
		public List<Dictionary<string, string>> OutputReplacementsLists { get; set; }
		public Theme SelectedTheme => _container.SelectedTheme;
		private static int RowsPerSlide => 7;

		public void PopulateReplacementsList()
		{
			var recordsCount = _summaryControls.Count;
			OutputReplacementsLists = new List<Dictionary<string, string>>();
			for (var i = 0; i < recordsCount; i += RowsPerSlide)
			{
				var slideRows = new Dictionary<string, string>();
				for (var j = 0; j < RowsPerSlide; j++)
				{
					if ((i + j) < recordsCount)
					{
						var summaryRecord = _summaryControls[i + j];
						slideRows.Add(String.Format("product{0}", j + 1), summaryRecord.Title);
						slideRows.Add(String.Format("details{0}", j + 1), summaryRecord.Details);
					}
					else
						slideRows.Add(String.Format("product{0}", j + 1), "DeleteRow");
				}

				if (checkEditStatement.Checked || checkEditMonthlyInvestment.Checked || checkEditTotalInvestment.Checked)
				{
					var statements = new List<string>();
					if (checkEditStatement.Checked && memoEditStatement.EditValue != null)
						statements.Add(memoEditStatement.EditValue as String);
					if (checkEditMonthlyInvestment.Checked || checkEditTotalInvestment.Checked)
					{
						var investments = new List<string>();
						if (checkEditMonthlyInvestment.Checked && spinEditMonthlyInvestment.EditValue != null && spinEditMonthlyInvestment.Value > 0)
							investments.Add(String.Format("Monthly Investment: {0}", spinEditMonthlyInvestment.Value.ToString("$#,##0")));
						if (checkEditTotalInvestment.Checked && spinEditTotalInvestment.EditValue != null && spinEditTotalInvestment.Value > 0)
							investments.Add(String.Format("Total Investment: {0}", spinEditTotalInvestment.Value.ToString("$#,##0")));
						if (investments.Any())
							statements.Add(String.Join(", ", investments));
					}
					slideRows.Add("Investment8", String.Join(Environment.NewLine, statements));
				}
				else
					slideRows.Add("Investment8", "DeleteRow");

				OutputReplacementsLists.Add(slideRows);
			}
		}

		public string SlideName => Text;

		public IList<IDigitalOutputItem> GetOutputItems()
		{
			return new List<IDigitalOutputItem> { this };
		}

		public void GenerateOutput()
		{
			PopulateReplacementsList();
			OnlineSchedulePowerPointHelper.Instance.AppendDigitalSummary(this);
		}

		public PreviewGroup GetPreviewGroup()
		{
			return new PreviewGroup
			{
				Name = Text,
				PresentationSourcePath = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
			};
		}

		public PreviewGroup GeneratePreview()
		{
			var previewGroup = GetPreviewGroup();
			PopulateReplacementsList();
			OnlineSchedulePowerPointHelper.Instance.PrepareDigitalSummaryEmail(previewGroup.PresentationSourcePath, this);
			return previewGroup;
		}
		#endregion

	}
}
