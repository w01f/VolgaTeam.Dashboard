namespace Asa.Media.Controls.PresentationClasses.Digital.ContentEditors
{
	partial class DigitalListEditorControl
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
			this.digitalProductListControl = new Asa.Online.Controls.PresentationClasses.Products.DigitalProductListControl();
			this.SuspendLayout();
			// 
			// digitalProductListControl
			// 
			this.digitalProductListControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.digitalProductListControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.digitalProductListControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.digitalProductListControl.Location = new System.Drawing.Point(0, 0);
			this.digitalProductListControl.Name = "digitalProductListControl";
			this.digitalProductListControl.Size = new System.Drawing.Size(950, 457);
			this.digitalProductListControl.TabIndex = 0;
			// 
			// DigitalListEditorControl
			// 
			this.Controls.Add(this.digitalProductListControl);
			this.Name = "DigitalListEditorControl";
			this.Size = new System.Drawing.Size(950, 457);
			this.ResumeLayout(false);

		}

		#endregion

		private Online.Controls.PresentationClasses.Products.DigitalProductListControl digitalProductListControl;
	}
}
