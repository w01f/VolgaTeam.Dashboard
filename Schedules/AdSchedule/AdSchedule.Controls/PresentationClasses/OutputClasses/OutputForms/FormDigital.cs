using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using NewBizWiz.Core.AdSchedule;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputForms
{
	public partial class FormDigital : Form
	{
		private readonly DigitalLegend _digitalLegend;
		public event EventHandler<RequestDigitalInfoEventArgs> WebsiteRequestDefault;
		public event EventHandler<RequestDigitalInfoEventArgs> SimpleDigitalInfoRequestDefault;
		public event EventHandler<RequestDigitalInfoEventArgs> DetailedDigitalInfoRequestDefault;

		public FormDigital(DigitalLegend digitalLegend)
		{
			InitializeComponent();
			_digitalLegend = digitalLegend;
		}

		private void checkEditEnable_CheckedChanged(object sender, EventArgs e)
		{
			memoEditWebsite.Enabled = checkEditEnable.Checked;
			memoEditProductInfo.Enabled = checkEditEnable.Checked;
			if (checkEditEnable.Checked)
			{
				if (memoEditWebsite.EditValue == null && WebsiteRequestDefault != null)
					WebsiteRequestDefault(this, new RequestDigitalInfoEventArgs(memoEditWebsite));
			}
			else
				memoEditWebsite.EditValue = null;
			if (checkEditEnable.Checked)
			{
				if (memoEditProductInfo.EditValue == null && SimpleDigitalInfoRequestDefault != null)
					SimpleDigitalInfoRequestDefault(this, new RequestDigitalInfoEventArgs(memoEditProductInfo));
			}
			else
				memoEditProductInfo.EditValue = null;
		}

		private void buttonXGetWebsites_Click(object sender, EventArgs e)
		{
			if (WebsiteRequestDefault != null)
				WebsiteRequestDefault(this, new RequestDigitalInfoEventArgs(memoEditWebsite));
		}

		private void buttonXGetSimpleInfo_Click(object sender, EventArgs e)
		{
			if (SimpleDigitalInfoRequestDefault != null)
				SimpleDigitalInfoRequestDefault(this, new RequestDigitalInfoEventArgs(memoEditProductInfo));
		}

		private void buttonXGetDetailedInfo_Click(object sender, EventArgs e)
		{
			if (DetailedDigitalInfoRequestDefault != null)
				DetailedDigitalInfoRequestDefault(this, new RequestDigitalInfoEventArgs(memoEditProductInfo));
		}

		private void FormDigital_Load(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(_digitalLegend.Websites))
				memoEditWebsite.EditValue = _digitalLegend.Websites;
			if (!String.IsNullOrEmpty(_digitalLegend.Info))
				memoEditProductInfo.EditValue = _digitalLegend.Info;
			checkEditEnable.Checked = _digitalLegend.Enabled;
			if (checkEditEnable.Checked && memoEditWebsite.EditValue == null && memoEditProductInfo.EditValue == null)
			{
				if (WebsiteRequestDefault != null)
					WebsiteRequestDefault(this, new RequestDigitalInfoEventArgs(memoEditWebsite));
				if (SimpleDigitalInfoRequestDefault != null)
					SimpleDigitalInfoRequestDefault(this, new RequestDigitalInfoEventArgs(memoEditProductInfo));
			}
		}

		private void FormDigital_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				_digitalLegend.Enabled = checkEditEnable.Checked;
				_digitalLegend.Websites = memoEditWebsite.EditValue != null ? memoEditWebsite.EditValue.ToString() : String.Empty;
				_digitalLegend.Info = memoEditProductInfo.EditValue != null ? memoEditProductInfo.EditValue.ToString() : String.Empty;
			}
		}
	}

	public class RequestDigitalInfoEventArgs : EventArgs
	{
		public BaseEdit Editor { get; private set; }

		public RequestDigitalInfoEventArgs(BaseEdit editor)
		{
			Editor = editor;
		}
	}
}
