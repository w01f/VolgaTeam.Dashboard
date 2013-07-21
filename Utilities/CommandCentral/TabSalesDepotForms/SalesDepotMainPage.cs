using System;
using System.Windows.Forms;

namespace CommandCentral.TabSalesDepotForms
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class SalesDepotMainPage : UserControl
    {
        private static SalesDepotMainPage _instance = null;

        private AppManager.NoParamDelegate _viewFile;
        private AppManager.NoParamDelegate _updateData;

        private SalesDepotMainPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public static SalesDepotMainPage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SalesDepotMainPage();
                return _instance;
            }
        }

        private void UncheckButtons()
        {
            FormMain.Instance.buttonItemSalesDepotSearch.Checked = false;
            FormMain.Instance.buttonItemSalesDepotAccessRights.Checked = false;
        }

        public void UpdatePageAccordingToggledButton()
        {
            Control parent = pnMain.Parent;
            pnMain.Parent = null;
            pnMain.Controls.Clear();
            _viewFile = null;
            _updateData = null;
            if (FormMain.Instance.buttonItemSalesDepotSearch != null && FormMain.Instance.buttonItemSalesDepotSearch.Checked)
            {
                DataControl.Instance.ViewSource = SalesDepotSearchManager.ViewSourceFile;
                DataControl.Instance.ButtonText = SalesDepotSearchManager.ButtonText;
                _viewFile = SalesDepotSearchManager.ViewDataFile;
                _updateData = SalesDepotSearchManager.UpdateData;
            }
            else if (FormMain.Instance.buttonItemSalesDepotAccessRights != null && FormMain.Instance.buttonItemSalesDepotAccessRights.Checked)
            {
                DataControl.Instance.ViewSource = SalesDepotAccessRightsManager.ViewSourceFile;
                DataControl.Instance.ButtonText = SalesDepotAccessRightsManager.ButtonText;
                _viewFile = SalesDepotAccessRightsManager.ViewDataFile;
                _updateData = SalesDepotAccessRightsManager.UpdateData;
            }
            pnMain.Controls.Add(DataControl.Instance);
            pnMain.Parent = parent;
            pnMain.BringToFront();
        }

        public void buttonItemSalesDepotUpdate_Click(object sender, EventArgs e)
        {
            if(_updateData != null)
                _updateData();
        }

        public void buttonItemSalesDepotViewFile_Click(object sender, EventArgs e)
        {
            if (_viewFile != null)
                _viewFile();
        }

        public void SalesDepotButton_Click(object sender, EventArgs e)
        {
            UncheckButtons();
            if (sender != null && sender.GetType() == typeof(DevComponents.DotNetBar.ButtonItem))
                ((DevComponents.DotNetBar.ButtonItem)sender).Checked = true;
            UpdatePageAccordingToggledButton();
        }
    }
}
