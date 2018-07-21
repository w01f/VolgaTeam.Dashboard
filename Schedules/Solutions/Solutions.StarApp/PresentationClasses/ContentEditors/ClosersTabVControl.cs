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
	public partial class ClosersTabVControl : ClosersTabBaseControl
	{
		public ClosersTabVControl(IClosersTabPageContainer closersContentContainer) : base(closersContentContainer)
		{
			InitializeComponent();

			slidesEditContainer.Init(ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartVSlides);
			slidesEditContainer.SelectionChanged += OnEditValueChanged;

			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			slidesEditContainer.LoadData(ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabV.Slide);
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
			ClosersContentContainer.RaiseDataChanged();
		}

		#region Output
		public override string OutputName => ClosersContentContainer.SlideContainer.StarInfo.Titles.Tab11SubVTitle;

		public override OutputItem GetOutputItem()
		{
			var slideObject = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabV.Slide;

			if (slideObject.SourceSlideMasters.ContainsKey(SlideSettingsManager.Instance.SlideSettings.Format))
			{
				var slideMasterName = slideObject.SourceSlideMasters[SlideSettingsManager.Instance.SlideSettings.Format];
				var targetSlideMaster = ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartVSlides.Slides
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
