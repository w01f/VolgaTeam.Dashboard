using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Online.Entities.NonPersistent;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;

namespace Asa.Online.Controls.PresentationClasses.Summary
{
	[ToolboxItem(false)]
	public partial class DigitalProductSummaryControl: UserControl
	{
		private DigitalProduct _sourceProduct;

		public DigitalProductSummaryControl()
		{
			InitializeComponent();
			Dock = DockStyle.Top;
		}

		public void FocusControl()
		{
			labelControlTitle.Focus();
		}

		public void LoadData(DigitalProduct sourceProduct)
		{
			_sourceProduct = sourceProduct;
			labelControlNumber.Text = String.Format("{0}.", _sourceProduct.Index);
			labelControlTitle.Text = String.Format("<b>{0}</b>{1}({2}{3})",
				_sourceProduct.Name,
				Environment.NewLine,
				_sourceProduct.Category,
				!String.IsNullOrEmpty(_sourceProduct.SubCategory) ? String.Format("/{0}", _sourceProduct.SubCategory) : String.Empty);
			labelControlSites.Text = !String.IsNullOrEmpty(_sourceProduct.OutputData.Websites) ? String.Format("<b>Sites: </b>{0}", _sourceProduct.OutputData.Websites) : String.Empty;
			memoEditDescription.EditValue = _sourceProduct.OutputData.Description;

			var impressionValue = _sourceProduct.OutputData.Impressions.HasValue ? _sourceProduct.OutputData.Impressions.Value.ToString("#,##0") : String.Empty;
			if (!String.IsNullOrEmpty(impressionValue))
			{
				checkEditImpressions.Text = String.Format("Impressions: <b>{0}</b>", impressionValue);
				checkEditImpressions.Visible = true;
				checkEditImpressions.Checked = sourceProduct.ShowSummaryImpressions;
				checkEditImpressions.BringToFront();
			}
			else
			{
				checkEditImpressions.Visible = false;
				checkEditImpressions.Checked = false;
			}

			var investmentValue = _sourceProduct.OutputData.Investments.HasValue ? _sourceProduct.OutputData.Investments.Value.ToString("$#,##0") : String.Empty;
			if (!String.IsNullOrEmpty(investmentValue))
			{
				checkEditInvestments.Text = String.Format("Investment: <b>{0}</b>", investmentValue);
				checkEditInvestments.Visible = true;
				checkEditInvestments.Checked = sourceProduct.ShowSummaryInvestments;
				checkEditInvestments.BringToFront();
			}
			else
			{
				checkEditInvestments.Visible = false;
				checkEditInvestments.Checked = false;
			}

			var cpmValue = _sourceProduct.OutputData.CPM.HasValue ? _sourceProduct.OutputData.CPM.Value.ToString("$#,##0.00") : String.Empty;
			if (!String.IsNullOrEmpty(cpmValue))
			{
				checkEditCPM.Text = String.Format("CPM: <b>{0}</b>", cpmValue);
				checkEditCPM.Visible = true;
				checkEditCPM.Checked = sourceProduct.ShowSummaryCPM;
				checkEditCPM.BringToFront();
			}
			else
			{
				checkEditCPM.Visible = false;
				checkEditCPM.Checked = false;
			}

			if (!String.IsNullOrEmpty(_sourceProduct.InvestmentDetails))
			{
				checkEditInvDetails.Text = String.Format("Investment Details: <b>{0}</b>", _sourceProduct.InvestmentDetails);
				checkEditInvDetails.Visible = true;
				checkEditInvDetails.Checked = sourceProduct.ShowSummaryInvestmentDetails;
				checkEditInvDetails.BringToFront();
			}
			else
			{
				checkEditInvDetails.Visible = false;
				checkEditInvDetails.Checked = false;
			}
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

		private void checkEdit_CheckedChanged(object sender, EventArgs e)
		{
			var checkEdit = sender as CheckEdit;
			if (checkEdit == null) return;
			checkEdit.ForeColor = checkEdit.Checked ? Color.Black : Color.Gray;
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
						investments.Add(checkEditImpressions.Text.Replace("<b>", String.Empty).Replace("</b>", String.Empty));
					if (checkEditInvestments.Checked)
						investments.Add(checkEditInvestments.Text.Replace("<b>", String.Empty).Replace("</b>", String.Empty));
					if (checkEditCPM.Checked)
						investments.Add(checkEditCPM.Text.Replace("<b>", String.Empty).Replace("</b>", String.Empty));
					if (checkEditInvDetails.Checked)
						investments.Add(checkEditInvDetails.Text.Replace("<b>", String.Empty).Replace("</b>", String.Empty));

					if (investments.Any())
						result.Add(String.Join(", ", investments));
				}

				return String.Join(Environment.NewLine, result);
			}
		}
		#endregion
	}
}
