using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Snapshot;
using Asa.Common.GUI.RetractableBar;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.PresentationClasses.SnapshotControls.ContentEditors;
using Asa.Media.Controls.PresentationClasses.SnapshotControls.Output;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Settings
{
	public partial class SettingsContainer : UserControl
	{
		private readonly List<ISettingsControl> _settingsControls = new List<ISettingsControl>();
		private readonly List<SnapshotEditorSettingsRelation> _snapshotEditorSettings = new List<SnapshotEditorSettingsRelation>();
		private SnapshotEditorType _selectedEditorType;
		private SnapshotContent _editedContent;
		private Snapshot _editedSnapshot;

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
			_snapshotEditorSettings.AddRange(new[]
			{
				new SnapshotEditorSettingsRelation 
				{
					EditorType = SnapshotEditorType.Schedule,
					SettingsTypes = new []
					{
						SnapshotSettingsType.Schedule,
						SnapshotSettingsType.Colors,
						SnapshotSettingsType.Calendar,
						SnapshotSettingsType.AdvancedColumns,
						SnapshotSettingsType.Contract,
					}
				} ,
				new SnapshotEditorSettingsRelation
				{
					EditorType = SnapshotEditorType.DigitalInfo,
					SettingsTypes = new []
					{
						SnapshotSettingsType.DigitalInfo,
						SnapshotSettingsType.Contract
					}
				},
				new SnapshotEditorSettingsRelation
				{
					EditorType = SnapshotEditorType.Summary,
					SettingsTypes = new []
					{
						SnapshotSettingsType.Summary,
						SnapshotSettingsType.Colors,
						SnapshotSettingsType.Contract
					}
				},
			});
		}

		private void InitSettingsControls()
		{
			_settingsControls.AddRange(new ISettingsControl[]
				{
					new SnapshotColumnSettingsControl(),
					new SnapshotDigitalInfoSettingsControl(),
					new SummaryColumnSettingsControl(),
					new ActiveWeeksSettingsControl(), 
					new ColorSettingsControl(),
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
		public void LoadContent(SnapshotContent editedContent)
		{
			_editedContent = editedContent;
			_settingsControls.OfType<IContentSettingsControl>().ToList().ForEach(c => c.LoadContentData(_editedContent));
		}

		public void LoadSnapshot(Snapshot editedSnapshot)
		{
			_editedSnapshot = editedSnapshot;
			_settingsControls.OfType<ISnapshotSettingsControl>().ToList().ForEach(c => c.LoadSnapshotData(editedSnapshot));
		}

		public void UpdateSettingsAccordingSelectedEditor(SnapshotEditorType editorType)
		{
			_selectedEditorType = editorType;

			var selectedTabPage = xtraTabControlOptions.SelectedTabPage;
			xtraTabControlOptions.TabPages.Clear();
			var contentRelation = _snapshotEditorSettings.FirstOrDefault(r => r.EditorType == editorType);
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
				contentRelation.SettingsTypes.Contains(SnapshotSettingsType.AdvancedColumns);
			hyperLinkEditInfoAdvanced.BringToFront();
			hyperLinkEditInfoContract.Visible =
				contentRelation != null &&
				contentRelation.SettingsTypes.Contains(SnapshotSettingsType.Contract);
			hyperLinkEditInfoContract.BringToFront();
			SettingsControlsUpdated?.Invoke(this, EventArgs.Empty);
		}

		public void UpdateSettingsAccordingDataChanges(SnapshotEditorType editorType)
		{
			switch (editorType)
			{
				case SnapshotEditorType.Schedule:
					_settingsControls.OfType<SnapshotColumnSettingsControl>().First().UpdateUniversalSettingsToggleVisibility();
					break;
			}
		}

		private void RaiseSettingsChanged(SettingsChangedEventArgs args)
		{
			if(args.ChangedSettingsType== SnapshotSettingsType.Schedule)
				_settingsControls.OfType<SummaryColumnSettingsControl>().First().LoadContentData(_editedContent);
			SettingsChanged?.Invoke(this, args);
		}

		private void OnSettingsChanged(object sender, SettingsChangedEventArgs e)
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
				switch (_selectedEditorType)
				{
					case SnapshotEditorType.Schedule:
						form.checkEditUseDecimalRate.Checked = _editedSnapshot.UseDecimalRates;
						form.checkEditShowSpotX.Checked = _editedSnapshot.ShowSpotsX;
						form.checkEditShowSpotsPerWeek.Enabled = true;
						form.labelControlDescriptionShowSpotsPerWeek.Enabled = true;
						form.checkEditShowSpotsPerWeek.Checked = _editedSnapshot.ShowSpotsPerWeek;
						form.checkEditCloneLineToTheEnd.Enabled = true;
						form.labelControlDescriptionCloneLineToTheEnd.Enabled = true;
						form.checkEditCloneLineToTheEnd.Checked = _editedSnapshot.CloneLineToTheEnd;
						break;
					case SnapshotEditorType.Summary:
						form.checkEditShowSpotsPerWeek.Enabled = false;
						form.labelControlDescriptionShowSpotsPerWeek.Enabled = false;
						form.checkEditUseDecimalRate.Checked = _editedContent.SnapshotSummary.UseDecimalRates;
						form.checkEditShowSpotX.Checked = _editedContent.SnapshotSummary.ShowSpotsX;
						form.checkEditCloneLineToTheEnd.Enabled = false;
						form.labelControlDescriptionCloneLineToTheEnd.Enabled = false;
						form.checkEditCloneLineToTheEnd.Checked = false;
						break;
					default:
						return;
				}
				form.checkEditLockToMaster.Checked = MediaMetaData.Instance.SettingsManager.UseSlideMaster;
				if (form.ShowDialog() != DialogResult.OK) return;
				switch (_selectedEditorType)
				{
					case SnapshotEditorType.Schedule:
						_editedSnapshot.UseDecimalRates = form.checkEditUseDecimalRate.Checked;
						_editedSnapshot.ShowSpotsX = form.checkEditShowSpotX.Checked;
						_editedSnapshot.ShowSpotsPerWeek = form.checkEditShowSpotsPerWeek.Checked;
						_editedSnapshot.CloneLineToTheEnd = form.checkEditCloneLineToTheEnd.Checked;
						if (_editedContent.SnapshotSummary.ApplySettingsForAll)
						{
							foreach (var snapshot in _editedContent.Snapshots.Where(os => os.UniqueID != _editedSnapshot.UniqueID))
							{
								snapshot.UseDecimalRates = form.checkEditUseDecimalRate.Checked;
								snapshot.ShowSpotsX = form.checkEditShowSpotX.Checked;
								snapshot.ShowSpotsPerWeek  = form.checkEditShowSpotsPerWeek.Checked;
								snapshot.CloneLineToTheEnd = form.checkEditCloneLineToTheEnd.Checked;
							}
						}
						break;
					case SnapshotEditorType.Summary:
						_editedContent.SnapshotSummary.UseDecimalRates = form.checkEditUseDecimalRate.Checked;
						_editedContent.SnapshotSummary.ShowSpotsX = form.checkEditShowSpotX.Checked;
						break;
				}
				MediaMetaData.Instance.SettingsManager.UseSlideMaster = form.checkEditLockToMaster.Checked;
				RaiseSettingsChanged(new SettingsChangedEventArgs
				{
					ChangedSettingsType = SnapshotSettingsType.AdvancedColumns,
				});
			}
		}

		private void OnContractSettingsEdit(object sender, OpenLinkEventArgs e)
		{
			e.Handled = true;
			using (var form = new FormContractSettings())
			{
				switch (_selectedEditorType)
				{
					case SnapshotEditorType.Schedule:
					case SnapshotEditorType.DigitalInfo:
						form.checkEditShowSignatureLine.Checked = _editedSnapshot.ContractSettings.ShowSignatureLine;
						form.checkEditShowRatesExpiration.Checked = _editedSnapshot.ContractSettings.RateExpirationDate.HasValue;
						form.checkEditShowDisclaimer.Checked = _editedSnapshot.ContractSettings.ShowDisclaimer;
						form.dateEditRatesExpirationDate.EditValue = _editedSnapshot.ContractSettings.RateExpirationDate;
						break;
					case SnapshotEditorType.Summary:
						form.checkEditShowSignatureLine.Checked = _editedContent.SnapshotSummary.ContractSettings.ShowSignatureLine;
						form.checkEditShowRatesExpiration.Checked = _editedContent.SnapshotSummary.ContractSettings.RateExpirationDate.HasValue;
						form.checkEditShowDisclaimer.Checked = _editedContent.SnapshotSummary.ContractSettings.ShowDisclaimer;
						form.dateEditRatesExpirationDate.EditValue = _editedContent.SnapshotSummary.ContractSettings.RateExpirationDate;
						break;
					default:
						return;
				}
				if (form.ShowDialog() != DialogResult.OK) return;
				switch (_selectedEditorType)
				{
					case SnapshotEditorType.Schedule:
					case SnapshotEditorType.DigitalInfo:
						_editedSnapshot.ContractSettings.ShowSignatureLine = form.checkEditShowSignatureLine.Checked;
						_editedSnapshot.ContractSettings.ShowDisclaimer = form.checkEditShowDisclaimer.Checked;
						_editedSnapshot.ContractSettings.RateExpirationDate = (DateTime?)form.dateEditRatesExpirationDate.EditValue;
						if (_editedContent.SnapshotSummary.ApplySettingsForAll)
						{
							foreach (var Snapshot in _editedContent.Snapshots.Where(os => os.UniqueID != _editedSnapshot.UniqueID))
							{
								Snapshot.ContractSettings.ShowSignatureLine = _editedSnapshot.ContractSettings.ShowSignatureLine;
								Snapshot.ContractSettings.ShowDisclaimer = _editedSnapshot.ContractSettings.ShowDisclaimer;
								Snapshot.ContractSettings.RateExpirationDate = _editedSnapshot.ContractSettings.RateExpirationDate;
							}
						}
						break;
					case SnapshotEditorType.Summary:
						_editedContent.SnapshotSummary.ContractSettings.ShowSignatureLine = form.checkEditShowSignatureLine.Checked;
						_editedContent.SnapshotSummary.ContractSettings.ShowDisclaimer = form.checkEditShowDisclaimer.Checked;
						_editedContent.SnapshotSummary.ContractSettings.RateExpirationDate = (DateTime?)form.dateEditRatesExpirationDate.EditValue;
						break;
					default:
						return;
				}
				RaiseSettingsChanged(new SettingsChangedEventArgs
				{
					ChangedSettingsType = SnapshotSettingsType.Contract
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
