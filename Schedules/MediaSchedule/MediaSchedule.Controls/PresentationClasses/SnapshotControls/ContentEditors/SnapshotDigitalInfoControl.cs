using System.Collections.Generic;
using System.Linq;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Enums;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.PresentationClasses.Digital.DigitalInfo;
using Asa.Media.Controls.PresentationClasses.SnapshotControls.Output;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.ContentEditors
{
	class SnapshotDigitalInfoControl : BaseDigitalInfoEditControl,
		ISnapshotEditorControl,
		ISnapshotCollectionEditorControl,
		IOutputItem
	{
		private SnapshotEditorsContainer _snapshotEditorsContainer;

		public SnapshotEditorType EditorType => SnapshotEditorType.DigitalInfo;
		public string CollectionTitle => "Digital";
		public string CollectionItemTitle => "Product";

		public SnapshotDigitalInfoControl(SnapshotEditorsContainer optionSetEditorsContainer)
		{
			_snapshotEditorsContainer = optionSetEditorsContainer;
		}

		#region Data Methods
		public override void LoadData()
		{
			_dataContainer = _snapshotEditorsContainer.SnapshotData;
			_digitalInfo = _dataContainer.DigitalInfo;
			base.LoadData();
		}

		protected override void RaiseDataChanged()
		{
			_snapshotEditorsContainer.RaiseDataChanged();
		}
		#endregion

		#region Common Methods
		public override void InitControls()
		{
			base.InitControls();
			pbNoProducts.Image = BusinessObjects.Instance.ImageResourcesManager.SnapshotsNoDigitalItemsLogo ?? pbNoProducts.Image;
		}

		public override void Release()
		{
			base.Release();
			_snapshotEditorsContainer = null;
		}
		#endregion

		#region Output
		public override SlideType SlideType => MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ?
			SlideType.TVOptionsDigital :
			SlideType.RadioOptionsDigital;

		public IList<OutputConfiguration> GetOutputConfigurations()
		{
			var outputConfigurations = new List<OutputConfiguration>();
			if (_digitalInfo.Records.Any())
			{
				outputConfigurations.Add(new OutputConfiguration(SnapshotOutputType.Digital));
				outputConfigurations.Add(new OutputConfiguration(SnapshotOutputType.DigitalStrategy));
			}
			return outputConfigurations;
		}
		#endregion
	}
}
