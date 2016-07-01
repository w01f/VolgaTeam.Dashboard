﻿using System;
using Asa.Business.Media.Entities.NonPersistent.Option;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.PresentationClasses.Digital.DigitalInfo;
using Asa.Media.Controls.Properties;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Settings
{
	class OptionsDigitalInfoSettingsControl : BaseDigitalInfoSettingsControl, IOptionSetSettingsControl
	{
		private OptionSet _optionSetData;

		public int Order => 0;
		public bool IsAvailable => true;
		public ButtonInfo BarButton { get; }
		public OptionSettingsType SettingsType => OptionSettingsType.DigitalInfo;
		public event EventHandler<SettingsChangedEventArgs> DataChanged;

		public OptionsDigitalInfoSettingsControl()
		{
			BarButton = new ButtonInfo
			{
				Logo = Resources.SectionSettingsInfo,
				Tooltip = "Open Digital Settings",
				Action = () => { TabControl.SelectedTabPage = this; }
			};
		}

		public void LoadOptionsSetData(OptionSet optionSetData)
		{
			_optionSetData = optionSetData;
			_digitalInfo = _optionSetData.DigitalInfo;
			LoadData();
		}

		protected override void RaiseDataChanged()
		{
			DataChanged?.Invoke(this, new SettingsChangedEventArgs { ChangedSettingsType = SettingsType });
		}
	}
}