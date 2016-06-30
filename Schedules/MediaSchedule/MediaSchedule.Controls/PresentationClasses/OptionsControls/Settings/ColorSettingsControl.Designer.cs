namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Settings
{
	partial class ColorSettingsControl
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
			this.pnStyle = new System.Windows.Forms.Panel();
			this.outputColorSelector = new Asa.Common.GUI.OutputColors.OutputColorSelector();
			this.laColorsTitle = new System.Windows.Forms.Label();
			this.pnStyle.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnStyle
			// 
			this.pnStyle.BackColor = System.Drawing.Color.Transparent;
			this.pnStyle.Controls.Add(this.outputColorSelector);
			this.pnStyle.Controls.Add(this.laColorsTitle);
			this.pnStyle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnStyle.Location = new System.Drawing.Point(0, 0);
			this.pnStyle.Name = "pnStyle";
			this.pnStyle.Size = new System.Drawing.Size(326, 510);
			this.pnStyle.TabIndex = 1;
			// 
			// outputColorSelector
			// 
			this.outputColorSelector.BackColor = System.Drawing.Color.White;
			this.outputColorSelector.Dock = System.Windows.Forms.DockStyle.Fill;
			this.outputColorSelector.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.outputColorSelector.Location = new System.Drawing.Point(0, 29);
			this.outputColorSelector.Name = "outputColorSelector";
			this.outputColorSelector.Size = new System.Drawing.Size(326, 481);
			this.outputColorSelector.TabIndex = 50;
			// 
			// laColorsTitle
			// 
			this.laColorsTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.laColorsTitle.Font = new System.Drawing.Font("Arial", 9.75F);
			this.laColorsTitle.Location = new System.Drawing.Point(0, 0);
			this.laColorsTitle.Name = "laColorsTitle";
			this.laColorsTitle.Size = new System.Drawing.Size(326, 29);
			this.laColorsTitle.TabIndex = 48;
			this.laColorsTitle.Text = "Schedule Table Color:";
			this.laColorsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ColorSettingsControl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnStyle);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "ColorSettingsControl";
			this.Size = new System.Drawing.Size(326, 510);
			this.pnStyle.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnStyle;
		protected Common.GUI.OutputColors.OutputColorSelector outputColorSelector;
		private System.Windows.Forms.Label laColorsTitle;
	}
}
