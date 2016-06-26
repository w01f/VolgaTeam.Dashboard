using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Common.GUI.RetractableBar;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings
{
	public partial class SettingsContainer : UserControl
	{
		private readonly List<ISettingsControl> _settingsControls = new List<ISettingsControl>();
		private readonly List<SectionEditorSettingsRelation> _sectionEditorSettings = new List<SectionEditorSettingsRelation>();
		private ProgramScheduleContent _editedContent;
		private ScheduleSection _editedSection;

		public event EventHandler<SettingsChangedEventArgs> SettingsChanged;
		public event EventHandler<EventArgs> SettingsControlsUpdated;

		public SettingsContainer()
		{
			InitializeComponent();
		}

		#region Controls Management
		public void InitControl()
		{
			InitSettingsControls();
			InitContentEditorSettingsRelations();

			if (CreateGraphics().DpiX > 96)
			{
				var regularFont = xtraTabControlOptions.AppearancePage.Header.Font;
				var activeFont = xtraTabControlOptions.AppearancePage.HeaderActive.Font;
				regularFont = new Font(regularFont.FontFamily, regularFont.Size - 2, regularFont.Style);
				activeFont = new Font(activeFont.FontFamily, activeFont.Size - 2, activeFont.Style);
				xtraTabControlOptions.AppearancePage.Header.Font = regularFont;
				xtraTabControlOptions.AppearancePage.HeaderActive.Font = activeFont;
				xtraTabControlOptions.AppearancePage.HeaderDisabled.Font = regularFont;
				xtraTabControlOptions.AppearancePage.HeaderHotTracked.Font = regularFont;

				hyperLinkEditInfoContract.Enabled = BusinessObjects.Instance.OutputManager.ContractTemplateFolder.ExistsLocal();
				hyperLinkEditInfoAdvanced.Font = new Font(hyperLinkEditInfoAdvanced.Font.FontFamily,
					hyperLinkEditInfoAdvanced.Font.Size - 2, hyperLinkEditInfoAdvanced.Font.Style);
				hyperLinkEditInfoContract.Font = new Font(hyperLinkEditInfoContract.Font.FontFamily,
					hyperLinkEditInfoContract.Font.Size - 2, hyperLinkEditInfoContract.Font.Style);
			}
		}

		private void InitContentEditorSettingsRelations()
		{
			_sectionEditorSettings.AddRange(new[]
			{
				new SectionEditorSettingsRelation
				{
					EditorType = SectionEditorType.ScheduleSection,
					SettingsTypes = new []
					{
						ScheduleSettingsType.Columns,
						ScheduleSettingsType.Totals,
						ScheduleSettingsType.Colors,
						ScheduleSettingsType.AdvancedColumns,
						ScheduleSettingsType.Contract
					}
				} ,
				new SectionEditorSettingsRelation
				{
					EditorType = SectionEditorType.DigitalSection,
					SettingsTypes = new []
					{
						ScheduleSettingsType.DigitalInfo,
						ScheduleSettingsType.Contract
					}
				},
				new SectionEditorSettingsRelation
				{
					EditorType = SectionEditorType.CustomSummary,
					SettingsTypes = new []
					{
						ScheduleSettingsType.CustomSummary,
						ScheduleSettingsType.Contract
					}
				},
			});
		}

		private void InitSettingsControls()
		{
			_settingsControls.AddRange(new ISettingsControl[]
				{
					new SectionColumnSettingsControl(),
					new SectionTotalsSettingsControl(),
					new ColorSettingsControl(),
					new SectionDigitalInfoSettingsControl(), 
					new CustomSummaryInfoControl(),
				});
			foreach (var settingsDataControl in _settingsControls.OfType<ISettingsDataControl>())
				settingsDataControl.DataChanged += OnSettingsChanged;
		}

		public IEnumerable<ButtonInfo> GetSettingsButtons()
		{
			return xtraTabControlOptions.TabPages
				.OfType<ISettingsControl>()
				.Select(sc => sc.BarButton)
				.ToList();
		}
		#endregion

		#region Data Management
		public void LoadContent(ProgramScheduleContent editedContent)
		{
			_editedContent = editedContent;
			_settingsControls.OfType<IContentSettingsControl>().ToList().ForEach(c => c.LoadContentData(_editedContent));
		}

		public void LoadSection(ScheduleSection editedSection)
		{
			_editedSection = editedSection;
			_settingsControls.OfType<ISectionSettingsControl>().ToList().ForEach(c => c.LoadSectionData(_editedSection));
		}

		public void UpdateSettingsAccordingSelectedSectionEditor(SectionEditorType editorType)
		{
			var selectedTabPage = xtraTabControlOptions.SelectedTabPage;
			xtraTabControlOptions.TabPages.Clear();
			var contentRelation = _sectionEditorSettings.FirstOrDefault(r => r.EditorType == editorType);
			if (contentRelation != null)
			{
				xtraTabControlOptions.TabPages.AddRange(_settingsControls
					.Where(sc => sc.IsAvailable && contentRelation.SettingsTypes.Contains(sc.SettingsType))
					.OrderBy(sc => sc.Order)
					.OfType<XtraTabPage>()
					.Where(tp => tp.PageVisible)
					.ToArray());
				if (selectedTabPage != null && xtraTabControlOptions.TabPages.Contains(selectedTabPage))
					xtraTabControlOptions.SelectedTabPage = selectedTabPage;
			}
			hyperLinkEditInfoAdvanced.Visible =
				contentRelation != null &&
				contentRelation.SettingsTypes.Contains(ScheduleSettingsType.AdvancedColumns);
			hyperLinkEditInfoAdvanced.BringToFront();
			hyperLinkEditInfoContract.Visible =
				contentRelation != null &&
				contentRelation.SettingsTypes.Contains(ScheduleSettingsType.Contract);
			hyperLinkEditInfoContract.BringToFront();
			SettingsControlsUpdated?.Invoke(this, EventArgs.Empty);
		}

		public void UpdateSettingsAccordingDataChanges(SectionEditorType editorType)
		{
			switch (editorType)
			{
				case SectionEditorType.ScheduleSection:
					_settingsControls.OfType<SectionColumnSettingsControl>().First().UpdateUniversalSettingsToggleVisibility();
					break;
				case SectionEditorType.CustomSummary:
					_settingsControls.OfType<CustomSummaryInfoControl>().First().UpdateTotals();
					break;
			}
		}

		private void RaiseSettingsChanged(SettingsChangedEventArgs args)
		{
			SettingsChanged?.Invoke(this, args);
		}

		private void OnSettingsChanged(Object sender, SettingsChangedEventArgs e)
		{
			RaiseSettingsChanged(e);
		}
		#endregion

		#region GUI Event Handlers
		private void OnAdvancedSettingsEdit(object sender, OpenLinkEventArgs e)
		{
			e.Handled = true;
			using (var form = new FormOutputSettings())
			{
				form.checkEditEmptySports.Text = String.Format(form.checkEditEmptySports.Text, String.Format("{0}s:", _editedSection.Parent.ScheduleSettings.SelectedSpotType));
				form.checkEditEmptySports.Enabled = _editedSection.ShowSpots;
				form.labelControlDescriptionEmptySports.Enabled = _editedSection.ShowSpots;
				form.checkEditEmptySports.Checked = !_editedSection.ShowEmptySpots;
				form.checkEditOutputNoBrackets.Checked = _editedSection.OutputNoBrackets;
				form.checkEditUseGenericDates.Checked = _editedSection.UseGenericDateColumns;
				form.checkEditUseDecimalRate.Checked = _editedSection.UseDecimalRates;
				form.checkEditCloneLineToTheEnd.Checked = _editedSection.CloneLineToTheEnd;
				form.checkEditOutputLimitQuarters.Enabled = _editedSection.Parent.ScheduleSettings.Quarters.Count > 1;
				form.labelControlDescriptionOutputLimitQuarters.Enabled = _editedSection.Parent.ScheduleSettings.Quarters.Count > 1;
				form.checkEditOutputLimitQuarters.Checked = _editedSection.Parent.ScheduleSettings.Quarters.Count > 1 && _editedSection.OutputPerQuater;
				form.checkEditOutputLimitPeriods.Checked = _editedSection.OutputMaxPeriods.HasValue;
				form.spinEditOutputLimitPeriods.EditValue = _editedSection.OutputMaxPeriods;
				form.checkEditOutputLimitPeriods.Text = String.Format(form.checkEditOutputLimitPeriods.Text, _editedSection.Parent.ScheduleSettings.SelectedSpotType);
				form.checkEditLockToMaster.Checked = MediaMetaData.Instance.SettingsManager.UseSlideMaster;

				if (form.ShowDialog() != DialogResult.OK) return;

				var updateColumns = _editedSection.UseGenericDateColumns != form.checkEditUseGenericDates.Checked;

				_editedSection.ShowEmptySpots = !form.checkEditEmptySports.Checked;
				_editedSection.OutputNoBrackets = form.checkEditOutputNoBrackets.Checked;
				_editedSection.UseDecimalRates = form.checkEditUseDecimalRate.Checked;
				_editedSection.CloneLineToTheEnd = form.checkEditCloneLineToTheEnd.Checked;
				_editedSection.UseGenericDateColumns = form.checkEditUseGenericDates.Checked;
				_editedSection.OutputPerQuater = form.checkEditOutputLimitQuarters.Checked;
				_editedSection.OutputMaxPeriods = form.spinEditOutputLimitPeriods.EditValue != null ? (Int32?)form.spinEditOutputLimitPeriods.Value : null;

				MediaMetaData.Instance.SettingsManager.UseSlideMaster = form.checkEditLockToMaster.Checked;

				RaiseSettingsChanged(new SettingsChangedEventArgs
				{
					ChangedSettingsType = ScheduleSettingsType.AdvancedColumns,
					UpdateGridColums = updateColumns
				});
			}
		}

		private void OnContractSettingsEdit(object sender, OpenLinkEventArgs e)
		{
			e.Handled = true;
			using (var form = new FormContractSettings())
			{
				form.checkEditShowSignatureLine.Checked = _editedSection.ContractSettings.ShowSignatureLine;
				form.checkEditShowRatesExpiration.Checked = _editedSection.ContractSettings.RateExpirationDate.HasValue;
				form.checkEditShowDisclaimer.Checked = _editedSection.ContractSettings.ShowDisclaimer;
				form.dateEditRatesExpirationDate.EditValue = _editedSection.ContractSettings.RateExpirationDate;
				if (form.ShowDialog() != DialogResult.OK) return;
				_editedSection.ContractSettings.ShowSignatureLine = form.checkEditShowSignatureLine.Checked;
				_editedSection.ContractSettings.ShowDisclaimer = form.checkEditShowDisclaimer.Checked;
				_editedSection.ContractSettings.RateExpirationDate = (DateTime?)form.dateEditRatesExpirationDate.EditValue;
				RaiseSettingsChanged(new SettingsChangedEventArgs
				{
					ChangedSettingsType = ScheduleSettingsType.Contract
				});
			}
		}

		private void OnBottomPanelResize(object sender, EventArgs e)
		{
			hyperLinkEditInfoAdvanced.Width = pnInfoBottom.Width / 2;
		}
		#endregion
	}
}
