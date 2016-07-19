using System;
using System.ComponentModel.DataAnnotations.Schema;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Solutions.Common.Enums;
using Asa.Business.Solutions.Common.Interfaces;
using Asa.Business.Solutions.Dashboard.Entities.NonPersistent;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.Persistent
{
	class MediaDashboardSolution : MediaSolution, IScheduleSolution<DashboardContent>
	{
		#region Nonpersistent Properties
		private DashboardContent _content;
		[NotMapped, JsonIgnore]
		public DashboardContent Content
		{
			get
			{
				if (_content == null)
					_content = SettingsContainer.CreateInstance<DashboardContent>(this, ContentEncoded);
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
