using System.ComponentModel;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using NewBizWiz.Dashboard.ToolForms;

namespace NewBizWiz.Dashboard.TabHomeForms
{
	[ToolboxItem(false)]
	public partial class TabHomeMainPage : UserControl
	{
		private static TabHomeMainPage _instance;
		private readonly SuperTooltipInfo _clientGoalsHelpToolTip = new SuperTooltipInfo("HELP", "", "Help me with the Client Needs Analysis Slide", null, null, eTooltipColor.Gray);
		private readonly SuperTooltipInfo _coverHelpToolTip = new SuperTooltipInfo("HELP", "", "Help me with the Cover Slide", null, null, eTooltipColor.Gray);
		private readonly SuperTooltipInfo _leadOffHelpToolTip = new SuperTooltipInfo("HELP", "", "Help me with the Introduction Slide", null, null, eTooltipColor.Gray);
		private readonly SuperTooltipInfo _overviewHelpToolTip = new SuperTooltipInfo("HELP", "", "Help me use this Sales Dashboard", null, null, eTooltipColor.Gray);
		private readonly SuperTooltipInfo _simpleSumaryHelpToolTip = new SuperTooltipInfo("HELP", "", "Help me with the Closing Summary Slide", null, null, eTooltipColor.Gray);
		private readonly SuperTooltipInfo _targetCustomersHelpToolTip = new SuperTooltipInfo("HELP", "", "Help me with the Target Customer Slide", null, null, eTooltipColor.Gray);

		private TabHomeMainPage()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
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
			Control parent = Parent;
			Parent = null;
			Controls.Clear();

			var borderedControl = new WhiteBorderControl();
			if (FormMain.Instance.buttonItemHomeOverview != null && FormMain.Instance.buttonItemHomeOverview.Checked)
			{
				FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _overviewHelpToolTip);
				Controls.Add(TabHomeOverviewControl.Instance);
			}
			else if (FormMain.Instance.buttonItemHomeCover != null && FormMain.Instance.buttonItemHomeCover.Checked)
			{
				FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _coverHelpToolTip);
				Controls.Add(CoverControl.Instance);
				CoverControl.Instance.EnableOutput = borderedControl.EnableOutputButton;
				CoverControl.Instance.EnableSavedFiles = borderedControl.EnableSavedFilesButton;
				CoverControl.Instance.UpdateOutputState();
				CoverControl.Instance.UpdateSavedFilesState();
				FormMain.Instance.OutputClick = CoverControl.Instance.Output;
			}
			else if (FormMain.Instance.buttonItemSimpleSummary != null && FormMain.Instance.buttonItemSimpleSummary.Checked)
			{
				FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _simpleSumaryHelpToolTip);
				Controls.Add(SimpleSummaryControl.Instance);
				SimpleSummaryControl.Instance.EnableOutput = borderedControl.EnableOutputButton;
				SimpleSummaryControl.Instance.EnableSavedFiles = borderedControl.EnableSavedFilesButton;
				SimpleSummaryControl.Instance.tabControl_SelectedTabChanged(null, null);
				SimpleSummaryControl.Instance.UpdateSavedFilesState();
				FormMain.Instance.OutputClick = SimpleSummaryControl.Instance.Output;
			}
			else
			{
				Controls.Add(borderedControl);
				Control parentSecond = borderedControl.panelExTop.Parent;
				borderedControl.panelExTop.Parent = null;
				borderedControl.panelExTop.Controls.Clear();
				borderedControl.OutputClick = null;

				if (FormMain.Instance.buttonItemLeadoffStatement != null && FormMain.Instance.buttonItemLeadoffStatement.Checked)
				{
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _leadOffHelpToolTip);
					borderedControl.panelExTop.Controls.Add(LeadoffStatementControl.Instance);
					LeadoffStatementControl.Instance.EnableOutput = borderedControl.EnableOutputButton;
					LeadoffStatementControl.Instance.EnableSavedFiles = borderedControl.EnableSavedFilesButton;
					LeadoffStatementControl.Instance.UpdateOutputState();
					LeadoffStatementControl.Instance.UpdateSavedFilesState();
					borderedControl.OutputClick = LeadoffStatementControl.Instance.Output;
					borderedControl.SavedFilesClick = LeadoffStatementControl.Instance.LoadFromFile;
					FormMain.Instance.OutputClick = LeadoffStatementControl.Instance.Output;
				}
				else if (FormMain.Instance.buttonItemClientGoals != null && FormMain.Instance.buttonItemClientGoals.Checked)
				{
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _clientGoalsHelpToolTip);
					borderedControl.panelExTop.Controls.Add(ClientGoalsControl.Instance);
					ClientGoalsControl.Instance.EnableOutput = borderedControl.EnableOutputButton;
					ClientGoalsControl.Instance.EnableSavedFiles = borderedControl.EnableSavedFilesButton;
					ClientGoalsControl.Instance.UpdateOutputState();
					ClientGoalsControl.Instance.UpdateSavedFilesState();
					borderedControl.OutputClick = ClientGoalsControl.Instance.Output;
					borderedControl.SavedFilesClick = ClientGoalsControl.Instance.LoadFromFile;
					FormMain.Instance.OutputClick = ClientGoalsControl.Instance.Output;
				}
				else if (FormMain.Instance.buttonItemTargetCustomers != null && FormMain.Instance.buttonItemTargetCustomers.Checked)
				{
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _targetCustomersHelpToolTip);
					borderedControl.panelExTop.Controls.Add(TargetCustomersControl.Instance);
					TargetCustomersControl.Instance.EnableOutput = borderedControl.EnableOutputButton;
					TargetCustomersControl.Instance.EnableSavedFiles = borderedControl.EnableSavedFilesButton;
					TargetCustomersControl.Instance.UpdateOutputState();
					TargetCustomersControl.Instance.UpdateSavedFilesState();
					borderedControl.OutputClick = TargetCustomersControl.Instance.Output;
					borderedControl.SavedFilesClick = TargetCustomersControl.Instance.LoadFromFile;
					FormMain.Instance.OutputClick = TargetCustomersControl.Instance.Output;
				}
				borderedControl.panelExTop.Parent = parentSecond;

				if (borderedControl.panelExTop.Controls.Count == 0)
				{
					FormMain.Instance.buttonItemHomeOverview.Checked = true;
					Controls.Clear();
					Controls.Add(TabHomeOverviewControl.Instance);
				}
			}
			Parent = parent;
		}
	}
}