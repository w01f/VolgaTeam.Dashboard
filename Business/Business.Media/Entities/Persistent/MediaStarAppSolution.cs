using System;
using System.ComponentModel.DataAnnotations.Schema;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Media.Entities.NonPersistent.Solutions;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Common.Enums;
using Asa.Business.Solutions.Common.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.Persistent
{
	class MediaStarAppSolution : MediaSolution, IScheduleSolution<MediaStarAppContent>
	{
		#region Nonpersistent Properties
		private MediaStarAppContent _content;
		[NotMapped, JsonIgnore]
		public MediaStarAppContent Content
		{
			get
			{
				if (_content == null)
					_content = SettingsContainer.CreateInstance<MediaStarAppContent>(this, ContentEncoded);
				return _content;
			}
			set => _content = value;
		}
		#endregion

		public MediaStarAppSolution()
		{
			Type = (Int32)SolutionType.StarApp;
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
