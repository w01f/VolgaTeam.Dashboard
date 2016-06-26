using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Enums;
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

		public SectionEditorType EditorType => SectionEditorType.DigitalSection;
		public string CollectionTitle => "Digital";
		public string CollectionItemTitle => "Product";

		public SectionDigitalInfoControl(SectionContainer sectionContainer) : base(sectionContainer.SectionData)
		{
			_sectionContainer = sectionContainer;
		}

		#region Data Methods
		public override void LoadData()
		{
			_digitalInfo = _sectionContainer.SectionData.DigitalInfo;
			base.LoadData();
		}

		protected override void RaiseDataChanged()
		{
			_sectionContainer.RaiseDataChanged();
		}
		#endregion

		#region Common Methods

		public override void Release()
		{
			base.Release();
			_sectionContainer = null;
		}
		#endregion

		#region Output
		protected override SlideType SlideType => MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ?
			SlideType.TVProgramSchedule :
			SlideType.RadioProgramSchedule;
		public IEnumerable<ScheduleSectionOutputType> GetAvailableOutputOptions()
		{
			return _digitalInfo != null && _digitalInfo.Records.Any() ?
				new[] { ScheduleSectionOutputType.Digital } :
				new ScheduleSectionOutputType[] { };
		}
		#endregion
	}
}
