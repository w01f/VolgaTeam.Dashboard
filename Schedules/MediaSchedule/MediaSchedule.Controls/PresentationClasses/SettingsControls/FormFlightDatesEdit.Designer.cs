namespace Asa.Media.Controls.PresentationClasses.SettingsControls
{
	partial class FormFlightDatesEdit
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
			DevExpress.Utils.ContextButton contextButton1 = new DevExpress.Utils.ContextButton();
			this.pbLogo = new System.Windows.Forms.PictureBox();
			this.laTitle = new System.Windows.Forms.Label();
			this.buttonXSave = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.calendarControlDateStart = new DevExpress.XtraEditors.Controls.CalendarControl();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.labelControlStartTitle = new DevExpress.XtraEditors.LabelControl();
			this.pnTop = new System.Windows.Forms.Panel();
			this.pnBottomButtons = new System.Windows.Forms.Panel();
			this.pnCalendars = new System.Windows.Forms.Panel();
			this.labelControlEndTitle = new DevExpress.XtraEditors.LabelControl();
			this.calendarControlDateEnd = new DevExpress.XtraEditors.Controls.CalendarControl();
			this.labelControlWarnings = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.calendarControlDateStart.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.pnTop.SuspendLayout();
			this.pnBottomButtons.SuspendLayout();
			this.pnCalendars.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.calendarControlDateEnd.CalendarTimeProperties)).BeginInit();
			this.SuspendLayout();
			// 
			// pbLogo
			// 
			this.pbLogo.ForeColor = System.Drawing.Color.Black;
			this.pbLogo.Image = global::Asa.Media.Controls.Properties.Resources.FlightDatesFormEditLogo;
			this.pbLogo.Location = new System.Drawing.Point(20, 13);
			this.pbLogo.Name = "pbLogo";
			this.pbLogo.Size = new System.Drawing.Size(66, 65);
			this.pbLogo.TabIndex = 0;
			this.pbLogo.TabStop = false;
			// 
			// laTitle
			// 
			this.laTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laTitle.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.ForeColor = System.Drawing.Color.Black;
			this.laTitle.Location = new System.Drawing.Point(92, 13);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(459, 65);
			this.laTitle.TabIndex = 1;
			this.laTitle.Text = "Select your campaign dates:";
			this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// buttonXSave
			// 
			this.buttonXSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXSave.Location = new System.Drawing.Point(343, 7);
			this.buttonXSave.Name = "buttonXSave";
			this.buttonXSave.Size = new System.Drawing.Size(85, 36);
			this.buttonXSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSave.TabIndex = 2;
			this.buttonXSave.Text = "Save";
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(458, 7);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(85, 36);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 3;
			this.buttonXCancel.Text = "Cancel";
			// 
			// calendarControlDateStart
			// 
			this.calendarControlDateStart.Appearance.BackColor = System.Drawing.Color.White;
			this.calendarControlDateStart.Appearance.ForeColor = System.Drawing.Color.Black;
			this.calendarControlDateStart.Appearance.Options.UseBackColor = true;
			this.calendarControlDateStart.Appearance.Options.UseForeColor = true;
			this.calendarControlDateStart.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.False;
			this.calendarControlDateStart.CalendarTimeProperties.AllowFocused = false;
			this.calendarControlDateStart.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.calendarControlDateStart.Cursor = System.Windows.Forms.Cursors.Hand;
			this.calendarControlDateStart.DateTime = new System.DateTime(2016, 3, 6, 0, 0, 0, 0);
			this.calendarControlDateStart.EditValue = new System.DateTime(2016, 3, 6, 0, 0, 0, 0);
			this.calendarControlDateStart.FirstDayOfWeek = System.DayOfWeek.Monday;
			this.calendarControlDateStart.HighlightTodayCell = DevExpress.Utils.DefaultBoolean.False;
			this.calendarControlDateStart.Location = new System.Drawing.Point(20, 38);
			this.calendarControlDateStart.Name = "calendarControlDateStart";
			this.calendarControlDateStart.ShowFooter = false;
			this.calendarControlDateStart.ShowMonthHeaders = false;
			this.calendarControlDateStart.ShowMonthNavigationButtons = DevExpress.Utils.DefaultBoolean.True;
			this.calendarControlDateStart.ShowTodayButton = false;
			this.calendarControlDateStart.Size = new System.Drawing.Size(232, 204);
			this.calendarControlDateStart.StyleController = this.styleController;
			this.calendarControlDateStart.TabIndex = 0;
			this.calendarControlDateStart.TodayDate = new System.DateTime(2016, 3, 5, 0, 0, 0, 0);
			this.calendarControlDateStart.UpdateDateTimeWhenNavigating = false;
			this.calendarControlDateStart.DateTimeChanged += new System.EventHandler(this.OnDateStartChanged);
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
			// labelControlStartTitle
			// 
			this.labelControlStartTitle.AllowHtmlString = true;
			this.labelControlStartTitle.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlStartTitle.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlStartTitle.Location = new System.Drawing.Point(20, 16);
			this.labelControlStartTitle.Name = "labelControlStartTitle";
			this.labelControlStartTitle.Size = new System.Drawing.Size(32, 16);
			this.labelControlStartTitle.StyleController = this.styleController;
			this.labelControlStartTitle.TabIndex = 4;
			this.labelControlStartTitle.Text = "<color=\"gray\">Start:</color>";
			// 
			// pnTop
			// 
			this.pnTop.BackColor = System.Drawing.Color.Transparent;
			this.pnTop.Controls.Add(this.pbLogo);
			this.pnTop.Controls.Add(this.laTitle);
			this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTop.ForeColor = System.Drawing.Color.Black;
			this.pnTop.Location = new System.Drawing.Point(0, 0);
			this.pnTop.Name = "pnTop";
			this.pnTop.Size = new System.Drawing.Size(563, 90);
			this.pnTop.TabIndex = 5;
			// 
			// pnBottomButtons
			// 
			this.pnBottomButtons.BackColor = System.Drawing.Color.Transparent;
			this.pnBottomButtons.Controls.Add(this.buttonXSave);
			this.pnBottomButtons.Controls.Add(this.buttonXCancel);
			this.pnBottomButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnBottomButtons.ForeColor = System.Drawing.Color.Black;
			this.pnBottomButtons.Location = new System.Drawing.Point(0, 380);
			this.pnBottomButtons.Name = "pnBottomButtons";
			this.pnBottomButtons.Size = new System.Drawing.Size(563, 51);
			this.pnBottomButtons.TabIndex = 6;
			// 
			// pnCalendars
			// 
			this.pnCalendars.BackColor = System.Drawing.Color.Transparent;
			this.pnCalendars.Controls.Add(this.labelControlEndTitle);
			this.pnCalendars.Controls.Add(this.calendarControlDateEnd);
			this.pnCalendars.Controls.Add(this.labelControlWarnings);
			this.pnCalendars.Controls.Add(this.labelControlStartTitle);
			this.pnCalendars.Controls.Add(this.calendarControlDateStart);
			this.pnCalendars.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnCalendars.ForeColor = System.Drawing.Color.Black;
			this.pnCalendars.Location = new System.Drawing.Point(0, 90);
			this.pnCalendars.Name = "pnCalendars";
			this.pnCalendars.Size = new System.Drawing.Size(563, 290);
			this.pnCalendars.TabIndex = 8;
			// 
			// labelControlEndTitle
			// 
			this.labelControlEndTitle.AllowHtmlString = true;
			this.labelControlEndTitle.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlEndTitle.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlEndTitle.Location = new System.Drawing.Point(311, 16);
			this.labelControlEndTitle.Name = "labelControlEndTitle";
			this.labelControlEndTitle.Size = new System.Drawing.Size(27, 16);
			this.labelControlEndTitle.StyleController = this.styleController;
			this.labelControlEndTitle.TabIndex = 7;
			this.labelControlEndTitle.Text = "<color=\"gray\">End:</color>";
			// 
			// calendarControlDateEnd
			// 
			this.calendarControlDateEnd.Appearance.BackColor = System.Drawing.Color.White;
			this.calendarControlDateEnd.Appearance.ForeColor = System.Drawing.Color.Black;
			this.calendarControlDateEnd.Appearance.Options.UseBackColor = true;
			this.calendarControlDateEnd.Appearance.Options.UseForeColor = true;
			this.calendarControlDateEnd.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.False;
			this.calendarControlDateEnd.CalendarTimeProperties.AllowFocused = false;
			this.calendarControlDateEnd.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			contextButton1.Id = new System.Guid("c3c4a178-7140-43d4-add7-2c7d02fb2cae");
			contextButton1.Name = "ContextButton";
			this.calendarControlDateEnd.ContextButtons.Add(contextButton1);
			this.calendarControlDateEnd.Cursor = System.Windows.Forms.Cursors.Hand;
			this.calendarControlDateEnd.DateTime = new System.DateTime(2016, 3, 6, 0, 0, 0, 0);
			this.calendarControlDateEnd.EditValue = new System.DateTime(2016, 3, 6, 0, 0, 0, 0);
			this.calendarControlDateEnd.FirstDayOfWeek = System.DayOfWeek.Monday;
			this.calendarControlDateEnd.HighlightTodayCell = DevExpress.Utils.DefaultBoolean.False;
			this.calendarControlDateEnd.Location = new System.Drawing.Point(311, 38);
			this.calendarControlDateEnd.Name = "calendarControlDateEnd";
			this.calendarControlDateEnd.ShowFooter = false;
			this.calendarControlDateEnd.ShowMonthHeaders = false;
			this.calendarControlDateEnd.ShowMonthNavigationButtons = DevExpress.Utils.DefaultBoolean.True;
			this.calendarControlDateEnd.ShowTodayButton = false;
			this.calendarControlDateEnd.Size = new System.Drawing.Size(232, 204);
			this.calendarControlDateEnd.StyleController = this.styleController;
			this.calendarControlDateEnd.TabIndex = 6;
			this.calendarControlDateEnd.TodayDate = new System.DateTime(2016, 3, 5, 0, 0, 0, 0);
			this.calendarControlDateEnd.UpdateDateTimeWhenNavigating = false;
			this.calendarControlDateEnd.DateTimeChanged += new System.EventHandler(this.OnDateEndChanged);
			// 
			// labelControlWarnings
			// 
			this.labelControlWarnings.AllowHtmlString = true;
			this.labelControlWarnings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlWarnings.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlWarnings.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlWarnings.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlWarnings.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlWarnings.Location = new System.Drawing.Point(20, 242);
			this.labelControlWarnings.Name = "labelControlWarnings";
			this.labelControlWarnings.Size = new System.Drawing.Size(523, 44);
			this.labelControlWarnings.StyleController = this.styleController;
			this.labelControlWarnings.TabIndex = 5;
			this.labelControlWarnings.Text = "labelControl1";
			this.labelControlWarnings.Visible = false;
			// 
			// FormFlightDatesEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(563, 431);
			this.Controls.Add(this.pnCalendars);
			this.Controls.Add(this.pnBottomButtons);
			this.Controls.Add(this.pnTop);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormFlightDatesEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Campaign Dates";
			this.Load += new System.EventHandler(this.OnFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.calendarControlDateStart.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.pnTop.ResumeLayout(false);
			this.pnBottomButtons.ResumeLayout(false);
			this.pnCalendars.ResumeLayout(false);
			this.pnCalendars.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.calendarControlDateEnd.CalendarTimeProperties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pbLogo;
		private System.Windows.Forms.Label laTitle;
		private DevComponents.DotNetBar.ButtonX buttonXSave;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevExpress.XtraEditors.Controls.CalendarControl calendarControlDateStart;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.LabelControl labelControlStartTitle;
		private System.Windows.Forms.Panel pnTop;
		private System.Windows.Forms.Panel pnBottomButtons;
		private System.Windows.Forms.Panel pnCalendars;
		private DevExpress.XtraEditors.LabelControl labelControlEndTitle;
		private DevExpress.XtraEditors.Controls.CalendarControl calendarControlDateEnd;
		private DevExpress.XtraEditors.LabelControl labelControlWarnings;
	}
}