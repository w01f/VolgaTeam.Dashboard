using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.RateCard
{
	[ToolboxItem(false)]
	public partial class RateCardControl : UserControl
	{
		public RateCardControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public void LoadRateCards()
		{
			if (BusinessWrapper.Instance.RateCardManager.RateCardFolders.Count > 0)
			{
				Controller.Instance.RateCardCombo.Properties.Items.Clear();
				Controller.Instance.RateCardCombo.Properties.Items.AddRange(BusinessWrapper.Instance.RateCardManager.RateCardFolders.Select(x => x.FolderName).ToArray());
				Controller.Instance.RateCardCombo.SelectedIndex = 0;
			}
		}

		public void comboBoxEditRateCards_EditValueChanged(object sender, EventArgs e)
		{
			int selectedIndex = Controller.Instance.RateCardCombo.SelectedIndex;
			if (selectedIndex >= 0)
			{
				RateCardFolder selectedFolder = BusinessWrapper.Instance.RateCardManager.RateCardFolders[selectedIndex];
				if (!Controls.Contains(selectedFolder.RateCardContainer))
				{
					selectedFolder.LoadRateCards();
					Controls.Add(selectedFolder.RateCardContainer);
				}
				selectedFolder.RateCardContainer.BringToFront();
			}
		}

		public void buttonItemRateCardHelp_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("ratecard");
		}
	}
}