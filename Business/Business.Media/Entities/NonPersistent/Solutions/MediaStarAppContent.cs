﻿using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.Persistent;
using Asa.Business.Solutions.StarApp.Entities.NonPersistent;

namespace Asa.Business.Media.Entities.NonPersistent.Solutions
{
	public class MediaStarAppContent : StarAppContent
	{
		public override BaseScheduleSettings ScheduleSettings => ((MediaStarAppSolution)Parent).Schedule.Settings;
	}
}