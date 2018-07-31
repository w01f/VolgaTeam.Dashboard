using System;
using System.Collections.Generic;
using Asa.Common.Core.Enums;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Common.Common;

namespace Asa.Solutions.Common.PresentationClasses
{
	public interface ISolutionEditor
	{
		string SolutionId { get; }
		SlideType SelectedSlideType { get; }
		string HelpKey { get; }
		bool MultipleSlidesAllowed { get; }
		bool ReadyForOutput { get; }
		void InitControl(bool showSplash);
		void ShowEditor();
		void ShowHomeSlide();
		void LoadData();
		void SaveData();
		void ApplyChanges();
		void OutputPowerPointCurrent();
		void OutputPowerPointAll();
		void OutputPowerPointCustom(IList<OutputItem> outputItems);
		void OutputPdf();
		void Email();
		event EventHandler<EventArgs> DataChanged;
		event EventHandler<SelectedSlideTypeChanged> SlideTypeChanged;
		event EventHandler<OutputStatusChangedEventArgs> OutputStatusChanged;
	}
}
