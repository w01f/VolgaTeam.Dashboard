namespace AdBar.Plugins.Slides
{
    partial class ContainerControl
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
            this.ribbonBar = new DevComponents.DotNetBar.RibbonBar();
            this.buttonItemOutput = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemSlideMaster = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // ribbonBar
            // 
            this.ribbonBar.AutoOverflowEnabled = true;
            // 
            // 
            // 
            this.ribbonBar.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBar.ContainerControlProcessDialogKey = true;
            this.ribbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemOutput,
            this.buttonItemSlideMaster});
            this.ribbonBar.ItemSpacing = 15;
            this.ribbonBar.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBar.Location = new System.Drawing.Point(0, 0);
            this.ribbonBar.Name = "ribbonBar";
            this.ribbonBar.ResizeItemsToFit = false;
            this.ribbonBar.Size = new System.Drawing.Size(192, 150);
            this.ribbonBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBar.TabIndex = 0;
            this.ribbonBar.Text = "Slides";
            // 
            // 
            // 
            this.ribbonBar.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBar.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBar.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
            // 
            // buttonItemOutput
            // 
            this.buttonItemOutput.Image = global::AdBar.Plugins.Slides.Properties.Resources.Output;
            this.buttonItemOutput.Name = "buttonItemOutput";
            this.buttonItemOutput.SubItemsExpandWidth = 14;
            this.buttonItemOutput.Text = "buttonItem1";
            this.buttonItemOutput.Tooltip = "Add this slide";
            this.buttonItemOutput.Click += new System.EventHandler(this.buttonItemOutput_Click);
            // 
            // buttonItemSlideMaster
            // 
            this.buttonItemSlideMaster.Name = "buttonItemSlideMaster";
            this.buttonItemSlideMaster.SubItemsExpandWidth = 14;
            this.buttonItemSlideMaster.Text = "buttonItem2";
            this.buttonItemSlideMaster.Tooltip = "View Slide Gallery";
            this.buttonItemSlideMaster.Click += new System.EventHandler(this.buttonItemSlideMaster_Click);
            // 
            // ContainerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ribbonBar);
            this.Name = "ContainerControl";
            this.Size = new System.Drawing.Size(199, 150);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem buttonItemOutput;
        private DevComponents.DotNetBar.ButtonItem buttonItemSlideMaster;
		internal DevComponents.DotNetBar.RibbonBar ribbonBar;
    }
}
