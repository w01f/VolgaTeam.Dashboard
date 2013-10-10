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
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls.AdPlan
{
	[ToolboxItem(false)]
	public partial class AdPlanPrintProductControl : XtraTabPage, IAdPlanItem
	//public partial class AdPlanPrintProductControl : UserControl, IAdPlanItem
	{
		private bool _allowToSave;

		public AdPlanPrintProductControl()
		{
			InitializeComponent();
		}

		public AdPlanControl Container { get; set; }
		public PrintProduct PrintProduct { get; set; }

		public bool SettingsNotSaved
		{
			get { return Container.SettingsNotSaved; }
			set { Container.SettingsNotSaved = value; }
		}

		private bool AllowToCheck()
		{
			int checkedNumber = 0;
			if (checkEditPageSize.Checked)
				checkedNumber++;
			if (checkEditPercentOfPage.Checked)
				checkedNumber++;
			if (checkEditAvgAdCost.Checked)
				checkedNumber++;
			if (checkEditAvgFinalCost.Checked)
				checkedNumber++;
			if (checkEditAvgPCI.Checked)
				checkedNumber++;
			if (checkEditColor.Checked)
				checkedNumber++;
			if (checkEditSquare.Checked)
				checkedNumber++;
			if (checkEditDimensions.Checked)
				checkedNumber++;
			if (checkEditDiscounts.Checked)
				checkedNumber++;
			if (checkEditMechanicals.Checked)
				checkedNumber++;
			if (checkEditSections.Checked)
				checkedNumber++;
			if (checkEditTotalAds.Checked)
				checkedNumber++;
			if (checkEditTotalSquare.Checked)
				checkedNumber++;
			return checkedNumber < 6;
		}

		public void LoadProduct()
		{
			_allowToSave = false;
			Text = PrintProduct.Name.Replace("&", "&&");

			textEditName.EditValue = PrintProduct.ViewSettings.AdPlanSettings.EditName && !String.IsNullOrEmpty(PrintProduct.ViewSettings.AdPlanSettings.Name) ? PrintProduct.ViewSettings.AdPlanSettings.Name : PrintProduct.Name;
			buttonXEditName.Checked = PrintProduct.ViewSettings.AdPlanSettings.EditName;

			var defaultImage = PrintProduct.SmallLogo != null ? new Bitmap(PrintProduct.SmallLogo) : null;
			pbLogo.Image = PrintProduct.ViewSettings.AdPlanSettings.Logo != null ? PrintProduct.ViewSettings.AdPlanSettings.Logo : defaultImage;

			checkEditFlightDates.Text = PrintProduct.Parent.FlightDateStart.ToString("MM/dd/yy") + " - " + PrintProduct.Parent.FlightDateEnd.ToString("MM/dd/yy");
			checkEditFlightDates.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowFlightDates;

			checkEditDates.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowDates;
			memoEditDates.EditValue = PrintProduct.ViewSettings.AdPlanSettings.EditDates && !String.IsNullOrEmpty(PrintProduct.ViewSettings.AdPlanSettings.Dates) ? PrintProduct.ViewSettings.AdPlanSettings.Dates : PrintProduct.InsertDates;
			buttonXEditDates.Checked = PrintProduct.ViewSettings.AdPlanSettings.EditDates;

			checkEditComments.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowComments;
			memoEditComments.EditValue = PrintProduct.ViewSettings.AdPlanSettings.Comments;

			checkEditInvestment.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowInvestment;
			spinEditInvestment.EditValue = PrintProduct.ViewSettings.AdPlanSettings.EditInvestment && PrintProduct.ViewSettings.AdPlanSettings.Investment.HasValue ? PrintProduct.ViewSettings.AdPlanSettings.Investment : (decimal?)PrintProduct.TotalFinalRate;
			buttonXEditInvestment.Checked = PrintProduct.ViewSettings.AdPlanSettings.EditInvestment;

			checkEditTotalAds.Text = "Total Ads: " + PrintProduct.TotalInserts.ToString("#,##0");
			checkEditTotalSquare.Text = PrintProduct.TotalSquare.HasValue && PrintProduct.AdPricingStrategy != AdPricingStrategies.SharePage ? ("Total Column Inches: " + PrintProduct.TotalSquare.Value.ToString("#,##0.00#")) : string.Empty;
			checkEditTotalSquare.Visible = PrintProduct.TotalSquare.HasValue && PrintProduct.AdPricingStrategy != AdPricingStrategies.SharePage;
			checkEditPageSize.Text = !string.IsNullOrEmpty(PrintProduct.SizeOptions.PageSize) ? ("Page Size: " + PrintProduct.SizeOptions.PageSize) : string.Empty;
			checkEditPageSize.Visible = !string.IsNullOrEmpty(PrintProduct.SizeOptions.PageSize);
			checkEditPercentOfPage.Text = !string.IsNullOrEmpty(PrintProduct.SizeOptions.PercentOfPage) ? (PrintProduct.SizeOptions.PercentOfPage + " Share of Page") : string.Empty;
			checkEditPercentOfPage.Visible = !string.IsNullOrEmpty(PrintProduct.SizeOptions.PercentOfPage);
			checkEditAvgAdCost.Text = "BW Avg Ad Cost: " + PrintProduct.AvgADRate.ToString("$#,##0.00");
			checkEditAvgFinalCost.Text = "Final Avg Ad Cost: " + PrintProduct.AvgFinalRate.ToString("$#,##0.00");
			checkEditAvgPCI.Text = PrintProduct.AvgPCIRate > 0 ? ("Avg PCI: " + PrintProduct.AvgPCIRate.ToString("$#,##0.00")) : string.Empty;
			checkEditAvgPCI.Visible = PrintProduct.AvgPCIRate > 0;
			checkEditSquare.Text = PrintProduct.SizeOptions.Square.HasValue && PrintProduct.AdPricingStrategy != AdPricingStrategies.SharePage ? ("Column Inches: " + PrintProduct.SizeOptions.Square.Value.ToString("#,##0.00#") + " col. in.") : string.Empty;
			checkEditSquare.Visible = PrintProduct.SizeOptions.Square.HasValue && PrintProduct.AdPricingStrategy != AdPricingStrategies.SharePage;
			checkEditDimensions.Text = !string.IsNullOrEmpty(PrintProduct.SizeOptions.Dimensions) ? ("Dimensions: " + PrintProduct.SizeOptions.Dimensions) : string.Empty;
			checkEditDimensions.Visible = !string.IsNullOrEmpty(PrintProduct.SizeOptions.Dimensions);
			checkEditDiscounts.Text = "Discounts: " + PrintProduct.TotalDiscountRate.ToString("$#,##0.00");
			labelControlSections.Text = "Sections: " + string.Join(", ", PrintProduct.Inserts.Where(x => !string.IsNullOrEmpty(x.FullSection)).Select(x => x.FullSection).Distinct());
			switch (PrintProduct.ColorOption)
			{
				case ColorOptions.BlackWhite:
					checkEditColor.Text = "Black && White";
					break;
				case ColorOptions.SpotColor:
					checkEditColor.Text = "Spot Color";
					break;
				case ColorOptions.FullColor:
					checkEditColor.Text = "Full Color";
					break;
			}
			checkEditTotalAds.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowTotalInserts;
			checkEditDimensions.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowDimensions & !string.IsNullOrEmpty(PrintProduct.SizeOptions.Dimensions);
			checkEditPageSize.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowPageSize & !string.IsNullOrEmpty(PrintProduct.SizeOptions.PageSize);
			checkEditPercentOfPage.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowPercentOfPage & !string.IsNullOrEmpty(PrintProduct.SizeOptions.PercentOfPage);
			checkEditColor.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowTotalColor;
			checkEditAvgAdCost.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowAvgAdCost;
			checkEditAvgFinalCost.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowAvgFinalCost;
			checkEditDiscounts.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowDiscounts;
			checkEditSections.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowSection;
			checkEditAvgPCI.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowAvgPCI & PrintProduct.AvgPCIRate > 0;
			checkEditTotalSquare.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowTotalSquare & PrintProduct.TotalSquare.HasValue && PrintProduct.AdPricingStrategy != AdPricingStrategies.SharePage;
			checkEditSquare.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowSquare & PrintProduct.SizeOptions.Square.HasValue && PrintProduct.AdPricingStrategy != AdPricingStrategies.SharePage;
			checkEditMechanicals.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowMechanicals;

			buttonXNotOutput.Checked = PrintProduct.ViewSettings.AdPlanSettings.NotOutput;
			_allowToSave = true;
		}

		private void buttonXEditName_CheckedChanged(object sender, EventArgs e)
		{
			textEditName.Properties.ReadOnly = !buttonXEditName.Checked;
			if (_allowToSave && !buttonXEditName.Checked)
			{
				textEditName.EditValue = PrintProduct.Name;
				checkEdit_CheckedChanged(null, null);
			}
		}

		private void pbLogo_Click(object sender, EventArgs e)
		{
			using (var form = new FormImageGallery(Core.AdSchedule.ListManager.Instance.Images))
			{
				form.SelectedImage = pbLogo.Image;
				if (form.ShowDialog() == DialogResult.OK && form.SelectedImageSource != null)
				{
					pbLogo.Image = new Bitmap(form.SelectedImage);
					checkEdit_CheckedChanged(null, null);
				}
			}
		}

		private void checkEditDates_CheckedChanged(object sender, EventArgs e)
		{
			memoEditDates.Enabled = checkEditDates.Checked;
			buttonXEditDates.Enabled = checkEditDates.Checked;
			checkEdit_CheckedChanged(null, null);
		}

		private void buttonXEditDates_CheckedChanged(object sender, EventArgs e)
		{
			memoEditDates.Properties.ReadOnly = !buttonXEditDates.Checked;
			if (_allowToSave && !buttonXEditDates.Checked)
			{
				memoEditDates.EditValue = PrintProduct.InsertDates;
				checkEdit_CheckedChanged(null, null);
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
				spinEditInvestment.EditValue = (decimal?)PrintProduct.TotalFinalRate;
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
				PrintProduct.ViewSettings.AdPlanSettings.EditName = buttonXEditName.Checked;
				PrintProduct.ViewSettings.AdPlanSettings.EditDates = buttonXEditDates.Checked;
				PrintProduct.ViewSettings.AdPlanSettings.EditInvestment = buttonXEditInvestment.Checked;

				PrintProduct.ViewSettings.AdPlanSettings.ShowFlightDates = checkEditFlightDates.Checked;
				PrintProduct.ViewSettings.AdPlanSettings.ShowDates = checkEditDates.Checked;
				PrintProduct.ViewSettings.AdPlanSettings.ShowComments = checkEditComments.Checked;
				PrintProduct.ViewSettings.AdPlanSettings.ShowInvestment = checkEditInvestment.Checked;

				PrintProduct.ViewSettings.AdPlanSettings.ShowPageSize = checkEditPageSize.Checked;
				PrintProduct.ViewSettings.AdPlanSettings.ShowPercentOfPage = checkEditPercentOfPage.Checked;
				PrintProduct.ViewSettings.AdPlanSettings.ShowAvgAdCost = checkEditAvgAdCost.Checked;
				PrintProduct.ViewSettings.AdPlanSettings.ShowAvgFinalCost = checkEditAvgFinalCost.Checked;
				PrintProduct.ViewSettings.AdPlanSettings.ShowAvgPCI = checkEditAvgPCI.Checked;
				PrintProduct.ViewSettings.AdPlanSettings.ShowSquare = checkEditSquare.Checked;
				PrintProduct.ViewSettings.AdPlanSettings.ShowDimensions = checkEditDimensions.Checked;
				PrintProduct.ViewSettings.AdPlanSettings.ShowDiscounts = checkEditDiscounts.Checked;
				PrintProduct.ViewSettings.AdPlanSettings.ShowMechanicals = checkEditMechanicals.Checked;
				PrintProduct.ViewSettings.AdPlanSettings.ShowSection = checkEditSections.Checked;
				PrintProduct.ViewSettings.AdPlanSettings.ShowTotalColor = checkEditColor.Checked;
				PrintProduct.ViewSettings.AdPlanSettings.ShowTotalInserts = checkEditTotalAds.Checked;
				PrintProduct.ViewSettings.AdPlanSettings.ShowTotalSquare = checkEditTotalSquare.Checked;

				PrintProduct.ViewSettings.AdPlanSettings.NotOutput = buttonXNotOutput.Checked;

				PrintProduct.ViewSettings.AdPlanSettings.Name = buttonXEditName.Checked && textEditName.EditValue != null ? textEditName.EditValue.ToString() : null;
				PrintProduct.ViewSettings.AdPlanSettings.Logo = pbLogo.Image;
				PrintProduct.ViewSettings.AdPlanSettings.Dates = checkEditDates.Checked && buttonXEditDates.Checked && memoEditDates.EditValue != null ? memoEditDates.EditValue.ToString() : null;
				PrintProduct.ViewSettings.AdPlanSettings.Comments = checkEditComments.Checked && memoEditComments.EditValue != null ? memoEditComments.EditValue.ToString() : null;
				PrintProduct.ViewSettings.AdPlanSettings.Investment = checkEditInvestment.Checked && buttonXEditInvestment.Checked ? spinEditInvestment.Value : (decimal?)null;
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
			checkEditTotalAds.Checked =
			checkEditDimensions.Checked =
			checkEditPageSize.Checked =
			checkEditPercentOfPage.Checked =
			checkEditColor.Checked =
			checkEditAvgAdCost.Checked =
			checkEditAvgFinalCost.Checked =
			checkEditDiscounts.Checked =
			checkEditSections.Checked =
			checkEditAvgPCI.Checked =
			checkEditTotalSquare.Checked =
			checkEditSquare.Checked =
			checkEditMechanicals.Checked = false;
		}

		private void buttonXItemsDefault_Click(object sender, EventArgs e)
		{
			PrintProduct.ViewSettings.AdPlanSettings.ResetItemsToDefault();
			_allowToSave = false;
			checkEditTotalAds.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowTotalInserts;
			checkEditDimensions.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowDimensions & !string.IsNullOrEmpty(PrintProduct.SizeOptions.Dimensions);
			checkEditPageSize.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowPageSize & !string.IsNullOrEmpty(PrintProduct.SizeOptions.PageSize);
			checkEditPercentOfPage.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowPercentOfPage & !string.IsNullOrEmpty(PrintProduct.SizeOptions.PercentOfPage);
			checkEditColor.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowTotalColor;
			checkEditAvgAdCost.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowAvgAdCost;
			checkEditAvgFinalCost.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowAvgFinalCost;
			checkEditDiscounts.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowDiscounts;
			checkEditSections.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowSection;
			checkEditAvgPCI.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowAvgPCI & PrintProduct.AvgPCIRate > 0;
			checkEditTotalSquare.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowTotalSquare & PrintProduct.TotalSquare.HasValue && PrintProduct.AdPricingStrategy != AdPricingStrategies.SharePage;
			checkEditSquare.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowSquare & PrintProduct.SizeOptions.Square.HasValue && PrintProduct.AdPricingStrategy != AdPricingStrategies.SharePage;
			checkEditMechanicals.Checked = PrintProduct.ViewSettings.AdPlanSettings.ShowMechanicals;
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
				if (checkEditTotalAds.Checked)
					details.Add(checkEditTotalAds.Text);
				if (checkEditTotalSquare.Checked && !string.IsNullOrEmpty(checkEditTotalSquare.Text))
					details.Add(checkEditTotalSquare.Text);
				if (checkEditSquare.Checked && !string.IsNullOrEmpty(checkEditSquare.Text))
					details.Add(checkEditSquare.Text);
				if (checkEditDimensions.Checked && !string.IsNullOrEmpty(checkEditDimensions.Text))
					details.Add(checkEditDimensions.Text);
				if (checkEditPageSize.Checked && !string.IsNullOrEmpty(checkEditPageSize.Text))
					details.Add(checkEditPageSize.Text);
				if (checkEditPercentOfPage.Checked && !string.IsNullOrEmpty(checkEditPercentOfPage.Text))
					details.Add(checkEditPercentOfPage.Text);
				if (checkEditColor.Checked)
					details.Add(checkEditColor.Text.Replace("&&", "&"));
				if (checkEditAvgPCI.Checked)
					details.Add(checkEditAvgPCI.Text);
				if (checkEditAvgAdCost.Checked)
					details.Add(checkEditAvgAdCost.Text);
				if (checkEditAvgFinalCost.Checked)
					details.Add(checkEditAvgFinalCost.Text);
				if (checkEditDiscounts.Checked)
					details.Add(checkEditDiscounts.Text);
				if (checkEditMechanicals.Checked)
					details.Add(checkEditMechanicals.Text);
				if (checkEditSections.Checked)
					details.Add(labelControlSections.Text);
				if (checkEditDates.Checked && memoEditDates.EditValue != null)
					details.Add(memoEditDates.EditValue.ToString());
				if (checkEditComments.Checked && memoEditComments.EditValue != null)
					details.Add(memoEditComments.EditValue.ToString());
				return String.Join(", ", details.ToArray());
			}
		}

		public string Investment
		{
			get { return checkEditInvestment.Checked ? spinEditInvestment.Value.ToString("$#,##0.00") : String.Empty; }
		}

		public bool NotOutput
		{
			get { return buttonXNotOutput.Checked; }
		}
		#endregion
	}
}