namespace Asa.Common.GUI.OutputColors
{
	partial class OutputColorSelector
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
			this.xtraScrollableControlColors = new DevExpress.XtraEditors.XtraScrollableControl();
			this.SuspendLayout();
			// 
			// xtraScrollableControlColors
			// 
			this.xtraScrollableControlColors.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.xtraScrollableControlColors.Appearance.Options.UseBackColor = true;
			this.xtraScrollableControlColors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraScrollableControlColors.Location = new System.Drawing.Point(0, 0);
			this.xtraScrollableControlColors.Name = "xtraScrollableControlColors";
			this.xtraScrollableControlColors.Size = new System.Drawing.Size(399, 475);
			this.xtraScrollableControlColors.TabIndex = 46;
			// 
			// OutputColorSelector
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.xtraScrollableControlColors);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "OutputColorSelector";
			this.Size = new System.Drawing.Size(399, 475);
			this.Resize += new System.EventHandler(this.OutputColorSelector_Resize);
			this.ResumeLayout(false);

		}

		#endregion

		protected DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControlColors;

	}
}
