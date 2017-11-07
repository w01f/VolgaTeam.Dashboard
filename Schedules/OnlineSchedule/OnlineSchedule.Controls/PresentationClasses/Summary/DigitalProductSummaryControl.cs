using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using DevExpress.Skins;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraLayout.Utils;

namespace Asa.Online.Controls.PresentationClasses.Summary
{
	[ToolboxItem(false)]
	public partial class DigitalProductSummaryControl : UserControl
	{
		private DigitalProduct _sourceProduct;

		public DigitalProductSummaryControl()
		{
			InitializeComponent();
			Dock = DockStyle.Top;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);

			simpleLabelItemIndex.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemIndex.MaxSize, scaleFactor);
			simpleLabelItemIndex.MinSize = RectangleHelper.ScaleSize(simpleLabelItemIndex.MinSize, scaleFactor);
			layoutControlItemImpressionsToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemImpressionsToggle.MaxSize, scaleFactor);
			layoutControlItemImpressionsToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemImpressionsToggle.MinSize, scaleFactor);
			simpleLabelItemImpressionsValue.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemImpressionsValue.MaxSize, scaleFactor);
			simpleLabelItemImpressionsValue.MinSize = RectangleHelper.ScaleSize(simpleLabelItemImpressionsValue.MinSize, scaleFactor);
			layoutControlItemInvestmentsToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemInvestmentsToggle.MaxSize, scaleFactor);
			layoutControlItemInvestmentsToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemInvestmentsToggle.MinSize, scaleFactor);
			simpleLabelItemInvestmentsValue.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemInvestmentsValue.MaxSize, scaleFactor);
			simpleLabelItemInvestmentsValue.MinSize = RectangleHelper.ScaleSize(simpleLabelItemInvestmentsValue.MinSize, scaleFactor);
			layoutControlItemCPMToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCPMToggle.MaxSize, scaleFactor);
			layoutControlItemCPMToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemCPMToggle.MinSize, scaleFactor);
			simpleLabelItemCPMValue.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemCPMValue.MaxSize, scaleFactor);
			simpleLabelItemCPMValue.MinSize = RectangleHelper.ScaleSize(simpleLabelItemCPMValue.MinSize, scaleFactor);
			layoutControlItemInvestmentDetailsToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemInvestmentDetailsToggle.MaxSize, scaleFactor);
			layoutControlItemInvestmentDetailsToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemInvestmentDetailsToggle.MinSize, scaleFactor);
		}

		public void FocusControl()
		{
			checkEditImpressions.Focus();
		}

		public void LoadData(DigitalProduct sourceProduct)
		{
			_sourceProduct = sourceProduct;
			simpleLabelItemIndex.Text = String.Format("{0}.", _sourceProduct.Index);
			simpleLabelItemTitle.Text = String.Format("<b>{0}</b>{1}({2}{3})",
				_sourceProduct.Name,
				Environment.NewLine,
				_sourceProduct.Category,
				!String.IsNullOrEmpty(_sourceProduct.SubCategory) ? String.Format("/{0}", _sourceProduct.SubCategory) : String.Empty);
			simpleLabelItemSites.Text = !String.IsNullOrEmpty(_sourceProduct.OutputData.Websites) ? String.Format("<b>Sites: </b>{0}", _sourceProduct.OutputData.Websites) : String.Empty;
			memoEditDescription.EditValue = _sourceProduct.OutputData.Description;

			var impressionValue = _sourceProduct.OutputData.Impressions.HasValue ? _sourceProduct.OutputData.Impressions.Value.ToString("#,##0") : String.Empty;
			if (!String.IsNullOrEmpty(impressionValue))
			{
				simpleLabelItemImpressionsValue.Text = String.Format("Impressions: <b>{0}</b>", impressionValue);
				layoutControlGroupImpressions.Visibility = LayoutVisibility.Always;
				checkEditImpressions.Checked = sourceProduct.ShowSummaryImpressions;
			}
			else
			{
				layoutControlGroupImpressions.Visibility = LayoutVisibility.Never;
				checkEditImpressions.Checked = false;
			}

			var investmentValue = _sourceProduct.OutputData.Investments.HasValue ? _sourceProduct.OutputData.Investments.Value.ToString("$#,##0") : String.Empty;
			if (!String.IsNullOrEmpty(investmentValue))
			{
				simpleLabelItemInvestmentsValue.Text = String.Format("Investment: <b>{0}</b>", investmentValue);
				layoutControlGroupInvestments.Visibility = LayoutVisibility.Always;
				checkEditInvestments.Checked = sourceProduct.ShowSummaryInvestments;
			}
			else
			{
				layoutControlGroupInvestments.Visibility = LayoutVisibility.Never;
				checkEditInvestments.Checked = false;
			}

			var cpmValue = _sourceProduct.OutputData.CPM.HasValue ? _sourceProduct.OutputData.CPM.Value.ToString("$#,##0.00") : String.Empty;
			if (!String.IsNullOrEmpty(cpmValue))
			{
				simpleLabelItemCPMValue.Text = String.Format("CPM: <b>{0}</b>", cpmValue);
				layoutControlGroupCPM.Visibility = LayoutVisibility.Always;
				checkEditCPM.Checked = sourceProduct.ShowSummaryCPM;
			}
			else
			{
				layoutControlGroupCPM.Visibility = LayoutVisibility.Never;
				checkEditCPM.Checked = false;
			}

			if (!String.IsNullOrEmpty(_sourceProduct.InvestmentDetails))
			{
				simpleLabelItemInvestmentsValue.Text = String.Format("Investment Details: <b>{0}</b>", _sourceProduct.InvestmentDetails);
				layoutControlGroupInvestmentDetails.Visibility = LayoutVisibility.Always;
				checkEditInvDetails.Checked = sourceProduct.ShowSummaryInvestmentDetails;
			}
			else
			{
				layoutControlGroupInvestmentDetails.Visibility = LayoutVisibility.Never;
				checkEditInvDetails.Checked = false;
			}

			layoutControlGroupToggles.Visibility = !String.IsNullOrEmpty(impressionValue) ||
												   !String.IsNullOrEmpty(investmentValue) || !String.IsNullOrEmpty(cpmValue) ||
												   !String.IsNullOrEmpty(_sourceProduct.InvestmentDetails)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
		}

		public void SaveData()
		{
			_sourceProduct.ShowSummaryInvestments = checkEditInvestments.Checked;
			_sourceProduct.ShowSummaryImpressions = checkEditImpressions.Checked;
			_sourceProduct.ShowSummaryCPM = checkEditCPM.Checked;
			_sourceProduct.ShowSummaryInvestmentDetails = checkEditInvDetails.Checked;
		}

		public void Release()
		{
			_sourceProduct = null;
		}

		private void OnImpressionsCheckedChanged(object sender, EventArgs e)
		{
			simpleLabelItemImpressionsValue.Enabled = checkEditImpressions.Checked;
		}

		private void OnInvestmentsCheckedChanged(object sender, EventArgs e)
		{
			simpleLabelItemInvestmentsValue.Enabled = checkEditInvestments.Checked;
		}

		private void OnCPMCheckedChanged(object sender, EventArgs e)
		{
			simpleLabelItemCPMValue.Enabled = checkEditCPM.Checked;
		}

		private void OnInvestmentDetailsCheckedChanged(object sender, EventArgs e)
		{
			simpleLabelItemInvestmentDetailsValue.Enabled = checkEditInvDetails.Checked;
		}

		private void memoEditDescription_EditValueChanged(object sender, EventArgs e)
		{
			var vi = (MemoEditViewInfo)memoEditDescription.GetViewInfo();
			using (var g = memoEditDescription.CreateGraphics())
			{
				using (var cache = new GraphicsCache(g))
				{
					var h = ((IHeightAdaptable)vi).CalcHeight(cache, vi.MaskBoxRect.Width);
					var width = (int)g.MeasureString(memoEditDescription.Text, vi.Appearance.Font, 0, vi.Appearance.GetStringFormat()).Width + 6;
					var args = new ObjectInfoArgs(cache, new Rectangle(0, 0, width, h), ObjectState.Normal);
					var rect = vi.BorderPainter.CalcBoundsByClientRectangle(args);
					memoEditDescription.Properties.ScrollBars = rect.Height > memoEditDescription.Height ? ScrollBars.Vertical : ScrollBars.None;
				}
			}
		}

		#region Output Options
		public string Title
		{
			get
			{
				var result = new List<string>();

				result.Add(_sourceProduct.Name);
				result.Add(String.Format("{0}{1}", _sourceProduct.Category, !String.IsNullOrEmpty(_sourceProduct.SubCategory) ? String.Format("/{0}", _sourceProduct.SubCategory) : String.Empty));
				if (!String.IsNullOrEmpty(_sourceProduct.OutputData.Websites))
					result.Add(_sourceProduct.OutputData.Websites);

				return String.Join(Environment.NewLine, result);
			}
		}

		public string Details
		{
			get
			{
				var result = new List<string>();

				var description = memoEditDescription.EditValue as String;
				if (!String.IsNullOrEmpty(description))
					result.Add(description);
				if (checkEditImpressions.Checked || checkEditInvestments.Checked || checkEditCPM.Checked || checkEditInvDetails.Checked)
				{
					var investments = new List<string>();

					if (checkEditImpressions.Checked)
						investments.Add(simpleLabelItemImpressionsValue.Text.Replace("<b>", String.Empty).Replace("</b>", String.Empty));
					if (checkEditInvestments.Checked)
						investments.Add(simpleLabelItemInvestmentsValue.Text.Replace("<b>", String.Empty).Replace("</b>", String.Empty));
					if (checkEditCPM.Checked)
						investments.Add(simpleLabelItemCPMValue.Text.Replace("<b>", String.Empty).Replace("</b>", String.Empty));
					if (checkEditInvDetails.Checked)
						investments.Add(simpleLabelItemInvestmentDetailsValue.Text.Replace("<b>", String.Empty).Replace("</b>", String.Empty));

					if (investments.Any())
						result.Add(String.Join(", ", investments));
				}

				return String.Join(Environment.NewLine, result);
			}
		}
		#endregion
	}
}
