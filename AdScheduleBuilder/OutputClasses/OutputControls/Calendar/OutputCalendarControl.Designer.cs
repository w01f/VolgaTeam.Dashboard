namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    partial class OutputCalendarControl
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
            this.laScheduleWindow = new System.Windows.Forms.Label();
            this.laAdvertiser = new System.Windows.Forms.Label();
            this.laScheduleName = new System.Windows.Forms.Label();
            this.pnMain = new System.Windows.Forms.Panel();
            this.pnCalendarView = new System.Windows.Forms.Panel();
            this.pnEmpty = new System.Windows.Forms.Panel();
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.comboBoxEditMonthSelector = new DevExpress.XtraEditors.ComboBoxEdit();
            this.pbCalendarLogo = new System.Windows.Forms.PictureBox();
            this.pnMonthSelector = new System.Windows.Forms.Panel();
            this.pnHeader.SuspendLayout();
            this.pnMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditMonthSelector.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCalendarLogo)).BeginInit();
            this.pnMonthSelector.SuspendLayout();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // pnHeader
            // 
            this.pnHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.pnHeader.Controls.Add(this.laScheduleWindow);
            this.pnHeader.Controls.Add(this.laAdvertiser);
            this.pnHeader.Controls.Add(this.laScheduleName);
            this.pnHeader.Controls.Add(this.pnMonthSelector);
            this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnHeader.Location = new System.Drawing.Point(0, 0);
            this.pnHeader.Name = "pnHeader";
            this.pnHeader.Size = new System.Drawing.Size(737, 41);
            this.pnHeader.TabIndex = 4;
            // 
            // laScheduleWindow
            // 
            this.laScheduleWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.laScheduleWindow.Location = new System.Drawing.Point(418, 0);
            this.laScheduleWindow.Name = "laScheduleWindow";
            this.laScheduleWindow.Size = new System.Drawing.Size(150, 41);
            this.laScheduleWindow.TabIndex = 1;
            this.laScheduleWindow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // laAdvertiser
            // 
            this.laAdvertiser.Dock = System.Windows.Forms.DockStyle.Left;
            this.laAdvertiser.Location = new System.Drawing.Point(167, 0);
            this.laAdvertiser.Name = "laAdvertiser";
            this.laAdvertiser.Size = new System.Drawing.Size(251, 41);
            this.laAdvertiser.TabIndex = 2;
            this.laAdvertiser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // laScheduleName
            // 
            this.laScheduleName.Dock = System.Windows.Forms.DockStyle.Right;
            this.laScheduleName.Location = new System.Drawing.Point(568, 0);
            this.laScheduleName.Name = "laScheduleName";
            this.laScheduleName.Size = new System.Drawing.Size(169, 41);
            this.laScheduleName.TabIndex = 0;
            this.laScheduleName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnMain
            // 
            this.pnMain.Controls.Add(this.pnCalendarView);
            this.pnMain.Controls.Add(this.pnEmpty);
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Location = new System.Drawing.Point(0, 41);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(737, 389);
            this.pnMain.TabIndex = 7;
            // 
            // pnCalendarView
            // 
            this.pnCalendarView.BackColor = System.Drawing.Color.AliceBlue;
            this.pnCalendarView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnCalendarView.Location = new System.Drawing.Point(0, 0);
            this.pnCalendarView.Name = "pnCalendarView";
            this.pnCalendarView.Size = new System.Drawing.Size(737, 389);
            this.pnCalendarView.TabIndex = 6;
            // 
            // pnEmpty
            // 
            this.pnEmpty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnEmpty.Location = new System.Drawing.Point(0, 0);
            this.pnEmpty.Name = "pnEmpty";
            this.pnEmpty.Size = new System.Drawing.Size(737, 389);
            this.pnEmpty.TabIndex = 7;
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
            // comboBoxEditMonthSelector
            // 
            this.comboBoxEditMonthSelector.Location = new System.Drawing.Point(41, 9);
            this.comboBoxEditMonthSelector.Name = "comboBoxEditMonthSelector";
            this.comboBoxEditMonthSelector.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditMonthSelector.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditMonthSelector.Size = new System.Drawing.Size(120, 22);
            this.comboBoxEditMonthSelector.StyleController = this.styleController;
            this.comboBoxEditMonthSelector.TabIndex = 3;
            this.comboBoxEditMonthSelector.SelectedIndexChanged += new System.EventHandler(this.comboBoxEditMonthSelector_SelectedIndexChanged);
            // 
            // pbCalendarLogo
            // 
            this.pbCalendarLogo.Image = global::AdScheduleBuilder.Properties.Resources.Calendar;
            this.pbCalendarLogo.Location = new System.Drawing.Point(3, 4);
            this.pbCalendarLogo.Name = "pbCalendarLogo";
            this.pbCalendarLogo.Size = new System.Drawing.Size(32, 32);
            this.pbCalendarLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCalendarLogo.TabIndex = 4;
            this.pbCalendarLogo.TabStop = false;
            // 
            // pnMonthSelector
            // 
            this.pnMonthSelector.Controls.Add(this.comboBoxEditMonthSelector);
            this.pnMonthSelector.Controls.Add(this.pbCalendarLogo);
            this.pnMonthSelector.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnMonthSelector.Location = new System.Drawing.Point(0, 0);
            this.pnMonthSelector.Name = "pnMonthSelector";
            this.pnMonthSelector.Size = new System.Drawing.Size(167, 41);
            this.pnMonthSelector.TabIndex = 3;
            // 
            // OutputCalendarControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.pnHeader);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "OutputCalendarControl";
            this.Size = new System.Drawing.Size(737, 430);
            this.pnHeader.ResumeLayout(false);
            this.pnMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditMonthSelector.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCalendarLogo)).EndInit();
            this.pnMonthSelector.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private System.Windows.Forms.Panel pnHeader;
        private System.Windows.Forms.Label laScheduleWindow;
        private System.Windows.Forms.Label laAdvertiser;
        private System.Windows.Forms.Label laScheduleName;
        private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.Panel pnEmpty;
        private System.Windows.Forms.Panel pnCalendarView;
        private System.Windows.Forms.Panel pnMonthSelector;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditMonthSelector;
        private DevExpress.XtraEditors.StyleController styleController;
        private System.Windows.Forms.PictureBox pbCalendarLogo;

    }
}
