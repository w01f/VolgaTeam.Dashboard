namespace AdScheduleBuilder.ToolForms
{
    partial class FormCloneInsert
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCloneInsert));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            this.laOriginalDate = new System.Windows.Forms.Label();
            this.checkEditPCIRate = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditDiscount = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditColorRate = new DevExpress.XtraEditors.CheckEdit();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
            this.checkEditComment = new DevExpress.XtraEditors.CheckEdit();
            this.buttonXClearAll = new DevComponents.DotNetBar.ButtonX();
            this.checkEditSections = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditDeadline = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditMechanicals = new DevExpress.XtraEditors.CheckEdit();
            this.pnTop = new System.Windows.Forms.Panel();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.laOriginalRate = new System.Windows.Forms.Label();
            this.pnMain = new System.Windows.Forms.Panel();
            this.xtraTabControlClone = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageDays = new DevExpress.XtraTab.XtraTabPage();
            this.labelControlDayTitle = new DevExpress.XtraEditors.LabelControl();
            this.buttonXAddAllWeekdays = new DevComponents.DotNetBar.ButtonX();
            this.checkEditHighlightWeekdays = new DevExpress.XtraEditors.CheckEdit();
            this.monthCalendarClone = new Pabo.Calendar.MonthCalendar();
            this.xtraTabPageOptions = new DevExpress.XtraTab.XtraTabPage();
            this.laOptionsTitle = new System.Windows.Forms.Label();
            this.labelControlFlightDates = new DevExpress.XtraEditors.LabelControl();
            this.gridControlDays = new DevExpress.XtraGrid.GridControl();
            this.gridViewDays = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnDay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.pnBottom = new System.Windows.Forms.Panel();
            this.labelControlClonedRate = new DevExpress.XtraEditors.LabelControl();
            this.labelControlClonedNumber = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditPCIRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditDiscount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditColorRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditSections.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditDeadline.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditMechanicals.Properties)).BeginInit();
            this.pnTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.pnMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlClone)).BeginInit();
            this.xtraTabControlClone.SuspendLayout();
            this.xtraTabPageDays.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditHighlightWeekdays.Properties)).BeginInit();
            this.xtraTabPageOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).BeginInit();
            this.pnBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // laOriginalDate
            // 
            this.laOriginalDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laOriginalDate.AutoSize = true;
            this.laOriginalDate.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laOriginalDate.Location = new System.Drawing.Point(96, 19);
            this.laOriginalDate.Name = "laOriginalDate";
            this.laOriginalDate.Size = new System.Drawing.Size(92, 39);
            this.laOriginalDate.TabIndex = 3;
            this.laOriginalDate.Text = "Date";
            this.laOriginalDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkEditPCIRate
            // 
            this.checkEditPCIRate.EditValue = true;
            this.checkEditPCIRate.Location = new System.Drawing.Point(21, 38);
            this.checkEditPCIRate.Name = "checkEditPCIRate";
            this.checkEditPCIRate.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkEditPCIRate.Properties.Appearance.Options.UseFont = true;
            this.checkEditPCIRate.Properties.Caption = "PCI Rate";
            this.checkEditPCIRate.Size = new System.Drawing.Size(121, 21);
            this.checkEditPCIRate.TabIndex = 5;
            this.checkEditPCIRate.CheckedChanged += new System.EventHandler(this.checkEditPCIRate_CheckedChanged);
            // 
            // checkEditDiscount
            // 
            this.checkEditDiscount.EditValue = true;
            this.checkEditDiscount.Location = new System.Drawing.Point(21, 73);
            this.checkEditDiscount.Name = "checkEditDiscount";
            this.checkEditDiscount.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkEditDiscount.Properties.Appearance.Options.UseFont = true;
            this.checkEditDiscount.Properties.Caption = "Discount";
            this.checkEditDiscount.Size = new System.Drawing.Size(93, 21);
            this.checkEditDiscount.TabIndex = 6;
            // 
            // checkEditColorRate
            // 
            this.checkEditColorRate.EditValue = true;
            this.checkEditColorRate.Location = new System.Drawing.Point(21, 144);
            this.checkEditColorRate.Name = "checkEditColorRate";
            this.checkEditColorRate.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkEditColorRate.Properties.Appearance.Options.UseFont = true;
            this.checkEditColorRate.Properties.Caption = "Color Rate";
            this.checkEditColorRate.Size = new System.Drawing.Size(99, 21);
            this.checkEditColorRate.TabIndex = 7;
            // 
            // buttonXCancel
            // 
            this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXCancel.Location = new System.Drawing.Point(495, 11);
            this.buttonXCancel.Name = "buttonXCancel";
            this.buttonXCancel.Size = new System.Drawing.Size(98, 35);
            this.buttonXCancel.TabIndex = 9;
            this.buttonXCancel.Text = "Cancel";
            this.buttonXCancel.TextColor = System.Drawing.Color.Black;
            // 
            // buttonXOK
            // 
            this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXOK.Location = new System.Drawing.Point(383, 11);
            this.buttonXOK.Name = "buttonXOK";
            this.buttonXOK.Size = new System.Drawing.Size(98, 35);
            this.buttonXOK.TabIndex = 8;
            this.buttonXOK.Text = "OK";
            this.buttonXOK.TextColor = System.Drawing.Color.Black;
            // 
            // checkEditComment
            // 
            this.checkEditComment.EditValue = true;
            this.checkEditComment.Location = new System.Drawing.Point(21, 108);
            this.checkEditComment.Name = "checkEditComment";
            this.checkEditComment.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkEditComment.Properties.Appearance.Options.UseFont = true;
            this.checkEditComment.Properties.Caption = "Comments";
            this.checkEditComment.Size = new System.Drawing.Size(88, 21);
            this.checkEditComment.TabIndex = 10;
            // 
            // buttonXClearAll
            // 
            this.buttonXClearAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXClearAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXClearAll.Location = new System.Drawing.Point(262, 70);
            this.buttonXClearAll.Name = "buttonXClearAll";
            this.buttonXClearAll.Size = new System.Drawing.Size(157, 33);
            this.buttonXClearAll.TabIndex = 12;
            this.buttonXClearAll.Text = "Clear All";
            this.buttonXClearAll.TextColor = System.Drawing.Color.Black;
            this.buttonXClearAll.Click += new System.EventHandler(this.buttonXClearAll_Click);
            // 
            // checkEditSections
            // 
            this.checkEditSections.EditValue = true;
            this.checkEditSections.Location = new System.Drawing.Point(148, 38);
            this.checkEditSections.Name = "checkEditSections";
            this.checkEditSections.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkEditSections.Properties.Appearance.Options.UseFont = true;
            this.checkEditSections.Properties.Caption = "Sections";
            this.checkEditSections.Size = new System.Drawing.Size(88, 21);
            this.checkEditSections.TabIndex = 13;
            // 
            // checkEditDeadline
            // 
            this.checkEditDeadline.EditValue = true;
            this.checkEditDeadline.Location = new System.Drawing.Point(148, 73);
            this.checkEditDeadline.Name = "checkEditDeadline";
            this.checkEditDeadline.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkEditDeadline.Properties.Appearance.Options.UseFont = true;
            this.checkEditDeadline.Properties.Caption = "Deadline";
            this.checkEditDeadline.Size = new System.Drawing.Size(88, 21);
            this.checkEditDeadline.TabIndex = 14;
            // 
            // checkEditMechanicals
            // 
            this.checkEditMechanicals.EditValue = true;
            this.checkEditMechanicals.Location = new System.Drawing.Point(148, 108);
            this.checkEditMechanicals.Name = "checkEditMechanicals";
            this.checkEditMechanicals.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkEditMechanicals.Properties.Appearance.Options.UseFont = true;
            this.checkEditMechanicals.Properties.Caption = "Mechanicals";
            this.checkEditMechanicals.Size = new System.Drawing.Size(148, 21);
            this.checkEditMechanicals.TabIndex = 15;
            // 
            // pnTop
            // 
            this.pnTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnTop.Controls.Add(this.pbLogo);
            this.pnTop.Controls.Add(this.laOriginalRate);
            this.pnTop.Controls.Add(this.laOriginalDate);
            this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTop.Location = new System.Drawing.Point(0, 0);
            this.pnTop.Name = "pnTop";
            this.pnTop.Size = new System.Drawing.Size(605, 80);
            this.pnTop.TabIndex = 16;
            // 
            // pbLogo
            // 
            this.pbLogo.Image = global::AdScheduleBuilder.Properties.Resources.CloneSchedule;
            this.pbLogo.Location = new System.Drawing.Point(3, -1);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(87, 79);
            this.pbLogo.TabIndex = 5;
            this.pbLogo.TabStop = false;
            // 
            // laOriginalRate
            // 
            this.laOriginalRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laOriginalRate.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laOriginalRate.Location = new System.Drawing.Point(373, 26);
            this.laOriginalRate.Name = "laOriginalRate";
            this.laOriginalRate.Size = new System.Drawing.Size(225, 24);
            this.laOriginalRate.TabIndex = 4;
            this.laOriginalRate.Text = "Rate";
            this.laOriginalRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnMain
            // 
            this.pnMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnMain.Controls.Add(this.xtraTabControlClone);
            this.pnMain.Controls.Add(this.labelControlFlightDates);
            this.pnMain.Controls.Add(this.gridControlDays);
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Location = new System.Drawing.Point(0, 80);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(605, 284);
            this.pnMain.TabIndex = 17;
            // 
            // xtraTabControlClone
            // 
            this.xtraTabControlClone.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.xtraTabControlClone.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabControlClone.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.xtraTabControlClone.AppearancePage.HeaderActive.Options.UseFont = true;
            this.xtraTabControlClone.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.xtraTabControlClone.AppearancePage.HeaderDisabled.Options.UseFont = true;
            this.xtraTabControlClone.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.xtraTabControlClone.AppearancePage.HeaderHotTracked.Options.UseFont = true;
            this.xtraTabControlClone.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xtraTabControlClone.AppearancePage.PageClient.Options.UseFont = true;
            this.xtraTabControlClone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControlClone.Location = new System.Drawing.Point(171, 33);
            this.xtraTabControlClone.Name = "xtraTabControlClone";
            this.xtraTabControlClone.SelectedTabPage = this.xtraTabPageDays;
            this.xtraTabControlClone.Size = new System.Drawing.Size(430, 247);
            this.xtraTabControlClone.TabIndex = 5;
            this.xtraTabControlClone.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageDays,
            this.xtraTabPageOptions});
            // 
            // xtraTabPageDays
            // 
            this.xtraTabPageDays.Controls.Add(this.labelControlDayTitle);
            this.xtraTabPageDays.Controls.Add(this.buttonXAddAllWeekdays);
            this.xtraTabPageDays.Controls.Add(this.checkEditHighlightWeekdays);
            this.xtraTabPageDays.Controls.Add(this.monthCalendarClone);
            this.xtraTabPageDays.Controls.Add(this.buttonXClearAll);
            this.xtraTabPageDays.Name = "xtraTabPageDays";
            this.xtraTabPageDays.Size = new System.Drawing.Size(428, 221);
            this.xtraTabPageDays.Text = "Days";
            // 
            // labelControlDayTitle
            // 
            this.labelControlDayTitle.AllowHtmlString = true;
            this.labelControlDayTitle.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControlDayTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlDayTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControlDayTitle.Location = new System.Drawing.Point(0, 0);
            this.labelControlDayTitle.Name = "labelControlDayTitle";
            this.labelControlDayTitle.Size = new System.Drawing.Size(428, 28);
            this.labelControlDayTitle.TabIndex = 19;
            this.labelControlDayTitle.Text = "  <b>DOUBLE-CLICK</b> the DAY you want to Add:";
            // 
            // buttonXAddAllWeekdays
            // 
            this.buttonXAddAllWeekdays.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXAddAllWeekdays.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXAddAllWeekdays.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXAddAllWeekdays.Location = new System.Drawing.Point(262, 31);
            this.buttonXAddAllWeekdays.Name = "buttonXAddAllWeekdays";
            this.buttonXAddAllWeekdays.Size = new System.Drawing.Size(159, 33);
            this.buttonXAddAllWeekdays.TabIndex = 18;
            this.buttonXAddAllWeekdays.Text = "Add All {0}s";
            this.buttonXAddAllWeekdays.TextColor = System.Drawing.Color.Black;
            this.buttonXAddAllWeekdays.Click += new System.EventHandler(this.buttonXAddAllWeekdays_Click);
            // 
            // checkEditHighlightWeekdays
            // 
            this.checkEditHighlightWeekdays.EditValue = true;
            this.checkEditHighlightWeekdays.Location = new System.Drawing.Point(5, 197);
            this.checkEditHighlightWeekdays.Name = "checkEditHighlightWeekdays";
            this.checkEditHighlightWeekdays.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkEditHighlightWeekdays.Properties.Appearance.Options.UseFont = true;
            this.checkEditHighlightWeekdays.Properties.AutoWidth = true;
            this.checkEditHighlightWeekdays.Properties.Caption = "Highlight all {0}s";
            this.checkEditHighlightWeekdays.Size = new System.Drawing.Size(116, 21);
            this.checkEditHighlightWeekdays.TabIndex = 16;
            this.checkEditHighlightWeekdays.CheckedChanged += new System.EventHandler(this.checkEditHighlightWeekdays_CheckedChanged);
            // 
            // monthCalendarClone
            // 
            this.monthCalendarClone.ActiveMonth.Month = 3;
            this.monthCalendarClone.ActiveMonth.Year = 2012;
            this.monthCalendarClone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(198)))), ((int)(((byte)(214)))));
            this.monthCalendarClone.Culture = new System.Globalization.CultureInfo("en-US");
            this.monthCalendarClone.Footer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.monthCalendarClone.Header.BackColor1 = System.Drawing.Color.White;
            this.monthCalendarClone.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.monthCalendarClone.Header.TextColor = System.Drawing.Color.White;
            this.monthCalendarClone.ImageList = null;
            this.monthCalendarClone.Location = new System.Drawing.Point(5, 31);
            this.monthCalendarClone.MaxDate = new System.DateTime(2022, 3, 9, 17, 37, 18, 958);
            this.monthCalendarClone.MinDate = new System.DateTime(2002, 3, 9, 17, 37, 18, 958);
            this.monthCalendarClone.Month.BackgroundImage = null;
            this.monthCalendarClone.Month.Colors.Disabled.Text = System.Drawing.Color.Black;
            this.monthCalendarClone.Month.Colors.Focus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(213)))), ((int)(((byte)(224)))));
            this.monthCalendarClone.Month.Colors.Focus.Border = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(198)))), ((int)(((byte)(214)))));
            this.monthCalendarClone.Month.Colors.Selected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(198)))), ((int)(((byte)(214)))));
            this.monthCalendarClone.Month.Colors.Selected.Border = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(97)))), ((int)(((byte)(135)))));
            this.monthCalendarClone.Month.Colors.Weekend.Date = System.Drawing.Color.Empty;
            this.monthCalendarClone.Month.Colors.Weekend.Saturday = false;
            this.monthCalendarClone.Month.Colors.Weekend.Sunday = false;
            this.monthCalendarClone.Month.Colors.Weekend.Text = System.Drawing.Color.Empty;
            this.monthCalendarClone.Month.DateFont = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.monthCalendarClone.Month.TextFont = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.monthCalendarClone.Month.Transparency.Background = 255;
            this.monthCalendarClone.Month.Transparency.Text = 255;
            this.monthCalendarClone.Name = "monthCalendarClone";
            this.monthCalendarClone.SelectionMode = Pabo.Calendar.mcSelectionMode.None;
            this.monthCalendarClone.SelectTrailingDates = false;
            this.monthCalendarClone.ShowFocus = false;
            this.monthCalendarClone.ShowFooter = false;
            this.monthCalendarClone.ShowToday = false;
            this.monthCalendarClone.Size = new System.Drawing.Size(251, 160);
            this.monthCalendarClone.TabIndex = 17;
            this.monthCalendarClone.Weekdays.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.monthCalendarClone.Weekdays.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.monthCalendarClone.Weeknumbers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.monthCalendarClone.Weeknumbers.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.monthCalendarClone.DayQueryInfo += new Pabo.Calendar.DayQueryInfoEventHandler(this.monthCalendarClone_DayQueryInfo);
            this.monthCalendarClone.DayDoubleClick += new Pabo.Calendar.DayClickEventHandler(this.monthCalendarClone_DayClick);
            // 
            // xtraTabPageOptions
            // 
            this.xtraTabPageOptions.Controls.Add(this.laOptionsTitle);
            this.xtraTabPageOptions.Controls.Add(this.checkEditPCIRate);
            this.xtraTabPageOptions.Controls.Add(this.checkEditDiscount);
            this.xtraTabPageOptions.Controls.Add(this.checkEditMechanicals);
            this.xtraTabPageOptions.Controls.Add(this.checkEditColorRate);
            this.xtraTabPageOptions.Controls.Add(this.checkEditDeadline);
            this.xtraTabPageOptions.Controls.Add(this.checkEditComment);
            this.xtraTabPageOptions.Controls.Add(this.checkEditSections);
            this.xtraTabPageOptions.Name = "xtraTabPageOptions";
            this.xtraTabPageOptions.Size = new System.Drawing.Size(428, 221);
            this.xtraTabPageOptions.Text = "Options";
            // 
            // laOptionsTitle
            // 
            this.laOptionsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.laOptionsTitle.Location = new System.Drawing.Point(0, 0);
            this.laOptionsTitle.Name = "laOptionsTitle";
            this.laOptionsTitle.Size = new System.Drawing.Size(428, 31);
            this.laOptionsTitle.TabIndex = 0;
            this.laOptionsTitle.Text = "  Do you want to Exclude any of these items in the Cloned Ad?";
            this.laOptionsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelControlFlightDates
            // 
            this.labelControlFlightDates.AllowHtmlString = true;
            this.labelControlFlightDates.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControlFlightDates.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlFlightDates.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControlFlightDates.Location = new System.Drawing.Point(171, 0);
            this.labelControlFlightDates.Name = "labelControlFlightDates";
            this.labelControlFlightDates.Size = new System.Drawing.Size(430, 33);
            this.labelControlFlightDates.TabIndex = 6;
            this.labelControlFlightDates.Text = "  Your Available Schedule Window: <b>{0}</b>";
            // 
            // gridControlDays
            // 
            this.gridControlDays.AllowDrop = true;
            this.gridControlDays.Dock = System.Windows.Forms.DockStyle.Left;
            this.gridControlDays.Location = new System.Drawing.Point(0, 0);
            this.gridControlDays.MainView = this.gridViewDays;
            this.gridControlDays.Name = "gridControlDays";
            this.gridControlDays.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit});
            this.gridControlDays.Size = new System.Drawing.Size(171, 280);
            this.gridControlDays.TabIndex = 4;
            this.gridControlDays.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDays});
            // 
            // gridViewDays
            // 
            this.gridViewDays.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridViewDays.Appearance.FocusedRow.Options.UseFont = true;
            this.gridViewDays.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gridViewDays.Appearance.Row.Options.UseFont = true;
            this.gridViewDays.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridViewDays.Appearance.SelectedRow.Options.UseFont = true;
            this.gridViewDays.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnDay});
            this.gridViewDays.GridControl = this.gridControlDays;
            this.gridViewDays.Name = "gridViewDays";
            this.gridViewDays.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewDays.OptionsCustomization.AllowColumnResizing = false;
            this.gridViewDays.OptionsCustomization.AllowFilter = false;
            this.gridViewDays.OptionsCustomization.AllowGroup = false;
            this.gridViewDays.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewDays.OptionsCustomization.AllowSort = false;
            this.gridViewDays.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewDays.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridViewDays.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridViewDays.OptionsView.RowAutoHeight = true;
            this.gridViewDays.OptionsView.ShowColumnHeaders = false;
            this.gridViewDays.OptionsView.ShowDetailButtons = false;
            this.gridViewDays.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridViewDays.OptionsView.ShowGroupPanel = false;
            this.gridViewDays.OptionsView.ShowIndicator = false;
            // 
            // gridColumnDay
            // 
            this.gridColumnDay.Caption = "gridColumnDay";
            this.gridColumnDay.ColumnEdit = this.repositoryItemButtonEdit;
            this.gridColumnDay.FieldName = "Date";
            this.gridColumnDay.Name = "gridColumnDay";
            this.gridColumnDay.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gridColumnDay.Visible = true;
            this.gridColumnDay.VisibleIndex = 0;
            // 
            // repositoryItemButtonEdit
            // 
            this.repositoryItemButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEdit.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.repositoryItemButtonEdit.DisplayFormat.FormatString = "ddd, M/d/yy";
            this.repositoryItemButtonEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemButtonEdit.EditFormat.FormatString = "ddd, M/d/yy";
            this.repositoryItemButtonEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemButtonEdit.Name = "repositoryItemButtonEdit";
            this.repositoryItemButtonEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemButtonEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit_ButtonClick);
            // 
            // pnBottom
            // 
            this.pnBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnBottom.Controls.Add(this.labelControlClonedRate);
            this.pnBottom.Controls.Add(this.labelControlClonedNumber);
            this.pnBottom.Controls.Add(this.buttonXOK);
            this.pnBottom.Controls.Add(this.buttonXCancel);
            this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnBottom.Location = new System.Drawing.Point(0, 364);
            this.pnBottom.Name = "pnBottom";
            this.pnBottom.Size = new System.Drawing.Size(605, 60);
            this.pnBottom.TabIndex = 18;
            // 
            // labelControlClonedRate
            // 
            this.labelControlClonedRate.AllowHtmlString = true;
            this.labelControlClonedRate.Appearance.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControlClonedRate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlClonedRate.Location = new System.Drawing.Point(3, 32);
            this.labelControlClonedRate.Name = "labelControlClonedRate";
            this.labelControlClonedRate.Size = new System.Drawing.Size(374, 21);
            this.labelControlClonedRate.TabIndex = 11;
            this.labelControlClonedRate.Text = "Added Dollars: <b>{0}</b>";
            // 
            // labelControlClonedNumber
            // 
            this.labelControlClonedNumber.AllowHtmlString = true;
            this.labelControlClonedNumber.Appearance.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControlClonedNumber.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlClonedNumber.Location = new System.Drawing.Point(3, 3);
            this.labelControlClonedNumber.Name = "labelControlClonedNumber";
            this.labelControlClonedNumber.Size = new System.Drawing.Size(374, 23);
            this.labelControlClonedNumber.TabIndex = 10;
            this.labelControlClonedNumber.Text = "Cloned Ads: <b>{0}</b>";
            // 
            // FormCloneInsert
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(605, 424);
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.pnBottom);
            this.Controls.Add(this.pnTop);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCloneInsert";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Clone this Line";
            ((System.ComponentModel.ISupportInitialize)(this.checkEditPCIRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditDiscount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditColorRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditSections.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditDeadline.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditMechanicals.Properties)).EndInit();
            this.pnTop.ResumeLayout(false);
            this.pnTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.pnMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlClone)).EndInit();
            this.xtraTabControlClone.ResumeLayout(false);
            this.xtraTabPageDays.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEditHighlightWeekdays.Properties)).EndInit();
            this.xtraTabPageOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).EndInit();
            this.pnBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        public System.Windows.Forms.Label laOriginalDate;
        public DevExpress.XtraEditors.CheckEdit checkEditPCIRate;
        public DevExpress.XtraEditors.CheckEdit checkEditDiscount;
        public DevExpress.XtraEditors.CheckEdit checkEditColorRate;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevComponents.DotNetBar.ButtonX buttonXOK;
        public DevExpress.XtraEditors.CheckEdit checkEditComment;
        private DevComponents.DotNetBar.ButtonX buttonXClearAll;
        public DevExpress.XtraEditors.CheckEdit checkEditSections;
        public DevExpress.XtraEditors.CheckEdit checkEditDeadline;
        public DevExpress.XtraEditors.CheckEdit checkEditMechanicals;
        private System.Windows.Forms.Panel pnTop;
        private System.Windows.Forms.Panel pnMain;
        private DevExpress.XtraGrid.GridControl gridControlDays;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDays;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDay;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit;
        private System.Windows.Forms.Panel pnBottom;
        private DevExpress.XtraTab.XtraTabControl xtraTabControlClone;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageDays;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageOptions;
        private System.Windows.Forms.Label laOptionsTitle;
        public DevExpress.XtraEditors.CheckEdit checkEditHighlightWeekdays;
        private Pabo.Calendar.MonthCalendar monthCalendarClone;
        private DevComponents.DotNetBar.ButtonX buttonXAddAllWeekdays;
        private DevExpress.XtraEditors.LabelControl labelControlDayTitle;
        private DevExpress.XtraEditors.LabelControl labelControlFlightDates;
        public System.Windows.Forms.Label laOriginalRate;
        private DevExpress.XtraEditors.LabelControl labelControlClonedRate;
        private DevExpress.XtraEditors.LabelControl labelControlClonedNumber;
        private System.Windows.Forms.PictureBox pbLogo;
    }
}