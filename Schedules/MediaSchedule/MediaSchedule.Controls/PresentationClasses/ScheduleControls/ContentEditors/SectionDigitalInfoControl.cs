using System.Collections.Generic;
using System.Linq;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Enums;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.BusinessClasses.Output.DigitalInfo;
using Asa.Media.Controls.PresentationClasses.Digital.DigitalInfo;
using Asa.Media.Controls.PresentationClasses.ScheduleControls.Output;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	class SectionDigitalInfoControl : BaseDigitalInfoEditControl,
		ISectionEditorControl,
		ISectionOutputControl,
		ISectionItemCollectionControl
	{
		private SectionContainer _sectionContainer;

		public SectionEditorType EditorType => SectionEditorType.DigitalInfo;
		public string CollectionTitle => "Digital";
		public string CollectionItemTitle => "Product";

		public SectionDigitalInfoControl(SectionContainer sectionContainer)
		{
			_sectionContainer = sectionContainer;
		}

		#region Data Methods
		public override void LoadData()
		{
			_dataContainer = _sectionContainer.SectionData;
			_digitalInfo = _dataContainer.DigitalInfo;

			base.LoadData();
		}

		protected override void RaiseDataChanged()
		{
			_sectionContainer.RaiseDataChanged(new SectionDataChangedEventArgs());
		}
		#endregion

		#region Common Methods
		public override void InitControls()
		{
			base.InitControls();
			pictureEditDefaultLogo.Image = BusinessObjects.Instance.ImageResourcesManager.ProgramScheduleNoDigitalItemsLogo ?? pictureEditDefaultLogo.Image;
		}

		public override void Release()
		{
			base.Release();
			_sectionContainer = null;
		}
		#endregion

		#region Output
		public override SlideType SlideType => MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ?
			SlideType.TVScheduleDigital :
			SlideType.RadioScheduleDigital;

		public IEnumerable<ScheduleSectionOutputItem> GetAvailableOutputItems()
		{
			return _digitalInfo != null && _digitalInfo.Records.Any() ?
				new[]
				{
					new ScheduleSectionOutputItem
					{
						OutputType = ScheduleSectionOutputType.DigitalOneSheet,
						IsCurrent = TabControl != null && TabControl.SelectedTabPage == this,
						SlidesCount = BaseDigitalInfoOneSheetOutputModel.SlideCount
					},
					new ScheduleSectionOutputItem
					{
						OutputType = ScheduleSectionOutputType.DigitalStrategy,
						SlidesCount = BaseDigitalInfoOneSheetOutputModel.SlideCount
					}
				} :
				new ScheduleSectionOutputItem[] { };
		}
		#endregion
	}
}
