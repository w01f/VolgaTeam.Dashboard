using System;
using System.Linq;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Online.Entities.NonPersistent;
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
			Controller.Instance.TabDigitalPackage.Enabled = false;
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
								   !String.IsNullOrEmpty(scheduleSettings.ClientType) &
								   scheduleSettings.PresentationDate.HasValue &
								   scheduleSettings.UserFlightDateStart.HasValue &
								   scheduleSettings.UserFlightDateEnd.HasValue;

			Controller.Instance.TabProgramSchedule.Enabled = scheduleInitialized;
			Controller.Instance.TabDigitalProduct.Enabled = scheduleInitialized;
			Controller.Instance.TabDigitalPackage.Enabled = scheduleInitialized;
			Controller.Instance.TabCalendar1.Enabled = scheduleInitialized;
			Controller.Instance.TabCalendar2.Enabled = scheduleInitialized;
			Controller.Instance.TabSnapshot.Enabled = scheduleInitialized;
			Controller.Instance.TabOptions.Enabled = scheduleInitialized;
			Controller.Instance.TabGallery1.Enabled = scheduleInitialized;
			Controller.Instance.TabGallery2.Enabled = scheduleInitialized;
			Controller.Instance.TabRateCard.Enabled = scheduleInitialized;

			if (scheduleInitialized)
			{
				DigitalProductsContent digitalContent;
				if (_contentController.ActiveEditor is HomeControl)
					digitalContent = ((HomeControl)_contentController.ActiveEditor).DigitalContent;
				else
					digitalContent = BusinessObjects.Instance.ScheduleManager.ActiveSchedule?.DigitalProductsContent;

				var digitalProductsInitialized = digitalContent?.DigitalProducts.Any(p => !String.IsNullOrEmpty(p.Name)) == true;
				Controller.Instance.TabDigitalProduct.Enabled = digitalProductsInitialized;
				Controller.Instance.TabDigitalPackage.Enabled = digitalProductsInitialized;
			}
		}
	}
}
