namespace AdScheduleBuilder.ToolForms
{
    partial class FormChangeAdStrategy
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
            this.laHeader = new System.Windows.Forms.Label();
            this.laPublication = new System.Windows.Forms.Label();
            this.rbSave = new System.Windows.Forms.RadioButton();
            this.rbReset = new System.Windows.Forms.RadioButton();
            this.rbDelete = new System.Windows.Forms.RadioButton();
            this.ckDeleteAllAdNotes = new System.Windows.Forms.CheckBox();
            this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            this.ckDeleteAllDiscounts = new System.Windows.Forms.CheckBox();
            this.ckDeleteAllColorRates = new System.Windows.Forms.CheckBox();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // laHeader
            // 
            this.laHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laHeader.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laHeader.Location = new System.Drawing.Point(80, 0);
            this.laHeader.Name = "laHeader";
            this.laHeader.Size = new System.Drawing.Size(444, 31);
            this.laHeader.TabIndex = 0;
            this.laHeader.Text = "You are CHANGING YOUR PRICING STRATEGY for:";
            this.laHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // laPublication
            // 
            this.laPublication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laPublication.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laPublication.Location = new System.Drawing.Point(83, 31);
            this.laPublication.Name = "laPublication";
            this.laPublication.Size = new System.Drawing.Size(441, 28);
            this.laPublication.TabIndex = 1;
            this.laPublication.Text = "Publication";
            // 
            // rbSave
            // 
            this.rbSave.AutoSize = true;
            this.rbSave.Checked = true;
            this.rbSave.Location = new System.Drawing.Point(12, 62);
            this.rbSave.Name = "rbSave";
            this.rbSave.Size = new System.Drawing.Size(329, 20);
            this.rbSave.TabIndex = 2;
            this.rbSave.TabStop = true;
            this.rbSave.Text = "Keep All Current Lines and Rates for this Publication";
            this.rbSave.UseVisualStyleBackColor = true;
            this.rbSave.CheckedChanged += new System.EventHandler(this.rbSave_CheckedChanged);
            // 
            // rbReset
            // 
            this.rbReset.AutoSize = true;
            this.rbReset.Location = new System.Drawing.Point(12, 97);
            this.rbReset.Name = "rbReset";
            this.rbReset.Size = new System.Drawing.Size(290, 20);
            this.rbReset.TabIndex = 3;
            this.rbReset.Text = "Keep All Lines and Reset Rates to ZERO ($0)";
            this.rbReset.UseVisualStyleBackColor = true;
            this.rbReset.CheckedChanged += new System.EventHandler(this.rbSave_CheckedChanged);
            // 
            // rbDelete
            // 
            this.rbDelete.AutoSize = true;
            this.rbDelete.Location = new System.Drawing.Point(12, 182);
            this.rbDelete.Name = "rbDelete";
            this.rbDelete.Size = new System.Drawing.Size(230, 20);
            this.rbDelete.TabIndex = 4;
            this.rbDelete.Text = "Delete all Lines  for this Publication";
            this.rbDelete.UseVisualStyleBackColor = true;
            this.rbDelete.CheckedChanged += new System.EventHandler(this.rbSave_CheckedChanged);
            // 
            // ckDeleteAllAdNotes
            // 
            this.ckDeleteAllAdNotes.AutoSize = true;
            this.ckDeleteAllAdNotes.ForeColor = System.Drawing.Color.Black;
            this.ckDeleteAllAdNotes.Location = new System.Drawing.Point(29, 208);
            this.ckDeleteAllAdNotes.Name = "ckDeleteAllAdNotes";
            this.ckDeleteAllAdNotes.Size = new System.Drawing.Size(138, 20);
            this.ckDeleteAllAdNotes.TabIndex = 5;
            this.ckDeleteAllAdNotes.Text = "Delete all Ad-Notes";
            this.ckDeleteAllAdNotes.UseVisualStyleBackColor = false;
            // 
            // buttonXOK
            // 
            this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXOK.ForeColor = System.Drawing.Color.Black;
            this.buttonXOK.Location = new System.Drawing.Point(363, 238);
            this.buttonXOK.Name = "buttonXOK";
            this.buttonXOK.Size = new System.Drawing.Size(75, 34);
            this.buttonXOK.TabIndex = 7;
            this.buttonXOK.Text = "Continue";
            // 
            // buttonXCancel
            // 
            this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXCancel.ForeColor = System.Drawing.Color.Black;
            this.buttonXCancel.Location = new System.Drawing.Point(449, 238);
            this.buttonXCancel.Name = "buttonXCancel";
            this.buttonXCancel.Size = new System.Drawing.Size(75, 34);
            this.buttonXCancel.TabIndex = 8;
            this.buttonXCancel.Text = "Cancel";
            // 
            // ckDeleteAllDiscounts
            // 
            this.ckDeleteAllDiscounts.AutoSize = true;
            this.ckDeleteAllDiscounts.Enabled = false;
            this.ckDeleteAllDiscounts.ForeColor = System.Drawing.Color.Black;
            this.ckDeleteAllDiscounts.Location = new System.Drawing.Point(29, 123);
            this.ckDeleteAllDiscounts.Name = "ckDeleteAllDiscounts";
            this.ckDeleteAllDiscounts.Size = new System.Drawing.Size(143, 20);
            this.ckDeleteAllDiscounts.TabIndex = 9;
            this.ckDeleteAllDiscounts.Text = "Delete all Discounts";
            this.ckDeleteAllDiscounts.UseVisualStyleBackColor = false;
            // 
            // ckDeleteAllColorRates
            // 
            this.ckDeleteAllColorRates.AutoSize = true;
            this.ckDeleteAllColorRates.Enabled = false;
            this.ckDeleteAllColorRates.ForeColor = System.Drawing.Color.Black;
            this.ckDeleteAllColorRates.Location = new System.Drawing.Point(29, 149);
            this.ckDeleteAllColorRates.Name = "ckDeleteAllColorRates";
            this.ckDeleteAllColorRates.Size = new System.Drawing.Size(153, 20);
            this.ckDeleteAllColorRates.TabIndex = 10;
            this.ckDeleteAllColorRates.Text = "Delete all Color Rates";
            this.ckDeleteAllColorRates.UseVisualStyleBackColor = false;
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.Location = new System.Drawing.Point(12, 6);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxImage.TabIndex = 11;
            this.pictureBoxImage.TabStop = false;
            // 
            // FormChangeAdStrategy
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(536, 280);
            this.Controls.Add(this.pictureBoxImage);
            this.Controls.Add(this.ckDeleteAllColorRates);
            this.Controls.Add(this.ckDeleteAllDiscounts);
            this.Controls.Add(this.buttonXCancel);
            this.Controls.Add(this.buttonXOK);
            this.Controls.Add(this.ckDeleteAllAdNotes);
            this.Controls.Add(this.rbDelete);
            this.Controls.Add(this.rbReset);
            this.Controls.Add(this.rbSave);
            this.Controls.Add(this.laPublication);
            this.Controls.Add(this.laHeader);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormChangeAdStrategy";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change Pricing Strategy?";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label laHeader;
        private DevComponents.DotNetBar.ButtonX buttonXOK;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        public System.Windows.Forms.CheckBox ckDeleteAllAdNotes;
        public System.Windows.Forms.RadioButton rbSave;
        public System.Windows.Forms.RadioButton rbReset;
        public System.Windows.Forms.RadioButton rbDelete;
        public System.Windows.Forms.Label laPublication;
        public System.Windows.Forms.CheckBox ckDeleteAllDiscounts;
        public System.Windows.Forms.CheckBox ckDeleteAllColorRates;
        public System.Windows.Forms.PictureBox pictureBoxImage;
    }
}