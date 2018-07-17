using System;
using Asa.Common.Core.Enums;
using Asa.Solutions.Common.Common;

namespace Asa.Solutions.Common.PresentationClasses
{
	public interface ISolutionEditor
	{
		string SolutionId { get; }
		SlideType SelectedSlideType { get; }
		string HelpKey { get; }
		bool ReadyForOutput { get; }
		void InitControl(bool showSplash);
		void ShowEditor();
		void ShowHomeSlide();
		void LoadData();
		void SaveData();
		void ApplyChanges();
		void OutputPowerPointCurrent();
		void OutputPowerPointAll();
		void OutputPdf();
		void Email();
		event EventHandler<EventArgs> DataChanged;
		event EventHandler<SelectedSlideTypeChanged> SlideTypeChanged;
		event EventHandler<OutputStatusChangedEventArgs> OutputStatusChanged;
	}
}
