namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	public partial class BaseShiftControl
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
			this.labelFocusFake = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// labelFocusFake
			// 
			this.labelFocusFake.AutoSize = true;
			this.labelFocusFake.Location = new System.Drawing.Point(-100, -100);
			this.labelFocusFake.Name = "labelFocusFake";
			this.labelFocusFake.Size = new System.Drawing.Size(35, 13);
			this.labelFocusFake.TabIndex = 0;
			this.labelFocusFake.Text = "label1";
			// 
			// ShiftControl
			// 
			this.Controls.Add(this.labelFocusFake);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "BaseShiftControl";
			this.Size = new System.Drawing.Size(997, 512);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.Label labelFocusFake;
	}
}
