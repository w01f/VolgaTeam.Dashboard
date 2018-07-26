using System;
using System.ComponentModel.DataAnnotations.Schema;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Media.Entities.NonPersistent.Solutions;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Common.Enums;
using Asa.Business.Solutions.Common.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.Persistent
{
	class MediaShiftSolution : MediaSolution, IScheduleSolution<MediaShiftContent>
	{
		#region Nonpersistent Properties
		private MediaShiftContent _content;
		[NotMapped, JsonIgnore]
		public MediaShiftContent Content
		{
			get
			{
				if (_content == null)
					_content = SettingsContainer.CreateInstance<MediaShiftContent>(this, ContentEncoded);
				return _content;
			}
			set => _content = value;
		}
		#endregion

		public MediaShiftSolution()
		{
			Type = (Int32)SolutionType.Shift;
		}

		public override void BeforeSave()
		{
			ContentEncoded = Content.Serialize();
		}

		public override void InitSolutionInfo(BaseSolutionInfo solutionInfo)
		{
			Content.SolutionId = solutionInfo.Id;
		}
	}
}
