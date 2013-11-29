using System;
using System.Windows.Forms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses
{
	public partial class DigitalInfoControl : UserControl
	{
		private bool _loading;
		private DigitalLegend _digitalLegend;
		public event EventHandler<RequestDigitalInfoEventArgs> RequestDefaultInfo;
		public event EventHandler<EventArgs> SettingsChanged;

		public bool ShowOutputOnce
		{
			set { checkEditOutputOnlyOnce.Visible = value; }
		}

		public bool ApplyForAll
		{
			get { return checkEditApplyAll.Checked; }
		}

		public DigitalInfoControl()
		{
			InitializeComponent();
		}

		private void UpdateWarning()
		{
			var togglesSelected = 0;
			if (buttonXShowWebsites.Checked)
				togglesSelected++;
			if (buttonXShowProduct.Checked)
				togglesSelected++;
			if (buttonXShowDimensions.Checked)
				togglesSelected++;
			if (buttonXShowDates.Checked)
				togglesSelected++;
			if (buttonXShowImpressions.Checked)
				togglesSelected++;
			if (buttonXShowCPM.Checked)
				togglesSelected++;
			if (buttonXShowInvestment.Checked)
				togglesSelected++;
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
			checkEditAllowEdit.Checked = _digitalLegend.AllowEdit;
			checkEditApplyAll.Checked = _digitalLegend.ApplyForAll;
			checkEditOutputOnlyOnce.Checked = _digitalLegend.OutputOnlyOnce;

			buttonXShowWebsites.Checked = _digitalLegend.ShowWebsites;
			buttonXShowProduct.Checked = _digitalLegend.ShowProduct;
			buttonXShowDimensions.Checked = _digitalLegend.ShowDimensions;
			buttonXShowDates.Checked = _digitalLegend.ShowDates;
			buttonXShowImpressions.Checked = _digitalLegend.ShowImpressions;
			buttonXShowCPM.Checked = _digitalLegend.ShowCPM;
			buttonXShowInvestment.Checked = _digitalLegend.ShowInvestment;
			if (_digitalLegend.AllowEdit && !String.IsNullOrEmpty(_digitalLegend.Info))
				memoEditInfo.EditValue = _digitalLegend.Info;
			if (checkEditEnable.Checked && memoEditInfo.EditValue == null)
			{
				if (RequestDefaultInfo != null)
					RequestDefaultInfo(this, new RequestDigitalInfoEventArgs(memoEditInfo, buttonXShowWebsites.Checked, buttonXShowProduct.Checked, buttonXShowDimensions.Checked, buttonXShowDates.Checked, buttonXShowImpressions.Checked, buttonXShowCPM.Checked, buttonXShowInvestment.Checked));
			}
			_loading = false;
		}

		public void SaveData()
		{
			_digitalLegend.Enabled = checkEditEnable.Checked;
			_digitalLegend.AllowEdit = checkEditAllowEdit.Checked;
			_digitalLegend.ApplyForAll = checkEditApplyAll.Checked;
			_digitalLegend.OutputOnlyOnce = checkEditOutputOnlyOnce.Checked;

			_digitalLegend.ShowWebsites = buttonXShowWebsites.Checked;
			_digitalLegend.ShowProduct = buttonXShowProduct.Checked;
			_digitalLegend.ShowDimensions = buttonXShowDimensions.Checked;
			_digitalLegend.ShowDates = buttonXShowDates.Checked;
			_digitalLegend.ShowImpressions = buttonXShowImpressions.Checked;
			_digitalLegend.ShowCPM = buttonXShowCPM.Checked;
			_digitalLegend.ShowInvestment = buttonXShowInvestment.Checked;
			_digitalLegend.Info = checkEditAllowEdit.Checked && memoEditInfo.EditValue != null ? memoEditInfo.EditValue.ToString() : String.Empty;
		}

		private void checkEditEnable_CheckedChanged(object sender, EventArgs e)
		{
			pnControls.Enabled = checkEditEnable.Checked;
			hyperLinkEditReset.Enabled = checkEditEnable.Checked;
			if (_loading) return;
			if (checkEditEnable.Checked)
			{
				if ((checkEditAllowEdit.Checked || memoEditInfo.EditValue == null) && RequestDefaultInfo != null)
					RequestDefaultInfo(this, new RequestDigitalInfoEventArgs(memoEditInfo, buttonXShowWebsites.Checked, buttonXShowProduct.Checked, buttonXShowDimensions.Checked, buttonXShowDates.Checked, buttonXShowImpressions.Checked, buttonXShowCPM.Checked, buttonXShowInvestment.Checked));
			}
			else
				memoEditInfo.EditValue = null;
			if (SettingsChanged != null)
				SettingsChanged(this, EventArgs.Empty);
		}

		private void checkEditAllowEdit_CheckedChanged(object sender, EventArgs e)
		{
			buttonXShowWebsites.Enabled = !checkEditAllowEdit.Checked;
			buttonXShowProduct.Enabled = !checkEditAllowEdit.Checked;
			buttonXShowDimensions.Enabled = !checkEditAllowEdit.Checked;
			buttonXShowDates.Enabled = !checkEditAllowEdit.Checked;
			buttonXShowImpressions.Enabled = !checkEditAllowEdit.Checked;
			buttonXShowCPM.Enabled = !checkEditAllowEdit.Checked;
			buttonXShowInvestment.Enabled = !checkEditAllowEdit.Checked;
			memoEditInfo.Properties.ReadOnly = !checkEditAllowEdit.Checked;

			if (_loading || checkEditAllowEdit.Checked) return;
			if (RequestDefaultInfo != null)
				RequestDefaultInfo(this, new RequestDigitalInfoEventArgs(memoEditInfo, buttonXShowWebsites.Checked, buttonXShowProduct.Checked, buttonXShowDimensions.Checked, buttonXShowDates.Checked, buttonXShowImpressions.Checked, buttonXShowCPM.Checked, buttonXShowInvestment.Checked));
			if (SettingsChanged != null)
				SettingsChanged(this, EventArgs.Empty);
		}

		private void buttonXShow_CheckedChanged(object sender, EventArgs e)
		{
			UpdateWarning();
			if (_loading) return;
			if (RequestDefaultInfo != null)
				RequestDefaultInfo(this, new RequestDigitalInfoEventArgs(memoEditInfo, buttonXShowWebsites.Checked, buttonXShowProduct.Checked, buttonXShowDimensions.Checked, buttonXShowDates.Checked, buttonXShowImpressions.Checked, buttonXShowCPM.Checked, buttonXShowInvestment.Checked));
			if (SettingsChanged != null)
				SettingsChanged(this, EventArgs.Empty);
		}

		private void hyperLinkEditReset_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("All Digital Product Info will be Refreshed") == DialogResult.Yes)
			{
				checkEditAllowEdit.Checked = false;
				if (RequestDefaultInfo != null)
					RequestDefaultInfo(this, new RequestDigitalInfoEventArgs(memoEditInfo, buttonXShowWebsites.Checked, buttonXShowProduct.Checked, buttonXShowDimensions.Checked, buttonXShowDates.Checked, buttonXShowImpressions.Checked, buttonXShowCPM.Checked, buttonXShowInvestment.Checked));
				if (SettingsChanged != null)
					SettingsChanged(this, EventArgs.Empty);
			}
			e.Handled = true;
		}

		private void memoEditInfo_EditValueChanged(object sender, EventArgs e)
		{
			if (_loading) return;
			if (SettingsChanged != null)
				SettingsChanged(this, EventArgs.Empty);
		}

		private void checkEditApplyAll_CheckedChanged(object sender, EventArgs e)
		{
			if (_loading) return;
			if (SettingsChanged != null)
				SettingsChanged(this, EventArgs.Empty);
		}

		private void checkEditOutputOnlyOnce_CheckedChanged(object sender, EventArgs e)
		{
			if (_loading) return;
			if (SettingsChanged != null)
				SettingsChanged(this, EventArgs.Empty);
		}
	}
}
