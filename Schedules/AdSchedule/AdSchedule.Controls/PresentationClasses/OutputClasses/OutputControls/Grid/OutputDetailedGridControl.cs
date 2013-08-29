﻿using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.InteropClasses;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputForms;
using NewBizWiz.AdSchedule.Controls.Properties;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;
using ListManager = NewBizWiz.Core.AdSchedule.ListManager;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	[ToolboxItem(false)]
	public partial class OutputDetailedGridControl : UserControl, IGridOutputControl
	{
		private readonly List<PublicationDetailedGridControl> _tabPages = new List<PublicationDetailedGridControl>();

		public OutputDetailedGridControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			ColumnsColumns = new ColumnsControl(this);
			ColumnsColumns.OnHelp += (o, e) => BusinessWrapper.Instance.HelpManager.OpenHelpLink("detailednavbar1");

			AdNotes = new AdNotesControl(this);
			AdNotes.OnHelp += (o, e) => BusinessWrapper.Instance.HelpManager.OpenHelpLink("detailednavbar2");

			SlideBullets = new SlideBulletsControl(this);
			SlideBullets.OnHelp += (o, e) => BusinessWrapper.Instance.HelpManager.OpenHelpLink("detailednavbar4");

			SlideHeader = new SlideHeaderControl(this);
			SlideHeader.OnHelp += (o, e) => BusinessWrapper.Instance.HelpManager.OpenHelpLink("detailednavbar3");

			SlideHeader.checkEditLogo1.Text = "Publication Logo";
			SlideHeader.checkEditLogo2.Visible = false;
			SlideHeader.checkEditLogo3.Visible = false;
			SlideHeader.checkEditLogo4.Visible = false;

			HelpToolTip = new SuperTooltipInfo("HELP", "", "Help me understand how to use the Detailed Grid", null, null, eTooltipColor.Gray);
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate()
			{
				if (sender != this)
					UpdateOutput(e.QuickSave);
			});
		}

		#region IGridOutputControl Members
		public SlideBulletsState SlideBulletsState
		{
			get { return LocalSchedule.ViewSettings.DetailedGridViewSettings.SlideBulletsState; }
		}

		public SlideHeaderState SlideHeaderState
		{
			get { return LocalSchedule.ViewSettings.DetailedGridViewSettings.SlideHeaderState; }
		}

		public DigitalLegend DigitalLegend
		{
			get { return LocalSchedule.ViewSettings.DetailedGridViewSettings.DigitalLegend; }
		}

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

		public void SaveView()
		{
			if (AllowToSave)
			{
				LocalSchedule.ViewSettings.DetailedGridViewSettings.ShowOptions = ShowOptions;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.SelectedOptionChapterIndex = SelectedOptionChapterIndex;

				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IDPosition = PositionID;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IndexPosition = PositionIndex;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DatePosition = PositionDate;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PCIPosition = PositionPCI;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.CostPosition = PositionCost;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.FinalCostPosition = PositionFinalCost;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DiscountPosition = PositionDiscount;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ColorPosition = PositionColor;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PublicationPosition = PositionPublication;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SquarePosition = PositionSquare;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PageSizePosition = PositionPageSize;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PercentOfPagePosition = PositionPercentOfPage;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DimensionsPosition = PositionDimensions;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.MechanicalsPosition = PositionMechanicals;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SectionPosition = PositionSection;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeliveryPosition = PositionDelivery;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ReadershipPosition = PositionReadership;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeadlinePosition = PositionDeadline;

				LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionSquare = PositionColumnInchesInPreview;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionComments = PositionCommentsInPreview;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionDeadline = PositionDeadlineInPreview;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionDelivery = PositionDeliveryInPreview;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionDimensions = PositionDimensionsInPreview;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionMechanicals = PositionMechanicalsInPreview;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionPageSize = PositionPageSizeInPreview;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionPercentOfPage = PositionPercentOfPageInPreview;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionPublication = PositionPublicationInPreview;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionReadership = PositionReadershipInPreview;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionSection = PositionSectionInPreview;

				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowColor = _showColorHeader;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowAdNotes = _showCommentsHeader;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowCost = _showCostHeader;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowDate = _showDateHeader;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowDeadline = _showDeadlineHeader;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowDelivery = _showDeliveryHeader;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowDimensions = _showDimensionsHeader;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowDiscount = _showDiscountHeader;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowFinalCost = _showFinalCostHeader;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowID = _showIDHeader;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowIndex = _showIndexHeader;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowMechanicals = _showMechanicalsHeader;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowPageSize = _showPageSizeHeader;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowPercentOfPage = _showPercentOfPageHeader;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowPCI = _showPCIHeader;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowPublication = _showPublicationHeader;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowReadership = _showReadershipHeader;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowSection = _showSectionHeader;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowSquare = _showSquareHeader;

				LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowSquare = ShowColumnInchesInPreview;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowComments = ShowCommentsInPreview;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowDeadline = ShowDeadlineInPreview;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowDelivery = ShowDeliveryInPreview;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowDimensions = ShowDimensionsInPreview;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowMechanicals = ShowMechanicalsInPreview;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowPageSize = ShowPageSizeInPreview;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowPercentOfPage = ShowPercentOfPageInPreview;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowPublication = ShowPublicationInPreview;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowReadership = ShowReadershipInPreview;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowSection = ShowSectionInPreview;

				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IDWidth = WidthID;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IndexWidth = WidthIndex;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DateWidth = WidthDate;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PCIWidth = WidthPCI;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.CostWidth = WidthCost;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.FinalCostWidth = WidthFinalCost;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DiscountWidth = WidthDiscount;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ColorWidth = WidthColor;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PublicationWidth = WidthPublication;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SquareWidth = WidthSquare;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PageSizeWidth = WidthPageSize;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PercentOfPageWidth = WidthPercentOfPage;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DimensionsWidth = WidthDimensions;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.MechanicalsWidth = WidthMechanicals;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SectionWidth = WidthSection;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeliveryWidth = WidthDelivery;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ReadershipWidth = WidthReadership;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeadlineWidth = WidthDeadline;

				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IDCaption = CaptionID;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IndexCaption = CaptionIndex;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DateCaption = CaptionDate;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PCICaption = CaptionPCI;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.CostCaption = CaptionCost;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.FinalCostCaption = CaptionFinalCost;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DiscountCaption = CaptionDiscount;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ColorCaption = CaptionColor;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PublicationCaption = CaptionPublication;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SquareCaption = CaptionSquare;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PageSizeCaption = CaptionPageSize;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PercentOfPageCaption = CaptionPercentOfPage;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DimensionsCaption = CaptionDimensions;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.MechanicalsCaption = CaptionMechanicals;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SectionCaption = CaptionSection;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeliveryCaption = CaptionDelivery;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ReadershipCaption = CaptionReadership;
				LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeadlineCaption = CaptionDeadline;

				SettingsNotSaved = true;
			}
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

		public void SetPreviewState()
		{
			foreach (PublicationDetailedGridControl publicationControl in _tabPages)
			{
				publicationControl.gridViewPublications.OptionsView.ShowPreview = _showCommentsHeader;
				publicationControl.gridViewPublications.RefreshData();
			}
		}

		public void SetSlideHeader()
		{
			foreach (PublicationDetailedGridControl publicationControl in _tabPages)
			{
				publicationControl.SetSlideHeader();
			}
		}

		public void UpdateOutput(bool quickLoad)
		{
			LocalSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
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
						PublicationDetailedGridControl publicationTab = _tabPages.Where(x => x.PrintProduct.UniqueID.Equals(publication.UniqueID)).FirstOrDefault();
						if (publicationTab == null)
						{
							publicationTab = new PublicationDetailedGridControl();
							_tabPages.Add(publicationTab);
							Application.DoEvents();
						}
						publicationTab.PrintProduct = publication;
						publicationTab.PageEnabled = publication.Inserts.Count > 0;
						publicationTab.LoadPublication();
						Application.DoEvents();
					}
				}
				_tabPages.Sort((x, y) => x.PrintProduct.Index.CompareTo(y.PrintProduct.Index));
				xtraTabControlPublications.TabPages.AddRange(_tabPages.ToArray());

				LoadView();

				AllowToSave = false;
				SetColumnsState();
				UpdateSlideBullets();
				AllowToSave = true;
				Application.DoEvents();
				xtraTabControlPublications.SelectedPageChanged += xtraTabControlPublications_SelectedPageChanged;
				;
				xtraTabControlPublications.ResumeLayout();
			}
			SettingsNotSaved = false;
		}

		private void ResetToDefault()
		{
			LocalSchedule.ViewSettings.DetailedGridViewSettings.ResetToDefault();
			LoadView();
			AllowToSave = false;
			SetColumnsState();
			UpdateSlideBullets();
			AllowToSave = true;
			SettingsNotSaved = false;
			Controller.Instance.SaveSchedule(LocalSchedule, true, this);
		}

		public void OpenHelp()
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("detailedgrid");
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

		private void LoadView()
		{
			ShowOptions = LocalSchedule.ViewSettings.DetailedGridViewSettings.ShowOptions;
			SelectedOptionChapterIndex = LocalSchedule.ViewSettings.DetailedGridViewSettings.SelectedOptionChapterIndex;

			PositionID = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IDPosition;
			PositionIndex = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IndexPosition;
			PositionDate = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DatePosition;
			PositionPCI = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PCIPosition;
			PositionCost = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.CostPosition;
			PositionFinalCost = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.FinalCostPosition;
			PositionDiscount = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DiscountPosition;
			PositionColor = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ColorPosition;
			PositionPublication = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PublicationPosition;
			PositionSquare = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SquarePosition;
			PositionPageSize = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PageSizePosition;
			PositionPercentOfPage = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PercentOfPagePosition;
			PositionDimensions = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DimensionsPosition;
			PositionMechanicals = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.MechanicalsPosition;
			PositionSection = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SectionPosition;
			PositionDelivery = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeliveryPosition;
			PositionReadership = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ReadershipPosition;
			PositionDeadline = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeadlinePosition;

			PositionColumnInchesInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionSquare;
			PositionCommentsInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionComments;
			PositionDeadlineInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionDeadline;
			PositionDeliveryInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionDelivery;
			PositionDimensionsInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionDimensions;
			PositionMechanicalsInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionMechanicals;
			PositionPageSizeInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionPageSize;
			PositionPercentOfPageInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionPercentOfPage;
			PositionPublicationInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionPublication;
			PositionReadershipInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionReadership;
			PositionSectionInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionSection;

			EnableColorHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.EnableColor;
			EnableCostHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.EnableCost;
			EnableDateHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.EnableDate;
			EnableDeadlineHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.EnableDeadline;
			EnableDeliveryHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.EnableDelivery;
			EnableDimensionsHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.EnableDimensions;
			EnableDiscountHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.EnableDiscount;
			EnableFinalCostHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.EnableFinalCost;
			EnableIDHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.EnableID;
			EnableIndexHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.EnableIndex;
			EnableMechanicalsHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.EnableMechanicals;
			EnablePageSizeHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.EnablePageSize;
			EnablePercentOfPageHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.EnablePercentOfPage & ListManager.Instance.ShareUnits.Count > 0;
			EnablePCIHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.EnablePCI;
			EnablePublicationHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.EnablePublication;
			EnableReadershipHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.EnableReadership;
			EnableSectionHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.EnableSection;
			EnableSquareHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.EnableSquare;
			EnableCommentsHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.EnableAdNotes;

			_showColorHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowColor;
			_showCostHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowCost;
			_showDateHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowDate;
			_showDeadlineHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowDeadline;
			_showDeliveryHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowDelivery;
			_showDimensionsHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowDimensions;
			_showDiscountHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowDiscount;
			_showFinalCostHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowFinalCost;
			_showIDHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowID;
			_showIndexHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowIndex;
			_showMechanicalsHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowMechanicals;
			_showPageSizeHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowPageSize;
			_showPercentOfPageHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowPercentOfPage & ListManager.Instance.ShareUnits.Count > 0;
			_showPCIHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowPCI;
			_showPublicationHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowPublication;
			_showReadershipHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowReadership;
			_showSectionHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowSection;
			_showSquareHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowSquare;
			_showCommentsHeader = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowAdNotes;

			EnableColumnInchesInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.EnableSquare;
			EnableCommentsInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.EnableComments;
			EnableDeadlineInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.EnableDeadline;
			EnableDeliveryInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.EnableDelivery;
			EnableDimensionsInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.EnableDimensions;
			EnableMechanicalsInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.EnableMechanicals;
			EnablePageSizeInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.EnablePageSize;
			EnablePercentOfPageInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.EnablePercentOfPage;
			EnablePublicationInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.EnablePublication;
			EnableReadershipInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.EnableReadership;
			EnableSectionInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.EnableSection;

			ShowColumnInchesInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowSquare;
			ShowCommentsInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowComments;
			ShowDeadlineInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowDeadline;
			ShowDeliveryInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowDelivery;
			ShowDimensionsInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowDimensions;
			ShowMechanicalsInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowMechanicals;
			ShowPageSizeInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowPageSize;
			ShowPercentOfPageInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowPercentOfPage;
			ShowPublicationInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowPublication;
			ShowReadershipInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowReadership;
			ShowSectionInPreview = LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowSection;

			WidthID = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IDWidth;
			WidthIndex = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IndexWidth;
			WidthDate = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DateWidth;
			WidthPCI = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PCIWidth;
			WidthCost = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.CostWidth;
			WidthFinalCost = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.FinalCostWidth;
			WidthDiscount = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DiscountWidth;
			WidthColor = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ColorWidth;
			WidthPublication = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PublicationWidth;
			WidthSquare = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SquareWidth;
			WidthPageSize = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PageSizeWidth;
			WidthPercentOfPage = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PercentOfPageWidth;
			WidthDimensions = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DimensionsWidth;
			WidthMechanicals = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.MechanicalsWidth;
			WidthSection = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SectionWidth;
			WidthDelivery = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeliveryWidth;
			WidthReadership = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ReadershipWidth;
			WidthDeadline = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeadlineWidth;

			CaptionID = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IDCaption;
			CaptionIndex = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IndexCaption;
			CaptionDate = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DateCaption;
			CaptionPCI = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PCICaption;
			CaptionCost = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.CostCaption;
			CaptionFinalCost = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.FinalCostCaption;
			CaptionDiscount = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DiscountCaption;
			CaptionColor = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ColorCaption;
			CaptionPublication = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PublicationCaption;
			CaptionSquare = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SquareCaption;
			CaptionPageSize = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PageSizeCaption;
			CaptionPercentOfPage = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PercentOfPageCaption;
			CaptionDimensions = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DimensionsCaption;
			CaptionMechanicals = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.MechanicalsCaption;
			CaptionSection = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SectionCaption;
			CaptionDelivery = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeliveryCaption;
			CaptionReadership = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ReadershipCaption;
			CaptionDeadline = LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeadlineCaption;

			ColumnsColumns.LoadColumnsState();
			AdNotes.LoadAdNotes();
			SlideBullets.LoadSlideBullets();
			SlideHeader.LoadSlideHeader();

			SetPreviewState();
			SaveView();
		}

		private void SetColumnsState()
		{
			foreach (PublicationDetailedGridControl publicationControl in _tabPages)
			{
				publicationControl.gridColumnColorPricing.VisibleIndex = PositionColor;
				publicationControl.gridColumnColumnInches.VisibleIndex = PositionSquare;
				publicationControl.gridColumnDate.VisibleIndex = PositionDate;
				publicationControl.gridColumnDeadline.VisibleIndex = PositionDeadline;
				publicationControl.gridColumnDelivery.VisibleIndex = PositionDelivery;
				publicationControl.gridColumnDiscountRate.VisibleIndex = PositionDiscount;
				publicationControl.gridColumnFinalRate.VisibleIndex = PositionFinalCost;
				publicationControl.gridColumnID.VisibleIndex = PositionID;
				publicationControl.gridColumnIndex.VisibleIndex = PositionIndex;
				publicationControl.gridColumnMechanicals.VisibleIndex = PositionMechanicals;
				publicationControl.gridColumnPageSize.VisibleIndex = PositionPageSize;
				publicationControl.gridColumnPercentOfPage.VisibleIndex = PositionPercentOfPage;
				publicationControl.gridColumnPCIRate.VisibleIndex = PositionPCI;
				publicationControl.gridColumnADRate.VisibleIndex = PositionCost;
				publicationControl.gridColumnPublication.VisibleIndex = PositionPublication;
				publicationControl.gridColumnDimensions.VisibleIndex = PositionDimensions;
				publicationControl.gridColumnReadership.VisibleIndex = PositionReadership;
				publicationControl.gridColumnSection.VisibleIndex = PositionSection;

				publicationControl.gridViewPublications.OptionsView.ShowPreview = _showCommentsHeader;
				publicationControl.gridColumnColorPricing.Visible = _showColorHeader;
				publicationControl.gridColumnColumnInches.Visible = _showSquareHeader;
				publicationControl.gridColumnDate.Visible = _showDateHeader;
				publicationControl.gridColumnDeadline.Visible = _showDeadlineHeader;
				publicationControl.gridColumnDelivery.Visible = _showDeliveryHeader;
				publicationControl.gridColumnDiscountRate.Visible = _showDiscountHeader;
				publicationControl.gridColumnFinalRate.Visible = _showFinalCostHeader;
				publicationControl.gridColumnID.Visible = _showIDHeader;
				publicationControl.gridColumnIndex.Visible = _showIndexHeader;
				publicationControl.gridColumnMechanicals.Visible = _showMechanicalsHeader;
				publicationControl.gridColumnPageSize.Visible = _showPageSizeHeader;
				publicationControl.gridColumnPercentOfPage.Visible = _showPercentOfPageHeader;
				publicationControl.gridColumnPCIRate.Visible = _showPCIHeader;
				publicationControl.gridColumnADRate.Visible = _showCostHeader;
				publicationControl.gridColumnPublication.Visible = _showPublicationHeader;
				publicationControl.gridColumnDimensions.Visible = _showDimensionsHeader;
				publicationControl.gridColumnReadership.Visible = _showReadershipHeader;
				publicationControl.gridColumnSection.Visible = _showSectionHeader;

				publicationControl.gridColumnColorPricing.Width = WidthColor;
				publicationControl.gridColumnColumnInches.Width = WidthSquare;
				publicationControl.gridColumnDate.Width = WidthDate;
				publicationControl.gridColumnDeadline.Width = WidthDeadline;
				publicationControl.gridColumnDelivery.Width = WidthDelivery;
				publicationControl.gridColumnDiscountRate.Width = WidthDiscount;
				publicationControl.gridColumnFinalRate.Width = WidthFinalCost;
				publicationControl.gridColumnID.Width = WidthID;
				publicationControl.gridColumnIndex.Width = WidthIndex;
				publicationControl.gridColumnMechanicals.Width = WidthMechanicals;
				publicationControl.gridColumnPageSize.Width = WidthPageSize;
				publicationControl.gridColumnPercentOfPage.Width = WidthPercentOfPage;
				publicationControl.gridColumnPCIRate.Width = WidthPCI;
				publicationControl.gridColumnADRate.Width = WidthCost;
				publicationControl.gridColumnPublication.Width = WidthPublication;
				publicationControl.gridColumnDimensions.Width = WidthDimensions;
				publicationControl.gridColumnReadership.Width = WidthReadership;
				publicationControl.gridColumnSection.Width = WidthSection;

				publicationControl.gridColumnColorPricing.Caption = CaptionColor;
				publicationControl.gridColumnColumnInches.Caption = CaptionSquare;
				publicationControl.gridColumnDate.Caption = CaptionDate;
				publicationControl.gridColumnDeadline.Caption = CaptionDeadline;
				publicationControl.gridColumnDelivery.Caption = CaptionDelivery;
				publicationControl.gridColumnDiscountRate.Caption = CaptionDiscount;
				publicationControl.gridColumnFinalRate.Caption = CaptionFinalCost;
				publicationControl.gridColumnID.Caption = CaptionID;
				publicationControl.gridColumnIndex.Caption = CaptionIndex;
				publicationControl.gridColumnMechanicals.Caption = CaptionMechanicals;
				publicationControl.gridColumnPageSize.Caption = CaptionPageSize;
				publicationControl.gridColumnPercentOfPage.Caption = CaptionPercentOfPage;
				publicationControl.gridColumnPCIRate.Caption = CaptionPCI;
				publicationControl.gridColumnADRate.Caption = CaptionCost;
				publicationControl.gridColumnPublication.Caption = CaptionPublication;
				publicationControl.gridColumnDimensions.Caption = CaptionDimensions;
				publicationControl.gridColumnReadership.Caption = CaptionReadership;
				publicationControl.gridColumnSection.Caption = CaptionSection;
			}
			SaveView();
		}

		private void UpdateSlideBullets()
		{
			var publicationControl = xtraTabControlPublications.SelectedTabPage as PublicationDetailedGridControl;
			if (publicationControl != null)
			{
				SlideBullets.TotalInserts = publicationControl.PrintProduct.TotalInserts.ToString("#,##0");
				SlideBullets.PageSize = publicationControl.PrintProduct.SizeOptions.PageSizeOutput;
				SlideBullets.PercentOfPage = publicationControl.PrintProduct.SizeOptions.PercentOfPageOutput;
				SlideBullets.Dimensions = publicationControl.PrintProduct.SizeOptions.Dimensions;
				SlideBullets.ColumnInches = publicationControl.PrintProduct.SizeOptions.Square.HasValue ? publicationControl.PrintProduct.SizeOptions.Square.Value.ToString("#,###.00#") : "N/A";
				SlideBullets.AvgAdCost = publicationControl.PrintProduct.AvgADRate.ToString("$#,###.00");
				SlideBullets.AvgFinalCost = publicationControl.PrintProduct.AvgFinalRate.ToString("$#,###.00");
				SlideBullets.AvgPCI = publicationControl.PrintProduct.AvgPCIRate.ToString("$#,###.00");
				SlideBullets.Delivery = publicationControl.PrintProduct.DailyDelivery != null ? publicationControl.PrintProduct.DailyDelivery.Value.ToString("#,##0") : string.Empty;
				SlideBullets.Readership = publicationControl.PrintProduct.DailyReadership != null ? publicationControl.PrintProduct.DailyReadership.Value.ToString("#,##0") : string.Empty;
				SlideBullets.TotalColor = publicationControl.PrintProduct.TotalColorPricingCalculated.ToString("$#,###.00");
				SlideBullets.Discounts = publicationControl.PrintProduct.TotalDiscountRate.ToString("$#,###.00");
				SlideBullets.TotalFinalCost = publicationControl.PrintProduct.TotalFinalRate.ToString("$#,###.00");
				SlideBullets.TotalSquare = publicationControl.PrintProduct.TotalSquare.HasValue ? publicationControl.PrintProduct.TotalSquare.Value.ToString("#,###.00#") : "N/A";
			}
			else
			{
				SlideBullets.TotalInserts = string.Empty;
				SlideBullets.PageSize = string.Empty;
				SlideBullets.PercentOfPage = string.Empty;
				SlideBullets.Dimensions = string.Empty;
				SlideBullets.ColumnInches = string.Empty;
				SlideBullets.AvgAdCost = string.Empty;
				SlideBullets.AvgFinalCost = string.Empty;
				SlideBullets.AvgPCI = string.Empty;
				SlideBullets.Delivery = string.Empty;
				SlideBullets.Readership = string.Empty;
				SlideBullets.TotalColor = string.Empty;
				SlideBullets.Discounts = string.Empty;
				SlideBullets.TotalFinalCost = string.Empty;
				SlideBullets.TotalSquare = string.Empty;
			}
		}

		private void xtraTabControlPublications_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			AllowToSave = false;
			UpdateSlideBullets();
			AllowToSave = true;
		}

		private void hyperLinkEditReset_OpenLink(object sender, OpenLinkEventArgs e)
		{
			ResetToDefault();
			e.Handled = true;
		}

		#region Output Stuff
		public void EditDigitalLegend()
		{
			using (var form = new FormDigital(LocalSchedule.ViewSettings.DetailedGridViewSettings.DigitalLegend))
			{
				form.RequestDefaultInfo += (o, e) =>
				{
					e.Editor.EditValue = LocalSchedule.GetDigitalInfo(e);
				};
				if (form.ShowDialog() == DialogResult.OK)
					SettingsNotSaved = true;
			}
		}

		public void PrintOutput()
		{
			using (var form = new FormSelectPublication())
			{
				form.Text = "Detailed Advertising Grid Slide Output";
				form.pbLogo.Image = Resources.Preview;
				form.laTitle.Text = "You have Several Publications in this Detailed Advertising Grid…";
				form.buttonXCurrentPublication.Text = "Send just the Current Advertising Grid to my PowerPoint Presentation";
				form.buttonXSelectedPublications.Text = "Send all Selected Advertising Grids to my PowerPoint Presentation";
				foreach (PublicationDetailedGridControl tabPage in _tabPages)
					if (tabPage.PageEnabled)
						form.checkedListBoxControlPublications.Items.Add(tabPage.PrintProduct.UniqueID, tabPage.PrintProduct.Name, CheckState.Checked, true);
				var result = DialogResult.Yes;
				if (form.checkedListBoxControlPublications.Items.Count > 1)
				{
					RegistryHelper.MainFormHandle = form.Handle;
					RegistryHelper.MaximizeMainForm = false;
					result = form.ShowDialog();
					RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
					RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
					if (result == DialogResult.Cancel)
						return;
				}
				using (var formGridType = new FormSelectOutput(OutputType.PowerPoint))
				{
					formGridType.buttonXGrid.Enabled = SelectedColumnsCount >= 4 && SelectedColumnsCount <= 6 && Directory.Exists(BusinessWrapper.Instance.OutputManager.DetailedGridGridBasedTemlatesFolderPath);
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
							if (result == DialogResult.Yes)
								(xtraTabControlPublications.TabPages[xtraTabControlPublications.SelectedTabPageIndex] as PublicationDetailedGridControl).PrintOutput(excelOutput, pasteAsImage);
							else if (result == DialogResult.No)
							{
								foreach (CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
								{
									if (item.CheckState == CheckState.Checked)
									{
										PublicationDetailedGridControl tabPage = _tabPages.Where(x => x.PrintProduct.UniqueID.Equals(item.Value)).FirstOrDefault();
										if (tabPage != null)
											tabPage.PrintOutput(excelOutput, pasteAsImage);
									}
								}
							}
							formProgress.Close();
						}
						using (var formOutput = new FormSlideOutput())
						{
							if (formOutput.ShowDialog() != DialogResult.OK)
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

		public void Email()
		{
			using (var form = new FormSelectPublication())
			{
				form.Text = "Detailed Advertising Grid Email Output";
				form.pbLogo.Image = Resources.EmailBig;
				form.laTitle.Text = "You have Several Publications in this Detailed Advertising Grid…";
				form.buttonXCurrentPublication.Text = "Attach the Current Advertising Grid to my Outlook Email Message";
				form.buttonXSelectedPublications.Text = "Attach all Selected Advertising Grids to my Outlook Email Message";
				foreach (PublicationDetailedGridControl tabPage in _tabPages)
					if (tabPage.PageEnabled)
						form.checkedListBoxControlPublications.Items.Add(tabPage.PrintProduct.UniqueID, tabPage.PrintProduct.Name, CheckState.Checked, true);
				var result = DialogResult.Yes;
				if (form.checkedListBoxControlPublications.Items.Count > 1)
				{
					RegistryHelper.MainFormHandle = form.Handle;
					RegistryHelper.MaximizeMainForm = false;
					result = form.ShowDialog();
					RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
					RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
					if (result == DialogResult.Cancel)
						return;
				}
				using (var formGridType = new FormSelectOutput(OutputType.Email))
				{
					formGridType.buttonXGrid.Enabled = SelectedColumnsCount >= 4 && SelectedColumnsCount <= 6 && Directory.Exists(BusinessWrapper.Instance.OutputManager.DetailedGridGridBasedTemlatesFolderPath);
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
							string tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
							if (result == DialogResult.Yes)
							{
								var outputControl = xtraTabControlPublications.TabPages[xtraTabControlPublications.SelectedTabPageIndex] as PublicationDetailedGridControl;
								if (outputControl != null)
								{
									outputControl.PrepareOutput(excelOutput);
									if (excelOutput)
										AdSchedulePowerPointHelper.Instance.PrepareDetailedGridExcelBasedEmail(tempFileName, new[] { outputControl }, pasteAsImage);
									else
										AdSchedulePowerPointHelper.Instance.PrepareDetailedGridGridBasedEmail(tempFileName, new[] { outputControl });
								}
							}
							else if (result == DialogResult.No)
							{
								var emailPages = new List<PublicationDetailedGridControl>();
								foreach (CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
								{
									if (item.CheckState == CheckState.Checked)
									{
										PublicationDetailedGridControl tabPage = _tabPages.Where(x => x.PrintProduct.UniqueID.Equals(item.Value)).FirstOrDefault();
										if (tabPage != null)
										{
											tabPage.PrepareOutput(excelOutput);
											emailPages.Add(tabPage);
										}
									}
								}
								if (excelOutput)
									AdSchedulePowerPointHelper.Instance.PrepareDetailedGridExcelBasedEmail(tempFileName, emailPages.ToArray(), pasteAsImage);
								else
									AdSchedulePowerPointHelper.Instance.PrepareDetailedGridGridBasedEmail(tempFileName, emailPages.ToArray());
							}
							Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
							formProgress.Close();
							if (File.Exists(tempFileName))
								using (var formEmail = new FormEmail())
								{
									formEmail.Text = "Email this Detailed Advertising Grid";
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
		}

		public void Preview()
		{
			using (var form = new FormSelectPublication())
			{
				form.Text = "Detailed Advertising Grid Preview";
				form.pbLogo.Image = Resources.Preview;
				form.laTitle.Text = "You have Several Publications in this Detailed Advertising Grid…";
				form.buttonXCurrentPublication.Text = "Preview the Current Advertising Grid";
				form.buttonXSelectedPublications.Text = "Preview all Selected Advertising Grids";
				foreach (PublicationDetailedGridControl tabPage in _tabPages)
					if (tabPage.PageEnabled)
						form.checkedListBoxControlPublications.Items.Add(tabPage.PrintProduct.UniqueID, tabPage.PrintProduct.Name, CheckState.Checked, true);
				var result = DialogResult.Yes;
				if (form.checkedListBoxControlPublications.Items.Count > 1)
				{
					RegistryHelper.MainFormHandle = form.Handle;
					RegistryHelper.MaximizeMainForm = false;
					result = form.ShowDialog();
					RegistryHelper.MaximizeMainForm = true;
					RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
					if (result == DialogResult.Cancel)
						return;
				}
				using (var formGridType = new FormSelectOutput(OutputType.Preview))
				{
					formGridType.buttonXGrid.Enabled = SelectedColumnsCount >= 4 && SelectedColumnsCount <= 6 && Directory.Exists(BusinessWrapper.Instance.OutputManager.DetailedGridGridBasedTemlatesFolderPath);
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
							string tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
							if (result == DialogResult.Yes)
							{
								var outputControl = xtraTabControlPublications.TabPages[xtraTabControlPublications.SelectedTabPageIndex] as PublicationDetailedGridControl;
								if (outputControl != null)
								{
									outputControl.PrepareOutput(excelOutput);
									if (excelOutput)
										AdSchedulePowerPointHelper.Instance.PrepareDetailedGridExcelBasedEmail(tempFileName, new[] { outputControl }, pasteAsImage);
									else
										AdSchedulePowerPointHelper.Instance.PrepareDetailedGridGridBasedEmail(tempFileName, new[] { outputControl });
								}
							}
							else if (result == DialogResult.No)
							{
								var emailPages = new List<PublicationDetailedGridControl>();
								foreach (CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
								{
									if (item.CheckState == CheckState.Checked)
									{
										PublicationDetailedGridControl tabPage = _tabPages.Where(x => x.PrintProduct.UniqueID.Equals(item.Value)).FirstOrDefault();
										if (tabPage != null)
										{
											tabPage.PrepareOutput(excelOutput);
											emailPages.Add(tabPage);
										}
									}
								}
								if (excelOutput)
									AdSchedulePowerPointHelper.Instance.PrepareDetailedGridExcelBasedEmail(tempFileName, emailPages.ToArray(), pasteAsImage);
								else
									AdSchedulePowerPointHelper.Instance.PrepareDetailedGridGridBasedEmail(tempFileName, emailPages.ToArray());
							}
							Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
							formProgress.Close();
							if (File.Exists(tempFileName))
								using (var formPreview = new FormPreview())
								{
									formPreview.Text = "Preview Detailed Advertising Grid";
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
		}
		#endregion
	}
}