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
			this.components = new System.ComponentModel.Container();
			DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
			this.pnTop = new System.Windows.Forms.Panel();
			this.hyperLinkEditReset = new DevExpress.XtraEditors.HyperLinkEdit();
			this.laCalendarWindow = new System.Windows.Forms.Label();
			this.laCalendarName = new System.Windows.Forms.Label();
			this.laAdvertiser = new System.Windows.Forms.Label();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.pnEmpty = new System.Windows.Forms.Panel();
			this.pnMain = new System.Windows.Forms.Panel();
			this.pictureBoxNoData = new System.Windows.Forms.PictureBox();
			this.retractableBarControl = new NewBizWiz.CommonGUI.RetractableBar.RetractableBarLeft();
			this.pnTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.hyperLinkEditReset.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxNoData)).BeginInit();
			this.SuspendLayout();
			// 
			// pnTop
			// 
			this.pnTop.Controls.Add(this.hyperLinkEditReset);
			this.pnTop.Controls.Add(this.laCalendarWindow);
			this.pnTop.Controls.Add(this.laCalendarName);
			this.pnTop.Controls.Add(this.laAdvertiser);
			this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTop.Location = new System.Drawing.Point(270, 0);
			this.pnTop.Name = "pnTop";
			this.pnTop.Size = new System.Drawing.Size(383, 35);
			this.pnTop.TabIndex = 1;
			// 
			// hyperLinkEditReset
			// 
			this.hyperLinkEditReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.hyperLinkEditReset.EditValue = "Reset";
			this.hyperLinkEditReset.Location = new System.Drawing.Point(316, 4);
			this.hyperLinkEditReset.Name = "hyperLinkEditReset";
			this.hyperLinkEditReset.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.hyperLinkEditReset.Properties.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.hyperLinkEditReset.Properties.Appearance.ForeColor = System.Drawing.Color.DarkBlue;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseBackColor = true;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseFont = true;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseForeColor = true;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseTextOptions = true;
			this.hyperLinkEditReset.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.hyperLinkEditReset.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.hyperLinkEditReset.Size = new System.Drawing.Size(64, 22);
			toolTipItem1.Text = "Reset original default data";
			superToolTip1.Items.Add(toolTipItem1);
			this.hyperLinkEditReset.SuperTip = superToolTip1;
			this.hyperLinkEditReset.TabIndex = 104;
			this.hyperLinkEditReset.Visible = false;
			// 
			// laCalendarWindow
			// 
			this.laCalendarWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laCalendarWindow.Location = new System.Drawing.Point(248, 0);
			this.laCalendarWindow.Name = "laCalendarWindow";
			this.laCalendarWindow.Size = new System.Drawing.Size(0, 35);
			this.laCalendarWindow.TabIndex = 3;
			this.laCalendarWindow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// laCalendarName
			// 
			this.laCalendarName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laCalendarName.Location = new System.Drawing.Point(171, 0);
			this.laCalendarName.Name = "laCalendarName";
			this.laCalendarName.Size = new System.Drawing.Size(211, 35);
			this.laCalendarName.TabIndex = 1;
			this.laCalendarName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// laAdvertiser
			// 
			this.laAdvertiser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.laAdvertiser.Location = new System.Drawing.Point(0, 0);
			this.laAdvertiser.Name = "laAdvertiser";
			this.laAdvertiser.Size = new System.Drawing.Size(248, 35);
			this.laAdvertiser.TabIndex = 4;
			this.laAdvertiser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			this.retractableBarControl.Location = new System.Drawing.Point(0, 0);
			this.retractableBarControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.retractableBarControl.Name = "retractableBarControl";
			this.retractableBarControl.Size = new System.Drawing.Size(270, 519);
			this.retractableBarControl.TabIndex = 6;
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
			((System.ComponentModel.ISupportInitialize)(this.hyperLinkEditReset.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxNoData)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		protected System.Windows.Forms.Panel pnTop;
		private DevExpress.XtraEditors.StyleController styleController;
		private System.Windows.Forms.Panel pnEmpty;
		protected System.Windows.Forms.Panel pnMain;
		protected System.Windows.Forms.PictureBox pictureBoxNoData;
		protected System.Windows.Forms.Label laCalendarName;
		protected System.Windows.Forms.Label laCalendarWindow;
		protected System.Windows.Forms.Label laAdvertiser;
		protected DevExpress.XtraEditors.HyperLinkEdit hyperLinkEditReset;
		private CommonGUI.RetractableBar.RetractableBarLeft retractableBarControl;
    }
}
