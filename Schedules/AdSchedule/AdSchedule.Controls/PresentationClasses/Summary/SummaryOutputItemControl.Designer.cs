namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.Summary
{
    partial class SummaryOutputItemControl
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
			this.laDetails = new System.Windows.Forms.Label();
			this.ckDetails = new System.Windows.Forms.CheckBox();
			this.ckItem = new System.Windows.Forms.CheckBox();
			this.ckMonthly = new System.Windows.Forms.CheckBox();
			this.ckTotal = new System.Windows.Forms.CheckBox();
			this.pnHeader = new System.Windows.Forms.Panel();
			this.pnMonthly = new System.Windows.Forms.Panel();
			this.pnTotal = new System.Windows.Forms.Panel();
			this.pnDetails = new System.Windows.Forms.Panel();
			this.pnHeader.SuspendLayout();
			this.pnMonthly.SuspendLayout();
			this.pnTotal.SuspendLayout();
			this.pnDetails.SuspendLayout();
			this.SuspendLayout();
			// 
			// laDetails
			// 
			this.laDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laDetails.Font = new System.Drawing.Font("Arial", 9.75F);
			this.laDetails.Location = new System.Drawing.Point(49, 7);
			this.laDetails.Name = "laDetails";
			this.laDetails.Size = new System.Drawing.Size(574, 32);
			this.laDetails.TabIndex = 34;
			this.laDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.laDetails.UseMnemonic = false;
			// 
			// ckDetails
			// 
			this.ckDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.ckDetails.Checked = true;
			this.ckDetails.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckDetails.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckDetails.Location = new System.Drawing.Point(33, 8);
			this.ckDetails.Name = "ckDetails";
			this.ckDetails.Size = new System.Drawing.Size(20, 32);
			this.ckDetails.TabIndex = 33;
			this.ckDetails.UseMnemonic = false;
			this.ckDetails.UseVisualStyleBackColor = true;
			// 
			// ckItem
			// 
			this.ckItem.Checked = true;
			this.ckItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckItem.Dock = System.Windows.Forms.DockStyle.Left;
			this.ckItem.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckItem.Location = new System.Drawing.Point(0, 0);
			this.ckItem.Name = "ckItem";
			this.ckItem.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.ckItem.Size = new System.Drawing.Size(333, 52);
			this.ckItem.TabIndex = 31;
			this.ckItem.UseVisualStyleBackColor = true;
			// 
			// ckMonthly
			// 
			this.ckMonthly.Checked = true;
			this.ckMonthly.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckMonthly.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ckMonthly.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckMonthly.Location = new System.Drawing.Point(0, 0);
			this.ckMonthly.Name = "ckMonthly";
			this.ckMonthly.Size = new System.Drawing.Size(137, 52);
			this.ckMonthly.TabIndex = 32;
			this.ckMonthly.Text = "$0.00";
			this.ckMonthly.UseVisualStyleBackColor = true;
			// 
			// ckTotal
			// 
			this.ckTotal.Checked = true;
			this.ckTotal.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckTotal.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ckTotal.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckTotal.Location = new System.Drawing.Point(0, 0);
			this.ckTotal.Name = "ckTotal";
			this.ckTotal.Size = new System.Drawing.Size(144, 52);
			this.ckTotal.TabIndex = 33;
			this.ckTotal.Text = "$0.00";
			this.ckTotal.UseVisualStyleBackColor = true;
			// 
			// pnHeader
			// 
			this.pnHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.pnHeader.Controls.Add(this.pnMonthly);
			this.pnHeader.Controls.Add(this.pnTotal);
			this.pnHeader.Controls.Add(this.ckItem);
			this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnHeader.Location = new System.Drawing.Point(0, 0);
			this.pnHeader.Name = "pnHeader";
			this.pnHeader.Size = new System.Drawing.Size(631, 52);
			this.pnHeader.TabIndex = 43;
			// 
			// pnMonthly
			// 
			this.pnMonthly.Controls.Add(this.ckMonthly);
			this.pnMonthly.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnMonthly.Location = new System.Drawing.Point(350, 0);
			this.pnMonthly.Name = "pnMonthly";
			this.pnMonthly.Size = new System.Drawing.Size(137, 52);
			this.pnMonthly.TabIndex = 35;
			// 
			// pnTotal
			// 
			this.pnTotal.Controls.Add(this.ckTotal);
			this.pnTotal.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnTotal.Location = new System.Drawing.Point(487, 0);
			this.pnTotal.Name = "pnTotal";
			this.pnTotal.Size = new System.Drawing.Size(144, 52);
			this.pnTotal.TabIndex = 34;
			// 
			// pnDetails
			// 
			this.pnDetails.Controls.Add(this.laDetails);
			this.pnDetails.Controls.Add(this.ckDetails);
			this.pnDetails.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnDetails.Location = new System.Drawing.Point(0, 52);
			this.pnDetails.Name = "pnDetails";
			this.pnDetails.Padding = new System.Windows.Forms.Padding(5, 0, 5, 10);
			this.pnDetails.Size = new System.Drawing.Size(631, 45);
			this.pnDetails.TabIndex = 44;
			// 
			// SummaryOutputItemControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.pnDetails);
			this.Controls.Add(this.pnHeader);
			this.Name = "SummaryOutputItemControl";
			this.Size = new System.Drawing.Size(631, 226);
			this.pnHeader.ResumeLayout(false);
			this.pnMonthly.ResumeLayout(false);
			this.pnTotal.ResumeLayout(false);
			this.pnDetails.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.CheckBox ckItem;
        private System.Windows.Forms.CheckBox ckDetails;
        public System.Windows.Forms.Panel pnMonthly;
        public System.Windows.Forms.Panel pnTotal;
        private System.Windows.Forms.CheckBox ckMonthly;
        private System.Windows.Forms.CheckBox ckTotal;
        public System.Windows.Forms.Panel pnDetails;
        public System.Windows.Forms.Panel pnHeader;
        private System.Windows.Forms.Label laDetails;
    }
}
