using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NewBizWizForm.TabHomeForms
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class SimpleSummaryOutputControl : UserControl
    {
        public int ItemNumber { get; set; }

        public SimpleSummaryOutputControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Top;
            AppManager.Instance.SetClickEventHandler(this);
            if ((base.CreateGraphics()).DpiX > 96)
            {
                ckDetails.Font = new Font(ckDetails.Font.FontFamily, ckDetails.Font.Size - 2, ckDetails.Font.Style);
                ckItem.Font = new Font(ckItem.Font.FontFamily, ckItem.Font.Size - 2, ckItem.Font.Style);
                ckMonthly.Font = new Font(ckMonthly.Font.FontFamily, ckMonthly.Font.Size - 2, ckMonthly.Font.Style);
                ckTotal.Font = new Font(ckTotal.Font.FontFamily, ckTotal.Font.Size - 2, ckTotal.Font.Style);
            }
        }

        public string ItemValue
        {
            set
            {
                ckItem.Text = value;
            }
        }

        public bool ItemChecked
        {
            get
            {
                return ckItem.Visible ? ckItem.Checked : false;
            }
        }

        public bool ItemVisible
        {
            set
            {
                ckItem.Visible = value;
            }
        }

        public string DetailsValue
        {
            set
            {
                ckDetails.Text = value;
            }
        }

        public bool DetailsChecked
        {
            get
            {
                return ckDetails.Visible ? ckDetails.Checked : false;
            }
        }

        public bool DetailsVisible
        {
            set
            {
                ckDetails.Visible = value;
            }
        }

        public string MonthlyValue
        {
            set
            {
                ckMonthly.Text = value;
            }
        }

        public bool MonthlyChecked
        {
            get
            {
                return ckMonthly.Checked;
            }
        }

        public bool MonthlyVisible
        {
            set
            {
                ckMonthly.Visible = value;
            }
        }

        public string ToatlValue
        {
            set
            {
                ckTotal.Text = value;
            }
        }

        public bool TotalChecked
        {
            get
            {
                return ckTotal.Checked;
            }
        }

        public bool TotalVisible
        {
            set
            {
                ckTotal.Visible = value;
            }
        }

        private void ckItem_CheckedChanged(object sender, EventArgs e)
        {
            if (SimpleSummaryControl.Instance.AllowToSave)
            {
                SimpleSummaryControl.Instance.UpdateOutputTotalValues();
                SimpleSummaryControl.Instance.SettingsNotSaved = true;
            }
        }
    }
}
