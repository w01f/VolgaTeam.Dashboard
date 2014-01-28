using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace CommandCentral.TabMainDashboard
{
	[ToolboxItem(false)]
	public partial class MainDashboardPage : UserControl
	{
		private static MainDashboardPage _instance;

		private AppManager.NoParamDelegate _updateData;
		private AppManager.NoParamDelegate _viewFile;

		private MainDashboardPage()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public static MainDashboardPage Instance
		{
			get
			{
				if (_instance == null)
					_instance = new MainDashboardPage();
				return _instance;
			}
		}

		private void UncheckButtons()
		{
			FormMain.Instance.buttonItemUsers.Checked = false;
			FormMain.Instance.buttonItemBasicClosingSummary.Checked = false;
			FormMain.Instance.buttonItemBasicCover.Checked = false;
			FormMain.Instance.buttonItemBasicIntroSlide.Checked = false;
			FormMain.Instance.buttonItemBasicNeedsAnalysis.Checked = false;
			FormMain.Instance.buttonItemBasicTargetCustomer.Checked = false;
			FormMain.Instance.buttonItemNewspaperStrategy.Checked = false;
			FormMain.Instance.buttonItemRadioStrategy.Checked = false;
			FormMain.Instance.buttonItemOnlineStrategy.Checked = false;
			FormMain.Instance.buttonItemTVStrategy.Checked = false;
		}

		public void UpdatePageAccordingToggledButton()
		{
			Control parent = pnMain.Parent;
			pnMain.Parent = null;
			pnMain.Controls.Clear();
			_viewFile = null;
			_updateData = null;
			if (FormMain.Instance.buttonItemUsers != null && FormMain.Instance.buttonItemUsers.Checked)
			{
				laTitle.Visible = false;
				pnMain.Controls.Add(UsersControl.Instance);
				_viewFile = UsersControl.Instance.ViewFile;
				_updateData = UsersControl.Instance.UpdateData;
				UsersControl.Instance.LoadButtons();
			}
			else if (FormMain.Instance.buttonItemBasicCover != null && FormMain.Instance.buttonItemBasicCover.Checked)
			{
				laTitle.Visible = true;
				DataControl.Instance.ViewSource = CoverManager.ViewSourceFile;
				DataControl.Instance.ButtonText = CoverManager.ButtonText;
				_viewFile = CoverManager.ViewDataFile;
				_updateData = CoverManager.UpdateData;
				pnMain.Controls.Add(DataControl.Instance);
			}
			else if (FormMain.Instance.buttonItemBasicIntroSlide != null && FormMain.Instance.buttonItemBasicIntroSlide.Checked)
			{
				laTitle.Visible = true;
				DataControl.Instance.ViewSource = IntroSlideManager.ViewSourceFile;
				DataControl.Instance.ButtonText = IntroSlideManager.ButtonText;
				_viewFile = IntroSlideManager.ViewDataFile;
				_updateData = IntroSlideManager.UpdateData;
				pnMain.Controls.Add(DataControl.Instance);
			}
			else if (FormMain.Instance.buttonItemBasicNeedsAnalysis != null && FormMain.Instance.buttonItemBasicNeedsAnalysis.Checked)
			{
				laTitle.Visible = true;
				DataControl.Instance.ViewSource = NeedsAnalysisManager.ViewSourceFile;
				DataControl.Instance.ButtonText = NeedsAnalysisManager.ButtonText;
				_viewFile = NeedsAnalysisManager.ViewDataFile;
				_updateData = NeedsAnalysisManager.UpdateData;
				pnMain.Controls.Add(DataControl.Instance);
			}
			else if (FormMain.Instance.buttonItemBasicTargetCustomer != null && FormMain.Instance.buttonItemBasicTargetCustomer.Checked)
			{
				laTitle.Visible = true;
				DataControl.Instance.ViewSource = TargetCustomerManager.ViewSourceFile;
				DataControl.Instance.ButtonText = TargetCustomerManager.ButtonText;
				_viewFile = TargetCustomerManager.ViewDataFile;
				_updateData = TargetCustomerManager.UpdateData;
				pnMain.Controls.Add(DataControl.Instance);
			}
			else if (FormMain.Instance.buttonItemBasicClosingSummary != null && FormMain.Instance.buttonItemBasicClosingSummary.Checked)
			{
				laTitle.Visible = true;
				DataControl.Instance.ViewSource = ClosingSummaryManager.ViewSourceFile;
				DataControl.Instance.ButtonText = ClosingSummaryManager.ButtonText;
				_viewFile = ClosingSummaryManager.ViewDataFile;
				_updateData = ClosingSummaryManager.UpdateData;
				pnMain.Controls.Add(DataControl.Instance);
			}
			else if (FormMain.Instance.buttonItemNewspaperStrategy != null && FormMain.Instance.buttonItemNewspaperStrategy.Checked)
			{
				laTitle.Visible = true;
				DataControl.Instance.ViewSource = NewspaperStrategyManager.ViewSourceFile;
				DataControl.Instance.ButtonText = NewspaperStrategyManager.ButtonText;
				_viewFile = NewspaperStrategyManager.ViewDataFile;
				_updateData = NewspaperStrategyManager.UpdateData;
				pnMain.Controls.Add(DataControl.Instance);
			}
			else if (FormMain.Instance.buttonItemRadioStrategy != null && FormMain.Instance.buttonItemRadioStrategy.Checked)
			{
				laTitle.Visible = true;
				DataControl.Instance.ViewSource = RadioStrategyManager.ViewSourceFile;
				DataControl.Instance.ButtonText = RadioStrategyManager.ButtonText;
				_viewFile = RadioStrategyManager.ViewDataFile;
				_updateData = RadioStrategyManager.UpdateData;
				pnMain.Controls.Add(DataControl.Instance);
			}
			else if (FormMain.Instance.buttonItemOnlineStrategy != null && FormMain.Instance.buttonItemOnlineStrategy.Checked)
			{
				laTitle.Visible = true;
				DataControl.Instance.ViewSource = OnlineStrategyManager.ViewSourceFile;
				DataControl.Instance.ButtonText = OnlineStrategyManager.ButtonText;
				_viewFile = OnlineStrategyManager.ViewDataFile;
				_updateData = OnlineStrategyManager.UpdateData;
				pnMain.Controls.Add(DataControl.Instance);
			}
			else if (FormMain.Instance.buttonItemTVStrategy != null && FormMain.Instance.buttonItemTVStrategy.Checked)
			{
				laTitle.Visible = true;
				DataControl.Instance.ViewSource = TVStrategyManager.ViewSourceFile;
				DataControl.Instance.ButtonText = TVStrategyManager.ButtonText;
				_viewFile = TVStrategyManager.ViewDataFile;
				_updateData = TVStrategyManager.UpdateData;
				pnMain.Controls.Add(DataControl.Instance);
			}
			else if (FormMain.Instance.buttonItemQuickList != null && FormMain.Instance.buttonItemQuickList.Checked)
			{
				laTitle.Visible = true;
				DataControl.Instance.ViewSource = QuickListManager.ViewSourceFile;
				DataControl.Instance.ButtonText = QuickListManager.ButtonText;
				_viewFile = QuickListManager.ViewDataFile;
				_updateData = QuickListManager.UpdateData;
				pnMain.Controls.Add(DataControl.Instance);
			}
			pnMain.Parent = parent;
			pnMain.BringToFront();
		}

		public void buttonItemMainDashboardUpdate_Click(object sender, EventArgs e)
		{
			if (_updateData != null)
				_updateData();
		}

		public void buttonItemMainDashboardViewFile_Click(object sender, EventArgs e)
		{
			if (_viewFile != null)
				_viewFile();
		}

		public void MainDashboardButton_Click(object sender, EventArgs e)
		{
			UncheckButtons();
			if (sender != null && sender.GetType() == typeof (ButtonItem))
				((ButtonItem) sender).Checked = true;
			UpdatePageAccordingToggledButton();
		}
	}
}