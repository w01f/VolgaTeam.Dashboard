using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Online.Dictionaries;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Preview;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.PresentationClasses.Digital.Output;
using Asa.Media.Controls.PresentationClasses.Digital.Settings;
using Asa.Online.Controls.InteropClasses;
using Asa.Online.Controls.PresentationClasses.Summary;
using DevExpress.Skins;
using DevExpress.XtraTab;
using ResourceManager = Asa.Common.Core.Configuration.ResourceManager;

namespace Asa.Media.Controls.PresentationClasses.Digital.ContentEditors
{
	[ToolboxItem(false)]
	//public partial class DigitalSummaryEditorControl : UserControl,
	public partial class DigitalSummaryEditorControl : XtraTabPage,
		IDigitalEditor,
		IDigitalSummaryContainerControl,
		IDigitalOutputContainer
	{
		private bool _allowToSave;
		private bool _needToReload;
		private readonly DigitalEditorsContainer _container;
		private readonly List<DigitalProductSummaryControl> _summaryControls = new List<DigitalProductSummaryControl>();
		public DigitalSectionType SectionType => DigitalSectionType.Summary;
		public string HelpTag => "digitalsl";
		public event EventHandler<DataChangedEventArgs> DataChanged;

		public DigitalSummaryEditorControl(DigitalEditorsContainer container)
		{
			InitializeComponent();
			Text = ListManager.Instance.DefaultControlsConfiguration.SectionsSummaryTitle ?? "Digital Summary";
			_container = container;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemStatementToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemStatementToggle.MaxSize, scaleFactor);
			layoutControlItemStatementToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemStatementToggle.MinSize, scaleFactor);
			emptySpaceItemInvestemtsSeparator.MaxSize = RectangleHelper.ScaleSize(emptySpaceItemInvestemtsSeparator.MaxSize, scaleFactor);
			emptySpaceItemInvestemtsSeparator.MinSize = RectangleHelper.ScaleSize(emptySpaceItemInvestemtsSeparator.MinSize, scaleFactor);
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
				summaryControl.Height = (Int32)(summaryControl.Height * Utilities.GetScaleFactor(CreateGraphics().DpiX).Height);
				summaryControl.LoadData(product);
				return summaryControl;
			}));
			if (_summaryControls.Any())
			{
				xtraScrollableControl.Controls.AddRange(_summaryControls.OfType<Control>().Reverse().ToArray());
				xtraScrollableControl.ScrollControlIntoView(_summaryControls[0]);
				SetFocus();
			}

			checkEditStatementValue.Checked = !String.IsNullOrEmpty(_container.EditedContent.DigitalProductSummary.Statement);
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
			_container.EditedContent.DigitalProductSummary.Statement = checkEditStatementValue.Checked ? memoEditStatement.EditValue as String : null;
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
			memoEditStatement.Enabled = checkEditStatementValue.Checked;
			if (!checkEditStatementValue.Checked)
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
				DataChanged?.Invoke(this, new DataChangedEventArgs { ChangedSectionType = SectionType });
		}

		#region Output
		public List<Dictionary<string, string>> OutputReplacementsLists { get; set; }
		public SlideType SlideType => SlideType.DigitalSummary;

		public Theme SelectedTheme
		{
			get
			{
				var selectedTheme = MediaMetaData.Instance.SettingsManager.GetSelectedThemeName(SlideType);
				return BusinessObjects.Instance.ThemeManager.GetThemes(SlideType).FirstOrDefault(t => t.Name.Equals(selectedTheme) || String.IsNullOrEmpty(selectedTheme));
			}
		}

		private static int RowsPerSlide => 7;

		public void PopulateReplacementsList()
		{
			if (_needToReload)
				LoadData();

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

				if (checkEditStatementValue.Checked || checkEditMonthlyInvestment.Checked || checkEditTotalInvestment.Checked)
				{
					var statements = new List<string>();
					if (checkEditStatementValue.Checked && memoEditStatement.EditValue != null)
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

		public OutputGroup GetOutputGroup()
		{
			var outputGroup = new OutputGroup
			{
				Name = Text,
				IsCurrent = TabControl != null && TabControl.SelectedTabPage == this
			};

			if (_container.EditedContent.DigitalProducts.Any(p => !String.IsNullOrEmpty(p.Name)))
				outputGroup.Items = new List<OutputItem>(new[]
				{
					new OutputItem
					{
						Name = Text,
						PresentationSourcePath =
							Path.Combine(ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName())),
						SlidesCount = _summaryControls.Count / RowsPerSlide + (_summaryControls.Count % RowsPerSlide > 0 ? 1 : 0),
						IsCurrent = true,
					    Enabled = outputGroup.IsCurrent && BusinessObjects.Instance.OutputManager.DigitalSlideOutputConfiguration.EnablePackages,
						SlideGeneratingAction = (processor, destinationPresentation) =>
						{
							PopulateReplacementsList();
							processor.AppendDigitalSummary(this, destinationPresentation);
						},
						PreviewGeneratingAction = (processor, filePath) =>
						{
							PopulateReplacementsList();
							processor.PrepareDigitalSummaryEmail(filePath, this);
						}
					}
				});
			return outputGroup;
		}
		#endregion
	}
}
