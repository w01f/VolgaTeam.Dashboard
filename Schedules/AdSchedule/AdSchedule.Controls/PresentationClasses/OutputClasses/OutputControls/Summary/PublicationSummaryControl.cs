using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	[ToolboxItem(false)]
	public partial class PublicationSummaryControl : UserControl
	{
		private readonly PublicationBasicOverviewControl _parent;

		public PublicationSummaryControl(PublicationBasicOverviewControl parent)
		{
			InitializeComponent();
			Dock = DockStyle.Top;
			_parent = parent;
		}

		public void FocusControl()
		{
			labelControlTitle.Focus();
		}

		public void UpdateControls()
		{
			labelControlNumber.Text = String.Format("{0}.", _parent.PrintProduct.Index);
			labelControlTitle.Text = _parent.PrintProduct.Name;
			labelFlightDates.Text = _parent.PrintProduct.Parent.FlightDates;
			memoEditDescription.EditValue = _parent.SummaryDescription;
			if (_parent.checkEditAvgADRate.Checked)
			{
				checkEditAvgADRate.Text = String.Format("Avg Ad Rate: <b>{0}</b>", _parent.PrintProduct.AvgADRate.ToString("$#,##0.00"));
				checkEditAvgADRate.Visible = true;
				checkEditAvgADRate.BringToFront();
			}
			else
			{
				checkEditAvgADRate.Visible = false;
				checkEditAvgADRate.Checked = false;
			}
			if (_parent.checkEditTotalDiscounts.Checked)
			{
				checkEditTotalDiscounts.Text = String.Format("Total Discounts: <b>{0}</b>", _parent.PrintProduct.TotalDiscountRate.ToString("$#,##0.00"));
				checkEditTotalDiscounts.Visible = true;
				checkEditTotalDiscounts.BringToFront();
			}
			else
			{
				checkEditTotalDiscounts.Visible = false;
				checkEditTotalDiscounts.Checked = false;
			}
			if (_parent.checkEditAvgPCIRate.Checked && !String.IsNullOrEmpty(_parent.checkEditAvgPCIRate.Text))
			{
				checkEditAvgPCIRate.Text = String.Format("Avg PCI: <b>{0}</b>", _parent.PrintProduct.AvgPCIRate.ToString("$#,##0.00"));
				checkEditAvgPCIRate.Visible = true;
				checkEditAvgPCIRate.BringToFront();
			}
			else
			{
				checkEditAvgPCIRate.Visible = false;
				checkEditAvgPCIRate.Checked = false;
			}
			if (_parent.checkEditTotalCost.Checked)
			{
				checkEditTotalCost.Text = String.Format("Total Cost: <b>{0}</b>", _parent.PrintProduct.TotalFinalRate.ToString("$#,##0.00"));
				checkEditTotalCost.Visible = true;
				checkEditTotalCost.BringToFront();
			}
			else
			{
				checkEditTotalCost.Visible = false;
				checkEditTotalCost.Checked = false;
			}
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

				result.Add(_parent.PrintProduct.Name);
				result.Add(_parent.PrintProduct.Parent.FlightDates);
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
				if (checkEditAvgADRate.Checked || checkEditTotalDiscounts.Checked || checkEditAvgPCIRate.Checked || checkEditTotalCost.Checked)
				{
					var investments = new List<string>();

					if (checkEditAvgADRate.Checked)
						investments.Add(checkEditAvgADRate.Text.Replace("<b>", String.Empty).Replace("</b>", String.Empty));
					if (checkEditTotalDiscounts.Checked)
						investments.Add(checkEditTotalDiscounts.Text.Replace("<b>", String.Empty).Replace("</b>", String.Empty));
					if (checkEditAvgPCIRate.Checked)
						investments.Add(checkEditAvgPCIRate.Text.Replace("<b>", String.Empty).Replace("</b>", String.Empty));
					if (checkEditTotalCost.Checked)
						investments.Add(checkEditTotalCost.Text.Replace("<b>", String.Empty).Replace("</b>", String.Empty));

					if (investments.Any())
						result.Add(String.Join(", ", investments));
				}

				return String.Join(Environment.NewLine, result);
			}
		}
		#endregion
	}
}
