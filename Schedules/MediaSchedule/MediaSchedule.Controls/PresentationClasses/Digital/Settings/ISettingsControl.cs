using System;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Common.GUI.RetractableBar;

namespace Asa.Media.Controls.PresentationClasses.Digital.Settings
{
	interface ISettingsControl
	{
		int Order { get; }
		ButtonInfo BarButton { get; }
		DigitalSettingsType SettingsType { get; }
		void LoadContentData(DigitalProductsContent content);

		event EventHandler<SettingsChangedEventArgs> DataChanged;
		
	}
}
