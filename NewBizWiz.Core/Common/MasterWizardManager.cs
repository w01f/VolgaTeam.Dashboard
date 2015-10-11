using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace NewBizWiz.Core.Common
{
	public class MasterWizardManager
	{
		private static readonly MasterWizardManager _instance = new MasterWizardManager();

		public const string LeadOffSlideTemplate = @"intro-{0}.pptx";
		public const string ClientGoalsSlideTemplate = @"needs-{0}.pptx";
		public const string TargetCustomersSlideTemplate = @"target-{0}.pptx";
		public const string SimpleSummarySlideTemplate = @"closing-{0}.pptx";
		public const string SimpleSummaryTableTemplate = @"product_table_{0}.pptx";

		private MasterWizardManager()
		{
			MasterWizards = new Dictionary<string, MasterWizard>();
		}

		public Dictionary<string, MasterWizard> MasterWizards { get; set; }

		public static MasterWizardManager Instance
		{
			get { return _instance; }
		}

		public MasterWizard SelectedWizard { get; set; }

		public void Load()
		{
			var storageDirectory = ResourceManager.Instance.BasicSlideTemplatesFolder;
			if (!storageDirectory.ExistsLocal()) return;

			foreach (var folder in storageDirectory.GetFolders())
			{
				var masterWizard = new MasterWizard(folder);
				masterWizard.Init();
				if (!masterWizard.Hide)
					MasterWizards.Add(masterWizard.Name, masterWizard);
			}
		}

		public void SetMasterWizard()
		{
			MasterWizard masterWizard;
			MasterWizards.TryGetValue(SettingsManager.Instance.SelectedWizard, out masterWizard);
			SelectedWizard = masterWizard ?? MasterWizards.FirstOrDefault().Value;
		}
	}

	public class MasterWizard
	{
		private readonly StorageDirectory _sourceFolder;

		public string Name { get; private set; }
		public bool Hide { get; private set; }

		private string GetBasicTemplateFile(string[] fileName)
		{
			var file = new StorageFile(_sourceFolder.RelativePathParts.Merge(new[] { SettingsManager.Instance.SlideFolder, "Basic Slides" }).Merge(fileName));
			return file.LocalPath;
		}

		public string GetCleanslateFile()
		{
			return GetBasicTemplateFile(new[] { "CleanSlate.pptx" });
		}

		public string GetCoverFile()
		{
			return GetBasicTemplateFile(new[] { "WizCover.pptx" });
		}

		public string GetGenericCoverFile()
		{
			return GetBasicTemplateFile(new[] { "WizCover2.pptx" });
		}

		public string GetLeadoffStatementsFile(string fileName)
		{
			return GetBasicTemplateFile(new[] { "intro slide", fileName });
		}

		public string GetClientGoalsFile(string fileName)
		{
			return GetBasicTemplateFile(new[] { "needs analysis", fileName });
		}

		public string GetTargetCustomersFile(string fileName)
		{
			return GetBasicTemplateFile(new[] { "target customer", fileName });
		}

		public string GetSimpleSummaryTemlateFile(string fileName)
		{
			return GetBasicTemplateFile(new[] { "closing summary", fileName });
		}

		public string GetSimpleSummaryTableFile(string fileName)
		{
			return GetBasicTemplateFile(new[] { "closing summary", "tables", fileName });
		}

		public string GetSimpleSummaryIconFile(string fileName)
		{
			return GetBasicTemplateFile(new[] { "closing summary", "tables", "icons", fileName });
		}

		private string GetOnlineTemplateFile(string[] fileName)
		{
			var file = new StorageFile(_sourceFolder.RelativePathParts.Merge(new[] { SettingsManager.Instance.SlideFolder, "Online Slides" }).Merge(fileName));
			return file.LocalPath;
		}

		public string GetOnlineOneSheetFile(string[] fileNameParts)
		{
			return GetOnlineTemplateFile(new[] { "onesheets" }.Merge(fileNameParts));
		}

		public string GetOnlinePackageFile(int rowCount, bool showScreenshot)
		{
			return GetOnlineTemplateFile(new[]
			{
				"table",
				String.Format("digitaltable_{0}{1}.pptx", rowCount, (showScreenshot ? "p" : String.Empty))
			});
		}

		public string GetAdPlanFile(int totalRecords, bool moreSlides)
		{
			string fileName;
			switch (totalRecords)
			{
				case 6:
				case 7:
				case 8:
				case 11:
				case 12:
					fileName = moreSlides ? "adplan4.pptx" : "adplan6.pptx";
					break;
				case 9:
				case 10:
				case 13:
				case 14:
				case 15:
					fileName = moreSlides ? "adplan5.pptx" : "adplan6.pptx";
					break;
				default:
					if (totalRecords < 6)
						fileName = String.Format("adplan{0}.pptx", totalRecords);
					else
						fileName = "adplan5.pptx";
					break;
			}
			return GetOnlineTemplateFile(new[]
			{
				"adplan",
				fileName
			});
		}

		public string GetOnlineSummaryFile()
		{
			return GetOnlineTemplateFile(new[] { "summary", "digital_summary.pptx" });
		}

		public MasterWizard(StorageDirectory sourceDirectory)
		{
			_sourceFolder = sourceDirectory;
			Name = _sourceFolder.Name;
		}

		public void Init()
		{
			LoadSettings();
		}

		private void LoadSettings()
		{
			Hide = false;
			var settingsFile = new StorageFile(_sourceFolder.RelativePathParts.Merge("Settings.xml"));
			if (!settingsFile.ExistsLocal()) return;
			var document = new XmlDocument();
			try
			{
				document.Load(settingsFile.LocalPath);
				var node = document.SelectSingleNode(@"/settings/hide");
				if (node != null)
				{
					bool tempBool = false;
					if (bool.TryParse(node.InnerText, out tempBool))
						Hide = tempBool;
				}
			}
			catch { }
		}
	}
}