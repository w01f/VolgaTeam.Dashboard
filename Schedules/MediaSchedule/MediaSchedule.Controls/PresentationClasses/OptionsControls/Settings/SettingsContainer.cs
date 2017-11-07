using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Option;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.RetractableBar;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.PresentationClasses.OptionsControls.ContentEditors;
using Asa.Media.Controls.PresentationClasses.OptionsControls.Output;
using DevExpress.Skins;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Settings
{
	public partial class SettingsContainer : UserControl
	{
		private readonly List<ISettingsControl> _settingsControls = new List<ISettingsControl>();
		private readonly List<OptionEditorSettingsRelation> _optionEditorSettings = new List<OptionEditorSettingsRelation>();
		private OptionEditorType _selectedEditorType;
		private OptionsContent _editedContent;
		private OptionSet _editedOptionSet;

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

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemInfoAdvanced.MaxSize = RectangleHelper.ScaleSize(layoutControlItemInfoAdvanced.MaxSize, scaleFactor);
			layoutControlItemInfoAdvanced.MinSize = RectangleHelper.ScaleSize(layoutControlItemInfoAdvanced.MinSize, scaleFactor);
			layoutControlItemInfoContract.MaxSize = RectangleHelper.ScaleSize(layoutControlItemInfoContract.MaxSize, scaleFactor);
			layoutControlItemInfoContract.MinSize = RectangleHelper.ScaleSize(layoutControlItemInfoContract.MinSize, scaleFactor);
		}

		private void InitContentEditorSettingsRelations()
		{
			_optionEditorSettings.AddRange(new[]
			{
				new OptionEditorSettingsRelation
				{
					EditorType = OptionEditorType.Schedule,
					SettingsTypes = new []
					{
						OptionSettingsType.Schedule,
						OptionSettingsType.Colors,
						OptionSettingsType.AdvancedColumns,
						OptionSettingsType.Contract,
					}
				} ,
				new OptionEditorSettingsRelation
				{
					EditorType = OptionEditorType.DigitalInfo,
					SettingsTypes = new []
					{
						OptionSettingsType.DigitalInfo,
						OptionSettingsType.Contract
					}
				},
				new OptionEditorSettingsRelation
				{
					EditorType = OptionEditorType.Summary,
					SettingsTypes = new []
					{
						OptionSettingsType.Summary,
						OptionSettingsType.Colors,
						OptionSettingsType.Contract
					}
				},
			});
		}

		private void InitSettingsControls()
		{
			_settingsControls.AddRange(new ISettingsControl[]
				{
					new OptionSetColumnSettingsControl(),
					new OptionsDigitalInfoSettingsControl(),
					new SummaryColumnSettingsControl(),
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
		public void LoadContent(OptionsContent editedContent)
		{
			_editedContent = editedContent;
			_settingsControls.OfType<IContentSettingsControl>().ToList().ForEach(c => c.LoadContentData(_editedContent));
		}

		public void LoadOptionSet(OptionSet editedOptionSet)
		{
			_editedOptionSet = editedOptionSet;
			_settingsControls.OfType<IOptionSetSettingsControl>().ToList().ForEach(c => c.LoadOptionsSetData(editedOptionSet));
		}

		public void UpdateSettingsAccordingSelectedEditor(OptionEditorType editorType)
		{
			_selectedEditorType = editorType;

			var selectedTabPage = xtraTabControlOptions.SelectedTabPage;
			xtraTabControlOptions.TabPages.Clear();
			var contentRelation = _optionEditorSettings.FirstOrDefault(r => r.EditorType == editorType);
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

			layoutControlItemInfoAdvanced.Visibility =
				contentRelation != null &&
				contentRelation.SettingsTypes.Contains(OptionSettingsType.AdvancedColumns) ? LayoutVisibility.Always : LayoutVisibility.Never;
			layoutControlItemInfoContract.Visibility =
				BusinessObjects.Instance.OutputManager.ContractTemplateFolder.ExistsLocal() &&
				contentRelation != null &&
				contentRelation.SettingsTypes.Contains(OptionSettingsType.Contract) ? LayoutVisibility.Always : LayoutVisibility.Never;
			emptySpaceItemInfo.Visibility =
				contentRelation != null &&
				contentRelation.SettingsTypes.Contains(OptionSettingsType.AdvancedColumns) &&
				contentRelation.SettingsTypes.Contains(OptionSettingsType.Contract) &&
				BusinessObjects.Instance.OutputManager.ContractTemplateFolder.ExistsLocal()
				? LayoutVisibility.Always : LayoutVisibility.Never;

			SettingsControlsUpdated?.Invoke(this, EventArgs.Empty);
		}

		public void UpdateSettingsAccordingDataChanges(OptionEditorType editorType)
		{
			switch (editorType)
			{
				case OptionEditorType.Schedule:
					_settingsControls.OfType<OptionSetColumnSettingsControl>().First().UpdateUniversalSettingsToggleVisibility();
					break;
			}
		}

		private void RaiseSettingsChanged(SettingsChangedEventArgs args)
		{
			if (args.ChangedSettingsType == OptionSettingsType.Schedule)
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
					case OptionEditorType.Schedule:
						form.checkEditUseDecimalRate.Checked = _editedOptionSet.UseDecimalRates;
						form.checkEditShowSpotX.Checked = _editedOptionSet.ShowSpotsX;
						
						form.layoutControlItemCloneLineToTheEnd.Enabled = true;
						form.simpleLabelItemCloneLineToTheEnd.Enabled = true;
						form.checkEditCloneLineToTheEnd.Checked = _editedOptionSet.CloneLineToTheEnd;
						break;
					case OptionEditorType.Summary:
						form.checkEditUseDecimalRate.Checked = _editedContent.OptionsSummary.UseDecimalRates;
						form.checkEditShowSpotX.Checked = _editedContent.OptionsSummary.ShowSpotsX;

						form.layoutControlItemCloneLineToTheEnd.Enabled = false;
						form.simpleLabelItemCloneLineToTheEnd.Enabled = false;
						form.checkEditCloneLineToTheEnd.Checked = false;
						break;
					default:
						return;
				}
				form.checkEditLockToMaster.Checked = MediaMetaData.Instance.SettingsManager.UseSlideMaster;
				if (form.ShowDialog() != DialogResult.OK) return;
				switch (_selectedEditorType)
				{
					case OptionEditorType.Schedule:
						_editedOptionSet.UseDecimalRates = form.checkEditUseDecimalRate.Checked;
						_editedOptionSet.ShowSpotsX = form.checkEditShowSpotX.Checked;
						_editedOptionSet.CloneLineToTheEnd = form.checkEditCloneLineToTheEnd.Checked;
						if (_editedContent.OptionsSummary.ApplySettingsForAll)
						{
							foreach (var optionSet in _editedContent.Options.Where(os => os.UniqueID != _editedOptionSet.UniqueID))
							{
								optionSet.UseDecimalRates = form.checkEditUseDecimalRate.Checked;
								optionSet.ShowSpotsX = form.checkEditShowSpotX.Checked;
								optionSet.CloneLineToTheEnd = form.checkEditCloneLineToTheEnd.Checked;
							}
						}
						break;
					case OptionEditorType.Summary:
						_editedContent.OptionsSummary.UseDecimalRates = form.checkEditUseDecimalRate.Checked;
						_editedContent.OptionsSummary.ShowSpotsX = form.checkEditShowSpotX.Checked;
						break;
				}
				MediaMetaData.Instance.SettingsManager.UseSlideMaster = form.checkEditLockToMaster.Checked;
				RaiseSettingsChanged(new SettingsChangedEventArgs
				{
					ChangedSettingsType = OptionSettingsType.AdvancedColumns,
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
					case OptionEditorType.Schedule:
					case OptionEditorType.DigitalInfo:
						form.checkEditShowSignatureLine.Checked = _editedOptionSet.ContractSettings.ShowSignatureLine;
						form.checkEditShowRatesExpiration.Checked = _editedOptionSet.ContractSettings.RateExpirationDate.HasValue;
						form.checkEditShowDisclaimer.Checked = _editedOptionSet.ContractSettings.ShowDisclaimer;
						form.dateEditRatesExpirationDate.EditValue = _editedOptionSet.ContractSettings.RateExpirationDate;
						break;
					case OptionEditorType.Summary:
						form.checkEditShowSignatureLine.Checked = _editedContent.OptionsSummary.ContractSettings.ShowSignatureLine;
						form.checkEditShowRatesExpiration.Checked = _editedContent.OptionsSummary.ContractSettings.RateExpirationDate.HasValue;
						form.checkEditShowDisclaimer.Checked = _editedContent.OptionsSummary.ContractSettings.ShowDisclaimer;
						form.dateEditRatesExpirationDate.EditValue = _editedContent.OptionsSummary.ContractSettings.RateExpirationDate;
						break;
					default:
						return;
				}
				if (form.ShowDialog() != DialogResult.OK) return;
				switch (_selectedEditorType)
				{
					case OptionEditorType.Schedule:
					case OptionEditorType.DigitalInfo:
						_editedOptionSet.ContractSettings.ShowSignatureLine = form.checkEditShowSignatureLine.Checked;
						_editedOptionSet.ContractSettings.ShowDisclaimer = form.checkEditShowDisclaimer.Checked;
						_editedOptionSet.ContractSettings.RateExpirationDate = (DateTime?)form.dateEditRatesExpirationDate.EditValue;
						if (_editedContent.OptionsSummary.ApplySettingsForAll)
						{
							foreach (var optionSet in _editedContent.Options.Where(os => os.UniqueID != _editedOptionSet.UniqueID))
							{
								optionSet.ContractSettings.ShowSignatureLine = _editedOptionSet.ContractSettings.ShowSignatureLine;
								optionSet.ContractSettings.ShowDisclaimer = _editedOptionSet.ContractSettings.ShowDisclaimer;
								optionSet.ContractSettings.RateExpirationDate = _editedOptionSet.ContractSettings.RateExpirationDate;
							}
						}
						break;
					case OptionEditorType.Summary:
						_editedContent.OptionsSummary.ContractSettings.ShowSignatureLine = form.checkEditShowSignatureLine.Checked;
						_editedContent.OptionsSummary.ContractSettings.ShowDisclaimer = form.checkEditShowDisclaimer.Checked;
						_editedContent.OptionsSummary.ContractSettings.RateExpirationDate = (DateTime?)form.dateEditRatesExpirationDate.EditValue;
						break;
					default:
						return;
				}
				RaiseSettingsChanged(new SettingsChangedEventArgs
				{
					ChangedSettingsType = OptionSettingsType.Contract
				});
			}
		}
		#endregion
	}
}
