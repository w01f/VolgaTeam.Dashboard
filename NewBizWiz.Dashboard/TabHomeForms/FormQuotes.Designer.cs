namespace NewBizWiz.Dashboard.TabHomeForms
{
    partial class FormQuotes
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
            this.laDescription = new System.Windows.Forms.Label();
            this.checkedListBoxControlQuotes = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlQuotes)).BeginInit();
            this.SuspendLayout();
            // 
            // laTitle
            // 
            this.laTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.laTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laTitle.ForeColor = System.Drawing.Color.Black;
            this.laTitle.Location = new System.Drawing.Point(-4, 0);
            this.laTitle.Name = "laTitle";
            this.laTitle.Size = new System.Drawing.Size(697, 43);
            this.laTitle.TabIndex = 9;
            this.laTitle.Text = "Sales Quotes Library";
            // 
            // laDescription
            // 
            this.laDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.laDescription.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laDescription.ForeColor = System.Drawing.Color.Black;
            this.laDescription.Location = new System.Drawing.Point(-4, 43);
            this.laDescription.Name = "laDescription";
            this.laDescription.Size = new System.Drawing.Size(697, 37);
            this.laDescription.TabIndex = 10;
            this.laDescription.Text = "Add a Creative \"Quote\" to your Cover Slide...";
            // 
            // checkedListBoxControlQuotes
            // 
            this.checkedListBoxControlQuotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxControlQuotes.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.checkedListBoxControlQuotes.Appearance.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkedListBoxControlQuotes.Appearance.Options.UseBackColor = true;
            this.checkedListBoxControlQuotes.Appearance.Options.UseFont = true;
            this.checkedListBoxControlQuotes.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.checkedListBoxControlQuotes.CheckOnClick = true;
            this.checkedListBoxControlQuotes.ItemHeight = 25;
            this.checkedListBoxControlQuotes.Location = new System.Drawing.Point(12, 83);
            this.checkedListBoxControlQuotes.Name = "checkedListBoxControlQuotes";
            this.checkedListBoxControlQuotes.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.checkedListBoxControlQuotes.Size = new System.Drawing.Size(670, 311);
            this.checkedListBoxControlQuotes.TabIndex = 11;
            this.checkedListBoxControlQuotes.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.checkedListBoxControlQuotes_ItemCheck);
            this.checkedListBoxControlQuotes.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxControlQuotes_SelectedIndexChanged);
            this.checkedListBoxControlQuotes.DrawItem += new DevExpress.XtraEditors.ListBoxDrawItemEventHandler(this.checkedListBoxControlQuotes_DrawItem);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonX1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonX1.ForeColor = System.Drawing.Color.Black;
            this.buttonX1.Location = new System.Drawing.Point(528, 406);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(154, 40);
            this.buttonX1.TabIndex = 12;
            this.buttonX1.Text = "Return to Cover";
            // 
            // FormQuotes
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(694, 454);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.checkedListBoxControlQuotes);
            this.Controls.Add(this.laDescription);
            this.Controls.Add(this.laTitle);
            this.Name = "FormQuotes";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Quotes Library";
            this.Load += new System.EventHandler(this.FormQuotes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlQuotes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label laTitle;
        private System.Windows.Forms.Label laDescription;
        private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControlQuotes;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevComponents.DotNetBar.ButtonX buttonX1;
    }
}