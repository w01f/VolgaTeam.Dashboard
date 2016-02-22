namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings
{
	partial class DigitalSettingsControl
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
			this.digitalInfoControl = new Asa.Media.Controls.PresentationClasses.Digital.DigitalInfoControl();
			this.SuspendLayout();
			// 
			// digitalInfoControl
			// 
			this.digitalInfoControl.BackColor = System.Drawing.Color.White;
			this.digitalInfoControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.digitalInfoControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.digitalInfoControl.Location = new System.Drawing.Point(0, 0);
			this.digitalInfoControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.digitalInfoControl.Name = "digitalInfoControl";
			this.digitalInfoControl.Size = new System.Drawing.Size(367, 510);
			this.digitalInfoControl.TabIndex = 1;
			// 
			// DigitalSettingsControl
			// 
			this.Controls.Add(digitalInfoControl);
			this.BackColor = System.Drawing.Color.White;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "DigitalSettingsControl";
			this.Size = new System.Drawing.Size(367, 560);
			this.ResumeLayout(false);

		}

		#endregion

		protected Digital.DigitalInfoControl digitalInfoControl;
	}
}
