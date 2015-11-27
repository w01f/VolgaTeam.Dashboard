namespace Asa.MediaSchedule.Controls.PresentationClasses.Summary
{
	partial class SummaryTypeSettingsControl
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
			this.buttonXTypeStrategy = new DevComponents.DotNetBar.ButtonX();
			this.buttonXTypeCustom = new DevComponents.DotNetBar.ButtonX();
			this.buttonXTypeProduct = new DevComponents.DotNetBar.ButtonX();
			this.checkEditTableOutput = new DevExpress.XtraEditors.CheckEdit();
			this.laProductSlideCount = new System.Windows.Forms.Label();
			this.laCustomSlideCount = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.checkEditTableOutput.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonXTypeStrategy
			// 
			this.buttonXTypeStrategy.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXTypeStrategy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXTypeStrategy.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXTypeStrategy.Location = new System.Drawing.Point(24, 199);
			this.buttonXTypeStrategy.Name = "buttonXTypeStrategy";
			this.buttonXTypeStrategy.Size = new System.Drawing.Size(249, 27);
			this.buttonXTypeStrategy.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXTypeStrategy.TabIndex = 59;
			this.buttonXTypeStrategy.Text = "Logo Summary";
			this.buttonXTypeStrategy.TextColor = System.Drawing.Color.Black;
			this.buttonXTypeStrategy.CheckedChanged += new System.EventHandler(this.OnSettingChanged);
			this.buttonXTypeStrategy.Click += new System.EventHandler(this.OnOutputSelectorClick);
			// 
			// buttonXTypeCustom
			// 
			this.buttonXTypeCustom.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXTypeCustom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXTypeCustom.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXTypeCustom.Location = new System.Drawing.Point(24, 15);
			this.buttonXTypeCustom.Name = "buttonXTypeCustom";
			this.buttonXTypeCustom.Size = new System.Drawing.Size(249, 27);
			this.buttonXTypeCustom.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXTypeCustom.TabIndex = 58;
			this.buttonXTypeCustom.Text = "General Summary";
			this.buttonXTypeCustom.TextColor = System.Drawing.Color.Black;
			this.buttonXTypeCustom.CheckedChanged += new System.EventHandler(this.OnSettingChanged);
			this.buttonXTypeCustom.Click += new System.EventHandler(this.OnOutputSelectorClick);
			// 
			// buttonXTypeProduct
			// 
			this.buttonXTypeProduct.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXTypeProduct.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXTypeProduct.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXTypeProduct.Location = new System.Drawing.Point(24, 107);
			this.buttonXTypeProduct.Name = "buttonXTypeProduct";
			this.buttonXTypeProduct.Size = new System.Drawing.Size(249, 27);
			this.buttonXTypeProduct.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXTypeProduct.TabIndex = 57;
			this.buttonXTypeProduct.Text = "Product Summary";
			this.buttonXTypeProduct.TextColor = System.Drawing.Color.Black;
			this.buttonXTypeProduct.CheckedChanged += new System.EventHandler(this.OnSettingChanged);
			this.buttonXTypeProduct.Click += new System.EventHandler(this.OnOutputSelectorClick);
			// 
			// checkEditTableOutput
			// 
			this.checkEditTableOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkEditTableOutput.Location = new System.Drawing.Point(24, 418);
			this.checkEditTableOutput.Name = "checkEditTableOutput";
			this.checkEditTableOutput.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditTableOutput.Properties.Appearance.Options.UseFont = true;
			this.checkEditTableOutput.Properties.AutoWidth = true;
			this.checkEditTableOutput.Properties.Caption = "Use PowerPoint Slide Tables";
			this.checkEditTableOutput.Size = new System.Drawing.Size(190, 19);
			this.checkEditTableOutput.TabIndex = 122;
			this.checkEditTableOutput.CheckedChanged += new System.EventHandler(this.OnTableOutputCheckedChanged);
			// 
			// laProductSlideCount
			// 
			this.laProductSlideCount.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laProductSlideCount.Location = new System.Drawing.Point(21, 137);
			this.laProductSlideCount.Name = "laProductSlideCount";
			this.laProductSlideCount.Size = new System.Drawing.Size(249, 19);
			this.laProductSlideCount.TabIndex = 123;
			this.laProductSlideCount.Text = "Estimated Slide Count:";
			this.laProductSlideCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// laCustomSlideCount
			// 
			this.laCustomSlideCount.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laCustomSlideCount.Location = new System.Drawing.Point(21, 45);
			this.laCustomSlideCount.Name = "laCustomSlideCount";
			this.laCustomSlideCount.Size = new System.Drawing.Size(249, 22);
			this.laCustomSlideCount.TabIndex = 124;
			this.laCustomSlideCount.Text = "Estimated Slide Count:";
			this.laCustomSlideCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// SummaryTypeSettingsControl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.laCustomSlideCount);
			this.Controls.Add(this.laProductSlideCount);
			this.Controls.Add(this.checkEditTableOutput);
			this.Controls.Add(this.buttonXTypeStrategy);
			this.Controls.Add(this.buttonXTypeCustom);
			this.Controls.Add(this.buttonXTypeProduct);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "SummaryTypeSettingsControl";
			this.Size = new System.Drawing.Size(296, 457);
			((System.ComponentModel.ISupportInitialize)(this.checkEditTableOutput.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.ButtonX buttonXTypeStrategy;
		private DevComponents.DotNetBar.ButtonX buttonXTypeCustom;
		private DevComponents.DotNetBar.ButtonX buttonXTypeProduct;
		protected DevExpress.XtraEditors.CheckEdit checkEditTableOutput;
		protected System.Windows.Forms.Label laProductSlideCount;
		protected System.Windows.Forms.Label laCustomSlideCount;


	}
}
