namespace NewBizWiz.Calendar.Controls.ToolForms
{
    partial class FormCloneNote
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
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			this.laTitle = new System.Windows.Forms.Label();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.buttonXClearAll = new DevComponents.DotNetBar.ButtonX();
			this.pnTop = new System.Windows.Forms.Panel();
			this.pbHelp = new System.Windows.Forms.PictureBox();
			this.laClonedNote = new System.Windows.Forms.Label();
			this.pbLogo = new System.Windows.Forms.PictureBox();
			this.pnMain = new System.Windows.Forms.Panel();
			this.xtraTabControlClone = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageDays = new DevExpress.XtraTab.XtraTabPage();
			this.labelControlTooltip = new DevExpress.XtraEditors.LabelControl();
			this.monthCalendarClone = new Pabo.Calendar.MonthCalendar();
			this.labelControlFlightDates = new DevExpress.XtraEditors.LabelControl();
			this.gridControlDays = new DevExpress.XtraGrid.GridControl();
			this.gridViewDays = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnRange = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.pnBottom = new System.Windows.Forms.Panel();
			this.labelControlClonedNumber = new DevExpress.XtraEditors.LabelControl();
			this.pnTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbHelp)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			this.pnMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlClone)).BeginInit();
			this.xtraTabControlClone.SuspendLayout();
			this.xtraTabPageDays.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControlDays)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewDays)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).BeginInit();
			this.pnBottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// laTitle
			// 
			this.laTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laTitle.AutoSize = true;
			this.laTitle.BackColor = System.Drawing.Color.White;
			this.laTitle.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.ForeColor = System.Drawing.Color.Black;
			this.laTitle.Location = new System.Drawing.Point(96, 7);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(135, 23);
			this.laTitle.TabIndex = 3;
			this.laTitle.Text = "Cloned Note:";
			this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(499, 11);
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
			this.buttonXOK.Location = new System.Drawing.Point(387, 11);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(98, 35);
			this.buttonXOK.TabIndex = 8;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXClearAll
			// 
			this.buttonXClearAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXClearAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXClearAll.Location = new System.Drawing.Point(264, 53);
			this.buttonXClearAll.Name = "buttonXClearAll";
			this.buttonXClearAll.Size = new System.Drawing.Size(157, 33);
			this.buttonXClearAll.TabIndex = 12;
			this.buttonXClearAll.Text = "Clear All";
			this.buttonXClearAll.TextColor = System.Drawing.Color.Black;
			this.buttonXClearAll.Click += new System.EventHandler(this.buttonXClearAll_Click);
			// 
			// pnTop
			// 
			this.pnTop.BackColor = System.Drawing.Color.Transparent;
			this.pnTop.Controls.Add(this.pbHelp);
			this.pnTop.Controls.Add(this.laClonedNote);
			this.pnTop.Controls.Add(this.pbLogo);
			this.pnTop.Controls.Add(this.laTitle);
			this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTop.ForeColor = System.Drawing.Color.Black;
			this.pnTop.Location = new System.Drawing.Point(0, 0);
			this.pnTop.Name = "pnTop";
			this.pnTop.Size = new System.Drawing.Size(605, 89);
			this.pnTop.TabIndex = 16;
			// 
			// pbHelp
			// 
			this.pbHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pbHelp.BackColor = System.Drawing.Color.White;
			this.pbHelp.ForeColor = System.Drawing.Color.Black;
			this.pbHelp.Image = global::NewBizWiz.Calendar.Controls.Properties.Resources.Help;
			this.pbHelp.Location = new System.Drawing.Point(521, 2);
			this.pbHelp.Name = "pbHelp";
			this.pbHelp.Size = new System.Drawing.Size(76, 81);
			this.pbHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pbHelp.TabIndex = 28;
			this.pbHelp.TabStop = false;
			this.pbHelp.Click += new System.EventHandler(this.pbHelp_Click);
			this.pbHelp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			this.pbHelp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			// 
			// laClonedNote
			// 
			this.laClonedNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laClonedNote.BackColor = System.Drawing.Color.White;
			this.laClonedNote.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laClonedNote.ForeColor = System.Drawing.Color.Black;
			this.laClonedNote.Location = new System.Drawing.Point(96, 30);
			this.laClonedNote.Name = "laClonedNote";
			this.laClonedNote.Size = new System.Drawing.Size(389, 58);
			this.laClonedNote.TabIndex = 6;
			this.laClonedNote.Text = "Cloned Note:";
			this.laClonedNote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pbLogo
			// 
			this.pbLogo.BackColor = System.Drawing.Color.White;
			this.pbLogo.ForeColor = System.Drawing.Color.Black;
			this.pbLogo.Image = global::NewBizWiz.Calendar.Controls.Properties.Resources.Clone;
			this.pbLogo.Location = new System.Drawing.Point(3, 5);
			this.pbLogo.Name = "pbLogo";
			this.pbLogo.Size = new System.Drawing.Size(87, 75);
			this.pbLogo.TabIndex = 5;
			this.pbLogo.TabStop = false;
			// 
			// pnMain
			// 
			this.pnMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
			this.pnMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnMain.Controls.Add(this.xtraTabControlClone);
			this.pnMain.Controls.Add(this.labelControlFlightDates);
			this.pnMain.Controls.Add(this.gridControlDays);
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.ForeColor = System.Drawing.Color.Black;
			this.pnMain.Location = new System.Drawing.Point(0, 89);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(605, 249);
			this.pnMain.TabIndex = 17;
			// 
			// xtraTabControlClone
			// 
			this.xtraTabControlClone.Appearance.BackColor = System.Drawing.Color.White;
			this.xtraTabControlClone.Appearance.ForeColor = System.Drawing.Color.Black;
			this.xtraTabControlClone.Appearance.Options.UseBackColor = true;
			this.xtraTabControlClone.Appearance.Options.UseForeColor = true;
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
			this.xtraTabControlClone.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
			this.xtraTabControlClone.Size = new System.Drawing.Size(432, 214);
			this.xtraTabControlClone.TabIndex = 5;
			this.xtraTabControlClone.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageDays});
			// 
			// xtraTabPageDays
			// 
			this.xtraTabPageDays.Appearance.PageClient.ForeColor = System.Drawing.Color.Black;
			this.xtraTabPageDays.Appearance.PageClient.Options.UseForeColor = true;
			this.xtraTabPageDays.Controls.Add(this.labelControlTooltip);
			this.xtraTabPageDays.Controls.Add(this.monthCalendarClone);
			this.xtraTabPageDays.Controls.Add(this.buttonXClearAll);
			this.xtraTabPageDays.Name = "xtraTabPageDays";
			this.xtraTabPageDays.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.xtraTabPageDays.Size = new System.Drawing.Size(426, 208);
			this.xtraTabPageDays.Text = "Days";
			// 
			// labelControlTooltip
			// 
			this.labelControlTooltip.AllowHtmlString = true;
			this.labelControlTooltip.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlTooltip.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlTooltip.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlTooltip.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlTooltip.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlTooltip.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelControlTooltip.Location = new System.Drawing.Point(5, 0);
			this.labelControlTooltip.Name = "labelControlTooltip";
			this.labelControlTooltip.Size = new System.Drawing.Size(421, 47);
			this.labelControlTooltip.TabIndex = 19;
			this.labelControlTooltip.Text = "Add Date Ranges selecting first and last days in week \r\nby Mouse Click holding Ct" +
    "rl or Shift Key";
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
			this.monthCalendarClone.Location = new System.Drawing.Point(5, 53);
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
			this.monthCalendarClone.SelectButton = System.Windows.Forms.MouseButtons.None;
			this.monthCalendarClone.SelectionMode = Pabo.Calendar.mcSelectionMode.One;
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
			this.monthCalendarClone.DayClick += new Pabo.Calendar.DayClickEventHandler(this.monthCalendarClone_DayClick);
			// 
			// labelControlFlightDates
			// 
			this.labelControlFlightDates.AllowHtmlString = true;
			this.labelControlFlightDates.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlFlightDates.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlFlightDates.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlFlightDates.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlFlightDates.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelControlFlightDates.Location = new System.Drawing.Point(171, 0);
			this.labelControlFlightDates.Name = "labelControlFlightDates";
			this.labelControlFlightDates.Size = new System.Drawing.Size(432, 33);
			this.labelControlFlightDates.TabIndex = 6;
			this.labelControlFlightDates.Text = "  Your Available Schedule Window: <b>{0}</b>";
			// 
			// gridControlDays
			// 
			this.gridControlDays.AllowDrop = true;
			this.gridControlDays.Dock = System.Windows.Forms.DockStyle.Left;
			this.gridControlDays.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.White;
			this.gridControlDays.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Black;
			this.gridControlDays.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
			this.gridControlDays.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
			this.gridControlDays.Location = new System.Drawing.Point(0, 0);
			this.gridControlDays.MainView = this.gridViewDays;
			this.gridControlDays.Name = "gridControlDays";
			this.gridControlDays.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit});
			this.gridControlDays.Size = new System.Drawing.Size(171, 247);
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
            this.gridColumnRange});
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
			// gridColumnRange
			// 
			this.gridColumnRange.Caption = "gridColumnRange";
			this.gridColumnRange.ColumnEdit = this.repositoryItemButtonEdit;
			this.gridColumnRange.FieldName = "Range";
			this.gridColumnRange.Name = "gridColumnRange";
			this.gridColumnRange.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnRange.Visible = true;
			this.gridColumnRange.VisibleIndex = 0;
			// 
			// repositoryItemButtonEdit
			// 
			this.repositoryItemButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::NewBizWiz.Calendar.Controls.Properties.Resources.DeleteData, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
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
			this.pnBottom.BackColor = System.Drawing.Color.Transparent;
			this.pnBottom.Controls.Add(this.labelControlClonedNumber);
			this.pnBottom.Controls.Add(this.buttonXOK);
			this.pnBottom.Controls.Add(this.buttonXCancel);
			this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnBottom.ForeColor = System.Drawing.Color.Black;
			this.pnBottom.Location = new System.Drawing.Point(0, 338);
			this.pnBottom.Name = "pnBottom";
			this.pnBottom.Size = new System.Drawing.Size(605, 60);
			this.pnBottom.TabIndex = 18;
			// 
			// labelControlClonedNumber
			// 
			this.labelControlClonedNumber.AllowHtmlString = true;
			this.labelControlClonedNumber.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlClonedNumber.Appearance.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlClonedNumber.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlClonedNumber.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlClonedNumber.Dock = System.Windows.Forms.DockStyle.Left;
			this.labelControlClonedNumber.Location = new System.Drawing.Point(0, 0);
			this.labelControlClonedNumber.Name = "labelControlClonedNumber";
			this.labelControlClonedNumber.Size = new System.Drawing.Size(374, 60);
			this.labelControlClonedNumber.TabIndex = 10;
			this.labelControlClonedNumber.Text = "Cloned Notes: <b>{0}</b>";
			// 
			// FormCloneNote
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(605, 398);
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.pnBottom);
			this.Controls.Add(this.pnTop);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormCloneNote";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Clone this Note";
			this.pnTop.ResumeLayout(false);
			this.pnTop.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbHelp)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			this.pnMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlClone)).EndInit();
			this.xtraTabControlClone.ResumeLayout(false);
			this.xtraTabPageDays.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControlDays)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewDays)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).EndInit();
			this.pnBottom.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		public System.Windows.Forms.Label laTitle;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevComponents.DotNetBar.ButtonX buttonXOK;
        private DevComponents.DotNetBar.ButtonX buttonXClearAll;
        private System.Windows.Forms.Panel pnTop;
        private System.Windows.Forms.Panel pnMain;
        private DevExpress.XtraGrid.GridControl gridControlDays;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDays;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnRange;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit;
        private System.Windows.Forms.Panel pnBottom;
        private DevExpress.XtraTab.XtraTabControl xtraTabControlClone;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageDays;
        private Pabo.Calendar.MonthCalendar monthCalendarClone;
        private DevExpress.XtraEditors.LabelControl labelControlTooltip;
        private DevExpress.XtraEditors.LabelControl labelControlFlightDates;
        private DevExpress.XtraEditors.LabelControl labelControlClonedNumber;
        private System.Windows.Forms.PictureBox pbLogo;
        public System.Windows.Forms.Label laClonedNote;
        private System.Windows.Forms.PictureBox pbHelp;
    }
}