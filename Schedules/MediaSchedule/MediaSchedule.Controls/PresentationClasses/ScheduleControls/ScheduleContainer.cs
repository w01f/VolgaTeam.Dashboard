using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Business.Media.Entities.Persistent;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Output;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.ContentEditors.Controls;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.RetractableBar;
using Asa.Common.GUI.Themes;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls.InteropClasses;
using Asa.Media.Controls.Properties;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Asa.Media.Controls.BusinessClasses;
using DevExpress.XtraTab.ViewInfo;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls
{
	[ToolboxItem(false)]
	public partial class ScheduleContainer : BasePartitionEditControl<ProgramScheduleContent, MediaSchedule, MediaScheduleSettings, MediaScheduleChangeInfo>
	//public partial class ScheduleContainer : UserControl
	{
		private bool _allowToSave;
		private XtraTabHitInfo _menuHitInfo;
		private XtraTabDragDropHelper<SectionControl> _tabDragDropHelper;

		#region Properties
		private MediaSchedule Schedule
		{
			get { return BusinessObjects.Instance.ScheduleManager.ActiveSchedule; }
		}

		private MediaScheduleSettings ScheduleSettings
		{
			get { return Schedule.Settings; }
		}

		public override string Identifier
		{
			get { return ContentIdentifiers.ProgramSchedule; }
		}

		public override RibbonTabItem TabPage
		{
			get { return Controller.Instance.TabProgramSchedule; }
		}

		private SectionControl ActiveSection
		{
			get { return xtraTabControlSections.SelectedTabPage as SectionControl; }
		}

		private string SpotTitle
		{
			get { return ScheduleSettings.SelectedSpotType.ToString(); }
		}

		public SlideType SlideType
		{
			get
			{
				return MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ?
					SlideType.TVProgramSchedule :
					SlideType.RadioProgramSchedule;
			}
		}
		#endregion

		public ScheduleContainer()
		{
			InitializeComponent();
		}

		#region BasePartitionEditControl Override
		public override void InitControl()
		{
			base.InitControl();
			pnSections.Dock = DockStyle.Fill;
			pnNoSections.Dock = DockStyle.Fill;

			if ((CreateGraphics()).DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				font = new Font(laTotalPeriodsTitle.Font.FontFamily, laTotalPeriodsTitle.Font.Size - 2,
					laTotalPeriodsTitle.Font.Style);
				laTotalPeriodsTitle.Font = font;
				laTotalGRPTitle.Font = font;
				laTotalCPPTitle.Font = font;
				laAvgRateTitle.Font = font;
				laTotalCostTitle.Font = font;
				laNetRateTitle.Font = font;
				laAgencyDiscountTitle.Font = font;
				font = new Font(laTotalPeriodsValue.Font.FontFamily, laTotalPeriodsValue.Font.Size - 2,
					laTotalPeriodsValue.Font.Style);
				laTotalPeriodsValue.Font = font;
				laTotalGRPValue.Font = font;
				laTotalCPPValue.Font = font;
				laAvgRateValue.Font = font;
				laTotalCostValue.Font = font;
				laNetRateValue.Font = font;
				laAgencyDiscountValue.Font = font;

				laColorsTitle.Font = new Font(laColorsTitle.Font.FontFamily, laColorsTitle.Font.Size - 2, laColorsTitle.Font.Style);
				labelControlScheduleInfo.Font = new Font(labelControlScheduleInfo.Font.FontFamily,
					labelControlScheduleInfo.Font.Size - 2, labelControlScheduleInfo.Font.Style);
				labelControlFlexFlightDatesWarning.Font = new Font(labelControlFlexFlightDatesWarning.Font.FontFamily,
					labelControlFlexFlightDatesWarning.Font.Size - 2, labelControlFlexFlightDatesWarning.Font.Style);
				labelControlGenericSpotColumNamesWarning.Font = new Font(labelControlGenericSpotColumNamesWarning.Font.FontFamily,
					labelControlGenericSpotColumNamesWarning.Font.Size - 2, labelControlGenericSpotColumNamesWarning.Font.Style);

				font = new Font(buttonXStation.Font.FontFamily, buttonXStation.Font.Size - 2, buttonXStation.Font.Style);
				buttonXAvgRate.Font = font;
				buttonXStation.Font = font;
				buttonXCPP.Font = font;
				buttonXCost.Font = font;
				buttonXDay.Font = font;
				buttonXDaypart.Font = font;
				buttonXDiscount.Font = font;
				buttonXGRP.Font = font;
				buttonXLength.Font = font;
				buttonXLogo.Font = font;
				buttonXNetRate.Font = font;
				buttonXProgram.Font = font;
				buttonXRate.Font = font;
				buttonXRating.Font = font;
				buttonXSpots.Font = font;
				buttonXTime.Font = font;
				buttonXTotalCPP.Font = font;
				buttonXTotalCost.Font = font;
				buttonXTotalPeriods.Font = font;
				buttonXTotalSpots.Font = font;
				buttonXTotalGRP.Font = font;

				hyperLinkEditInfoAdvanced.Font = new Font(hyperLinkEditInfoAdvanced.Font.FontFamily,
					hyperLinkEditInfoAdvanced.Font.Size - 2, hyperLinkEditInfoAdvanced.Font.Style);
				hyperLinkEditInfoContract.Font = new Font(hyperLinkEditInfoContract.Font.FontFamily,
					hyperLinkEditInfoContract.Font.Size - 2, hyperLinkEditInfoContract.Font.Style);
			}

			xtraTabPageOptionsLine.Text = MediaMetaData.Instance.DataTypeString;

			quarterSelectorControl.QuarterSelected += OnQuarterChanged;

			retractableBarControl.Collapse(true);

			_tabDragDropHelper = new XtraTabDragDropHelper<SectionControl>(xtraTabControlSections);
			_tabDragDropHelper.TabMoved += OnTabMoved;

			digitalInfoControl.RequestDefaultInfo += (o, e) =>
			{
				((BaseEdit)e.Editor).EditValue = Schedule.DigitalProductsContent.GetDigitalInfo(e);
				((BaseEdit)e.Editor).Tag = ((BaseEdit)e.Editor).EditValue;
			};

			digitalInfoControl.SettingsChanged += (o, args) =>
			{
				SettingsNotSaved = true;
			};

			xtraTabPageOptionsDigital.PageVisible = Controller.Instance.TabDigitalProduct.Visible || Controller.Instance.TabDigitalPackage.Visible;
			BusinessObjects.Instance.OutputManager.ColorsChanged += (o, e) =>
			{
				InitColorControls();
				LoadBarButtons();
			};

			BusinessObjects.Instance.ThemeManager.ThemesChanged += (o, e) => OnOuterThemeChanged();

			Controller.Instance.ProgramScheduleNew.Click += OnAddSection;
			Controller.Instance.ProgramScheduleProgramAdd.Click += OnAddProgram;
			Controller.Instance.ProgramScheduleProgramDelete.Click += OnDeleteProgram;
		}

		protected override void UpdateEditedContet()
		{
			_allowToSave = false;

			var quickLoad = EditedContent != null && !(ContentUpdateInfo.ChangeInfo.WholeScheduleChanged ||
				ContentUpdateInfo.ChangeInfo.ScheduleDatesChanged ||
				ContentUpdateInfo.ChangeInfo.CalendarTypeChanged ||
				ContentUpdateInfo.ChangeInfo.SpotTypeChanged ||
				ContentUpdateInfo.ChangeInfo.SummaryChanged);

			if (EditedContent != null)
				EditedContent.Dispose();
			EditedContent = Schedule.ProgramSchedule.Clone<ProgramScheduleContent, ProgramScheduleContent>();

			labelControlScheduleInfo.Text = String.Format("{0}{3}<color=gray><i>{1} ({2})</i></color>",
				ScheduleSettings.BusinessName,
				ScheduleSettings.FlightDates,
				String.Format("{0} {1}s", EditedContent.TotalPeriods, SpotTitle),
				Environment.NewLine);

			laTotalPeriodsTitle.Text = String.Format("Total {0}s:", SpotTitle);
			if (MediaMetaData.Instance.ListManager.FlexFlightDatesAllowed &&
				(ScheduleSettings.FlightDateStart != ScheduleSettings.UserFlightDateStart ||
					ScheduleSettings.FlightDateEnd != ScheduleSettings.UserFlightDateEnd))
				labelControlFlexFlightDatesWarning.Visible = true;
			else
				labelControlFlexFlightDatesWarning.Visible = false;

			buttonXRating.Enabled = ScheduleSettings.UseDemo & !String.IsNullOrEmpty(ScheduleSettings.Demo);
			buttonXRating.Text = ScheduleSettings.DemoType == DemoType.Rtg ? "Rtg" : "(000s)";
			buttonXCPP.Enabled = ScheduleSettings.UseDemo & !String.IsNullOrEmpty(ScheduleSettings.Demo);
			buttonXGRP.Enabled = ScheduleSettings.UseDemo & !String.IsNullOrEmpty(ScheduleSettings.Demo);
			buttonXTotalCPP.Enabled = ScheduleSettings.UseDemo & !String.IsNullOrEmpty(ScheduleSettings.Demo);
			buttonXTotalGRP.Enabled = ScheduleSettings.UseDemo & !String.IsNullOrEmpty(ScheduleSettings.Demo);
			buttonXCPP.Text = ScheduleSettings.DemoType == DemoType.Rtg ? "CPP" : "CPM";
			buttonXTotalCPP.Text = ScheduleSettings.DemoType == DemoType.Rtg ? "Overall CPP" : "Overall CPM";
			laTotalCPPTitle.Text = ScheduleSettings.DemoType == DemoType.Rtg ? "Overall CPP:" : "Overall CPM:";
			buttonXGRP.Text = ScheduleSettings.DemoType == DemoType.Rtg ? "GRPs" : "Impr";
			buttonXTotalGRP.Text = ScheduleSettings.DemoType == DemoType.Rtg ? "Total GRPs" : "Total Impr";
			laTotalGRPTitle.Text = ScheduleSettings.DemoType == DemoType.Rtg ? "Total GRPs:" : "Total Impr:";

			quarterSelectorControl.InitControls(
				ScheduleSettings.Quarters,
				ScheduleSettings.Quarters.FirstOrDefault(q => q.DateAnchor == EditedContent.SelectedQuarter));
			quarterSelectorControl.Enabled = false;

			LoadSections(quickLoad);
			if (!quickLoad)
				InitColorControls();

			hyperLinkEditInfoContract.Enabled = BusinessObjects.Instance.OutputManager.ContractTemplateFolder.ExistsLocal();

			xtraTabPageOptionsDigital.PageEnabled = Schedule.DigitalProductsContent.DigitalProducts.Any();
			digitalInfoControl.InitData(EditedContent.DigitalLegend);
			LoadBarButtons();

			LoadActiveSectionData();

			_allowToSave = true;
		}

		protected override void ApplyChanges()
		{
			xtraTabControlSections.TabPages
				.OfType<SectionControl>()
				.ToList()
				.ForEach(sectionControl => sectionControl.SaveData());
			MediaMetaData.Instance.SettingsManager.SelectedColor = outputColorSelector.SelectedColor ?? String.Empty;
			MediaMetaData.Instance.SettingsManager.SaveSettings();
			digitalInfoControl.SaveData();

			ChangeInfo.ProgramScheduleChanged = ChangeInfo.ProgramScheduleChanged || SettingsNotSaved;
		}

		protected override void SaveData()
		{
			Schedule.ProgramSchedule = EditedContent.Clone<ProgramScheduleContent, ProgramScheduleContent>();
		}

		public override void GetHelp()
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink(String.Format("{0}ly", SpotTitle.ToLower()));
		}

		protected override void LoadThemes()
		{
			base.LoadThemes();
			FormThemeSelector.Link(Controller.Instance.ProgramScheduleTheme, BusinessObjects.Instance.ThemeManager.GetThemes(SlideType), MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType), (t =>
			{
				MediaMetaData.Instance.SettingsManager.SetSelectedTheme(SlideType, t.Name);
				MediaMetaData.Instance.SettingsManager.SaveSettings();
				IsThemeChanged = true;
			}));
			Controller.Instance.ProgramScheduleThemeBar.RecalcLayout();
			Controller.Instance.ProgramSchedulePanel.PerformLayout();
		}
		#endregion

		#region Sections Management
		private void LoadSections(bool quickLoad)
		{
			if (!quickLoad)
			{
				xtraTabControlSections.TabPages
					.OfType<SectionControl>()
					.ToList()
					.ForEach(sc => sc.Release());
				xtraTabControlSections.TabPages.Clear();
			}
			foreach (var section in EditedContent.Sections.OrderBy(s => s.Index))
			{
				if (quickLoad)
				{
					var sectionTabControl = xtraTabControlSections.TabPages
						.OfType<SectionControl>()
						.FirstOrDefault(sc => sc.SectionData.UniqueID.Equals(section.UniqueID));
					if (sectionTabControl == null) continue;
					sectionTabControl.LoadData(section);
				}
				else
					AddSectionControl(section);
			}
			if (!quickLoad)
				xtraTabControlSections.SelectedTabPageIndex = 0;
			UpdateSectionSplash();
		}

		private void LoadActiveSectionData()
		{
			_allowToSave = false;

			pnApplyForAll.Visible = EditedContent.Sections.Count > 0 && ActiveSection != null;
			checkEditApplyForAll.Checked = EditedContent.ApplySettingsForAll;

			if (ActiveSection != null)
			{
				buttonXRate.Checked = ActiveSection.SectionData.ShowRate;
				buttonXRating.Checked = ActiveSection.SectionData.ShowRating;
				buttonXCost.Checked = ActiveSection.SectionData.ShowCost;
				buttonXCPP.Checked = ActiveSection.SectionData.ShowCPP;
				buttonXDay.Checked = ActiveSection.SectionData.ShowDay;
				buttonXDaypart.Checked = ActiveSection.SectionData.ShowDaypart;
				buttonXGRP.Checked = ActiveSection.SectionData.ShowGRP;
				buttonXLength.Checked = ActiveSection.SectionData.ShowLenght;
				buttonXLogo.Checked = ActiveSection.SectionData.ShowLogo;
				buttonXStation.Checked = ActiveSection.SectionData.ShowStation;
				buttonXProgram.Checked = ActiveSection.SectionData.ShowProgram;
				buttonXTime.Checked = ActiveSection.SectionData.ShowTime;
				buttonXSpots.Text = String.Format("{0}s", SpotTitle);
				buttonXSpots.Checked = ActiveSection.SectionData.ShowSpots;

				buttonXTotalPeriods.Checked = ActiveSection.SectionData.ShowTotalPeriods;
				buttonXTotalPeriods.Text = String.Format("Total {0}s", SpotTitle);
				buttonXTotalSpots.Checked = ActiveSection.SectionData.ShowTotalSpots;
				buttonXTotalGRP.Checked = ActiveSection.SectionData.ShowTotalGRP;
				buttonXTotalCPP.Checked = ActiveSection.SectionData.ShowTotalCPP;
				buttonXAvgRate.Checked = ActiveSection.SectionData.ShowAverageRate;
				buttonXTotalCost.Checked = ActiveSection.SectionData.ShowTotalRate;
				buttonXNetRate.Checked = ActiveSection.SectionData.ShowNetRate;
				buttonXDiscount.Checked = ActiveSection.SectionData.ShowDiscount;

				UpdateQuarterSelectorControl();
				UpdateTotalsValues();
				UpdateTotalsVisibility();
			}
			UpdateOutputStatus();
			UpdateGenericDateColumnNamesWarning();
			_allowToSave = true;
		}

		private SectionControl AddSectionControl(ScheduleSection sectionData, int position = -1)
		{
			var sectionTabControl = new SectionControl();
			sectionTabControl.LoadData(sectionData);
			sectionTabControl.UpdateSpotsByQuarter(quarterSelectorControl.SelectedQuarter);
			sectionTabControl.DataChanged += (o, e) =>
			{
				var sourceControl = o as SectionControl;
				if (sourceControl == null) return;
				if (!_allowToSave) return;
				if (EditedContent.ApplySettingsForAll)
					ApplySharedSettings(sourceControl);
				UpdateTotalsVisibility();
				UpdateTotalsValues();
				UpdateOutputStatus();
				SettingsNotSaved = true;
			};
			position = position == -1 ? xtraTabControlSections.TabPages.OfType<SectionControl>().Count() : position;
			xtraTabControlSections.TabPages.Insert(position, sectionTabControl);
			return sectionTabControl;
		}

		private void AddSection()
		{
			using (var form = new FormSectionName())
			{
				if (form.ShowDialog(Controller.Instance.FormMain) != DialogResult.OK) return;
				var section = EditedContent.CreateSection();
				section.Name = form.SectionName;
				EditedContent.Sections.Add(section);
				EditedContent.RebuildSectionIndexes();
				var sectionControl = AddSectionControl(section);
				xtraTabControlSections.SelectedTabPage = sectionControl;
				UpdateSectionSplash();
				SettingsNotSaved = true;
			}
		}

		private void CloneSection(SectionControl sectionControl)
		{
			using (var form = new FormSectionName())
			{
				form.SectionName = String.Format("{0} (Clone)", sectionControl.SectionData.Name);
				if (form.ShowDialog(Controller.Instance.FormMain) != DialogResult.OK) return;
				var section = sectionControl.SectionData.Clone<ScheduleSection, ScheduleSection>();
				section.Name = form.SectionName;
				section.Index += 0.5;
				EditedContent.Sections.Add(section);
				EditedContent.RebuildSectionIndexes();
				var newControl = AddSectionControl(section, (Int32)section.Index);
				xtraTabControlSections.SelectedTabPage = newControl;
				SettingsNotSaved = true;
			}
		}

		private void DeleteSection(SectionControl sectionControl)
		{
			if (PopupMessageHelper.Instance.ShowWarningQuestion("Are you sure want to delete {0}?", sectionControl.SectionData.Name) != DialogResult.Yes) return;
			EditedContent.Sections.Remove(sectionControl.SectionData);
			EditedContent.RebuildSectionIndexes();
			xtraTabControlSections.TabPages.Remove(sectionControl);
			UpdateSectionSplash();
			SettingsNotSaved = true;
		}

		private void RenameSection(SectionControl snapshotControl)
		{
			if (snapshotControl == null) return;
			using (var form = new FormSectionName())
			{
				form.SectionName = snapshotControl.SectionData.Name;
				if (form.ShowDialog(Controller.Instance.FormMain) != DialogResult.OK) return;
				snapshotControl.SectionData.Name = form.SectionName;
				snapshotControl.Text = form.SectionName;
				SettingsNotSaved = true;
			}
		}

		private void ApplySharedSettings(SectionControl templateControl)
		{
			foreach (var sectionTabControl in xtraTabControlSections.TabPages
				.OfType<SectionControl>()
				.Where(oc => oc.SectionData.UniqueID != templateControl.SectionData.UniqueID))
				sectionTabControl.ApplyDataFromTemplate(templateControl);
		}

		private void ApplySharedContractSettings(ContractSettings templateSettings)
		{
			foreach (var sectionTabControl in xtraTabControlSections.TabPages.OfType<SectionControl>())
			{
				sectionTabControl.SectionData.ContractSettings.ShowSignatureLine = templateSettings.ShowSignatureLine;
				sectionTabControl.SectionData.ContractSettings.ShowDisclaimer = templateSettings.ShowDisclaimer;
				sectionTabControl.SectionData.ContractSettings.RateExpirationDate = templateSettings.RateExpirationDate;
			}
		}

		private void UpdateQuarterSelectorControl()
		{
			if (ActiveSection == null) return;
			quarterSelectorControl.Enabled = ActiveSection.SectionData.ShowSpots;
		}

		private void UpdateTotalsVisibility()
		{
			if (ActiveSection == null) return;

			pnBottom.Visible = ActiveSection.SectionData.Programs.Any() &&
				(ActiveSection.SectionData.ShowTotalCPP ||
				ActiveSection.SectionData.ShowTotalGRP ||
				ActiveSection.SectionData.ShowTotalSpots ||
				ActiveSection.SectionData.ShowTotalPeriods ||
				ActiveSection.SectionData.ShowAverageRate ||
				ActiveSection.SectionData.ShowTotalRate ||
				ActiveSection.SectionData.ShowNetRate ||
				ActiveSection.SectionData.ShowDiscount);

			pnTotalCPP.Visible = ActiveSection.SectionData.ShowTotalCPP;
			pnTotalCPP.SendToBack();
			pnTotalGRP.Visible = ActiveSection.SectionData.ShowTotalGRP;
			pnTotalGRP.SendToBack();
			pnTotalSpots.Visible = ActiveSection.SectionData.ShowTotalSpots;
			pnTotalSpots.SendToBack();
			pnTotalPeriods.Visible = ActiveSection.SectionData.ShowTotalPeriods;
			pnTotalPeriods.SendToBack();
			pnAvgRate.Visible = ActiveSection.SectionData.ShowAverageRate;
			pnAvgRate.BringToFront();
			pnTotalCost.Visible = ActiveSection.SectionData.ShowTotalRate;
			pnTotalCost.BringToFront();
			pnNetRate.Visible = ActiveSection.SectionData.ShowNetRate;
			pnNetRate.BringToFront();
			pnAgencyDiscount.Visible = ActiveSection.SectionData.ShowDiscount;
			pnAgencyDiscount.BringToFront();
		}

		private void UpdateTotalsValues()
		{
			if (ActiveSection != null && ActiveSection.SectionData.Programs.Any())
			{
				laTotalPeriodsValue.Text = ActiveSection.TotalPeriodsValueFormatted;
				laTotalSpotsValue.Text = ActiveSection.TotalSpotsValueFormatted;
				laTotalGRPValue.Text = ActiveSection.TotalGRPValueFormatted;
				laTotalCPPValue.Text = ActiveSection.TotalCPPValueFormatted;
				laAvgRateValue.Text = ActiveSection.AvgRateValueFormatted;
				laTotalCostValue.Text = ActiveSection.TotalCostValuesFormatted;
				laNetRateValue.Text = ActiveSection.NetRateValueFormatted;
				laAgencyDiscountValue.Text = ActiveSection.TotalDiscountValueFormatted;
			}
			else
			{
				laTotalPeriodsValue.Text =
				laTotalSpotsValue.Text =
				laTotalGRPValue.Text =
				laTotalCPPValue.Text =
				laAvgRateValue.Text =
				laTotalCostValue.Text =
				laNetRateValue.Text =
					laAgencyDiscountValue.Text = String.Empty;
			}
		}

		private void UpdateGenericDateColumnNamesWarning()
		{
			labelControlGenericSpotColumNamesWarning.Visible = ActiveSection != null &&
				ActiveSection.SectionData.UseGenericDateColumns;
		}

		private void UpdateOutputStatus()
		{
			Controller.Instance.ProgramSchedulePowerPoint.Enabled =
				Controller.Instance.ProgramSchedulePdf.Enabled =
				Controller.Instance.ProgramSchedulePreview.Enabled =
				Controller.Instance.ProgramScheduleEmail.Enabled = xtraTabControlSections.TabPages
				.OfType<SectionControl>()
				.Any(sectionTabControl => sectionTabControl.ReadyForOutput);
			Controller.Instance.ContentController.UpdateTabsSate();
		}

		private void OnTabMoved(object sender, TabMoveEventArgs e)
		{
			EditedContent.ChangeSectionPosition(
				EditedContent.Sections.IndexOf(((SectionControl)e.MovedPage).SectionData),
				EditedContent.Sections.IndexOf(((SectionControl)e.TargetPage).SectionData) + (1 * e.Offset));
			SettingsNotSaved = true;
		}

		private void UpdateSectionSplash()
		{
			if (EditedContent.Sections.Any())
			{
				pnSections.BringToFront();
				Controller.Instance.ProgramScheduleProgramAdd.Enabled = true;
				Controller.Instance.ProgramScheduleProgramDelete.Enabled = true;
			}
			else
			{
				pnNoSections.BringToFront();
				Controller.Instance.ProgramScheduleProgramAdd.Enabled = false;
				Controller.Instance.ProgramScheduleProgramDelete.Enabled = false;
			}
		}

		private void xtraTabControlSections_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			if (!_allowToSave) return;
			LoadActiveSectionData();
		}

		private void xtraTabControlSections_CloseButtonClick(object sender, EventArgs e)
		{
			var arg = (ClosePageButtonEventArgs)e;
			var sectionControl = arg.Page as SectionControl;
			if (sectionControl == null) return;
			DeleteSection(sectionControl);
		}

		private void xtraTabControlSections_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) return;
			_menuHitInfo = xtraTabControlSections.CalcHitInfo(new Point(e.X, e.Y));
			if (_menuHitInfo.HitTest != XtraTabHitTest.PageHeader) return;
			contextMenuStripSections.Show((Control)sender, e.Location);
		}

		private void toolStripMenuItemSectionsRename_Click(object sender, EventArgs e)
		{
			RenameSection((SectionControl)_menuHitInfo.Page);
		}

		private void toolStripMenuItemSectionsClone_Click(object sender, EventArgs e)
		{
			CloneSection((SectionControl)_menuHitInfo.Page);
		}
		#endregion

		#region Program Management
		#endregion

		#region Settings management
		private void InitColorControls()
		{
			xtraTabPageOptionsStyle.PageVisible = BusinessObjects.Instance.OutputManager.ScheduleColors.Items.Count > 1;
			outputColorSelector.InitData(BusinessObjects.Instance.OutputManager.ScheduleColors, MediaMetaData.Instance.SettingsManager.SelectedColor);
			outputColorSelector.ColorChanged += OnColorChanged;
		}

		private void LoadBarButtons()
		{
			var buttonInfos = new List<ButtonInfo>();
			buttonInfos.Add(new ButtonInfo { Logo = MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? Resources.SectionSettingsTV : Resources.SectionSettingsRadio, Tooltip = String.Format("Open {0} Schedule Settings", MediaMetaData.Instance.DataTypeString), Action = () => { xtraTabControlOptions.SelectedTabPage = xtraTabPageOptionsLine; } });
			if (Schedule.DigitalProductsContent.DigitalProducts.Any() && (Controller.Instance.TabDigitalProduct.Visible || Controller.Instance.TabDigitalPackage.Visible))
				buttonInfos.Add(new ButtonInfo { Logo = Resources.SectionSettingsDigital, Tooltip = "Open Digital Settings", Action = () => { xtraTabControlOptions.SelectedTabPage = xtraTabPageOptionsDigital; } });
			buttonInfos.Add(new ButtonInfo { Logo = Resources.SectionSettingsInfo, Tooltip = "Open Schedule Info", Action = () => { xtraTabControlOptions.SelectedTabPage = xtraTabPageOptionsTotals; } });
			if (BusinessObjects.Instance.OutputManager.ScheduleColors.Items.Count > 1)
				buttonInfos.Add(new ButtonInfo { Logo = Resources.SectionSettingsOptions, Tooltip = "Open Slide Style", Action = () => { xtraTabControlOptions.SelectedTabPage = xtraTabPageOptionsStyle; } });
			retractableBarControl.AddButtons(buttonInfos);
		}

		private void OnSettingsChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			if (ActiveSection == null) return;
			ActiveSection.SectionData.ShowRate = buttonXRate.Checked;
			ActiveSection.SectionData.ShowRating = buttonXRating.Checked;
			ActiveSection.SectionData.ShowCost = buttonXCost.Checked;
			ActiveSection.SectionData.ShowCPP = buttonXCPP.Checked;
			ActiveSection.SectionData.ShowDay = buttonXDay.Checked;
			ActiveSection.SectionData.ShowDaypart = buttonXDaypart.Checked;
			ActiveSection.SectionData.ShowGRP = buttonXGRP.Checked;
			ActiveSection.SectionData.ShowLenght = buttonXLength.Checked;
			ActiveSection.SectionData.ShowLogo = buttonXLogo.Checked;
			ActiveSection.SectionData.ShowStation = buttonXStation.Checked;
			ActiveSection.SectionData.ShowProgram = buttonXProgram.Checked;
			ActiveSection.SectionData.ShowTime = buttonXTime.Checked;
			ActiveSection.SectionData.ShowSpots = buttonXSpots.Checked;

			ActiveSection.SectionData.ShowTotalPeriods = buttonXTotalPeriods.Checked;
			ActiveSection.SectionData.ShowTotalSpots = buttonXTotalSpots.Checked;
			ActiveSection.SectionData.ShowTotalGRP = buttonXTotalGRP.Checked;
			ActiveSection.SectionData.ShowTotalCPP = buttonXTotalCPP.Checked;
			ActiveSection.SectionData.ShowAverageRate = buttonXAvgRate.Checked;
			ActiveSection.SectionData.ShowTotalRate = buttonXTotalCost.Checked;
			ActiveSection.SectionData.ShowNetRate = buttonXNetRate.Checked;
			ActiveSection.SectionData.ShowDiscount = buttonXDiscount.Checked;

			EditedContent.ApplySettingsForAll = checkEditApplyForAll.Checked;

			if (EditedContent.ApplySettingsForAll)
			{
				ApplySharedSettings(ActiveSection);
				xtraTabControlSections.TabPages.OfType<SectionControl>().ToList().ForEach(oc => oc.UpdateGridView());
			}
			else
				ActiveSection.UpdateGridView();

			UpdateQuarterSelectorControl();
			UpdateTotalsVisibility();
			UpdateTotalsValues();
			SettingsNotSaved = true;
		}

		private void OnAdvancedSettingsOpenLink(object sender, OpenLinkEventArgs e)
		{
			e.Handled = true;
			if (ActiveSection == null) return;
			using (var form = new FormOutputSettings())
			{
				form.checkEditEmptySports.Text = String.Format(form.checkEditEmptySports.Text, String.Format("{0}s:", SpotTitle));
				form.checkEditEmptySports.Enabled = ActiveSection.SectionData.ShowSpots;
				form.labelControlDescriptionEmptySports.Enabled = ActiveSection.SectionData.ShowSpots;
				form.checkEditEmptySports.Checked = !ActiveSection.SectionData.ShowEmptySpots;
				form.checkEditOutputNoBrackets.Checked = ActiveSection.SectionData.OutputNoBrackets;
				form.checkEditUseGenericDates.Checked = ActiveSection.SectionData.UseGenericDateColumns;
				form.checkEditUseDecimalRate.Checked = ActiveSection.SectionData.UseDecimalRates;
				form.checkEditOutputLimitQuarters.Enabled = ScheduleSettings.Quarters.Count > 1;
				form.labelControlDescriptionOutputLimitQuarters.Enabled = ScheduleSettings.Quarters.Count > 1;
				form.checkEditOutputLimitQuarters.Checked = ScheduleSettings.Quarters.Count > 1 && ActiveSection.SectionData.OutputPerQuater;
				form.checkEditOutputLimitPeriods.Checked = ActiveSection.SectionData.OutputMaxPeriods.HasValue;
				form.spinEditOutputLimitPeriods.EditValue = ActiveSection.SectionData.OutputMaxPeriods;
				form.checkEditOutputLimitPeriods.Text = String.Format(form.checkEditOutputLimitPeriods.Text, SpotTitle);
				form.checkEditLockToMaster.Checked = MediaMetaData.Instance.SettingsManager.UseSlideMaster;

				if (form.ShowDialog() != DialogResult.OK) return;

				var updateColumns = ActiveSection.SectionData.UseGenericDateColumns != form.checkEditUseGenericDates.Checked;

				ActiveSection.SectionData.ShowEmptySpots = !form.checkEditEmptySports.Checked;
				ActiveSection.SectionData.OutputNoBrackets = form.checkEditOutputNoBrackets.Checked;
				ActiveSection.SectionData.UseDecimalRates = form.checkEditUseDecimalRate.Checked;
				ActiveSection.SectionData.UseGenericDateColumns = form.checkEditUseGenericDates.Checked;
				ActiveSection.SectionData.OutputPerQuater = form.checkEditOutputLimitQuarters.Checked;
				ActiveSection.SectionData.OutputMaxPeriods = form.spinEditOutputLimitPeriods.EditValue != null ? (Int32?)form.spinEditOutputLimitPeriods.Value : null;

				MediaMetaData.Instance.SettingsManager.UseSlideMaster = form.checkEditLockToMaster.Checked;

				if (EditedContent.ApplySettingsForAll)
				{
					ApplySharedSettings(ActiveSection);
					xtraTabControlSections.TabPages.OfType<SectionControl>().ToList().ForEach(oc =>
					{
						oc.UpdateGridView();
						if (updateColumns)
							oc.UpdateGridData(true);
					});
				}
				else
				{
					ActiveSection.UpdateGridView();
					if (updateColumns)
						ActiveSection.UpdateGridData(true);
				}

				UpdateGenericDateColumnNamesWarning();

				SettingsNotSaved = true;
			}
		}

		private void OnContractSettingsOpenLink(object sender, OpenLinkEventArgs e)
		{
			e.Handled = true;
			if (ActiveSection == null) return;
			using (var form = new FormContractSettings())
			{
				form.checkEditShowSignatureLine.Checked = ActiveSection.SectionData.ContractSettings.ShowSignatureLine;
				form.checkEditShowRatesExpiration.Checked = ActiveSection.SectionData.ContractSettings.RateExpirationDate.HasValue;
				form.checkEditShowDisclaimer.Checked = ActiveSection.SectionData.ContractSettings.ShowDisclaimer;
				form.dateEditRatesExpirationDate.EditValue = ActiveSection.SectionData.ContractSettings.RateExpirationDate;
				if (form.ShowDialog() != DialogResult.OK) return;
				ActiveSection.SectionData.ContractSettings.ShowSignatureLine = form.checkEditShowSignatureLine.Checked;
				ActiveSection.SectionData.ContractSettings.ShowDisclaimer = form.checkEditShowDisclaimer.Checked;
				ActiveSection.SectionData.ContractSettings.RateExpirationDate = (DateTime?)form.dateEditRatesExpirationDate.EditValue;
				if (EditedContent.ApplySettingsForAll)
					ApplySharedContractSettings(ActiveSection.SectionData.ContractSettings);
				SettingsNotSaved = true;
			}
		}

		private void OnQuarterChanged(object sender, EventArgs e)
		{
			var selectedQuarter = quarterSelectorControl.SelectedQuarter;
			EditedContent.SelectedQuarter = selectedQuarter != null ? selectedQuarter.DateAnchor : (DateTime?)null;
			foreach (var sectionTabControl in xtraTabControlSections.TabPages.OfType<SectionControl>())
				sectionTabControl.UpdateSpotsByQuarter(selectedQuarter);
			SettingsNotSaved = true;
		}

		private void OnFlexFlightDatesWarningClick(object sender, EventArgs e)
		{
			using (var form = new FormFlexFlightDatesWarning())
			{
				form.ShowDialog();
			}
		}

		private void OnColorChanged(object sender, EventArgs e)
		{
			SettingsNotSaved = true;
		}
		#endregion

		#region Ribbon Operations Events
		public void OnAddSection(object sender, EventArgs e)
		{
			AddSection();
		}

		public void OnAddProgram(object sender, EventArgs e)
		{
			if (ActiveSection == null) return;
			ActiveSection.AddProgram();
		}

		public void OnDeleteProgram(object sender, EventArgs e)
		{
			if (ActiveSection == null) return;
			ActiveSection.DeleteProgram();
		}
		#endregion

		#region Output Staff

		public bool ShowDigitalLegendOnlyFirstSlide
		{
			get { return EditedContent.DigitalLegend.OutputOnlyOnce; }
		}

		public string DigitalLegend
		{
			get
			{
				if (!EditedContent.DigitalLegend.Enabled) return String.Empty;
				var requestOptions = EditedContent.DigitalLegend.RequestOptions;
				if (!EditedContent.DigitalLegend.AllowEdit)
				{
					requestOptions.Separator = " ";
					return String.Format("Digital Product Info: {0}{1}{2}",
						Schedule.DigitalProductsContent.GetDigitalInfo(requestOptions),
						requestOptions.Separator,
						EditedContent.DigitalLegend.GetAdditionalData(" "));
				}
				if (!String.IsNullOrEmpty(EditedContent.DigitalLegend.CompiledInfo))
					return String.Format("Digital Product Info: {0}{1}{2}",
						EditedContent.DigitalLegend.CompiledInfo,
						requestOptions.Separator,
						EditedContent.DigitalLegend.GetAdditionalData(" "));
				return String.Empty;
			}
		}

		private IEnumerable<SectionControl> SelectSectionsForOutput()
		{
			var tabPages = xtraTabControlSections.TabPages.OfType<SectionControl>().Where(ss => ss.ReadyForOutput).ToList();
			var selectedSections = new List<SectionControl>();
			if (tabPages.Count > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Schedules";
					var currentSection = (SectionControl)xtraTabControlSections.SelectedTabPage;
					foreach (var tabPage in tabPages)
					{
						var item = new CheckedListBoxItem(tabPage, tabPage.SectionData.Name);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (tabPage == currentSection)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						selectedSections.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<SectionControl>());
				}
			else
				selectedSections.AddRange(tabPages);
			return selectedSections;
		}

		public override void OutputPowerPoint()
		{
			var selectedSections = new List<SectionControl>(SelectSectionsForOutput());
			if (!selectedSections.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				foreach (var sectionTabControl in selectedSections)
					sectionTabControl.GenerateOutput();
				FormProgress.CloseProgress();
			});
		}

		public override void OutputPdf()
		{
			var selectedSections = new List<SectionControl>(SelectSectionsForOutput());
			if (!selectedSections.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				var previewGroups = selectedSections.Select(summaryTab => summaryTab.GeneratePreview()).ToList();
				var pdfFileName = Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
					String.Format("{0}-{1}.pdf", Schedule.Name, DateTime.Now.ToString("MM-dd-yy-hmmss")));
				RegularMediaSchedulePowerPointHelper.Instance.BuildPdf(pdfFileName, previewGroups.Select(pg => pg.PresentationSourcePath));
				if (File.Exists(pdfFileName))
					try
					{
						Process.Start(pdfFileName);
					}
					catch { }
				FormProgress.CloseProgress();
			});
		}

		public override void Preview()
		{
			var selectedSections = new List<SectionControl>(SelectSectionsForOutput());
			if (!selectedSections.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			var previewGroups = selectedSections.Select(sectionControl => sectionControl.GeneratePreview()).ToList();
			Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			FormProgress.CloseProgress();

			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;

			using (var formPreview = new FormPreview(
				Controller.Instance.FormMain,
				RegularMediaSchedulePowerPointHelper.Instance,
				BusinessObjects.Instance.HelpManager,
				Controller.Instance.ShowFloater))
			{
				formPreview.Text = "Preview Schedule";
				formPreview.LoadGroups(previewGroups);
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			}
		}

		public override void Email()
		{
			var selectedSections = new List<SectionControl>(SelectSectionsForOutput());
			if (!selectedSections.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			var previewGroups = selectedSections.Select(sectionControl => sectionControl.GeneratePreview()).ToList();
			Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			FormProgress.CloseProgress();

			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formEmail = new FormEmail(RegularMediaSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager))
			{
				formEmail.Text = "Email this Schedule";
				formEmail.LoadGroups(previewGroups);
				Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
				RegistryHelper.MainFormHandle = formEmail.Handle;
				RegistryHelper.MaximizeMainForm = false;
				formEmail.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
			}
		}
		#endregion
	}
}