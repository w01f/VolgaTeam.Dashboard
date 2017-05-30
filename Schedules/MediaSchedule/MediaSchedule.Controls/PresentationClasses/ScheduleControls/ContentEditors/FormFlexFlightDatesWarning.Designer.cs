namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	partial class FormFlexFlightDatesWarning
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFlexFlightDatesWarning));
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.labelControlHeader = new DevExpress.XtraEditors.LabelControl();
			this.labelControlDescription = new DevExpress.XtraEditors.LabelControl();
			this.SuspendLayout();
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(371, 271);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(91, 36);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 0;
			this.buttonXOK.Text = "OK";
			// 
			// labelControlHeader
			// 
			this.labelControlHeader.AllowHtmlString = true;
			this.labelControlHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlHeader.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlHeader.Appearance.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlHeader.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlHeader.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlHeader.Location = new System.Drawing.Point(12, 12);
			this.labelControlHeader.Name = "labelControlHeader";
			this.labelControlHeader.Size = new System.Drawing.Size(450, 39);
			this.labelControlHeader.TabIndex = 1;
			this.labelControlHeader.Text = "<color=\"red\">Your schedule has PARTIAL WEEKS:</color>";
			// 
			// labelControlDescription
			// 
			this.labelControlDescription.AllowHtmlString = true;
			this.labelControlDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlDescription.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlDescription.Appearance.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlDescription.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlDescription.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.labelControlDescription.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlDescription.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlDescription.Location = new System.Drawing.Point(12, 57);
			this.labelControlDescription.Name = "labelControlDescription";
			this.labelControlDescription.Size = new System.Drawing.Size(450, 203);
			this.labelControlDescription.TabIndex = 2;
			this.labelControlDescription.Text = resources.GetString("labelControlDescription.Text");
			// 
			// FormFlexFlightDatesWarning
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(474, 314);
			this.Controls.Add(this.labelControlDescription);
			this.Controls.Add(this.labelControlHeader);
			this.Controls.Add(this.buttonXOK);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormFlexFlightDatesWarning";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "IMPORTANT INFO";
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.ButtonX buttonXOK;
		private DevExpress.XtraEditors.LabelControl labelControlHeader;
		private DevExpress.XtraEditors.LabelControl labelControlDescription;
	}
}