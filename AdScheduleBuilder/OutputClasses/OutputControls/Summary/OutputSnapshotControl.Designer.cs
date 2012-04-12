namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    partial class OutputSnapshotControl
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
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.pnHeader = new System.Windows.Forms.Panel();
            this.comboBoxEditSchedule = new DevExpress.XtraEditors.ComboBoxEdit();
            this.checkEditSchedule = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditDate = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditDecisionMaker = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditFlightDates = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditBusinessName = new DevExpress.XtraEditors.CheckEdit();
            this.pnLine = new System.Windows.Forms.Panel();
            this.pnTopHeader = new System.Windows.Forms.Panel();
            this.laScheduleWindow = new System.Windows.Forms.Label();
            this.laAdvertiser = new System.Windows.Forms.Label();
            this.laScheduleName = new System.Windows.Forms.Label();
            this.xtraScrollableControl = new DevExpress.XtraEditors.XtraScrollableControl();
            this.outputSnapshotContainer = new AdScheduleBuilder.OutputClasses.OutputControls.OutputSnapshotContainer();
            this.pnMain = new System.Windows.Forms.Panel();
            this.pnHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSchedule.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditSchedule.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditDecisionMaker.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditFlightDates.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditBusinessName.Properties)).BeginInit();
            this.pnTopHeader.SuspendLayout();
            this.xtraScrollableControl.SuspendLayout();
            this.pnMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // pnHeader
            // 
            this.pnHeader.Controls.Add(this.comboBoxEditSchedule);
            this.pnHeader.Controls.Add(this.checkEditSchedule);
            this.pnHeader.Controls.Add(this.checkEditDate);
            this.pnHeader.Controls.Add(this.checkEditDecisionMaker);
            this.pnHeader.Controls.Add(this.checkEditFlightDates);
            this.pnHeader.Controls.Add(this.checkEditBusinessName);
            this.pnHeader.Controls.Add(this.pnLine);
            this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnHeader.Location = new System.Drawing.Point(0, 30);
            this.pnHeader.Name = "pnHeader";
            this.pnHeader.Size = new System.Drawing.Size(792, 108);
            this.pnHeader.TabIndex = 0;
            // 
            // comboBoxEditSchedule
            // 
            this.comboBoxEditSchedule.Location = new System.Drawing.Point(28, 9);
            this.comboBoxEditSchedule.Name = "comboBoxEditSchedule";
            this.comboBoxEditSchedule.Properties.Appearance.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxEditSchedule.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEditSchedule.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditSchedule.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditSchedule.Size = new System.Drawing.Size(389, 28);
            this.comboBoxEditSchedule.TabIndex = 43;
            this.comboBoxEditSchedule.EditValueChanged += new System.EventHandler(this.comboBoxEditSchedule_EditValueChanged);
            // 
            // checkEditSchedule
            // 
            this.checkEditSchedule.AutoSizeInLayoutControl = true;
            this.checkEditSchedule.EditValue = true;
            this.checkEditSchedule.Location = new System.Drawing.Point(3, 14);
            this.checkEditSchedule.Name = "checkEditSchedule";
            this.checkEditSchedule.Properties.Caption = "";
            this.checkEditSchedule.Size = new System.Drawing.Size(19, 19);
            this.checkEditSchedule.TabIndex = 42;
            this.checkEditSchedule.CheckedChanged += new System.EventHandler(this.checkEditSchedule_CheckedChanged);
            // 
            // checkEditDate
            // 
            this.checkEditDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkEditDate.EditValue = true;
            this.checkEditDate.Location = new System.Drawing.Point(634, 14);
            this.checkEditDate.Name = "checkEditDate";
            this.checkEditDate.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkEditDate.Properties.Appearance.Options.UseFont = true;
            this.checkEditDate.Properties.Appearance.Options.UseTextOptions = true;
            this.checkEditDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.checkEditDate.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.checkEditDate.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.checkEditDate.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.checkEditDate.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.checkEditDate.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.checkEditDate.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.checkEditDate.Properties.Caption = "Date Tag";
            this.checkEditDate.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.checkEditDate.Size = new System.Drawing.Size(155, 21);
            this.checkEditDate.TabIndex = 21;
            this.checkEditDate.CheckedChanged += new System.EventHandler(this.checkEdit_CheckedChanged);
            this.checkEditDate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkEdit_MouseDown);
            // 
            // checkEditDecisionMaker
            // 
            this.checkEditDecisionMaker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkEditDecisionMaker.EditValue = true;
            this.checkEditDecisionMaker.Location = new System.Drawing.Point(520, 54);
            this.checkEditDecisionMaker.Name = "checkEditDecisionMaker";
            this.checkEditDecisionMaker.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkEditDecisionMaker.Properties.Appearance.Options.UseFont = true;
            this.checkEditDecisionMaker.Properties.Appearance.Options.UseTextOptions = true;
            this.checkEditDecisionMaker.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.checkEditDecisionMaker.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.checkEditDecisionMaker.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.checkEditDecisionMaker.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.checkEditDecisionMaker.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.checkEditDecisionMaker.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.checkEditDecisionMaker.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.checkEditDecisionMaker.Properties.Caption = "Decision-Maker Tag";
            this.checkEditDecisionMaker.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.checkEditDecisionMaker.Size = new System.Drawing.Size(269, 21);
            this.checkEditDecisionMaker.TabIndex = 20;
            this.checkEditDecisionMaker.CheckedChanged += new System.EventHandler(this.checkEdit_CheckedChanged);
            this.checkEditDecisionMaker.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkEdit_MouseDown);
            // 
            // checkEditFlightDates
            // 
            this.checkEditFlightDates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkEditFlightDates.EditValue = true;
            this.checkEditFlightDates.Location = new System.Drawing.Point(3, 81);
            this.checkEditFlightDates.Name = "checkEditFlightDates";
            this.checkEditFlightDates.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkEditFlightDates.Properties.Appearance.Options.UseFont = true;
            this.checkEditFlightDates.Properties.Caption = "Flight Dates";
            this.checkEditFlightDates.Size = new System.Drawing.Size(786, 21);
            this.checkEditFlightDates.TabIndex = 19;
            this.checkEditFlightDates.CheckedChanged += new System.EventHandler(this.checkEdit_CheckedChanged);
            this.checkEditFlightDates.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkEdit_MouseDown);
            // 
            // checkEditBusinessName
            // 
            this.checkEditBusinessName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkEditBusinessName.EditValue = true;
            this.checkEditBusinessName.Location = new System.Drawing.Point(3, 54);
            this.checkEditBusinessName.Name = "checkEditBusinessName";
            this.checkEditBusinessName.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkEditBusinessName.Properties.Appearance.Options.UseFont = true;
            this.checkEditBusinessName.Properties.Caption = "Business Name Tag";
            this.checkEditBusinessName.Size = new System.Drawing.Size(511, 21);
            this.checkEditBusinessName.TabIndex = 18;
            this.checkEditBusinessName.CheckedChanged += new System.EventHandler(this.checkEdit_CheckedChanged);
            this.checkEditBusinessName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkEdit_MouseDown);
            // 
            // pnLine
            // 
            this.pnLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnLine.Location = new System.Drawing.Point(2, 43);
            this.pnLine.Name = "pnLine";
            this.pnLine.Size = new System.Drawing.Size(789, 1);
            this.pnLine.TabIndex = 12;
            // 
            // pnTopHeader
            // 
            this.pnTopHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.pnTopHeader.Controls.Add(this.laScheduleWindow);
            this.pnTopHeader.Controls.Add(this.laAdvertiser);
            this.pnTopHeader.Controls.Add(this.laScheduleName);
            this.pnTopHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTopHeader.Location = new System.Drawing.Point(0, 0);
            this.pnTopHeader.Name = "pnTopHeader";
            this.pnTopHeader.Size = new System.Drawing.Size(792, 30);
            this.pnTopHeader.TabIndex = 4;
            // 
            // laScheduleWindow
            // 
            this.laScheduleWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.laScheduleWindow.Location = new System.Drawing.Point(300, 0);
            this.laScheduleWindow.Name = "laScheduleWindow";
            this.laScheduleWindow.Size = new System.Drawing.Size(192, 30);
            this.laScheduleWindow.TabIndex = 1;
            this.laScheduleWindow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // laAdvertiser
            // 
            this.laAdvertiser.Dock = System.Windows.Forms.DockStyle.Left;
            this.laAdvertiser.Location = new System.Drawing.Point(0, 0);
            this.laAdvertiser.Name = "laAdvertiser";
            this.laAdvertiser.Size = new System.Drawing.Size(300, 30);
            this.laAdvertiser.TabIndex = 2;
            this.laAdvertiser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // laScheduleName
            // 
            this.laScheduleName.Dock = System.Windows.Forms.DockStyle.Right;
            this.laScheduleName.Location = new System.Drawing.Point(492, 0);
            this.laScheduleName.Name = "laScheduleName";
            this.laScheduleName.Size = new System.Drawing.Size(300, 30);
            this.laScheduleName.TabIndex = 0;
            this.laScheduleName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // xtraScrollableControl
            // 
            this.xtraScrollableControl.AlwaysScrollActiveControlIntoView = false;
            this.xtraScrollableControl.Controls.Add(this.outputSnapshotContainer);
            this.xtraScrollableControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControl.Name = "xtraScrollableControl";
            this.xtraScrollableControl.Size = new System.Drawing.Size(788, 288);
            this.xtraScrollableControl.TabIndex = 6;
            // 
            // outputSnapshotContainer
            // 
            this.outputSnapshotContainer.BackColor = System.Drawing.Color.AliceBlue;
            this.outputSnapshotContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.outputSnapshotContainer.Location = new System.Drawing.Point(0, 0);
            this.outputSnapshotContainer.Name = "outputSnapshotContainer";
            this.outputSnapshotContainer.Size = new System.Drawing.Size(771, 290);
            this.outputSnapshotContainer.TabIndex = 5;
            // 
            // pnMain
            // 
            this.pnMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnMain.Controls.Add(this.xtraScrollableControl);
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Location = new System.Drawing.Point(0, 138);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(792, 292);
            this.pnMain.TabIndex = 7;
            // 
            // OutputSnapshotControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.pnHeader);
            this.Controls.Add(this.pnTopHeader);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "OutputSnapshotControl";
            this.Size = new System.Drawing.Size(792, 430);
            this.pnHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSchedule.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditSchedule.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditDecisionMaker.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditFlightDates.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditBusinessName.Properties)).EndInit();
            this.pnTopHeader.ResumeLayout(false);
            this.xtraScrollableControl.ResumeLayout(false);
            this.pnMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private System.Windows.Forms.Panel pnHeader;
        private System.Windows.Forms.Panel pnLine;
        public DevExpress.XtraEditors.CheckEdit checkEditBusinessName;
        public DevExpress.XtraEditors.CheckEdit checkEditFlightDates;
        public DevExpress.XtraEditors.CheckEdit checkEditDate;
        public DevExpress.XtraEditors.CheckEdit checkEditDecisionMaker;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSchedule;
        private DevExpress.XtraEditors.CheckEdit checkEditSchedule;
        private System.Windows.Forms.Panel pnTopHeader;
        private System.Windows.Forms.Label laScheduleWindow;
        private System.Windows.Forms.Label laAdvertiser;
        private System.Windows.Forms.Label laScheduleName;
        private OutputSnapshotContainer outputSnapshotContainer;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl;
        private System.Windows.Forms.Panel pnMain;

    }
}
