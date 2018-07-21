using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Preview;
using DevExpress.XtraTab;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ShareTabWControl : ShareTabBaseControl
	{
		public ShareTabWControl(IShareTabPageContainer roiTabPageContainer) : base(roiTabPageContainer)
		{
			InitializeComponent();

			slidesEditContainer.Init(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartWSlides);
			slidesEditContainer.SelectionChanged += OnEditValueChanged;

			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			slidesEditContainer.LoadData(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabW.Slide);
			Application.DoEvents();

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			slidesEditContainer.SaveData();

			_dataChanged = false;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_dataChanged = true;
			ShareContentContainer.RaiseDataChanged();
		}

		#region Output
		public override string OutputName => ShareContentContainer.SlideContainer.StarInfo.Titles.Tab5SubWTitle;

		public override OutputItem GetOutputItem()
		{
			var slideObject = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabW.Slide;

			if (slideObject.SourceSlideMasters.ContainsKey(SlideSettingsManager.Instance.SlideSettings.Format))
			{
				var slideMasterName = slideObject.SourceSlideMasters[SlideSettingsManager.Instance.SlideSettings.Format];
				var targetSlideMaster = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartWSlides.Slides
					.FirstOrDefault(slideMaster =>
						String.Equals(slideMaster.Name, slideMasterName, StringComparison.OrdinalIgnoreCase));

				if (targetSlideMaster != null)
				{
					return new OutputItem
					{
						Name = OutputName,
						PresentationSourcePath = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath,
							Path.GetFileName(Path.GetTempFileName())),
						SlidesCount = 1,
						IsCurrent = ((XtraTabPage)TabPageContainer).TabControl?.SelectedTabPage == TabPageContainer,
						SlideGeneratingAction = (processor, destinationPresentation) =>
						{
							processor.AppendSlideMaster(targetSlideMaster.GetMasterPath(), destinationPresentation);
						},
						PreviewGeneratingAction = (processor, presentationSourcePath) =>
						{
							processor.PreparePresentation(presentationSourcePath,
								presentation => processor.AppendSlideMaster(targetSlideMaster.GetMasterPath(), presentation));
						}
					};
				}
			}

			return null;
		}
		#endregion
	}
}
