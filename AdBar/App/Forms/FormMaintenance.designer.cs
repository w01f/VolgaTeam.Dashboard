namespace Asa.Bar.App.Forms
{
	partial class FormMaintenance
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
			this.components = new System.ComponentModel.Container();
			this.laTitle = new System.Windows.Forms.Label();
			this.panelMain = new System.Windows.Forms.Panel();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.panelMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// laTitle
			// 
			this.laTitle.BackColor = System.Drawing.Color.Transparent;
			this.laTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.laTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.ForeColor = System.Drawing.Color.White;
			this.laTitle.Location = new System.Drawing.Point(0, 0);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(375, 140);
			this.laTitle.TabIndex = 3;
			this.laTitle.Text = "Maintenance";
			this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panelMain
			// 
			this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(96)))), ((int)(((byte)(13)))));
			this.panelMain.Controls.Add(this.laTitle);
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(1, 1);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(375, 140);
			this.panelMain.TabIndex = 5;
			// 
			// timer
			// 
			this.timer.Interval = 1000;
			this.timer.Tick += new System.EventHandler(this.OnTimerTick);
			// 
			// FormMaintenance
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(377, 142);
			this.ControlBox = false;
			this.Controls.Add(this.panelMain);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMaintenance";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			this.panelMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		public System.Windows.Forms.Label laTitle;
		private System.Windows.Forms.Panel panelMain;
		private System.Windows.Forms.Timer timer;
	}
}

