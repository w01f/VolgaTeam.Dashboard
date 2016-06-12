namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings
{
	partial class SectionDigitalSettingsControl
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
			this.labelControlComingSoon = new DevExpress.XtraEditors.LabelControl();
			this.SuspendLayout();
			// 
			// labelControlComingSoon
			// 
			this.labelControlComingSoon.AllowHtmlString = true;
			this.labelControlComingSoon.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.labelControlComingSoon.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.labelControlComingSoon.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlComingSoon.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlComingSoon.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelControlComingSoon.Location = new System.Drawing.Point(0, 0);
			this.labelControlComingSoon.Name = "labelControlComingSoon";
			this.labelControlComingSoon.Size = new System.Drawing.Size(150, 150);
			this.labelControlComingSoon.TabIndex = 109;
			this.labelControlComingSoon.Text = "<size=+6><color=gray>New Digital Sales tools coming soon!</color></size>";
			// 
			// SectionDigitalSettingsControl
			// 
			this.Controls.Add(this.labelControlComingSoon);
			this.Name = "SectionDigitalSettingsControl";
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.LabelControl labelControlComingSoon;
	}
}
