using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommandCentral.TabSalesProForms
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class SalesProMainPage : UserControl
    {
        private static SalesProMainPage _instance = null;

        private AppManager.NoParamDelegate _viewFile;
        private AppManager.NoParamDelegate _updateData;

        private SalesProMainPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public static SalesProMainPage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SalesProMainPage();
                return _instance;
            }
        }

        private void UncheckButtons()
        {
            FormMain.Instance.buttonItemSalesProBigIdea.Checked = false;
            FormMain.Instance.buttonItemSalesProCampaignSummary.Checked = false;
            FormMain.Instance.buttonItemSalesProCampaignTimeline.Checked = false;
            FormMain.Instance.buttonItemSalesProClientBenefits.Checked = false;
            FormMain.Instance.buttonItemSalesProCreativeStrategy.Checked = false;
            FormMain.Instance.buttonItemSalesProInvestmentCalendar.Checked = false;
            FormMain.Instance.buttonItemSalesProROIFormula.Checked = false;
            FormMain.Instance.buttonItemSalesProValueAnalysis.Checked = false;
        }

        public void UpdatePageAccordingToggledButton()
        {
            Control parent = pnMain.Parent;
            pnMain.Parent = null;
            pnMain.Controls.Clear();
            _viewFile = null;
            _updateData = null;
            if (FormMain.Instance.buttonItemSalesProBigIdea != null && FormMain.Instance.buttonItemSalesProBigIdea.Checked)
            {
                DataControl.Instance.ViewSource = BigIdeaManager.ViewSourceFile;
                DataControl.Instance.ButtonText = BigIdeaManager.ButtonText;
                _viewFile = BigIdeaManager.ViewDataFile;
                _updateData = BigIdeaManager.UpdateData;
            }
            else if (FormMain.Instance.buttonItemSalesProCampaignSummary != null && FormMain.Instance.buttonItemSalesProCampaignSummary.Checked)
            {
                DataControl.Instance.ViewSource = CampaignSummaryManager.ViewSourceFile;
                DataControl.Instance.ButtonText = CampaignSummaryManager.ButtonText;
                _viewFile = CampaignSummaryManager.ViewDataFile;
                _updateData = CampaignSummaryManager.UpdateData;
            }
            else if (FormMain.Instance.buttonItemSalesProCampaignTimeline != null && FormMain.Instance.buttonItemSalesProCampaignTimeline.Checked)
            {
                DataControl.Instance.ViewSource = CampaignTimelineManager.ViewSourceFile;
                DataControl.Instance.ButtonText = CampaignTimelineManager.ButtonText;
                _viewFile = CampaignTimelineManager.ViewDataFile;
                _updateData = CampaignTimelineManager.UpdateData;
            }
            else if (FormMain.Instance.buttonItemSalesProClientBenefits != null && FormMain.Instance.buttonItemSalesProClientBenefits.Checked)
            {
                DataControl.Instance.ViewSource = ClientBenefitsManager.ViewSourceFile;
                DataControl.Instance.ButtonText = ClientBenefitsManager.ButtonText;
                _viewFile = ClientBenefitsManager.ViewDataFile;
                _updateData = ClientBenefitsManager.UpdateData;
            }
            else if (FormMain.Instance.buttonItemSalesProCreativeStrategy != null && FormMain.Instance.buttonItemSalesProCreativeStrategy.Checked)
            {
                DataControl.Instance.ViewSource = CreativeStrategyManager.ViewSourceFile;
                DataControl.Instance.ButtonText = CreativeStrategyManager.ButtonText;
                _viewFile = CreativeStrategyManager.ViewDataFile;
                _updateData = CreativeStrategyManager.UpdateData;
            }
            else if (FormMain.Instance.buttonItemSalesProInvestmentCalendar != null && FormMain.Instance.buttonItemSalesProInvestmentCalendar.Checked)
            {
                DataControl.Instance.ViewSource = InvestmentCalendarManager.ViewSourceFile;
                DataControl.Instance.ButtonText = InvestmentCalendarManager.ButtonText;
                _viewFile = InvestmentCalendarManager.ViewDataFile;
                _updateData = InvestmentCalendarManager.UpdateData;
            }
            else if (FormMain.Instance.buttonItemSalesProROIFormula != null && FormMain.Instance.buttonItemSalesProROIFormula.Checked)
            {
                DataControl.Instance.ViewSource = ROIFormulaManager.ViewSourceFile;
                DataControl.Instance.ButtonText = ROIFormulaManager.ButtonText;
                _viewFile = ROIFormulaManager.ViewDataFile;
                _updateData = ROIFormulaManager.UpdateData;
            }
            else if (FormMain.Instance.buttonItemSalesProValueAnalysis != null && FormMain.Instance.buttonItemSalesProValueAnalysis.Checked)
            {
                DataControl.Instance.ViewSource = ValueAnalysisManager.ViewSourceFile;
                DataControl.Instance.ButtonText = ValueAnalysisManager.ButtonText;
                _viewFile = ValueAnalysisManager.ViewDataFile;
                _updateData = ValueAnalysisManager.UpdateData;
            }
            pnMain.Controls.Add(DataControl.Instance);
            pnMain.Parent = parent;
            pnMain.BringToFront();
        }

        public void buttonItemAdvancedUpdate_Click(object sender, EventArgs e)
        {
			_updateData?.Invoke();
		}

        public void buttonItemAdvancedViewFile_Click(object sender, EventArgs e)
        {
			_viewFile?.Invoke();
		}

        public void AdvancedButton_Click(object sender, EventArgs e)
        {
            UncheckButtons();
            if (sender != null && sender.GetType() == typeof(DevComponents.DotNetBar.ButtonItem))
                ((DevComponents.DotNetBar.ButtonItem)sender).Checked = true;
            UpdatePageAccordingToggledButton();
        }
    }
}
