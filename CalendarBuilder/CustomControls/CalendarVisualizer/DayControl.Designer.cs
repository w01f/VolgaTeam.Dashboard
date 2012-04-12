namespace CalendarBuilder.CustomControls.CalendarVisualizer
{
    partial class DayControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.laSmallDayCaption = new System.Windows.Forms.Label();
            this.pnData = new System.Windows.Forms.Panel();
            this.xtraScrollableControl = new DevExpress.XtraEditors.XtraScrollableControl();
            this.labelControlData = new DevExpress.XtraEditors.LabelControl();
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.pnData.SuspendLayout();
            this.xtraScrollableControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            this.SuspendLayout();
            // 
            // laSmallDayCaption
            // 
            this.laSmallDayCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.laSmallDayCaption.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.laSmallDayCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.laSmallDayCaption.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laSmallDayCaption.Location = new System.Drawing.Point(0, 0);
            this.laSmallDayCaption.Name = "laSmallDayCaption";
            this.laSmallDayCaption.Size = new System.Drawing.Size(276, 21);
            this.laSmallDayCaption.TabIndex = 0;
            this.laSmallDayCaption.Text = "label1";
            this.laSmallDayCaption.Click += new System.EventHandler(this.Control_Click);
            this.laSmallDayCaption.DoubleClick += new System.EventHandler(this.Control_DoubleClick);
            // 
            // pnData
            // 
            this.pnData.BackColor = System.Drawing.Color.AliceBlue;
            this.pnData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnData.Controls.Add(this.xtraScrollableControl);
            this.pnData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnData.Location = new System.Drawing.Point(0, 21);
            this.pnData.Name = "pnData";
            this.pnData.Size = new System.Drawing.Size(276, 226);
            this.pnData.TabIndex = 1;
            // 
            // xtraScrollableControl
            // 
            this.xtraScrollableControl.AlwaysScrollActiveControlIntoView = false;
            this.xtraScrollableControl.Appearance.BackColor = System.Drawing.Color.AliceBlue;
            this.xtraScrollableControl.Appearance.Options.UseBackColor = true;
            this.xtraScrollableControl.Controls.Add(this.labelControlData);
            this.xtraScrollableControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControl.Name = "xtraScrollableControl";
            this.xtraScrollableControl.Padding = new System.Windows.Forms.Padding(3);
            this.xtraScrollableControl.Size = new System.Drawing.Size(272, 222);
            this.xtraScrollableControl.TabIndex = 0;
            this.xtraScrollableControl.Click += new System.EventHandler(this.Control_Click);
            this.xtraScrollableControl.DoubleClick += new System.EventHandler(this.Control_DoubleClick);
            // 
            // labelControlData
            // 
            this.labelControlData.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControlData.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControlData.Location = new System.Drawing.Point(3, 3);
            this.labelControlData.Name = "labelControlData";
            this.labelControlData.Size = new System.Drawing.Size(266, 16);
            this.labelControlData.StyleController = this.styleController;
            this.labelControlData.TabIndex = 0;
            this.labelControlData.Text = "Data";
            this.labelControlData.Click += new System.EventHandler(this.Control_Click);
            this.labelControlData.DoubleClick += new System.EventHandler(this.Control_DoubleClick);
            // 
            // styleController
            // 
            this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.styleController.Appearance.Options.UseFont = true;
            this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDisabled.Options.UseFont = true;
            this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDropDown.Options.UseFont = true;
            this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
            this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceFocused.Options.UseFont = true;
            this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceReadOnly.Options.UseFont = true;
            // 
            // DayControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnData);
            this.Controls.Add(this.laSmallDayCaption);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Name = "DayControl";
            this.Size = new System.Drawing.Size(276, 247);
            this.pnData.ResumeLayout(false);
            this.xtraScrollableControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label laSmallDayCaption;
        private System.Windows.Forms.Panel pnData;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl;
        private DevExpress.XtraEditors.LabelControl labelControlData;
        private DevExpress.XtraEditors.StyleController styleController;

    }
}
