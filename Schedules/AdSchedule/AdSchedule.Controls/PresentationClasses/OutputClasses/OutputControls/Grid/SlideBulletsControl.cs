using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using NewBizWiz.Core.Common;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	[ToolboxItem(false)]
	public partial class SlideBulletsControl : UserControl
	{
		private readonly IGridOutputControl _settingsContainer;
		private bool _allowToCheckSlideOptions;
		private bool _allowToSave;

		#region Slide Options
		public string TotalInserts
		{
			get { return checkEditTotalInserts.Checked ? checkEditTotalInserts.Text.Replace("<b>", string.Empty).Replace("</b>", string.Empty) : string.Empty; }
			set { checkEditTotalInserts.Text = "Total Ads: <b>" + value + "</b>"; }
		}

		public string PageSize
		{
			get { return checkEditPageSize.Checked ? checkEditPageSize.Text.Replace("<b>", string.Empty).Replace("</b>", string.Empty) : string.Empty; }
			set { checkEditPageSize.Text = "Page Size: <b>" + value + "</b>"; }
		}

		public string PercentOfPage
		{
			get { return checkEditPercentOfPage.Checked ? checkEditPercentOfPage.Text.Replace("<b>", string.Empty).Replace("</b>", string.Empty) : string.Empty; }
			set { checkEditPercentOfPage.Text = "% of Page: <b>" + value + "</b>"; }
		}

		public string Dimensions
		{
			get { return checkEditDimensions.Checked ? checkEditDimensions.Text.Replace("<b>", string.Empty).Replace("</b>", string.Empty) : string.Empty; }
			set { checkEditDimensions.Text = "Col. x Inches: <b>" + value + "</b>"; }
		}

		public string ColumnInches
		{
			get { return checkEditColumnInches.Checked ? checkEditColumnInches.Text.Replace("<b>", string.Empty).Replace("</b>", string.Empty) : string.Empty; }
			set { checkEditColumnInches.Text = "Total Col. In.: <b>" + value + "</b>"; }
		}

		public string AvgAdCost
		{
			get { return checkEditAvgAdCost.Checked ? checkEditAvgAdCost.Text.Replace("<b>", string.Empty).Replace("</b>", string.Empty) : string.Empty; }
			set { checkEditAvgAdCost.Text = "BW Ad Cost: <b>" + value + "</b>"; }
		}

		public string AvgFinalCost
		{
			get { return checkEditAvgFinalCost.Checked ? checkEditAvgFinalCost.Text.Replace("<b>", string.Empty).Replace("</b>", string.Empty) : string.Empty; }
			set { checkEditAvgFinalCost.Text = "Final Ad Cost: <b>" + value + "</b>"; }
		}

		public string AvgPCI
		{
			get { return checkEditAvgPCI.Checked ? checkEditAvgPCI.Text.Replace("<b>", string.Empty).Replace("</b>", string.Empty) : string.Empty; }
			set { checkEditAvgPCI.Text = "Avg PCI: <b>" + value + "</b>"; }
		}

		public string Delivery
		{
			get { return checkEditDelivery.Checked ? checkEditDelivery.Text.Replace("<b>", string.Empty).Replace("</b>", string.Empty) : string.Empty; }
			set { checkEditDelivery.Text = "Delivery: <b>" + value + "</b>"; }
		}

		public string Readership
		{
			get { return checkEditReadership.Checked ? checkEditReadership.Text.Replace("<b>", string.Empty).Replace("</b>", string.Empty) : string.Empty; }
			set { checkEditReadership.Text = "Readership: <b>" + value + "</b>"; }
		}

		public string TotalColor
		{
			get { return checkEditTotalColor.Checked ? checkEditTotalColor.Text.Replace("<b>", string.Empty).Replace("</b>", string.Empty) : string.Empty; }
			set { checkEditTotalColor.Text = "Total Color: <b>" + value + "</b>"; }
		}

		public string Discounts
		{
			get { return checkEditTotalDiscounts.Checked ? checkEditTotalDiscounts.Text.Replace("<b>", string.Empty).Replace("</b>", string.Empty) : string.Empty; }
			set
			{
				if (value.Equals("$.00"))
					checkEditTotalDiscounts.Checked = false;
				checkEditTotalDiscounts.Text = "Discounts: <b>" + value + "</b>";
			}
		}

		public string TotalFinalCost
		{
			get { return checkEditTotalFinalCost.Checked ? checkEditTotalFinalCost.Text.Replace("<b>", string.Empty).Replace("</b>", string.Empty) : string.Empty; }
			set { checkEditTotalFinalCost.Text = "Investment: <b>" + value + "</b>"; }
		}

		public string TotalSquare
		{
			get { return checkEditTotalSquare.Checked ? checkEditTotalSquare.Text.Replace("<b>", string.Empty).Replace("</b>", string.Empty) : string.Empty; }
			set { checkEditTotalSquare.Text = "Total Inches: <b>" + value + "</b>"; }
		}
		#endregion

		public SlideBulletsControl(IGridOutputControl settingsContainer)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			_settingsContainer = settingsContainer;

			if ((base.CreateGraphics()).DpiX > 96)
			{
				checkEditEnableSlideBullets.Font = new Font(checkEditEnableSlideBullets.Font.FontFamily, checkEditEnableSlideBullets.Font.Size - 3, checkEditEnableSlideBullets.Font.Style);
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;
				checkEditAllSlides.Font = font;
				checkEditLastSlide.Font = font;
				checkEditAvgAdCost.Font = font;
				checkEditAvgFinalCost.Font = font;
				checkEditAvgPCI.Font = font;
				checkEditColumnInches.Font = font;
				checkEditDelivery.Font = font;
				checkEditDimensions.Font = font;
				checkEditPageSize.Font = font;
				checkEditPercentOfPage.Font = font;
				checkEditReadership.Font = font;
				checkEditSignature.Font = font;
				checkEditTotalColor.Font = font;
				checkEditTotalDiscounts.Font = font;
				checkEditTotalFinalCost.Font = font;
				checkEditTotalInserts.Font = font;
				checkEditTotalSquare.Font = font;
			}
		}

		private bool AllowToSelectSlideOptions()
		{
			int count = 0;
			if (checkEditTotalInserts.Checked)
				count++;
			if (checkEditPageSize.Checked)
				count++;
			if (checkEditPercentOfPage.Checked)
				count++;
			if (checkEditDimensions.Checked)
				count++;
			if (checkEditColumnInches.Checked)
				count++;
			if (checkEditAvgAdCost.Checked)
				count++;
			if (checkEditAvgFinalCost.Checked)
				count++;
			if (checkEditAvgPCI.Checked)
				count++;
			if (checkEditDelivery.Checked)
				count++;
			if (checkEditReadership.Checked)
				count++;
			if (checkEditTotalColor.Checked)
				count++;
			if (checkEditTotalDiscounts.Checked)
				count++;
			if (checkEditTotalFinalCost.Checked)
				count++;
			if (checkEditTotalSquare.Checked)
				count++;
			return count < 6;
		}

		public void LoadSlideBullets()
		{
			_allowToCheckSlideOptions = false;
			_allowToSave = false;

			checkEditEnableSlideBullets.Enabled = _settingsContainer.SlideBulletsState.EnableSlideBullets;
			checkEditAvgAdCost.Enabled = _settingsContainer.SlideBulletsState.EnableAvgAdCost;
			checkEditAvgFinalCost.Enabled = _settingsContainer.SlideBulletsState.EnableAvgFinalCost;
			checkEditAvgPCI.Enabled = _settingsContainer.SlideBulletsState.EnableAvgPCI;
			checkEditColumnInches.Enabled = _settingsContainer.SlideBulletsState.EnableColumnInches;
			checkEditDelivery.Enabled = _settingsContainer.SlideBulletsState.EnableDelivery;
			checkEditDimensions.Enabled = _settingsContainer.SlideBulletsState.EnableDimensions;
			checkEditPageSize.Enabled = _settingsContainer.SlideBulletsState.EnablePageSize;
			checkEditPercentOfPage.Enabled = _settingsContainer.SlideBulletsState.EnablePercentOfPage;
			checkEditReadership.Enabled = _settingsContainer.SlideBulletsState.EnableReadership;
			checkEditSignature.Enabled = _settingsContainer.SlideBulletsState.EnableSignature;
			checkEditTotalColor.Enabled = _settingsContainer.SlideBulletsState.EnableTotalColor;
			checkEditTotalDiscounts.Enabled = _settingsContainer.SlideBulletsState.EnableDiscounts;
			checkEditTotalFinalCost.Enabled = _settingsContainer.SlideBulletsState.EnableTotalFinalCost;
			checkEditTotalInserts.Enabled = _settingsContainer.SlideBulletsState.EnableTotalInserts;
			checkEditTotalSquare.Enabled = _settingsContainer.SlideBulletsState.EnableTotalSquare;
			checkEditLastSlide.Enabled = _settingsContainer.SlideBulletsState.EnableOnlyOnLastSlide;
			checkEditAllSlides.Enabled = !_settingsContainer.SlideBulletsState.EnableOnlyOnLastSlide;

			checkEditEnableSlideBullets.Checked = _settingsContainer.SlideBulletsState.ShowSlideBullets;
			checkEditAvgAdCost.Checked = _settingsContainer.SlideBulletsState.ShowAvgAdCost;
			checkEditAvgFinalCost.Checked = _settingsContainer.SlideBulletsState.ShowAvgFinalCost;
			checkEditAvgPCI.Checked = _settingsContainer.SlideBulletsState.ShowAvgPCI;
			checkEditColumnInches.Checked = _settingsContainer.SlideBulletsState.ShowColumnInches;
			checkEditDelivery.Checked = _settingsContainer.SlideBulletsState.ShowDelivery;
			checkEditDimensions.Checked = _settingsContainer.SlideBulletsState.ShowDimensions;
			checkEditPageSize.Checked = _settingsContainer.SlideBulletsState.ShowPageSize;
			checkEditPercentOfPage.Checked = _settingsContainer.SlideBulletsState.ShowPercentOfPage;
			checkEditReadership.Checked = _settingsContainer.SlideBulletsState.ShowReadership;
			checkEditSignature.Checked = _settingsContainer.SlideBulletsState.ShowSignature;
			checkEditTotalColor.Checked = _settingsContainer.SlideBulletsState.ShowTotalColor;
			checkEditTotalDiscounts.Checked = _settingsContainer.SlideBulletsState.ShowDiscounts && !checkEditTotalDiscounts.Text.Equals("Discounts: <b>$.00</b>");
			checkEditTotalFinalCost.Checked = _settingsContainer.SlideBulletsState.ShowTotalFinalCost;
			checkEditTotalInserts.Checked = _settingsContainer.SlideBulletsState.ShowTotalInserts;
			checkEditTotalSquare.Checked = _settingsContainer.SlideBulletsState.ShowTotalSquare;
			checkEditLastSlide.Checked = _settingsContainer.SlideBulletsState.ShowOnlyOnLastSlide;
			checkEditAllSlides.Checked = !_settingsContainer.SlideBulletsState.ShowOnlyOnLastSlide;

			_allowToCheckSlideOptions = true;
			_allowToSave = true;
		}

		private void SaveSlideBullets()
		{
			if (!_allowToSave) return;
			_settingsContainer.SlideBulletsState.ShowSlideBullets = checkEditEnableSlideBullets.Checked;
			_settingsContainer.SlideBulletsState.ShowAvgAdCost = checkEditAvgAdCost.Checked & checkEditEnableSlideBullets.Checked;
			_settingsContainer.SlideBulletsState.ShowAvgFinalCost = checkEditAvgFinalCost.Checked & checkEditEnableSlideBullets.Checked;
			_settingsContainer.SlideBulletsState.ShowAvgPCI = checkEditAvgPCI.Checked & checkEditEnableSlideBullets.Checked;
			_settingsContainer.SlideBulletsState.ShowColumnInches = checkEditColumnInches.Checked & checkEditEnableSlideBullets.Checked;
			_settingsContainer.SlideBulletsState.ShowDelivery = checkEditDelivery.Checked & checkEditEnableSlideBullets.Checked;
			_settingsContainer.SlideBulletsState.ShowDimensions = checkEditDimensions.Checked & checkEditEnableSlideBullets.Checked;
			_settingsContainer.SlideBulletsState.ShowPageSize = checkEditPageSize.Checked & checkEditEnableSlideBullets.Checked;
			_settingsContainer.SlideBulletsState.ShowPercentOfPage = checkEditPercentOfPage.Checked & checkEditEnableSlideBullets.Checked;
			_settingsContainer.SlideBulletsState.ShowReadership = checkEditReadership.Checked & checkEditEnableSlideBullets.Checked;
			_settingsContainer.SlideBulletsState.ShowSignature = checkEditSignature.Checked & checkEditEnableSlideBullets.Checked;
			_settingsContainer.SlideBulletsState.ShowTotalColor = checkEditTotalColor.Checked & checkEditEnableSlideBullets.Checked;
			_settingsContainer.SlideBulletsState.ShowDiscounts = checkEditTotalDiscounts.Checked & checkEditEnableSlideBullets.Checked;
			_settingsContainer.SlideBulletsState.ShowTotalFinalCost = checkEditTotalFinalCost.Checked & checkEditEnableSlideBullets.Checked;
			_settingsContainer.SlideBulletsState.ShowTotalInserts = checkEditTotalInserts.Checked & checkEditEnableSlideBullets.Checked;
			_settingsContainer.SlideBulletsState.ShowTotalSquare = checkEditTotalSquare.Checked & checkEditEnableSlideBullets.Checked;
			_settingsContainer.SlideBulletsState.ShowOnlyOnLastSlide = checkEditLastSlide.Checked;
			_settingsContainer.SettingsNotSaved = true;
		}

		private void checkEdit_EditValueChanging(object sender, ChangingEventArgs e)
		{
			e.Cancel = false;
			if (((bool)e.NewValue))
			{
				if (!AllowToSelectSlideOptions() && _allowToCheckSlideOptions)
				{
					Utilities.Instance.ShowWarning("You can select only up to six slide bullets.");
					e.Cancel = true;
				}
			}
		}

		private void checkEdit_CheckedChanged(object sender, EventArgs e)
		{
			SaveSlideBullets();
		}

		private void checkEditEnableSlideBullets_CheckedChanged(object sender, EventArgs e)
		{
			checkEditAllSlides.Enabled = checkEditEnableSlideBullets.Checked;
			checkEditLastSlide.Enabled = checkEditEnableSlideBullets.Checked;
			xtraScrollableControl.Enabled = checkEditEnableSlideBullets.Checked;
			checkEditSignature.Enabled = checkEditEnableSlideBullets.Checked;
			SaveSlideBullets();
		}
	}
}