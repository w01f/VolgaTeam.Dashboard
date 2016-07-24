namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Output
{
    partial class FormConfigureOutput
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
			this.buttonXContinue = new DevComponents.DotNetBar.ButtonX();
			this.buttonXClose = new DevComponents.DotNetBar.ButtonX();
			this.treeView = new Asa.Common.GUI.Common.NoDoubleClickTreeView();
			this.buttonXSelectNone = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSelectAll = new DevComponents.DotNetBar.ButtonX();
			this.SuspendLayout();
			// 
			// buttonXContinue
			// 
			this.buttonXContinue.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXContinue.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXContinue.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXContinue.Location = new System.Drawing.Point(14, 370);
			this.buttonXContinue.Name = "buttonXContinue";
			this.buttonXContinue.Size = new System.Drawing.Size(148, 43);
			this.buttonXContinue.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXContinue.TabIndex = 9;
			this.buttonXContinue.Text = "Continue";
			this.buttonXContinue.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXClose
			// 
			this.buttonXClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXClose.Location = new System.Drawing.Point(188, 370);
			this.buttonXClose.Name = "buttonXClose";
			this.buttonXClose.Size = new System.Drawing.Size(148, 43);
			this.buttonXClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXClose.TabIndex = 11;
			this.buttonXClose.Text = "Cancel";
			this.buttonXClose.TextColor = System.Drawing.Color.Black;
			// 
			// treeView
			// 
			this.treeView.BackColor = System.Drawing.Color.White;
			this.treeView.CausesValidation = false;
			this.treeView.CheckBoxes = true;
			this.treeView.ItemHeight = 40;
			this.treeView.Location = new System.Drawing.Point(14, 53);
			this.treeView.Name = "treeView";
			this.treeView.ShowLines = false;
			this.treeView.ShowPlusMinus = false;
			this.treeView.ShowRootLines = false;
			this.treeView.Size = new System.Drawing.Size(322, 311);
			this.treeView.TabIndex = 12;
			this.treeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.OnTreeViewAfterCheck);
			this.treeView.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.OnTreeViewBeforeCollapse);
			this.treeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.OnTreeViewBeforeSelect);
			this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OnTreeViewNodeMouseClick);
			this.treeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnTreeViewMouseDown);
			// 
			// buttonXSelectNone
			// 
			this.buttonXSelectNone.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSelectNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXSelectNone.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSelectNone.Location = new System.Drawing.Point(240, 12);
			this.buttonXSelectNone.Name = "buttonXSelectNone";
			this.buttonXSelectNone.Size = new System.Drawing.Size(96, 35);
			this.buttonXSelectNone.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSelectNone.TabIndex = 21;
			this.buttonXSelectNone.Text = "Clear";
			this.buttonXSelectNone.TextColor = System.Drawing.Color.Black;
			this.buttonXSelectNone.Click += new System.EventHandler(this.OnSelectNoneClick);
			// 
			// buttonXSelectAll
			// 
			this.buttonXSelectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXSelectAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSelectAll.Location = new System.Drawing.Point(14, 12);
			this.buttonXSelectAll.Name = "buttonXSelectAll";
			this.buttonXSelectAll.Size = new System.Drawing.Size(96, 35);
			this.buttonXSelectAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSelectAll.TabIndex = 19;
			this.buttonXSelectAll.Text = "All";
			this.buttonXSelectAll.TextColor = System.Drawing.Color.Black;
			this.buttonXSelectAll.Click += new System.EventHandler(this.OnSelectAllClick);
			// 
			// FormConfigureOutput
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(350, 420);
			this.Controls.Add(this.buttonXSelectNone);
			this.Controls.Add(this.buttonXSelectAll);
			this.Controls.Add(this.treeView);
			this.Controls.Add(this.buttonXClose);
			this.Controls.Add(this.buttonXContinue);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormConfigureOutput";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Slide Output Options";
			this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXClose;
		public DevComponents.DotNetBar.ButtonX buttonXContinue;
		private Common.GUI.Common.NoDoubleClickTreeView treeView;
		public DevComponents.DotNetBar.ButtonX buttonXSelectNone;
		public DevComponents.DotNetBar.ButtonX buttonXSelectAll;
	}
}