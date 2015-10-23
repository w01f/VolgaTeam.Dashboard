namespace Asa.AdSchedule.Controls.ToolForms
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
			this.laAdCampaign = new System.Windows.Forms.Label();
			this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
			this.pbAdCampaign = new System.Windows.Forms.PictureBox();
			this.pbAdvertiserProfile = new System.Windows.Forms.PictureBox();
			this.pbWarning = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pbAdCampaign)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbAdvertiserProfile)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbWarning)).BeginInit();
			this.SuspendLayout();
			// 
			// laTitle
			// 
			this.laTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laTitle.BackColor = System.Drawing.Color.White;
			this.laTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.ForeColor = System.Drawing.Color.Black;
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
			this.laAdvertiserProfile.BackColor = System.Drawing.Color.White;
			this.laAdvertiserProfile.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAdvertiserProfile.ForeColor = System.Drawing.Color.Black;
			this.laAdvertiserProfile.Location = new System.Drawing.Point(57, 86);
			this.laAdvertiserProfile.Name = "laAdvertiserProfile";
			this.laAdvertiserProfile.Size = new System.Drawing.Size(285, 40);
			this.laAdvertiserProfile.TabIndex = 3;
			this.laAdvertiserProfile.Text = "Basic Info";
			this.laAdvertiserProfile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// laAdCampaign
			// 
			this.laAdCampaign.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laAdCampaign.BackColor = System.Drawing.Color.White;
			this.laAdCampaign.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAdCampaign.ForeColor = System.Drawing.Color.Black;
			this.laAdCampaign.Location = new System.Drawing.Point(57, 148);
			this.laAdCampaign.Name = "laAdCampaign";
			this.laAdCampaign.Size = new System.Drawing.Size(285, 40);
			this.laAdCampaign.TabIndex = 7;
			this.laAdCampaign.Text = "Campaign Dates";
			this.laAdCampaign.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// buttonX1
			// 
			this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonX1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonX1.Location = new System.Drawing.Point(115, 202);
			this.buttonX1.Name = "buttonX1";
			this.buttonX1.Size = new System.Drawing.Size(126, 36);
			this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonX1.TabIndex = 8;
			this.buttonX1.Text = "OK";
			this.buttonX1.TextColor = System.Drawing.Color.Black;
			// 
			// pbAdCampaign
			// 
			this.pbAdCampaign.BackColor = System.Drawing.Color.White;
			this.pbAdCampaign.ForeColor = System.Drawing.Color.Black;
			this.pbAdCampaign.Image = global::Asa.AdSchedule.Controls.Properties.Resources.Calendar;
			this.pbAdCampaign.Location = new System.Drawing.Point(10, 148);
			this.pbAdCampaign.Name = "pbAdCampaign";
			this.pbAdCampaign.Size = new System.Drawing.Size(40, 40);
			this.pbAdCampaign.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbAdCampaign.TabIndex = 6;
			this.pbAdCampaign.TabStop = false;
			// 
			// pbAdvertiserProfile
			// 
			this.pbAdvertiserProfile.BackColor = System.Drawing.Color.White;
			this.pbAdvertiserProfile.ForeColor = System.Drawing.Color.Black;
			this.pbAdvertiserProfile.Image = global::Asa.AdSchedule.Controls.Properties.Resources.BasicInfo;
			this.pbAdvertiserProfile.Location = new System.Drawing.Point(10, 86);
			this.pbAdvertiserProfile.Name = "pbAdvertiserProfile";
			this.pbAdvertiserProfile.Size = new System.Drawing.Size(40, 40);
			this.pbAdvertiserProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbAdvertiserProfile.TabIndex = 2;
			this.pbAdvertiserProfile.TabStop = false;
			// 
			// pbWarning
			// 
			this.pbWarning.BackColor = System.Drawing.Color.White;
			this.pbWarning.ForeColor = System.Drawing.Color.Black;
			this.pbWarning.Image = global::Asa.AdSchedule.Controls.Properties.Resources.Warning;
			this.pbWarning.Location = new System.Drawing.Point(2, 12);
			this.pbWarning.Name = "pbWarning";
			this.pbWarning.Size = new System.Drawing.Size(48, 48);
			this.pbWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbWarning.TabIndex = 0;
			this.pbWarning.TabStop = false;
			// 
			// FormAddPublicationWarning
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(356, 251);
			this.Controls.Add(this.buttonX1);
			this.Controls.Add(this.laAdCampaign);
			this.Controls.Add(this.pbAdCampaign);
			this.Controls.Add(this.laAdvertiserProfile);
			this.Controls.Add(this.pbAdvertiserProfile);
			this.Controls.Add(this.laTitle);
			this.Controls.Add(this.pbWarning);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAddPublicationWarning";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Not So Fast…";
			((System.ComponentModel.ISupportInitialize)(this.pbAdCampaign)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbAdvertiserProfile)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbWarning)).EndInit();
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