namespace NewBizWizForm.TabRadioForms
{
    partial class RadioOverviewControl
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
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.laTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pbLogo
            // 
            this.pbLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbLogo.Image = global::NewBizWizForm.Properties.Resources.CableLogo;
            this.pbLogo.Location = new System.Drawing.Point(0, 203);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(422, 284);
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // laTitle
            // 
            this.laTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.laTitle.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laTitle.Location = new System.Drawing.Point(17, 18);
            this.laTitle.Name = "laTitle";
            this.laTitle.Size = new System.Drawing.Size(864, 159);
            this.laTitle.TabIndex = 1;
            this.laTitle.Text = "New & Exciting Radio Ad Sales\r\nTools are COMING SOON!";
            this.laTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.laTitle.UseMnemonic = false;
            // 
            // RadioOverviewControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.laTitle);
            this.Controls.Add(this.pbLogo);
            this.Name = "RadioOverviewControl";
            this.Size = new System.Drawing.Size(894, 487);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label laTitle;


    }
}
