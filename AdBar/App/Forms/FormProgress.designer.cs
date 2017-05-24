namespace Asa.Bar.App.Forms
{
    partial class FormProgress
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
			this.circularProgress = new DevComponents.DotNetBar.Controls.CircularProgress();
			this.panelEx = new DevComponents.DotNetBar.PanelEx();
			this.pnText = new System.Windows.Forms.Panel();
			this.laDetails = new System.Windows.Forms.Label();
			this.panelEx.SuspendLayout();
			this.pnText.SuspendLayout();
			this.SuspendLayout();
			// 
			// laTitle
			// 
			this.laTitle.BackColor = System.Drawing.Color.Transparent;
			this.laTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.laTitle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.ForeColor = System.Drawing.Color.Black;
			this.laTitle.Location = new System.Drawing.Point(0, 0);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(350, 33);
			this.laTitle.TabIndex = 2;
			this.laTitle.Text = "Loading data...";
			this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.laTitle.UseMnemonic = false;
			this.laTitle.UseWaitCursor = true;
			// 
			// circularProgress
			// 
			this.circularProgress.AnimationSpeed = 50;
			this.circularProgress.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.circularProgress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.circularProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.circularProgress.Enabled = false;
			this.circularProgress.Location = new System.Drawing.Point(0, 52);
			this.circularProgress.Name = "circularProgress";
			this.circularProgress.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
			this.circularProgress.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.circularProgress.ProgressTextFormat = "";
			this.circularProgress.Size = new System.Drawing.Size(350, 36);
			this.circularProgress.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.circularProgress.TabIndex = 3;
			// 
			// panelEx
			// 
			this.panelEx.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx.Controls.Add(this.pnText);
			this.panelEx.Controls.Add(this.circularProgress);
			this.panelEx.DisabledBackColor = System.Drawing.Color.Empty;
			this.panelEx.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx.Location = new System.Drawing.Point(2, 2);
			this.panelEx.Name = "panelEx";
			this.panelEx.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
			this.panelEx.Size = new System.Drawing.Size(350, 98);
			this.panelEx.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx.Style.BackColor1.Color = System.Drawing.Color.White;
			this.panelEx.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx.Style.GradientAngle = 90;
			this.panelEx.TabIndex = 4;
			// 
			// pnText
			// 
			this.pnText.Controls.Add(this.laTitle);
			this.pnText.Controls.Add(this.laDetails);
			this.pnText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnText.ForeColor = System.Drawing.Color.Black;
			this.pnText.Location = new System.Drawing.Point(0, 0);
			this.pnText.Name = "pnText";
			this.pnText.Size = new System.Drawing.Size(350, 52);
			this.pnText.TabIndex = 4;
			// 
			// laDetails
			// 
			this.laDetails.BackColor = System.Drawing.Color.Transparent;
			this.laDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.laDetails.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laDetails.ForeColor = System.Drawing.Color.Black;
			this.laDetails.Location = new System.Drawing.Point(0, 33);
			this.laDetails.Name = "laDetails";
			this.laDetails.Size = new System.Drawing.Size(350, 19);
			this.laDetails.TabIndex = 3;
			this.laDetails.Text = "Loading data...";
			this.laDetails.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.laDetails.UseMnemonic = false;
			this.laDetails.UseWaitCursor = true;
			// 
			// FormProgress
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(354, 102);
			this.ControlBox = false;
			this.Controls.Add(this.panelEx);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FormProgress";
			this.Opacity = 0.85D;
			this.Padding = new System.Windows.Forms.Padding(2);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ProgressForm";
			this.Shown += new System.EventHandler(this.FormProgress_Shown);
			this.panelEx.ResumeLayout(false);
			this.pnText.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label laTitle;
        private DevComponents.DotNetBar.Controls.CircularProgress circularProgress;
        private DevComponents.DotNetBar.PanelEx panelEx;
		private System.Windows.Forms.Panel pnText;
		public System.Windows.Forms.Label laDetails;
    }
}