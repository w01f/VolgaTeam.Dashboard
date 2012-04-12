using System.Windows.Forms;

namespace NewBizWizForm.TabHomeForms
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TabHomeMainPage : UserControl
    {
        private static TabHomeMainPage _instance;
        private DevComponents.DotNetBar.SuperTooltipInfo _overviewHelpToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Help me use this Sales Dashboard", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
        private DevComponents.DotNetBar.SuperTooltipInfo _coverHelpToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Help me with the Cover Slide", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
        private DevComponents.DotNetBar.SuperTooltipInfo _leadOffHelpToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Help me with the Introduction Slide", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
        private DevComponents.DotNetBar.SuperTooltipInfo _clientGoalsHelpToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Help me with the Client Needs Analysis Slide", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
        private DevComponents.DotNetBar.SuperTooltipInfo _targetCustomersHelpToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Help me with the Target Customer Slide", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
        private DevComponents.DotNetBar.SuperTooltipInfo _simpleSumaryHelpToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Help me with the Closing Summary Slide", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);

        private TabHomeMainPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            AppManager.Instance.SetClickEventHandler(this);
        }

        public static TabHomeMainPage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TabHomeMainPage();
                return _instance;
            }
        }

        public void UpdatePageAccordingToggledButton()
        {
            Control parent = this.Parent;
            this.Parent = null;
            this.Controls.Clear();

            ToolForms.WhiteBorderControl borderedControl = new NewBizWizForm.ToolForms.WhiteBorderControl();
            if (FormMain.Instance.buttonItemHomeOverview != null && FormMain.Instance.buttonItemHomeOverview.Checked)
            {
                FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _overviewHelpToolTip);
                this.Controls.Add(TabHomeOverviewControl.Instance);
            }
            else if (FormMain.Instance.buttonItemHomeCover != null && FormMain.Instance.buttonItemHomeCover.Checked)
            {
                FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _coverHelpToolTip);
                this.Controls.Add(TabHomeForms.CoverControl.Instance);
                TabHomeForms.CoverControl.Instance.EnableOutput = borderedControl.EnableOutputButton;
                TabHomeForms.CoverControl.Instance.EnableSavedFiles = borderedControl.EnableSavedFilesButton;
                TabHomeForms.CoverControl.Instance.UpdateOutputState();
                TabHomeForms.CoverControl.Instance.UpdateSavedFilesState();
                FormMain.Instance.OutputClick = TabHomeForms.CoverControl.Instance.Output;
            }
            else if (FormMain.Instance.buttonItemSimpleSummary != null && FormMain.Instance.buttonItemSimpleSummary.Checked)
            {
                FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _simpleSumaryHelpToolTip);
                this.Controls.Add(SimpleSummaryControl.Instance);
                SimpleSummaryControl.Instance.EnableOutput = borderedControl.EnableOutputButton;
                SimpleSummaryControl.Instance.EnableSavedFiles = borderedControl.EnableSavedFilesButton;
                SimpleSummaryControl.Instance.tabControl_SelectedTabChanged(null, null);
                SimpleSummaryControl.Instance.UpdateSavedFilesState();
                FormMain.Instance.OutputClick = SimpleSummaryControl.Instance.Output;
            }
            else
            {
                this.Controls.Add(borderedControl);
                Control parentSecond = borderedControl.panelExTop.Parent;
                borderedControl.panelExTop.Parent = null;
                borderedControl.panelExTop.Controls.Clear();
                borderedControl.OutputClick = null;

                if (FormMain.Instance.buttonItemLeadoffStatement != null && FormMain.Instance.buttonItemLeadoffStatement.Checked)
                {
                    FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _leadOffHelpToolTip);
                    borderedControl.panelExTop.Controls.Add(LeadoffStatementControl.Instance);
                    TabHomeForms.LeadoffStatementControl.Instance.EnableOutput = borderedControl.EnableOutputButton;
                    TabHomeForms.LeadoffStatementControl.Instance.EnableSavedFiles = borderedControl.EnableSavedFilesButton;
                    TabHomeForms.LeadoffStatementControl.Instance.UpdateOutputState();
                    TabHomeForms.LeadoffStatementControl.Instance.UpdateSavedFilesState();
                    borderedControl.OutputClick = TabHomeForms.LeadoffStatementControl.Instance.Output;
                    borderedControl.SavedFilesClick = TabHomeForms.LeadoffStatementControl.Instance.LoadFromFile;
                    FormMain.Instance.OutputClick = TabHomeForms.LeadoffStatementControl.Instance.Output;
                }
                else if (FormMain.Instance.buttonItemClientGoals != null && FormMain.Instance.buttonItemClientGoals.Checked)
                {
                    FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _clientGoalsHelpToolTip);
                    borderedControl.panelExTop.Controls.Add(ClientGoalsControl.Instance);
                    TabHomeForms.ClientGoalsControl.Instance.EnableOutput = borderedControl.EnableOutputButton;
                    TabHomeForms.ClientGoalsControl.Instance.EnableSavedFiles = borderedControl.EnableSavedFilesButton;
                    TabHomeForms.ClientGoalsControl.Instance.UpdateOutputState();
                    TabHomeForms.ClientGoalsControl.Instance.UpdateSavedFilesState();
                    borderedControl.OutputClick = TabHomeForms.ClientGoalsControl.Instance.Output;
                    borderedControl.SavedFilesClick = TabHomeForms.ClientGoalsControl.Instance.LoadFromFile;
                    FormMain.Instance.OutputClick = TabHomeForms.ClientGoalsControl.Instance.Output;
                }
                else if (FormMain.Instance.buttonItemTargetCustomers != null && FormMain.Instance.buttonItemTargetCustomers.Checked)
                {
                    FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _targetCustomersHelpToolTip);
                    borderedControl.panelExTop.Controls.Add(TargetCustomersControl.Instance);
                    TabHomeForms.TargetCustomersControl.Instance.EnableOutput = borderedControl.EnableOutputButton;
                    TabHomeForms.TargetCustomersControl.Instance.EnableSavedFiles = borderedControl.EnableSavedFilesButton;
                    TabHomeForms.TargetCustomersControl.Instance.UpdateOutputState();
                    TabHomeForms.TargetCustomersControl.Instance.UpdateSavedFilesState();
                    borderedControl.OutputClick = TabHomeForms.TargetCustomersControl.Instance.Output;
                    borderedControl.SavedFilesClick = TabHomeForms.TargetCustomersControl.Instance.LoadFromFile;
                    FormMain.Instance.OutputClick = TabHomeForms.TargetCustomersControl.Instance.Output;
                }
                borderedControl.panelExTop.Parent = parentSecond;

                if (borderedControl.panelExTop.Controls.Count == 0)
                {
                    FormMain.Instance.buttonItemHomeOverview.Checked = true;
                    this.Controls.Clear();
                    this.Controls.Add(TabHomeOverviewControl.Instance);
                }
            }
            this.Parent = parent;
        }
    }
}
