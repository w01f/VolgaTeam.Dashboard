using System;
using System.Threading.Tasks;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Media.Configuration
{
	public class ResourceManager
	{
		public static ResourceManager Instance { get; } = new ResourceManager();

		public StorageFile TabsConfigFile { get; private set; }
		public StorageFile Gallery1ConfigFile { get; private set; }
		public StorageFile Gallery2ConfigFile { get; private set; }

		public StorageFile MediaListsFile { get; private set; }

		public StorageFile SolutionsConfigFile { get; private set; }

		public StorageFile MainAppTitleTextFile { get; private set; }
		public StorageFile MainAppIconFile { get; private set; }
		public StorageFile MainAppRibbonLogoFile { get; private set; }

		#region Program Schedule Resources
		public StorageFile ProgramScheduleRibbonLogoFile { get; private set; }
		public StorageFile ProgramScheduleNoRecordsLogoFile { get; private set; }
		public StorageFile ProgramScheduleNoProgramsLogoFile { get; private set; }
		public StorageFile ProgramScheduleNoDigitalItemsLogoFile { get; private set; }
		#endregion

		#region Snapshots Resources
		public StorageFile SnapshotsRibbonLogoFile { get; private set; }
		public StorageFile SnapshotsNoRecordsLogoFile { get; private set; }
		public StorageFile SnapshotsNoProgramsLogoFile { get; private set; }
		public StorageFile SnapshotsNoDigitalItemsLogoFile { get; private set; }
		#endregion

		#region Options Resources
		public StorageFile OptionsRibbonLogoFile { get; private set; }
		public StorageFile OptionsNoRecordsLogoFile { get; private set; }
		public StorageFile OptionsNoProgramsLogoFile { get; private set; }
		public StorageFile OptionsNoDigitalItemsLogoFile { get; private set; }
		#endregion

		#region Digital Resources
		public StorageFile DigitalProductsRibbonLogoFile { get; private set; }
		public StorageFile DigitalProductsHomeMainLogoFile { get; private set; }
		public StorageFile DigitalProductsHomeRightLogoFile { get; private set; }
		public StorageFile DigitalProductsHomeBottomLogoFile { get; private set; }
		public StorageFile DigitalStandalonePackageNoRecordsLogoFile { get; private set; }
		#endregion

		#region Calendar Resources
		public StorageFile CalendarNoDataLogoFile { get; private set; }
		#endregion

		private ResourceManager() { }

		public async Task Load()
		{
			await Asa.Common.Core.Configuration.ResourceManager.Instance.Load();

			await Asa.Common.Core.Configuration.ResourceManager.Instance.ScheduleSlideTemplatesFolder.Download();
			await Asa.Common.Core.Configuration.ResourceManager.Instance.CalendarSlideTemplatesFolder.Download();
			await Asa.Common.Core.Configuration.ResourceManager.Instance.ArtworkFolder.Download();
			await Asa.Common.Core.Configuration.ResourceManager.Instance.RateCardFolder.Download();

			TabsConfigFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				String.Format("{0}_tab_names.xml",MediaMetaData.Instance.DataTypeString.ToLower())
			});
			await TabsConfigFile.Download();

			Gallery1ConfigFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"Gallery1.xml"
			});
			await Gallery1ConfigFile.Download();

			Gallery2ConfigFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"Gallery2.xml"
			});
			await Gallery2ConfigFile.Download();

			MediaListsFile = new StorageFile(
				AppProfileManager.Instance.AppDataFolder.RelativePathParts.Merge(
					String.Format("{0} Strategy.xml", MediaMetaData.Instance.DataTypeString)));
			await MediaListsFile.Download();

			SolutionsConfigFile = new StorageFile(new[]
{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"solution_templates.xml"
			});
			await SolutionsConfigFile.Download();

			MainAppTitleTextFile = new StorageFile(new[]
{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"app_brand.txt"
			});
			if (await MainAppTitleTextFile.Exists(true))
				await MainAppTitleTextFile.Download();

			MainAppIconFile = new StorageFile(new[]
{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"form_icon.ico"
			});
			if (await MainAppIconFile.Exists(true))
				await MainAppIconFile.Download();

			MainAppRibbonLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"branding_image.png"
			});
			if (await MainAppRibbonLogoFile.Exists(true))
				await MainAppRibbonLogoFile.Download();

			#region Program Schedule Resources
			ProgramScheduleRibbonLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"1d_schedule_group1.png"
			});
			if (await ProgramScheduleRibbonLogoFile.Exists(true))
				await ProgramScheduleRibbonLogoFile.Download();

			ProgramScheduleNoRecordsLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"1a_new_schedule_default.png"
			});
			if (await ProgramScheduleNoRecordsLogoFile.Exists(true))
				await ProgramScheduleNoRecordsLogoFile.Download();

			ProgramScheduleNoProgramsLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"1b_new_schedule_line_default.png"
			});
			if (await ProgramScheduleNoProgramsLogoFile.Exists(true))
				await ProgramScheduleNoProgramsLogoFile.Download();

			ProgramScheduleNoDigitalItemsLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"1c_new_schedule_digital_line.png"
			});
			if (await ProgramScheduleNoDigitalItemsLogoFile.Exists(true))
				await ProgramScheduleNoDigitalItemsLogoFile.Download();
			#endregion

			#region Snapshots Resources
			SnapshotsRibbonLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"2d_snapshot_group1.png"
			});
			if (await SnapshotsRibbonLogoFile.Exists(true))
				await SnapshotsRibbonLogoFile.Download();

			SnapshotsNoRecordsLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"2a_new_snapshot_default.png"
			});
			if (await SnapshotsNoRecordsLogoFile.Exists(true))
				await SnapshotsNoRecordsLogoFile.Download();

			SnapshotsNoProgramsLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"2b_new_snapshot_line_default.png"
			});
			if (await SnapshotsNoProgramsLogoFile.Exists(true))
				await SnapshotsNoProgramsLogoFile.Download();

			SnapshotsNoDigitalItemsLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"2c_new_snapshot_digital_line.png"
			});
			if (await SnapshotsNoDigitalItemsLogoFile.Exists(true))
				await SnapshotsNoDigitalItemsLogoFile.Download();
			#endregion

			#region Options Resources
			OptionsRibbonLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"3d_options_group1.png"
			});
			if (await OptionsRibbonLogoFile.Exists(true))
				await OptionsRibbonLogoFile.Download();

			OptionsNoRecordsLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"3a_new_options_default.png"
			});
			if (await OptionsNoRecordsLogoFile.Exists(true))
				await OptionsNoRecordsLogoFile.Download();

			OptionsNoProgramsLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"3b_new_options_line_default.png"
			});
			if (await OptionsNoProgramsLogoFile.Exists(true))
				await OptionsNoProgramsLogoFile.Download();

			OptionsNoDigitalItemsLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"3c_new_optiond_digital_line.png"
			});
			if (await OptionsNoDigitalItemsLogoFile.Exists(true))
				await OptionsNoDigitalItemsLogoFile.Download();
			#endregion

			#region Digital Resources
			DigitalProductsRibbonLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"digital_ribbon_group_1.png"
			});
			if (await DigitalProductsRibbonLogoFile.Exists(true))
				await DigitalProductsRibbonLogoFile.Download();

			DigitalProductsHomeMainLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"digital_home.png"
			});
			if (await DigitalProductsHomeMainLogoFile.Exists(true))
				await DigitalProductsHomeMainLogoFile.Download();

			DigitalProductsHomeRightLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"digital_home_right.png"
			});
			if (await DigitalProductsHomeRightLogoFile.Exists(true))
				await DigitalProductsHomeRightLogoFile.Download();

			DigitalProductsHomeBottomLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"digital_home_bottom.png"
			});
			if (await DigitalProductsHomeBottomLogoFile.Exists(true))
				await DigitalProductsHomeBottomLogoFile.Download();

			DigitalStandalonePackageNoRecordsLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"4_digital_subtab_6_default.png"
			});
			if (await DigitalStandalonePackageNoRecordsLogoFile.Exists(true))
				await DigitalStandalonePackageNoRecordsLogoFile.Download();
			#endregion

			#region Calendar Resources
			CalendarNoDataLogoFile = new StorageFile(new[]
				{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"5_summary_calendar_default.png"
			});
			if (await CalendarNoDataLogoFile.Exists(true))
				await CalendarNoDataLogoFile.Download();
			#endregion
		}
	}
}
