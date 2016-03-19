using System;
using System.Collections.Generic;
using System.IO;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Interfaces;

namespace Asa.Business.Common.Entities.NonPersistent.ScheduleTemplates
{
	public class ScheduleTemplate : IJsonCloneable<ScheduleTemplate>
	{
		public string Name { get; set; }
		public string Advertiser { get; set; }
		public DateTime Date { get; set; }
		public string ScheduleSettingsContent { get; set; }
		public List<SchedulePartitionTemplate> PartitionTemplates { get; private set; }

		public ScheduleTemplate()
		{
			PartitionTemplates = new List<SchedulePartitionTemplate>();
		}

		public static ScheduleTemplate FromFile(string filePath)
		{
			if (!File.Exists(filePath))
				return new ScheduleTemplate();
			var encodedContent = File.ReadAllText(filePath);
			return CloneHelper.Deserialize<ScheduleTemplate, ScheduleTemplate>(encodedContent);
		}

		public TemplateInfo GetTemplateInfo()
		{
			var templateInfo = new TemplateInfo();
			templateInfo.Name = Name;
			templateInfo.Advertiser = Advertiser;
			templateInfo.Date = Date;
			templateInfo.User = SiteCredentialsManager.Instance.Settings.Login;
			return templateInfo;
		}

		public void AfterClone(ScheduleTemplate source, Boolean fullClone = true) { }
	}
}
