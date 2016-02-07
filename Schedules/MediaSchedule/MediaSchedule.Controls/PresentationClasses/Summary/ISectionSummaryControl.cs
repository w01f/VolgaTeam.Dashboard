using System;
using System.Collections.Generic;
using Asa.Business.Media.Entities.NonPersistent.Section.Summary;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.RetractableBar;

namespace Asa.Media.Controls.PresentationClasses.Summary
{
	public interface ISectionSummaryControl
	{
		bool ReadyForOutput { get; }
		List<ISummaryInfoControl> SettingsPages { get; }
		List<ButtonInfo> BarButtons { get; }
		event EventHandler<EventArgs> DataChanged;
		void LoadData(SectionSummary sectionData, bool quickLoad);
		void SaveData();
		void Release();
		void GeneratePowerPointOutput();
		PreviewGroup GeneratePreview();
	}
}
