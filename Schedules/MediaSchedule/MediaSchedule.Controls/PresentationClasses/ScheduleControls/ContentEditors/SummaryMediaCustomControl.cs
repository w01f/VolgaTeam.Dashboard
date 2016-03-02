using System;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Section.Summary;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Summary;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	public class SummaryMediaCustomControl : SummaryCustomItemControl
	{
		private ProductInfoSummaryItem ProductInfoData => Data as ProductInfoSummaryItem;

		public SummaryMediaCustomControl()
		{
			hyperLinkEditReset.OpenLink += OnResetData;
		}

		protected override void RaiseDescriptionChanged()
		{
			if (ProductInfoData != null)
				ProductInfoData.IsDefaultSate = false;
			base.RaiseDescriptionChanged();
		}

		private void OnResetData(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			e.Handled = true;

			if (PopupMessageHelper.Instance.ShowWarningQuestion(
				String.Format("Are you sure you want to delete and update the {0} Data? ", ProductInfoData.Title)) == DialogResult.Yes)
			{
				ProductInfoData.ResetToDefault();
				LoadData();
				RaiseDataChanged();
			}
		}

		public override void LoadData()
		{
			base.LoadData();

			if (ProductInfoData != null)
			{
				hyperLinkEditReset.Visible = true;
				hyperLinkEditReset.Text = String.Format("Reset {0}", ProductInfoData.Title);
			}
			else
			{
				hyperLinkEditReset.Visible = false;
			}
		}
	}
}
