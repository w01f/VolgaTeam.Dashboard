namespace NewBizWiz.AdSchedule.Controls.ToolForms
{
    partial class FormAddPublicationWarning
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
			this.laAdvertiserProfile = new System.Windows.Forms.Label();
			this.pbAdvertiserProfile = new System.Windows.Forms.PictureBox();
			this.pbWarning = new System.Windows.Forms.PictureBox();
			this.laAdCampaign = new System.Windows.Forms.Label();
			this.pbAdCampaign = new System.Windows.Forms.PictureBox();
			this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.pbAdvertiserProfile)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbWarning)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbAdCampaign)).BeginInit();
			this.SuspendLayout();
			// 
			// laTitle
			// 
			this.laTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.Location = new System.Drawing.Point(56, 12);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(296, 61);
			this.laTitle.TabIndex = 1;
			this.laTitle.Text = "Before you add a Publication, you need to complete the following information:";
			// 
			// laAdvertiserProfile
			// 
			this.laAdvertiserProfile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laAdvertiserProfile.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAdvertiserProfile.Location = new System.Drawing.Point(57, 86);
			this.laAdvertiserProfile.Name = "laAdvertiserProfile";
			this.laAdvertiserProfile.Size = new System.Drawing.Size(285, 40);
			this.laAdvertiserProfile.TabIndex = 3;
			this.laAdvertiserProfile.Text = "Advertiser Profile";
			this.laAdvertiserProfile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pbAdvertiserProfile
			// 
			this.pbAdvertiserProfile.Image = global::NewBizWiz.AdSchedule.Controls.Properties.Resources.Advertiser;
			this.pbAdvertiserProfile.Location = new System.Drawing.Point(10, 86);
			this.pbAdvertiserProfile.Name = "pbAdvertiserProfile";
			this.pbAdvertiserProfile.Size = new System.Drawing.Size(40, 40);
			this.pbAdvertiserProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbAdvertiserProfile.TabIndex = 2;
			this.pbAdvertiserProfile.TabStop = false;
			// 
			// pbWarning
			// 
			this.pbWarning.Image = global::NewBizWiz.AdSchedule.Controls.Properties.Resources.Warning;
			this.pbWarning.Location = new System.Drawing.Point(2, 12);
			this.pbWarning.Name = "pbWarning";
			this.pbWarning.Size = new System.Drawing.Size(48, 48);
			this.pbWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbWarning.TabIndex = 0;
			this.pbWarning.TabStop = false;
			// 
			// laAdCampaign
			// 
			this.laAdCampaign.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laAdCampaign.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAdCampaign.Location = new System.Drawing.Point(57, 148);
			this.laAdCampaign.Name = "laAdCampaign";
			this.laAdCampaign.Size = new System.Drawing.Size(285, 40);
			this.laAdCampaign.TabIndex = 7;
			this.laAdCampaign.Text = "Campaign Dates";
			this.laAdCampaign.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pbAdCampaign
			// 
			this.pbAdCampaign.Image = global::NewBizWiz.AdSchedule.Controls.Properties.Resources.Calendar;
			this.pbAdCampaign.Location = new System.Drawing.Point(10, 148);
			this.pbAdCampaign.Name = "pbAdCampaign";
			this.pbAdCampaign.Size = new System.Drawing.Size(40, 40);
			this.pbAdCampaign.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbAdCampaign.TabIndex = 6;
			this.pbAdCampaign.TabStop = false;
			// 
			// buttonX1
			// 
			this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonX1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonX1.Location = new System.Drawing.Point(115, 202);
			this.buttonX1.Name = "buttonX1";
			this.buttonX1.Size = new System.Drawing.Size(126, 36);
			this.buttonX1.TabIndex = 8;
			this.buttonX1.Text = "OK";
			this.buttonX1.TextColor = System.Drawing.Color.Black;
			// 
			// FormAddPublicationWarning
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(356, 251);
			this.Controls.Add(this.buttonX1);
			this.Controls.Add(this.laAdCampaign);
			this.Controls.Add(this.pbAdCampaign);
			this.Controls.Add(this.laAdvertiserProfile);
			this.Controls.Add(this.pbAdvertiserProfile);
			this.Controls.Add(this.laTitle);
			this.Controls.Add(this.pbWarning);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAddPublicationWarning";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Not So Fast…";
			((System.ComponentModel.ISupportInitialize)(this.pbAdvertiserProfile)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbWarning)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbAdCampaign)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbWarning;
        private System.Windows.Forms.Label laTitle;
        private System.Windows.Forms.Label laAdvertiserProfile;
		private System.Windows.Forms.PictureBox pbAdvertiserProfile;
        private System.Windows.Forms.Label laAdCampaign;
        private System.Windows.Forms.PictureBox pbAdCampaign;
        private DevComponents.DotNetBar.ButtonX buttonX1;

    }
}