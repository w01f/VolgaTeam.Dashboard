using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Common.Core.Objects.Slides
{
	public class SlideMaster
	{
		public const int DefaultThumbnailHeight = 144;

		private readonly StorageDirectory _contentsFolder;
		private readonly StorageDirectory _templatesFolder;

		public Guid Identifier { get; set; }
		public string Name { get; private set; }
		public string ToolTipHeader { get; private set; }
		public string ToolTipBody { get; private set; }
		public string Group { get; set; }
		public SlideFormatEnum Format { get; set; }
		public int Order { get; private set; }
		public StorageFile LogoFile { get; private set; }

		public SlideMaster(StorageDirectory contentsFolder, StorageDirectory templatesFolder)
		{
			_contentsFolder = contentsFolder;
			_templatesFolder = templatesFolder;
			Identifier = Guid.NewGuid();
		}

		public void Load()
		{
			var files = _contentsFolder.GetLocalFiles().ToList();

			var titleFile = files.FirstOrDefault(file => file.Name == "title.txt");
			Name = titleFile != null ?
				File.ReadAllText(titleFile.LocalPath).Trim() :
				Path.GetFileName(_contentsFolder.LocalPath);

			var toolTipFile = files.FirstOrDefault(file => file.Name == "tip.txt");
			if (toolTipFile != null)
			{
				var tooltipLines = File.ReadAllLines(toolTipFile.LocalPath);
				ToolTipHeader = tooltipLines.ElementAtOrDefault(0)?.Replace("*", "");
				ToolTipBody = tooltipLines.ElementAtOrDefault(1)?.Replace("*", "");
			}

			if (Int32.TryParse(Path.GetFileName(_contentsFolder.LocalPath), out var tempInt))
				Order = tempInt;

			LogoFile = files.FirstOrDefault(file =>
				(String.Equals(file.Extension, ".PNG", StringComparison.OrdinalIgnoreCase) ||
				String.Equals(file.Extension, ".JPG", StringComparison.OrdinalIgnoreCase) ||
				String.Equals(file.Extension, ".JPEG", StringComparison.OrdinalIgnoreCase)) &&
				!file.Name.Contains("_rbn"));

		}

		public string GetMasterPath()
		{
			var masterFilePath = _contentsFolder.GetLocalFiles().FirstOrDefault(file => String.Equals(file.Extension, ".PPTX", StringComparison.OrdinalIgnoreCase));
			if (masterFilePath == null)
			{
				if (!_templatesFolder.ExistsLocal())
					AsyncHelper.RunSync(() => _templatesFolder.Allocate(false));
				masterFilePath = new StorageFile(_templatesFolder.RelativePathParts.Merge(GetMasterFileName()));
				var lastUpdateInfoFilePath = Path.Combine(_templatesFolder.LocalPath, "last_update_info.txt");
				if (!masterFilePath.ExistsLocal() ||
					!File.Exists(lastUpdateInfoFilePath) ||
					File.GetLastWriteTime(LogoFile.LocalPath) > File.GetLastWriteTime(lastUpdateInfoFilePath))
				{
					Task.Run(async () => { await masterFilePath.Download(true); }).Wait();
					File.WriteAllText(lastUpdateInfoFilePath, DateTime.Now.ToString(CultureInfo.InvariantCulture));
				}
			}
			return masterFilePath.LocalPath;
		}

		public string GetMasterFileName()
		{
			return Path.ChangeExtension(Path.GetFileName(_contentsFolder.LocalPath), ".pptx");
		}
	}
}
