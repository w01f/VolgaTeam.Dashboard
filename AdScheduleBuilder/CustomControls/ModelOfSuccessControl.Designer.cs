namespace AdScheduleBuilder.CustomControls
{
    partial class ModelOfSuccessControl
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
            this.components = new System.ComponentModel.Container();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.laLink = new System.Windows.Forms.LinkLabel();
            this.laIndex = new System.Windows.Forms.Label();
            this.laDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // laLink
            // 
            this.laLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.laLink.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laLink.Location = new System.Drawing.Point(41, 5);
            this.laLink.Name = "laLink";
            this.laLink.Size = new System.Drawing.Size(535, 28);
            this.laLink.TabIndex = 0;
            this.laLink.TabStop = true;
            this.laLink.Text = "Link";
            this.laLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.laLink_LinkClicked);
            this.laLink.MouseMove += new System.Windows.Forms.MouseEventHandler(this.laDescription_MouseMove);
            // 
            // laIndex
            // 
            this.laIndex.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laIndex.Location = new System.Drawing.Point(6, 6);
            this.laIndex.Name = "laIndex";
            this.laIndex.Size = new System.Drawing.Size(34, 28);
            this.laIndex.TabIndex = 1;
            this.laIndex.Text = "1.";
            this.laIndex.MouseMove += new System.Windows.Forms.MouseEventHandler(this.laDescription_MouseMove);
            // 
            // laDescription
            // 
            this.laDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.laDescription.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laDescription.Location = new System.Drawing.Point(41, 33);
            this.laDescription.Name = "laDescription";
            this.laDescription.Size = new System.Drawing.Size(535, 49);
            this.laDescription.TabIndex = 2;
            this.laDescription.Text = "Description";
            this.laDescription.MouseMove += new System.Windows.Forms.MouseEventHandler(this.laDescription_MouseMove);
            // 
            // ModelOfSuccessControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.laDescription);
            this.Controls.Add(this.laIndex);
            this.Controls.Add(this.laLink);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "ModelOfSuccessControl";
            this.Size = new System.Drawing.Size(584, 91);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.laDescription_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private System.Windows.Forms.LinkLabel laLink;
        private System.Windows.Forms.Label laIndex;
        private System.Windows.Forms.Label laDescription;
    }
}
