﻿using System.Collections.Generic;
using System.Linq;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Objects.Output;

namespace Asa.Common.Core.Helpers
{
	public class MasterWizardManager
	{
		public const string LeadOffSlideTemplate = @"intro-{0}.pptx";
		public const string ClientGoalsSlideTemplate = @"needs-{0}.pptx";
		public const string TargetCustomersSlideTemplate = @"target-{0}.pptx";
		public const string SimpleSummarySlideTemplate = @"closing-{0}.pptx";
		public const string SimpleSummaryTableTemplate = @"product_table_{0}.pptx";

		private MasterWizardManager()
		{
			MasterWizards = new Dictionary<string, MasterWizard>();
		}

		public Dictionary<string, MasterWizard> MasterWizards { get; set; }

		public static MasterWizardManager Instance { get; } = new MasterWizardManager();

		public MasterWizard SelectedWizard { get; set; }

		public void Load()
		{
			var storageDirectory = ResourceManager.Instance.MasterWizardsFolder;
			if (!storageDirectory.ExistsLocal()) return;

			foreach (var folder in storageDirectory.GetLocalFolders())
			{
				var masterWizard = new MasterWizard(folder);
				masterWizard.Init();
				if (!masterWizard.Hide)
					MasterWizards.Add(masterWizard.Name, masterWizard);
			}
		}

		public void SetMasterWizard()
		{
			MasterWizards.TryGetValue(SettingsManager.Instance.SelectedWizard, out var masterWizard);
			SelectedWizard = masterWizard ?? MasterWizards.FirstOrDefault().Value;
		}
	}
}
