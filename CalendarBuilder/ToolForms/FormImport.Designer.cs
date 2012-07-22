﻿namespace CalendarBuilder.ToolForms
{
    partial class FormImport
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
            this.laTitle = new System.Windows.Forms.Label();
            this.gridControlSchedules = new DevExpress.XtraGrid.GridControl();
            this.gridViewSchedules = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnBusinessName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnScheduleFile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnLastModifiedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemComboBoxStatus = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.buttonXImport = new DevComponents.DotNetBar.ButtonX();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            this.laCalendarType = new System.Windows.Forms.Label();
            this.checkEditAdvanced = new DevExpress.XtraEditors.CheckEdit();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.checkEditGraphic = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditSimple = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSchedules)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSchedules)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditAdvanced.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditGraphic.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditSimple.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // laTitle
            // 
            this.laTitle.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.laTitle.Location = new System.Drawing.Point(12, 9);
            this.laTitle.Name = "laTitle";
            this.laTitle.Size = new System.Drawing.Size(645, 46);
            this.laTitle.TabIndex = 24;
            this.laTitle.Text = "Select Ad Schedule…";
            this.laTitle.UseMnemonic = false;
            this.laTitle.Click += new System.EventHandler(this.Outside_Click);
            // 
            // gridControlSchedules
            // 
            this.gridControlSchedules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gridControlSchedules.Location = new System.Drawing.Point(12, 58);
            this.gridControlSchedules.MainView = this.gridViewSchedules;
            this.gridControlSchedules.Name = "gridControlSchedules";
            this.gridControlSchedules.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit,
            this.repositoryItemComboBoxStatus});
            this.gridControlSchedules.Size = new System.Drawing.Size(645, 297);
            this.gridControlSchedules.TabIndex = 22;
            this.gridControlSchedules.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSchedules});
            this.gridControlSchedules.Click += new System.EventHandler(this.Outside_Click);
            // 
            // gridViewSchedules
            // 
            this.gridViewSchedules.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridViewSchedules.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewSchedules.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewSchedules.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewSchedules.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridViewSchedules.Appearance.Row.Options.UseFont = true;
            this.gridViewSchedules.Appearance.Row.Options.UseTextOptions = true;
            this.gridViewSchedules.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridViewSchedules.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnBusinessName,
            this.gridColumnScheduleFile,
            this.gridColumnLastModifiedDate,
            this.gridColumnStatus});
            this.gridViewSchedules.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridViewSchedules.GridControl = this.gridControlSchedules;
            this.gridViewSchedules.Name = "gridViewSchedules";
            this.gridViewSchedules.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewSchedules.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewSchedules.OptionsBehavior.Editable = false;
            this.gridViewSchedules.OptionsBehavior.ReadOnly = true;
            this.gridViewSchedules.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewSchedules.OptionsCustomization.AllowFilter = false;
            this.gridViewSchedules.OptionsCustomization.AllowGroup = false;
            this.gridViewSchedules.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewSchedules.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridViewSchedules.OptionsMenu.EnableColumnMenu = false;
            this.gridViewSchedules.OptionsMenu.EnableFooterMenu = false;
            this.gridViewSchedules.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridViewSchedules.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.gridViewSchedules.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.gridViewSchedules.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewSchedules.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridViewSchedules.OptionsView.ShowDetailButtons = false;
            this.gridViewSchedules.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridViewSchedules.OptionsView.ShowGroupPanel = false;
            this.gridViewSchedules.OptionsView.ShowIndicator = false;
            this.gridViewSchedules.RowHeight = 40;
            this.gridViewSchedules.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewSchedules_RowClick);
            // 
            // gridColumnBusinessName
            // 
            this.gridColumnBusinessName.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnBusinessName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumnBusinessName.Caption = "Advertiser";
            this.gridColumnBusinessName.FieldName = "BusinessName";
            this.gridColumnBusinessName.Name = "gridColumnBusinessName";
            this.gridColumnBusinessName.Visible = true;
            this.gridColumnBusinessName.VisibleIndex = 0;
            this.gridColumnBusinessName.Width = 181;
            // 
            // gridColumnScheduleFile
            // 
            this.gridColumnScheduleFile.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnScheduleFile.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumnScheduleFile.Caption = "Schedule File";
            this.gridColumnScheduleFile.FieldName = "ShortFileName";
            this.gridColumnScheduleFile.Name = "gridColumnScheduleFile";
            this.gridColumnScheduleFile.Visible = true;
            this.gridColumnScheduleFile.VisibleIndex = 1;
            this.gridColumnScheduleFile.Width = 186;
            // 
            // gridColumnLastModifiedDate
            // 
            this.gridColumnLastModifiedDate.Caption = "Last Modified";
            this.gridColumnLastModifiedDate.FieldName = "LastModifiedDate";
            this.gridColumnLastModifiedDate.Name = "gridColumnLastModifiedDate";
            this.gridColumnLastModifiedDate.Visible = true;
            this.gridColumnLastModifiedDate.VisibleIndex = 2;
            this.gridColumnLastModifiedDate.Width = 150;
            // 
            // gridColumnStatus
            // 
            this.gridColumnStatus.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gridColumnStatus.AppearanceCell.Options.UseFont = true;
            this.gridColumnStatus.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnStatus.Caption = "Status";
            this.gridColumnStatus.FieldName = "Status";
            this.gridColumnStatus.Name = "gridColumnStatus";
            this.gridColumnStatus.Visible = true;
            this.gridColumnStatus.VisibleIndex = 3;
            this.gridColumnStatus.Width = 124;
            // 
            // repositoryItemButtonEdit
            // 
            this.repositoryItemButtonEdit.AutoHeight = false;
            this.repositoryItemButtonEdit.DisplayFormat.FormatString = "MM/dd/yy h:mmtt";
            this.repositoryItemButtonEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemButtonEdit.EditFormat.FormatString = "MM/dd/yy h:mmtt";
            this.repositoryItemButtonEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemButtonEdit.Name = "repositoryItemButtonEdit";
            this.repositoryItemButtonEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // repositoryItemComboBoxStatus
            // 
            this.repositoryItemComboBoxStatus.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxStatus.Appearance.Options.UseFont = true;
            this.repositoryItemComboBoxStatus.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxStatus.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemComboBoxStatus.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxStatus.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemComboBoxStatus.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxStatus.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemComboBoxStatus.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxStatus.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemComboBoxStatus.AutoHeight = false;
            this.repositoryItemComboBoxStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxStatus.Name = "repositoryItemComboBoxStatus";
            this.repositoryItemComboBoxStatus.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // buttonXImport
            // 
            this.buttonXImport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXImport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXImport.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXImport.Enabled = false;
            this.buttonXImport.Location = new System.Drawing.Point(159, 413);
            this.buttonXImport.Name = "buttonXImport";
            this.buttonXImport.Size = new System.Drawing.Size(136, 44);
            this.buttonXImport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXImport.TabIndex = 25;
            this.buttonXImport.Text = "Import";
            this.buttonXImport.TextColor = System.Drawing.Color.Black;
            // 
            // buttonXCancel
            // 
            this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXCancel.Location = new System.Drawing.Point(376, 413);
            this.buttonXCancel.Name = "buttonXCancel";
            this.buttonXCancel.Size = new System.Drawing.Size(136, 44);
            this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXCancel.TabIndex = 26;
            this.buttonXCancel.Text = "Cancel";
            this.buttonXCancel.TextColor = System.Drawing.Color.Black;
            // 
            // laCalendarType
            // 
            this.laCalendarType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.laCalendarType.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laCalendarType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.laCalendarType.Location = new System.Drawing.Point(12, 369);
            this.laCalendarType.Name = "laCalendarType";
            this.laCalendarType.Size = new System.Drawing.Size(309, 26);
            this.laCalendarType.TabIndex = 27;
            this.laCalendarType.Text = "What calendars you want to build:";
            this.laCalendarType.UseMnemonic = false;
            // 
            // checkEditAdvanced
            // 
            this.checkEditAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkEditAdvanced.EditValue = true;
            this.checkEditAdvanced.Location = new System.Drawing.Point(330, 369);
            this.checkEditAdvanced.Name = "checkEditAdvanced";
            this.checkEditAdvanced.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkEditAdvanced.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.checkEditAdvanced.Properties.Appearance.Options.UseFont = true;
            this.checkEditAdvanced.Properties.Appearance.Options.UseForeColor = true;
            this.checkEditAdvanced.Properties.Caption = "NERD";
            this.checkEditAdvanced.Size = new System.Drawing.Size(75, 24);
            this.checkEditAdvanced.TabIndex = 28;
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // checkEditGraphic
            // 
            this.checkEditGraphic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkEditGraphic.EditValue = true;
            this.checkEditGraphic.Location = new System.Drawing.Point(411, 369);
            this.checkEditGraphic.Name = "checkEditGraphic";
            this.checkEditGraphic.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkEditGraphic.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.checkEditGraphic.Properties.Appearance.Options.UseFont = true;
            this.checkEditGraphic.Properties.Appearance.Options.UseForeColor = true;
            this.checkEditGraphic.Properties.Caption = "COOL";
            this.checkEditGraphic.Size = new System.Drawing.Size(75, 24);
            this.checkEditGraphic.TabIndex = 29;
            // 
            // checkEditSimple
            // 
            this.checkEditSimple.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkEditSimple.EditValue = true;
            this.checkEditSimple.Location = new System.Drawing.Point(492, 369);
            this.checkEditSimple.Name = "checkEditSimple";
            this.checkEditSimple.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkEditSimple.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.checkEditSimple.Properties.Appearance.Options.UseFont = true;
            this.checkEditSimple.Properties.Appearance.Options.UseForeColor = true;
            this.checkEditSimple.Properties.Caption = "EASY";
            this.checkEditSimple.Size = new System.Drawing.Size(75, 24);
            this.checkEditSimple.TabIndex = 30;
            // 
            // FormImport
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(671, 469);
            this.Controls.Add(this.checkEditSimple);
            this.Controls.Add(this.checkEditGraphic);
            this.Controls.Add(this.checkEditAdvanced);
            this.Controls.Add(this.laCalendarType);
            this.Controls.Add(this.buttonXCancel);
            this.Controls.Add(this.buttonXImport);
            this.Controls.Add(this.laTitle);
            this.Controls.Add(this.gridControlSchedules);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Schedule";
            this.Load += new System.EventHandler(this.FormImport_Load);
            this.Click += new System.EventHandler(this.Outside_Click);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSchedules)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSchedules)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditAdvanced.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditGraphic.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditSimple.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label laTitle;
        private DevExpress.XtraGrid.GridControl gridControlSchedules;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSchedules;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnBusinessName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnScheduleFile;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnLastModifiedDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnStatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxStatus;
        private DevComponents.DotNetBar.ButtonX buttonXImport;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private System.Windows.Forms.Label laCalendarType;
        private DevExpress.XtraEditors.CheckEdit checkEditAdvanced;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraEditors.CheckEdit checkEditGraphic;
        private DevExpress.XtraEditors.CheckEdit checkEditSimple;
    }
}