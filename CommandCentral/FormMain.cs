using System;
using System.Windows.Forms;

namespace CommandCentral
{
    public partial class FormMain : Form
    {
        private static FormMain _instance = null;

        private FormMain()
        {
            InitializeComponent();
            #region Main Dashboard Buttons
            buttonItemMainDashboardViewFile.Click += new EventHandler(TabMainDashboard.MainDashboardPage.Instance.buttonItemMainDashboardViewFile_Click);
            buttonItemMainDashboardUpdate.Click += new EventHandler(TabMainDashboard.MainDashboardPage.Instance.buttonItemMainDashboardUpdate_Click);
            buttonItemUsers.Click += new EventHandler(TabMainDashboard.MainDashboardPage.Instance.MainDashboardButton_Click);
            buttonItemBasicCover.Click += new EventHandler(TabMainDashboard.MainDashboardPage.Instance.MainDashboardButton_Click);
            buttonItemBasicIntroSlide.Click += new EventHandler(TabMainDashboard.MainDashboardPage.Instance.MainDashboardButton_Click);
            buttonItemBasicNeedsAnalysis.Click += new EventHandler(TabMainDashboard.MainDashboardPage.Instance.MainDashboardButton_Click);
            buttonItemBasicTargetCustomer.Click += new EventHandler(TabMainDashboard.MainDashboardPage.Instance.MainDashboardButton_Click);
            buttonItemBasicClosingSummary.Click += new EventHandler(TabMainDashboard.MainDashboardPage.Instance.MainDashboardButton_Click);
            buttonItemMobileStrategy.Click += new EventHandler(TabMainDashboard.MainDashboardPage.Instance.MainDashboardButton_Click);
            buttonItemOnlineStrategy.Click += new EventHandler(TabMainDashboard.MainDashboardPage.Instance.MainDashboardButton_Click);
            buttonItemRadioStrategy.Click += new EventHandler(TabMainDashboard.MainDashboardPage.Instance.MainDashboardButton_Click);
            buttonItemNewspaperStrategy.Click += new EventHandler(TabMainDashboard.MainDashboardPage.Instance.MainDashboardButton_Click);
            buttonItemTVStrategy.Click += new EventHandler(TabMainDashboard.MainDashboardPage.Instance.MainDashboardButton_Click);
            #endregion

            #region Media Library Buttons
            buttonItemMarketProViewFile.Click += new EventHandler(TabMarketProForms.MarketProMainPage.Instance.buttonItemMediaLibraryViewFile_Click);
            buttonItemMarketProUpdate.Click += new EventHandler(TabMarketProForms.MarketProMainPage.Instance.buttonItemMediaLibraryUpdate_Click);
            buttonItemMarketProCable.Click += new EventHandler(TabMarketProForms.MarketProMainPage.Instance.MediaLibraryButton_Click);
            buttonItemMarketProDirectMail.Click += new EventHandler(TabMarketProForms.MarketProMainPage.Instance.MediaLibraryButton_Click);
            buttonItemMarketProMediaStrategy.Click += new EventHandler(TabMarketProForms.MarketProMainPage.Instance.MediaLibraryButton_Click);
            buttonItemMarketProMobile.Click += new EventHandler(TabMarketProForms.MarketProMainPage.Instance.MediaLibraryButton_Click);
            buttonItemMarketProOutdoor.Click += new EventHandler(TabMarketProForms.MarketProMainPage.Instance.MediaLibraryButton_Click);
            buttonItemMarketProPrint.Click += new EventHandler(TabMarketProForms.MarketProMainPage.Instance.MediaLibraryButton_Click);
            buttonItemMarketProRadio.Click += new EventHandler(TabMarketProForms.MarketProMainPage.Instance.MediaLibraryButton_Click);
            buttonItemMarketProTV.Click += new EventHandler(TabMarketProForms.MarketProMainPage.Instance.MediaLibraryButton_Click);
            buttonItemMarketProWeb.Click += new EventHandler(TabMarketProForms.MarketProMainPage.Instance.MediaLibraryButton_Click);
            buttonItemMarketProYellowPages.Click += new EventHandler(TabMarketProForms.MarketProMainPage.Instance.MediaLibraryButton_Click);
            #endregion
            
            #region Pro Slides Buttons
            buttonItemSalesProViewFile.Click += new EventHandler(TabSalesProForms.SalesProMainPage.Instance.buttonItemAdvancedViewFile_Click);
            buttonItemSalesProUpdate.Click += new EventHandler(TabSalesProForms.SalesProMainPage.Instance.buttonItemAdvancedUpdate_Click);
            buttonItemSalesProBigIdea.Click += new EventHandler(TabSalesProForms.SalesProMainPage.Instance.AdvancedButton_Click);
            buttonItemSalesProCampaignSummary.Click += new EventHandler(TabSalesProForms.SalesProMainPage.Instance.AdvancedButton_Click);
            buttonItemSalesProCampaignTimeline.Click += new EventHandler(TabSalesProForms.SalesProMainPage.Instance.AdvancedButton_Click);
            buttonItemSalesProClientBenefits.Click += new EventHandler(TabSalesProForms.SalesProMainPage.Instance.AdvancedButton_Click);
            buttonItemSalesProCreativeStrategy.Click += new EventHandler(TabSalesProForms.SalesProMainPage.Instance.AdvancedButton_Click);
            buttonItemSalesProInvestmentCalendar.Click += new EventHandler(TabSalesProForms.SalesProMainPage.Instance.AdvancedButton_Click);
            buttonItemSalesProROIFormula.Click += new EventHandler(TabSalesProForms.SalesProMainPage.Instance.AdvancedButton_Click);
            buttonItemSalesProValueAnalysis.Click += new EventHandler(TabSalesProForms.SalesProMainPage.Instance.AdvancedButton_Click);
            #endregion

            #region Sales Depot Buttons
            buttonItemSalesDepotViewFile.Click += new EventHandler(TabSalesDepotForms.SalesDepotMainPage.Instance.buttonItemSalesDepotViewFile_Click);
            buttonItemSalesDepotUpdate.Click += new EventHandler(TabSalesDepotForms.SalesDepotMainPage.Instance.buttonItemSalesDepotUpdate_Click);
            buttonItemSalesDepotSearch.Click += new EventHandler(TabSalesDepotForms.SalesDepotMainPage.Instance.SalesDepotButton_Click);
            #endregion
        }

        public static FormMain Instance
        {
            get 
            {
                if(_instance == null)
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
                pnMain.Controls.Add(TabMainDashboard.MainDashboardPage.Instance);
                TabMainDashboard.MainDashboardPage.Instance.UpdatePageAccordingToggledButton();
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSalesPro)
            {
                pnMain.Controls.Add(TabSalesProForms.SalesProMainPage.Instance);
                TabSalesProForms.SalesProMainPage.Instance.UpdatePageAccordingToggledButton();
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemMarketPro)
            {
                pnMain.Controls.Add(TabMarketProForms.MarketProMainPage.Instance);
                TabMarketProForms.MarketProMainPage.Instance.UpdatePageAccordingToggledButton();
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSalesDepot)
            {
                pnMain.Controls.Add(TabSalesDepotForms.SalesDepotMainPage.Instance);
                TabSalesDepotForms.SalesDepotMainPage.Instance.UpdatePageAccordingToggledButton();
            }
            pnMain.Parent = parent;
            pnMain.BringToFront();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            ribbonControl_SelectedRibbonTabChanged(null, null);
            ribbonControl.SelectedRibbonTabChanged+=new EventHandler(ribbonControl_SelectedRibbonTabChanged);
        }
    }
}
