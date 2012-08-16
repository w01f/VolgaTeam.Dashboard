namespace AdScheduleBuilder.ToolForms
{
    partial class FormAdNotes
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
            this.ckComment = new DevExpress.XtraEditors.CheckEdit();
            this.textEditComment = new DevExpress.XtraEditors.TextEdit();
            this.checkedListBoxControlComments = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            this.buttonXClearSection = new DevComponents.DotNetBar.ButtonX();
            this.tabControlAdNotes = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanelComments = new DevComponents.DotNetBar.TabControlPanel();
            this.adNotesWeekdaysSelectorComments = new AdScheduleBuilder.CustomControls.AdNotesWeekdaysSelector();
            this.buttonXClearComment = new DevComponents.DotNetBar.ButtonX();
            this.ckCommentAll = new DevExpress.XtraEditors.CheckEdit();
            this.tabItemComments = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanelSections = new DevComponents.DotNetBar.TabControlPanel();
            this.adNotesWeekdaysSelectorSections = new AdScheduleBuilder.CustomControls.AdNotesWeekdaysSelector();
            this.textEditSection = new DevExpress.XtraEditors.TextEdit();
            this.ckSection = new DevExpress.XtraEditors.CheckEdit();
            this.checkedListBoxControlSections = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.tabItemSections = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanelDeadlines = new DevComponents.DotNetBar.TabControlPanel();
            this.adNotesWeekdaysSelectorDeadlines = new AdScheduleBuilder.CustomControls.AdNotesWeekdaysSelector();
            this.textEditDeadline = new DevExpress.XtraEditors.TextEdit();
            this.ckDeadline = new DevExpress.XtraEditors.CheckEdit();
            this.checkedListBoxControlDeadline = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.laDeadline = new System.Windows.Forms.Label();
            this.buttonXClearDeadline = new DevComponents.DotNetBar.ButtonX();
            this.tabItemDeadlines = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanelMechanicals = new DevComponents.DotNetBar.TabControlPanel();
            this.adNotesWeekdaysSelectorMechanicals = new AdScheduleBuilder.CustomControls.AdNotesWeekdaysSelector();
            this.buttonXClearMechanicals = new DevComponents.DotNetBar.ButtonX();
            this.xtraTabControlMechanicals = new DevExpress.XtraTab.XtraTabControl();
            this.textEditMechanicals = new DevExpress.XtraEditors.TextEdit();
            this.ckMechanicals = new DevExpress.XtraEditors.CheckEdit();
            this.tabItemMechanicals = new DevComponents.DotNetBar.TabItem(this.components);
            this.persistentRepository = new DevExpress.XtraEditors.Repository.PersistentRepository(this.components);
            this.repositoryItemCheckEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemSpinEdit = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.pnMain = new System.Windows.Forms.Panel();
            this.pnTop = new System.Windows.Forms.Panel();
            this.pbHelp = new System.Windows.Forms.PictureBox();
            this.laFinalRate = new System.Windows.Forms.Label();
            this.laDate = new System.Windows.Forms.Label();
            this.laID = new System.Windows.Forms.Label();
            this.llaLogo = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.pnButtons = new System.Windows.Forms.Panel();
            this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
            ((System.ComponentModel.ISupportInitialize)(this.ckComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlComments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdNotes)).BeginInit();
            this.tabControlAdNotes.SuspendLayout();
            this.tabControlPanelComments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckCommentAll.Properties)).BeginInit();
            this.tabControlPanelSections.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckSection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlSections)).BeginInit();
            this.tabControlPanelDeadlines.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDeadline.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckDeadline.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlDeadline)).BeginInit();
            this.tabControlPanelMechanicals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlMechanicals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditMechanicals.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckMechanicals.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit)).BeginInit();
            this.pnMain.SuspendLayout();
            this.pnTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHelp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.pnButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // ckComment
            // 
            this.ckComment.Location = new System.Drawing.Point(8, 13);
            this.ckComment.Name = "ckComment";
            this.ckComment.Properties.Caption = "";
            this.ckComment.Size = new System.Drawing.Size(21, 19);
            this.ckComment.TabIndex = 2;
            this.ckComment.CheckedChanged += new System.EventHandler(this.ckComment_CheckedChanged);
            // 
            // textEditComment
            // 
            this.textEditComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditComment.Enabled = false;
            this.textEditComment.Location = new System.Drawing.Point(33, 11);
            this.textEditComment.Name = "textEditComment";
            this.textEditComment.Size = new System.Drawing.Size(570, 20);
            this.textEditComment.TabIndex = 3;
            this.textEditComment.EditValueChanged += new System.EventHandler(this.textEditComment_EditValueChanged);
            // 
            // checkedListBoxControlComments
            // 
            this.checkedListBoxControlComments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxControlComments.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.checkedListBoxControlComments.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkedListBoxControlComments.Appearance.Options.UseBackColor = true;
            this.checkedListBoxControlComments.Appearance.Options.UseFont = true;
            this.checkedListBoxControlComments.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.checkedListBoxControlComments.CheckOnClick = true;
            this.checkedListBoxControlComments.ItemHeight = 30;
            this.checkedListBoxControlComments.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Label A"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Label B"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Label C"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Label D")});
            this.checkedListBoxControlComments.Location = new System.Drawing.Point(8, 37);
            this.checkedListBoxControlComments.Name = "checkedListBoxControlComments";
            this.checkedListBoxControlComments.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.checkedListBoxControlComments.Size = new System.Drawing.Size(595, 214);
            this.checkedListBoxControlComments.TabIndex = 4;
            this.checkedListBoxControlComments.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.checkedListBoxControlComments_ItemCheck);
            // 
            // buttonXOK
            // 
            this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXOK.Location = new System.Drawing.Point(380, 6);
            this.buttonXOK.Name = "buttonXOK";
            this.buttonXOK.Size = new System.Drawing.Size(107, 36);
            this.buttonXOK.TabIndex = 5;
            this.buttonXOK.Text = "OK";
            this.buttonXOK.TextColor = System.Drawing.Color.Black;
            // 
            // buttonXCancel
            // 
            this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXCancel.Location = new System.Drawing.Point(496, 6);
            this.buttonXCancel.Name = "buttonXCancel";
            this.buttonXCancel.Size = new System.Drawing.Size(107, 36);
            this.buttonXCancel.TabIndex = 6;
            this.buttonXCancel.Text = "Cancel";
            this.buttonXCancel.TextColor = System.Drawing.Color.Black;
            // 
            // buttonXClearSection
            // 
            this.buttonXClearSection.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXClearSection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXClearSection.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXClearSection.Location = new System.Drawing.Point(8, 257);
            this.buttonXClearSection.Name = "buttonXClearSection";
            this.buttonXClearSection.Size = new System.Drawing.Size(107, 59);
            this.buttonXClearSection.TabIndex = 7;
            this.buttonXClearSection.Text = "Clear\r\nSection";
            this.buttonXClearSection.TextColor = System.Drawing.Color.Black;
            this.buttonXClearSection.Click += new System.EventHandler(this.buttonXClearSection_Click);
            // 
            // tabControlAdNotes
            // 
            this.tabControlAdNotes.BackColor = System.Drawing.Color.Transparent;
            this.tabControlAdNotes.CanReorderTabs = false;
            this.tabControlAdNotes.ColorScheme.TabBackground = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.tabControlAdNotes.ColorScheme.TabBackground2 = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.tabControlAdNotes.ColorScheme.TabItemBackground = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.tabControlAdNotes.ColorScheme.TabItemBackground2 = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.tabControlAdNotes.ColorScheme.TabItemBorder = System.Drawing.Color.White;
            this.tabControlAdNotes.ColorScheme.TabItemBorderDark = System.Drawing.Color.White;
            this.tabControlAdNotes.ColorScheme.TabItemHotBackground = System.Drawing.Color.Gold;
            this.tabControlAdNotes.ColorScheme.TabItemHotBackground2 = System.Drawing.Color.Orange;
            this.tabControlAdNotes.ColorScheme.TabItemHotBorder = System.Drawing.Color.White;
            this.tabControlAdNotes.ColorScheme.TabItemHotBorderDark = System.Drawing.Color.White;
            this.tabControlAdNotes.ColorScheme.TabItemHotBorderLight = System.Drawing.Color.White;
            this.tabControlAdNotes.ColorScheme.TabItemHotText = System.Drawing.Color.Black;
            this.tabControlAdNotes.ColorScheme.TabItemSelectedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.tabControlAdNotes.ColorScheme.TabItemSelectedBackground2 = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.tabControlAdNotes.ColorScheme.TabItemSelectedBorder = System.Drawing.Color.White;
            this.tabControlAdNotes.ColorScheme.TabItemSelectedText = System.Drawing.Color.Black;
            this.tabControlAdNotes.ColorScheme.TabItemText = System.Drawing.Color.Black;
            this.tabControlAdNotes.ColorScheme.TabPanelBackground = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.tabControlAdNotes.ColorScheme.TabPanelBackground2 = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.tabControlAdNotes.ColorScheme.TabPanelBorder = System.Drawing.Color.White;
            this.tabControlAdNotes.Controls.Add(this.tabControlPanelComments);
            this.tabControlAdNotes.Controls.Add(this.tabControlPanelSections);
            this.tabControlAdNotes.Controls.Add(this.tabControlPanelDeadlines);
            this.tabControlAdNotes.Controls.Add(this.tabControlPanelMechanicals);
            this.tabControlAdNotes.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControlAdNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAdNotes.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControlAdNotes.ForeColor = System.Drawing.Color.Black;
            this.tabControlAdNotes.Location = new System.Drawing.Point(0, 86);
            this.tabControlAdNotes.Name = "tabControlAdNotes";
            this.tabControlAdNotes.SelectedTabFont = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControlAdNotes.SelectedTabIndex = 0;
            this.tabControlAdNotes.Size = new System.Drawing.Size(610, 364);
            this.tabControlAdNotes.Style = DevComponents.DotNetBar.eTabStripStyle.Office2007Document;
            this.tabControlAdNotes.TabIndex = 17;
            this.tabControlAdNotes.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControlAdNotes.Tabs.Add(this.tabItemComments);
            this.tabControlAdNotes.Tabs.Add(this.tabItemSections);
            this.tabControlAdNotes.Tabs.Add(this.tabItemDeadlines);
            this.tabControlAdNotes.Tabs.Add(this.tabItemMechanicals);
            this.tabControlAdNotes.TabStop = false;
            this.tabControlAdNotes.Text = "tabControl1";
            this.tabControlAdNotes.SelectedTabChanged += new DevComponents.DotNetBar.TabStrip.SelectedTabChangedEventHandler(this.tabControlAdNotes_SelectedTabChanged);
            // 
            // tabControlPanelComments
            // 
            this.tabControlPanelComments.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.tabControlPanelComments.Controls.Add(this.adNotesWeekdaysSelectorComments);
            this.tabControlPanelComments.Controls.Add(this.buttonXClearComment);
            this.tabControlPanelComments.Controls.Add(this.ckCommentAll);
            this.tabControlPanelComments.Controls.Add(this.textEditComment);
            this.tabControlPanelComments.Controls.Add(this.ckComment);
            this.tabControlPanelComments.Controls.Add(this.checkedListBoxControlComments);
            this.tabControlPanelComments.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControlPanelComments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanelComments.Location = new System.Drawing.Point(0, 41);
            this.tabControlPanelComments.Name = "tabControlPanelComments";
            this.tabControlPanelComments.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanelComments.Size = new System.Drawing.Size(610, 323);
            this.tabControlPanelComments.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.tabControlPanelComments.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.tabControlPanelComments.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanelComments.Style.BorderColor.Color = System.Drawing.Color.White;
            this.tabControlPanelComments.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanelComments.Style.GradientAngle = 90;
            this.tabControlPanelComments.TabIndex = 1;
            this.tabControlPanelComments.TabItem = this.tabItemComments;
            // 
            // adNotesWeekdaysSelectorComments
            // 
            this.adNotesWeekdaysSelectorComments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.adNotesWeekdaysSelectorComments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.adNotesWeekdaysSelectorComments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.adNotesWeekdaysSelectorComments.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adNotesWeekdaysSelectorComments.Location = new System.Drawing.Point(163, 257);
            this.adNotesWeekdaysSelectorComments.Name = "adNotesWeekdaysSelectorComments";
            this.adNotesWeekdaysSelectorComments.Size = new System.Drawing.Size(440, 59);
            this.adNotesWeekdaysSelectorComments.TabIndex = 9;
            // 
            // buttonXClearComment
            // 
            this.buttonXClearComment.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXClearComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXClearComment.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXClearComment.Location = new System.Drawing.Point(8, 257);
            this.buttonXClearComment.Name = "buttonXClearComment";
            this.buttonXClearComment.Size = new System.Drawing.Size(107, 59);
            this.buttonXClearComment.TabIndex = 8;
            this.buttonXClearComment.Text = "Clear\r\nComment";
            this.buttonXClearComment.TextColor = System.Drawing.Color.Black;
            this.buttonXClearComment.Click += new System.EventHandler(this.buttonXClearComment_Click);
            // 
            // ckCommentAll
            // 
            this.ckCommentAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ckCommentAll.Location = new System.Drawing.Point(245, 291);
            this.ckCommentAll.Name = "ckCommentAll";
            this.ckCommentAll.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ckCommentAll.Properties.Appearance.Options.UseFont = true;
            this.ckCommentAll.Properties.Appearance.Options.UseTextOptions = true;
            this.ckCommentAll.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ckCommentAll.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.ckCommentAll.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ckCommentAll.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.ckCommentAll.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ckCommentAll.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.ckCommentAll.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ckCommentAll.Properties.Caption = "Apply to All Lines in this Schedule";
            this.ckCommentAll.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ckCommentAll.Size = new System.Drawing.Size(358, 21);
            this.ckCommentAll.TabIndex = 5;
            // 
            // tabItemComments
            // 
            this.tabItemComments.AttachedControl = this.tabControlPanelComments;
            this.tabItemComments.Image = global::AdScheduleBuilder.Properties.Resources.Unselected;
            this.tabItemComments.Name = "tabItemComments";
            this.tabItemComments.Text = "Comments && Publications";
            // 
            // tabControlPanelSections
            // 
            this.tabControlPanelSections.Controls.Add(this.adNotesWeekdaysSelectorSections);
            this.tabControlPanelSections.Controls.Add(this.textEditSection);
            this.tabControlPanelSections.Controls.Add(this.ckSection);
            this.tabControlPanelSections.Controls.Add(this.buttonXClearSection);
            this.tabControlPanelSections.Controls.Add(this.checkedListBoxControlSections);
            this.tabControlPanelSections.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControlPanelSections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanelSections.Location = new System.Drawing.Point(0, 41);
            this.tabControlPanelSections.Name = "tabControlPanelSections";
            this.tabControlPanelSections.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanelSections.Size = new System.Drawing.Size(610, 323);
            this.tabControlPanelSections.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.tabControlPanelSections.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.tabControlPanelSections.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanelSections.Style.BorderColor.Color = System.Drawing.Color.White;
            this.tabControlPanelSections.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanelSections.Style.GradientAngle = 90;
            this.tabControlPanelSections.TabIndex = 2;
            this.tabControlPanelSections.TabItem = this.tabItemSections;
            // 
            // adNotesWeekdaysSelectorSections
            // 
            this.adNotesWeekdaysSelectorSections.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.adNotesWeekdaysSelectorSections.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.adNotesWeekdaysSelectorSections.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.adNotesWeekdaysSelectorSections.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adNotesWeekdaysSelectorSections.Location = new System.Drawing.Point(163, 257);
            this.adNotesWeekdaysSelectorSections.Name = "adNotesWeekdaysSelectorSections";
            this.adNotesWeekdaysSelectorSections.Size = new System.Drawing.Size(440, 59);
            this.adNotesWeekdaysSelectorSections.TabIndex = 10;
            // 
            // textEditSection
            // 
            this.textEditSection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditSection.Location = new System.Drawing.Point(33, 11);
            this.textEditSection.Name = "textEditSection";
            this.textEditSection.Size = new System.Drawing.Size(570, 20);
            this.textEditSection.TabIndex = 6;
            this.textEditSection.EditValueChanged += new System.EventHandler(this.textEditSection_EditValueChanged);
            // 
            // ckSection
            // 
            this.ckSection.EditValue = true;
            this.ckSection.Location = new System.Drawing.Point(8, 13);
            this.ckSection.Name = "ckSection";
            this.ckSection.Properties.Caption = "";
            this.ckSection.Size = new System.Drawing.Size(21, 19);
            this.ckSection.TabIndex = 5;
            this.ckSection.CheckedChanged += new System.EventHandler(this.ckSection_CheckedChanged);
            // 
            // checkedListBoxControlSections
            // 
            this.checkedListBoxControlSections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxControlSections.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.checkedListBoxControlSections.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkedListBoxControlSections.Appearance.Options.UseBackColor = true;
            this.checkedListBoxControlSections.Appearance.Options.UseFont = true;
            this.checkedListBoxControlSections.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.checkedListBoxControlSections.CheckOnClick = true;
            this.checkedListBoxControlSections.ItemHeight = 30;
            this.checkedListBoxControlSections.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Label A"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Label B"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Label C"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Label D")});
            this.checkedListBoxControlSections.Location = new System.Drawing.Point(8, 37);
            this.checkedListBoxControlSections.Name = "checkedListBoxControlSections";
            this.checkedListBoxControlSections.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.checkedListBoxControlSections.Size = new System.Drawing.Size(595, 214);
            this.checkedListBoxControlSections.TabIndex = 7;
            this.checkedListBoxControlSections.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.checkedListBoxControlSections_ItemCheck);
            // 
            // tabItemSections
            // 
            this.tabItemSections.AttachedControl = this.tabControlPanelSections;
            this.tabItemSections.Image = global::AdScheduleBuilder.Properties.Resources.Unselected;
            this.tabItemSections.Name = "tabItemSections";
            this.tabItemSections.Text = "Sections";
            // 
            // tabControlPanelDeadlines
            // 
            this.tabControlPanelDeadlines.Controls.Add(this.adNotesWeekdaysSelectorDeadlines);
            this.tabControlPanelDeadlines.Controls.Add(this.textEditDeadline);
            this.tabControlPanelDeadlines.Controls.Add(this.ckDeadline);
            this.tabControlPanelDeadlines.Controls.Add(this.checkedListBoxControlDeadline);
            this.tabControlPanelDeadlines.Controls.Add(this.laDeadline);
            this.tabControlPanelDeadlines.Controls.Add(this.buttonXClearDeadline);
            this.tabControlPanelDeadlines.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControlPanelDeadlines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanelDeadlines.Location = new System.Drawing.Point(0, 41);
            this.tabControlPanelDeadlines.Name = "tabControlPanelDeadlines";
            this.tabControlPanelDeadlines.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanelDeadlines.Size = new System.Drawing.Size(610, 323);
            this.tabControlPanelDeadlines.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.tabControlPanelDeadlines.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.tabControlPanelDeadlines.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanelDeadlines.Style.BorderColor.Color = System.Drawing.Color.White;
            this.tabControlPanelDeadlines.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanelDeadlines.Style.GradientAngle = 90;
            this.tabControlPanelDeadlines.TabIndex = 3;
            this.tabControlPanelDeadlines.TabItem = this.tabItemDeadlines;
            // 
            // adNotesWeekdaysSelectorDeadlines
            // 
            this.adNotesWeekdaysSelectorDeadlines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.adNotesWeekdaysSelectorDeadlines.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.adNotesWeekdaysSelectorDeadlines.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.adNotesWeekdaysSelectorDeadlines.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adNotesWeekdaysSelectorDeadlines.Location = new System.Drawing.Point(163, 257);
            this.adNotesWeekdaysSelectorDeadlines.Name = "adNotesWeekdaysSelectorDeadlines";
            this.adNotesWeekdaysSelectorDeadlines.Size = new System.Drawing.Size(440, 59);
            this.adNotesWeekdaysSelectorDeadlines.TabIndex = 23;
            // 
            // textEditDeadline
            // 
            this.textEditDeadline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditDeadline.Location = new System.Drawing.Point(33, 231);
            this.textEditDeadline.Name = "textEditDeadline";
            this.textEditDeadline.Size = new System.Drawing.Size(570, 20);
            this.textEditDeadline.TabIndex = 8;
            this.textEditDeadline.EditValueChanged += new System.EventHandler(this.textEditDeadline_EditValueChanged);
            // 
            // ckDeadline
            // 
            this.ckDeadline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ckDeadline.EditValue = true;
            this.ckDeadline.Location = new System.Drawing.Point(8, 232);
            this.ckDeadline.Name = "ckDeadline";
            this.ckDeadline.Properties.Caption = "";
            this.ckDeadline.Size = new System.Drawing.Size(21, 19);
            this.ckDeadline.TabIndex = 7;
            this.ckDeadline.CheckedChanged += new System.EventHandler(this.ckDeadline_CheckedChanged);
            // 
            // checkedListBoxControlDeadline
            // 
            this.checkedListBoxControlDeadline.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxControlDeadline.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.checkedListBoxControlDeadline.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkedListBoxControlDeadline.Appearance.Options.UseBackColor = true;
            this.checkedListBoxControlDeadline.Appearance.Options.UseFont = true;
            this.checkedListBoxControlDeadline.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.checkedListBoxControlDeadline.CheckOnClick = true;
            this.checkedListBoxControlDeadline.ItemHeight = 30;
            this.checkedListBoxControlDeadline.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Label A"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Label B"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Label C"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Label D")});
            this.checkedListBoxControlDeadline.Location = new System.Drawing.Point(8, 48);
            this.checkedListBoxControlDeadline.Name = "checkedListBoxControlDeadline";
            this.checkedListBoxControlDeadline.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.checkedListBoxControlDeadline.Size = new System.Drawing.Size(595, 177);
            this.checkedListBoxControlDeadline.TabIndex = 9;
            this.checkedListBoxControlDeadline.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.checkedListBoxControlDeadline_ItemCheck);
            // 
            // laDeadline
            // 
            this.laDeadline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laDeadline.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.laDeadline.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laDeadline.ForeColor = System.Drawing.Color.Red;
            this.laDeadline.Location = new System.Drawing.Point(10, 1);
            this.laDeadline.Name = "laDeadline";
            this.laDeadline.Size = new System.Drawing.Size(593, 44);
            this.laDeadline.TabIndex = 22;
            this.laDeadline.Text = "Deadline: Not Selected";
            this.laDeadline.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonXClearDeadline
            // 
            this.buttonXClearDeadline.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXClearDeadline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXClearDeadline.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXClearDeadline.Location = new System.Drawing.Point(8, 257);
            this.buttonXClearDeadline.Name = "buttonXClearDeadline";
            this.buttonXClearDeadline.Size = new System.Drawing.Size(107, 59);
            this.buttonXClearDeadline.TabIndex = 20;
            this.buttonXClearDeadline.Text = "Clear\r\nDeadline";
            this.buttonXClearDeadline.TextColor = System.Drawing.Color.Black;
            this.buttonXClearDeadline.Click += new System.EventHandler(this.buttonXClearDeadline_Click);
            // 
            // tabItemDeadlines
            // 
            this.tabItemDeadlines.AttachedControl = this.tabControlPanelDeadlines;
            this.tabItemDeadlines.Image = global::AdScheduleBuilder.Properties.Resources.Unselected;
            this.tabItemDeadlines.Name = "tabItemDeadlines";
            this.tabItemDeadlines.Text = "Deadlines";
            // 
            // tabControlPanelMechanicals
            // 
            this.tabControlPanelMechanicals.Controls.Add(this.adNotesWeekdaysSelectorMechanicals);
            this.tabControlPanelMechanicals.Controls.Add(this.buttonXClearMechanicals);
            this.tabControlPanelMechanicals.Controls.Add(this.xtraTabControlMechanicals);
            this.tabControlPanelMechanicals.Controls.Add(this.textEditMechanicals);
            this.tabControlPanelMechanicals.Controls.Add(this.ckMechanicals);
            this.tabControlPanelMechanicals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanelMechanicals.Location = new System.Drawing.Point(0, 41);
            this.tabControlPanelMechanicals.Name = "tabControlPanelMechanicals";
            this.tabControlPanelMechanicals.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanelMechanicals.Size = new System.Drawing.Size(610, 323);
            this.tabControlPanelMechanicals.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.tabControlPanelMechanicals.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.tabControlPanelMechanicals.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanelMechanicals.Style.BorderColor.Color = System.Drawing.Color.White;
            this.tabControlPanelMechanicals.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanelMechanicals.Style.GradientAngle = 90;
            this.tabControlPanelMechanicals.TabIndex = 4;
            this.tabControlPanelMechanicals.TabItem = this.tabItemMechanicals;
            // 
            // adNotesWeekdaysSelectorMechanicals
            // 
            this.adNotesWeekdaysSelectorMechanicals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.adNotesWeekdaysSelectorMechanicals.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.adNotesWeekdaysSelectorMechanicals.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.adNotesWeekdaysSelectorMechanicals.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adNotesWeekdaysSelectorMechanicals.Location = new System.Drawing.Point(163, 257);
            this.adNotesWeekdaysSelectorMechanicals.Name = "adNotesWeekdaysSelectorMechanicals";
            this.adNotesWeekdaysSelectorMechanicals.Size = new System.Drawing.Size(440, 59);
            this.adNotesWeekdaysSelectorMechanicals.TabIndex = 24;
            // 
            // buttonXClearMechanicals
            // 
            this.buttonXClearMechanicals.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXClearMechanicals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXClearMechanicals.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXClearMechanicals.Location = new System.Drawing.Point(8, 257);
            this.buttonXClearMechanicals.Name = "buttonXClearMechanicals";
            this.buttonXClearMechanicals.Size = new System.Drawing.Size(107, 59);
            this.buttonXClearMechanicals.TabIndex = 10;
            this.buttonXClearMechanicals.Text = "Clear\r\nMechanicals";
            this.buttonXClearMechanicals.TextColor = System.Drawing.Color.Black;
            this.buttonXClearMechanicals.Click += new System.EventHandler(this.buttonXClearMechanicals_Click);
            // 
            // xtraTabControlMechanicals
            // 
            this.xtraTabControlMechanicals.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.xtraTabControlMechanicals.Appearance.Options.UseFont = true;
            this.xtraTabControlMechanicals.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.xtraTabControlMechanicals.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabControlMechanicals.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.xtraTabControlMechanicals.AppearancePage.HeaderActive.Options.UseFont = true;
            this.xtraTabControlMechanicals.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.xtraTabControlMechanicals.Location = new System.Drawing.Point(8, 38);
            this.xtraTabControlMechanicals.Name = "xtraTabControlMechanicals";
            this.xtraTabControlMechanicals.Size = new System.Drawing.Size(595, 213);
            this.xtraTabControlMechanicals.TabIndex = 9;
            // 
            // textEditMechanicals
            // 
            this.textEditMechanicals.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditMechanicals.Enabled = false;
            this.textEditMechanicals.Location = new System.Drawing.Point(33, 11);
            this.textEditMechanicals.Name = "textEditMechanicals";
            this.textEditMechanicals.Size = new System.Drawing.Size(570, 20);
            this.textEditMechanicals.TabIndex = 8;
            this.textEditMechanicals.EditValueChanged += new System.EventHandler(this.textEditMechanicals_EditValueChanged);
            // 
            // ckMechanicals
            // 
            this.ckMechanicals.Location = new System.Drawing.Point(8, 13);
            this.ckMechanicals.Name = "ckMechanicals";
            this.ckMechanicals.Properties.Caption = "";
            this.ckMechanicals.Size = new System.Drawing.Size(21, 19);
            this.ckMechanicals.TabIndex = 7;
            this.ckMechanicals.CheckedChanged += new System.EventHandler(this.ckMechanicals_CheckedChanged);
            // 
            // tabItemMechanicals
            // 
            this.tabItemMechanicals.AttachedControl = this.tabControlPanelMechanicals;
            this.tabItemMechanicals.Image = global::AdScheduleBuilder.Properties.Resources.Unselected;
            this.tabItemMechanicals.Name = "tabItemMechanicals";
            this.tabItemMechanicals.Text = "Mechanicals";
            this.tabItemMechanicals.TextColor = System.Drawing.Color.Black;
            // 
            // persistentRepository
            // 
            this.persistentRepository.Items.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit,
            this.repositoryItemSpinEdit});
            // 
            // repositoryItemCheckEdit
            // 
            this.repositoryItemCheckEdit.AutoHeight = false;
            this.repositoryItemCheckEdit.Name = "repositoryItemCheckEdit";
            this.repositoryItemCheckEdit.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // repositoryItemSpinEdit
            // 
            this.repositoryItemSpinEdit.AutoHeight = false;
            this.repositoryItemSpinEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEdit.DisplayFormat.FormatString = "#,##0";
            this.repositoryItemSpinEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEdit.EditFormat.FormatString = "#,##0";
            this.repositoryItemSpinEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEdit.Name = "repositoryItemSpinEdit";
            this.repositoryItemSpinEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // pnMain
            // 
            this.pnMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnMain.Controls.Add(this.tabControlAdNotes);
            this.pnMain.Controls.Add(this.pnTop);
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Location = new System.Drawing.Point(0, 0);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(614, 454);
            this.pnMain.TabIndex = 18;
            // 
            // pnTop
            // 
            this.pnTop.Controls.Add(this.pbHelp);
            this.pnTop.Controls.Add(this.laFinalRate);
            this.pnTop.Controls.Add(this.laDate);
            this.pnTop.Controls.Add(this.laID);
            this.pnTop.Controls.Add(this.llaLogo);
            this.pnTop.Controls.Add(this.pbLogo);
            this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTop.Location = new System.Drawing.Point(0, 0);
            this.pnTop.Name = "pnTop";
            this.pnTop.Size = new System.Drawing.Size(610, 86);
            this.pnTop.TabIndex = 1;
            // 
            // pbHelp
            // 
            this.pbHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbHelp.Image = global::AdScheduleBuilder.Properties.Resources.Help;
            this.pbHelp.Location = new System.Drawing.Point(531, 3);
            this.pbHelp.Name = "pbHelp";
            this.pbHelp.Size = new System.Drawing.Size(76, 81);
            this.pbHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.superTooltip.SetSuperTooltip(this.pbHelp, new DevComponents.DotNetBar.SuperTooltipInfo("Ad-Notes", "", "Learn more about adding notes and comments to your schedule", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
            this.pbHelp.TabIndex = 25;
            this.pbHelp.TabStop = false;
            this.pbHelp.Click += new System.EventHandler(this.pbHelp_Click);
            this.pbHelp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbHelp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // laFinalRate
            // 
            this.laFinalRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laFinalRate.Location = new System.Drawing.Point(315, 56);
            this.laFinalRate.Name = "laFinalRate";
            this.laFinalRate.Size = new System.Drawing.Size(211, 25);
            this.laFinalRate.TabIndex = 9;
            this.laFinalRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // laDate
            // 
            this.laDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laDate.Location = new System.Drawing.Point(315, 31);
            this.laDate.Name = "laDate";
            this.laDate.Size = new System.Drawing.Size(211, 25);
            this.laDate.TabIndex = 8;
            this.laDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // laID
            // 
            this.laID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laID.Location = new System.Drawing.Point(318, 6);
            this.laID.Name = "laID";
            this.laID.Size = new System.Drawing.Size(208, 25);
            this.laID.TabIndex = 7;
            this.laID.Text = "Line ID: {0}";
            this.laID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // llaLogo
            // 
            this.llaLogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.llaLogo.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.llaLogo.Location = new System.Drawing.Point(81, 13);
            this.llaLogo.Name = "llaLogo";
            this.llaLogo.Size = new System.Drawing.Size(247, 59);
            this.llaLogo.TabIndex = 6;
            this.llaLogo.Text = "Important Details about\r\nthis Scheduled Ad…\r\n";
            this.llaLogo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbLogo
            // 
            this.pbLogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pbLogo.Image = global::AdScheduleBuilder.Properties.Resources.AdNoteNormal;
            this.pbLogo.Location = new System.Drawing.Point(9, 7);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(64, 68);
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
            this.pnButtons.Location = new System.Drawing.Point(0, 454);
            this.pnButtons.Name = "pnButtons";
            this.pnButtons.Size = new System.Drawing.Size(614, 53);
            this.pnButtons.TabIndex = 19;
            // 
            // superTooltip
            // 
            this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            // 
            // FormAdNotes
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(614, 507);
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.pnButtons);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(365, 260);
            this.Name = "FormAdNotes";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ad-Notes";
            this.Load += new System.EventHandler(this.FormAdNotes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ckComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlComments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdNotes)).EndInit();
            this.tabControlAdNotes.ResumeLayout(false);
            this.tabControlPanelComments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ckCommentAll.Properties)).EndInit();
            this.tabControlPanelSections.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditSection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckSection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlSections)).EndInit();
            this.tabControlPanelDeadlines.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditDeadline.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckDeadline.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlDeadline)).EndInit();
            this.tabControlPanelMechanicals.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlMechanicals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditMechanicals.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckMechanicals.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit)).EndInit();
            this.pnMain.ResumeLayout(false);
            this.pnTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbHelp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.pnButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit ckComment;
        private DevExpress.XtraEditors.TextEdit textEditComment;
        private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControlComments;
        private DevComponents.DotNetBar.ButtonX buttonXOK;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevComponents.DotNetBar.ButtonX buttonXClearSection;
        private DevComponents.DotNetBar.TabControl tabControlAdNotes;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanelComments;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanelDeadlines;
        private DevComponents.DotNetBar.TabItem tabItemDeadlines;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanelSections;
        private DevComponents.DotNetBar.TabItem tabItemSections;
        private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.Panel pnTop;
        public System.Windows.Forms.Label laFinalRate;
        public System.Windows.Forms.Label laDate;
        public System.Windows.Forms.Label laID;
        private System.Windows.Forms.Label llaLogo;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Panel pnButtons;
        private DevExpress.XtraEditors.TextEdit textEditSection;
        private DevExpress.XtraEditors.CheckEdit ckSection;
        private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControlSections;
        public DevComponents.DotNetBar.TabItem tabItemComments;
        public DevExpress.XtraEditors.CheckEdit ckCommentAll;
        private DevComponents.DotNetBar.ButtonX buttonXClearComment;
        private DevExpress.XtraEditors.TextEdit textEditDeadline;
        private DevExpress.XtraEditors.CheckEdit ckDeadline;
        private DevComponents.DotNetBar.ButtonX buttonXClearDeadline;
        private System.Windows.Forms.Label laDeadline;
        private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControlDeadline;
        private DevExpress.XtraEditors.Repository.PersistentRepository persistentRepository;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanelMechanicals;
        private DevComponents.DotNetBar.TabItem tabItemMechanicals;
        private DevExpress.XtraEditors.TextEdit textEditMechanicals;
        private DevExpress.XtraEditors.CheckEdit ckMechanicals;
        private DevComponents.DotNetBar.ButtonX buttonXClearMechanicals;
        public DevExpress.XtraTab.XtraTabControl xtraTabControlMechanicals;
        public CustomControls.AdNotesWeekdaysSelector adNotesWeekdaysSelectorComments;
        public CustomControls.AdNotesWeekdaysSelector adNotesWeekdaysSelectorSections;
        public CustomControls.AdNotesWeekdaysSelector adNotesWeekdaysSelectorDeadlines;
        public CustomControls.AdNotesWeekdaysSelector adNotesWeekdaysSelectorMechanicals;
        private System.Windows.Forms.PictureBox pbHelp;
        public DevComponents.DotNetBar.SuperTooltip superTooltip;
    }
}