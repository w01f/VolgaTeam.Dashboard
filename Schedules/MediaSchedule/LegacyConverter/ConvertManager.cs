using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Contexts;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Dictionaries;
using Asa.Common.Core.Helpers;
using Asa.Legacy.Media.Entities.Schedule;
using Asa.Media.LegacyConverter.Converters;

namespace Asa.Media.LegacyConverter
{
	class ConvertManager
	{
		private readonly MediaScheduleManager _sceduleManager = new MediaScheduleManager();

		public void Init()
		{
			AppSettingsManager.Instance.LoadSettings();

			MediaMetaData.Instance.Init("tv".Equals(AppSettingsManager.Instance.MediaType, StringComparison.OrdinalIgnoreCase) ? MediaDataType.TVSchedule : MediaDataType.RadioSchedule);
			AppProfileManager.Instance.InitApplication(MediaMetaData.Instance.AppType);
			FileStorageManager.Instance.InitLight(AppSettingsManager.Instance.DataFolderName);

			AsyncHelper.RunSync(ResourceManager.Instance.Load);

			AsyncHelper.RunSync(() => AppProfileManager.Instance.LoadProfile(false));

			ListManager.Instance.Load();
			MediaMetaData.Instance.ListManager.Load();
			Business.Online.Dictionaries.ListManager.Instance.Load(Common.Core.Configuration.ResourceManager.Instance.OnlineListsFile);

			_sceduleManager.Init();
		}

		public bool RunConversion(string sourcePath)
		{
			try
			{
				foreach (var oldSchedulePath in Directory.GetFiles(sourcePath, "*.xml"))
				{
					var oldSchedule = new RegularSchedule(oldSchedulePath);
					Application.DoEvents();

					if (_sceduleManager.SchedulesContainer.Schedules.Any(s => s.Name.Equals(oldSchedule.Name, StringComparison.OrdinalIgnoreCase)))
						continue;
					_sceduleManager.AddReqularSchedule(oldSchedule.Name);
					Application.DoEvents();

					_sceduleManager.ActiveSchedule.ImportData(oldSchedule);
					_sceduleManager.ActiveSchedule.Save();
					Application.DoEvents();
				}
				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
