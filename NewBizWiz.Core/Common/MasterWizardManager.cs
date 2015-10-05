using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

		public async Task Load()
		{
			var storageDirectory = ResourceManager.Instance.SlideTemplatesFolder;
			if (!await storageDirectory.Exists(true)) return;

			foreach (var folder in await storageDirectory.GetFolders())
			{
				var masterWizard = new MasterWizard(folder);
				await masterWizard.Init();
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

		private async Task<string> GetTemplateFile(string[] fileName)
		{
			var file = new StorageFile(_sourceFolder.RelativePathParts.Merge(new[] { SettingsManager.Instance.SlideFolder, "Basic Slides" }).Merge(fileName));
			await file.Download();
			return file.LocalPath;
		}

		public async Task<string> GetCleanslateFile()
		{
			return await GetTemplateFile(new[] { "CleanSlate.pptx" });
		}

		public async Task<string> GetCoverFile()
		{
			return await GetTemplateFile(new[] { "WizCover.pptx" });
		}

		public async Task<string> GetGenericCoverFile()
		{
			return await GetTemplateFile(new[] { "WizCover2.pptx" });
		}

		public async Task<string> GetLeadoffStatementsFile(string fileName)
		{
			return await GetTemplateFile(new[] { "intro slide", fileName });
		}

		public async Task<string> GetClientGoalsFile(string fileName)
		{
			return await GetTemplateFile(new[] { "needs analysis", fileName });
		}

		public async Task<string> GetTargetCustomersFile(string fileName)
		{
			return await GetTemplateFile(new[] { "target customer", fileName });
		}

		public async Task<string> GetSimpleSummaryTemlateFile(string fileName)
		{
			return await GetTemplateFile(new[] { "closing summary", fileName });
		}

		public async Task<string> GetSimpleSummaryTableFile(string fileName)
		{
			return await GetTemplateFile(new[] { "closing summary", "tables", fileName });
		}

		public async Task<string> GetSimpleSummaryIconFile(string fileName)
		{
			return await GetTemplateFile(new[] { "closing summary", "icons", fileName });
		}

		public MasterWizard(StorageDirectory sourceDirectory)
		{
			_sourceFolder = sourceDirectory;
			Name = _sourceFolder.Name;
		}

		public async Task Init()
		{
			await LoadSettings();
		}

		private async Task LoadSettings()
		{
			Hide = false;
			var settingsFile = new StorageFile(_sourceFolder.RelativePathParts.Merge("Settings.xml"));
			if (!await settingsFile.Exists(true)) return;
			await settingsFile.Download();
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