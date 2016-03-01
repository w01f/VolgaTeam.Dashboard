using System;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Section.Summary;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Summary;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	public class SummaryMediaCustomControl : SummaryCustomItemControl
	{
		public SummaryMediaCustomControl()
		{
			hyperLinkEditReset.OpenLink += OnResetData;
		}

		protected override void RaiseDescriptionChanged()
		{
			((ProductInfoSummaryItem)Data).IsDefaultSate = false;
			base.RaiseDescriptionChanged();
		}

		private void OnResetData(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			e.Handled = true;

			var productInfoSummaryItem = (ProductInfoSummaryItem)Data;
			if (PopupMessageHelper.Instance.ShowWarningQuestion(
				String.Format("Are you sure you want to delete and update the {0} Data? ", productInfoSummaryItem.Title)) == DialogResult.Yes)
			{
				productInfoSummaryItem.ResetToDefault();
				LoadData();
				RaiseDataChanged();
			}
		}

		public override void LoadData()
		{
			base.LoadData();

			if (Data is ProductInfoSummaryItem)
			{
				hyperLinkEditReset.Visible = true;
				hyperLinkEditReset.Text = String.Format("Reset {0}", ((ProductInfoSummaryItem)Data).Title);
			}
			else
			{
				hyperLinkEditReset.Visible = false;
			}
		}
	}
}
