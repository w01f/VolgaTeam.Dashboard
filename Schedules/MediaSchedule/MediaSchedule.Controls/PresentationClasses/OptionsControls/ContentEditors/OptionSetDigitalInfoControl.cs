﻿using System.Collections.Generic;
using System.Linq;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Enums;
using Asa.Media.Controls.PresentationClasses.Digital.DigitalInfo;
using Asa.Media.Controls.PresentationClasses.OptionsControls.Output;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.ContentEditors
{
	class OptionSetDigitalInfoControl : BaseDigitalInfoEditControl,
		IOptionSetEditorControl,
		IOptionSetCollectionEditorControl,
		IOutputItem
	{
		private OptionSetEditorsContainer _optionSetEditorsContainer;

		public OptionEditorType EditorType => OptionEditorType.DigitalInfo;
		public string CollectionTitle => "Digital";
		public string CollectionItemTitle => "Product";

		public OptionSetDigitalInfoControl(OptionSetEditorsContainer optionSetEditorsContainer)
		{
			_optionSetEditorsContainer = optionSetEditorsContainer;
		}

		#region Data Methods
		public override void LoadData()
		{
			_dataContainer = _optionSetEditorsContainer.OptionSetData;
			_digitalInfo = _dataContainer.DigitalInfo;
			base.LoadData();
		}

		protected override void RaiseDataChanged()
		{
			_optionSetEditorsContainer.RaiseDataChanged();
		}
		#endregion

		#region Common Methods
		public override void Release()
		{
			base.Release();
			_optionSetEditorsContainer = null;
		}
		#endregion

		#region Output
		protected override SlideType SlideType => MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ?
			SlideType.TVOptions :
			SlideType.RadioOptions;

		public IList<OutputConfiguration> GetOutputConfigurations()
		{
			var outputConfigurations = new List<OutputConfiguration>();
			if (_digitalInfo.Records.Any())
				outputConfigurations.Add(new OutputConfiguration(OptionSetOutputType.Digital));
			return outputConfigurations;
		}
		#endregion
	}
}