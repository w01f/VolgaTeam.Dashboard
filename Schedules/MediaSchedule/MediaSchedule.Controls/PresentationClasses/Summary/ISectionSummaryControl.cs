using System;
using System.Collections.Generic;
using Asa.CommonGUI.Preview;
using Asa.CommonGUI.RetractableBar;
using Asa.Core.MediaSchedule;
using DevExpress.XtraGrid.Blending;
using DevExpress.XtraTab;

namespace Asa.MediaSchedule.Controls.PresentationClasses.Summary
{
	public interface ISectionSummaryControl
	{
		bool ReadyForOutput { get; }
		List<XtraTabPage> SettingsPages { get; }
		List<ButtonInfo> BarButtons { get; }
		event EventHandler<EventArgs> DataChanged;
		void LoadData(SectionSummary sectionData, bool quickLoad);
		void SaveData();
		void GeneratePowerPointOutput();
		PreviewGroup GeneratePreview();
	}
}
