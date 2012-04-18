namespace CalendarBuilder.CustomControls.DayProperties
{
    partial class DigitalPropertiesControl
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
            this.memoEditCustomNote = new DevExpress.XtraEditors.MemoEdit();
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.comboBoxEditQuickList = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEditCategory = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEditSubCategory = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEditProduct = new DevExpress.XtraEditors.ComboBoxEdit();
            this.xtraScrollableControl = new DevExpress.XtraEditors.XtraScrollableControl();
            this.buttonXQuickList = new DevComponents.DotNetBar.ButtonX();
            this.buttonXCustomNote = new DevComponents.DotNetBar.ButtonX();
            this.buttonXInventory = new DevComponents.DotNetBar.ButtonX();
            this.checkEditShowProduct = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditShowSubCategory = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditShowCategory = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditCustomNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditQuickList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSubCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditProduct.Properties)).BeginInit();
            this.xtraScrollableControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditShowProduct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditShowSubCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditShowCategory.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // memoEditCustomNote
            // 
            this.memoEditCustomNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memoEditCustomNote.Enabled = false;
            this.memoEditCustomNote.Location = new System.Drawing.Point(10, 33);
            this.memoEditCustomNote.Name = "memoEditCustomNote";
            this.memoEditCustomNote.Properties.NullText = "Type Here";
            this.memoEditCustomNote.Size = new System.Drawing.Size(280, 83);
            this.memoEditCustomNote.StyleController = this.styleController;
            this.memoEditCustomNote.TabIndex = 7;
            this.memoEditCustomNote.EditValueChanged += new System.EventHandler(this.editor_EditValueChanged);
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
            // comboBoxEditQuickList
            // 
            this.comboBoxEditQuickList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEditQuickList.Enabled = false;
            this.comboBoxEditQuickList.Location = new System.Drawing.Point(10, 200);
            this.comboBoxEditQuickList.Name = "comboBoxEditQuickList";
            this.comboBoxEditQuickList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditQuickList.Properties.NullText = "My Favorite Quick List";
            this.comboBoxEditQuickList.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditQuickList.Size = new System.Drawing.Size(280, 22);
            this.comboBoxEditQuickList.StyleController = this.styleController;
            this.comboBoxEditQuickList.TabIndex = 9;
            this.comboBoxEditQuickList.EditValueChanged += new System.EventHandler(this.editor_EditValueChanged);
            // 
            // comboBoxEditCategory
            // 
            this.comboBoxEditCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEditCategory.Enabled = false;
            this.comboBoxEditCategory.Location = new System.Drawing.Point(30, 315);
            this.comboBoxEditCategory.Name = "comboBoxEditCategory";
            this.comboBoxEditCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditCategory.Properties.NullText = "Select Category...";
            this.comboBoxEditCategory.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditCategory.Size = new System.Drawing.Size(260, 22);
            this.comboBoxEditCategory.StyleController = this.styleController;
            this.comboBoxEditCategory.TabIndex = 11;
            this.comboBoxEditCategory.EditValueChanged += new System.EventHandler(this.comboBoxEditCategory_EditValueChanged);
            // 
            // comboBoxEditSubCategory
            // 
            this.comboBoxEditSubCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEditSubCategory.Enabled = false;
            this.comboBoxEditSubCategory.Location = new System.Drawing.Point(30, 352);
            this.comboBoxEditSubCategory.Name = "comboBoxEditSubCategory";
            this.comboBoxEditSubCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditSubCategory.Properties.NullText = "Select Sub-Group...";
            this.comboBoxEditSubCategory.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditSubCategory.Size = new System.Drawing.Size(260, 22);
            this.comboBoxEditSubCategory.StyleController = this.styleController;
            this.comboBoxEditSubCategory.TabIndex = 12;
            this.comboBoxEditSubCategory.EditValueChanged += new System.EventHandler(this.comboBoxEditSubCategory_EditValueChanged);
            // 
            // comboBoxEditProduct
            // 
            this.comboBoxEditProduct.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEditProduct.Enabled = false;
            this.comboBoxEditProduct.Location = new System.Drawing.Point(30, 389);
            this.comboBoxEditProduct.Name = "comboBoxEditProduct";
            this.comboBoxEditProduct.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditProduct.Properties.NullText = "Type or Select Digital Inventory..";
            this.comboBoxEditProduct.Size = new System.Drawing.Size(260, 22);
            this.comboBoxEditProduct.StyleController = this.styleController;
            this.comboBoxEditProduct.TabIndex = 13;
            this.comboBoxEditProduct.EditValueChanged += new System.EventHandler(this.editor_EditValueChanged);
            // 
            // xtraScrollableControl
            // 
            this.xtraScrollableControl.Controls.Add(this.buttonXQuickList);
            this.xtraScrollableControl.Controls.Add(this.buttonXCustomNote);
            this.xtraScrollableControl.Controls.Add(this.buttonXInventory);
            this.xtraScrollableControl.Controls.Add(this.checkEditShowProduct);
            this.xtraScrollableControl.Controls.Add(this.checkEditShowSubCategory);
            this.xtraScrollableControl.Controls.Add(this.checkEditShowCategory);
            this.xtraScrollableControl.Controls.Add(this.comboBoxEditProduct);
            this.xtraScrollableControl.Controls.Add(this.comboBoxEditSubCategory);
            this.xtraScrollableControl.Controls.Add(this.memoEditCustomNote);
            this.xtraScrollableControl.Controls.Add(this.comboBoxEditCategory);
            this.xtraScrollableControl.Controls.Add(this.comboBoxEditQuickList);
            this.xtraScrollableControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControl.Name = "xtraScrollableControl";
            this.xtraScrollableControl.Size = new System.Drawing.Size(300, 425);
            this.xtraScrollableControl.TabIndex = 14;
            // 
            // buttonXQuickList
            // 
            this.buttonXQuickList.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXQuickList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXQuickList.AutoCheckOnClick = true;
            this.buttonXQuickList.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXQuickList.Enabled = false;
            this.buttonXQuickList.Location = new System.Drawing.Point(10, 170);
            this.buttonXQuickList.Name = "buttonXQuickList";
            this.buttonXQuickList.Size = new System.Drawing.Size(280, 24);
            this.buttonXQuickList.TabIndex = 22;
            this.buttonXQuickList.Text = "B. My Favorite Quick List";
            this.buttonXQuickList.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXQuickList.CheckedChanged += new System.EventHandler(this.checkEditQuickList_CheckedChanged);
            // 
            // buttonXCustomNote
            // 
            this.buttonXCustomNote.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXCustomNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXCustomNote.AutoCheckOnClick = true;
            this.buttonXCustomNote.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXCustomNote.Location = new System.Drawing.Point(10, 3);
            this.buttonXCustomNote.Name = "buttonXCustomNote";
            this.buttonXCustomNote.Size = new System.Drawing.Size(280, 24);
            this.buttonXCustomNote.TabIndex = 21;
            this.buttonXCustomNote.Text = "A. Type a Custom Digital Note";
            this.buttonXCustomNote.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXCustomNote.CheckedChanged += new System.EventHandler(this.checkEditUseCustomNote_CheckedChanged);
            // 
            // buttonXInventory
            // 
            this.buttonXInventory.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXInventory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXInventory.AutoCheckOnClick = true;
            this.buttonXInventory.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXInventory.Location = new System.Drawing.Point(10, 276);
            this.buttonXInventory.Name = "buttonXInventory";
            this.buttonXInventory.Size = new System.Drawing.Size(280, 24);
            this.buttonXInventory.TabIndex = 20;
            this.buttonXInventory.Text = "C. Select from Digital Inventory?";
            this.buttonXInventory.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXInventory.CheckedChanged += new System.EventHandler(this.checkEditInventory_CheckedChanged);
            // 
            // checkEditShowProduct
            // 
            this.checkEditShowProduct.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkEditShowProduct.Location = new System.Drawing.Point(8, 389);
            this.checkEditShowProduct.Name = "checkEditShowProduct";
            this.checkEditShowProduct.Properties.AutoWidth = true;
            this.checkEditShowProduct.Properties.Caption = "";
            this.checkEditShowProduct.Size = new System.Drawing.Size(23, 21);
            this.checkEditShowProduct.StyleController = this.styleController;
            this.checkEditShowProduct.TabIndex = 19;
            this.checkEditShowProduct.CheckedChanged += new System.EventHandler(this.editor_EditValueChanged);
            // 
            // checkEditShowSubCategory
            // 
            this.checkEditShowSubCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkEditShowSubCategory.Location = new System.Drawing.Point(8, 352);
            this.checkEditShowSubCategory.Name = "checkEditShowSubCategory";
            this.checkEditShowSubCategory.Properties.AutoWidth = true;
            this.checkEditShowSubCategory.Properties.Caption = "";
            this.checkEditShowSubCategory.Size = new System.Drawing.Size(23, 21);
            this.checkEditShowSubCategory.StyleController = this.styleController;
            this.checkEditShowSubCategory.TabIndex = 18;
            this.checkEditShowSubCategory.CheckedChanged += new System.EventHandler(this.editor_EditValueChanged);
            // 
            // checkEditShowCategory
            // 
            this.checkEditShowCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkEditShowCategory.Location = new System.Drawing.Point(8, 315);
            this.checkEditShowCategory.Name = "checkEditShowCategory";
            this.checkEditShowCategory.Properties.AutoWidth = true;
            this.checkEditShowCategory.Properties.Caption = "";
            this.checkEditShowCategory.Size = new System.Drawing.Size(23, 21);
            this.checkEditShowCategory.StyleController = this.styleController;
            this.checkEditShowCategory.TabIndex = 17;
            this.checkEditShowCategory.CheckedChanged += new System.EventHandler(this.editor_EditValueChanged);
            // 
            // DigitalPropertiesControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.xtraScrollableControl);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "DigitalPropertiesControl";
            this.Size = new System.Drawing.Size(300, 425);
            ((System.ComponentModel.ISupportInitialize)(this.memoEditCustomNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditQuickList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSubCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditProduct.Properties)).EndInit();
            this.xtraScrollableControl.ResumeLayout(false);
            this.xtraScrollableControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditShowProduct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditShowSubCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditShowCategory.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.MemoEdit memoEditCustomNote;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditQuickList;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditCategory;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSubCategory;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditProduct;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl;
        public DevExpress.XtraEditors.CheckEdit checkEditShowCategory;
        public DevExpress.XtraEditors.CheckEdit checkEditShowProduct;
        public DevExpress.XtraEditors.CheckEdit checkEditShowSubCategory;
        private DevComponents.DotNetBar.ButtonX buttonXQuickList;
        private DevComponents.DotNetBar.ButtonX buttonXCustomNote;
        private DevComponents.DotNetBar.ButtonX buttonXInventory;
    }
}
