using System;
using System.Collections.Generic;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Enums;
using Asa.Common.Core.Enums;
using DevExpress.XtraLayout.Utils;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Cover
{
	class CoverControl : MultiTabControl
	{
		public override SlideType SlideType => SlideType.StarAppCover;

		public CoverControl(BaseStarAppContainer slideContainer, StarChildTabsContainer tabInfo) : base(slideContainer, tabInfo) { }

		public override void InitControls()
		{
			base.InitControls();
			layoutControlItemAddAsPageOne.Visibility = LayoutVisibility.Always;
		}

		protected override IList<IChildTabPageContainer> GetChildTabPages()
		{
			var tabPages = new List<IChildTabPageContainer>();
			foreach (var tabInfo in TabContainerInfo.ChildTabs)
			{
				switch (tabInfo.TabType)
				{
					case StarChildTabType.A:
						tabPages.Add(new ChildTabPageContainerControl<CoverTabAControl>(this, tabInfo));
						break;
					case StarChildTabType.K:
					case StarChildTabType.L:
					case StarChildTabType.M:
					case StarChildTabType.N:
					case StarChildTabType.O:
						tabPages.Add(new ChildTabPageContainerControl<SingleSlidesTabControl>(this, tabInfo));
						break;
					case StarChildTabType.U:
					case StarChildTabType.V:
					case StarChildTabType.W:
						tabPages.Add(new ChildTabPageContainerControl<MultiSlidesTabControl>(this, tabInfo));
						break;
					case StarChildTabType.X:
					case StarChildTabType.Y:
					case StarChildTabType.Z:
						tabPages.Add(new ChildTabPageContainerControl<TilesTabControl>(this, tabInfo));
						break;
					default:
						throw new ArgumentOutOfRangeException("Star tab type is not defined");
				}
			}
			return tabPages;
		}

		public override void LoadData()
		{
			base.LoadData();

			_allowToSave = false;

			checkEditAddAsPageOne.Checked = SlideContainer.EditedContent.CoverState.TabA.AddAsPageOne;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			base.ApplyChanges();

			if (!_dataChanged) return;
			SlideContainer.EditedContent.CoverState.TabA.AddAsPageOne = checkEditAddAsPageOne.Checked;
		}

		private void InitializeComponent()
		{
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
			this.layoutControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditLogoRight.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSlideHeader)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditAddAsPageOne.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAddAsPageOne)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl
			// 
			this.layoutControl.Appearance.Control.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.layoutControl.Appearance.Control.Options.UseFont = true;
			this.layoutControl.Appearance.ControlDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlDisabled.Options.UseFont = true;
			this.layoutControl.Appearance.ControlDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlDropDown.Options.UseFont = true;
			this.layoutControl.Appearance.ControlDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlDropDownHeader.Options.UseFont = true;
			this.layoutControl.Appearance.ControlFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlFocused.Options.UseFont = true;
			this.layoutControl.Appearance.ControlReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlReadOnly.Options.UseFont = true;
			this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(802, 383, 250, 350);
			this.layoutControl.OptionsFocus.ActivateSelectedControlOnGotFocus = false;
			this.layoutControl.OptionsFocus.AllowFocusGroups = false;
			this.layoutControl.OptionsFocus.AllowFocusReadonlyEditors = false;
			this.layoutControl.OptionsFocus.AllowFocusTabbedGroups = false;
			this.layoutControl.Controls.SetChildIndex(this.checkEditAddAsPageOne, 0);
			this.layoutControl.Controls.SetChildIndex(this.comboBoxEditSlideHeader, 0);
			// 
			// comboBoxEditSlideHeader
			// 
			this.comboBoxEditSlideHeader.Location = new System.Drawing.Point(170, 29);
			this.comboBoxEditSlideHeader.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.comboBoxEditSlideHeader.Properties.Appearance.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSlideHeader.Properties.AppearanceDisabled.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSlideHeader.Properties.AppearanceDropDown.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Properties.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSlideHeader.Properties.AppearanceFocused.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSlideHeader.Properties.AppearanceReadOnly.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Size = new System.Drawing.Size(283, 22);
			// 
			// pictureEditLogoRight
			// 
			// 
			// layoutControlGroupRoot
			// 
			this.layoutControlGroupRoot.AppearanceGroup.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceGroup.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.Header.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderActive.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true;
			// 
			// layoutControlItemSlideHeader
			// 
			this.layoutControlItemSlideHeader.Location = new System.Drawing.Point(128, 0);
			this.layoutControlItemSlideHeader.OptionsTableLayoutItem.ColumnIndex = 2;
			this.layoutControlItemSlideHeader.Size = new System.Drawing.Size(287, 80);
			// 
			// checkEditAddAsPageOne
			// 
			this.checkEditAddAsPageOne.Location = new System.Drawing.Point(508, 30);
			this.checkEditAddAsPageOne.Size = new System.Drawing.Size(167, 20);
			// 
			// layoutControlItemAddAsPageOne
			// 
			this.layoutControlItemAddAsPageOne.Location = new System.Drawing.Point(445, 0);
			this.layoutControlItemAddAsPageOne.OptionsTableLayoutItem.ColumnIndex = 4;
			this.layoutControlItemAddAsPageOne.Size = new System.Drawing.Size(192, 80);
			// 
			// CoverControl
			// 
			this.Name = "CoverControl";
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
			this.layoutControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditLogoRight.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSlideHeader)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditAddAsPageOne.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAddAsPageOne)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
