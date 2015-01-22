﻿using System.Collections.Generic;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.Core.Common;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.SnapshotControls
{
	public interface ISnapshotSlide
	{
		bool ReadyForOutput { get; }
		string SlideName { get; }
		string TemplateFileName { get; }
		string[][] Logos { get; set; }
		List<Dictionary<string, string>> ReplacementsList { get; }
		PreviewGroup GetPreviewGroup(Theme selectedTheme);
		void Output(Theme selectedTheme);
	}
}