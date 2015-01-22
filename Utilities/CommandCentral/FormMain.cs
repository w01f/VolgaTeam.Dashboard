using System;
using System.Windows.Forms;
using CommandCentral.TabMainDashboardForms;
using CommandCentral.TabMarketProForms;
using CommandCentral.TabSalesDepotForms;
using CommandCentral.TabSalesProForms;
using DevComponents.DotNetBar;

namespace CommandCentral
{
	public partial class FormMain : RibbonForm
	{
		private static FormMain _instance;

		private FormMain()
		{
			InitializeComponent();

			#region Main Dashboard Buttons

			buttonItemMainDashboardViewFile.Click += MainDashboardPage.Instance.buttonItemMainDashboardViewFile_Click;
			buttonItemMainDashboardUpdate.Click += MainDashboardPage.Instance.buttonItemMainDashboardUpdate_Click;
			buttonItemUsers.Click += MainDashboardPage.Instance.MainDashboardButton_Click;
			buttonItemBasicCover.Click += MainDashboardPage.Instance.MainDashboardButton_Click;
			buttonItemBasicIntroSlide.Click += MainDashboardPage.Instance.MainDashboardButton_Click;
			buttonItemBasicNeedsAnalysis.Click += MainDashboardPage.Instance.MainDashboardButton_Click;
			buttonItemBasicTargetCustomer.Click += MainDashboardPage.Instance.MainDashboardButton_Click;
			buttonItemBasicClosingSummary.Click += MainDashboardPage.Instance.MainDashboardButton_Click;
			buttonItemOnlineStrategy.Click += MainDashboardPage.Instance.MainDashboardButton_Click;
			buttonItemRadioStrategy.Click += MainDashboardPage.Instance.MainDashboardButton_Click;
			buttonItemNewspaperStrategy.Click += MainDashboardPage.Instance.MainDashboardButton_Click;
			buttonItemTVStrategy.Click += MainDashboardPage.Instance.MainDashboardButton_Click;
			buttonItemQuickList.Click += MainDashboardPage.Instance.MainDashboardButton_Click;

			#endregion

			#region Media Library Buttons

			buttonItemMarketProViewFile.Click += MarketProMainPage.Instance.buttonItemMediaLibraryViewFile_Click;
			buttonItemMarketProUpdate.Click += MarketProMainPage.Instance.buttonItemMediaLibraryUpdate_Click;
			buttonItemMarketProCable.Click += MarketProMainPage.Instance.MediaLibraryButton_Click;
			buttonItemMarketProDirectMail.Click += MarketProMainPage.Instance.MediaLibraryButton_Click;
			buttonItemMarketProMediaStrategy.Click += MarketProMainPage.Instance.MediaLibraryButton_Click;
			buttonItemMarketProMobile.Click += MarketProMainPage.Instance.MediaLibraryButton_Click;
			buttonItemMarketProOutdoor.Click += MarketProMainPage.Instance.MediaLibraryButton_Click;
			buttonItemMarketProPrint.Click += MarketProMainPage.Instance.MediaLibraryButton_Click;
			buttonItemMarketProRadio.Click += MarketProMainPage.Instance.MediaLibraryButton_Click;
			buttonItemMarketProTV.Click += MarketProMainPage.Instance.MediaLibraryButton_Click;
			buttonItemMarketProWeb.Click += MarketProMainPage.Instance.MediaLibraryButton_Click;
			buttonItemMarketProYellowPages.Click += MarketProMainPage.Instance.MediaLibraryButton_Click;

			#endregion

			#region Pro Slides Buttons

			buttonItemSalesProViewFile.Click += SalesProMainPage.Instance.buttonItemAdvancedViewFile_Click;
			buttonItemSalesProUpdate.Click += SalesProMainPage.Instance.buttonItemAdvancedUpdate_Click;
			buttonItemSalesProBigIdea.Click += SalesProMainPage.Instance.AdvancedButton_Click;
			buttonItemSalesProCampaignSummary.Click += SalesProMainPage.Instance.AdvancedButton_Click;
			buttonItemSalesProCampaignTimeline.Click += SalesProMainPage.Instance.AdvancedButton_Click;
			buttonItemSalesProClientBenefits.Click += SalesProMainPage.Instance.AdvancedButton_Click;
			buttonItemSalesProCreativeStrategy.Click += SalesProMainPage.Instance.AdvancedButton_Click;
			buttonItemSalesProInvestmentCalendar.Click += SalesProMainPage.Instance.AdvancedButton_Click;
			buttonItemSalesProROIFormula.Click += SalesProMainPage.Instance.AdvancedButton_Click;
			buttonItemSalesProValueAnalysis.Click += SalesProMainPage.Instance.AdvancedButton_Click;

			#endregion

			#region Sales Depot Buttons

			buttonItemSalesDepotViewFile.Click += SalesDepotMainPage.Instance.buttonItemSalesDepotViewFile_Click;
			buttonItemSalesDepotUpdate.Click += SalesDepotMainPage.Instance.buttonItemSalesDepotUpdate_Click;
			buttonItemSalesDepotSearch.Click += SalesDepotMainPage.Instance.SalesDepotButton_Click;
			buttonItemSalesDepotAccessRights.Click += SalesDepotMainPage.Instance.SalesDepotButton_Click;

			#endregion
		}

		public static FormMain Instance
		{
			get
			{
				if (_instance == null)
					_instance = new FormMain();
				return _instance;
			}
		}

		private void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			Control parent = pnMain.Parent;
			pnMain.Parent = null;
			pnMain.Controls.Clear();
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemMainDashboard)
			{
				pnMain.Controls.Add(MainDashboardPage.Instance);
				MainDashboardPage.Instance.UpdatePageAccordingToggledButton();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSalesPro)
			{
				pnMain.Controls.Add(SalesProMainPage.Instance);
				SalesProMainPage.Instance.UpdatePageAccordingToggledButton();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemMarketPro)
			{
				pnMain.Controls.Add(MarketProMainPage.Instance);
				MarketProMainPage.Instance.UpdatePageAccordingToggledButton();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSalesDepot)
			{
				pnMain.Controls.Add(SalesDepotMainPage.Instance);
				SalesDepotMainPage.Instance.UpdatePageAccordingToggledButton();
			}
			pnMain.Parent = parent;
			pnMain.BringToFront();
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			ribbonControl_SelectedRibbonTabChanged(null, null);
			ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
		}
	}
}