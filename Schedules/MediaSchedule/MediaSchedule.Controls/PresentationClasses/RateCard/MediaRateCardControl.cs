using Asa.Common.GUI.ContentEditors.Events;
using Asa.Common.GUI.RateCard;
using Asa.Media.Controls.BusinessClasses.Managers;
using DevComponents.DotNetBar;

namespace Asa.Media.Controls.PresentationClasses.RateCard
{
	class MediaRateCardControl : RateCardControl
	{
		public override string Identifier => ContentIdentifiers.RateCard;

		public override RibbonTabItem TabPage => Controller.Instance.TabRateCard;

		public override void InitControl()
		{
			_manager = BusinessObjects.Instance.RateCardManager;
			_listControl = Controller.Instance.RateCardCombo;

			base.InitControl();
		}

		public override void ShowControl(ContentOpenEventArgs args = null)
		{
			Controller.Instance.MenuOutputPdfButton.Enabled = Controller.Instance.MenuEmailButton.Enabled = false;
			base.ShowControl(args);
		}

		public override void GetHelp()
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink("ratecard");
		}
	}
}
