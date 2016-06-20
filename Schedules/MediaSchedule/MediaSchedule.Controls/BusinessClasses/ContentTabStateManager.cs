using System;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Media.Controls.PresentationClasses.SettingsControls;

namespace Asa.Media.Controls.BusinessClasses
{
	class ContentTabStateManager
	{
		private readonly ContentController _contentController;

		public ContentTabStateManager(ContentController contentController)
		{
			_contentController = contentController;
		}

		public void Init()
		{
			Controller.Instance.TabProgramSchedule.Enabled = false;
			Controller.Instance.TabDigitalProduct.Enabled = false;
			Controller.Instance.TabCalendar1.Enabled = false;
			Controller.Instance.TabCalendar2.Enabled = false;
			Controller.Instance.TabSnapshot.Enabled = false;
			Controller.Instance.TabOptions.Enabled = false;
			Controller.Instance.TabGallery1.Enabled = false;
			Controller.Instance.TabGallery2.Enabled = false;
			Controller.Instance.TabRateCard.Enabled = false;
		}

		public void UpdateTabState()
		{
			MediaScheduleSettings scheduleSettings;
			if (_contentController.ActiveEditor is HomeControl)
				scheduleSettings = ((HomeControl)_contentController.ActiveEditor).EditedSettings;
			else
				scheduleSettings = BusinessObjects.Instance.ScheduleManager.ActiveSchedule?.Settings;

			var scheduleInitialized = scheduleSettings != null &&
									!String.IsNullOrEmpty(scheduleSettings.BusinessName) &
								   !String.IsNullOrEmpty(scheduleSettings.DecisionMaker) &
								   scheduleSettings.PresentationDate.HasValue &
								   scheduleSettings.UserFlightDateStart.HasValue &
								   scheduleSettings.UserFlightDateEnd.HasValue;

			Controller.Instance.TabProgramSchedule.Enabled = scheduleInitialized;
			Controller.Instance.TabDigitalProduct.Enabled = scheduleInitialized;
			Controller.Instance.TabCalendar1.Enabled = scheduleInitialized;
			Controller.Instance.TabCalendar2.Enabled = scheduleInitialized;
			Controller.Instance.TabSnapshot.Enabled = scheduleInitialized;
			Controller.Instance.TabOptions.Enabled = scheduleInitialized;
			Controller.Instance.TabGallery1.Enabled = scheduleInitialized;
			Controller.Instance.TabGallery2.Enabled = scheduleInitialized;
			Controller.Instance.TabRateCard.Enabled = scheduleInitialized;
		}
	}
}
