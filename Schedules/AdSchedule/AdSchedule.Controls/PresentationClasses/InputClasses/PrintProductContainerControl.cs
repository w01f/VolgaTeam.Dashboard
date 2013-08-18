using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.Properties;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;
using ListManager = NewBizWiz.Core.AdSchedule.ListManager;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.InputClasses
{
	[ToolboxItem(false)]
	public partial class PrintProductContainerControl : UserControl
	{
		private readonly List<PrintProductControl> _tabPages = new List<PrintProductControl>();
		private bool _allowToSave;

		public PrintProductContainerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			xtraTabControlPublications.SelectedPageChanged += xtraTabControlPublications_SelectedPageChanged;
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			});
		}

		public bool SettingsNotSaved { get; set; }
		public Schedule LocalSchedule { get; set; }

		public bool AllowToLeaveControl
		{
			get
			{
				foreach (XtraTabPage tab in xtraTabControlPublications.TabPages)
				{
					((PrintProductControl)tab).advBandedGridViewPublication.CloseEditor();
				}
				bool result = false;
				if (SettingsNotSaved)
				{
					if (SaveSchedule())
						result = true;
				}
				else
					result = true;
				return result;
			}
		}

		#region Common Methods
		private void AssignCloseActiveEditorsonOutSideClick(Control control)
		{
			if (control != Controller.Instance.HomeBusinessName
				&& control != Controller.Instance.HomeClientType
				&& control != Controller.Instance.HomeDecisionMaker
				&& control != Controller.Instance.HomeAccountNumberText
				&& control != Controller.Instance.PrintProductRateCard
				&& control != Controller.Instance.RateCardCombo
				&& control != Controller.Instance.HomeFlightDatesEnd
				&& control != Controller.Instance.HomeFlightDatesStart
				&& control != Controller.Instance.HomePresentationDate
				&& control != Controller.Instance.PrintProductStandartHeight
				&& control != Controller.Instance.PrintProductStandartWidth
				&& control != Controller.Instance.PrintProductPercentOfPage
				&& control != Controller.Instance.PrintProductSharePagePageSizeCombo
				&& control != Controller.Instance.PrintProductStandartPageSizeCombo
				&& control != Controller.Instance.PrintProductSharePageSquare
				&& control != Controller.Instance.PrintProductCostPerInch)
			{
				control.Click += CloseActiveEditorsonOutSideClick;
				foreach (Control childControl in control.Controls)
					AssignCloseActiveEditorsonOutSideClick(childControl);
			}
		}

		private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
		{
			Focus();
			foreach (PrintProductControl control in xtraTabControlPublications.TabPages)
			{
				control.advBandedGridViewPublication.CloseEditor();
				Controller.Instance.ScheduleSettings.gridViewPrintProducts.CloseEditor();
			}
		}

		public void LoadSchedule(bool quickLoad)
		{
			LocalSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			laScheduleWindow.Text = string.Format("{0} - {1}", new object[] { LocalSchedule.FlightDateStart.ToString("MM/dd/yy"), LocalSchedule.FlightDateEnd.ToString("MM/dd/yy") });
			laAdvertiser.Text = LocalSchedule.BusinessName + (!string.IsNullOrEmpty(LocalSchedule.AccountNumber) ? (" - " + LocalSchedule.AccountNumber) : string.Empty);
			Controller.Instance.UpdateOutputTabs(LocalSchedule.PrintProducts.Select(x => x.Inserts.Count).Sum() > 0);
			if (!quickLoad)
			{
				xtraTabControlPublications.SuspendLayout();
				Application.DoEvents();
				xtraTabControlPublications.SelectedPageChanged -= xtraTabControlPublications_SelectedPageChanged;
				;
				xtraTabControlPublications.TabPages.Clear();
				_tabPages.RemoveAll(x => !LocalSchedule.PrintProducts.Select(y => y.UniqueID).Contains(x.PrintProduct.UniqueID));
				foreach (PrintProduct publication in LocalSchedule.PrintProducts)
				{
					if (!string.IsNullOrEmpty(publication.Name))
					{
						PrintProductControl printProductTab = _tabPages.Where(x => x.PrintProduct.UniqueID.Equals(publication.UniqueID)).FirstOrDefault();
						if (printProductTab == null)
						{
							printProductTab = new PrintProductControl();
							_tabPages.Add(printProductTab);
							Application.DoEvents();
						}
						printProductTab.PrintProduct = publication;
						printProductTab.Text = publication.Name.Replace("&", "&&");
						publication.RefreshAvailableDays();
						printProductTab.LoadInserts();
						Application.DoEvents();
					}
				}
				_tabPages.Sort((x, y) => x.PrintProduct.Index.CompareTo(y.PrintProduct.Index));
				xtraTabControlPublications.TabPages.AddRange(_tabPages.ToArray());
				Application.DoEvents();
				LoadPublication();
				xtraTabControlPublications.SelectedPageChanged += xtraTabControlPublications_SelectedPageChanged;
				;
				xtraTabControlPublications.ResumeLayout();
			}
			else
			{
				foreach (PrintProduct publication in LocalSchedule.PrintProducts)
				{
					if (!string.IsNullOrEmpty(publication.Name))
					{
						PrintProductControl printProductTab = _tabPages.Where(x => x.PrintProduct.UniqueID.Equals(publication.UniqueID)).FirstOrDefault();
						if (printProductTab != null)
						{
							printProductTab.PrintProduct = publication;
							publication.RefreshAvailableDays();
							printProductTab.LoadInserts();
						}
						Application.DoEvents();
					}
				}
			}
			SettingsNotSaved = false;
		}

		private void ClearSettings()
		{
			_allowToSave = false;

			#region Clear Pricing
			Controller.Instance.PrintProductAdPricingColumnInches.Checked = false;
			Controller.Instance.PrintProductAdPricingFlat.Checked = false;
			Controller.Instance.PrintProductAdPricingPagePercent.Checked = false;
			#endregion

			#region Clear Size
			Controller.Instance.PrintProductAdSizeStandart.Visible = false;
			Controller.Instance.PrintProductAdSizeStandartSquare.Checked = false;
			Controller.Instance.PrintProductStandartHeight.Enabled = false;
			Controller.Instance.PrintProductStandartWidth.Enabled = false;
			Controller.Instance.PrintProductStandartWidth.Value = 0;
			Controller.Instance.PrintProductStandartHeight.Value = 0;
			Controller.Instance.PrintProductStandartWidth.Value = 0;
			Controller.Instance.PrintProductStandartEqualSign.Visible = false;
			Controller.Instance.PrintProductStandartSquareMetric.Visible = false;
			Controller.Instance.PrintProductStandartSquareValue.Visible = false;
			Controller.Instance.PrintProductStandartSquareValue.Text = "0.00";
			Controller.Instance.PrintProductStandartPageSizeCheck.Checked = false;
			Controller.Instance.PrintProductStandartPageSizeCombo.Enabled = false;
			Controller.Instance.PrintProductStandartPageSizeCombo.EditValue = null;

			Controller.Instance.PrintProductAdSizeSharePage.Visible = false;
			Controller.Instance.PrintProductRateCard.EditValue = null;
			Controller.Instance.PrintProductPercentOfPage.EditValue = null;
			Controller.Instance.PrintProductPercentOfPage.Enabled = false;
			Controller.Instance.PrintProductSharePagePageSizeCheck.Checked = false;
			Controller.Instance.PrintProductSharePagePageSizeCombo.Enabled = false;
			Controller.Instance.PrintProductSharePagePageSizeCombo.EditValue = null;
			Controller.Instance.PrintProductSharePageSquare.Items.Clear();
			Controller.Instance.PrintProductSharePageSquare.Enabled = false;
			#endregion

			#region Clear Color
			Controller.Instance.PrintProductColorOptionsSingle.Checked = false;
			Controller.Instance.PrintProductColorOptionsSpot.Checked = false;
			Controller.Instance.PrintProductColorOptionsFull.Checked = false;
			Controller.Instance.PrintProductColorOptionsCostPerAd.Checked = false;
			Controller.Instance.PrintProductColorOptionsCostPerAd.Enabled = false;
			Controller.Instance.PrintProductColorOptionsPercentOfAd.Checked = false;
			Controller.Instance.PrintProductColorOptionsPercentOfAd.Enabled = false;
			Controller.Instance.PrintProductColorOptionsIncluded.Checked = false;
			Controller.Instance.PrintProductColorOptionsIncluded.Enabled = false;
			Controller.Instance.PrintProductColorOptionsPCI.Checked = false;
			Controller.Instance.PrintProductCostPerInch.Value = 0;
			Controller.Instance.PrintProductCostPerInch.Enabled = false;
			#endregion

			_allowToSave = true;
		}

		private void LoadPublication()
		{
			bool temSettingsNotSaved = SettingsNotSaved;
			var publicationControl = xtraTabControlPublications.SelectedTabPage as PrintProductControl;
			if (publicationControl != null)
			{
				PrintProduct printProduct = publicationControl.PrintProduct;
				Controller.Instance.PrintProductDelete.Enabled = printProduct.Inserts.Count > 0;
				Controller.Instance.PrintProductClone.Enabled = printProduct.Inserts.Count > 0;

				Controller.Instance.PrintProductAdPricingColumnInches.Enabled = ListManager.Instance.DefaultPrintScheduleViewSettings.EnablePCI;
				Controller.Instance.PrintProductAdPricingFlat.Enabled = ListManager.Instance.DefaultPrintScheduleViewSettings.EnableFlat;
				Controller.Instance.PrintProductAdPricingPagePercent.Enabled = ListManager.Instance.ShareUnits.Count > 0 & ListManager.Instance.DefaultPrintScheduleViewSettings.EnableShare;

				Controller.Instance.PrintProductColorOptionsSingle.Enabled = ListManager.Instance.DefaultPrintScheduleViewSettings.EnableBlackWhite;
				Controller.Instance.PrintProductColorOptionsSpot.Enabled = ListManager.Instance.DefaultPrintScheduleViewSettings.EnableSpotColor;
				Controller.Instance.PrintProductColorOptionsFull.Enabled = ListManager.Instance.DefaultPrintScheduleViewSettings.EnableFullColor;

				Controller.Instance.PrintProductColorOptionsCostPerAd.Enabled = ListManager.Instance.DefaultPrintScheduleViewSettings.EnableCostPerAd;
				Controller.Instance.PrintProductColorOptionsPercentOfAd.Enabled = ListManager.Instance.DefaultPrintScheduleViewSettings.EnablePercentOfAd;
				Controller.Instance.PrintProductColorOptionsIncluded.Enabled = ListManager.Instance.DefaultPrintScheduleViewSettings.EnableColorIncluded;
				Controller.Instance.PrintProductColorOptionsPCI.Enabled = ListManager.Instance.DefaultPrintScheduleViewSettings.EnableCostPerInch;
				Controller.Instance.PrintProductCostPerInch.Enabled = ListManager.Instance.DefaultPrintScheduleViewSettings.EnableCostPerInch;

				ClearSettings();
				LoadPricingOptions(publicationControl);
				LoadSizeOptions(publicationControl);
				LoadColorOptions(publicationControl);
				SettingsNotSaved = temSettingsNotSaved;
			}
		}

		private bool SaveSchedule(string scheduleName = "")
		{
			if (!string.IsNullOrEmpty(scheduleName))
			{
				LocalSchedule.Name = scheduleName;
			}
			Controller.Instance.SaveSchedule(LocalSchedule, false, this);
			LoadSchedule(true);
			SettingsNotSaved = false;
			return true;
		}

		private void xtraTabControlPublications_MouseDown(object sender, MouseEventArgs e)
		{
			var c = sender as XtraTabControl;

			XtraTabHitInfo hi = c.CalcHitInfo(new Point(e.X, e.Y));
			if (hi.HitTest == XtraTabHitTest.PageHeader && e.Button == MouseButtons.Right)
			{
				var publicationControl = hi.Page as PrintProductControl;
				using (var form = new FormCloneSchedule())
				{
					if (form.ShowDialog() == DialogResult.Yes && publicationControl != null)
					{
						var selectedPage = xtraTabControlPublications.SelectedTabPage as PrintProductControl;
						PrintProduct newPrintProduct = publicationControl.PrintProduct.Clone();
						xtraTabControlPublications.SelectedPageChanged -= xtraTabControlPublications_SelectedPageChanged;
						;
						xtraTabControlPublications.TabPages.Clear();
						var newPublicationTab = new PrintProductControl();
						newPublicationTab.PrintProduct = newPrintProduct;
						newPublicationTab.Text = newPrintProduct.Name.Replace("&", "&&");
						newPrintProduct.RefreshAvailableDays();
						newPublicationTab.LoadInserts();
						_tabPages.Add(newPublicationTab);
						_tabPages.Sort((x, y) => x.PrintProduct.Index.CompareTo(y.PrintProduct.Index));
						xtraTabControlPublications.TabPages.AddRange(_tabPages.ToArray());
						xtraTabControlPublications.SelectedPageChanged += xtraTabControlPublications_SelectedPageChanged;
						;
						xtraTabControlPublications.SelectedTabPage = selectedPage;
						SettingsNotSaved = true;
					}
				}
			}
		}
		#endregion

		#region Form Events
		private void ScheduleBuilderControl_Load(object sender, EventArgs e)
		{
			AssignCloseActiveEditorsonOutSideClick(Controller.Instance.Ribbon);
			AssignCloseActiveEditorsonOutSideClick(pnHeader);
		}

		private void xtraTabControlPublications_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			LoadPublication();
		}
		#endregion

		#region Pricing Sections
		private void LoadPricingOptions(PrintProductControl printProductControl)
		{
			_allowToSave = false;
			PrintProduct printProduct = printProductControl.PrintProduct;
			switch (printProduct.AdPricingStrategy)
			{
				case AdPricingStrategies.StandartPCI:
					Controller.Instance.PrintProductAdPricingColumnInches.Checked = true;
					Controller.Instance.PrintProductAdPricingFlat.Checked = false;
					Controller.Instance.PrintProductAdPricingPagePercent.Checked = false;
					break;
				case AdPricingStrategies.FlatModular:
					Controller.Instance.PrintProductAdPricingColumnInches.Checked = false;
					Controller.Instance.PrintProductAdPricingFlat.Checked = true;
					Controller.Instance.PrintProductAdPricingPagePercent.Checked = false;
					break;
				case AdPricingStrategies.SharePage:
					Controller.Instance.PrintProductAdPricingColumnInches.Checked = false;
					Controller.Instance.PrintProductAdPricingFlat.Checked = false;
					Controller.Instance.PrintProductAdPricingPagePercent.Checked = true;
					break;
			}
			FormatAccordingPricingOptions(printProductControl);
			_allowToSave = true;
		}

		private void FormatAccordingPricingOptions(PrintProductControl printProductControl)
		{
			PrintProduct printProduct = printProductControl.PrintProduct;
			switch (printProduct.AdPricingStrategy)
			{
				case AdPricingStrategies.StandartPCI:
					Controller.Instance.PrintProductAdSizeStandart.Visible = true;
					Controller.Instance.PrintProductAdSizeSharePage.Visible = false;
					Controller.Instance.PrintProductAdSizeStandartSquare.Enabled = false;
					Controller.Instance.PrintProductAdSizeStandartSquare.Checked = true;
					Controller.Instance.PrintProductColorOptionsPCI.Enabled = printProductControl.PrintProduct.ColorOption != ColorOptions.BlackWhite;
					Controller.Instance.PrintProductCostPerInch.Enabled = printProductControl.PrintProduct.ColorOption != ColorOptions.BlackWhite & printProductControl.PrintProduct.ColorPricing == ColorPricingType.CostPerInch;

					printProductControl.gridBandPCIRate.Caption = "PCI";
					printProductControl.gridBandADRate.Caption = "Cost (B&W)";
					printProductControl.gridColumnPCIRate.OptionsColumn.ReadOnly = false;
					printProductControl.gridColumnPCIRate.OptionsColumn.AllowEdit = true;
					printProductControl.gridColumnPCIRate.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
					printProductControl.gridColumnPCIRate.AppearanceCell.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditPCIRateDisplay.Appearance.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditPCIRateDisplay.AppearanceDisabled.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditPCIRateDisplay.AppearanceFocused.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditPCIRateDisplay.AppearanceReadOnly.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditPCIRateDisplayFirstRow.Appearance.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditPCIRateDisplayFirstRow.AppearanceDisabled.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditPCIRateDisplayFirstRow.AppearanceFocused.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditPCIRateDisplayFirstRow.AppearanceReadOnly.ForeColor = Color.Black;
					printProductControl.gridColumnADRate.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
					printProductControl.gridColumnADRate.AppearanceCell.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditADRateDisplay.Appearance.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditADRateDisplay.AppearanceDisabled.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditADRateDisplay.AppearanceFocused.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditADRateDisplay.AppearanceReadOnly.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditADRateDisplayNull.Appearance.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditADRateDisplayNull.AppearanceDisabled.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditADRateDisplayNull.AppearanceFocused.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditADRateDisplayNull.AppearanceReadOnly.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditADRateDisplayFirstRow.Appearance.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditADRateDisplayFirstRow.AppearanceDisabled.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditADRateDisplayFirstRow.AppearanceFocused.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditADRateDisplayFirstRow.AppearanceReadOnly.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditADRateDisplayNullFirstRow.Appearance.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditADRateDisplayNullFirstRow.AppearanceDisabled.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditADRateDisplayNullFirstRow.AppearanceFocused.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditADRateDisplayNullFirstRow.AppearanceReadOnly.ForeColor = Color.Gray;
					break;
				case AdPricingStrategies.FlatModular:
					Controller.Instance.PrintProductAdSizeStandart.Visible = true;
					Controller.Instance.PrintProductAdSizeSharePage.Visible = false;
					Controller.Instance.PrintProductAdSizeStandartSquare.Enabled = true;
					Controller.Instance.PrintProductAdSizeStandartSquare.Enabled = true;
					Controller.Instance.PrintProductColorOptionsPCI.Enabled = printProduct.SizeOptions.EnableSquare & printProductControl.PrintProduct.ColorOption != ColorOptions.BlackWhite;
					Controller.Instance.PrintProductCostPerInch.Enabled = printProduct.SizeOptions.EnableSquare & printProductControl.PrintProduct.ColorOption != ColorOptions.BlackWhite & printProductControl.PrintProduct.ColorPricing == ColorPricingType.CostPerInch;

					printProductControl.gridBandPCIRate.Caption = "Package PCI";
					printProductControl.gridBandADRate.Caption = "Package Rate";
					printProductControl.gridColumnPCIRate.OptionsColumn.ReadOnly = true;
					printProductControl.gridColumnPCIRate.OptionsColumn.AllowEdit = false;
					printProductControl.gridColumnPCIRate.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
					printProductControl.gridColumnPCIRate.AppearanceCell.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditPCIRateDisplay.Appearance.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditPCIRateDisplay.AppearanceDisabled.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditPCIRateDisplay.AppearanceFocused.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditPCIRateDisplay.AppearanceReadOnly.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditPCIRateDisplayFirstRow.Appearance.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditPCIRateDisplayFirstRow.AppearanceDisabled.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditPCIRateDisplayFirstRow.AppearanceFocused.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditPCIRateDisplayFirstRow.AppearanceReadOnly.ForeColor = Color.Gray;
					printProductControl.gridColumnADRate.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
					printProductControl.gridColumnADRate.AppearanceCell.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplay.Appearance.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplay.AppearanceDisabled.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplay.AppearanceFocused.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplay.AppearanceReadOnly.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayNull.Appearance.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayNull.AppearanceDisabled.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayNull.AppearanceFocused.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayNull.AppearanceReadOnly.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayFirstRow.Appearance.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayFirstRow.AppearanceDisabled.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayFirstRow.AppearanceFocused.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayFirstRow.AppearanceReadOnly.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayNullFirstRow.Appearance.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayNullFirstRow.AppearanceDisabled.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayNullFirstRow.AppearanceFocused.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayNullFirstRow.AppearanceReadOnly.ForeColor = Color.Black;
					break;
				case AdPricingStrategies.SharePage:
					Controller.Instance.PrintProductAdSizeStandart.Visible = false;
					Controller.Instance.PrintProductAdSizeSharePage.Visible = true;
					Controller.Instance.PrintProductAdSizeStandartSquare.Enabled = false;
					Controller.Instance.PrintProductColorOptionsPCI.Enabled = false;
					Controller.Instance.PrintProductCostPerInch.Enabled = false;
					Controller.Instance.PrintProductCostPerInch.Value = 0;

					printProductControl.gridBandPCIRate.Caption = "Package PCI";
					printProductControl.gridBandADRate.Caption = "Package Rate";
					printProductControl.gridColumnPCIRate.OptionsColumn.ReadOnly = true;
					printProductControl.gridColumnPCIRate.OptionsColumn.AllowEdit = false;
					printProductControl.gridColumnPCIRate.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
					printProductControl.gridColumnPCIRate.AppearanceCell.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditPCIRateDisplay.Appearance.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditPCIRateDisplay.AppearanceDisabled.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditPCIRateDisplay.AppearanceFocused.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditPCIRateDisplay.AppearanceReadOnly.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditPCIRateDisplayFirstRow.Appearance.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditPCIRateDisplayFirstRow.AppearanceDisabled.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditPCIRateDisplayFirstRow.AppearanceFocused.ForeColor = Color.Gray;
					printProductControl.repositoryItemSpinEditPCIRateDisplayFirstRow.AppearanceReadOnly.ForeColor = Color.Gray;
					printProductControl.gridColumnADRate.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
					printProductControl.gridColumnADRate.AppearanceCell.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplay.Appearance.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplay.AppearanceDisabled.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplay.AppearanceFocused.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplay.AppearanceReadOnly.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayNull.Appearance.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayNull.AppearanceDisabled.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayNull.AppearanceFocused.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayNull.AppearanceReadOnly.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayFirstRow.Appearance.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayFirstRow.AppearanceDisabled.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayFirstRow.AppearanceFocused.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayFirstRow.AppearanceReadOnly.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayNullFirstRow.Appearance.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayNullFirstRow.AppearanceDisabled.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayNullFirstRow.AppearanceFocused.ForeColor = Color.Black;
					printProductControl.repositoryItemSpinEditADRateDisplayNullFirstRow.AppearanceReadOnly.ForeColor = Color.Black;
					break;
			}
			Controller.Instance.PrintProductAdSizeRibbonBar.RecalcLayout();
			Controller.Instance.PrintProductPanel.PerformLayout();
		}

		public void buttonItemAdPricingColumnInches_Click(object sender, EventArgs e)
		{
			var publicationControl = xtraTabControlPublications.SelectedTabPage as PrintProductControl;
			if (publicationControl != null)
			{
				PrintProduct printProduct = publicationControl.PrintProduct;
				string oldPricingStrategy = string.Empty;
				switch (printProduct.AdPricingStrategy)
				{
					case AdPricingStrategies.StandartPCI:
						oldPricingStrategy = "Column Inches";
						break;
					case AdPricingStrategies.FlatModular:
						oldPricingStrategy = "Flat Rate";
						break;
					case AdPricingStrategies.SharePage:
						oldPricingStrategy = "% Share of Page";
						break;
				}

				string newPricingStrategy = string.Empty;
				Image newPricingStrategyImage = null;
				if (sender == Controller.Instance.PrintProductAdPricingColumnInches)
				{
					newPricingStrategy = "Column Inches";
					newPricingStrategyImage = Resources.ColumnInchesBig;
				}
				else if (sender == Controller.Instance.PrintProductAdPricingFlat)
				{
					newPricingStrategy = "Flat Rate";
					newPricingStrategyImage = Resources.FlatRateBig;
				}
				else if (sender == Controller.Instance.PrintProductAdPricingPagePercent)
				{
					newPricingStrategy = "% Share of Page";
					newPricingStrategyImage = Resources.SharePageBig;
				}
				if (!newPricingStrategy.Equals(oldPricingStrategy))
				{
					using (var warningForm = new FormChangeAdStrategyWarning())
					{
						warningForm.labelControlText.Text = string.Format("You are changing the Pricing Strategy from<br><b>{0}</b> to <b>{1}</b>.<br><br>Do you want to continue?", new object[] { oldPricingStrategy, newPricingStrategy });
						warningForm.pictureBoxImage.Image = newPricingStrategyImage;
						if (warningForm.ShowDialog() == DialogResult.Yes)
						{
							using (var form = new FormChangeAdStrategy())
							{
								form.laPublication.Text = printProduct.Name;
								form.pictureBoxImage.Image = newPricingStrategyImage;
								DialogResult result = printProduct.Inserts.Count > 0 ? form.ShowDialog() : DialogResult.OK;
								if (result == DialogResult.OK)
								{
									if (printProduct.Inserts.Count > 0)
									{
										if (form.rbSave.Checked)
											foreach (Insert insert in printProduct.Inserts)
												insert.SaveRates();
										if (form.rbReset.Checked)
											foreach (Insert insert in printProduct.Inserts)
												insert.ResetRates();
										if (form.rbDelete.Checked)
											printProduct.Inserts.Clear();
										if (form.ckDeleteAllColorRates.Checked)
											foreach (Insert insert in printProduct.Inserts)
											{
												insert.ColorPricing = 0;
												insert.ColorPricingPercent = 0;
												printProduct.ColorInchRate = 0;
											}
										if (form.ckDeleteAllDiscounts.Checked)
											foreach (Insert insert in printProduct.Inserts)
											{
												insert.Discounts = 0;
											}
										if (form.ckDeleteAllAdNotes.Checked)
											foreach (Insert insert in printProduct.Inserts)
											{
												insert.CustomComment = string.Empty;
												insert.Comments.Clear();
												insert.CustomSection = string.Empty;
												insert.Sections.Clear();
												insert.Deadline = string.Empty;
												insert.Mechanicals = null;
											}
										if (form.ckDeleteAllAdNotes.Checked || form.ckDeleteAllColorRates.Checked || form.ckDeleteAllDiscounts.Checked)
											publicationControl.LoadInserts();
									}
									_allowToSave = false;
									Controller.Instance.PrintProductAdPricingColumnInches.Checked = false;
									Controller.Instance.PrintProductAdPricingFlat.Checked = false;
									Controller.Instance.PrintProductAdPricingPagePercent.Checked = false;
									_allowToSave = true;
									(sender as ButtonItem).Checked = true;
								}
							}
						}
					}
				}
			}
		}

		public void buttonItemAdPricing_CheckedChanged(object sender, EventArgs e)
		{
			var publicationControl = xtraTabControlPublications.SelectedTabPage as PrintProductControl;
			if (publicationControl != null && _allowToSave)
			{
				PrintProduct printProduct = publicationControl.PrintProduct;
				if (Controller.Instance.PrintProductAdPricingColumnInches.Checked)
				{
					if (printProduct.AdPricingStrategy == AdPricingStrategies.SharePage)
						printProduct.SizeOptions.ResetToDefaults(AdPricingStrategies.StandartPCI);
					printProduct.AdPricingStrategy = AdPricingStrategies.StandartPCI;
				}
				else if (Controller.Instance.PrintProductAdPricingFlat.Checked || Controller.Instance.PrintProductAdPricingPagePercent.Checked)
				{
					if (Controller.Instance.PrintProductAdPricingFlat.Checked)
					{
						AdPricingStrategies prevStrategy = printProduct.AdPricingStrategy;
						printProduct.AdPricingStrategy = AdPricingStrategies.FlatModular;
						if (prevStrategy == AdPricingStrategies.SharePage)
						{
							printProduct.SizeOptions.ResetToDefaults(AdPricingStrategies.FlatModular);
						}
					}
					else if (Controller.Instance.PrintProductAdPricingPagePercent.Checked)
					{
						ColorPricingType prevColorPricing = printProduct.ColorPricing;
						if (printProduct.AdPricingStrategy != AdPricingStrategies.SharePage)
							printProduct.SizeOptions.ResetToDefaults(AdPricingStrategies.SharePage);
						printProduct.AdPricingStrategy = AdPricingStrategies.SharePage;
						if (prevColorPricing == ColorPricingType.CostPerInch)
						{
							switch (ListManager.Instance.DefaultColorPricing)
							{
								case ColorPricingType.CostPerAd:
									buttonItemColorOptions_Click(Controller.Instance.PrintProductColorOptionsCostPerAd, null);
									break;
								case ColorPricingType.PercentOfAdRate:
									buttonItemColorOptions_Click(Controller.Instance.PrintProductColorOptionsPercentOfAd, null);
									break;
								case ColorPricingType.ColorIncluded:
									buttonItemColorOptions_Click(Controller.Instance.PrintProductColorOptionsIncluded, null);
									break;
								case ColorPricingType.CostPerInch:
									buttonItemColorOptions_Click(Controller.Instance.PrintProductColorOptionsPCI, null);
									break;
							}
						}
					}
				}
				FormatAccordingPricingOptions(publicationControl);
				LoadSizeOptions(publicationControl);
				LoadColorOptions(publicationControl);
				publicationControl.LoadInserts();
				publicationControl.UpdateTotals();
				SettingsNotSaved = true;
			}
		}
		#endregion

		#region Size Section
		private void LoadSizeOptions(PrintProductControl printProductControl)
		{
			_allowToSave = false;
			SizeOptions sizeOptions = printProductControl.PrintProduct.SizeOptions;
			Controller.Instance.PrintProductStandartPageSizeCombo.Properties.Items.Clear();
			Controller.Instance.PrintProductStandartPageSizeCombo.Properties.Items.AddRange(ListManager.Instance.PageSizes.ToArray());
			Controller.Instance.PrintProductSharePagePageSizeCombo.Properties.Items.Clear();
			Controller.Instance.PrintProductSharePagePageSizeCombo.Properties.Items.AddRange(ListManager.Instance.PageSizes.ToArray());
			Controller.Instance.PrintProductRateCard.Properties.Items.Clear();
			Controller.Instance.PrintProductRateCard.Properties.Items.AddRange(ListManager.Instance.ShareUnits.Select(x => x.RateCard).Distinct().ToArray());
			Controller.Instance.PrintProductStandartPageSizeCheck.Checked = sizeOptions.EnablePageSize;
			Controller.Instance.PrintProductStandartPageSizeCombo.EditValue = sizeOptions.PageSize;
			Controller.Instance.PrintProductSharePagePageSizeCheck.Checked = sizeOptions.EnablePageSize;
			Controller.Instance.PrintProductSharePagePageSizeCombo.EditValue = sizeOptions.PageSize;
			switch (printProductControl.PrintProduct.AdPricingStrategy)
			{
				case AdPricingStrategies.StandartPCI:
				case AdPricingStrategies.FlatModular:
					Controller.Instance.PrintProductAdSizeStandartSquare.Checked = sizeOptions.EnableSquare;
					Controller.Instance.PrintProductStandartWidth.Value = (decimal)sizeOptions.Width;
					Controller.Instance.PrintProductStandartHeight.Value = (decimal)sizeOptions.Height;
					break;
				case AdPricingStrategies.SharePage:
					Controller.Instance.PrintProductRateCard.EditValue = sizeOptions.RateCard;
					Controller.Instance.PrintProductPercentOfPage.Properties.Items.Clear();
					if (!string.IsNullOrEmpty(sizeOptions.RateCard))
						Controller.Instance.PrintProductPercentOfPage.Properties.Items.AddRange(ListManager.Instance.ShareUnits.Where(x => x.RateCard.Equals(sizeOptions.RateCard)).Select(x => x.PercentOfPage).Distinct().ToArray());
					Controller.Instance.PrintProductPercentOfPage.EditValue = sizeOptions.PercentOfPage;
					Controller.Instance.PrintProductSharePageSquare.Items.Clear();
					var shareUnits = new ShareUnit[] { };
					if (Controller.Instance.PrintProductPercentOfPage.EditValue != null && Controller.Instance.PrintProductRateCard.EditValue != null)
						shareUnits = ListManager.Instance.ShareUnits.Where(x => x.RateCard.Equals(Controller.Instance.PrintProductRateCard.EditValue.ToString()) && x.PercentOfPage.Equals(Controller.Instance.PrintProductPercentOfPage.EditValue.ToString())).ToArray();
					if (shareUnits.Length > 0)
					{
						ShareUnit storedShareUnit = printProductControl.PrintProduct.SizeOptions.RelatedShareUnit;
						foreach (ShareUnit shareUnit in shareUnits)
							Controller.Instance.PrintProductSharePageSquare.Items.Add(shareUnit, shareUnit.Dimensions, shareUnit.Dimensions.Equals(storedShareUnit.Dimensions) ? CheckState.Checked : CheckState.Unchecked, true);
						if (Controller.Instance.PrintProductSharePageSquare.CheckedIndices.Count == 0)
							Controller.Instance.PrintProductSharePageSquare.Items[0].CheckState = CheckState.Checked;
					}
					break;
			}
			FormatAccordingSizeOptions(printProductControl);
			_allowToSave = true;
		}

		private void SetSizeOptions(PrintProductControl printProductControl)
		{
			SizeOptions sizeOptions = printProductControl.PrintProduct.SizeOptions;
			switch (printProductControl.PrintProduct.AdPricingStrategy)
			{
				case AdPricingStrategies.StandartPCI:
				case AdPricingStrategies.FlatModular:
					sizeOptions.ResetToDefaults(AdPricingStrategies.StandartPCI);
					_allowToSave = false;
					Controller.Instance.PrintProductRateCard.EditValue = null;
					Controller.Instance.PrintProductPercentOfPage.EditValue = null;
					Controller.Instance.PrintProductSharePageSquare.Items.Clear();
					_allowToSave = true;

					sizeOptions.EnableSquare = Controller.Instance.PrintProductAdSizeStandartSquare.Checked;
					sizeOptions.Width = sizeOptions.EnableSquare ? (double)Controller.Instance.PrintProductStandartWidth.Value : 0;
					sizeOptions.Height = sizeOptions.EnableSquare ? (double)Controller.Instance.PrintProductStandartHeight.Value : 0;
					sizeOptions.EnablePageSize = Controller.Instance.PrintProductStandartPageSizeCheck.Checked;
					sizeOptions.PageSize = sizeOptions.EnablePageSize && !string.IsNullOrEmpty((string)Controller.Instance.PrintProductStandartPageSizeCombo.EditValue) ? Controller.Instance.PrintProductStandartPageSizeCombo.EditValue.ToString() : null;
					break;
				case AdPricingStrategies.SharePage:
					sizeOptions.ResetToDefaults(AdPricingStrategies.SharePage);
					_allowToSave = false;
					Controller.Instance.PrintProductAdSizeStandartSquare.Checked = false;
					Controller.Instance.PrintProductStandartHeight.Value = 0;
					Controller.Instance.PrintProductStandartWidth.Value = 0;
					_allowToSave = true;
					sizeOptions.RateCard = !string.IsNullOrEmpty((string)Controller.Instance.PrintProductRateCard.EditValue) ? Controller.Instance.PrintProductRateCard.EditValue.ToString() : null;
					sizeOptions.PercentOfPage = !string.IsNullOrEmpty((string)Controller.Instance.PrintProductPercentOfPage.EditValue) ? Controller.Instance.PrintProductPercentOfPage.EditValue.ToString() : null;
					ShareUnit shareUnit = null;
					foreach (CheckedListBoxItem item in Controller.Instance.PrintProductSharePageSquare.Items)
						if (item.CheckState == CheckState.Checked)
						{
							shareUnit = item.Value as ShareUnit;
							break;
						}
					sizeOptions.Height = shareUnit != null ? shareUnit.HeightValue : 0;
					sizeOptions.HeightMeasure = shareUnit != null ? shareUnit.HeightMeasureUnit : sizeOptions.HeightMeasure;
					sizeOptions.Width = shareUnit != null ? shareUnit.WidthValue : 0;
					sizeOptions.WidthMeasure = shareUnit != null ? shareUnit.WidthMeasureUnit : sizeOptions.WidthMeasure;
					sizeOptions.EnablePageSize = Controller.Instance.PrintProductSharePagePageSizeCheck.Checked;
					sizeOptions.EnableSquare = false;
					sizeOptions.PageSize = sizeOptions.EnablePageSize && !string.IsNullOrEmpty((string)Controller.Instance.PrintProductSharePagePageSizeCombo.EditValue) ? Controller.Instance.PrintProductSharePagePageSizeCombo.EditValue.ToString() : null;
					break;
			}
			FormatAccordingSizeOptions(printProductControl);
			printProductControl.LoadInserts();
			printProductControl.UpdateTotals();
		}

		private void FormatAccordingSizeOptions(PrintProductControl printProductControl)
		{
			SizeOptions sizeOptions = printProductControl.PrintProduct.SizeOptions;
			Controller.Instance.PrintProductStandartHeight.Enabled = sizeOptions.EnableSquare;
			Controller.Instance.PrintProductStandartWidth.Enabled = sizeOptions.EnableSquare;
			Controller.Instance.PrintProductStandartEqualSignContainer.Visible = sizeOptions.Square.HasValue;
			Controller.Instance.PrintProductStandartSquareMetricContainer.Visible = sizeOptions.Square.HasValue;
			Controller.Instance.PrintProductStandartSquareValueContainer.Visible = sizeOptions.Square.HasValue;
			Controller.Instance.PrintProductStandartSquareValue.Text = sizeOptions.Square.HasValue ? sizeOptions.Square.Value.ToString("#,##0.00#") : string.Empty;
			Controller.Instance.PrintProductStandartPageSizeCombo.Enabled = sizeOptions.EnablePageSize;
			Controller.Instance.PrintProductSharePagePageSizeCombo.Enabled = sizeOptions.EnablePageSize;
			Controller.Instance.PrintProductColorOptionsPCI.Enabled = sizeOptions.EnableSquare & printProductControl.PrintProduct.AdPricingStrategy != AdPricingStrategies.SharePage & printProductControl.PrintProduct.ColorOption != ColorOptions.BlackWhite;
			Controller.Instance.PrintProductCostPerInch.Enabled = sizeOptions.EnableSquare & printProductControl.PrintProduct.AdPricingStrategy != AdPricingStrategies.SharePage & printProductControl.PrintProduct.ColorOption != ColorOptions.BlackWhite & printProductControl.PrintProduct.ColorPricing == ColorPricingType.CostPerInch;
			Controller.Instance.PrintProductPercentOfPage.Enabled = !string.IsNullOrEmpty(sizeOptions.RateCard);
			Controller.Instance.PrintProductAdSizeSharePagePercentOfPageLabel.ForeColor = !string.IsNullOrEmpty(sizeOptions.RateCard) ? Color.Black : Color.Gray;
			Controller.Instance.PrintProductSharePageSquare.Enabled = Controller.Instance.PrintProductSharePageSquare.ItemCount > 0;
			Controller.Instance.PrintProductSharePageSquare.BackColor = Controller.Instance.PrintProductSharePageSquare.ItemCount > 0 ? Color.White : Color.FromArgb(197, 214, 232);
			Controller.Instance.PrintProductAdSizeSharePageDimensionsLabel.ForeColor = Controller.Instance.PrintProductSharePageSquare.ItemCount > 0 ? Color.Black : Color.Gray;
			Controller.Instance.PrintProductAdSizeRibbonBar.RecalcLayout();
			Controller.Instance.PrintProductPanel.PerformLayout();
		}

		public void checkBoxItemAdSizeStandartSquare_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
		{
			var publicationControl = xtraTabControlPublications.SelectedTabPage as PrintProductControl;
			if (publicationControl != null && _allowToSave)
			{
				if (!Controller.Instance.PrintProductAdSizeStandartSquare.Checked)
				{
					ColorPricingType prevColorPricing = publicationControl.PrintProduct.ColorPricing;
					if (prevColorPricing == ColorPricingType.CostPerInch)
					{
						switch (ListManager.Instance.DefaultColorPricing)
						{
							case ColorPricingType.CostPerAd:
								buttonItemColorOptions_Click(Controller.Instance.PrintProductColorOptionsCostPerAd, null);
								break;
							case ColorPricingType.PercentOfAdRate:
								buttonItemColorOptions_Click(Controller.Instance.PrintProductColorOptionsPercentOfAd, null);
								break;
							case ColorPricingType.ColorIncluded:
								buttonItemColorOptions_Click(Controller.Instance.PrintProductColorOptionsIncluded, null);
								break;
							case ColorPricingType.CostPerInch:
								buttonItemColorOptions_Click(Controller.Instance.PrintProductColorOptionsPCI, null);
								break;
						}
					}
				}
				SetSizeOptions(publicationControl);
				SettingsNotSaved = true;
			}
		}

		public void checkBoxItemSizeOptions_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
		{
			var publicationControl = xtraTabControlPublications.SelectedTabPage as PrintProductControl;
			if (publicationControl != null && _allowToSave)
			{
				SetSizeOptions(publicationControl);
				SettingsNotSaved = true;
			}
		}

		public void spinEditStandart_EditValueChanged(object sender, EventArgs e)
		{
			var publicationControl = xtraTabControlPublications.SelectedTabPage as PrintProductControl;
			if (publicationControl != null && _allowToSave)
			{
				SetSizeOptions(publicationControl);
				SettingsNotSaved = true;
			}
		}

		public void comboBoxEditSizeOptions_EditValueChanged(object sender, EventArgs e)
		{
			var publicationControl = xtraTabControlPublications.SelectedTabPage as PrintProductControl;
			if (publicationControl != null && _allowToSave)
			{
				SetSizeOptions(publicationControl);
				SettingsNotSaved = true;
			}
		}

		public void comboBoxEditRateCard_EditValueChanged(object sender, EventArgs e)
		{
			var publicationControl = xtraTabControlPublications.SelectedTabPage as PrintProductControl;
			if (publicationControl != null && _allowToSave)
			{
				_allowToSave = false;
				Controller.Instance.PrintProductPercentOfPage.EditValue = null;
				Controller.Instance.PrintProductPercentOfPage.Properties.Items.Clear();
				if (Controller.Instance.PrintProductRateCard.EditValue != null)
					Controller.Instance.PrintProductPercentOfPage.Properties.Items.AddRange(ListManager.Instance.ShareUnits.Where(x => x.RateCard.Equals(Controller.Instance.PrintProductRateCard.EditValue.ToString())).Select(x => x.PercentOfPage).Distinct().ToArray());
				_allowToSave = true;
				SetSizeOptions(publicationControl);
				SettingsNotSaved = true;
			}
		}

		public void comboBoxEditPercentOfPage_EditValueChanged(object sender, EventArgs e)
		{
			var publicationControl = xtraTabControlPublications.SelectedTabPage as PrintProductControl;
			if (publicationControl != null && _allowToSave)
			{
				_allowToSave = false;
				Controller.Instance.PrintProductSharePageSquare.Items.Clear();
				var shareUnits = new ShareUnit[] { };
				if (Controller.Instance.PrintProductPercentOfPage.EditValue != null && Controller.Instance.PrintProductRateCard.EditValue != null)
					shareUnits = ListManager.Instance.ShareUnits.Where(x => x.RateCard.Equals(Controller.Instance.PrintProductRateCard.EditValue.ToString()) && x.PercentOfPage.Equals(Controller.Instance.PrintProductPercentOfPage.EditValue.ToString())).ToArray();
				if (shareUnits.Length > 0)
				{
					ShareUnit storedShareUnit = publicationControl.PrintProduct.SizeOptions.RelatedShareUnit;
					foreach (ShareUnit shareUnit in shareUnits)
						Controller.Instance.PrintProductSharePageSquare.Items.Add(shareUnit, shareUnit.Dimensions, shareUnit.Dimensions.Equals(storedShareUnit.Dimensions) ? CheckState.Checked : CheckState.Unchecked, true);
					if (Controller.Instance.PrintProductSharePageSquare.CheckedIndices.Count == 0)
						Controller.Instance.PrintProductSharePageSquare.Items[0].CheckState = CheckState.Checked;
				}
				_allowToSave = true;
				SetSizeOptions(publicationControl);
				SettingsNotSaved = true;
			}
		}

		public void checkedListBoxControlSharePageSquare_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			var publicationControl = xtraTabControlPublications.SelectedTabPage as PrintProductControl;
			if (publicationControl != null && _allowToSave)
			{
				_allowToSave = false;
				foreach (CheckedListBoxItem item in Controller.Instance.PrintProductSharePageSquare.Items)
					if (item.Description != Controller.Instance.PrintProductSharePageSquare.Items[e.Index].Description)
						item.CheckState = CheckState.Unchecked;
				_allowToSave = true;
				SetSizeOptions(publicationControl);
				SettingsNotSaved = true;
			}
		}
		#endregion

		#region Color Section
		private void LoadColorOptions(PrintProductControl printProductControl)
		{
			_allowToSave = false;
			PrintProduct printProduct = printProductControl.PrintProduct;
			switch (printProduct.ColorOption)
			{
				case ColorOptions.BlackWhite:
					Controller.Instance.PrintProductColorOptionsSingle.Checked = true;
					break;
				case ColorOptions.SpotColor:
				case ColorOptions.FullColor:

					if (printProduct.ColorOption == ColorOptions.SpotColor)
						Controller.Instance.PrintProductColorOptionsSpot.Checked = true;
					else if (printProduct.ColorOption == ColorOptions.FullColor)
						Controller.Instance.PrintProductColorOptionsFull.Checked = true;
					switch (printProduct.ColorPricing)
					{
						case ColorPricingType.CostPerAd:
							Controller.Instance.PrintProductColorOptionsCostPerAd.Checked = true;
							break;
						case ColorPricingType.PercentOfAdRate:
							Controller.Instance.PrintProductColorOptionsPercentOfAd.Checked = true;
							break;
						case ColorPricingType.ColorIncluded:
							Controller.Instance.PrintProductColorOptionsIncluded.Checked = true;
							break;
						case ColorPricingType.CostPerInch:
							Controller.Instance.PrintProductColorOptionsPCI.Checked = true;
							Controller.Instance.PrintProductCostPerInch.Value = (decimal)printProduct.ColorInchRate;
							break;
					}
					break;
			}
			FormatAccordingColorOptions(printProductControl);
			_allowToSave = true;
		}

		private void FormatAccordingColorOptions(PrintProductControl printProductControl)
		{
			PrintProduct printProduct = printProductControl.PrintProduct;
			switch (printProduct.ColorOption)
			{
				case ColorOptions.BlackWhite:
					Controller.Instance.PrintProductColorOptionsCostPerAd.Enabled = false;
					Controller.Instance.PrintProductColorOptionsPercentOfAd.Enabled = false;
					Controller.Instance.PrintProductColorOptionsIncluded.Enabled = false;
					Controller.Instance.PrintProductColorOptionsPCI.Enabled = false;
					Controller.Instance.PrintProductCostPerInch.Enabled = false;
					Controller.Instance.PrintProductCostPerInch.Value = 0;

					printProductControl.gridBandColorPricing.Caption = @"Black & White";
					printProductControl.gridColumnColorPricing.OptionsColumn.ReadOnly = true;
					printProductControl.gridColumnColorPricing.OptionsColumn.AllowEdit = false;
					printProductControl.gridColumnColorPricingPercent.Visible = false;
					printProductControl.gridColumnColorPricing.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
					printProductControl.repositoryItemSpinEditColorPricingDisplay.NullText = "B-W";
					printProductControl.repositoryItemSpinEditColorPricingDisplayFirstRow.NullText = "B-W";
					printProductControl.repositoryItemSpinEditColorPricingDisplayFirstRow.Buttons[1].Visible = false;
					break;
				case ColorOptions.SpotColor:
				case ColorOptions.FullColor:
					Controller.Instance.PrintProductColorOptionsCostPerAd.Enabled = true;
					Controller.Instance.PrintProductColorOptionsPercentOfAd.Enabled = true;
					Controller.Instance.PrintProductColorOptionsIncluded.Enabled = true;
					Controller.Instance.PrintProductColorOptionsPCI.Enabled = printProduct.SizeOptions.EnableSquare & printProduct.AdPricingStrategy != AdPricingStrategies.SharePage;
					printProductControl.repositoryItemSpinEditColorPricingDisplay.NullText = "Included";
					printProductControl.repositoryItemSpinEditColorPricingDisplayFirstRow.NullText = "Included";

					if (printProduct.ColorOption == ColorOptions.SpotColor)
					{
						printProductControl.gridBandColorPricing.Caption = "Spot Color";
					}
					else if (printProduct.ColorOption == ColorOptions.FullColor)
					{
						printProductControl.gridBandColorPricing.Caption = "Full Color";
					}
					switch (printProduct.ColorPricing)
					{
						case ColorPricingType.CostPerAd:
							Controller.Instance.PrintProductCostPerInch.Enabled = false;
							Controller.Instance.PrintProductCostPerInch.Value = 0;

							printProductControl.gridColumnColorPricing.OptionsColumn.AllowEdit = true;
							printProductControl.gridColumnColorPricing.OptionsColumn.ReadOnly = false;
							printProductControl.gridColumnColorPricingPercent.Visible = false;
							printProductControl.gridColumnColorPricing.ColumnEdit = printProductControl.repositoryItemSpinEditColorPricingDisplay;
							printProductControl.repositoryItemSpinEditColorPricingDisplayFirstRow.Buttons[1].Visible = true;
							printProductControl.gridColumnColorPricing.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;

							break;
						case ColorPricingType.PercentOfAdRate:
							Controller.Instance.PrintProductCostPerInch.Enabled = false;
							Controller.Instance.PrintProductCostPerInch.Value = 0;

							printProductControl.gridColumnColorPricing.OptionsColumn.AllowEdit = false;
							printProductControl.gridColumnColorPricing.OptionsColumn.ReadOnly = true;
							printProductControl.gridColumnColorPricingPercent.Visible = true;
							printProductControl.gridColumnColorPricing.ColumnEdit = printProductControl.repositoryItemSpinEditDiscountRate;
							printProductControl.repositoryItemSpinEditColorPricingDisplayFirstRow.Buttons[1].Visible = false;
							printProductControl.gridColumnColorPricing.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
							break;
						case ColorPricingType.ColorIncluded:
							Controller.Instance.PrintProductCostPerInch.Enabled = false;
							Controller.Instance.PrintProductCostPerInch.Value = 0;

							printProductControl.gridColumnColorPricing.OptionsColumn.AllowEdit = false;
							printProductControl.gridColumnColorPricing.OptionsColumn.ReadOnly = true;
							printProductControl.gridColumnColorPricingPercent.Visible = false;
							printProductControl.gridColumnColorPricing.ColumnEdit = printProductControl.repositoryItemSpinEditColorPricingDisplay;
							printProductControl.repositoryItemSpinEditColorPricingDisplayFirstRow.Buttons[1].Visible = false;
							printProductControl.gridColumnColorPricing.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
							break;
						case ColorPricingType.CostPerInch:
							Controller.Instance.PrintProductCostPerInch.Enabled = printProduct.SizeOptions.EnableSquare & printProduct.AdPricingStrategy != AdPricingStrategies.SharePage;

							printProductControl.gridColumnColorPricing.OptionsColumn.AllowEdit = false;
							printProductControl.gridColumnColorPricing.OptionsColumn.ReadOnly = true;
							printProductControl.gridColumnColorPricingPercent.Visible = false;
							printProductControl.gridColumnColorPricing.ColumnEdit = printProductControl.repositoryItemSpinEditColorPricingDisplay;
							printProductControl.repositoryItemSpinEditColorPricingDisplayFirstRow.Buttons[1].Visible = false;
							printProductControl.gridColumnColorPricing.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
							break;
					}
					break;
			}
		}

		public void ColorOptions_Click(object sender, EventArgs e)
		{
			var button = (ButtonItem)sender;
			_allowToSave = false;
			Controller.Instance.PrintProductColorOptionsSingle.Checked = false;
			Controller.Instance.PrintProductColorOptionsSpot.Checked = false;
			Controller.Instance.PrintProductColorOptionsFull.Checked = false;
			_allowToSave = true;
			button.Checked = true;
		}

		public void ColorOptions_CheckedChanged(object sender, EventArgs e)
		{
			var publicationControl = xtraTabControlPublications.SelectedTabPage as PrintProductControl;
			if (publicationControl != null && _allowToSave)
			{
				PrintProduct printProduct = publicationControl.PrintProduct;
				ColorOptions prevColorOption = printProduct.ColorOption;
				if (Controller.Instance.PrintProductColorOptionsSingle.Checked)
				{
					printProduct.ColorOption = ColorOptions.BlackWhite;
				}
				else
				{
					if (Controller.Instance.PrintProductColorOptionsSpot.Checked)
						printProduct.ColorOption = ColorOptions.SpotColor;
					else if (Controller.Instance.PrintProductColorOptionsFull.Checked)
						printProduct.ColorOption = ColorOptions.FullColor;

					if (prevColorOption == ColorOptions.BlackWhite)
					{
						switch (ListManager.Instance.DefaultColorPricing)
						{
							case ColorPricingType.CostPerAd:
								buttonItemColorOptions_Click(Controller.Instance.PrintProductColorOptionsCostPerAd, null);
								break;
							case ColorPricingType.PercentOfAdRate:
								buttonItemColorOptions_Click(Controller.Instance.PrintProductColorOptionsPercentOfAd, null);
								break;
							case ColorPricingType.ColorIncluded:
								buttonItemColorOptions_Click(Controller.Instance.PrintProductColorOptionsIncluded, null);
								break;
							case ColorPricingType.CostPerInch:
								buttonItemColorOptions_Click(Controller.Instance.PrintProductColorOptionsPCI, null);
								break;
						}
					}
				}
				_allowToSave = false;
				FormatAccordingColorOptions(publicationControl);
				_allowToSave = true;
				publicationControl.LoadInserts();
				publicationControl.UpdateTotals();
				SettingsNotSaved = true;
			}
		}

		public void buttonItemColorOptions_Click(object sender, EventArgs e)
		{
			_allowToSave = false;
			Controller.Instance.PrintProductColorOptionsCostPerAd.Checked = false;
			Controller.Instance.PrintProductColorOptionsPercentOfAd.Checked = false;
			Controller.Instance.PrintProductColorOptionsIncluded.Checked = false;
			Controller.Instance.PrintProductColorOptionsPCI.Checked = false;
			_allowToSave = true;
			(sender as ButtonItem).Checked = true;
		}

		public void buttonItemColorOptions_CheckedChanged(object sender, EventArgs e)
		{
			var publicationControl = xtraTabControlPublications.SelectedTabPage as PrintProductControl;
			if (publicationControl != null && _allowToSave)
			{
				PrintProduct printProduct = publicationControl.PrintProduct;
				Controller.Instance.PrintProductCostPerInch.Enabled = Controller.Instance.PrintProductColorOptionsPCI.Checked;
				if (Controller.Instance.PrintProductColorOptionsPCI.Checked)
				{
					printProduct.ColorPricing = ColorPricingType.CostPerInch;
				}
				else if (Controller.Instance.PrintProductColorOptionsCostPerAd.Checked)
				{
					printProduct.ColorPricing = ColorPricingType.CostPerAd;
					printProduct.ColorInchRate = 0;
				}
				else if (Controller.Instance.PrintProductColorOptionsPercentOfAd.Checked)
				{
					printProduct.ColorPricing = ColorPricingType.PercentOfAdRate;
					printProduct.ColorInchRate = 0;
				}
				else if (Controller.Instance.PrintProductColorOptionsIncluded.Checked)
				{
					printProduct.ColorPricing = ColorPricingType.ColorIncluded;
					printProduct.ColorInchRate = 0;
				}
				_allowToSave = false;
				FormatAccordingColorOptions(publicationControl);
				_allowToSave = true;
				publicationControl.LoadInserts();
				publicationControl.UpdateTotals();
				SettingsNotSaved = true;
			}
		}

		public void spinEditCostPerInch_EditValueChanged(object sender, EventArgs e)
		{
			var publicationControl = xtraTabControlPublications.SelectedTabPage as PrintProductControl;
			if (publicationControl != null && _allowToSave)
			{
				PrintProduct printProduct = publicationControl.PrintProduct;
				printProduct.ColorInchRate = (double)Controller.Instance.PrintProductCostPerInch.Value;
				publicationControl.LoadInserts();
				publicationControl.UpdateTotals();
				SettingsNotSaved = true;
			}
		}
		#endregion

		#region Buttons Clicks
		public void buttonItemAddInsert_Click(object sender, EventArgs e)
		{
			if (xtraTabControlPublications.SelectedTabPageIndex >= 0)
			{
				((PrintProductControl)xtraTabControlPublications.TabPages[xtraTabControlPublications.SelectedTabPageIndex]).AddInsert();
				Controller.Instance.UpdateOutputTabs(LocalSchedule.PrintProducts.Select(x => x.Inserts.Count).Sum() > 0);
				SettingsNotSaved = true;
			}
		}

		public void buttonItemDeleteInsert_Click(object sender, EventArgs e)
		{
			if (xtraTabControlPublications.SelectedTabPageIndex >= 0)
			{
				((PrintProductControl)xtraTabControlPublications.TabPages[xtraTabControlPublications.SelectedTabPageIndex]).DeleteInsert();
				Controller.Instance.UpdateOutputTabs(LocalSchedule.PrintProducts.Select(x => x.Inserts.Count).Sum() > 0);
				SettingsNotSaved = true;
			}
		}

		public void buttonItemCloneInsert_Click(object sender, EventArgs e)
		{
			if (xtraTabControlPublications.SelectedTabPageIndex >= 0)
			{
				((PrintProductControl)xtraTabControlPublications.TabPages[xtraTabControlPublications.SelectedTabPageIndex]).CloneInsert();
				Controller.Instance.UpdateOutputTabs(LocalSchedule.PrintProducts.Select(x => x.Inserts.Count).Sum() > 0);
				SettingsNotSaved = true;
			}
		}

		public void buttonItemPrintScheduleHelp_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("schedules");
		}

		public void buttonItemPrintScheduleSave_Click(object sender, EventArgs e)
		{
			if (xtraTabControlPublications.SelectedTabPageIndex >= 0)
			{
				SaveSchedule();
				Utilities.Instance.ShowInformation("Schedule Saved");
			}
		}

		public void buttonItemPrintScheduleSaveAs_Click(object sender, EventArgs e)
		{
			using (var from = new FormNewSchedule())
			{
				from.Text = "Save Schedule";
				from.laLogo.Text = "Please set a new name for your Schedule:";
				if (from.ShowDialog() == DialogResult.OK)
				{
					if (!string.IsNullOrEmpty(from.ScheduleName))
					{
						if (SaveSchedule(from.ScheduleName))
							Utilities.Instance.ShowInformation("Schedule was saved");
					}
					else
					{
						Utilities.Instance.ShowWarning("Scheduke Name can't be empty");
					}
				}
			}
		}
		#endregion
	}
}