namespace NewBizWiz.Calendar.Controls.PresentationClasses.Calendars
{
	public partial class BaseCalendarControl
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
			DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
			this.pnTop = new System.Windows.Forms.Panel();
			this.labelControlScheduleInfo = new DevExpress.XtraEditors.LabelControl();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.hyperLinkEditReset = new DevExpress.XtraEditors.HyperLinkEdit();
			this.pnEmpty = new System.Windows.Forms.Panel();
			this.pnMain = new System.Windows.Forms.Panel();
			this.retractableBarControl = new NewBizWiz.CommonGUI.RetractableBar.RetractableBarLeft();
			this.pictureBoxNoData = new System.Windows.Forms.PictureBox();
			this.pnTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.hyperLinkEditReset.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxNoData)).BeginInit();
			this.SuspendLayout();
			// 
			// pnTop
			// 
			this.pnTop.Controls.Add(this.labelControlScheduleInfo);
			this.pnTop.Controls.Add(this.hyperLinkEditReset);
			this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTop.Location = new System.Drawing.Point(270, 0);
			this.pnTop.Name = "pnTop";
			this.pnTop.Size = new System.Drawing.Size(383, 35);
			this.pnTop.TabIndex = 1;
			// 
			// labelControlScheduleInfo
			// 
			this.labelControlScheduleInfo.AllowHtmlString = true;
			this.labelControlScheduleInfo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlScheduleInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlScheduleInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelControlScheduleInfo.Location = new System.Drawing.Point(0, 0);
			this.labelControlScheduleInfo.Name = "labelControlScheduleInfo";
			this.labelControlScheduleInfo.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.labelControlScheduleInfo.Size = new System.Drawing.Size(257, 35);
			this.labelControlScheduleInfo.StyleController = this.styleController;
			this.labelControlScheduleInfo.TabIndex = 126;
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
			// hyperLinkEditReset
			// 
			this.hyperLinkEditReset.Dock = System.Windows.Forms.DockStyle.Right;
			this.hyperLinkEditReset.EditValue = "Reset Defaults";
			this.hyperLinkEditReset.Location = new System.Drawing.Point(257, 0);
			this.hyperLinkEditReset.Name = "hyperLinkEditReset";
			this.hyperLinkEditReset.Properties.AllowFocused = false;
			this.hyperLinkEditReset.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.hyperLinkEditReset.Properties.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.hyperLinkEditReset.Properties.Appearance.ForeColor = System.Drawing.Color.DarkBlue;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseBackColor = true;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseFont = true;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseForeColor = true;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseTextOptions = true;
			this.hyperLinkEditReset.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.hyperLinkEditReset.Properties.AutoHeight = false;
			this.hyperLinkEditReset.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.hyperLinkEditReset.Size = new System.Drawing.Size(126, 35);
			toolTipItem1.Text = "Reset original default data";
			superToolTip1.Items.Add(toolTipItem1);
			this.hyperLinkEditReset.SuperTip = superToolTip1;
			this.hyperLinkEditReset.TabIndex = 104;
			this.hyperLinkEditReset.Visible = false;
			// 
			// pnEmpty
			// 
			this.pnEmpty.Location = new System.Drawing.Point(485, 60);
			this.pnEmpty.Name = "pnEmpty";
			this.pnEmpty.Size = new System.Drawing.Size(139, 175);
			this.pnEmpty.TabIndex = 3;
			// 
			// pnMain
			// 
			this.pnMain.Location = new System.Drawing.Point(485, 287);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(139, 175);
			this.pnMain.TabIndex = 4;
			// 
			// retractableBarControl
			// 
			this.retractableBarControl.AnimationDelay = 0;
			this.retractableBarControl.BackColor = System.Drawing.Color.Transparent;
			// 
			// retractableBarControl.Content
			// 
			this.retractableBarControl.Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.retractableBarControl.Content.Location = new System.Drawing.Point(2, 42);
			this.retractableBarControl.Content.Name = "Content";
			this.retractableBarControl.Content.Size = new System.Drawing.Size(266, 475);
			this.retractableBarControl.Content.TabIndex = 1;
			this.retractableBarControl.ContentSize = 300;
			this.retractableBarControl.Dock = System.Windows.Forms.DockStyle.Left;
			this.retractableBarControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			// 
			// retractableBarControl.Header
			// 
			this.retractableBarControl.Header.Dock = System.Windows.Forms.DockStyle.Fill;
			this.retractableBarControl.Header.Location = new System.Drawing.Point(49, 2);
			this.retractableBarControl.Header.Name = "Header";
			this.retractableBarControl.Header.Size = new System.Drawing.Size(215, 36);
			this.retractableBarControl.Header.TabIndex = 2;
			this.retractableBarControl.Location = new System.Drawing.Point(0, 0);
			this.retractableBarControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.retractableBarControl.Name = "retractableBarControl";
			this.retractableBarControl.Size = new System.Drawing.Size(270, 519);
			this.retractableBarControl.TabIndex = 6;
			// 
			// pictureBoxNoData
			// 
			this.pictureBoxNoData.BackColor = System.Drawing.Color.White;
			this.pictureBoxNoData.Location = new System.Drawing.Point(322, 279);
			this.pictureBoxNoData.Name = "pictureBoxNoData";
			this.pictureBoxNoData.Size = new System.Drawing.Size(111, 183);
			this.pictureBoxNoData.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBoxNoData.TabIndex = 5;
			this.pictureBoxNoData.TabStop = false;
			this.pictureBoxNoData.Visible = false;
			// 
			// BaseCalendarControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pictureBoxNoData);
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.pnTop);
			this.Controls.Add(this.pnEmpty);
			this.Controls.Add(this.retractableBarControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "BaseCalendarControl";
			this.Size = new System.Drawing.Size(653, 519);
			this.pnTop.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.hyperLinkEditReset.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxNoData)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		protected System.Windows.Forms.Panel pnTop;
		private DevExpress.XtraEditors.StyleController styleController;
		private System.Windows.Forms.Panel pnEmpty;
		protected System.Windows.Forms.Panel pnMain;
		protected System.Windows.Forms.PictureBox pictureBoxNoData;
		protected DevExpress.XtraEditors.HyperLinkEdit hyperLinkEditReset;
		protected CommonGUI.RetractableBar.RetractableBarLeft retractableBarControl;
		protected DevExpress.XtraEditors.LabelControl labelControlScheduleInfo;
    }
}
