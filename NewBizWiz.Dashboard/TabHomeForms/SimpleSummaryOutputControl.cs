using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NewBizWiz.Dashboard.TabHomeForms
{
	[ToolboxItem(false)]
	public partial class SimpleSummaryOutputControl : UserControl
	{
		private readonly int _baseDetailHeight;

		public SimpleSummaryOutputControl()
		{
			InitializeComponent();
			Dock = DockStyle.Top;
			AppManager.Instance.SetClickEventHandler(this);
			if ((base.CreateGraphics()).DpiX > 96)
			{
				ckDetails.Font = new Font(ckDetails.Font.FontFamily, ckDetails.Font.Size - 2, ckDetails.Font.Style);
				laDetails.Font = new Font(laDetails.Font.FontFamily, laDetails.Font.Size - 2, laDetails.Font.Style);
				ckItem.Font = new Font(ckItem.Font.FontFamily, ckItem.Font.Size - 2, ckItem.Font.Style);
				ckMonthly.Font = new Font(ckMonthly.Font.FontFamily, ckMonthly.Font.Size - 2, ckMonthly.Font.Style);
				ckTotal.Font = new Font(ckTotal.Font.FontFamily, ckTotal.Font.Size - 2, ckTotal.Font.Style);
			}

			_baseDetailHeight = laDetails.Height;
		}

		public int ItemNumber { get; set; }

		public string ItemValue
		{
			set { ckItem.Text = value; }
		}

		public bool ItemChecked
		{
			get { return ckItem.Visible ? ckItem.Checked : false; }
		}

		public bool ItemVisible
		{
			set { ckItem.Visible = value; }
		}

		public string DetailsValue
		{
			set
			{
				laDetails.Text = value;

				var textSize = new SizeF();
				using (Graphics g = laDetails.CreateGraphics())
					textSize = g.MeasureString(laDetails.Text, laDetails.Font, laDetails.Width);

				var textHeight = (int)textSize.Height;
				if (textHeight < _baseDetailHeight)
					textHeight = _baseDetailHeight;

				int heightDifference = textHeight - laDetails.Height;
				pnDetails.Height += (heightDifference > 0 ? (heightDifference + 10) : 0);
				SimpleSummaryControl.Instance.simpleSummaryItemContainer.HideDescription();
			}
		}

		public bool DetailsChecked
		{
			get { return ckDetails.Visible ? ckDetails.Checked : false; }
		}

		public bool DetailsVisible
		{
			set { ckDetails.Visible = value; }
		}

		public string MonthlyValue
		{
			set { ckMonthly.Text = value; }
		}

		public bool MonthlyChecked
		{
			get { return ckMonthly.Checked; }
		}

		public bool MonthlyVisible
		{
			set { ckMonthly.Visible = value; }
		}

		public string ToatlValue
		{
			set { ckTotal.Text = value; }
		}

		public bool TotalChecked
		{
			get { return ckTotal.Checked; }
		}

		public bool TotalVisible
		{
			set { ckTotal.Visible = value; }
		}

		private void ckItem_CheckedChanged(object sender, EventArgs e)
		{
			if (SimpleSummaryControl.Instance.AllowToSave)
			{
				SimpleSummaryControl.Instance.UpdateOutputTotalValues();
				SimpleSummaryControl.Instance.SettingsNotSaved = true;
			}
		}
	}
}