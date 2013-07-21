namespace NewBizWiz.Dashboard.TabiPadForms
{
    partial class iPadControl
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
            this.laTitle = new System.Windows.Forms.Label();
            this.laHint = new System.Windows.Forms.Label();
            this.pbSlideShark = new System.Windows.Forms.PictureBox();
            this.pbSlideRocket = new System.Windows.Forms.PictureBox();
            this.pbDropBox = new System.Windows.Forms.PictureBox();
            this.pbGoogleDocs = new System.Windows.Forms.PictureBox();
            this.pbEmail = new System.Windows.Forms.PictureBox();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbSlideShark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSlideRocket)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDropBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGoogleDocs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // laTitle
            // 
            this.laTitle.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold);
            this.laTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.laTitle.Location = new System.Drawing.Point(11, 13);
            this.laTitle.Name = "laTitle";
            this.laTitle.Size = new System.Drawing.Size(483, 184);
            this.laTitle.TabIndex = 12;
            this.laTitle.Text = "You can send your PowerPoint presentation to the iPad as an email attachment, or " +
                "with one of these cloud services…";
            // 
            // laHint
            // 
            this.laHint.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.laHint.Location = new System.Drawing.Point(13, 169);
            this.laHint.Name = "laHint";
            this.laHint.Size = new System.Drawing.Size(481, 28);
            this.laHint.TabIndex = 43;
            this.laHint.Text = "*You may need to set up an account to use one of these services";
            this.laHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbSlideShark
            // 
            this.pbSlideShark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSlideShark.Image = global::NewBizWiz.Dashboard.Properties.Resources.SlideSharkLogo;
            this.pbSlideShark.Location = new System.Drawing.Point(612, 411);
            this.pbSlideShark.Name = "pbSlideShark";
            this.pbSlideShark.Size = new System.Drawing.Size(272, 92);
            this.pbSlideShark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSlideShark.TabIndex = 48;
            this.pbSlideShark.TabStop = false;
            this.pbSlideShark.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbSlideShark.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // pbSlideRocket
            // 
            this.pbSlideRocket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSlideRocket.Image = global::NewBizWiz.Dashboard.Properties.Resources.SlideRocketLogo;
            this.pbSlideRocket.Location = new System.Drawing.Point(612, 309);
            this.pbSlideRocket.Name = "pbSlideRocket";
            this.pbSlideRocket.Size = new System.Drawing.Size(272, 92);
            this.pbSlideRocket.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSlideRocket.TabIndex = 47;
            this.pbSlideRocket.TabStop = false;
            this.pbSlideRocket.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbSlideRocket.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // pbDropBox
            // 
            this.pbDropBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDropBox.Image = global::NewBizWiz.Dashboard.Properties.Resources.DropboxLogo;
            this.pbDropBox.Location = new System.Drawing.Point(612, 208);
            this.pbDropBox.Name = "pbDropBox";
            this.pbDropBox.Size = new System.Drawing.Size(272, 92);
            this.pbDropBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDropBox.TabIndex = 46;
            this.pbDropBox.TabStop = false;
            this.pbDropBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbDropBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // pbGoogleDocs
            // 
            this.pbGoogleDocs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbGoogleDocs.Image = global::NewBizWiz.Dashboard.Properties.Resources.GoogleDocsLogo;
            this.pbGoogleDocs.Location = new System.Drawing.Point(612, 107);
            this.pbGoogleDocs.Name = "pbGoogleDocs";
            this.pbGoogleDocs.Size = new System.Drawing.Size(272, 92);
            this.pbGoogleDocs.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbGoogleDocs.TabIndex = 45;
            this.pbGoogleDocs.TabStop = false;
            this.pbGoogleDocs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbGoogleDocs.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // pbEmail
            // 
            this.pbEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbEmail.Image = global::NewBizWiz.Dashboard.Properties.Resources.EmailiPadLogo;
            this.pbEmail.Location = new System.Drawing.Point(612, 6);
            this.pbEmail.Name = "pbEmail";
            this.pbEmail.Size = new System.Drawing.Size(272, 92);
            this.pbEmail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbEmail.TabIndex = 44;
            this.pbEmail.TabStop = false;
            this.pbEmail.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbEmail.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // pbLogo
            // 
            this.pbLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbLogo.Image = global::NewBizWiz.Dashboard.Properties.Resources.iPadLogo;
            this.pbLogo.Location = new System.Drawing.Point(0, 277);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(231, 210);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 11;
            this.pbLogo.TabStop = false;
            // 
            // iPadControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pbSlideShark);
            this.Controls.Add(this.pbSlideRocket);
            this.Controls.Add(this.pbDropBox);
            this.Controls.Add(this.pbGoogleDocs);
            this.Controls.Add(this.pbEmail);
            this.Controls.Add(this.laHint);
            this.Controls.Add(this.laTitle);
            this.Controls.Add(this.pbLogo);
            this.Name = "iPadControl";
            this.Size = new System.Drawing.Size(894, 487);
            ((System.ComponentModel.ISupportInitialize)(this.pbSlideShark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSlideRocket)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDropBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGoogleDocs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label laTitle;
        private System.Windows.Forms.Label laHint;
        private System.Windows.Forms.PictureBox pbEmail;
        private System.Windows.Forms.PictureBox pbGoogleDocs;
        private System.Windows.Forms.PictureBox pbDropBox;
        private System.Windows.Forms.PictureBox pbSlideRocket;
        private System.Windows.Forms.PictureBox pbSlideShark;

    }
}
