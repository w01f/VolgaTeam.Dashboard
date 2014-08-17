using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.Digital
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
			memoEditAuto1.Enter += Utilities.Instance.Editor_Enter;
			memoEditAuto1.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEditAuto1.MouseUp += Utilities.Instance.Editor_MouseUp;
			memoEditAuto2.Enter += Utilities.Instance.Editor_Enter;
			memoEditAuto2.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEditAuto2.MouseUp += Utilities.Instance.Editor_MouseUp;
			memoEditAuto3.Enter += Utilities.Instance.Editor_Enter;
			memoEditAuto3.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEditAuto3.MouseUp += Utilities.Instance.Editor_MouseUp;
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

			checkEditAuto1.Checked = (_digitalLegend.Enabled && _digitalLegend.ShowWebsites && _digitalLegend.ShowProduct && !_digitalLegend.ShowDimensions && !_digitalLegend.ShowDates && !_digitalLegend.ShowImpressions && !_digitalLegend.ShowCPM && !_digitalLegend.ShowInvestment) ||
				(_digitalLegend.AllowEdit && !String.IsNullOrEmpty(_digitalLegend.Info1));
			if (_digitalLegend.AllowEdit && !String.IsNullOrEmpty(_digitalLegend.Info1))
				memoEditAuto1.EditValue = _digitalLegend.Info1;
			else if (RequestDefaultInfo != null)
				RequestDefaultInfo(this, new RequestDigitalInfoEventArgs(memoEditAuto1, true, true, false, false, false, false, false));


			checkEditAuto2.Checked = (_digitalLegend.Enabled && _digitalLegend.ShowWebsites && _digitalLegend.ShowProduct && !_digitalLegend.ShowDimensions && _digitalLegend.ShowDates && _digitalLegend.ShowImpressions && !_digitalLegend.ShowCPM && !_digitalLegend.ShowInvestment) ||
				(_digitalLegend.AllowEdit && !String.IsNullOrEmpty(_digitalLegend.Info2));
			if (_digitalLegend.AllowEdit && !String.IsNullOrEmpty(_digitalLegend.Info2))
				memoEditAuto2.EditValue = _digitalLegend.Info2;
			else if (RequestDefaultInfo != null)
				RequestDefaultInfo(this, new RequestDigitalInfoEventArgs(memoEditAuto2, true, true, false, true, true, false, false));


			checkEditAuto3.Checked = (_digitalLegend.Enabled && _digitalLegend.ShowWebsites && _digitalLegend.ShowProduct && _digitalLegend.ShowDimensions && _digitalLegend.ShowDates && _digitalLegend.ShowImpressions && _digitalLegend.ShowCPM && _digitalLegend.ShowInvestment) ||
				(_digitalLegend.AllowEdit && !String.IsNullOrEmpty(_digitalLegend.Info3));
			if (_digitalLegend.AllowEdit && !String.IsNullOrEmpty(_digitalLegend.Info3))
				memoEditAuto3.EditValue = _digitalLegend.Info3;
			else if (RequestDefaultInfo != null)
				RequestDefaultInfo(this, new RequestDigitalInfoEventArgs(memoEditAuto3, true, true, true, true, true, true, true));

			checkEditTotal.Checked = _digitalLegend.Total.HasValue;
			spinEditTotal.EditValue = _digitalLegend.Total;
			checkEditMonthly.Checked = _digitalLegend.Monthly.HasValue;
			spinEditMonthly.EditValue = _digitalLegend.Monthly;

			_loading = false;
		}

		public void SaveData()
		{
			_digitalLegend.Enabled = checkEditAuto1.Checked || checkEditAuto2.Checked || checkEditAuto3.Checked;
			if (checkEditAuto1.Checked)
			{
				if (memoEditAuto1.EditValue == memoEditAuto1.Tag)
				{
					_digitalLegend.AllowEdit = false;
					_digitalLegend.Info1 = null;
					_digitalLegend.ShowWebsites = true;
					_digitalLegend.ShowProduct = true;
					_digitalLegend.ShowDimensions = false;
					_digitalLegend.ShowDates = false;
					_digitalLegend.ShowImpressions = false;
					_digitalLegend.ShowCPM = false;
					_digitalLegend.ShowInvestment = false;
				}
				else
				{
					_digitalLegend.AllowEdit = true;
					_digitalLegend.Info1 = memoEditAuto1.EditValue as string;
					_digitalLegend.ShowWebsites = false;
					_digitalLegend.ShowProduct = false;
					_digitalLegend.ShowDimensions = false;
					_digitalLegend.ShowDates = false;
					_digitalLegend.ShowImpressions = false;
					_digitalLegend.ShowCPM = false;
					_digitalLegend.ShowInvestment = false;
				}
			}
			else if (checkEditAuto2.Checked)
			{
				if (memoEditAuto2.EditValue == memoEditAuto2.Tag)
				{
					_digitalLegend.AllowEdit = false;
					_digitalLegend.Info2 = null;
					_digitalLegend.ShowWebsites = true;
					_digitalLegend.ShowProduct = true;
					_digitalLegend.ShowDimensions = false;
					_digitalLegend.ShowDates = true;
					_digitalLegend.ShowImpressions = true;
					_digitalLegend.ShowCPM = false;
					_digitalLegend.ShowInvestment = false;
				}
				else
				{
					_digitalLegend.AllowEdit = true;
					_digitalLegend.Info2 = memoEditAuto2.EditValue as string;
					_digitalLegend.ShowWebsites = false;
					_digitalLegend.ShowProduct = false;
					_digitalLegend.ShowDimensions = false;
					_digitalLegend.ShowDates = false;
					_digitalLegend.ShowImpressions = false;
					_digitalLegend.ShowCPM = false;
					_digitalLegend.ShowInvestment = false;
				}
			}
			else if (checkEditAuto3.Checked)
			{
				if (memoEditAuto3.EditValue == memoEditAuto3.Tag)
				{
					_digitalLegend.AllowEdit = false;
					_digitalLegend.Info3 = null;
					_digitalLegend.ShowWebsites = true;
					_digitalLegend.ShowProduct = true;
					_digitalLegend.ShowDimensions = true;
					_digitalLegend.ShowDates = true;
					_digitalLegend.ShowImpressions = true;
					_digitalLegend.ShowCPM = true;
					_digitalLegend.ShowInvestment = true;
				}
				else
				{
					_digitalLegend.AllowEdit = true;
					_digitalLegend.Info3 = memoEditAuto3.EditValue as string;
					_digitalLegend.ShowWebsites = false;
					_digitalLegend.ShowProduct = false;
					_digitalLegend.ShowDimensions = false;
					_digitalLegend.ShowDates = false;
					_digitalLegend.ShowImpressions = false;
					_digitalLegend.ShowCPM = false;
					_digitalLegend.ShowInvestment = false;
				}
			}
			else
			{
				_digitalLegend.AllowEdit = false;
				_digitalLegend.Info1 = null;
				_digitalLegend.Info2 = null;
				_digitalLegend.Info3 = null;
				_digitalLegend.ShowWebsites = false;
				_digitalLegend.ShowProduct = false;
				_digitalLegend.ShowDimensions = false;
				_digitalLegend.ShowDates = false;
				_digitalLegend.ShowImpressions = false;
				_digitalLegend.ShowCPM = false;
				_digitalLegend.ShowInvestment = false;
			}
			_digitalLegend.Total = spinEditTotal.EditValue != null ? spinEditTotal.Value : (decimal?)null;
			_digitalLegend.Monthly = spinEditMonthly.EditValue != null ? spinEditMonthly.Value : (decimal?)null;
		}

		private void checkEditCase_CheckedChanged(object sender, EventArgs e)
		{
			var checkEdit = sender as CheckEdit;
			if (checkEdit == null) return;
			if (checkEdit.Checked)
			{
				if (checkEdit != checkEditAuto1)
					checkEditAuto1.Checked = false;
				if (checkEdit != checkEditAuto2)
					checkEditAuto2.Checked = false;
				if (checkEdit != checkEditAuto3)
					checkEditAuto3.Checked = false;
			}
			memoEditAuto1.Enabled = checkEditAuto1.Checked;
			memoEditAuto2.Enabled = checkEditAuto2.Checked;
			memoEditAuto3.Enabled = checkEditAuto3.Checked;
			if (_loading) return;
			if (!checkEditAuto1.Checked)
				memoEditAuto1.EditValue = memoEditAuto1.Tag;
			if (!checkEditAuto2.Checked)
				memoEditAuto2.EditValue = memoEditAuto2.Tag;
			if (!checkEditAuto3.Checked)
				memoEditAuto3.EditValue = memoEditAuto3.Tag;
			if (SettingsChanged != null)
				SettingsChanged(this, EventArgs.Empty);
		}

		private void memoEdit_EditValueChanged(object sender, EventArgs e)
		{
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

		private void pnControls_Resize(object sender, EventArgs e)
		{
			pnCase1.Height = pnCase2.Height = pnCase3.Height = pnControls.Height / 3;
		}
	}
}
