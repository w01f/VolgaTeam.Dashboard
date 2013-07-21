namespace CommandCentral.TabMainDashboard
{
    partial class UsersControl
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
            this.pnMain = new System.Windows.Forms.Panel();
            this.laTotalUserLists = new System.Windows.Forms.Label();
            this.pnButtonContainer = new System.Windows.Forms.Panel();
            this.laTitle = new System.Windows.Forms.Label();
            this.pnMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnMain
            // 
            this.pnMain.Controls.Add(this.laTotalUserLists);
            this.pnMain.Controls.Add(this.pnButtonContainer);
            this.pnMain.Controls.Add(this.laTitle);
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pnMain.Location = new System.Drawing.Point(0, 0);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(669, 463);
            this.pnMain.TabIndex = 0;
            // 
            // laTotalUserLists
            // 
            this.laTotalUserLists.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.laTotalUserLists.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laTotalUserLists.Location = new System.Drawing.Point(451, 0);
            this.laTotalUserLists.Name = "laTotalUserLists";
            this.laTotalUserLists.Size = new System.Drawing.Size(215, 77);
            this.laTotalUserLists.TabIndex = 5;
            this.laTotalUserLists.Text = "Total User List: {0}";
            this.laTotalUserLists.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnButtonContainer
            // 
            this.pnButtonContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnButtonContainer.AutoScroll = true;
            this.pnButtonContainer.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pnButtonContainer.Location = new System.Drawing.Point(0, 80);
            this.pnButtonContainer.Name = "pnButtonContainer";
            this.pnButtonContainer.Size = new System.Drawing.Size(669, 383);
            this.pnButtonContainer.TabIndex = 4;
            // 
            // laTitle
            // 
            this.laTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.laTitle.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laTitle.Location = new System.Drawing.Point(3, 0);
            this.laTitle.Name = "laTitle";
            this.laTitle.Size = new System.Drawing.Size(165, 77);
            this.laTitle.TabIndex = 1;
            this.laTitle.Text = "Users";
            this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UsersControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pnMain);
            this.Name = "UsersControl";
            this.Size = new System.Drawing.Size(669, 463);
            this.pnMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.Label laTitle;
        private System.Windows.Forms.Panel pnButtonContainer;
        private System.Windows.Forms.Label laTotalUserLists;
    }
}
