using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraTab;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;

namespace NewBizWiz.OnlineSchedule.Controls.PresentationClasses
{
	[ToolboxItem(false)]
	public partial class AdPlanDigitalProductControl : XtraTabPage, IAdPlanItem
	//public partial class AdPlanDigitalProductControl : UserControl, IAdPlanItem
	{
		private bool _allowToSave;

		public AdPlanDigitalProductControl()
		{
			InitializeComponent();
		}

		public AdPlanControl Container { get; set; }
		public DigitalProduct DigitalProduct { get; set; }

		public Control SettingsContainer
		{
			get { return pnItemsBorder; }
		}

		public bool SettingsNotSaved
		{
			get { return Container.SettingsNotSaved; }
			set { Container.SettingsNotSaved = value; }
		}

		private bool AllowToCheck()
		{
			int checkedNumber = 0;
			if (checkEditMonthlyImpressions.Checked)
				checkedNumber++;
			if (checkEditMonthlyCPM.Checked)
				checkedNumber++;
			if (checkEditComment1.Checked)
				checkedNumber++;
			if (checkEditComment2.Checked)
				checkedNumber++;
			if (checkEditTotalCPM.Checked)
				checkedNumber++;
			if (checkEditTotalImpressions.Checked)
				checkedNumber++;
			if (checkEditDimensions.Checked)
				checkedNumber++;
			if (checkEditComment3.Checked)
				checkedNumber++;
			if (checkEditWebsites.Checked)
				checkedNumber++;
			return checkedNumber < 6;
		}

		public void LoadProduct()
		{
			_allowToSave = false;
			Text = DigitalProduct.Name.Replace("&", "&&");

			textEditName.EditValue = DigitalProduct.AdPlanSettings.EditName && !String.IsNullOrEmpty(DigitalProduct.AdPlanSettings.Name) ?
				DigitalProduct.AdPlanSettings.Name :
				DigitalProduct.FullName;
			buttonXEditName.Checked = DigitalProduct.AdPlanSettings.EditName;

			var defaultImage = Core.OnlineSchedule.ListManager.Instance.Images.FirstOrDefault(i => i.IsDefault);
			pbLogo.Image = DigitalProduct.AdPlanSettings.Logo ?? (defaultImage != null ? defaultImage.BigImage : null);

			checkEditFlightDates.Text = DigitalProduct.Parent.FlightDates;
			checkEditFlightDates.Checked = DigitalProduct.AdPlanSettings.ShowFlightDates;

			checkEditComments.Checked = DigitalProduct.AdPlanSettings.ShowComments;
			memoEditComments.EditValue = DigitalProduct.AdPlanSettings.Comments;

			checkEditInvestment.Checked = DigitalProduct.AdPlanSettings.ShowInvestment;
			spinEditInvestment.EditValue = DigitalProduct.AdPlanSettings.EditInvestment && DigitalProduct.AdPlanSettings.Investment.HasValue ? DigitalProduct.AdPlanSettings.Investment : (decimal?)DigitalProduct.TotalInvestmentCalculated;
			buttonXEditInvestment.Checked = DigitalProduct.AdPlanSettings.EditInvestment;

			checkEditWebsites.Text = "Active Websites: " + String.Join(", ", DigitalProduct.Websites);
			checkEditWebsites.Visible = !DigitalProduct.Websites.Any();
			checkEditDimensions.Text = !String.IsNullOrEmpty(DigitalProduct.Dimensions) ? ("Dimensions: " + DigitalProduct.Dimensions) : String.Empty;
			checkEditDimensions.Visible = !String.IsNullOrEmpty(DigitalProduct.Dimensions);
			checkEditMonthlyImpressions.Text = DigitalProduct.MonthlyImpressionsCalculated.HasValue ? ("Monthly Impressions: " + DigitalProduct.MonthlyImpressionsCalculated.Value.ToString("#,##0")) : String.Empty;
			checkEditMonthlyImpressions.Visible = DigitalProduct.MonthlyImpressionsCalculated.HasValue;
			checkEditMonthlyCPM.Text = DigitalProduct.MonthlyCPMCalculated.HasValue ? ("Monthly CPM: " + DigitalProduct.MonthlyCPMCalculated.Value.ToString("$#,##0.00")) : String.Empty;
			checkEditMonthlyCPM.Visible = DigitalProduct.MonthlyCPMCalculated.HasValue;
			checkEditTotalImpressions.Text = DigitalProduct.TotalImpressionsCalculated.HasValue ? ("Total Impressions: " + DigitalProduct.TotalImpressionsCalculated.Value.ToString("#,##0")) : String.Empty;
			checkEditTotalImpressions.Visible = DigitalProduct.TotalImpressionsCalculated.HasValue;
			checkEditTotalCPM.Text = DigitalProduct.TotalCPMCalculated.HasValue ? ("Overall CPM: " + DigitalProduct.TotalCPMCalculated.Value.ToString("$#,##0.00")) : String.Empty;
			checkEditTotalCPM.Visible = DigitalProduct.TotalCPMCalculated.HasValue;
			checkEditComment1.Text = DigitalProduct.Strength1;
			checkEditComment1.Visible = !String.IsNullOrEmpty(DigitalProduct.Strength1);
			checkEditComment2.Text = DigitalProduct.Strength2;
			checkEditComment2.Visible = !String.IsNullOrEmpty(DigitalProduct.Strength2);
			checkEditComment3.Text = DigitalProduct.Comment;
			checkEditComment3.Visible = !String.IsNullOrEmpty(DigitalProduct.Comment);
			checkEditWebsites.Checked = DigitalProduct.AdPlanSettings.ShowWebsites & !DigitalProduct.Websites.Any();
			checkEditDimensions.Checked = DigitalProduct.AdPlanSettings.ShowDimensions & !String.IsNullOrEmpty(DigitalProduct.Dimensions);
			checkEditMonthlyImpressions.Checked = DigitalProduct.AdPlanSettings.ShowMonthlyImpressions & DigitalProduct.MonthlyImpressionsCalculated.HasValue;
			checkEditMonthlyCPM.Checked = DigitalProduct.AdPlanSettings.ShowMonthlyImpressions & DigitalProduct.MonthlyCPMCalculated.HasValue;
			checkEditTotalImpressions.Checked = DigitalProduct.AdPlanSettings.ShowTotalImpressions & DigitalProduct.TotalImpressionsCalculated.HasValue;
			checkEditTotalCPM.Checked = DigitalProduct.AdPlanSettings.ShowTotalImpressions & DigitalProduct.TotalCPMCalculated.HasValue;
			checkEditComment1.Checked = DigitalProduct.AdPlanSettings.ShowComment1 & !String.IsNullOrEmpty(DigitalProduct.Strength1);
			checkEditComment2.Checked = DigitalProduct.AdPlanSettings.ShowComment2 & !String.IsNullOrEmpty(DigitalProduct.Strength2);
			checkEditComment3.Checked = DigitalProduct.AdPlanSettings.ShowComment3 & !String.IsNullOrEmpty(DigitalProduct.Comment);

			buttonXNotOutput.Checked = DigitalProduct.AdPlanSettings.NotOutput;
			_allowToSave = true;
		}

		private void buttonXEditName_CheckedChanged(object sender, EventArgs e)
		{
			textEditName.Properties.ReadOnly = !buttonXEditName.Checked;
			if (_allowToSave && !buttonXEditName.Checked)
			{
				textEditName.EditValue = DigitalProduct.Name;
				checkEdit_CheckedChanged(null, null);
			}
		}

		private void pbLogo_Click(object sender, EventArgs e)
		{
			using (var form = new FormImageGallery(Core.OnlineSchedule.ListManager.Instance.Images))
			{
				form.SelectedImage = pbLogo.Image;
				if (form.ShowDialog() == DialogResult.OK && form.SelectedImageSource != null)
				{
					pbLogo.Image = new Bitmap(form.SelectedImage);
					checkEdit_CheckedChanged(null, null);
				}
			}
		}

		private void checkEditComments_CheckedChanged(object sender, EventArgs e)
		{
			memoEditComments.Enabled = checkEditComments.Checked;
			checkEdit_CheckedChanged(null, null);
		}

		private void checkEditInvestment_CheckedChanged(object sender, EventArgs e)
		{
			spinEditInvestment.Enabled = checkEditInvestment.Checked;
			buttonXEditInvestment.Enabled = checkEditInvestment.Checked;
			checkEdit_CheckedChanged(null, null);
		}

		private void buttonXEditInvestment_CheckedChanged(object sender, EventArgs e)
		{
			spinEditInvestment.Properties.ReadOnly = !buttonXEditInvestment.Checked;
			if (_allowToSave && !buttonXEditInvestment.Checked)
			{
				spinEditInvestment.EditValue = (decimal?)DigitalProduct.TotalInvestmentCalculated;
				checkEdit_CheckedChanged(null, null);
			}
		}

		private void checkEditAdItems_EditValueChanging(object sender, ChangingEventArgs e)
		{
			if (_allowToSave)
			{
				if ((bool)e.NewValue)
				{
					if (!AllowToCheck())
					{
						Utilities.Instance.ShowWarning("You may select only up to 6 Ad-Items");
						e.Cancel = true;
					}
				}
			}
		}

		private void checkEdit_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				DigitalProduct.AdPlanSettings.EditName = buttonXEditName.Checked;
				DigitalProduct.AdPlanSettings.EditInvestment = buttonXEditInvestment.Checked;

				DigitalProduct.AdPlanSettings.ShowFlightDates = checkEditFlightDates.Checked;
				DigitalProduct.AdPlanSettings.ShowComments = checkEditComments.Checked;
				DigitalProduct.AdPlanSettings.ShowInvestment = checkEditInvestment.Checked;

				DigitalProduct.AdPlanSettings.ShowWebsites = checkEditWebsites.Checked;
				DigitalProduct.AdPlanSettings.ShowDimensions = checkEditDimensions.Checked;
				DigitalProduct.AdPlanSettings.ShowMonthlyImpressions = checkEditMonthlyImpressions.Checked;
				DigitalProduct.AdPlanSettings.ShowMonthlyImpressions = checkEditMonthlyCPM.Checked;
				DigitalProduct.AdPlanSettings.ShowTotalImpressions = checkEditTotalImpressions.Checked;
				DigitalProduct.AdPlanSettings.ShowTotalImpressions = checkEditTotalCPM.Checked;
				DigitalProduct.AdPlanSettings.ShowComment1 = checkEditComment1.Checked;
				DigitalProduct.AdPlanSettings.ShowComment2 = checkEditComment2.Checked;
				DigitalProduct.AdPlanSettings.ShowComment3 = checkEditComment3.Checked;

				DigitalProduct.AdPlanSettings.NotOutput = buttonXNotOutput.Checked;

				DigitalProduct.AdPlanSettings.Name = buttonXEditName.Checked && textEditName.EditValue != null ? textEditName.EditValue.ToString() : null;
				DigitalProduct.AdPlanSettings.Logo = pbLogo.Image;
				DigitalProduct.AdPlanSettings.Comments = checkEditComments.Checked && memoEditComments.EditValue != null ? memoEditComments.EditValue.ToString() : null;
				DigitalProduct.AdPlanSettings.Investment = checkEditInvestment.Checked && buttonXEditInvestment.Checked ? spinEditInvestment.Value : (decimal?)null;
				SettingsNotSaved = true;
			}
		}

		private void checkEdit_MouseDown(object sender, MouseEventArgs e)
		{
			var cEdit = (CheckEdit)sender;
			var cInfo = (CheckEditViewInfo)cEdit.GetViewInfo();
			var r = cInfo.CheckInfo.GlyphRect;
			var editorRect = new Rectangle(new Point(0, 0), cEdit.Size);
			if (!r.Contains(e.Location) && editorRect.Contains(e.Location))
				((DXMouseEventArgs)e).Handled = true;
		}

		private void buttonXItemsClear_Click(object sender, EventArgs e)
		{
			checkEditWebsites.Checked =
			checkEditDimensions.Checked =
			checkEditMonthlyImpressions.Checked =
			checkEditMonthlyCPM.Checked =
			checkEditTotalImpressions.Checked =
			checkEditTotalCPM.Checked =
			checkEditComment1.Checked =
			checkEditComment2.Checked =
			checkEditComment3.Checked = false;
		}

		private void buttonXItemsDefault_Click(object sender, EventArgs e)
		{
			DigitalProduct.AdPlanSettings.ResetItemsToDefault();
			_allowToSave = false;
			checkEditWebsites.Checked = DigitalProduct.AdPlanSettings.ShowWebsites & !DigitalProduct.Websites.Any();
			checkEditDimensions.Checked = DigitalProduct.AdPlanSettings.ShowDimensions & !String.IsNullOrEmpty(DigitalProduct.Dimensions);
			checkEditMonthlyImpressions.Checked = DigitalProduct.AdPlanSettings.ShowMonthlyImpressions & DigitalProduct.MonthlyImpressionsCalculated.HasValue;
			checkEditMonthlyCPM.Checked = DigitalProduct.AdPlanSettings.ShowMonthlyImpressions & DigitalProduct.MonthlyCPMCalculated.HasValue;
			checkEditTotalImpressions.Checked = DigitalProduct.AdPlanSettings.ShowTotalImpressions & DigitalProduct.TotalImpressionsCalculated.HasValue;
			checkEditTotalCPM.Checked = DigitalProduct.AdPlanSettings.ShowTotalImpressions & DigitalProduct.TotalCPMCalculated.HasValue;
			checkEditComment1.Checked = DigitalProduct.AdPlanSettings.ShowComment1 & !String.IsNullOrEmpty(DigitalProduct.Strength1);
			checkEditComment2.Checked = DigitalProduct.AdPlanSettings.ShowComment2 & !String.IsNullOrEmpty(DigitalProduct.Strength2);
			checkEditComment3.Checked = DigitalProduct.AdPlanSettings.ShowComment3 & !String.IsNullOrEmpty(DigitalProduct.Comment);
			_allowToSave = true;
			SettingsNotSaved = true;
		}

		private void buttonXNotOutput_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
				Container.UpdateSlidesNumberSelector();
			checkEdit_CheckedChanged(null, null);
		}

		#region Output Staff

		public string Product
		{
			get { return textEditName.EditValue != null ? textEditName.EditValue.ToString() : String.Empty; }
		}

		public string LogoFile
		{
			get
			{
				if (pbLogo.Image != null)
				{
					string fileName = Path.GetTempFileName();
					pbLogo.Image.Save(fileName);
					return fileName;
				}
				return String.Empty;
			}
		}

		public string Details
		{
			get
			{
				var details = new List<string>();
				if (checkEditFlightDates.Checked)
					details.Add(checkEditFlightDates.Text);
				if (checkEditWebsites.Checked)
					details.Add(checkEditWebsites.Text);
				if (checkEditDimensions.Checked && !String.IsNullOrEmpty(checkEditDimensions.Text))
					details.Add(checkEditDimensions.Text);
				if (checkEditMonthlyImpressions.Checked && !String.IsNullOrEmpty(checkEditMonthlyImpressions.Text))
					details.Add(checkEditMonthlyImpressions.Text);
				if (checkEditMonthlyCPM.Checked && !String.IsNullOrEmpty(checkEditMonthlyCPM.Text))
					details.Add(checkEditMonthlyCPM.Text);
				if (checkEditTotalImpressions.Checked && !String.IsNullOrEmpty(checkEditTotalImpressions.Text))
					details.Add(checkEditTotalImpressions.Text);
				if (checkEditTotalCPM.Checked && !String.IsNullOrEmpty(checkEditTotalCPM.Text))
					details.Add(checkEditTotalCPM.Text);
				if (checkEditComment1.Checked && !String.IsNullOrEmpty(checkEditComment1.Text))
					details.Add(checkEditComment1.Text);
				if (checkEditComment2.Checked && !String.IsNullOrEmpty(checkEditComment2.Text))
					details.Add(checkEditComment2.Text);
				if (checkEditComment3.Checked && !String.IsNullOrEmpty(checkEditComment3.Text))
					details.Add(checkEditComment3.Text);
				if (checkEditComments.Checked && memoEditComments.EditValue != null)
					details.Add(memoEditComments.EditValue.ToString());
				return String.Join(", ", details.ToArray());
			}
		}

		public decimal? Investment
		{
			get { return checkEditInvestment.Checked && spinEditInvestment.EditValue != null ? spinEditInvestment.EditValue as Decimal? : null; }
		}

		public string InvestmentFormatted
		{
			get { return Investment.HasValue ? Investment.Value.ToString("$#,##0.00") : String.Empty; }
		}

		public bool NotOutput
		{
			get { return buttonXNotOutput.Checked; }
		}
		#endregion
	}
}