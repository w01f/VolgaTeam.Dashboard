using System.Windows.Forms;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.GUI.Preview;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class SlidesTabControl : ChildTabBaseControl
	{
		private SlidesChildTabInfo CustomTabInfo => (SlidesChildTabInfo)TabInfo;

		public SlidesTabControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			slidesEditContainer.Init(CustomTabInfo.Slides);
			slidesEditContainer.SlideOutput += SlideContainer.OnCustomSlideOutput;

			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			_dataChanged = false;
		}

		#region Output
		public override bool ReadyForOutput => false;

		public override OutputItem GetOutputItem()
		{
			return null;
		}
		#endregion
	}
}
