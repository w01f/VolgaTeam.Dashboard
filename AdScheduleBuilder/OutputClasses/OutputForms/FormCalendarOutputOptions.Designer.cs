namespace AdScheduleBuilder.OutputClasses.OutputForms
{
    partial class FormCalendarOutputOptions
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            this.pnMain = new System.Windows.Forms.Panel();
            this.pnSettingsViewer = new System.Windows.Forms.Panel();
            this.pnApplyForAll = new System.Windows.Forms.Panel();
            this.checkEditApplyForAll = new DevExpress.XtraEditors.CheckEdit();
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.listBoxControlMonth = new DevExpress.XtraEditors.ListBoxControl();
            this.pnTop = new System.Windows.Forms.Panel();
            this.laTitle = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.pnButtons = new System.Windows.Forms.Panel();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.pnMain.SuspendLayout();
            this.pnApplyForAll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditApplyForAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlMonth)).BeginInit();
            this.pnTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.pnButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonXOK
            // 
            this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXOK.ForeColor = System.Drawing.Color.Black;
            this.buttonXOK.Location = new System.Drawing.Point(436, 6);
            this.buttonXOK.Name = "buttonXOK";
            this.buttonXOK.Size = new System.Drawing.Size(107, 36);
            this.buttonXOK.TabIndex = 5;
            this.buttonXOK.Text = "OK";
            // 
            // buttonXCancel
            // 
            this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXCancel.ForeColor = System.Drawing.Color.Black;
            this.buttonXCancel.Location = new System.Drawing.Point(552, 6);
            this.buttonXCancel.Name = "buttonXCancel";
            this.buttonXCancel.Size = new System.Drawing.Size(107, 36);
            this.buttonXCancel.TabIndex = 6;
            this.buttonXCancel.Text = "Cancel";
            // 
            // pnMain
            // 
            this.pnMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnMain.Controls.Add(this.pnSettingsViewer);
            this.pnMain.Controls.Add(this.pnApplyForAll);
            this.pnMain.Controls.Add(this.listBoxControlMonth);
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Location = new System.Drawing.Point(0, 80);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(670, 367);
            this.pnMain.TabIndex = 18;
            // 
            // pnSettingsViewer
            // 
            this.pnSettingsViewer.BackColor = System.Drawing.Color.AliceBlue;
            this.pnSettingsViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnSettingsViewer.Location = new System.Drawing.Point(151, 0);
            this.pnSettingsViewer.Name = "pnSettingsViewer";
            this.pnSettingsViewer.Size = new System.Drawing.Size(515, 324);
            this.pnSettingsViewer.TabIndex = 50;
            // 
            // pnApplyForAll
            // 
            this.pnApplyForAll.BackColor = System.Drawing.Color.AliceBlue;
            this.pnApplyForAll.Controls.Add(this.checkEditApplyForAll);
            this.pnApplyForAll.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnApplyForAll.Location = new System.Drawing.Point(151, 324);
            this.pnApplyForAll.Name = "pnApplyForAll";
            this.pnApplyForAll.Size = new System.Drawing.Size(515, 39);
            this.pnApplyForAll.TabIndex = 51;
            // 
            // checkEditApplyForAll
            // 
            this.checkEditApplyForAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkEditApplyForAll.Location = new System.Drawing.Point(6, 9);
            this.checkEditApplyForAll.Name = "checkEditApplyForAll";
            this.checkEditApplyForAll.Properties.Caption = "Apply For All";
            this.checkEditApplyForAll.Size = new System.Drawing.Size(499, 21);
            this.checkEditApplyForAll.StyleController = this.styleController;
            this.checkEditApplyForAll.TabIndex = 0;
            // 
            // styleController
            // 
            this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.styleController.Appearance.Options.UseFont = true;
            this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.styleController.AppearanceDisabled.Options.UseFont = true;
            this.styleController.AppearanceDisabled.Options.UseForeColor = true;
            this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDropDown.Options.UseFont = true;
            this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
            this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceFocused.Options.UseFont = true;
            this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceReadOnly.ForeColor = System.Drawing.Color.Black;
            this.styleController.AppearanceReadOnly.Options.UseFont = true;
            this.styleController.AppearanceReadOnly.Options.UseForeColor = true;
            // 
            // listBoxControlMonth
            // 
            this.listBoxControlMonth.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBoxControlMonth.ItemHeight = 25;
            this.listBoxControlMonth.Location = new System.Drawing.Point(0, 0);
            this.listBoxControlMonth.Name = "listBoxControlMonth";
            this.listBoxControlMonth.Size = new System.Drawing.Size(151, 363);
            this.listBoxControlMonth.StyleController = this.styleController;
            this.listBoxControlMonth.TabIndex = 49;
            this.listBoxControlMonth.SelectedValueChanged += new System.EventHandler(this.listBoxControlMonth_SelectedValueChanged);
            // 
            // pnTop
            // 
            this.pnTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnTop.Controls.Add(this.laTitle);
            this.pnTop.Controls.Add(this.pbLogo);
            this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTop.Location = new System.Drawing.Point(0, 0);
            this.pnTop.Name = "pnTop";
            this.pnTop.Size = new System.Drawing.Size(670, 80);
            this.pnTop.TabIndex = 1;
            // 
            // laTitle
            // 
            this.laTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laTitle.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laTitle.Location = new System.Drawing.Point(81, 7);
            this.laTitle.Name = "laTitle";
            this.laTitle.Size = new System.Drawing.Size(578, 65);
            this.laTitle.TabIndex = 6;
            this.laTitle.Text = "Title";
            this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbLogo
            // 
            this.pbLogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pbLogo.Image = global::AdScheduleBuilder.Properties.Resources.CalendarNotes;
            this.pbLogo.Location = new System.Drawing.Point(9, 7);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(66, 65);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 5;
            this.pbLogo.TabStop = false;
            // 
            // pnButtons
            // 
            this.pnButtons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnButtons.Controls.Add(this.buttonXCancel);
            this.pnButtons.Controls.Add(this.buttonXOK);
            this.pnButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnButtons.Location = new System.Drawing.Point(0, 447);
            this.pnButtons.Name = "pnButtons";
            this.pnButtons.Size = new System.Drawing.Size(670, 53);
            this.pnButtons.TabIndex = 19;
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // FormCalendarOutputOptions
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(670, 500);
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.pnButtons);
            this.Controls.Add(this.pnTop);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(365, 260);
            this.Name = "FormCalendarOutputOptions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Slide Calendar Notes";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormCalendarNotes_FormClosed);
            this.Load += new System.EventHandler(this.FormCalendarNotes_Load);
            this.pnMain.ResumeLayout(false);
            this.pnApplyForAll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEditApplyForAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlMonth)).EndInit();
            this.pnTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.pnButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXOK;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.Panel pnTop;
        private System.Windows.Forms.Label laTitle;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Panel pnButtons;
        private DevExpress.XtraEditors.ListBoxControl listBoxControlMonth;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private System.Windows.Forms.Panel pnSettingsViewer;
        private System.Windows.Forms.Panel pnApplyForAll;
        private DevExpress.XtraEditors.CheckEdit checkEditApplyForAll;
    }
}