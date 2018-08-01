using System;
using System.IO;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Common.InteropClasses;
using Asa.Solutions.Common.PresentationClasses.Output;
using DevExpress.XtraTab;
using ResourceManager = Asa.Common.Core.Configuration.ResourceManager;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ChildTabBaseControl : UserControl
	{
		protected bool _allowToSave;
		protected bool _dataChanged;

		protected IChildTabPageContainer TabPageContainer { get; }
		protected BaseStarAppContainer SlideContainer => TabPageContainer.ParentControl.SlideContainer;

		public StarChildTabInfo TabInfo { get; }

		public ChildTabBaseControl()
		{
			InitializeComponent();
		}

		public ChildTabBaseControl(IChildTabPageContainer tabPageContainer, StarChildTabInfo tabInfo) : this()
		{
			TabPageContainer = tabPageContainer;
			TabInfo = tabInfo;
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

		public virtual ListDataItem GetSlideHeaderValue()
		{
			throw new NotImplementedException();
		}

		public virtual bool GetOutputEnableState()
		{
			throw new NotImplementedException();
		}

		public virtual void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
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
