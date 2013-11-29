using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Layout;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;

namespace NewBizWiz.AdSchedule.Controls.ToolForms
{
	public partial class FormDigital : Form
	{
		private bool _loading;
		private readonly DigitalLegend _digitalLegend;
		private readonly List<ImageSource> _images = new List<ImageSource>();
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

		public bool ShowLogo
		{
			set
			{
				xtraTabControlMain.ShowTabHeader = value ? DefaultBoolean.True : DefaultBoolean.False;
			}
		}

		public FormDigital(DigitalLegend digitalLegend)
		{
			InitializeComponent();
			_digitalLegend = digitalLegend;
			_images.AddRange(Core.OnlineSchedule.ListManager.Instance.Images);
			_images.AddRange(Core.AdSchedule.ListManager.Instance.Images);
			gridControlLogoGallery.DataSource = _images;
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
			labelControlWarning.Visible = togglesSelected > 3;
		}

		private void FormDigital_Load(object sender, EventArgs e)
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

			checkEditEnableLogo.Checked = _digitalLegend.Logo != null;
			var selectedLogo = _images.FirstOrDefault(l => l.EncodedBigImage.Equals(_digitalLegend.EncodedLogo));
			if (selectedLogo != null)
			{
				var index = _images.IndexOf(selectedLogo);
				layoutViewLogoGallery.FocusedRowHandle = layoutViewLogoGallery.GetRowHandle(index);
			}
			else
				layoutViewLogoGallery.FocusedRowHandle = 0;

			_loading = false;
		}

		private void FormDigital_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
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

				var selecteImageSource = layoutViewLogoGallery.GetFocusedRow() as ImageSource;
				_digitalLegend.Logo = checkEditEnableLogo.Checked && selecteImageSource != null ? selecteImageSource.BigImage : null;
				_digitalLegend.EncodedLogo = null;
			}
		}

		private void checkEditEnable_CheckedChanged(object sender, EventArgs e)
		{
			pnControls.Enabled = checkEditEnable.Checked;
			hyperLinkEditReset.Enabled = checkEditEnable.Checked;
			if (!_loading)
			{
				if (checkEditEnable.Checked)
				{
					if ((checkEditAllowEdit.Checked || memoEditInfo.EditValue == null) && RequestDefaultInfo != null)
						RequestDefaultInfo(this, new RequestDigitalInfoEventArgs(memoEditInfo, buttonXShowWebsites.Checked, buttonXShowProduct.Checked, buttonXShowDimensions.Checked, buttonXShowDates.Checked, buttonXShowImpressions.Checked, buttonXShowCPM.Checked, buttonXShowInvestment.Checked));
				}
				else
					memoEditInfo.EditValue = null;
			}
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

			if (!_loading && !checkEditAllowEdit.Checked)
				if (RequestDefaultInfo != null)
					RequestDefaultInfo(this, new RequestDigitalInfoEventArgs(memoEditInfo, buttonXShowWebsites.Checked, buttonXShowProduct.Checked, buttonXShowDimensions.Checked, buttonXShowDates.Checked, buttonXShowImpressions.Checked, buttonXShowCPM.Checked, buttonXShowInvestment.Checked));
		}

		private void buttonXShow_CheckedChanged(object sender, EventArgs e)
		{
			UpdateWarning();
			if (!_loading)
			{
				if (RequestDefaultInfo != null)
					RequestDefaultInfo(this, new RequestDigitalInfoEventArgs(memoEditInfo, buttonXShowWebsites.Checked, buttonXShowProduct.Checked, buttonXShowDimensions.Checked, buttonXShowDates.Checked, buttonXShowImpressions.Checked, buttonXShowCPM.Checked, buttonXShowInvestment.Checked));
			}
		}

		private void hyperLinkEditReset_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("All Digital Product Info will be Refreshed") == DialogResult.Yes)
			{
				checkEditAllowEdit.Checked = false;
				if (RequestDefaultInfo != null)
					RequestDefaultInfo(this, new RequestDigitalInfoEventArgs(memoEditInfo, buttonXShowWebsites.Checked, buttonXShowProduct.Checked, buttonXShowDimensions.Checked, buttonXShowDates.Checked, buttonXShowImpressions.Checked, buttonXShowCPM.Checked, buttonXShowInvestment.Checked));
			}
			e.Handled = true;
		}

		private void layoutViewLogoGallery_CustomFieldValueStyle(object sender, DevExpress.XtraGrid.Views.Layout.Events.LayoutViewFieldValueStyleEventArgs e)
		{
			var view = sender as LayoutView;
			if (view.FocusedRowHandle == e.RowHandle)
			{
				e.Appearance.BackColor = Color.Orange;
				e.Appearance.BackColor2 = Color.Orange;
			}
		}

		private void checkEditEnableLogo_CheckedChanged(object sender, EventArgs e)
		{
			gridControlLogoGallery.Enabled = checkEditEnableLogo.Checked;
		}
	}
}
