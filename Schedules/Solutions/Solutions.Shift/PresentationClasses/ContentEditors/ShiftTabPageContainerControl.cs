using System;
using System.Windows.Forms;
using DevExpress.XtraTab;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	public partial class ShiftTabPageContainerControl<TBaseShiftControl> : XtraTabPage, IShiftTabPageContainer where TBaseShiftControl : BaseShiftControl
	{
		private TBaseShiftControl _contentControl;
		private readonly BaseShiftContainer _slideContainer;
		public BaseShiftControl ContentControl => _contentControl;

		public ShiftTabPageContainerControl(BaseShiftContainer slideContainer)
		{
			_slideContainer = slideContainer;
			InitializeComponent();

			if (typeof(CleanslateControl) == typeof(TBaseShiftControl))
				Text = _slideContainer.ShiftInfo.Titles.Tab0Title;
			if (typeof(StartersControl) == typeof(TBaseShiftControl))
				Text = _slideContainer.ShiftInfo.Titles.Tab1Title;
			else if (typeof(CNAControl) == typeof(TBaseShiftControl))
				Text = _slideContainer.ShiftInfo.Titles.Tab2Title;
			else if (typeof(MarketControl) == typeof(TBaseShiftControl))
				Text = _slideContainer.ShiftInfo.Titles.Tab3Title;
			else if (typeof(NeedsSolutionsControl) == typeof(TBaseShiftControl))
				Text = _slideContainer.ShiftInfo.Titles.Tab4Title;
			else if (typeof(CBCControl) == typeof(TBaseShiftControl))
				Text = _slideContainer.ShiftInfo.Titles.Tab5Title;
			else if (typeof(IntegratedSolutionControl) == typeof(TBaseShiftControl))
				Text = _slideContainer.ShiftInfo.Titles.Tab6Title;
			else if (typeof(InvestmentControl) == typeof(TBaseShiftControl))
				Text = _slideContainer.ShiftInfo.Titles.Tab7Title;
			else if (typeof(ClosersControl) == typeof(TBaseShiftControl))
				Text = _slideContainer.ShiftInfo.Titles.Tab8Title;
			else if (typeof(SupportMaterialsControl) == typeof(TBaseShiftControl))
				Text = _slideContainer.ShiftInfo.Titles.Tab9Title;
		}

		public void LoadContent()
		{
			if (_contentControl != null) return;
			Application.DoEvents();
			_contentControl = (TBaseShiftControl)Activator.CreateInstance(typeof(TBaseShiftControl), _slideContainer);
			_contentControl.Dock = DockStyle.Fill;
			Controls.Add(_contentControl);
			_slideContainer.AssignCloseActiveEditorsOnOutsideClick(_contentControl);
			Application.DoEvents();
		}
	}
}
