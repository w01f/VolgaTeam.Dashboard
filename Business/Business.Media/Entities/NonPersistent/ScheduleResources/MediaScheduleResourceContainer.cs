using System;
using System.IO;
using Asa.Business.Common.Entities.NonPersistent.ScheduleResources;
using Asa.Business.Media.Entities.Persistent;
using Asa.Common.Core.Configuration;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.ScheduleResources
{
	public class MediaScheduleResourceContainer : BaseScheduleResourceContainer
	{
		[JsonIgnore]
		public MediaSchedule Schedule => (MediaSchedule)Parent;

		[JsonIgnore]
		public override String ResourceFolderPath => Path.Combine(Path.GetDirectoryName(Schedule.Context.StoragePath),
			Constants.ScheduleResorcesFolderName); 
	}
}
