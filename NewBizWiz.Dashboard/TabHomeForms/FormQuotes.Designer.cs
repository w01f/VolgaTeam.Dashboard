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
			this.laTitle = new System.Windows.Forms.Label();
			this.laDescription = new System.Windows.Forms.Label();
			this.checkedListBoxControlQuotes = new DevExpress.XtraEditors.CheckedListBoxControl();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlQuotes)).BeginInit();
			this.SuspendLayout();
			// 
			// laTitle
			// 
			this.laTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laTitle.BackColor = System.Drawing.Color.White;
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
			this.laDescription.BackColor = System.Drawing.Color.White;
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
			this.checkedListBoxControlQuotes.Appearance.BackColor = System.Drawing.Color.White;
			this.checkedListBoxControlQuotes.Appearance.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkedListBoxControlQuotes.Appearance.ForeColor = System.Drawing.Color.Black;
			this.checkedListBoxControlQuotes.Appearance.Options.UseBackColor = true;
			this.checkedListBoxControlQuotes.Appearance.Options.UseFont = true;
			this.checkedListBoxControlQuotes.Appearance.Options.UseForeColor = true;
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
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXOK.Location = new System.Drawing.Point(528, 406);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(154, 40);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 12;
			this.buttonXOK.Text = "Return to Cover";
			// 
			// FormQuotes
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(694, 454);
			this.Controls.Add(this.buttonXOK);
			this.Controls.Add(this.checkedListBoxControlQuotes);
			this.Controls.Add(this.laDescription);
			this.Controls.Add(this.laTitle);
			this.DoubleBuffered = true;
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
        private DevComponents.DotNetBar.ButtonX buttonXOK;
    }
}