namespace Asa.Media.Controls.PresentationClasses.Summary
{
	partial class StrategyImageControl
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
			this.favoriteImagesControl = new Asa.Common.GUI.FavoriteImages.FavoriteImagesControl();
			this.SuspendLayout();
			// 
			// favoriteImagesControl
			// 
			this.favoriteImagesControl.BackColor = System.Drawing.Color.White;
			this.favoriteImagesControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.favoriteImagesControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.favoriteImagesControl.ImageTooltip = null;
			this.favoriteImagesControl.Location = new System.Drawing.Point(0, 0);
			this.favoriteImagesControl.Name = "favoriteImagesControl";
			this.favoriteImagesControl.Size = new System.Drawing.Size(296, 457);
			this.favoriteImagesControl.TabIndex = 1;
			// 
			// StrategyImageControl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.favoriteImagesControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "StrategyImageControl";
			this.Size = new System.Drawing.Size(296, 457);
			this.ResumeLayout(false);

		}

		#endregion

		private Common.GUI.FavoriteImages.FavoriteImagesControl favoriteImagesControl;
	}
}
