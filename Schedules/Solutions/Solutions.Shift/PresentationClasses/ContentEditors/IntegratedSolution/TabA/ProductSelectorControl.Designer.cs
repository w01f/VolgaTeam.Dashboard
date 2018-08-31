namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution.TabA
{
	partial class ProductSelectorControl
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
			this.xtraScrollableControl = new DevExpress.XtraEditors.XtraScrollableControl();
			this.SuspendLayout();
			// 
			// xtraScrollableControl
			// 
			this.xtraScrollableControl.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.xtraScrollableControl.Appearance.Options.UseBackColor = true;
			this.xtraScrollableControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraScrollableControl.Location = new System.Drawing.Point(0, 0);
			this.xtraScrollableControl.Name = "xtraScrollableControl";
			this.xtraScrollableControl.Size = new System.Drawing.Size(399, 475);
			this.xtraScrollableControl.TabIndex = 46;
			// 
			// ItemSelectorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.xtraScrollableControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "ProductSelectorControl";
			this.Size = new System.Drawing.Size(399, 475);
			this.Resize += new System.EventHandler(this.OnResize);
			this.ResumeLayout(false);

		}

		#endregion

		protected DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl;

	}
}
