﻿using Asa.Common.Core.Enums;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.ContentEditors
{
	public interface IOptionSetEditorControl
	{
		OptionEditorType EditorType { get; }
		SlideType SlideType { get; }
		void InitControls();
		void Release();
		void SaveData();
	}
}
