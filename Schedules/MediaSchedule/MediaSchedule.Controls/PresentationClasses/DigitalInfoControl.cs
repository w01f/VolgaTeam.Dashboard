using System;
using System.Windows.Forms;
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
			memoEditInfo.Enter += Utilities.Instance.Editor_Enter;
			memoEditInfo.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEditInfo.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditMonthly.Enter += Utilities.Instance.Editor_Enter;
			spinEditMonthly.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditMonthly.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditTotal.Enter += Utilities.Instance.Editor_Enter;
			spinEditTotal.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditTotal.MouseUp += Utilities.Instance.Editor_MouseUp;
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

			buttonXShowWebsites.Checked = _digitalLegend.ShowWebsites;
			buttonXShowProduct.Checked = _digitalLegend.ShowProduct;
			buttonXShowDimensions.Checked = _digitalLegend.ShowDimensions;
			buttonXShowDates.Checked = _digitalLegend.ShowDates;
			buttonXShowImpressions.Checked = _digitalLegend.ShowImpressions;
			buttonXShowCPM.Checked = _digitalLegend.ShowCPM;
			buttonXShowInvestment.Checked = _digitalLegend.ShowInvestment;
			if (_digitalLegend.AllowEdit && !String.IsNullOrEmpty(_digitalLegend.Info))
				memoEditInfo.EditValue = _digitalLegend.Info;
			checkEditTotal.Checked = _digitalLegend.Total.HasValue;
			spinEditTotal.EditValue = _digitalLegend.Total;
			checkEditMonthly.Checked = _digitalLegend.Monthly.HasValue;
			spinEditMonthly.EditValue = _digitalLegend.Monthly;
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

			_digitalLegend.ShowWebsites = buttonXShowWebsites.Checked;
			_digitalLegend.ShowProduct = buttonXShowProduct.Checked;
			_digitalLegend.ShowDimensions = buttonXShowDimensions.Checked;
			_digitalLegend.ShowDates = buttonXShowDates.Checked;
			_digitalLegend.ShowImpressions = buttonXShowImpressions.Checked;
			_digitalLegend.ShowCPM = buttonXShowCPM.Checked;
			_digitalLegend.ShowInvestment = buttonXShowInvestment.Checked;
			_digitalLegend.Info = checkEditAllowEdit.Checked && memoEditInfo.EditValue != null ? memoEditInfo.EditValue.ToString() : String.Empty;
			_digitalLegend.Total = spinEditTotal.EditValue != null ? spinEditTotal.Value : (decimal?)null;
			_digitalLegend.Monthly = spinEditMonthly.EditValue != null ? spinEditMonthly.Value : (decimal?)null;
		}

		private void checkEditEnable_CheckedChanged(object sender, EventArgs e)
		{
			pnControls.Enabled = checkEditEnable.Checked;
			hyperLinkEditReset.Enabled = checkEditEnable.Checked;
			checkEditMonthly.Enabled = checkEditEnable.Checked;
			checkEditTotal.Enabled = checkEditEnable.Checked;
			if (_loading) return;
			if (checkEditEnable.Checked)
			{
				if ((checkEditAllowEdit.Checked || memoEditInfo.EditValue == null) && RequestDefaultInfo != null)
					RequestDefaultInfo(this, new RequestDigitalInfoEventArgs(memoEditInfo, buttonXShowWebsites.Checked, buttonXShowProduct.Checked, buttonXShowDimensions.Checked, buttonXShowDates.Checked, buttonXShowImpressions.Checked, buttonXShowCPM.Checked, buttonXShowInvestment.Checked));
			}
			else
			{
				memoEditInfo.EditValue = null;
				checkEditMonthly.Checked = false;
				checkEditTotal.Checked = false;
			}
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
