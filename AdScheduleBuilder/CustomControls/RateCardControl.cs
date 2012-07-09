using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AdScheduleBuilder.CustomControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class RateCardControl : UserControl
    {
        private static RateCardControl _instance = null;

        public static RateCardControl Instance
        {
            get
            { 
                if(_instance == null)
                    _instance = new RateCardControl();
                return _instance;
            }
        }

        public static void RemoveInstance()
        {
            try
            {
                _instance.Dispose();
            }
            catch
            {
            }
            finally
            {
                _instance = null;
            }
        }

        private RateCardControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public void LoadRateCards()
        {
            if (BusinessClasses.RateCardManager.Instance.RateCardFolders.Count > 0)
            {
                FormMain.Instance.comboBoxEditRateCards.Properties.Items.Clear();
                FormMain.Instance.comboBoxEditRateCards.Properties.Items.AddRange(BusinessClasses.RateCardManager.Instance.RateCardFolders.Select(x => x.FolderName).ToArray());
                FormMain.Instance.comboBoxEditRateCards.SelectedIndex = 0;
            }
        }

        public void comboBoxEditRateCards_EditValueChanged(object sender, EventArgs e)
        {
            int selectedIndex = FormMain.Instance.comboBoxEditRateCards.SelectedIndex;
            if (selectedIndex >= 0)
            {
                BusinessClasses.RateCardFolder selectedFolder = BusinessClasses.RateCardManager.Instance.RateCardFolders[selectedIndex];
                if(!this.Controls.Contains(selectedFolder.RateCardContainer))
                {
                    selectedFolder.LoadRateCards();
                    this.Controls.Add(selectedFolder.RateCardContainer);
                }
                selectedFolder.RateCardContainer.BringToFront();
            }
        }

        public void buttonItemRateCardHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("ratecard");
        }
    }
}
