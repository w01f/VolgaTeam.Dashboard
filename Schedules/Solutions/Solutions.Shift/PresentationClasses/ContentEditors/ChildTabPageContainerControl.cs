using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Enums;
using DevExpress.XtraTab;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	//public partial class ChildTabPageContainerControl<TChildTabControl> : UserControl, IChildTabPageContainer where TChildTabControl : ChildTabBaseControl
	public partial class ChildTabPageContainerControl<TChildTabControl> : XtraTabPage, IChildTabPageContainer where TChildTabControl : ChildTabBaseControl
	{
		private TChildTabControl _contentControl;
		public ShiftChildTabInfo TabInfo { get; }
		public MultiTabControl ParentControl { get; }
		public ChildTabBaseControl ContentControl => _contentControl;
		public bool OutputEnabled => TabInfo.IsRegularChildTab && (_contentControl?.GetOutputEnableState() ?? GetOutputEnabledStateIfControlNotLoaded());

		public ChildTabPageContainerControl(MultiTabControl multiTabControl, ShiftChildTabInfo tabInfo)
		{
			ParentControl = multiTabControl;
			TabInfo = tabInfo;
			InitializeComponent();

			Text = TabInfo.Title;

			FormatSlideHeader();
		}

		public void LoadContent()
		{
			if (_contentControl != null) return;
			_contentControl = (TChildTabControl)Activator.CreateInstance(typeof(TChildTabControl), this, TabInfo);
			_contentControl.Dock = DockStyle.Fill;
			ParentControl.SlideContainer.AssignCloseActiveEditorsOnOutsideClick(ContentControl);
			Controls.Add(_contentControl);
			_contentControl.ApplyBackground();
		}

		public void FormatSlideHeader()
		{
			if (TabInfo.IsRegularChildTab)
			{
				if (!OutputEnabled)
					Appearance.Header.ForeColor =
						Appearance.HeaderActive.ForeColor =
							Appearance.HeaderHotTracked.ForeColor = Color.Gray;
				else
					Appearance.Reset();
			}
		}

		private bool GetOutputEnabledStateIfControlNotLoaded()
		{
			switch (ParentControl.TabInfo.TabType)
			{
				case ShiftTopTabType.Cover:
					switch (TabInfo.TabType)
					{
						case ShiftChildTabType.A:
							return ParentControl.SlideContainer.EditedContent.CoverState.TabA.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.B:
							return ParentControl.SlideContainer.EditedContent.CoverState.TabB.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.C:
							return ParentControl.SlideContainer.EditedContent.CoverState.TabC.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.D:
							return ParentControl.SlideContainer.EditedContent.CoverState.TabD.EnableOutput ?? TabInfo.EnableOutput;
						default:
							return TabInfo.EnableOutput;
					}
				default:
					return TabInfo.EnableOutput;
			}
		}
	}
}
