using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses
{
	public partial class DigitalInfoControl : UserControl
	{
		private bool _loading;
		private DigitalLegend _digitalLegend;
		public event EventHandler<RequestDigitalInfoEventArgs> RequestDefaultInfo;
		public event EventHandler<EventArgs> SettingsChanged;

		public DigitalInfoControl()
		{
			InitializeComponent();
			memoEditManual.Enter += Utilities.Instance.Editor_Enter;
			memoEditManual.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEditManual.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditMonthly.Enter += Utilities.Instance.Editor_Enter;
			spinEditMonthly.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditMonthly.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditTotal.Enter += Utilities.Instance.Editor_Enter;
			spinEditTotal.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditTotal.MouseUp += Utilities.Instance.Editor_MouseUp;
		}

		public void InitData(DigitalLegend digitalLegend)
		{
			_digitalLegend = digitalLegend;
			LoadData();
		}

		private void LoadData()
		{
			_loading = true;
			checkEditEnable.Checked = _digitalLegend.Enabled;
			checkEditAuto1.Checked = _digitalLegend.ShowWebsites && _digitalLegend.ShowProduct && !_digitalLegend.ShowDimensions && !_digitalLegend.ShowDates && !_digitalLegend.ShowImpressions && !_digitalLegend.ShowCPM && !_digitalLegend.ShowInvestment;
			checkEditAuto2.Checked = _digitalLegend.ShowWebsites && _digitalLegend.ShowProduct && !_digitalLegend.ShowDimensions && _digitalLegend.ShowDates && _digitalLegend.ShowImpressions && !_digitalLegend.ShowCPM && !_digitalLegend.ShowInvestment;
			checkEditAuto3.Checked = _digitalLegend.ShowWebsites && _digitalLegend.ShowProduct && _digitalLegend.ShowDimensions && _digitalLegend.ShowDates && _digitalLegend.ShowImpressions && _digitalLegend.ShowCPM && _digitalLegend.ShowInvestment;
			checkEditManual.Checked = _digitalLegend.AllowEdit;
			if (_digitalLegend.AllowEdit && !String.IsNullOrEmpty(_digitalLegend.Info))
				memoEditManual.EditValue = _digitalLegend.Info;
			if (RequestDefaultInfo != null)
			{
				RequestDefaultInfo(this, new RequestDigitalInfoEventArgs(memoEditAuto1, true, true, false, false, false, false, false));
				RequestDefaultInfo(this, new RequestDigitalInfoEventArgs(memoEditAuto2, true, true, false, true, true, false, false));
				RequestDefaultInfo(this, new RequestDigitalInfoEventArgs(memoEditAuto3, true, true, true, true, true, true, true));
			}
			memoEditManual_EditValueChanged(memoEditManual, EventArgs.Empty);
			checkEditTotal.Checked = _digitalLegend.Total.HasValue;
			spinEditTotal.EditValue = _digitalLegend.Total;
			checkEditMonthly.Checked = _digitalLegend.Monthly.HasValue;
			spinEditMonthly.EditValue = _digitalLegend.Monthly;
			_loading = false;
		}

		public void SaveData()
		{
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
			_digitalLegend.Info = memoEditManual.EditValue as String;
			_digitalLegend.Total = spinEditTotal.EditValue != null ? spinEditTotal.Value : (decimal?)null;
			_digitalLegend.Monthly = spinEditMonthly.EditValue != null ? spinEditMonthly.Value : (decimal?)null;
		}

		private void checkEditEnable_CheckedChanged(object sender, EventArgs e)
		{
			pnControls.Enabled = checkEditEnable.Checked;
			checkEditMonthly.Enabled = checkEditEnable.Checked;
			checkEditTotal.Enabled = checkEditEnable.Checked;
			if (_loading) return;
			if (checkEditEnable.Checked) return;
			memoEditManual.EditValue = null;
			checkEditMonthly.Checked = false;
			checkEditTotal.Checked = false;
			if (SettingsChanged != null)
				SettingsChanged(this, EventArgs.Empty);
		}

		private void checkEditCase_CheckedChanged(object sender, EventArgs e)
		{
			memoEditAuto1.Enabled = checkEditAuto1.Checked;
			memoEditAuto2.Enabled = checkEditAuto2.Checked;
			memoEditAuto3.Enabled = checkEditAuto3.Checked;
			memoEditManual.Enabled = checkEditManual.Checked;
			if (!checkEditManual.Checked)
				memoEditManual.EditValue = null;
			if (_loading) return;
			if (SettingsChanged != null)
				SettingsChanged(this, EventArgs.Empty);
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
			if (_loading) return;
			if (SettingsChanged != null)
				SettingsChanged(this, EventArgs.Empty);
		}
	
		private void checkEditMonthly_CheckedChanged(object sender, EventArgs e)
		{
			spinEditMonthly.Enabled = checkEditMonthly.Checked;
			labelControlMonthly.Enabled = checkEditMonthly.Checked;
			if (_loading) return;
			if (!checkEditMonthly.Checked)
				spinEditMonthly.EditValue = null;
			if (SettingsChanged != null)
				SettingsChanged(this, EventArgs.Empty);
		}

		private void checkEditTotal_CheckedChanged(object sender, EventArgs e)
		{
			spinEditTotal.Enabled = checkEditTotal.Checked;
			labelControlTotal.Enabled = checkEditTotal.Checked;
			if (_loading) return;
			if (!checkEditTotal.Checked)
				spinEditTotal.EditValue = null;
			if (SettingsChanged != null)
				SettingsChanged(this, EventArgs.Empty);
		}

		private void spinEdit_EditValueChanged(object sender, EventArgs e)
		{
			if (_loading) return;
			if (SettingsChanged != null)
				SettingsChanged(this, EventArgs.Empty);
		}

		private void pbHelp_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("navdig");
		}

		#region Picture Box Clicks Habdlers

		/// <summary>
		///     Buttonize the PictureBox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top += 1;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top -= 1;
		}

		#endregion
	}
}
