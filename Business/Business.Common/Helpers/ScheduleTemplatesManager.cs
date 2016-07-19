using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Asa.Business.Common.Entities.NonPersistent.ScheduleTemplates;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Common.Helpers
{
	public class ScheduleTemplatesManager
	{
		private const string TemplatesFolderName = "ScheduleTemplates";
		private const string TemplatesListFileName = "list.json";

		public StorageDirectory TemplatesFolder { get; private set; }
		public StorageFile TemplateListFile { get; private set; }

		public async Task Init()
		{
			TemplatesFolder = new StorageDirectory(AppProfileManager.Instance.SharedFolder.RelativePathParts.Merge(TemplatesFolderName));
			TemplateListFile = new StorageFile(TemplatesFolder.RelativePathParts.Merge(TemplatesListFileName));
			await TemplateListFile.AllocateParentFolder(true);
		}

		public async Task<TemplateList> GetTemplatesList()
		{
			if (await TemplateListFile.Exists(true, true))
			{
				await TemplateListFile.Download(true);
				var templateList = TemplateList.FromFile(TemplateListFile.LocalPath);
				var actualTemplateFiles = (await TemplatesFolder.GetRemoteFiles()).ToList();
				templateList.Items.RemoveAll(
					item =>
						!actualTemplateFiles.Any(template => 
							template.NameOnly.Equals(item.Name, StringComparison.OrdinalIgnoreCase)));
				return templateList;
			}
			return TemplateList.Empty();
		}

		private async Task SaveTemplatesList(TemplateList target)
		{
			File.WriteAllText(TemplateListFile.LocalPath, target.Serialize());
			await TemplateListFile.Upload();
		}

		public async Task<ScheduleTemplate> GetScheduleTemplate(string name)
		{
			var templateArchiveFile = new StorageFile(
				AppProfileManager.Instance.SharedFolder.RelativePathParts.Merge(
					new[]
					{
						TemplatesFolderName,
						String.Format("{0}.zip", name)
					}));
			if (await templateArchiveFile.Exists(true, true))
			{
				await templateArchiveFile.Download(true);
				var templateEncodedFile = ZipHelper.ExtractFiles(templateArchiveFile.LocalPath).FirstOrDefault();
				return ScheduleTemplate.FromFile(templateEncodedFile);
			}
			throw new FileNotFoundException("Schedule Template not found in cloud");
		}

		public async Task SaveTemplate(ScheduleTemplate template)
		{
			var templateList = await GetTemplatesList();
			if (!templateList.Items.Any(templateInfo => templateInfo.Name.Equals(template.Name)))
				templateList.Items.Add(template.GetTemplateInfo());

			var templateEncodedFile = Path.Combine(Path.GetTempPath(), String.Format("{0}.json", template.Name));
			File.WriteAllText(templateEncodedFile, template.Serialize());

			var templateArchiveFile = new StorageFile(
				AppProfileManager.Instance.SharedFolder.RelativePathParts.Merge(
					new[]
					{
						TemplatesFolderName,
						String.Format("{0}.zip", template.Name)
					}));
			ZipHelper.CompressFiles(new[] { templateEncodedFile }, templateArchiveFile.LocalPath);
			await templateArchiveFile.Upload();
			await SaveTemplatesList(templateList);
		}
	}
}
