using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Preview;
using DevExpress.XtraTab;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ClosersTabUControl : ClosersTabBaseControl
	{
		public ClosersTabUControl(IClosersTabPageContainer closersContentContainer) : base(closersContentContainer)
		{
			InitializeComponent();

			slidesEditContainer.Init(ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartUSlides);
			slidesEditContainer.SlideOutput += ClosersContentContainer.SlideContainer.OnCustomSlideOutput;
			
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

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_dataChanged = true;
			ClosersContentContainer.RaiseDataChanged();
		}

		#region Output
		public override bool ReadyForOutput => false;
		public override string OutputName => ClosersContentContainer.SlideContainer.StarInfo.Titles.Tab11SubUTitle;

		public override OutputItem GetOutputItem()
		{
			return null;
		}
		#endregion
	}
}
