using System;
using Asa.Media.Controls.PresentationClasses.Digital.Settings;

namespace Asa.Media.Controls.PresentationClasses.Digital.ContentEditors
{
	interface IDigitalEditor : IDigitalSection
	{
		void LoadData();
		void RequestReload();
		void SaveData();
		void UpdateAccordingSettings(SettingsChangedEventArgs e);
		event EventHandler<DataChangedEventArgs> DataChanged;
	}
}
