namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	partial class SectionDigitalControl
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
			this.pbNoPrograms = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pbNoPrograms)).BeginInit();
			this.SuspendLayout();
			// 
			// pbNoPrograms
			// 
			this.pbNoPrograms.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbNoPrograms.Image = global::Asa.Media.Controls.Properties.Resources.SectionNoDigitalItems;
			this.pbNoPrograms.Location = new System.Drawing.Point(0, 0);
			this.pbNoPrograms.Name = "pbNoPrograms";
			this.pbNoPrograms.Size = new System.Drawing.Size(454, 378);
			this.pbNoPrograms.TabIndex = 2;
			this.pbNoPrograms.TabStop = false;
			// 
			// SectionDigitalControl
			// 
			this.Controls.Add(this.pbNoPrograms);
			this.Size = new System.Drawing.Size(454, 378);
			((System.ComponentModel.ISupportInitialize)(this.pbNoPrograms)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pbNoPrograms;
	}
}
