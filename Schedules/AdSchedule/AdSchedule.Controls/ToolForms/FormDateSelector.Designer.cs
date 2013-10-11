namespace NewBizWiz.AdSchedule.Controls.ToolForms
{
	partial class FormDateSelector
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
			this.laTitle = new System.Windows.Forms.Label();
			this.pnTopButtons = new System.Windows.Forms.Panel();
			this.buttonXClearAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSelectAll = new DevComponents.DotNetBar.ButtonX();
			this.pnBottomButtons = new System.Windows.Forms.Panel();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.checkedListBoxControlDates = new DevExpress.XtraEditors.CheckedListBoxControl();
			this.pnTopButtons.SuspendLayout();
			this.pnBottomButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlDates)).BeginInit();
			this.SuspendLayout();
			// 
			// laTitle
			// 
			this.laTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.laTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.laTitle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.Location = new System.Drawing.Point(0, 0);
			this.laTitle.Name = "laTitle";
			this.laTitle.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.laTitle.Size = new System.Drawing.Size(357, 44);
			this.laTitle.TabIndex = 0;
			this.laTitle.Text = "label1";
			this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnTopButtons
			// 
			this.pnTopButtons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnTopButtons.Controls.Add(this.buttonXClearAll);
			this.pnTopButtons.Controls.Add(this.buttonXSelectAll);
			this.pnTopButtons.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTopButtons.Location = new System.Drawing.Point(0, 44);
			this.pnTopButtons.Name = "pnTopButtons";
			this.pnTopButtons.Size = new System.Drawing.Size(357, 52);
			this.pnTopButtons.TabIndex = 1;
			// 
			// buttonXClearAll
			// 
			this.buttonXClearAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXClearAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXClearAll.Location = new System.Drawing.Point(234, 10);
			this.buttonXClearAll.Name = "buttonXClearAll";
			this.buttonXClearAll.Size = new System.Drawing.Size(107, 33);
			this.buttonXClearAll.TabIndex = 9;
			this.buttonXClearAll.Text = "Clear All";
			this.buttonXClearAll.TextColor = System.Drawing.Color.Black;
			this.buttonXClearAll.Click += new System.EventHandler(this.buttonXClearAll_Click);
			// 
			// buttonXSelectAll
			// 
			this.buttonXSelectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSelectAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSelectAll.Location = new System.Drawing.Point(12, 10);
			this.buttonXSelectAll.Name = "buttonXSelectAll";
			this.buttonXSelectAll.Size = new System.Drawing.Size(107, 33);
			this.buttonXSelectAll.TabIndex = 8;
			this.buttonXSelectAll.Text = "Select All";
			this.buttonXSelectAll.TextColor = System.Drawing.Color.Black;
			this.buttonXSelectAll.Click += new System.EventHandler(this.buttonXSelectAll_Click);
			// 
			// pnBottomButtons
			// 
			this.pnBottomButtons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnBottomButtons.Controls.Add(this.buttonXCancel);
			this.pnBottomButtons.Controls.Add(this.buttonXOK);
			this.pnBottomButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnBottomButtons.Location = new System.Drawing.Point(0, 392);
			this.pnBottomButtons.Name = "pnBottomButtons";
			this.pnBottomButtons.Size = new System.Drawing.Size(357, 52);
			this.pnBottomButtons.TabIndex = 2;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(234, 6);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(107, 36);
			this.buttonXCancel.TabIndex = 8;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(98, 6);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(107, 36);
			this.buttonXOK.TabIndex = 7;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// checkedListBoxControlDates
			// 
			this.checkedListBoxControlDates.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.checkedListBoxControlDates.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkedListBoxControlDates.Appearance.Options.UseBackColor = true;
			this.checkedListBoxControlDates.Appearance.Options.UseFont = true;
			this.checkedListBoxControlDates.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
			this.checkedListBoxControlDates.CheckOnClick = true;
			this.checkedListBoxControlDates.Dock = System.Windows.Forms.DockStyle.Fill;
			this.checkedListBoxControlDates.ItemHeight = 25;
			this.checkedListBoxControlDates.Location = new System.Drawing.Point(0, 96);
			this.checkedListBoxControlDates.Name = "checkedListBoxControlDates";
			this.checkedListBoxControlDates.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.checkedListBoxControlDates.Size = new System.Drawing.Size(357, 296);
			this.checkedListBoxControlDates.TabIndex = 16;
			// 
			// FormDateSelector
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(357, 444);
			this.Controls.Add(this.checkedListBoxControlDates);
			this.Controls.Add(this.pnBottomButtons);
			this.Controls.Add(this.pnTopButtons);
			this.Controls.Add(this.laTitle);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FormDateSelector";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FormDateSelector";
			this.TopMost = true;
			this.pnTopButtons.ResumeLayout(false);
			this.pnBottomButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlDates)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnTopButtons;
		private System.Windows.Forms.Panel pnBottomButtons;
		private DevComponents.DotNetBar.ButtonX buttonXClearAll;
		private DevComponents.DotNetBar.ButtonX buttonXSelectAll;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.ButtonX buttonXOK;
		public DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControlDates;
		public System.Windows.Forms.Label laTitle;
	}
}