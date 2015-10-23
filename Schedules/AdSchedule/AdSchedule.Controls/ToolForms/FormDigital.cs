using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;
using Asa.Core.OnlineSchedule;

namespace Asa.AdSchedule.Controls.ToolForms
{
	public partial class FormDigital : MetroForm
	{
		private bool _loading;
		private readonly DigitalLegend _digitalLegend;
		public event EventHandler<RequestDigitalInfoEventArgs> RequestDefaultInfo;

		public bool ShowOutputOnce
		{
			set
			{
				checkEditOutputOnlyOnce.Visible = value;
			}
		}

		public bool OutputOnlyFirstSlide
		{
			set
			{
				if (value)
					checkEditOutputOnlyOnce.Text = "Output Digital Info only on first Slide";
				else
					checkEditOutputOnlyOnce.Text = "Output Digital Info only on Very LAST slide";
			}
		}

		public FormDigital(DigitalLegend digitalLegend)
		{
			InitializeComponent();
			_digitalLegend = digitalLegend;
		}

		private void FormDigital_Load(object sender, EventArgs e)
		{
			_loading = true;
			checkEditEnable.Checked = _digitalLegend.Enabled;
			checkEditAuto1.Checked = _digitalLegend.ShowWebsites && _digitalLegend.ShowProduct && !_digitalLegend.ShowDimensions && !_digitalLegend.ShowDates && !_digitalLegend.ShowImpressions && !_digitalLegend.ShowCPM && !_digitalLegend.ShowInvestment;
			checkEditAuto2.Checked = _digitalLegend.ShowWebsites && _digitalLegend.ShowProduct && !_digitalLegend.ShowDimensions && _digitalLegend.ShowDates && _digitalLegend.ShowImpressions && !_digitalLegend.ShowCPM && !_digitalLegend.ShowInvestment;
			checkEditAuto3.Checked = _digitalLegend.ShowWebsites && _digitalLegend.ShowProduct && _digitalLegend.ShowDimensions && _digitalLegend.ShowDates && _digitalLegend.ShowImpressions && _digitalLegend.ShowCPM && _digitalLegend.ShowInvestment;
			checkEditManual.Checked = _digitalLegend.AllowEdit;
			checkEditApplyAll.Checked = _digitalLegend.ApplyForAll;
			checkEditOutputOnlyOnce.Checked = _digitalLegend.OutputOnlyOnce;


			if (_digitalLegend.AllowEdit && !String.IsNullOrEmpty(_digitalLegend.Info1))
				memoEditManual.EditValue = _digitalLegend.Info1;
			if (RequestDefaultInfo != null)
			{
				RequestDefaultInfo(this, new RequestDigitalInfoEventArgs(memoEditAuto1, true, true, false, false, false, false, false));
				RequestDefaultInfo(this, new RequestDigitalInfoEventArgs(memoEditAuto2, true, true, false, true, true, false, false));
				RequestDefaultInfo(this, new RequestDigitalInfoEventArgs(memoEditAuto3, true, true, true, true, true, true, true));
			}
			memoEditManual_EditValueChanged(memoEditManual, EventArgs.Empty);
			_loading = false;
		}

		private void FormDigital_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			_digitalLegend.Enabled = checkEditEnable.Checked;
			if (checkEditAuto1.Checked)
			{
				_digitalLegend.ShowWebsites = true;
				_digitalLegend.ShowProduct = true;
				_digitalLegend.ShowDimensions = false;
				_digitalLegend.ShowDates = false;
				_digitalLegend.ShowImpressions = false;
				_digitalLegend.ShowCPM = false;
				_digitalLegend.ShowInvestment = false;
			}
			else if (checkEditAuto2.Checked)
			{
				_digitalLegend.ShowWebsites = true;
				_digitalLegend.ShowProduct = true;
				_digitalLegend.ShowDimensions = false;
				_digitalLegend.ShowDates = true;
				_digitalLegend.ShowImpressions = true;
				_digitalLegend.ShowCPM = false;
				_digitalLegend.ShowInvestment = false;
			}
			else if (checkEditAuto3.Checked)
			{
				_digitalLegend.ShowWebsites = true;
				_digitalLegend.ShowProduct = true;
				_digitalLegend.ShowDimensions = true;
				_digitalLegend.ShowDates = true;
				_digitalLegend.ShowImpressions = true;
				_digitalLegend.ShowCPM = true;
				_digitalLegend.ShowInvestment = true;
			}
			else if (checkEditManual.Checked)
			{
				_digitalLegend.AllowEdit = true;
			}
			_digitalLegend.ApplyForAll = checkEditApplyAll.Checked;
			_digitalLegend.OutputOnlyOnce = checkEditOutputOnlyOnce.Checked;
			_digitalLegend.Info1 = memoEditManual.EditValue as String;
		}

		private void checkEditEnable_CheckedChanged(object sender, EventArgs e)
		{
			pnControls.Enabled = checkEditEnable.Checked;
			checkEditOutputOnlyOnce.Enabled = false;
			checkEditApplyAll.Enabled = false;
			if (_loading) return;
			if (checkEditEnable.Checked) return;
			memoEditManual.EditValue = null;
			checkEditOutputOnlyOnce.Checked = false;
			checkEditApplyAll.Checked = false;
		}

		private void checkEditCase_CheckedChanged(object sender, EventArgs e)
		{
			memoEditAuto1.Enabled = checkEditAuto1.Checked;
			memoEditAuto2.Enabled = checkEditAuto2.Checked;
			memoEditAuto3.Enabled = checkEditAuto3.Checked;
			memoEditManual.Enabled = checkEditManual.Checked;
			if (!checkEditManual.Checked)
				memoEditManual.EditValue = null;
		}

		private void memoEditAuto_EditValueChanged(object sender, EventArgs e)
		{
			var memoEdit = sender as MemoEdit;
			if (memoEdit == null) return;
			var vi = (MemoEditViewInfo)memoEdit.GetViewInfo();
			using (var g = memoEdit.CreateGraphics())
			{
				using (var cache = new GraphicsCache(g))
				{
					var h = ((IHeightAdaptable)vi).CalcHeight(cache, vi.MaskBoxRect.Width);
					var width = (int)g.MeasureString(memoEdit.Text, vi.Appearance.Font, 0, vi.Appearance.GetStringFormat()).Width + 6;
					var args = new ObjectInfoArgs(cache, new Rectangle(0, 0, width, h), ObjectState.Normal);
					var rect = vi.BorderPainter.CalcBoundsByClientRectangle(args);
					memoEdit.Properties.ScrollBars = rect.Height > memoEdit.Height ? ScrollBars.Vertical : ScrollBars.None;
				}
			}
		}

		private void memoEditManual_EditValueChanged(object sender, EventArgs e)
		{
			var memoEdit = sender as MemoEdit;
			if (memoEdit == null) return;
			if (memoEdit.EditValue == null)
			{
				memoEdit.Font = new Font(memoEdit.Font.Name, memoEdit.Font.Size, FontStyle.Italic);
				memoEdit.ForeColor = Color.DarkGray;
			}
			else
			{
				memoEdit.Font = new Font(memoEdit.Font.Name, memoEdit.Font.Size, FontStyle.Regular);
				memoEdit.ForeColor = Color.Black;
			}
		}
	}
}
