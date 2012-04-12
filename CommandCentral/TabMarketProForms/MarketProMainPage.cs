using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommandCentral.TabMarketProForms
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class MarketProMainPage : UserControl
    {
        private static MarketProMainPage _instance = null;

        private AppManager.NoParamDelegate _viewFile;
        private AppManager.NoParamDelegate _updateData;

        private MarketProMainPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public static MarketProMainPage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MarketProMainPage();
                return _instance;
            }
        }

        private void UncheckButtons()
        {
            FormMain.Instance.buttonItemMarketProCable.Checked = false;
            FormMain.Instance.buttonItemMarketProDirectMail.Checked = false;
            FormMain.Instance.buttonItemMarketProMediaStrategy.Checked = false;
            FormMain.Instance.buttonItemMarketProMobile.Checked = false;
            FormMain.Instance.buttonItemMarketProOutdoor.Checked = false;
            FormMain.Instance.buttonItemMarketProPrint.Checked = false;
            FormMain.Instance.buttonItemMarketProRadio.Checked = false;
            FormMain.Instance.buttonItemMarketProTV.Checked = false;
            FormMain.Instance.buttonItemMarketProWeb.Checked = false;
            FormMain.Instance.buttonItemMarketProYellowPages.Checked = false;
        }

        public void UpdatePageAccordingToggledButton()
        {
            Control parent = pnMain.Parent;
            pnMain.Parent = null;
            pnMain.Controls.Clear();
            _viewFile = null;
            _updateData = null;
            if (FormMain.Instance.buttonItemMarketProCable != null && FormMain.Instance.buttonItemMarketProCable.Checked)
                MarketProManager.AssignMediaDataType(MediaDataType.Cable);
            else if (FormMain.Instance.buttonItemMarketProDirectMail != null && FormMain.Instance.buttonItemMarketProDirectMail.Checked)
                MarketProManager.AssignMediaDataType(MediaDataType.DirectMail);
            else if (FormMain.Instance.buttonItemMarketProMediaStrategy != null && FormMain.Instance.buttonItemMarketProMediaStrategy.Checked)
                MarketProManager.AssignMediaDataType(MediaDataType.MediaStrategy);
            else if (FormMain.Instance.buttonItemMarketProMobile != null && FormMain.Instance.buttonItemMarketProMobile.Checked)
                MarketProManager.AssignMediaDataType(MediaDataType.Mobile);
            else if (FormMain.Instance.buttonItemMarketProOutdoor != null && FormMain.Instance.buttonItemMarketProOutdoor.Checked)
                MarketProManager.AssignMediaDataType(MediaDataType.Outdoor);
            else if (FormMain.Instance.buttonItemMarketProPrint != null && FormMain.Instance.buttonItemMarketProPrint.Checked)
                MarketProManager.AssignMediaDataType(MediaDataType.Print);
            else if (FormMain.Instance.buttonItemMarketProRadio != null && FormMain.Instance.buttonItemMarketProRadio.Checked)
                MarketProManager.AssignMediaDataType(MediaDataType.Radio);
            else if (FormMain.Instance.buttonItemMarketProTV != null && FormMain.Instance.buttonItemMarketProTV.Checked)
                MarketProManager.AssignMediaDataType(MediaDataType.TV);
            else if (FormMain.Instance.buttonItemMarketProWeb != null && FormMain.Instance.buttonItemMarketProWeb.Checked)
                MarketProManager.AssignMediaDataType(MediaDataType.Internet);
            else if (FormMain.Instance.buttonItemMarketProYellowPages != null && FormMain.Instance.buttonItemMarketProYellowPages.Checked)
                MarketProManager.AssignMediaDataType(MediaDataType.YellowPages);

            DataControl.Instance.ViewSource = MarketProManager.ViewSourceFile;
            DataControl.Instance.ButtonText = MarketProManager.ButtonText;
            _viewFile = MarketProManager.ViewDataFile;
            _updateData = MarketProManager.UpdateData;
            pnMain.Controls.Add(DataControl.Instance);
            pnMain.Parent = parent;
            pnMain.BringToFront();
        }

        public void buttonItemMediaLibraryUpdate_Click(object sender, EventArgs e)
        {
            if(_updateData != null)
                _updateData();
        }

        public void buttonItemMediaLibraryViewFile_Click(object sender, EventArgs e)
        {
            if (_viewFile != null)
                _viewFile();
        }

        public void MediaLibraryButton_Click(object sender, EventArgs e)
        {
            UncheckButtons();
            if (sender != null && sender.GetType() == typeof(DevComponents.DotNetBar.ButtonItem))
                ((DevComponents.DotNetBar.ButtonItem)sender).Checked = true;
            UpdatePageAccordingToggledButton();
        }
    }
}
