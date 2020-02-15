namespace Asa.Common.GUI.ToolForms
{
    partial class FormProgress
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
            this.circularProgress = new DevComponents.DotNetBar.Controls.CircularProgress();
            this.panelEx = new DevComponents.DotNetBar.PanelEx();
            this.pnText = new System.Windows.Forms.Panel();
            this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.labelControlDescription = new DevExpress.XtraEditors.LabelControl();
            this.panelEx.SuspendLayout();
            this.pnText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            this.SuspendLayout();
            // 
            // circularProgress
            // 
            this.circularProgress.AnimationSpeed = 50;
            this.circularProgress.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.circularProgress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.circularProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.circularProgress.Enabled = false;
            this.circularProgress.Location = new System.Drawing.Point(0, 60);
            this.circularProgress.Name = "circularProgress";
            this.circularProgress.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
            this.circularProgress.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.circularProgress.ProgressTextFormat = "";
            this.circularProgress.Size = new System.Drawing.Size(367, 36);
            this.circularProgress.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.circularProgress.TabIndex = 3;
            // 
            // panelEx
            // 
            this.panelEx.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx.Controls.Add(this.pnText);
            this.panelEx.Controls.Add(this.circularProgress);
            this.panelEx.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx.Location = new System.Drawing.Point(2, 2);
            this.panelEx.Name = "panelEx";
            this.panelEx.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panelEx.Size = new System.Drawing.Size(367, 106);
            this.panelEx.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx.Style.BackColor1.Color = System.Drawing.Color.White;
            this.panelEx.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx.Style.GradientAngle = 90;
            this.panelEx.TabIndex = 4;
            // 
            // pnText
            // 
            this.pnText.Controls.Add(this.labelControlTitle);
            this.pnText.Controls.Add(this.labelControlDescription);
            this.pnText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnText.ForeColor = System.Drawing.Color.Black;
            this.pnText.Location = new System.Drawing.Point(0, 0);
            this.pnText.Name = "pnText";
            this.pnText.Size = new System.Drawing.Size(367, 60);
            this.pnText.TabIndex = 4;
            // 
            // labelControlTitle
            // 
            this.labelControlTitle.AllowHtmlString = true;
            this.labelControlTitle.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControlTitle.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControlTitle.Appearance.Options.UseBackColor = true;
            this.labelControlTitle.Appearance.Options.UseForeColor = true;
            this.labelControlTitle.Appearance.Options.UseTextOptions = true;
            this.labelControlTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlTitle.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
            this.labelControlTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControlTitle.AppearanceDisabled.Options.UseTextOptions = true;
            this.labelControlTitle.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlTitle.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
            this.labelControlTitle.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControlTitle.AppearanceHovered.Options.UseTextOptions = true;
            this.labelControlTitle.AppearanceHovered.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlTitle.AppearanceHovered.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
            this.labelControlTitle.AppearanceHovered.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControlTitle.AppearancePressed.Options.UseTextOptions = true;
            this.labelControlTitle.AppearancePressed.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlTitle.AppearancePressed.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
            this.labelControlTitle.AppearancePressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControlTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControlTitle.Location = new System.Drawing.Point(0, 0);
            this.labelControlTitle.Name = "labelControlTitle";
            this.labelControlTitle.Size = new System.Drawing.Size(367, 46);
            this.labelControlTitle.StyleController = this.styleController;
            this.labelControlTitle.TabIndex = 4;
            this.labelControlTitle.Text = "<b>Loading data...</b>";
            this.labelControlTitle.UseMnemonic = false;
            this.labelControlTitle.UseWaitCursor = true;
            // 
            // styleController
            // 
            this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.styleController.Appearance.Options.UseFont = true;
            this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDisabled.Options.UseFont = true;
            this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDropDown.Options.UseFont = true;
            this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
            this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceFocused.Options.UseFont = true;
            this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceReadOnly.Options.UseFont = true;
            // 
            // labelControlDescription
            // 
            this.labelControlDescription.AllowHtmlString = true;
            this.labelControlDescription.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControlDescription.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControlDescription.Appearance.Options.UseBackColor = true;
            this.labelControlDescription.Appearance.Options.UseForeColor = true;
            this.labelControlDescription.Appearance.Options.UseTextOptions = true;
            this.labelControlDescription.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDescription.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
            this.labelControlDescription.AppearanceDisabled.Options.UseTextOptions = true;
            this.labelControlDescription.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDescription.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
            this.labelControlDescription.AppearanceHovered.Options.UseTextOptions = true;
            this.labelControlDescription.AppearanceHovered.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDescription.AppearanceHovered.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
            this.labelControlDescription.AppearancePressed.Options.UseTextOptions = true;
            this.labelControlDescription.AppearancePressed.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDescription.AppearancePressed.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
            this.labelControlDescription.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControlDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelControlDescription.Location = new System.Drawing.Point(0, 46);
            this.labelControlDescription.Name = "labelControlDescription";
            this.labelControlDescription.Size = new System.Drawing.Size(367, 14);
            this.labelControlDescription.StyleController = this.styleController;
            this.labelControlDescription.TabIndex = 5;
            this.labelControlDescription.Text = "<size=-1.5>Loading data...</size>";
            this.labelControlDescription.UseMnemonic = false;
            this.labelControlDescription.UseWaitCursor = true;
            // 
            // FormProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(371, 110);
            this.ControlBox = false;
            this.Controls.Add(this.panelEx);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormProgress";
            this.Opacity = 0.85D;
            this.Padding = new System.Windows.Forms.Padding(2);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProgressForm";
            this.Shown += new System.EventHandler(this.FormProgress_Shown);
            this.panelEx.ResumeLayout(false);
            this.pnText.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.Controls.CircularProgress circularProgress;
        private DevComponents.DotNetBar.PanelEx panelEx;
		private System.Windows.Forms.Panel pnText;
        private DevExpress.XtraEditors.StyleController styleController;
        public DevExpress.XtraEditors.LabelControl labelControlTitle;
        public DevExpress.XtraEditors.LabelControl labelControlDescription;
    }
}