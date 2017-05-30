namespace Asa.Reset
{
	partial class FormWarning
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
			this.pbLogo = new System.Windows.Forms.PictureBox();
			this.buttonXReset = new DevComponents.DotNetBar.ButtonX();
			this.pnBackground = new System.Windows.Forms.Panel();
			this.laDescription = new System.Windows.Forms.Label();
			this.laTitle = new System.Windows.Forms.Label();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			this.pnBackground.SuspendLayout();
			this.SuspendLayout();
			// 
			// styleManager
			// 
			this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2013;
			this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
			// 
			// pbLogo
			// 
			this.pbLogo.BackColor = System.Drawing.Color.Transparent;
			this.pbLogo.ForeColor = System.Drawing.Color.Black;
			this.pbLogo.Image = global::Asa.Reset.Properties.Resources.Warning;
			this.pbLogo.Location = new System.Drawing.Point(12, 25);
			this.pbLogo.Name = "pbLogo";
			this.pbLogo.Size = new System.Drawing.Size(115, 111);
			this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbLogo.TabIndex = 0;
			this.pbLogo.TabStop = false;
			// 
			// buttonXReset
			// 
			this.buttonXReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXReset.BackColor = System.Drawing.Color.White;
			this.buttonXReset.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXReset.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXReset.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXReset.Location = new System.Drawing.Point(121, 168);
			this.buttonXReset.Name = "buttonXReset";
			this.buttonXReset.Size = new System.Drawing.Size(142, 42);
			this.buttonXReset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXReset.TabIndex = 1;
			this.buttonXReset.Text = "RESET";
			this.buttonXReset.TextColor = System.Drawing.Color.Red;
			// 
			// pnBackground
			// 
			this.pnBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
			this.pnBackground.Controls.Add(this.laDescription);
			this.pnBackground.Controls.Add(this.laTitle);
			this.pnBackground.Controls.Add(this.buttonXCancel);
			this.pnBackground.Controls.Add(this.pbLogo);
			this.pnBackground.Controls.Add(this.buttonXReset);
			this.pnBackground.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnBackground.ForeColor = System.Drawing.Color.Black;
			this.pnBackground.Location = new System.Drawing.Point(0, 0);
			this.pnBackground.Name = "pnBackground";
			this.pnBackground.Size = new System.Drawing.Size(570, 222);
			this.pnBackground.TabIndex = 2;
			// 
			// laDescription
			// 
			this.laDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laDescription.BackColor = System.Drawing.Color.White;
			this.laDescription.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laDescription.ForeColor = System.Drawing.Color.Black;
			this.laDescription.Location = new System.Drawing.Point(139, 84);
			this.laDescription.Name = "laDescription";
			this.laDescription.Size = new System.Drawing.Size(419, 55);
			this.laDescription.TabIndex = 4;
			this.laDescription.Text = "This process will remove all of your adSALESapps settings from this device.";
			// 
			// laTitle
			// 
			this.laTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laTitle.BackColor = System.Drawing.Color.White;
			this.laTitle.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.ForeColor = System.Drawing.Color.Black;
			this.laTitle.Location = new System.Drawing.Point(133, 25);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(425, 38);
			this.laTitle.TabIndex = 3;
			this.laTitle.Text = "ARE YOU SURE?";
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.BackColor = System.Drawing.Color.White;
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXCancel.Location = new System.Drawing.Point(308, 168);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(142, 42);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 2;
			this.buttonXCancel.Text = "CANCEL";
			this.buttonXCancel.TextColor = System.Drawing.Color.Red;
			// 
			// FormWarning
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(570, 222);
			this.ControlBox = false;
			this.Controls.Add(this.pnBackground);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormWarning";
			this.RenderFormIcon = false;
			this.RenderFormText = false;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Shown += new System.EventHandler(this.OnFormShown);
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			this.pnBackground.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.StyleManager styleManager;
		private System.Windows.Forms.PictureBox pbLogo;
		private DevComponents.DotNetBar.ButtonX buttonXReset;
		private System.Windows.Forms.Panel pnBackground;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private System.Windows.Forms.Label laDescription;
		private System.Windows.Forms.Label laTitle;
	}
}

