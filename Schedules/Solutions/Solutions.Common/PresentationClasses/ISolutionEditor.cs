using System;
using Asa.Business.Solutions.Common.Enums;
using Asa.Common.Core.Enums;
using Asa.Solutions.Common.Common;

namespace Asa.Solutions.Common.PresentationClasses
{
	public interface ISolutionEditor
	{
		SolutionType SolutionType { get; }
		SlideType SelectedSlideType { get; }
		string HelpKey { get; }
		bool ReadyForOutput { get; }
		void InitControl();
		void ShowEditor();
		void ShowHomeSlide();
		void LoadData();
		void SaveData();
		void ApplyChanges();
		void OutputPowerPoint();
		void OutputPdf();
		void Preview();
		void Email();
		event EventHandler<EventArgs> DataChanged;
		event EventHandler<SelectedSlideTypeChanged> SlideTypeChanged;
		event EventHandler<OutputStatusChangedEventArgs> OutputStatusChanged;
	}
}
