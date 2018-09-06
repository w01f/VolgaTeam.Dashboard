using System;
using System.Drawing;
using System.Linq;
using Asa.Business.Solutions.Common.Enums;

namespace Asa.Business.Solutions.Common.Entities.NonPersistent
{
	public class VideoClipartObject : ClipartObject
	{
		public override ClipartObjectType Type => ClipartObjectType.Video;
		
		public Guid ResourceId { get; }
		public string SourceFilePath { get; set; }
		public Image Thumbnail { get; set; }

		public VideoClipartObject(Guid resourceId)
		{
			ResourceId = resourceId;
		}

		public static VideoClipartObject FromVideoResource(VideoResourceItem resource)
		{
			return new VideoClipartObject(resource.Id);
		}

		public override ClipartObject Clone()
		{
			var cloned = new VideoClipartObject(ResourceId);
			cloned.SourceFilePath = SourceFilePath;
			cloned.Thumbnail = Thumbnail?.Clone() as Image;
			return cloned;
		}
	}
}
