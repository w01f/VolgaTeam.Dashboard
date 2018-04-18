using System;
using System.Windows.Forms;
using DevExpress.XtraTab;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ClosersTabPageContainerControl<TClosersTabControl> : XtraTabPage, IClosersTabPageContainer where TClosersTabControl : ClosersTabBaseControl
	{
		private readonly ClosersControl _closersControl;
		public ClosersTabBaseControl ContentControl { get; private set; }
		

		public ClosersTabPageContainerControl(ClosersControl closersControl)
		{
			_closersControl = closersControl;
			InitializeComponent();

			if (typeof(ClosersTabAControl) == typeof(TClosersTabControl))
				Text = _closersControl.SlideContainer.StarInfo.Titles.Tab11SubATitle;
			else if (typeof(ClosersTabBControl) == typeof(TClosersTabControl))
				Text = _closersControl.SlideContainer.StarInfo.Titles.Tab11SubBTitle;
			else if (typeof(ClosersTabCControl) == typeof(TClosersTabControl))
				Text = _closersControl.SlideContainer.StarInfo.Titles.Tab11SubCTitle;
		}

		public void LoadContent()
		{
			if (ContentControl != null) return;
			Application.DoEvents();
			ContentControl = (TClosersTabControl)Activator.CreateInstance(typeof(TClosersTabControl), _closersControl);
			ContentControl.Dock = DockStyle.Fill;
			Controls.Add(ContentControl);
			_closersControl.SlideContainer.AssignCloseActiveEditorsOnOutsideClick(ContentControl);
			Application.DoEvents();
		}
	}
}
