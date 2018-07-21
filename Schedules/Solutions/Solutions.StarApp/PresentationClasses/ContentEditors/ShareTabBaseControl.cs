using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Configuration;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Common.InteropClasses;
using Asa.Solutions.Common.PresentationClasses.Output;
using DevExpress.XtraTab;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ShareTabBaseControl : UserControl
	{
		protected bool _allowToSave;
		protected bool _dataChanged;

		protected IShareTabPageContainer TabPageContainer { get; }
		protected ShareControl ShareContentContainer => TabPageContainer.ParentControl;

		public virtual string OutputName { get; }
		public int SlidesCount => 1;
		public bool ReadyForOutput => !String.IsNullOrWhiteSpace(OutputName) && GetOutputDataTextItems().Any();

		protected ShareTabBaseControl()
		{
			InitializeComponent();
		}

		protected ShareTabBaseControl(IShareTabPageContainer shareTabPageContainer) : this()
		{
			TabPageContainer = shareTabPageContainer;
		}

		public virtual void LoadData()
		{
			throw new NotImplementedException();
		}

		public virtual void ApplyChanges()
		{
			throw new NotImplementedException();
		}

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

		protected virtual Dictionary<string, string> GetOutputDataTextItems()
		{
			throw new NotImplementedException();
		}
	}
}
