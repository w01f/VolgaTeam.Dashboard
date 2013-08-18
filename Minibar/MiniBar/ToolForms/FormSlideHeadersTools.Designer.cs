﻿namespace NewBizWiz.MiniBar.ToolForms
{
    partial class FormSlideHeadersTools
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
            this.buttonXAdd = new DevComponents.DotNetBar.ButtonX();
            this.buttonXReplace = new DevComponents.DotNetBar.ButtonX();
            this.buttonXDelete = new DevComponents.DotNetBar.ButtonX();
            this.buttonXClose = new DevComponents.DotNetBar.ButtonX();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // laTitle
            // 
            this.laTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laTitle.Location = new System.Drawing.Point(62, 3);
            this.laTitle.Name = "laTitle";
            this.laTitle.Size = new System.Drawing.Size(257, 50);
            this.laTitle.TabIndex = 1;
            this.laTitle.Text = "Do you want to add a Slide Header to a slide in this presentation?";
            this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonXAdd
            // 
            this.buttonXAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXAdd.Location = new System.Drawing.Point(30, 71);
            this.buttonXAdd.Name = "buttonXAdd";
            this.buttonXAdd.Size = new System.Drawing.Size(271, 41);
            this.buttonXAdd.TabIndex = 2;
            this.buttonXAdd.Text = "Add a Slide Header to the Active Slide";
            this.buttonXAdd.TextColor = System.Drawing.Color.Black;
            this.buttonXAdd.Click += new System.EventHandler(this.buttonXAdd_Click);
            // 
            // buttonXReplace
            // 
            this.buttonXReplace.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXReplace.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXReplace.Location = new System.Drawing.Point(30, 138);
            this.buttonXReplace.Name = "buttonXReplace";
            this.buttonXReplace.Size = new System.Drawing.Size(271, 41);
            this.buttonXReplace.TabIndex = 3;
            this.buttonXReplace.Text = "Replace all Slide Headers on All Slides with NEW Headers";
            this.buttonXReplace.TextColor = System.Drawing.Color.Black;
            this.buttonXReplace.Click += new System.EventHandler(this.buttonXReplace_Click);
            // 
            // buttonXDelete
            // 
            this.buttonXDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXDelete.Location = new System.Drawing.Point(30, 204);
            this.buttonXDelete.Name = "buttonXDelete";
            this.buttonXDelete.Size = new System.Drawing.Size(271, 41);
            this.buttonXDelete.TabIndex = 4;
            this.buttonXDelete.Text = "Delete All Slide Headers in this Presentation";
            this.buttonXDelete.TextColor = System.Drawing.Color.Black;
            this.buttonXDelete.Click += new System.EventHandler(this.buttonXDelete_Click);
            // 
            // buttonXClose
            // 
            this.buttonXClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXClose.Location = new System.Drawing.Point(30, 269);
            this.buttonXClose.Name = "buttonXClose";
            this.buttonXClose.Size = new System.Drawing.Size(271, 41);
            this.buttonXClose.TabIndex = 5;
            this.buttonXClose.Text = "CANCEL/CLOSE";
            this.buttonXClose.TextColor = System.Drawing.Color.Black;
            this.buttonXClose.Click += new System.EventHandler(this.buttonXClose_Click);
            // 
            // pbLogo
            // 
            this.pbLogo.Image = global::NewBizWiz.MiniBar.Properties.Resources.SlideHeader;
            this.pbLogo.Location = new System.Drawing.Point(3, 3);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(53, 50);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // FormSlideHeadersTools
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(331, 322);
            this.Controls.Add(this.buttonXClose);
            this.Controls.Add(this.buttonXDelete);
            this.Controls.Add(this.buttonXReplace);
            this.Controls.Add(this.buttonXAdd);
            this.Controls.Add(this.laTitle);
            this.Controls.Add(this.pbLogo);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSlideHeadersTools";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Slide Header";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label laTitle;
        private DevComponents.DotNetBar.ButtonX buttonXAdd;
        private DevComponents.DotNetBar.ButtonX buttonXReplace;
        private DevComponents.DotNetBar.ButtonX buttonXDelete;
        private DevComponents.DotNetBar.ButtonX buttonXClose;
    }
}