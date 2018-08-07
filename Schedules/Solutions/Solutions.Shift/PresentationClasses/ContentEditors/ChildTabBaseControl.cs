using System;
using System.IO;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Common.InteropClasses;
using Asa.Solutions.Common.PresentationClasses.Output;
using DevExpress.XtraTab;
using ResourceManager = Asa.Common.Core.Configuration.ResourceManager;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	public partial class ChildTabBaseControl : UserControl
	{
		protected bool _allowToSave;
		protected bool _dataChanged;

		protected IChildTabPageContainer TabPageContainer { get; }
		protected BaseShiftContainer SlideContainer => TabPageContainer.ParentControl.SlideContainer;

		public ShiftChildTabInfo TabInfo { get; }

		public ChildTabBaseControl()
		{
			InitializeComponent();
		}

		public ChildTabBaseControl(IChildTabPageContainer tabPageContainer, ShiftChildTabInfo tabInfo) : this()
		{
			TabPageContainer = tabPageContainer;
			TabInfo = tabInfo;

			if (TabInfo.CommonEditorConfiguration.FontSize.HasValue)
			{
				var fontSizeDelte = TabInfo.CommonEditorConfiguration.FontSize.Value - TextEditorConfiguration.DefaultFontSize;
				layoutControl.Appearance.Control.FontSizeDelta = fontSizeDelte;
				layoutControl.Appearance.ControlFocused.FontSizeDelta = fontSizeDelte;
				layoutControl.Appearance.ControlDropDown.FontSizeDelta = fontSizeDelte;
				layoutControl.Appearance.ControlDropDownHeader.FontSizeDelta = fontSizeDelte;
				layoutControl.Appearance.ControlDisabled.FontSizeDelta = fontSizeDelte;
				layoutControl.Appearance.ControlReadOnly.FontSizeDelta = fontSizeDelte;
			}
			if (!TabInfo.CommonEditorConfiguration.BackColor.IsEmpty)
			{
				layoutControl.Appearance.Control.BackColor = TabInfo.CommonEditorConfiguration.BackColor;
				layoutControl.Appearance.ControlFocused.BackColor = TabInfo.CommonEditorConfiguration.BackColor;
			}
			if (!TabInfo.CommonEditorConfiguration.ForeColor.IsEmpty)
			{
				layoutControl.Appearance.Control.ForeColor = TabInfo.CommonEditorConfiguration.ForeColor;
				layoutControl.Appearance.ControlFocused.ForeColor = TabInfo.CommonEditorConfiguration.ForeColor;
			}
			if (!TabInfo.CommonEditorConfiguration.DropdownForeColor.IsEmpty)
			{
				layoutControl.Appearance.ControlDropDown.ForeColor = TabInfo.CommonEditorConfiguration.DropdownForeColor;
				layoutControl.Appearance.ControlDropDownHeader.ForeColor = TabInfo.CommonEditorConfiguration.DropdownForeColor;
			}
		}

		public virtual void ApplyBackground()
		{
			if (TabInfo.BackgroundLogo == null) return;
			layoutControlGroupRoot.BackgroundImage = TabInfo.BackgroundLogo;
			layoutControlGroupRoot.BackgroundImageVisible = true;
			layoutControlGroupRoot.BackgroundImageLayout = ImageLayout.Stretch;
		}

		public virtual void LoadData()
		{
			throw new NotImplementedException();
		}

		public virtual void ApplyChanges()
		{
			throw new NotImplementedException();
		}

		public virtual bool GetOutputEnableState()
		{
			throw new NotImplementedException();
		}

		public virtual void ApplyOutputEnableState(bool outputEnabled)
		{
			TabPageContainer.FormatSlideHeader();
		}

		protected void RaiseEditValueChanged()
		{
			if (!_allowToSave) return;
			_dataChanged = true;
			TabPageContainer.ParentControl.RaiseDataChanged();
		}

		#region Output
		public virtual string OutputName => TabInfo.Title;
		public virtual int SlidesCount => 1;
		public virtual bool MultipleSlidesAllowed => true;
		public virtual bool ReadyForOutput => !String.IsNullOrWhiteSpace(OutputName);
		public virtual SlideType SlideType => SlideType.ShiftCleanslate;
		public Theme SelectedTheme => SlideContainer.GetSelectedTheme(SlideType);

		public virtual OutputItem GetOutputItem()
		{
			var outputData = GetOutputData();
			return new OutputItem
			{
				Name = OutputName,
				PresentationSourcePath = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath,
					Path.GetFileName(Path.GetTempFileName())),
				SlidesCount = SlidesCount,
				IsCurrent = ((XtraTabPage)TabPageContainer).TabControl?.SelectedTabPage == TabPageContainer,
				SlideGeneratingAction = (processor, destinationPresentation) =>
				{
					processor.AppendSolutionCommonSlide(outputData, destinationPresentation);
				},
				PreviewGeneratingAction = (processor, presentationSourcePath) =>
				{
					processor.PrepareSolutionCommonSlide(presentationSourcePath, outputData);
				}
			};
		}

		protected virtual OutputDataPackage GetOutputData()
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
