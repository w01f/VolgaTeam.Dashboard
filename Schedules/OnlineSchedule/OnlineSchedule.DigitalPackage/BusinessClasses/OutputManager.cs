using System;
using System.IO;
using NewBizWiz.Core.Common;

namespace NewBizWiz.OnlineSchedule.DigitalPackage.BusinessClasses
{
	public class OutputManager
	{
		public static string MasterWizardsRootFolderPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Dashboard", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
		private const string DigitalPackageTemplatesFolderName = @"{0}\Online Slides\table";
		public const string DigitalPackageTemplateFileName = "digitaltable_{0}{1}.pptx";

		public string DigitalPackageTemplatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, Core.Common.SettingsManager.Instance.SelectedWizard, String.Format(DigitalPackageTemplatesFolderName, Core.Common.SettingsManager.Instance.SlideFolder)); }
		}
	}
}