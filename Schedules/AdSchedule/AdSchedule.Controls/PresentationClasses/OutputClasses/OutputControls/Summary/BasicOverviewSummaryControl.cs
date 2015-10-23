using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using Asa.AdSchedule.Controls.BusinessClasses;
using Asa.AdSchedule.Controls.InteropClasses;
using Asa.CommonGUI.Preview;
using Asa.Core.Common;

namespace Asa.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	[ToolboxItem(false)]
	//public partial class BasicOverviewSummaryControl : UserControl
	public partial class BasicOverviewSummaryControl : XtraTabPage, IBasicOverviewOutputControl
	{
		private readonly List<PublicationSummaryControl> _summaryControls = new List<PublicationSummaryControl>();
		private bool _allowToSave;

		public OutputBasicOverviewControl Parent { get; private set; }
		public List<Dictionary<string, string>> OutputReplacementsLists { get; set; }

		public BasicOverviewSummaryControl(OutputBasicOverviewControl parent)
		{
			InitializeComponent();
			Parent = parent;
			Text = "Product Summary";
		}

		public void UpdateControls(IEnumerable<PublicationSummaryControl> summaryControls)
		{
			_allowToSave = false;

			xtraScrollableControl.Controls.Clear();
			_summaryControls.Clear();
			_summaryControls.AddRange(summaryControls);
			if (_summaryControls.Any())
			{
				xtraScrollableControl.Controls.AddRange(summaryControls.Reverse().ToArray());
				xtraScrollableControl.ScrollControlIntoView(_summaryControls[0]);
				SetFocus();
			}

			checkEditStatement.Checked = !String.IsNullOrEmpty(Parent.LocalSchedule.ViewSettings.BasicOverviewSummaryViewSettings.Statement);
			memoEditStatement.EditValue = Parent.LocalSchedule.ViewSettings.BasicOverviewSummaryViewSettings.Statement;
			checkEditMonthlyInvestment.Checked = Parent.LocalSchedule.ViewSettings.BasicOverviewSummaryViewSettings.MonthlyInvestment.HasValue;
			spinEditMonthlyInvestment.EditValue = Parent.LocalSchedule.ViewSettings.BasicOverviewSummaryViewSettings.MonthlyInvestment;
			checkEditTotalInvestment.Checked = Parent.LocalSchedule.ViewSettings.BasicOverviewSummaryViewSettings.TotalInvestment.HasValue;
			spinEditTotalInvestment.EditValue = Parent.LocalSchedule.ViewSettings.BasicOverviewSummaryViewSettings.TotalInvestment;

			_allowToSave = true;
		}

		public void Save()
		{
			Parent.LocalSchedule.ViewSettings.BasicOverviewSummaryViewSettings.Statement = checkEditStatement.Checked ? memoEditStatement.EditValue as String : null;
			Parent.LocalSchedule.ViewSettings.BasicOverviewSummaryViewSettings.MonthlyInvestment = checkEditMonthlyInvestment.Checked ? spinEditMonthlyInvestment.EditValue as decimal? : null;
			Parent.LocalSchedule.ViewSettings.BasicOverviewSummaryViewSettings.TotalInvestment = checkEditTotalInvestment.Checked ? spinEditTotalInvestment.EditValue as decimal? : null;
		}

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
				Parent.SettingsNotSaved = true;
		}

		#region Output
		public int RowsPerSlide
		{
			get { return 7; }
		}

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

		public PreviewGroup GetPreviewGroup()
		{
			return new PreviewGroup
			{
				Name = Text,
				PresentationSourcePath = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
			};
		}

		public string SlideName
		{
			get { return Text; }
		}

		public Theme SelectedTheme
		{
			get { return BusinessObjects.Instance.ThemeManager.GetThemes(SlideType.PrintBasicOverview).FirstOrDefault(t => t.Name.Equals(Core.AdSchedule.SettingsManager.Instance.GetSelectedTheme(SlideType.PrintBasicOverview)) || String.IsNullOrEmpty(Core.AdSchedule.SettingsManager.Instance.GetSelectedTheme(SlideType.PrintBasicOverview))); }
		}

		public void Output()
		{
			PopulateReplacementsList();
			AdSchedulePowerPointHelper.Instance.AppendBasicOverviewSummary(this);
		}
		#endregion
	}
}
