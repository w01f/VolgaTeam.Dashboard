using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Business.Online.Common;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Common.GUI.Common;

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
			if (CreateGraphics().DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 2, laTitle.Font.Style);
				buttonXRefreshInfoCase1.Font = new Font(buttonXRefreshInfoCase1.Font.FontFamily, buttonXRefreshInfoCase1.Font.Size - 2, buttonXRefreshInfoCase1.Font.Style);
				buttonXRefreshInfoCase2.Font = new Font(buttonXRefreshInfoCase2.Font.FontFamily, buttonXRefreshInfoCase2.Font.Size - 2, buttonXRefreshInfoCase2.Font.Style);
				buttonXRefreshInfoCase3.Font = new Font(buttonXRefreshInfoCase3.Font.FontFamily, buttonXRefreshInfoCase3.Font.Size - 2, buttonXRefreshInfoCase3.Font.Style);
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

			checkEditEnable.Checked = _digitalLegend.Enabled;

			if(!_digitalLegend.Enabled)
				_digitalLegend.ResetDefaults();

			if (_digitalLegend.AllowEdit && !String.IsNullOrEmpty(_digitalLegend.Info))
				memoEditInfo.EditValue = _digitalLegend.Info;
			else
				RequestDefaultInfo?.Invoke(this,
					new RequestDigitalInfoEventArgs(
						memoEditInfo,
						_digitalLegend.ShowWebsites,
						_digitalLegend.ShowProduct,
						_digitalLegend.ShowDimensions,
						_digitalLegend.ShowDates,
						_digitalLegend.ShowImpressions,
						_digitalLegend.ShowCPM,
						_digitalLegend.ShowInvestment));


			_loading = false;
		}

		public void SaveData()
		{
			_digitalLegend.Enabled = checkEditEnable.Checked;
			if (checkEditEnable.Checked && memoEditInfo.EditValue != memoEditInfo.Tag)
			{
				_digitalLegend.AllowEdit = true;
				_digitalLegend.Info = memoEditInfo.EditValue as string;
			}
			else
			{
				_digitalLegend.AllowEdit = false;
				_digitalLegend.Info = null;
			}
		}

		private void checkEditEnable_CheckedChanged(object sender, EventArgs e)
		{
			memoEditInfo.Enabled = checkEditEnable.Checked;
			pnBottom.Enabled = checkEditEnable.Checked;
			if (_loading) return;
			if (!checkEditEnable.Checked)
				_digitalLegend.ResetDefaults();
			SettingsChanged?.Invoke(this, EventArgs.Empty);
		}

		private void memoEdit_EditValueChanged(object sender, EventArgs e)
		{
			if (_loading) return;
			SettingsChanged?.Invoke(this, EventArgs.Empty);
		}

		private void buttonXRefreshInfoCase1_Click(object sender, EventArgs e)
		{
			_digitalLegend.ShowWebsites = false;
			_digitalLegend.ShowProduct = true;
			_digitalLegend.ShowDimensions = false;
			_digitalLegend.ShowDates = false;
			_digitalLegend.ShowImpressions = false;
			_digitalLegend.ShowCPM = false;
			_digitalLegend.ShowInvestment = false;

			RequestDefaultInfo?.Invoke(this,
				new RequestDigitalInfoEventArgs(
					memoEditInfo,
					_digitalLegend.ShowWebsites,
					_digitalLegend.ShowProduct,
					_digitalLegend.ShowDimensions,
					_digitalLegend.ShowDates,
					_digitalLegend.ShowImpressions,
					_digitalLegend.ShowCPM,
					_digitalLegend.ShowInvestment));

			SettingsChanged?.Invoke(this, EventArgs.Empty);
		}

		private void buttonXRefreshInfoCase2_Click(object sender, EventArgs e)
		{
			_digitalLegend.ShowWebsites = false;
			_digitalLegend.ShowProduct = true;
			_digitalLegend.ShowDimensions = false;
			_digitalLegend.ShowDates = false;
			_digitalLegend.ShowImpressions = true;
			_digitalLegend.ShowCPM = false;
			_digitalLegend.ShowInvestment = false;

			RequestDefaultInfo?.Invoke(this,
				new RequestDigitalInfoEventArgs(
					memoEditInfo,
					_digitalLegend.ShowWebsites,
					_digitalLegend.ShowProduct,
					_digitalLegend.ShowDimensions,
					_digitalLegend.ShowDates,
					_digitalLegend.ShowImpressions,
					_digitalLegend.ShowCPM,
					_digitalLegend.ShowInvestment));

			SettingsChanged?.Invoke(this, EventArgs.Empty);
		}

		private void buttonXRefreshInfoCase3_Click(object sender, EventArgs e)
		{
			_digitalLegend.AllowEdit = false;
			_digitalLegend.ShowWebsites = false;
			_digitalLegend.ShowProduct = true;
			_digitalLegend.ShowDimensions = false;
			_digitalLegend.ShowDates = false;
			_digitalLegend.ShowImpressions = true;
			_digitalLegend.ShowCPM = true;
			_digitalLegend.ShowInvestment = false;

			RequestDefaultInfo?.Invoke(this,
				new RequestDigitalInfoEventArgs(
					memoEditInfo,
					_digitalLegend.ShowWebsites,
					_digitalLegend.ShowProduct,
					_digitalLegend.ShowDimensions,
					_digitalLegend.ShowDates,
					_digitalLegend.ShowImpressions,
					_digitalLegend.ShowCPM,
					_digitalLegend.ShowInvestment));

			SettingsChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
