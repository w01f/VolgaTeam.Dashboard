using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.InteropClasses;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputForms;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	[ToolboxItem(false)]
	public partial class OutputMultiGridControl : UserControl, IGridOutputControl
	{
		private readonly List<Insert> _inserts = new List<Insert>();
		private GridColumn _activeCol;

		public OutputMultiGridControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AllowToSave = true;

			textEditHeader.Hide();
			textEditHeader.Parent = gridControlPublication;

			ColumnsColumns = new ColumnsControl(this);
			ColumnsColumns.OnHelp += (o, e) => BusinessWrapper.Instance.HelpManager.OpenHelpLink("logonavbar1");

			AdNotes = new AdNotesControl(this);
			AdNotes.OnHelp += (o, e) => BusinessWrapper.Instance.HelpManager.OpenHelpLink("logonavbar2");

			SlideBullets = new SlideBulletsControl(this);
			SlideBullets.OnHelp += (o, e) => BusinessWrapper.Instance.HelpManager.OpenHelpLink("logonavbar4");
			SlideBullets.checkEditColumnInches.Visible = false;
			SlideBullets.checkEditDimensions.Visible = false;
			SlideBullets.checkEditPageSize.Visible = false;
			SlideBullets.checkEditPercentOfPage.Visible = false;
			SlideBullets.checkEditDelivery.Visible = false;
			SlideBullets.checkEditReadership.Visible = false;

			SlideHeader = new SlideHeaderControl(this);
			SlideHeader.OnHelp += (o, e) => BusinessWrapper.Instance.HelpManager.OpenHelpLink("logonavbar3");
			SlideHeader.checkEditPublicationName.Visible = false;
			SlideHeader.checkEditLogo1.Visible = false;
			SlideHeader.checkEditLogo2.Visible = false;
			SlideHeader.checkEditLogo3.Visible = false;
			SlideHeader.checkEditLogo4.Visible = false;

			HelpToolTip = new SuperTooltipInfo("HELP", "", "Learn more about the Logo Grid", null, null, eTooltipColor.Gray);

			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate()
			{
				if (sender != this)
					UpdateOutput(e.QuickSave);
			});
		}

		public SlideBulletsState SlideBulletsState
		{
			get { return LocalSchedule.ViewSettings.MultiGridViewSettings.SlideBulletsState; }
		}

		public SlideHeaderState SlideHeaderState
		{
			get { return LocalSchedule.ViewSettings.MultiGridViewSettings.SlideHeaderState; }
		}

		public DigitalLegend DigitalLegend
		{
			get { return LocalSchedule.ViewSettings.MultiGridViewSettings.DigitalLegend; }
		}

		#region Common Methods
		public void EditDigitalLegend()
		{
			using (var form = new FormDigital(LocalSchedule.ViewSettings.MultiGridViewSettings.DigitalLegend))
			{
				form.WebsiteRequestDefault += (o, e) =>
				{
					e.Editor.EditValue = LocalSchedule.DigitalWebsites;
				};
				form.SimpleDigitalInfoRequestDefault += (o, e) =>
				{
					e.Editor.EditValue = LocalSchedule.DigitalSimpleInfo;
				};
				form.DetailedDigitalInfoRequestDefault += (o, e) =>
				{
					e.Editor.EditValue = LocalSchedule.DigitalDetailedInfo;
				};
				if (form.ShowDialog() == DialogResult.OK)
					SettingsNotSaved = true;
			}
		}

		public void UpdateOutput(bool quickLoad)
		{
			LocalSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			if (!quickLoad)
			{
				AllowToSave = false;
				comboBoxEditSchedule.Properties.Items.Clear();
				comboBoxEditSchedule.Properties.Items.AddRange(Core.AdSchedule.ListManager.Instance.OutputHeaders.ToArray());
				if (string.IsNullOrEmpty(LocalSchedule.ViewSettings.MultiGridViewSettings.SlideHeader))
				{
					if (comboBoxEditSchedule.Properties.Items.Count > 0)
						comboBoxEditSchedule.SelectedIndex = 0;
				}
				else
				{
					int index = comboBoxEditSchedule.Properties.Items.IndexOf(LocalSchedule.ViewSettings.MultiGridViewSettings.SlideHeader);
					if (index >= 0)
						comboBoxEditSchedule.SelectedIndex = index;
					else
						comboBoxEditSchedule.SelectedIndex = 0;
				}

				laDate.Text = LocalSchedule.PresentationDateObject != null ? LocalSchedule.PresentationDate.ToString("MM/dd/yy") : string.Empty;
				laBusinessName.Text = " " + LocalSchedule.BusinessName + (!string.IsNullOrEmpty(LocalSchedule.AccountNumber) ? (" - " + LocalSchedule.AccountNumber) : string.Empty);
				laDecisionMaker.Text = LocalSchedule.DecisionMaker;
				laFlightDates.Text = " " + LocalSchedule.FlightDateStart.ToString("MM/dd/yy") + " - " + LocalSchedule.FlightDateEnd.ToString("MM/dd/yy");

				PrepareInserts();
				gridControlPublication.DataSource = _inserts;

				LoadView();

				SetColumnsState();

				UpdateSlideBullets();
				AllowToSave = true;
			}
			SettingsNotSaved = false;
		}

		private void ResetToDefault()
		{
			LocalSchedule.ViewSettings.MultiGridViewSettings.ResetToDefault();
			AllowToSave = false;
			LoadView();
			SetColumnsState();
			UpdateSlideBullets();
			AllowToSave = true;
			SettingsNotSaved = false;
			Controller.Instance.SaveSchedule(LocalSchedule, true, this);
		}

		public void OpenHelp()
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("logogrid");
		}

		private void UpdateSlideBullets()
		{
			SlideBullets.TotalInserts = LocalSchedule.PrintProducts.Sum(x => x.TotalInserts).ToString("#,##0");
			SlideBullets.AvgAdCost = LocalSchedule.PrintProducts.Count > 0 ? LocalSchedule.PrintProducts.Average(x => x.AvgADRate).ToString("$#,###.00") : string.Empty;
			SlideBullets.AvgFinalCost = LocalSchedule.PrintProducts.Count > 0 ? LocalSchedule.PrintProducts.Average(x => x.AvgFinalRate).ToString("$#,###.00") : string.Empty;
			SlideBullets.AvgPCI = LocalSchedule.PrintProducts.Count > 0 ? LocalSchedule.PrintProducts.Average(x => x.AvgPCIRate).ToString("$#,###.00") : string.Empty;
			SlideBullets.TotalColor = LocalSchedule.PrintProducts.Sum(x => x.TotalColorPricingCalculated).ToString("$#,###.00");
			SlideBullets.Discounts = LocalSchedule.PrintProducts.Sum(x => x.TotalDiscountRate).ToString("$#,###.00");
			SlideBullets.TotalFinalCost = LocalSchedule.PrintProducts.Sum(x => x.TotalFinalRate).ToString("$#,###.00");
			SlideBullets.TotalSquare = LocalSchedule.PrintProducts.Sum(x => x.TotalSquare.HasValue ? x.TotalSquare.Value : 0).ToString("#,###.00#");
		}

		private void PrepareInserts()
		{
			_inserts.Clear();
			foreach (PrintProduct publication in LocalSchedule.PrintProducts)
				_inserts.AddRange(publication.Inserts);
			_inserts.Sort((x, y) => x.Date.CompareTo(y.Date));
		}
		#endregion

		#region View Methods
		public void SaveView()
		{
			if (AllowToSave)
			{
				LocalSchedule.ViewSettings.MultiGridViewSettings.ShowOptions = ShowOptions;
				LocalSchedule.ViewSettings.MultiGridViewSettings.SelectedOptionChapterIndex = SelectedOptionChapterIndex;

				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IDPosition = PositionID;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IndexPosition = PositionIndex;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DatePosition = PositionDate;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PCIPosition = PositionPCI;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.CostPosition = PositionCost;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.FinalCostPosition = PositionFinalCost;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DiscountPosition = PositionDiscount;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ColorPosition = PositionColor;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PublicationPosition = PositionPublication;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SquarePosition = PositionSquare;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PageSizePosition = PositionPageSize;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PercentOfPagePosition = PositionPercentOfPage;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DimensionsPosition = PositionDimensions;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.MechanicalsPosition = PositionMechanicals;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SectionPosition = PositionSection;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeliveryPosition = PositionDelivery;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ReadershipPosition = PositionReadership;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeadlinePosition = PositionDeadline;

				LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionSquare = PositionColumnInchesInPreview;
				LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionComments = PositionCommentsInPreview;
				LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionDeadline = PositionDeadlineInPreview;
				LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionDelivery = PositionDeliveryInPreview;
				LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionDimensions = PositionDimensionsInPreview;
				LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionMechanicals = PositionMechanicalsInPreview;
				LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionPageSize = PositionPageSizeInPreview;
				LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionPercentOfPage = PositionPercentOfPageInPreview;
				LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionPublication = PositionPublicationInPreview;
				LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionReadership = PositionReadershipInPreview;
				LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionSection = PositionSectionInPreview;

				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowColor = _showColorHeader;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowAdNotes = _showCommentsHeader;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowCost = _showCostHeader;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowDate = _showDateHeader;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowDeadline = _showDeadlineHeader;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowDelivery = _showDeliveryHeader;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowDimensions = _showDimensionsHeader;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowDiscount = _showDiscountHeader;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowFinalCost = _showFinalCostHeader;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowID = _showIDHeader;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowIndex = _showIndexHeader;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowMechanicals = _showMechanicalsHeader;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowPageSize = _showPageSizeHeader;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowPercentOfPage = _showPercentOfPageHeader;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowPCI = _showPCIHeader;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowPublication = _showPublicationHeader;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowReadership = _showReadershipHeader;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowSection = _showSectionHeader;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowSquare = _showSquareHeader;

				LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowSquare = ShowColumnInchesInPreview;
				LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowComments = ShowCommentsInPreview;
				LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowDeadline = ShowDeadlineInPreview;
				LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowDelivery = ShowDeliveryInPreview;
				LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowDimensions = ShowDimensionsInPreview;
				LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowMechanicals = ShowMechanicalsInPreview;
				LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowPageSize = ShowPageSizeInPreview;
				LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowPercentOfPage = ShowPercentOfPageInPreview;
				LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowPublication = ShowPublicationInPreview;
				LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowReadership = ShowReadershipInPreview;
				LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowSection = ShowSectionInPreview;

				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IDWidth = WidthID;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IndexWidth = WidthIndex;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DateWidth = WidthDate;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PCIWidth = WidthPCI;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.CostWidth = WidthCost;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.FinalCostWidth = WidthFinalCost;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DiscountWidth = WidthDiscount;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ColorWidth = WidthColor;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PublicationWidth = WidthPublication;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SquareWidth = WidthSquare;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PageSizeWidth = WidthPageSize;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PercentOfPageWidth = WidthPercentOfPage;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DimensionsWidth = WidthDimensions;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.MechanicalsWidth = WidthMechanicals;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SectionWidth = WidthSection;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeliveryWidth = WidthDelivery;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ReadershipWidth = WidthReadership;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeadlineWidth = WidthDeadline;

				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IDCaption = CaptionID;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IndexCaption = CaptionIndex;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DateCaption = CaptionDate;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PCICaption = CaptionPCI;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.CostCaption = CaptionCost;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.FinalCostCaption = CaptionFinalCost;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DiscountCaption = CaptionDiscount;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ColorCaption = CaptionColor;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PublicationCaption = CaptionPublication;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SquareCaption = CaptionSquare;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PageSizeCaption = CaptionPageSize;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PercentOfPageCaption = CaptionPercentOfPage;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DimensionsCaption = CaptionDimensions;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.MechanicalsCaption = CaptionMechanicals;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SectionCaption = CaptionSection;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeliveryCaption = CaptionDelivery;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ReadershipCaption = CaptionReadership;
				LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeadlineCaption = CaptionDeadline;

				SettingsNotSaved = true;
			}
		}

		public void SetPreviewState()
		{
			gridViewPublications.OptionsView.ShowPreview = _showCommentsHeader;
			gridViewPublications.RefreshData();
		}

		public void SetSlideHeader()
		{
			comboBoxEditSchedule.Enabled = SlideHeaderState.ShowSlideHeader;
			laBusinessName.Enabled = SlideHeaderState.ShowAdvertiser;
			laDate.Enabled = SlideHeaderState.ShowPresentationDate;
			laDecisionMaker.Enabled = SlideHeaderState.ShowDecisionMaker;
			laFlightDates.Enabled = SlideHeaderState.ShowFlightDates;
		}

		public void UpdateColumnsAccordingToggles()
		{
			if (AllowToSave)
			{
				AdNotes.LoadAdNotes();
				SetColumnsState();
			}
		}

		public void SetToggleStateAfterAdNotesChange()
		{
			ShowSquareHeader &= !ShowColumnInchesInPreview;
			ShowDeadlineHeader &= !ShowDeadlineInPreview;
			ShowDimensionsHeader &= !ShowDimensionsInPreview;
			ShowMechanicalsHeader &= !ShowMechanicalsInPreview;
			ShowDeliveryHeader &= !ShowDeliveryInPreview;
			ShowSectionHeader &= !ShowSectionInPreview;
			ShowPageSizeHeader &= !ShowPageSizeInPreview;
			ShowPercentOfPageHeader &= !ShowPercentOfPageInPreview;
			ShowPublicationHeader &= !ShowPublicationInPreview;
			ShowReadershipHeader &= !ShowReadershipInPreview;

			SetColumnsState();
			ColumnsColumns.LoadColumnsState();
		}

		private void LoadView()
		{
			ShowOptions = LocalSchedule.ViewSettings.MultiGridViewSettings.ShowOptions;
			SelectedOptionChapterIndex = LocalSchedule.ViewSettings.MultiGridViewSettings.SelectedOptionChapterIndex;

			PositionID = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IDPosition;
			PositionIndex = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IndexPosition;
			PositionDate = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DatePosition;
			PositionPCI = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PCIPosition;
			PositionCost = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.CostPosition;
			PositionFinalCost = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.FinalCostPosition;
			PositionDiscount = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DiscountPosition;
			PositionColor = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ColorPosition;
			PositionPublication = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PublicationPosition;
			PositionSquare = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SquarePosition;
			PositionPageSize = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PageSizePosition;
			PositionPercentOfPage = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PercentOfPagePosition;
			PositionDimensions = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DimensionsPosition;
			PositionMechanicals = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.MechanicalsPosition;
			PositionSection = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SectionPosition;
			PositionDelivery = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeliveryPosition;
			PositionReadership = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ReadershipPosition;
			PositionDeadline = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeadlinePosition;

			PositionColumnInchesInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionSquare;
			PositionCommentsInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionComments;
			PositionDeadlineInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionDeadline;
			PositionDeliveryInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionDelivery;
			PositionDimensionsInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionDimensions;
			PositionMechanicalsInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionMechanicals;
			PositionPageSizeInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionPageSize;
			PositionPercentOfPageInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionPercentOfPage;
			PositionPublicationInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionPublication;
			PositionReadershipInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionReadership;
			PositionSectionInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionSection;

			EnableColorHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableColor;
			EnableCostHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableCost;
			EnableDateHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableDate;
			EnableDeadlineHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableDeadline;
			EnableDeliveryHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableDelivery;
			EnableDimensionsHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableDimensions;
			EnableDiscountHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableDiscount;
			EnableFinalCostHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableFinalCost;
			EnableIDHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableID;
			EnableIndexHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableIndex;
			EnableMechanicalsHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableMechanicals;
			EnablePageSizeHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnablePageSize;
			EnablePercentOfPageHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.EnablePercentOfPage & Core.AdSchedule.ListManager.Instance.ShareUnits.Count > 0;
			EnablePCIHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnablePCI;
			EnablePublicationHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnablePublication;
			EnableReadershipHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableReadership;
			EnableSectionHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableSection;
			EnableSquareHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableSquare;
			EnableCommentsHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableAdNotes;

			_showColorHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowColor;
			_showCostHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowCost;
			_showDateHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowDate;
			_showDeadlineHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowDeadline;
			_showDeliveryHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowDelivery;
			_showDimensionsHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowDimensions;
			_showDiscountHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowDiscount;
			_showFinalCostHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowFinalCost;
			_showIDHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowID;
			_showIndexHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowIndex;
			_showMechanicalsHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowMechanicals;
			_showPageSizeHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowPageSize;
			_showPercentOfPageHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowPercentOfPage & Core.AdSchedule.ListManager.Instance.ShareUnits.Count > 0;
			_showPCIHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowPCI;
			_showPublicationHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowPublication;
			_showReadershipHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowReadership;
			_showSectionHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowSection;
			_showSquareHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowSquare;
			_showCommentsHeader = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowAdNotes;

			EnableColumnInchesInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.EnableSquare;
			EnableCommentsInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.EnableComments;
			EnableDeadlineInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.EnableDeadline;
			EnableDeliveryInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.EnableDelivery;
			EnableDimensionsInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.EnableDimensions;
			EnableMechanicalsInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.EnableMechanicals;
			EnablePageSizeInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.EnablePageSize;
			EnablePercentOfPageInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.EnablePercentOfPage;
			EnablePublicationInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.EnablePublication;
			EnableReadershipInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.EnableReadership;
			EnableSectionInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.EnableSection;

			ShowColumnInchesInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowSquare;
			ShowCommentsInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowComments;
			ShowDeadlineInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowDeadline;
			ShowDeliveryInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowDelivery;
			ShowDimensionsInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowDimensions;
			ShowMechanicalsInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowMechanicals;
			ShowPageSizeInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowPageSize;
			ShowPercentOfPageInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowPercentOfPage;
			ShowPublicationInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowPublication;
			ShowReadershipInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowReadership;
			ShowSectionInPreview = LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowSection;

			WidthID = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IDWidth;
			WidthIndex = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IndexWidth;
			WidthDate = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DateWidth;
			WidthPCI = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PCIWidth;
			WidthCost = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.CostWidth;
			WidthFinalCost = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.FinalCostWidth;
			WidthDiscount = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DiscountWidth;
			WidthColor = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ColorWidth;
			WidthPublication = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PublicationWidth;
			WidthSquare = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SquareWidth;
			WidthPageSize = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PageSizeWidth;
			WidthPercentOfPage = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PercentOfPageWidth;
			WidthDimensions = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DimensionsWidth;
			WidthMechanicals = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.MechanicalsWidth;
			WidthSection = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SectionWidth;
			WidthDelivery = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeliveryWidth;
			WidthReadership = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ReadershipWidth;
			WidthDeadline = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeadlineWidth;

			CaptionID = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IDCaption;
			CaptionIndex = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IndexCaption;
			CaptionDate = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DateCaption;
			CaptionPCI = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PCICaption;
			CaptionCost = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.CostCaption;
			CaptionFinalCost = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.FinalCostCaption;
			CaptionDiscount = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DiscountCaption;
			CaptionColor = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ColorCaption;
			CaptionPublication = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PublicationCaption;
			CaptionSquare = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SquareCaption;
			CaptionPageSize = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PageSizeCaption;
			CaptionPercentOfPage = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PercentOfPageCaption;
			CaptionDimensions = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DimensionsCaption;
			CaptionMechanicals = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.MechanicalsCaption;
			CaptionSection = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SectionCaption;
			CaptionDelivery = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeliveryCaption;
			CaptionReadership = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ReadershipCaption;
			CaptionDeadline = LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeadlineCaption;

			ColumnsColumns.LoadColumnsState();
			AdNotes.LoadAdNotes();
			SlideBullets.LoadSlideBullets();
			SlideHeader.LoadSlideHeader();

			SetPreviewState();
			SaveView();
		}

		private void SetColumnsState()
		{
			gridViewPublications.ColumnPositionChanged -= gridViewPublications_ColumnPositionChanged;
			gridColumnColorPricing.VisibleIndex = PositionColor != -1 ? PositionColor + 1 : -1;
			gridColumnColumnInches.VisibleIndex = PositionSquare != -1 ? PositionSquare + 1 : -1;
			gridColumnDate.VisibleIndex = PositionDate != -1 ? PositionDate + 1 : -1;
			gridColumnDeadline.VisibleIndex = PositionDeadline != -1 ? PositionDeadline + 1 : -1;
			gridColumnDelivery.VisibleIndex = PositionDelivery != -1 ? PositionDelivery + 1 : -1;
			gridColumnDiscountRate.VisibleIndex = PositionDiscount != -1 ? PositionDiscount + 1 : -1;
			gridColumnFinalRate.VisibleIndex = PositionFinalCost != -1 ? PositionFinalCost + 1 : -1;
			gridColumnID.VisibleIndex = PositionID != -1 ? PositionID + 1 : -1;
			gridColumnIndex.VisibleIndex = PositionIndex != -1 ? PositionIndex + 1 : -1;
			gridColumnMechanicals.VisibleIndex = PositionMechanicals != -1 ? PositionMechanicals + 1 : -1;
			gridColumnPageSize.VisibleIndex = PositionPageSize != -1 ? PositionPageSize + 1 : -1;
			gridColumnPercentOfPage.VisibleIndex = PositionPercentOfPage != -1 ? PositionPercentOfPage + 1 : -1;
			gridColumnPCIRate.VisibleIndex = PositionPCI != -1 ? PositionPCI + 1 : -1;
			gridColumnADRate.VisibleIndex = PositionCost != -1 ? PositionCost + 1 : -1;
			gridColumnPublication.VisibleIndex = PositionPublication != -1 ? PositionPublication + 1 : -1;
			gridColumnDimensions.VisibleIndex = PositionDimensions != -1 ? PositionDimensions + 1 : -1;
			gridColumnReadership.VisibleIndex = PositionReadership != -1 ? PositionReadership + 1 : -1;
			gridColumnSection.VisibleIndex = PositionSection != -1 ? PositionSection + 1 : -1;


			gridViewPublications.OptionsView.ShowPreview = _showCommentsHeader;
			gridColumnColorPricing.Visible = _showColorHeader;
			gridColumnColumnInches.Visible = _showSquareHeader;
			gridColumnDate.Visible = _showDateHeader;
			gridColumnDeadline.Visible = _showDeadlineHeader;
			gridColumnDelivery.Visible = _showDeliveryHeader;
			gridColumnDiscountRate.Visible = _showDiscountHeader;
			gridColumnFinalRate.Visible = _showFinalCostHeader;
			gridColumnID.Visible = _showIDHeader;
			gridColumnIndex.Visible = _showIndexHeader;
			gridColumnMechanicals.Visible = _showMechanicalsHeader;
			gridColumnPageSize.Visible = _showPageSizeHeader;
			gridColumnPercentOfPage.Visible = _showPercentOfPageHeader;
			gridColumnPCIRate.Visible = _showPCIHeader;
			gridColumnADRate.Visible = _showCostHeader;
			gridColumnPublication.Visible = _showPublicationHeader;
			gridColumnDimensions.Visible = _showDimensionsHeader;
			gridColumnReadership.Visible = _showReadershipHeader;
			gridColumnSection.Visible = _showSectionHeader;
			gridViewPublications.ColumnPositionChanged += gridViewPublications_ColumnPositionChanged;

			gridViewPublications.ColumnWidthChanged -= gridViewPublications_ColumnWidthChanged;
			gridColumnColorPricing.Width = WidthColor;
			gridColumnColumnInches.Width = WidthSquare;
			gridColumnDate.Width = WidthDate;
			gridColumnDeadline.Width = WidthDeadline;
			gridColumnDelivery.Width = WidthDelivery;
			gridColumnDiscountRate.Width = WidthDiscount;
			gridColumnFinalRate.Width = WidthFinalCost;
			gridColumnID.Width = WidthID;
			gridColumnIndex.Width = WidthIndex;
			gridColumnMechanicals.Width = WidthMechanicals;
			gridColumnPageSize.Width = WidthPageSize;
			gridColumnPercentOfPage.Width = WidthPercentOfPage;
			gridColumnPCIRate.Width = WidthPCI;
			gridColumnADRate.Width = WidthCost;
			gridColumnPublication.Width = WidthPublication;
			gridColumnDimensions.Width = WidthDimensions;
			gridColumnReadership.Width = WidthReadership;
			gridColumnSection.Width = WidthSection;
			gridViewPublications.ColumnWidthChanged += gridViewPublications_ColumnWidthChanged;

			gridColumnColorPricing.Caption = CaptionColor;
			gridColumnColumnInches.Caption = CaptionSquare;
			gridColumnDate.Caption = CaptionDate;
			gridColumnDeadline.Caption = CaptionDeadline;
			gridColumnDelivery.Caption = CaptionDelivery;
			gridColumnDiscountRate.Caption = CaptionDiscount;
			gridColumnFinalRate.Caption = CaptionFinalCost;
			gridColumnID.Caption = CaptionID;
			gridColumnIndex.Caption = CaptionIndex;
			gridColumnMechanicals.Caption = CaptionMechanicals;
			gridColumnPageSize.Caption = CaptionPageSize;
			gridColumnPercentOfPage.Caption = CaptionPercentOfPage;
			gridColumnPCIRate.Caption = CaptionPCI;
			gridColumnADRate.Caption = CaptionCost;
			gridColumnPublication.Caption = CaptionPublication;
			gridColumnDimensions.Caption = CaptionDimensions;
			gridColumnReadership.Caption = CaptionReadership;
			gridColumnSection.Caption = CaptionSection;

			SetPreviewState();
			gridViewPublications_ColumnPositionChanged(null, null);
		}
		#endregion

		#region Editor's Events
		private void comboBoxEditSchedule_EditValueChanged(object sender, EventArgs e)
		{
			if (AllowToSave)
			{
				LocalSchedule.ViewSettings.MultiGridViewSettings.SlideHeader = comboBoxEditSchedule.EditValue != null ? comboBoxEditSchedule.EditValue.ToString() : string.Empty;
				SettingsNotSaved = true;
			}
		}

		private void textEditHeader_Leave(object sender, EventArgs e)
		{
			_activeCol.Caption = textEditHeader.Text;

			if (_activeCol == gridColumnADRate)
				CaptionCost = _activeCol.Caption;
			else if (_activeCol == gridColumnColorPricing)
				CaptionColor = _activeCol.Caption;
			else if (_activeCol == gridColumnColumnInches)
				CaptionSquare = _activeCol.Caption;
			else if (_activeCol == gridColumnDate)
				CaptionDate = _activeCol.Caption;
			else if (_activeCol == gridColumnDeadline)
				CaptionDeadline = _activeCol.Caption;
			else if (_activeCol == gridColumnDelivery)
				CaptionDelivery = _activeCol.Caption;
			else if (_activeCol == gridColumnDimensions)
				CaptionDimensions = _activeCol.Caption;
			else if (_activeCol == gridColumnDiscountRate)
				CaptionDiscount = _activeCol.Caption;
			else if (_activeCol == gridColumnFinalRate)
				CaptionFinalCost = _activeCol.Caption;
			else if (_activeCol == gridColumnID)
				CaptionID = _activeCol.Caption;
			else if (_activeCol == gridColumnIndex)
				CaptionIndex = _activeCol.Caption;
			else if (_activeCol == gridColumnMechanicals)
				CaptionMechanicals = _activeCol.Caption;
			else if (_activeCol == gridColumnPageSize)
				CaptionPageSize = _activeCol.Caption;
			else if (_activeCol == gridColumnPercentOfPage)
				CaptionPercentOfPage = _activeCol.Caption;
			else if (_activeCol == gridColumnPCIRate)
				CaptionPCI = _activeCol.Caption;
			else if (_activeCol == gridColumnPublication)
				CaptionPublication = _activeCol.Caption;
			else if (_activeCol == gridColumnReadership)
				CaptionReadership = _activeCol.Caption;
			else if (_activeCol == gridColumnSection)
				CaptionSection = _activeCol.Caption;

			textEditHeader.Hide();

			SaveView();
		}

		private void hyperLinkEditReset_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ResetToDefault();
			e.Handled = true;
		}
		#endregion

		#region Grid Events
		private void gridViewPublications_ColumnPositionChanged(object sender, EventArgs e)
		{
			if (AllowToSave)
			{
				PositionCost = gridColumnADRate.VisibleIndex != -1 ? gridColumnADRate.VisibleIndex - 1 : -1;
				PositionColor = gridColumnColorPricing.VisibleIndex != -1 ? gridColumnColorPricing.VisibleIndex - 1 : -1;
				PositionSquare = gridColumnColumnInches.VisibleIndex != -1 ? gridColumnColumnInches.VisibleIndex - 1 : -1;
				PositionDate = gridColumnDate.VisibleIndex != -1 ? gridColumnDate.VisibleIndex - 1 : -1;
				PositionDeadline = gridColumnDeadline.VisibleIndex != -1 ? gridColumnDeadline.VisibleIndex - 1 : -1;
				PositionDelivery = gridColumnDelivery.VisibleIndex != -1 ? gridColumnDelivery.VisibleIndex - 1 : -1;
				PositionDimensions = gridColumnDimensions.VisibleIndex != -1 ? gridColumnDimensions.VisibleIndex - 1 : -1;
				PositionDiscount = gridColumnDiscountRate.VisibleIndex != -1 ? gridColumnDiscountRate.VisibleIndex - 1 : -1;
				PositionFinalCost = gridColumnFinalRate.VisibleIndex != -1 ? gridColumnFinalRate.VisibleIndex - 1 : -1;
				PositionID = gridColumnID.VisibleIndex != -1 ? gridColumnID.VisibleIndex - 1 : -1;
				PositionIndex = gridColumnIndex.VisibleIndex != -1 ? gridColumnIndex.VisibleIndex - 1 : -1;
				PositionMechanicals = gridColumnMechanicals.VisibleIndex != -1 ? gridColumnMechanicals.VisibleIndex - 1 : -1;
				PositionPageSize = gridColumnPageSize.VisibleIndex != -1 ? gridColumnPageSize.VisibleIndex - 1 : -1;
				PositionPercentOfPage = gridColumnPercentOfPage.VisibleIndex != -1 ? gridColumnPercentOfPage.VisibleIndex - 1 : -1;
				PositionPCI = gridColumnPCIRate.VisibleIndex != -1 ? gridColumnPCIRate.VisibleIndex - 1 : -1;
				PositionPublication = gridColumnPublication.VisibleIndex != -1 ? gridColumnPublication.VisibleIndex - 1 : -1;
				PositionReadership = gridColumnReadership.VisibleIndex != -1 ? gridColumnReadership.VisibleIndex - 1 : -1;
				PositionSection = gridColumnSection.VisibleIndex != -1 ? gridColumnSection.VisibleIndex - 1 : -1;
				SaveView();
			}
		}

		private void gridViewPublications_ColumnWidthChanged(object sender, ColumnEventArgs e)
		{
			if (AllowToSave)
			{
				WidthCost = gridColumnADRate.Width;
				WidthColor = gridColumnColorPricing.Width;
				WidthSquare = gridColumnColumnInches.Width;
				WidthDate = gridColumnDate.Width;
				WidthDeadline = gridColumnDeadline.Width;
				WidthDelivery = gridColumnDelivery.Width;
				WidthDimensions = gridColumnDimensions.Width;
				WidthDiscount = gridColumnDiscountRate.Width;
				WidthFinalCost = gridColumnFinalRate.Width;
				WidthID = gridColumnID.Width;
				WidthIndex = gridColumnIndex.Width;
				WidthMechanicals = gridColumnMechanicals.Width;
				WidthPageSize = gridColumnPageSize.Width;
				WidthPercentOfPage = gridColumnPercentOfPage.Width;
				WidthPCI = gridColumnPCIRate.Width;
				WidthPublication = gridColumnPublication.Width;
				WidthReadership = gridColumnReadership.Width;
				WidthSection = gridColumnSection.Width;
				SaveView();
			}
		}

		private void gridViewPublication_DoubleClick(object sender, EventArgs e)
		{
			var args = (e as DXMouseEventArgs);
			var view = sender as GridView;
			GridHitInfo hi = view.CalcHitInfo(args.Location);
			if (hi.InColumn)
			{
				var vi = view.GetViewInfo() as GridViewInfo;
				Rectangle bounds = vi.ColumnsInfo[hi.Column].Bounds;
				bounds.Width -= 10;
				bounds.Height -= 3;
				bounds.Y += 3;
				textEditHeader.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
				textEditHeader.EditValue = hi.Column.Caption;
				textEditHeader.Show();
				textEditHeader.Focus();
				_activeCol = hi.Column;
			}
			else
				Utilities.Instance.ShowWarning(" If you want to modify this Schedule Data,\ngo to the Schedules Tab and save changes...");
		}

		private void gridViewPublications_MouseUp(object sender, MouseEventArgs e)
		{
			var args = (e as DXMouseEventArgs);
			var view = sender as GridView;
			GridHitInfo hi = view.CalcHitInfo(args.Location);
			if (hi.InColumn && e.Clicks == 2)
			{
				if (hi.Column != gridColumnDate && hi.Column != gridColumnPublication)
				{
					var ViewInfo = view.GetViewInfo() as GridViewInfo;
					GridState prevState = view.State;
					if ((e.Button & MouseButtons.Left) != 0)
					{
						if (ViewInfo.ColumnsInfo[hi.Column].CaptionRect.Contains(new Point(e.X, e.Y)))
						{
							ViewInfo.SelectionInfo.ClearPressedInfo();
							args.Handled = true;
						}
					}
				}
			}
		}

		private void gridViewPublications_CalcPreviewText(object sender, CalcPreviewTextEventArgs e)
		{
			var previewText = new SortedDictionary<int, string>();
			int maxNumber = 12;
			if (ShowCommentsInPreview && !string.IsNullOrEmpty(e.PreviewText))
				previewText.Add(PositionCommentsInPreview > 0 && !previewText.Keys.Contains(PositionCommentsInPreview) ? PositionCommentsInPreview : ++maxNumber, e.PreviewText);
			if (ShowSectionInPreview && !string.IsNullOrEmpty(gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnSection).ToString()))
				previewText.Add(PositionSectionInPreview > 0 && !previewText.Keys.Contains(PositionSectionInPreview) ? PositionSectionInPreview : ++maxNumber, "Section: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnSection));
			if (ShowMechanicalsInPreview && !string.IsNullOrEmpty((string)gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnMechanicals)))
				previewText.Add(PositionMechanicalsInPreview > 0 && !previewText.Keys.Contains(PositionMechanicalsInPreview) ? PositionMechanicalsInPreview : ++maxNumber, "Mech: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnMechanicals));
			if (ShowDeliveryInPreview && !string.IsNullOrEmpty(gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnDelivery).ToString()))
				previewText.Add(PositionDeliveryInPreview > 0 && !previewText.Keys.Contains(PositionDeliveryInPreview) ? PositionDeliveryInPreview : ++maxNumber, "Delivery: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnDelivery));
			if (ShowPublicationInPreview && !string.IsNullOrEmpty(gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnPublication).ToString()))
				previewText.Add(PositionPublicationInPreview > 0 && !previewText.Keys.Contains(PositionPublicationInPreview) ? PositionPublicationInPreview : ++maxNumber, "Publication: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnPublication));
			if (ShowPageSizeInPreview && !string.IsNullOrEmpty((string)gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnPageSize)))
				previewText.Add(PositionPageSizeInPreview > 0 && !previewText.Keys.Contains(PositionPageSizeInPreview) ? PositionPageSizeInPreview : ++maxNumber, "Page Size: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnPageSize));
			if (ShowPercentOfPageInPreview && !string.IsNullOrEmpty((string)gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnPercentOfPage)))
				previewText.Add(PositionPercentOfPageInPreview > 0 && !previewText.Keys.Contains(PositionPercentOfPageInPreview) ? PositionPercentOfPageInPreview : ++maxNumber, gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnPercentOfPage) + " Share of Page");
			if (ShowDimensionsInPreview && !string.IsNullOrEmpty(gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnDimensions).ToString()))
				previewText.Add(PositionDimensionsInPreview > 0 && !previewText.Keys.Contains(PositionDimensionsInPreview) ? PositionDimensionsInPreview : ++maxNumber, "Col. x Inches: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnDimensions));
			if (ShowColumnInchesInPreview && !string.IsNullOrEmpty(gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnColumnInches).ToString()))
				previewText.Add(PositionColumnInchesInPreview > 0 && !previewText.Keys.Contains(PositionColumnInchesInPreview) ? PositionColumnInchesInPreview : ++maxNumber, "Total Col In: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnColumnInches) + " col. in.");
			if (ShowReadershipInPreview && !string.IsNullOrEmpty(gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnReadership).ToString()))
				previewText.Add(PositionReadershipInPreview > 0 && !previewText.Keys.Contains(PositionReadershipInPreview) ? PositionReadershipInPreview : ++maxNumber, "Readership: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnReadership));
			if (ShowDeadlineInPreview && !string.IsNullOrEmpty(gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnDeadline).ToString()))
				previewText.Add(PositionDeadlineInPreview > 0 && !previewText.Keys.Contains(PositionDeadlineInPreview) ? PositionDeadlineInPreview : ++maxNumber, "Deadline: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnDeadline));
			e.PreviewText = string.Join(",   ", previewText.Values.ToArray());
			if (string.IsNullOrEmpty(e.PreviewText))
				e.PreviewText = "            ";
		}

		private void gridViewPublications_MouseMove(object sender, MouseEventArgs e)
		{
			gridViewPublications.Focus();
		}
		#endregion

		#region Output Stuff
		public int OutputFileIndex
		{
			get
			{
				if (AdSchedulePowerPointHelper.Instance.Is2003)
					return _showCommentsHeader ? 1 : 2;
				else
					return _showCommentsHeader ? 3 : 4;
			}
		}

		public string Header
		{
			get
			{
				string result = string.Empty;
				if (comboBoxEditSchedule.EditValue != null && SlideHeaderState.ShowSlideHeader)
					result = comboBoxEditSchedule.EditValue.ToString();
				return result;
			}
		}


		public string PresentationDate
		{
			get
			{
				string result = string.Empty;
				if (SlideHeaderState.ShowPresentationDate)
					result = laDate.Text;
				return result;
			}
		}


		public string BusinessName
		{
			get
			{
				string result = string.Empty;
				if (SlideHeaderState.ShowAdvertiser)
					result = laBusinessName.Text.Trim();
				return result;
			}
		}

		public string DecisionMaker
		{
			get
			{
				string result = string.Empty;
				if (SlideHeaderState.ShowDecisionMaker)
					result = laDecisionMaker.Text.Trim();
				return result;
			}
		}

		public string FlightDates
		{
			get
			{
				string result = string.Empty;
				if (SlideHeaderState.ShowFlightDates)
					result = laFlightDates.Text.Trim();
				return result;
			}
		}

		public bool ShowSignatureLine
		{
			get { return SlideBulletsState.ShowSignature; }
		}

		public string[][] PublicationLogos { get; set; }

		public bool ShowAdSpecsOnlyOnLastSlide
		{
			get { return SlideBulletsState.ShowOnlyOnLastSlide; }
		}

		public bool DoNotShowAdSpecs
		{
			get { return !SlideBulletsState.ShowSlideBullets; }
		}

		public bool ShowDigitalLegend
		{
			get
			{
				return DigitalLegend.Enabled;
			}
		}

		public string DigitalLegendWebsites
		{
			get
			{
				if (!String.IsNullOrEmpty(DigitalLegend.Websites))
					return String.Format("Website: {0}", DigitalLegend.Websites);
				return String.Empty;
			}
		}

		public string DigitalLegendInfo
		{
			get
			{
				if (!String.IsNullOrEmpty(DigitalLegend.Info))
					return String.Format("Digital Product Info: {0}", DigitalLegend.Info);
				return String.Empty;
			}
		}

		public string[] AdSpecs
		{
			get
			{
				var values = new List<string>();
				if (!string.IsNullOrEmpty(SlideBullets.TotalInserts))
					values.Add(SlideBullets.TotalInserts);
				if (!string.IsNullOrEmpty(SlideBullets.PageSize))
					values.Add(SlideBullets.PageSize);
				if (!string.IsNullOrEmpty(SlideBullets.PercentOfPage))
					values.Add(SlideBullets.PercentOfPage);
				if (!string.IsNullOrEmpty(SlideBullets.Dimensions))
					values.Add(SlideBullets.Dimensions);
				if (!string.IsNullOrEmpty(SlideBullets.ColumnInches))
					values.Add(SlideBullets.ColumnInches);
				if (!string.IsNullOrEmpty(SlideBullets.AvgAdCost))
					values.Add(SlideBullets.AvgAdCost);
				if (!string.IsNullOrEmpty(SlideBullets.AvgFinalCost))
					values.Add(SlideBullets.AvgFinalCost);
				if (!string.IsNullOrEmpty(SlideBullets.AvgPCI))
					values.Add(SlideBullets.AvgPCI);
				if (!string.IsNullOrEmpty(SlideBullets.Delivery))
					values.Add(SlideBullets.Delivery);
				if (!string.IsNullOrEmpty(SlideBullets.Readership))
					values.Add(SlideBullets.Readership);
				if (!string.IsNullOrEmpty(SlideBullets.TotalColor))
					values.Add(SlideBullets.TotalColor);
				if (!string.IsNullOrEmpty(SlideBullets.Discounts))
					values.Add(SlideBullets.Discounts);
				if (!string.IsNullOrEmpty(SlideBullets.TotalFinalCost))
					values.Add(SlideBullets.TotalFinalCost);
				if (!string.IsNullOrEmpty(SlideBullets.TotalSquare))
					values.Add(SlideBullets.TotalSquare);
				return values.ToArray();
			}
		}

		public string[] GridHeaders
		{
			get
			{
				var headers = new SortedDictionary<int, string>();
				if (PositionID != -1)
					headers.Add(PositionID, CaptionID);
				if (PositionDate != -1)
					headers.Add(PositionDate, CaptionDate);
				if (PositionPCI != -1)
					headers.Add(PositionPCI, CaptionPCI);
				if (PositionCost != -1)
					headers.Add(PositionCost, CaptionCost.Replace("&&", "&"));
				if (PositionDiscount != -1)
					headers.Add(PositionDiscount, CaptionDiscount);
				if (PositionColor != -1)
					headers.Add(PositionColor, CaptionColor);
				if (PositionFinalCost != -1)
					headers.Add(PositionFinalCost, CaptionFinalCost);
				if (PositionIndex != -1)
					headers.Add(PositionIndex, CaptionIndex);
				if (PositionSquare != -1)
					headers.Add(PositionSquare, CaptionSquare);
				if (PositionPageSize != -1)
					headers.Add(PositionPageSize, CaptionPageSize);
				if (PositionPercentOfPage != -1)
					headers.Add(PositionPercentOfPage, CaptionPercentOfPage);
				if (PositionMechanicals != -1)
					headers.Add(PositionMechanicals, CaptionMechanicals);
				if (PositionPublication != -1)
					headers.Add(PositionPublication, CaptionPublication);
				if (PositionDimensions != -1)
					headers.Add(PositionDimensions, CaptionDimensions);
				if (PositionSection != -1)
					headers.Add(PositionSection, CaptionSection);
				if (PositionReadership != -1)
					headers.Add(PositionReadership, CaptionReadership);
				if (PositionDelivery != -1)
					headers.Add(PositionDelivery, CaptionDelivery);
				if (PositionDeadline != -1)
					headers.Add(PositionDeadline, CaptionDeadline);
				return headers.Values.ToArray();
			}
		}

		public int[] GridHeaderSizes
		{
			get
			{
				var sizes = new SortedDictionary<int, int>();
				if (PositionID != -1)
					sizes.Add(PositionID, WidthID);
				if (PositionDate != -1)
					sizes.Add(PositionDate, WidthDate);
				if (PositionPCI != -1)
					sizes.Add(PositionPCI, WidthPCI);
				if (PositionCost != -1)
					sizes.Add(PositionCost, WidthCost);
				if (PositionDiscount != -1)
					sizes.Add(PositionDiscount, WidthDiscount);
				if (PositionColor != -1)
					sizes.Add(PositionColor, WidthColor);
				if (PositionFinalCost != -1)
					sizes.Add(PositionFinalCost, WidthFinalCost);
				if (PositionIndex != -1)
					sizes.Add(PositionIndex, WidthIndex);
				if (PositionSquare != -1)
					sizes.Add(PositionSquare, WidthSquare);
				if (PositionPageSize != -1)
					sizes.Add(PositionPageSize, WidthPageSize);
				if (PositionPercentOfPage != -1)
					sizes.Add(PositionPercentOfPage, WidthPercentOfPage);
				if (PositionMechanicals != -1)
					sizes.Add(PositionMechanicals, WidthMechanicals);
				if (PositionPublication != -1)
					sizes.Add(PositionPublication, WidthPublication);
				if (PositionDimensions != -1)
					sizes.Add(PositionDimensions, WidthDimensions);
				if (PositionSection != -1)
					sizes.Add(PositionSection, WidthSection);
				if (PositionReadership != -1)
					sizes.Add(PositionReadership, WidthReadership);
				if (PositionDelivery != -1)
					sizes.Add(PositionDelivery, WidthDelivery);
				if (PositionDeadline != -1)
					sizes.Add(PositionDeadline, WidthDeadline);
				return sizes.Values.ToArray();
			}
		}

		public string[][][] Grid { get; private set; }

		public List<Dictionary<string, string>> OutputReplacementsLists { get; set; }

		public void PrintOutput()
		{
			using (var formGridType = new FormSelectOutput(OutputType.PowerPoint))
			{
				formGridType.buttonXGrid.Enabled = SelectedColumnsCount >= 4 && SelectedColumnsCount <= 5 && Directory.Exists(BusinessWrapper.Instance.OutputManager.MultiGridGridBasedTemlatesFolderPath);
				DialogResult gridTypeResult = formGridType.ShowDialog();
				if (gridTypeResult != DialogResult.Cancel)
				{
					bool pasteAsImage = gridTypeResult == DialogResult.Ignore;
					bool excelOutput = gridTypeResult == DialogResult.No || gridTypeResult == DialogResult.Ignore;
					using (var formProgress = new FormProgress())
					{
						formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
						formProgress.TopMost = true;
						formProgress.Show();
						PrepareOutput(excelOutput);
						if (excelOutput)
							AdSchedulePowerPointHelper.Instance.AppendMultiGridExcelBased(pasteAsImage);
						else
							AdSchedulePowerPointHelper.Instance.AppendMultiGridGridBased();
						formProgress.Close();
					}
					using (var formOutput = new FormSlideOutput())
					{
						if (formOutput.ShowDialog() != DialogResult.OK)
							Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
						else
						{
							Utilities.Instance.ActivatePowerPoint(AdSchedulePowerPointHelper.Instance.PowerPointObject);
							Utilities.Instance.ActivateMiniBar();
						}
					}
				}
			}
		}

		public void Email()
		{
			using (var formGridType = new FormSelectOutput(OutputType.Email))
			{
				formGridType.buttonXGrid.Enabled = SelectedColumnsCount >= 4 && SelectedColumnsCount <= 5 && Directory.Exists(BusinessWrapper.Instance.OutputManager.MultiGridGridBasedTemlatesFolderPath);
				DialogResult gridTypeResult = formGridType.ShowDialog();
				if (gridTypeResult != DialogResult.Cancel)
				{
					bool pasteAsImage = gridTypeResult == DialogResult.Ignore;
					bool excelOutput = gridTypeResult == DialogResult.No || gridTypeResult == DialogResult.Ignore;
					using (var formProgress = new FormProgress())
					{
						formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
						formProgress.TopMost = true;
						formProgress.Show();
						PrepareOutput(excelOutput);
						string tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
						if (excelOutput)
							AdSchedulePowerPointHelper.Instance.PrepareMultiGridExcelBasedEmail(tempFileName, pasteAsImage);
						else
							AdSchedulePowerPointHelper.Instance.PrepareMultiGridGridBasedEmail(tempFileName);
						Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
						formProgress.Close();
						if (File.Exists(tempFileName))
							using (var formEmail = new FormEmail())
							{
								formEmail.Text = "Email this Logo Grid";
								formEmail.PresentationFile = tempFileName;
								RegistryHelper.MainFormHandle = formEmail.Handle;
								RegistryHelper.MaximizeMainForm = false;
								formEmail.ShowDialog();
								RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
								RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
							}
					}
				}
			}
		}

		public void Preview()
		{
			using (var formGridType = new FormSelectOutput(OutputType.Preview))
			{
				formGridType.buttonXGrid.Enabled = SelectedColumnsCount >= 4 && SelectedColumnsCount <= 5 && Directory.Exists(BusinessWrapper.Instance.OutputManager.MultiGridGridBasedTemlatesFolderPath);
				DialogResult gridTypeResult = formGridType.ShowDialog();
				if (gridTypeResult != DialogResult.Cancel)
				{
					bool pasteAsImage = gridTypeResult == DialogResult.Ignore;
					bool excelOutput = gridTypeResult == DialogResult.No || gridTypeResult == DialogResult.Ignore;
					using (var formProgress = new FormProgress())
					{
						formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
						formProgress.TopMost = true;
						formProgress.Show();
						PrepareOutput(excelOutput);
						string tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
						if (excelOutput)
							AdSchedulePowerPointHelper.Instance.PrepareMultiGridExcelBasedEmail(tempFileName, pasteAsImage);
						else
							AdSchedulePowerPointHelper.Instance.PrepareMultiGridGridBasedEmail(tempFileName);
						Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
						formProgress.Close();
						if (File.Exists(tempFileName))
							using (var formPreview = new FormPreview())
							{
								formPreview.Text = "Preview Logo Grid";
								formPreview.PresentationFile = tempFileName;
								RegistryHelper.MainFormHandle = formPreview.Handle;
								RegistryHelper.MaximizeMainForm = false;
								DialogResult previewResult = formPreview.ShowDialog();
								RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
								RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
								if (previewResult != DialogResult.OK)
									Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
								else
								{
									Utilities.Instance.ActivatePowerPoint(AdSchedulePowerPointHelper.Instance.PowerPointObject);
									Utilities.Instance.ActivateMiniBar();
								}
							}
					}
				}
			}
		}

		private string[][] GetPublicationLogos(bool excelOutput)
		{
			var logos = new List<string[]>();
			var logosOnSlide = new List<string>();
			int rowCountPerSlide = _showCommentsHeader ? (excelOutput ? OutputManager.MultiGridExcelBasedRowsCountWithNotes : OutputManager.MultiGridGridBasedRowsCountWithNotes) : (excelOutput ? OutputManager.MultiGridExcelBasedRowsCountWithoutNotes : OutputManager.MultiGridGridBasedRowsCountWithoutNotes);
			int insertsCount = _inserts.Count;
			for (int i = 0; i < insertsCount; i += rowCountPerSlide)
			{
				logosOnSlide.Clear();
				for (int j = 0; j < rowCountPerSlide; j++)
				{
					string fileName = string.Empty;
					if ((i + j) < insertsCount)
					{
						int k = gridViewPublications.GetDataSourceRowIndex(i + j);
						if (_inserts[k].MultiGridLogo != null)
						{
							fileName = Path.GetTempFileName();
							_inserts[k].MultiGridLogo.Save(fileName);
						}
					}
					logosOnSlide.Add(fileName);
				}
				logos.Add(logosOnSlide.ToArray());
			}
			return logos.ToArray();
		}

		private string[][][] GetGrid(bool excelOutput)
		{
			var result = new List<string[][]>();
			var slide = new List<string[]>();
			var row = new SortedDictionary<int, string>();
			var adNotes = new SortedDictionary<int, string>();

			var rowCountPerSlide = _showCommentsHeader ? (excelOutput ? OutputManager.MultiGridExcelBasedRowsCountWithNotes : OutputManager.MultiGridGridBasedRowsCountWithNotes) : (excelOutput ? OutputManager.MultiGridExcelBasedRowsCountWithoutNotes : OutputManager.MultiGridGridBasedRowsCountWithoutNotes);
			var insertsCount = _inserts.Count;
			var totalRowCount = insertsCount;
			if (ShowDigitalLegend && !String.IsNullOrEmpty(DigitalLegendWebsites))
				totalRowCount++;
			if (ShowDigitalLegend && !String.IsNullOrEmpty(DigitalLegendInfo))
				totalRowCount++;
			for (int i = 0; i < totalRowCount; i += rowCountPerSlide)
			{
				slide.Clear();
				for (int j = 0; j < rowCountPerSlide; j++)
				{
					row.Clear();
					if ((i + j) < insertsCount)
					{
						int k = gridViewPublications.GetDataSourceRowIndex(i + j);
						if (_showCommentsHeader)
						{
							adNotes.Clear();
							int maxNumber = 12;
							if (ShowCommentsInPreview && !string.IsNullOrEmpty(_inserts[k].FullComment))
								adNotes.Add(PositionCommentsInPreview > 0 && !adNotes.Keys.Contains(PositionCommentsInPreview) ? PositionCommentsInPreview : ++maxNumber, _inserts[k].FullComment);
							if (ShowSectionInPreview && !string.IsNullOrEmpty(_inserts[k].FullSection))
								adNotes.Add(PositionSectionInPreview > 0 && !adNotes.Keys.Contains(PositionSectionInPreview) ? PositionSectionInPreview : ++maxNumber, "Section: " + _inserts[k].FullSection);
							if (ShowMechanicalsInPreview && !string.IsNullOrEmpty(_inserts[k].Mechanicals))
								adNotes.Add(PositionMechanicalsInPreview > 0 && !adNotes.Keys.Contains(PositionMechanicalsInPreview) ? PositionMechanicalsInPreview : ++maxNumber, "Mech: " + _inserts[k].MechanicalsOutput);
							if (ShowDeliveryInPreview && !string.IsNullOrEmpty(_inserts[k].Delivery))
								adNotes.Add(PositionDeliveryInPreview > 0 && !adNotes.Keys.Contains(PositionDeliveryInPreview) ? PositionDeliveryInPreview : ++maxNumber, "Delivery: " + _inserts[k].Delivery);
							if (ShowPublicationInPreview && !string.IsNullOrEmpty(_inserts[k].Publication))
								adNotes.Add(PositionPublicationInPreview > 0 && !adNotes.Keys.Contains(PositionPublicationInPreview) ? PositionPublicationInPreview : ++maxNumber, "Publication: " + _inserts[k].Publication);
							if (ShowPageSizeInPreview && !string.IsNullOrEmpty(_inserts[k].PageSize))
								adNotes.Add(PositionPageSizeInPreview > 0 && !adNotes.Keys.Contains(PositionPageSizeInPreview) ? PositionPageSizeInPreview : ++maxNumber, "Page Size: " + _inserts[k].PageSizeOutput);
							if (ShowPercentOfPageInPreview && !string.IsNullOrEmpty(_inserts[k].PercentOfPage))
								adNotes.Add(PositionPercentOfPageInPreview > 0 && !adNotes.Keys.Contains(PositionPercentOfPageInPreview) ? PositionPercentOfPageInPreview : ++maxNumber, "% of Page: " + _inserts[k].PercentOfPageOutput);
							if (ShowDimensionsInPreview && !string.IsNullOrEmpty(_inserts[k].Dimensions))
								adNotes.Add(PositionDimensionsInPreview > 0 && !adNotes.Keys.Contains(PositionDimensionsInPreview) ? PositionDimensionsInPreview : ++maxNumber, "Col. x Inches: " + _inserts[k].Dimensions);
							if (ShowColumnInchesInPreview && !string.IsNullOrEmpty(_inserts[k].SquareStringFormatted))
								adNotes.Add(PositionColumnInchesInPreview > 0 && !adNotes.Keys.Contains(PositionColumnInchesInPreview) ? PositionColumnInchesInPreview : ++maxNumber, "Total Col In: " + _inserts[k].SquareStringFormatted + " col. in.");
							if (ShowReadershipInPreview && !string.IsNullOrEmpty(_inserts[k].Readership))
								adNotes.Add(PositionReadershipInPreview > 0 && !adNotes.Keys.Contains(PositionReadershipInPreview) ? PositionReadershipInPreview : ++maxNumber, "Readership: " + _inserts[k].Readership);
							if (ShowDeadlineInPreview && !string.IsNullOrEmpty(_inserts[k].Deadline))
								adNotes.Add(PositionDeadlineInPreview > 0 && !adNotes.Keys.Contains(PositionDeadlineInPreview) ? PositionDeadlineInPreview : ++maxNumber, "Deadline: " + _inserts[k].DeadlineForOutput);
							if (adNotes.Count > 0)
								row.Add(-1, string.Join(",   ", adNotes.Values.ToArray()));
							else
								row.Add(-1, "            ");
						}
						if (PositionID != -1)
							row.Add(PositionID, _inserts[k].ID);
						if (PositionDate != -1)
							row.Add(PositionDate, _inserts[k].Date.ToString("MM/dd/yy"));
						if (PositionPCI != -1)
							row.Add(PositionPCI, _inserts[k].PCIRate.HasValue ? (_inserts[k].PCIRate.Value.ToString("$#,###.00")) : "N/A");
						if (PositionCost != -1)
							row.Add(PositionCost, _inserts[k].ADRate.ToString("$#,###.00"));
						if (PositionDiscount != -1)
							row.Add(PositionDiscount, _inserts[k].DiscountRate.ToString("$#,###.00"));
						if (PositionColor != -1)
							row.Add(PositionColor, _inserts[k].ColorPricingCalculated > 0 ? _inserts[k].ColorPricingCalculated.ToString("$#,###.00") : _inserts[k].ColorPricingObject.ToString());
						if (PositionFinalCost != -1)
							row.Add(PositionFinalCost, _inserts[k].FinalRate.ToString("$#,###.00"));
						if (PositionIndex != -1)
							row.Add(PositionIndex, _inserts[k].Index.ToString("#,##0"));
						if (PositionSquare != -1)
							row.Add(PositionSquare, "'" + _inserts[k].SquareStringFormatted);
						if (PositionPageSize != -1)
							row.Add(PositionPageSize, _inserts[k].PageSizeOutput);
						if (PositionPercentOfPage != -1)
							row.Add(PositionPercentOfPage, _inserts[k].PercentOfPageOutput);
						if (PositionMechanicals != -1)
							row.Add(PositionMechanicals, _inserts[k].MechanicalsOutput);
						if (PositionPublication != -1)
							row.Add(PositionPublication, _inserts[k].Publication);
						if (PositionDimensions != -1)
							row.Add(PositionDimensions, _inserts[k].DimensionsOutput);
						if (PositionSection != -1)
							row.Add(PositionSection, _inserts[k].FullSection);
						if (PositionReadership != -1)
							row.Add(PositionReadership, _inserts[k].Readership);
						if (PositionDelivery != -1)
							row.Add(PositionDelivery, _inserts[k].Delivery);
						if (PositionDeadline != -1)
							row.Add(PositionDeadline, _inserts[k].DeadlineForOutput);
						if (row.Values.Count > 0)
							slide.Add(row.Values.ToArray());
					}
				}
				if (i >= (totalRowCount - rowCountPerSlide))
				{
					if (ShowDigitalLegend && !String.IsNullOrEmpty(DigitalLegendWebsites))
						slide.Add(new[] { DigitalLegendWebsites });
					if (ShowDigitalLegend && !String.IsNullOrEmpty(DigitalLegendInfo))
						slide.Add(new[] { DigitalLegendInfo });
				}
				result.Add(slide.ToArray());
			}
			return result.ToArray();
		}

		public void PopulateReplacementsList()
		{
			string key = string.Empty;
			string value = string.Empty;
			var temp = new List<string>();
			OutputReplacementsLists = new List<Dictionary<string, string>>();
			foreach (var slideGrid in Grid)
			{
				var slideReplacementList = new Dictionary<string, string>();

				string[] gridHeaders = GridHeaders;
				for (int i = 0; i < gridHeaders.Length; i++)
				{
					key = string.Format("Header{0}", (i + 1).ToString());
					value = gridHeaders[i];
					if (!slideReplacementList.Keys.Contains(key))
						slideReplacementList.Add(key, value);
				}

				for (int i = 0; i < slideGrid.Length; i++)
				{
					var rowItemsNumber = slideGrid[i].Length;
					if (rowItemsNumber > 1)
					{
						for (int j = 0; j < rowItemsNumber; j++)
						{
							string columnPrefix = "a";
							if (ShowCommentsHeader)
							{
								switch (j)
								{
									case 0:
										columnPrefix = "g";
										break;
									case 1:
										columnPrefix = "a";
										break;
									case 2:
										columnPrefix = "b";
										break;
									case 3:
										columnPrefix = "c";
										break;
									case 4:
										columnPrefix = "d";
										break;
									case 5:
										columnPrefix = "e";
										break;
									case 6:
										columnPrefix = "f";
										break;
								}
							}
							else
							{
								switch (j)
								{
									case 0:
										columnPrefix = "a";
										break;
									case 1:
										columnPrefix = "b";
										break;
									case 2:
										columnPrefix = "c";
										break;
									case 3:
										columnPrefix = "d";
										break;
									case 4:
										columnPrefix = "e";
										break;
									case 5:
										columnPrefix = "f";
										break;
								}
							}
							key = string.Format("{0}{1}", new object[] { (i + 1).ToString(), columnPrefix });
							value = slideGrid[i][j];
							if (!slideReplacementList.Keys.Contains(key))
								slideReplacementList.Add(key, value);
						}
					}
					else
					{
						key = string.Format("{0}{1}", (i + 1).ToString(), "a");
						value = slideGrid[i][0];
						if (!slideReplacementList.Keys.Contains(key))
							slideReplacementList.Add(key, value);
						key = string.Format("{0}{1}", (i + 1).ToString(), "b");
						value = "Merge";
						if (!slideReplacementList.Keys.Contains(key))
							slideReplacementList.Add(key, value);
						key = string.Format("{0}{1}", (i + 1).ToString(), "c");
						if (!slideReplacementList.Keys.Contains(key))
							slideReplacementList.Add(key, value);
						key = string.Format("{0}{1}", (i + 1).ToString(), "d");
						if (!slideReplacementList.Keys.Contains(key))
							slideReplacementList.Add(key, value);
						key = string.Format("{0}{1}", (i + 1).ToString(), "e");
						if (!slideReplacementList.Keys.Contains(key))
							slideReplacementList.Add(key, value);
						key = string.Format("{0}{1}", (i + 1).ToString(), "f");
						if (!slideReplacementList.Keys.Contains(key))
							slideReplacementList.Add(key, value);
						value = "MergeComment";
						key = string.Format("{0}{1}", (i + 1).ToString(), "g");
						if (!slideReplacementList.Keys.Contains(key))
							slideReplacementList.Add(key, value);
					}
				}
				OutputReplacementsLists.Add(slideReplacementList);
			}
		}

		public void PrepareOutput(bool excelOutput)
		{
			Grid = GetGrid(excelOutput);
			PublicationLogos = GetPublicationLogos(excelOutput);
			if (!excelOutput)
				PopulateReplacementsList();
		}
		#endregion

		#region IGridOutputControl Members
		public Schedule LocalSchedule { get; set; }
		public ColumnsControl ColumnsColumns { get; private set; }
		public AdNotesControl AdNotes { get; private set; }
		public SlideBulletsControl SlideBullets { get; private set; }
		public SlideHeaderControl SlideHeader { get; private set; }

		public bool AllowToSave { get; set; }
		public bool SettingsNotSaved { get; set; }

		public SuperTooltipInfo HelpToolTip { get; private set; }

		public bool ShowOptions { get; set; }
		public int SelectedOptionChapterIndex { get; set; }

		public int SelectedColumnsCount
		{
			get
			{
				int count = 0;
				if (ShowColorHeader)
					count++;
				if (ShowCostHeader)
					count++;
				if (ShowDateHeader)
					count++;
				if (ShowDeadlineHeader)
					count++;
				if (ShowDeliveryHeader)
					count++;
				if (ShowDiscountHeader)
					count++;
				if (ShowFinalCostHeader)
					count++;
				if (ShowIDHeader)
					count++;
				if (ShowIndexHeader)
					count++;
				if (ShowMechanicalsHeader)
					count++;
				if (ShowPageSizeHeader)
					count++;
				if (ShowPercentOfPageHeader)
					count++;
				if (ShowDimensionsHeader)
					count++;
				if (ShowPCIHeader)
					count++;
				if (ShowPublicationHeader)
					count++;
				if (ShowReadershipHeader)
					count++;
				if (ShowSectionHeader)
					count++;
				if (ShowSquareHeader)
					count++;
				return count;
			}
		}
		#endregion

		#region Column Positions
		public int PositionID { get; set; }
		public int PositionIndex { get; set; }
		public int PositionDate { get; set; }
		public int PositionPCI { get; set; }
		public int PositionCost { get; set; }
		public int PositionFinalCost { get; set; }
		public int PositionDiscount { get; set; }
		public int PositionColor { get; set; }
		public int PositionPublication { get; set; }
		public int PositionSquare { get; set; }
		public int PositionPageSize { get; set; }
		public int PositionPercentOfPage { get; set; }
		public int PositionDimensions { get; set; }
		public int PositionMechanicals { get; set; }
		public int PositionSection { get; set; }
		public int PositionDelivery { get; set; }
		public int PositionReadership { get; set; }
		public int PositionDeadline { get; set; }
		#endregion

		#region Column Widths
		public int WidthID { get; set; }
		public int WidthIndex { get; set; }
		public int WidthDate { get; set; }
		public int WidthPCI { get; set; }
		public int WidthCost { get; set; }
		public int WidthFinalCost { get; set; }
		public int WidthDiscount { get; set; }
		public int WidthColor { get; set; }
		public int WidthPublication { get; set; }
		public int WidthSquare { get; set; }
		public int WidthPageSize { get; set; }
		public int WidthPercentOfPage { get; set; }
		public int WidthDimensions { get; set; }
		public int WidthMechanicals { get; set; }
		public int WidthSection { get; set; }
		public int WidthDelivery { get; set; }
		public int WidthReadership { get; set; }
		public int WidthDeadline { get; set; }
		#endregion

		#region Column Captions
		public string CaptionID { get; set; }
		public string CaptionIndex { get; set; }
		public string CaptionDate { get; set; }
		public string CaptionPCI { get; set; }
		public string CaptionCost { get; set; }
		public string CaptionFinalCost { get; set; }
		public string CaptionDiscount { get; set; }
		public string CaptionColor { get; set; }
		public string CaptionPublication { get; set; }
		public string CaptionSquare { get; set; }
		public string CaptionPageSize { get; set; }
		public string CaptionPercentOfPage { get; set; }
		public string CaptionDimensions { get; set; }
		public string CaptionMechanicals { get; set; }
		public string CaptionSection { get; set; }
		public string CaptionDelivery { get; set; }
		public string CaptionReadership { get; set; }
		public string CaptionDeadline { get; set; }
		#endregion

		#region Enable Columns
		public bool EnableIDHeader { get; set; }
		public bool EnableIndexHeader { get; set; }
		public bool EnableDateHeader { get; set; }
		public bool EnableColorHeader { get; set; }
		public bool EnableSectionHeader { get; set; }
		public bool EnablePCIHeader { get; set; }
		public bool EnableFinalCostHeader { get; set; }
		public bool EnablePublicationHeader { get; set; }
		public bool EnablePercentOfPageHeader { get; set; }
		public bool EnableCostHeader { get; set; }
		public bool EnableDimensionsHeader { get; set; }
		public bool EnableMechanicalsHeader { get; set; }
		public bool EnableDeliveryHeader { get; set; }
		public bool EnableDiscountHeader { get; set; }
		public bool EnablePageSizeHeader { get; set; }
		public bool EnableSquareHeader { get; set; }
		public bool EnableDeadlineHeader { get; set; }
		public bool EnableReadershipHeader { get; set; }
		public bool EnableCommentsHeader { get; set; }
		#endregion

		#region Show Columns
		private bool _showColorHeader = true;
		private bool _showCommentsHeader;
		private bool _showCostHeader = true;
		private bool _showDateHeader = true;
		private bool _showDeadlineHeader;
		private bool _showDeliveryHeader;
		private bool _showDimensionsHeader;
		private bool _showDiscountHeader = true;
		private bool _showFinalCostHeader = true;
		private bool _showIDHeader = true;
		private bool _showIndexHeader = true;
		private bool _showMechanicalsHeader;
		private bool _showPCIHeader = true;
		private bool _showPageSizeHeader;
		private bool _showPercentOfPageHeader;
		private bool _showPublicationHeader;
		private bool _showReadershipHeader;
		private bool _showSectionHeader;
		private bool _showSquareHeader;

		public bool ShowIDHeader
		{
			get { return _showIDHeader; }
			set { _showIDHeader = value; }
		}
		public bool ShowDateHeader
		{
			get { return _showDateHeader; }
			set { _showDateHeader = value; }
		}
		public bool ShowPCIHeader
		{
			get { return _showPCIHeader; }
			set { _showPCIHeader = value; }
		}
		public bool ShowCostHeader
		{
			get { return _showCostHeader; }
			set { _showCostHeader = value; }
		}
		public bool ShowDiscountHeader
		{
			get { return _showDiscountHeader; }
			set { _showDiscountHeader = value; }
		}
		public bool ShowColorHeader
		{
			get { return _showColorHeader; }
			set { _showColorHeader = value; }
		}
		public bool ShowFinalCostHeader
		{
			get { return _showFinalCostHeader; }
			set { _showFinalCostHeader = value; }
		}
		public bool ShowIndexHeader
		{
			get { return _showIndexHeader; }
			set { _showIndexHeader = value; }
		}
		public bool ShowCommentsHeader
		{
			get { return _showCommentsHeader; }
			set { _showCommentsHeader = value; }
		}
		public bool ShowSquareHeader
		{
			get { return _showSquareHeader; }
			set
			{
				_showSquareHeader = value;
				ShowColumnInchesInPreview &= !value;
			}
		}
		public bool ShowPageSizeHeader
		{
			get { return _showPageSizeHeader; }
			set
			{
				_showPageSizeHeader = value;
				ShowPageSizeInPreview &= !value;
			}
		}
		public bool ShowPercentOfPageHeader
		{
			get { return _showPercentOfPageHeader; }
			set
			{
				_showPercentOfPageHeader = value;
				ShowPercentOfPageInPreview &= !value;
			}
		}
		public bool ShowMechanicalsHeader
		{
			get { return _showMechanicalsHeader; }
			set
			{
				_showMechanicalsHeader = value;
				ShowMechanicalsInPreview &= !value;
			}
		}
		public bool ShowPublicationHeader
		{
			get { return _showPublicationHeader; }
			set
			{
				_showPublicationHeader = value;
				ShowPublicationInPreview &= !value;
			}
		}
		public bool ShowDimensionsHeader
		{
			get { return _showDimensionsHeader; }
			set
			{
				_showDimensionsHeader = value;
				ShowDimensionsInPreview &= !value;
			}
		}
		public bool ShowSectionHeader
		{
			get { return _showSectionHeader; }
			set
			{
				_showSectionHeader = value;
				ShowSectionInPreview &= !value;
			}
		}
		public bool ShowReadershipHeader
		{
			get { return _showReadershipHeader; }
			set
			{
				_showReadershipHeader = value;
				ShowReadershipInPreview &= !value;
			}
		}
		public bool ShowDeliveryHeader
		{
			get { return _showDeliveryHeader; }
			set
			{
				_showDeliveryHeader = value;
				ShowDeliveryInPreview &= !value;
			}
		}
		public bool ShowDeadlineHeader
		{
			get { return _showDeadlineHeader; }
			set
			{
				_showDeadlineHeader = value;
				ShowDeadlineInPreview &= !value;
			}
		}
		#endregion

		#region Enable AdNotes
		public bool EnableCommentsInPreview { get; set; }
		public bool EnableSectionInPreview { get; set; }
		public bool EnableMechanicalsInPreview { get; set; }
		public bool EnableColumnInchesInPreview { get; set; }
		public bool EnablePublicationInPreview { get; set; }
		public bool EnablePageSizeInPreview { get; set; }
		public bool EnablePercentOfPageInPreview { get; set; }
		public bool EnableDimensionsInPreview { get; set; }
		public bool EnableReadershipInPreview { get; set; }
		public bool EnableDeliveryInPreview { get; set; }
		public bool EnableDeadlineInPreview { get; set; }
		#endregion

		#region Show AdNotes
		public bool ShowCommentsInPreview { get; set; }
		public bool ShowSectionInPreview { get; set; }
		public bool ShowMechanicalsInPreview { get; set; }
		public bool ShowColumnInchesInPreview { get; set; }
		public bool ShowPublicationInPreview { get; set; }
		public bool ShowPageSizeInPreview { get; set; }
		public bool ShowPercentOfPageInPreview { get; set; }
		public bool ShowDimensionsInPreview { get; set; }
		public bool ShowReadershipInPreview { get; set; }
		public bool ShowDeliveryInPreview { get; set; }
		public bool ShowDeadlineInPreview { get; set; }
		#endregion

		#region Position AdNotes
		public int PositionCommentsInPreview { get; set; }
		public int PositionSectionInPreview { get; set; }
		public int PositionMechanicalsInPreview { get; set; }
		public int PositionColumnInchesInPreview { get; set; }
		public int PositionPublicationInPreview { get; set; }
		public int PositionPageSizeInPreview { get; set; }
		public int PositionPercentOfPageInPreview { get; set; }
		public int PositionDimensionsInPreview { get; set; }
		public int PositionReadershipInPreview { get; set; }
		public int PositionDeliveryInPreview { get; set; }
		public int PositionDeadlineInPreview { get; set; }
		#endregion
	}
}