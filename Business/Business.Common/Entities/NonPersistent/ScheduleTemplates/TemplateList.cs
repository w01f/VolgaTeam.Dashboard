using System;
using System.Collections.Generic;
using System.IO;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Interfaces;

namespace Asa.Business.Common.Entities.NonPersistent.ScheduleTemplates
{
	public class TemplateList : IJsonCloneable<TemplateList>
	{
		public List<TemplateInfo> Items { get; private set; }

		private TemplateList()
		{
			Items = new List<TemplateInfo>();
		}

		public static TemplateList FromFile(string filePath)
		{
			if (!File.Exists(filePath))
				return Empty();
			var encodedContent = File.ReadAllText(filePath);
			return CloneHelper.Deserialize<TemplateList, TemplateList>(encodedContent);
		}

		public static TemplateList Empty()
		{
			return new TemplateList();
		}

		public void AfterClone(TemplateList source, Boolean fullClone = true) { }
	}
}

