using System;
using System.IO;
using System.Windows.Forms;
using Asa.Common.Core.Configuration;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Common.InteropClasses;
using Asa.Solutions.Common.PresentationClasses.Output;
using DevExpress.XtraTab;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ClosersTabBaseControl : UserControl
	{
		protected bool _allowToSave;
		protected bool _dataChanged;

		protected IClosersTabPageContainer TabPageContainer { get; }
		protected ClosersControl ClosersContentContainer => TabPageContainer.ParentControl;
		public virtual string OutputName { get; }
		public int SlidesCount => 1;
		public virtual bool ReadyForOutput => !String.IsNullOrWhiteSpace(OutputName);

		public ClosersTabBaseControl(IClosersTabPageContainer closersContentContainer)
		{
			TabPageContainer = closersContentContainer;
			InitializeComponent();
		}

		public virtual void LoadData()
		{
			throw new NotImplementedException();
		}

		public virtual void ApplyChanges()
		{
			throw new NotImplementedException();
		}

		public OutputItem GetOutputItem()
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
					processor.AppendStarCommonSlide(outputData, destinationPresentation);
				},
				PreviewGeneratingAction = (processor, presentationSourcePath) =>
				{
					processor.PrepareStarCommonSlide(presentationSourcePath, outputData);
				}
			};
		}

		protected virtual OutputDataPackage GetOutputData()
		{
			throw new NotImplementedException();
		}
	}
}
