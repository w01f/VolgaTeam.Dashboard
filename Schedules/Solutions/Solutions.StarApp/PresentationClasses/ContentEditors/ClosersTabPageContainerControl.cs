using System;
using System.Windows.Forms;
using DevExpress.XtraTab;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ClosersTabPageContainerControl<TClosersTabControl> : XtraTabPage, IClosersTabPageContainer where TClosersTabControl : ClosersTabBaseControl
	{
		public ClosersControl ParentControl { get; }
		public ClosersTabBaseControl ContentControl { get; private set; }

		public ClosersTabPageContainerControl(ClosersControl closersControl)
		{
			ParentControl = closersControl;
			InitializeComponent();

			if (typeof(ClosersTabAControl) == typeof(TClosersTabControl))
				Text = ParentControl.SlideContainer.StarInfo.Titles.Tab11SubATitle;
			else if (typeof(ClosersTabBControl) == typeof(TClosersTabControl))
				Text = ParentControl.SlideContainer.StarInfo.Titles.Tab11SubBTitle;
			else if (typeof(ClosersTabCControl) == typeof(TClosersTabControl))
				Text = ParentControl.SlideContainer.StarInfo.Titles.Tab11SubCTitle;
		}

		public void LoadContent()
		{
			if (ContentControl != null) return;
			Application.DoEvents();
			ContentControl = (TClosersTabControl)Activator.CreateInstance(typeof(TClosersTabControl), this);
			ContentControl.Dock = DockStyle.Fill;
			Controls.Add(ContentControl);
			ParentControl.SlideContainer.AssignCloseActiveEditorsOnOutsideClick(ContentControl);
			Application.DoEvents();
		}
	}
}
