using System.Drawing;
using Asa.Common.Core.Objects.RemoteStorage;
using Asa.Common.Core.Objects.Slides;

namespace Asa.Business.Solutions.Common.Entities.NonPersistent
{
	class SolutionSlideMaster : SlideMaster
	{
		public SolutionSlideMaster(StorageDirectory root, Size thumbnailSize) : base(root)
		{
			_thumbnailSize = thumbnailSize;
		}
	}
}
