namespace NewBizWiz.OnlineSchedule.DigitalPackage.PresentationClasses
{
	partial class DigitalPackageControl
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
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.comboBoxEditAdvertiser = new DevExpress.XtraEditors.ComboBoxEdit();
			this.scheduleListControl = new NewBizWiz.OnlineSchedule.DigitalPackage.PresentationClasses.ScheduleListControl();
			this.laLastModified = new System.Windows.Forms.Label();
			this.pnHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.hyperLinkEditReset.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbDisabledOutput)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbFormualHelp)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditAdvertiser.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// pnHeader
			// 
			this.pnHeader.Controls.Add(this.comboBoxEditAdvertiser);
			this.pnHeader.Controls.Add(this.laLastModified);
			this.pnHeader.Controls.SetChildIndex(this.laLastModified, 0);
			this.pnHeader.Controls.SetChildIndex(this.laAdvertiser, 0);
			this.pnHeader.Controls.SetChildIndex(this.hyperLinkEditReset, 0);
			this.pnHeader.Controls.SetChildIndex(this.comboBoxEditAdvertiser, 0);
			// 
			// laAdvertiser
			// 
			this.laAdvertiser.Visible = false;
			// 
			// hyperLinkEditReset
			// 
			this.hyperLinkEditReset.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.hyperLinkEditReset.Properties.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.hyperLinkEditReset.Properties.Appearance.ForeColor = System.Drawing.Color.DarkBlue;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseBackColor = true;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseFont = true;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseForeColor = true;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseTextOptions = true;
			this.hyperLinkEditReset.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.hyperLinkEditReset.Visible = false;
			// 
			// pbFormualHelp
			// 
			this.pbFormualHelp.Image = global::NewBizWiz.OnlineSchedule.DigitalPackage.Properties.Resources.HelpSmall;
			this.pbFormualHelp.Click += new System.EventHandler(this.pbFormualHelp_Click);
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
			// comboBoxEditAdvertiser
			// 
			this.comboBoxEditAdvertiser.Location = new System.Drawing.Point(3, 10);
			this.comboBoxEditAdvertiser.Name = "comboBoxEditAdvertiser";
			this.comboBoxEditAdvertiser.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditAdvertiser.Properties.NullText = "Select or Type Advertiser...";
			this.comboBoxEditAdvertiser.Size = new System.Drawing.Size(297, 22);
			this.comboBoxEditAdvertiser.StyleController = this.styleController;
			this.comboBoxEditAdvertiser.TabIndex = 103;
			this.comboBoxEditAdvertiser.EditValueChanged += new System.EventHandler(this.comboBoxEditAdvertiser_EditValueChanged);
			// 
			// scheduleListControl
			// 
			this.scheduleListControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.scheduleListControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.scheduleListControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scheduleListControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.scheduleListControl.Location = new System.Drawing.Point(0, 0);
			this.scheduleListControl.Name = "scheduleListControl";
			this.scheduleListControl.Size = new System.Drawing.Size(230, 581);
			this.scheduleListControl.TabIndex = 55;
			this.scheduleListControl.ScheduleChanged += new System.EventHandler<NewBizWiz.OnlineSchedule.DigitalPackage.ScheduleEventArgs>(this.scheduleListControl_ScheduleChanged);
			this.scheduleListControl.ScheduleCreated += new System.EventHandler<System.EventArgs>(this.scheduleListControl_ScheduleCreated);
			this.scheduleListControl.ScheduleCloned += new System.EventHandler<System.EventArgs>(this.scheduleListControl_ScheduleCloned);
			this.scheduleListControl.ScheduleDeleted += new System.EventHandler<NewBizWiz.OnlineSchedule.DigitalPackage.ScheduleEventArgs>(this.scheduleListControl_ScheduleDeleted);
			// 
			// laLastModified
			// 
			this.laLastModified.Dock = System.Windows.Forms.DockStyle.Right;
			this.laLastModified.Location = new System.Drawing.Point(503, 0);
			this.laLastModified.Name = "laLastModified";
			this.laLastModified.Size = new System.Drawing.Size(300, 42);
			this.laLastModified.TabIndex = 104;
			this.laLastModified.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// DigitalPackageControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "DigitalPackageControl";
			this.pnHeader.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.hyperLinkEditReset.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbDisabledOutput)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbFormualHelp)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditAdvertiser.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditAdvertiser;
		private ScheduleListControl scheduleListControl;
		private System.Windows.Forms.Label laLastModified;
	}
}
