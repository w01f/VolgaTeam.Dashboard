namespace Asa.Common.GUI.RetractableBar
{
	partial class RetractableBarRight
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
			((System.ComponentModel.ISupportInitialize)(this.pnClosed)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnOpened)).BeginInit();
			this.SuspendLayout();
			// 
			// pnClosed
			// 
			this.pnClosed.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.pnClosed.Appearance.Options.UseBackColor = true;
			this.pnClosed.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnClosed.Location = new System.Drawing.Point(301, 0);
			this.pnClosed.Size = new System.Drawing.Size(58, 388);
			// 
			// pnOpened
			// 
			this.pnOpened.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.pnOpened.Appearance.Options.UseBackColor = true;
			this.pnOpened.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnOpened.Location = new System.Drawing.Point(0, 0);
			this.pnOpened.Size = new System.Drawing.Size(301, 388);
			// 
			// pnContent
			// 
			this.pnContent.Size = new System.Drawing.Size(293, 340);
			// 
			// pnHeaderContent
			// 
			this.pnHeaderContent.Size = new System.Drawing.Size(246, 40);
			// 
			// simpleButtonExpand
			// 
			this.simpleButtonExpand.Image = global::Asa.Common.GUI.Properties.Resources.RetractableBarCollapse;
			this.simpleButtonExpand.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			// 
			// simpleButtonCollapse
			// 
			this.simpleButtonCollapse.Image = global::Asa.Common.GUI.Properties.Resources.RetractableBarExpand;
			this.simpleButtonCollapse.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			// 
			// RetractableBarRight
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "RetractableBarRight";
			((System.ComponentModel.ISupportInitialize)(this.pnClosed)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnOpened)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
	}
}
