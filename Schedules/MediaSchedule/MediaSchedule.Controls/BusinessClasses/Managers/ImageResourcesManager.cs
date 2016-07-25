using System.Drawing;
using System.IO;
using Asa.Business.Media.Configuration;

namespace Asa.Media.Controls.BusinessClasses.Managers
{
	public class ImageResourcesManager
	{
		public Icon MainAppIcon { get; private set; }
		public Image MainAppRibbonLogo { get; private set; }

		#region Program Schedule Resources
		public Image ProgramScheduleRibbonLogo { get; private set; }
		public Image ProgramScheduleNoRecordsLogo { get; private set; }
		public Image ProgramScheduleNoProgramsLogo { get; private set; }
		public Image ProgramScheduleNoDigitalItemsLogo { get; private set; }
		#endregion

		#region Snapshots Resources
		public Image SnapshotsRibbonLogo { get; private set; }
		public Image SnapshotsNoRecordsLogo { get; private set; }
		public Image SnapshotsNoProgramsLogo { get; private set; }
		public Image SnapshotsNoDigitalItemsLogo { get; private set; }
		#endregion

		#region Options Resources
		public Image OptionsRibbonLogo { get; private set; }
		public Image OptionsNoRecordsLogo { get; private set; }
		public Image OptionsNoProgramsLogo { get; private set; }
		public Image OptionsNoDigitalItemsLogo { get; private set; }
		#endregion

		#region Digital Resources
		public Image DigitalProductsRibbonLogo { get; private set; }
		public Image DigitalProductsHomeMainLogo { get; private set; }
		public Image DigitalProductsHomeRightLogo { get; private set; }
		public Image DigitalProductsHomeBottomLogo { get; private set; }
		public Image DigitalStandalonePackageNoRecordsLogo { get; private set; }
		#endregion

		#region Calendar Resources
		public Image CalendarNoDataLogo { get; private set; }
		#endregion

		public void Load()
		{
			var resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "form_icon.ico");
			if(File.Exists(resourceFile))
				MainAppIcon = new Icon(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "branding_image.png");
			if (File.Exists(resourceFile))
				MainAppRibbonLogo = Image.FromFile(resourceFile);

			#region Program Schedule Resources
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "1d_schedule_group1.png");
			if (File.Exists(resourceFile))
				ProgramScheduleRibbonLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "1a_new_schedule_default.png");
			if (File.Exists(resourceFile))
				ProgramScheduleNoRecordsLogo= Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "1b_new_schedule_line_default.png");
			if (File.Exists(resourceFile))
				ProgramScheduleNoProgramsLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "1c_new_schedule_digital_line.png");
			if (File.Exists(resourceFile))
				ProgramScheduleNoDigitalItemsLogo = Image.FromFile(resourceFile);
			#endregion

			#region Snapshots Resources
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "2d_snapshot_group1.png");
			if (File.Exists(resourceFile))
				SnapshotsRibbonLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "2a_new_snapshot_default.png");
			if (File.Exists(resourceFile))
				SnapshotsNoRecordsLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "2b_new_snapshot_line_default.png");
			if (File.Exists(resourceFile))
				SnapshotsNoProgramsLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "2c_new_snapshot_digital_line.png");
			if (File.Exists(resourceFile))
				SnapshotsNoDigitalItemsLogo = Image.FromFile(resourceFile);
			#endregion

			#region Options Resources
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "3d_options_group1.png");
			if (File.Exists(resourceFile))
				OptionsRibbonLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "3a_new_options_default.png");
			if (File.Exists(resourceFile))
				OptionsNoRecordsLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "3b_new_options_line_default.png");
			if (File.Exists(resourceFile))
				OptionsNoProgramsLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "3c_new_options_digital_line.png");
			if (File.Exists(resourceFile))
				OptionsNoDigitalItemsLogo = Image.FromFile(resourceFile);
			#endregion

			#region Digital Resources
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "digital_ribbon_group_1.png");
			if (File.Exists(resourceFile))
				DigitalProductsRibbonLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "digital_home.png");
			if (File.Exists(resourceFile))
				DigitalProductsHomeMainLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "digital_home_right.png");
			if (File.Exists(resourceFile))
				DigitalProductsHomeRightLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "digital_home_bottom.png");
			if (File.Exists(resourceFile))
				DigitalProductsHomeBottomLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "4_digital_subtab_6_default.png");
			if (File.Exists(resourceFile))
				DigitalStandalonePackageNoRecordsLogo = Image.FromFile(resourceFile);
			#endregion

			#region Calendar Resources
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "5_summary_calendar_default.png");
			if (File.Exists(resourceFile))
				CalendarNoDataLogo = Image.FromFile(resourceFile);
			#endregion
		}
	}
}
