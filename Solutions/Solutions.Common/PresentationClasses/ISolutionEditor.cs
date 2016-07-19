using System;
using System.Drawing;
using Asa.Business.Solutions.Common.Enums;
using Asa.Common.Core.Enums;
using Asa.Solutions.Common.Common;

namespace Asa.Solutions.Common.PresentationClasses
{
	public interface ISolutionEditor
	{
		SolutionType SolutionType { get; }
		SlideType SelectedSlideType { get; }
		string HomeText { get; }
		Image HomeLogo { get; }
		string HelpKey { get; }
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
		event EventHandler<HomeButtonStatusChangedEventArgs> HomeButtonStatusChanged;
		event EventHandler<OutputStatusChangedEventArgs> OutputStatusChanged;
	}
}
