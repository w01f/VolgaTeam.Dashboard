using System;
using Asa.Business.Common.Enums;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Media.Controls.PresentationClasses.SettingsControls;

namespace Asa.Media.Controls.BusinessClasses.Managers
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

			var commonPartisionsAvailable =
				scheduleSettings?.EditMode == ScheduleEditMode.Quick ||
					(!String.IsNullOrEmpty(scheduleSettings?.BusinessName) &&
					!String.IsNullOrEmpty(scheduleSettings.DecisionMaker) &&
					scheduleSettings.PresentationDate.HasValue);

			var dateSensiblePartitionsAvailable = commonPartisionsAvailable &&
				scheduleSettings.UserFlightDateStart.HasValue &&
				scheduleSettings.UserFlightDateEnd.HasValue;

			Controller.Instance.TabProgramSchedule.Enabled = dateSensiblePartitionsAvailable;
			Controller.Instance.TabDigitalProduct.Enabled = commonPartisionsAvailable;
			Controller.Instance.TabCalendar1.Enabled = dateSensiblePartitionsAvailable;
			Controller.Instance.TabCalendar2.Enabled = dateSensiblePartitionsAvailable;
			Controller.Instance.TabSnapshot.Enabled = dateSensiblePartitionsAvailable;
			Controller.Instance.TabOptions.Enabled = commonPartisionsAvailable;
			Controller.Instance.TabGallery1.Enabled = commonPartisionsAvailable;
			Controller.Instance.TabGallery2.Enabled = commonPartisionsAvailable;
			Controller.Instance.TabRateCard.Enabled = commonPartisionsAvailable;
		}
	}
}
