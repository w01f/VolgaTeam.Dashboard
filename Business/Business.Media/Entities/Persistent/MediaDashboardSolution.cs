using System;
using System.ComponentModel.DataAnnotations.Schema;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Media.Entities.NonPersistent.Solutions;
using Asa.Business.Solutions.Common.Enums;
using Asa.Business.Solutions.Common.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.Persistent
{
	class MediaDashboardSolution : MediaSolution, IScheduleSolution<MediaDashboardContent>
	{
		#region Nonpersistent Properties
		private MediaDashboardContent _content;
		[NotMapped, JsonIgnore]
		public MediaDashboardContent Content
		{
			get
			{
				if (_content == null)
					_content = SettingsContainer.CreateInstance<MediaDashboardContent>(this, ContentEncoded);
				return _content;
			}
			set { _content = value; }
		}
		#endregion

		public MediaDashboardSolution()
		{
			Type = (Int32)SolutionType.Dashboard;
		}

		public override void BeforeSave()
		{
			ContentEncoded = Content.Serialize();
		}
	}
}
