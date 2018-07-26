﻿using System;
using System.IO;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Common.GUI.Preview;
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

		public virtual void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			throw new NotImplementedException();
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
					//processor.AppendShiftCommonSlide(outputData, destinationPresentation);
				},
				PreviewGeneratingAction = (processor, presentationSourcePath) =>
				{
					//processor.PrepareShiftCommonSlide(presentationSourcePath, outputData);
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
