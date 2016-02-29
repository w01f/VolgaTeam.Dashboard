using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Business.Online.Common;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Common.GUI.Common;
using DevExpress.XtraEditors;

namespace Asa.Media.Controls.PresentationClasses.Digital
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
			memoEditAuto1.Enter += TextEditorsHelper.Editor_Enter;
			memoEditAuto1.MouseDown += TextEditorsHelper.Editor_MouseDown;
			memoEditAuto1.MouseUp += TextEditorsHelper.Editor_MouseUp;
			memoEditAuto2.Enter += TextEditorsHelper.Editor_Enter;
			memoEditAuto2.MouseDown += TextEditorsHelper.Editor_MouseDown;
			memoEditAuto2.MouseUp += TextEditorsHelper.Editor_MouseUp;
			if (CreateGraphics().DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 2, laTitle.Font.Style);
			}
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

			_loading = false;
		}

		public void SaveData()
		{
			_digitalLegend.Enabled = checkEditAuto1.Checked || checkEditAuto2.Checked;
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
			else
			{
				_digitalLegend.AllowEdit = false;
				_digitalLegend.Info1 = null;
				_digitalLegend.Info2 = null;
				_digitalLegend.ShowWebsites = false;
				_digitalLegend.ShowProduct = false;
				_digitalLegend.ShowDimensions = false;
				_digitalLegend.ShowDates = false;
				_digitalLegend.ShowImpressions = false;
				_digitalLegend.ShowCPM = false;
				_digitalLegend.ShowInvestment = false;
			}
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
			}
			memoEditAuto1.Enabled = checkEditAuto1.Checked;
			memoEditAuto2.Enabled = checkEditAuto2.Checked;
			if (_loading) return;
			if (!checkEditAuto1.Checked)
				memoEditAuto1.EditValue = memoEditAuto1.Tag;
			if (!checkEditAuto2.Checked)
				memoEditAuto2.EditValue = memoEditAuto2.Tag;
			if (SettingsChanged != null)
				SettingsChanged(this, EventArgs.Empty);
		}

		private void memoEdit_EditValueChanged(object sender, EventArgs e)
		{
			if (_loading) return;
			if (SettingsChanged != null)
				SettingsChanged(this, EventArgs.Empty);
		}

		private void pnControls_Resize(object sender, EventArgs e)
		{
			pnCase1.Height = pnCase2.Height = pnControls.Height / 2;
		}
	}
}
