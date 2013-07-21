using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CommandCentral
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class DataControl : UserControl
    {
        private static DataControl _instance = null;

        public AppManager.NoParamDelegate ViewSource { get; set; }

        public string ButtonText
        {
            set
            {
                buttonXSourceFile.Text = value;
            }
        }

        private DataControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public static DataControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DataControl();
                return _instance;
            }
        }

        private void buttonXSourceFile_Click(object sender, EventArgs e)
        {
            this.ViewSource();
        }
    }
}
