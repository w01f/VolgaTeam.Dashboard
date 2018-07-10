using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Asa.Business.Common.Entities.NonPersistent.ScheduleResources;

namespace Asa.Business.Solutions.Common.Entities.NonPersistent
{
	public class VideoResourceItem : BaseResourceItem
	{
		public const string VideoSourceFileName = "source";
		public const string VideoSourceDataFileName = "data.json";
		public const string VideoThumbnailFilePrefixName = "thumbnail";

		public VideoResourceItem(BaseScheduleResourceContainer parent) : base(parent) { }

		public IList<string> GetThumbnailFies()
		{
			return Directory.GetFiles(ResourceFolderPath, String.Format("{0}*.*", VideoThumbnailFilePrefixName))
				.ToList();
		}

		public string GetSourceFile()
		{
			return Directory.GetFiles(ResourceFolderPath, String.Format("{0}.*", VideoSourceFileName))
				.First();
		}
	}
}
