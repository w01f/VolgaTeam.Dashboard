﻿using System;
using Asa.Business.Common.Entities.NonPersistent.Schedule;

namespace Asa.Common.GUI.ContentEditors.Objects
{
	public class ContentUpdateInfo<TChangeInfo>
		where TChangeInfo : BaseScheduleChangeInfo
	{
		public bool NeedToUpdate { get; set; }
		public TChangeInfo ChangeInfo { get; set; }

		public ContentUpdateInfo()
		{
			NeedToUpdate = true;
			ChangeInfo = Activator.CreateInstance<TChangeInfo>();
		}
	}
}
