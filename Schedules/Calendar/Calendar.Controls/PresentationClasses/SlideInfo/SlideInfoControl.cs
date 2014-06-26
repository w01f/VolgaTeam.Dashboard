using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraTab;
using NewBizWiz.Calendar.Controls.BusinessClasses;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;
using ListManager = NewBizWiz.Core.AdSchedule.ListManager;

namespace NewBizWiz.Calendar.Controls.PresentationClasses.SlideInfo
{
	public partial class SlideInfoControl : UserControl, ISlideInfoControl
	{
		private bool _allowToSave;
		private string _helpKey = "info";
		private CalendarMonth _month;

		public SlideInfoControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			favoriteImagesControl.Init();

			#region Assign Properties Changed Event To Controls

			#region Basic
			buttonXBasicSlideTitle.CheckedChanged += propertiesControl_PropertiesChanged;
			comboBoxEditBasicSlideTitle.EditValueChanged += propertiesControl_PropertiesChanged;
			buttonXBasicBusinessName.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXBasicDecisionMaker.CheckedChanged += propertiesControl_PropertiesChanged;
			checkEditBasicApplyForAll.CheckedChanged += propertiesControl_PropertiesChanged;

			comboBoxEditBasicSlideTitle.Enter += Utilities.Instance.Editor_Enter;
			comboBoxEditBasicSlideTitle.MouseDown += Utilities.Instance.Editor_MouseDown;
			comboBoxEditBasicSlideTitle.MouseUp += Utilities.Instance.Editor_MouseUp;
			#endregion

			#region Cost
			buttonXCostDigital.CheckedChanged += propertiesControl_PropertiesChanged;
			spinEditCostDigital.EditValueChanged += propertiesControl_PropertiesChanged;
			buttonXCostNewspaperManual.CheckedChanged += propertiesControl_PropertiesChanged;
			spinEditCostNewspaper.EditValueChanged += propertiesControl_PropertiesChanged;
			checkEditCostApplyForAll.CheckedChanged += propertiesControl_PropertiesChanged;

			spinEditCostDigital.Enter += Utilities.Instance.Editor_Enter;
			spinEditCostDigital.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditCostDigital.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditCostNewspaper.Enter += Utilities.Instance.Editor_Enter;
			spinEditCostNewspaper.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditCostNewspaper.MouseUp += Utilities.Instance.Editor_MouseUp;
			#endregion

			#region Other Numbers
			#endregion

			#region Notes
			buttonXNotesCustomComment.CheckedChanged += propertiesControl_PropertiesChanged;
			memoEditNotesCustomComment.EditValueChanged += propertiesControl_PropertiesChanged;
			checkEditNotesCustomCommentApplyFoAll.CheckedChanged += propertiesControl_PropertiesChanged;

			memoEditNotesCustomComment.Enter += Utilities.Instance.Editor_Enter;
			memoEditNotesCustomComment.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEditNotesCustomComment.MouseUp += Utilities.Instance.Editor_MouseUp;

			buttonXOtherNumbersActiveDays.CheckedChanged += propertiesControl_PropertiesChanged;
			spinEditOtherNumbersActiveDays.EditValueChanged += propertiesControl_PropertiesChanged;
			buttonXOtherNumbersDigitalCPM.CheckedChanged += propertiesControl_PropertiesChanged;
			spinEditOtherNumbersDigitalCPM.EditValueChanged += propertiesControl_PropertiesChanged;
			buttonXOtherNumbersImpressions.CheckedChanged += propertiesControl_PropertiesChanged;
			spinEditOtherNumbersImpressions.EditValueChanged += propertiesControl_PropertiesChanged;
			buttonXOtherNumbersNewspaperAdsNumber.CheckedChanged += propertiesControl_PropertiesChanged;
			spinEditOtherNumbersNewspaperAdsNumber.EditValueChanged += propertiesControl_PropertiesChanged;
			checkEditOtherNumbersApplyForAll.CheckedChanged += propertiesControl_PropertiesChanged;

			spinEditOtherNumbersActiveDays.Enter += Utilities.Instance.Editor_Enter;
			spinEditOtherNumbersActiveDays.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditOtherNumbersActiveDays.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditOtherNumbersDigitalCPM.Enter += Utilities.Instance.Editor_Enter;
			spinEditOtherNumbersDigitalCPM.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditOtherNumbersDigitalCPM.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditOtherNumbersImpressions.Enter += Utilities.Instance.Editor_Enter;
			spinEditOtherNumbersImpressions.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditOtherNumbersImpressions.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditOtherNumbersNewspaperAdsNumber.Enter += Utilities.Instance.Editor_Enter;
			spinEditOtherNumbersNewspaperAdsNumber.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditOtherNumbersNewspaperAdsNumber.MouseUp += Utilities.Instance.Editor_MouseUp;
			#endregion

			#region Style
			buttonXThemeColorBlack.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXThemeColorBlue.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXThemeColorGray.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXThemeColorGreen.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXThemeColorOrange.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXThemeColorTeal.CheckedChanged += propertiesControl_PropertiesChanged;
			checkEditThemeColorApplyForAll.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXStyleBigDate.CheckedChanged += propertiesControl_PropertiesChanged;
			#endregion

			#region Logo
			buttonXLogo.CheckedChanged += propertiesControl_PropertiesChanged;
			checkEditLogoApplyForAll.CheckedChanged += propertiesControl_PropertiesChanged;
			layoutViewLogoGallery.FocusedRowChanged += propertiesControl_PropertiesChanged;
			gridControlLogoGallery.DataSource = ListManager.Instance.Images;
			#endregion

			#endregion
		}

		public string MonthTitle { get; set; }
		public bool SettingsNotSaved { get; set; }

		protected CommonCalendarOutputData OutputData
		{
			get { return _month.OutputData as CommonCalendarOutputData; }
		}

		[Browsable(true)]
		[Category("Action")]
		public event EventHandler Closed;

		[Browsable(true)]
		[Category("Action")]
		public event EventHandler<EventArgs> PropertyChanged;

		public void OnThemeChanged(EventArgs e)
		{
			EventHandler<EventArgs> handler = PropertyChanged;
			if (handler != null) handler(this, e);
		}

		public void LoadMonth(CalendarMonth month)
		{
			_month = month;
			LoadCurrentMonthData();
		}

		public void LoadCurrentMonthData()
		{
			if (_month == null) return;
			_allowToSave = false;
			MonthTitle = "Slide Info - " + _month.Date.ToString("MMMM yyyy");

			#region Basic
			string[] slideHeaders = ListManager.Instance.OutputHeaders.Union(Core.OnlineSchedule.ListManager.Instance.SlideHeaders).ToArray();
			buttonXBasicSlideTitle.Checked = OutputData.ShowHeader;
			comboBoxEditBasicSlideTitle.Properties.Items.Clear();
			comboBoxEditBasicSlideTitle.Properties.Items.AddRange(slideHeaders);
			if (!string.IsNullOrEmpty(OutputData.Header))
				comboBoxEditBasicSlideTitle.EditValue = OutputData.Header;
			else
				comboBoxEditBasicSlideTitle.EditValue = slideHeaders.FirstOrDefault();

			buttonXBasicBusinessName.Checked = OutputData.ShowBusinessName;
			labelControlAdvertiser.Text = !string.IsNullOrEmpty(OutputData.BusinessName) ? OutputData.BusinessName : _month.Parent.Schedule.BusinessName;

			buttonXBasicDecisionMaker.Checked = OutputData.ShowDecisionMaker;
			labelControlDecisionMaker.Text = !string.IsNullOrEmpty(OutputData.DecisionMaker) ? OutputData.DecisionMaker : _month.Parent.Schedule.DecisionMaker;

			buttonXNotesCustomComment.Checked = OutputData.ShowCustomComment;
			memoEditNotesCustomComment.EditValue = OutputData.CustomComment;
			checkEditNotesCustomCommentApplyFoAll.Checked = OutputData.ApplyForAllCustomComment;

			checkEditBasicApplyForAll.Checked = OutputData.ApplyForAllBasic;
			#endregion

			#region Cost
			buttonXCostNewspaperManual.Checked = OutputData.ShowPrintTotalCostManual;
			spinEditCostNewspaper.Value = OutputData.PrintTotalCost.HasValue ? (decimal)OutputData.PrintTotalCost.Value : 0;

			buttonXCostDigital.Checked = OutputData.ShowDigitalTotalCost;
			spinEditCostDigital.Value = OutputData.DigitalTotalCost.HasValue ? (decimal)OutputData.DigitalTotalCost.Value : 0;

			checkEditCostApplyForAll.Checked = OutputData.ApplyForAlCost;
			#endregion

			#region Notes
			buttonXOtherNumbersActiveDays.Checked = OutputData.ShowActiveDays;
			spinEditOtherNumbersActiveDays.Value = OutputData.ActiveDays;

			buttonXOtherNumbersNewspaperAdsNumber.Checked = OutputData.ShowPrintAdsNumber;
			spinEditOtherNumbersNewspaperAdsNumber.Value = OutputData.PrintAdsNumber;

			buttonXOtherNumbersImpressions.Checked = OutputData.ShowImpressions;
			spinEditOtherNumbersImpressions.Value = OutputData.Impressions.HasValue ? (decimal)OutputData.Impressions.Value : 0;

			buttonXOtherNumbersDigitalCPM.Checked = OutputData.ShowDigitalCPM;
			spinEditOtherNumbersDigitalCPM.Value = OutputData.DigitalCPM.HasValue ? (decimal)OutputData.DigitalCPM.Value : 0;

			checkEditOtherNumbersApplyForAll.Checked = OutputData.ApplyForAllOtherNumbers;
			#endregion

			#region Style
			buttonXThemeColorBlack.Checked = false;
			buttonXThemeColorBlue.Checked = false;
			buttonXThemeColorGray.Checked = false;
			buttonXThemeColorGreen.Checked = false;
			buttonXThemeColorOrange.Checked = false;
			buttonXThemeColorTeal.Checked = false;
			switch (OutputData.SlideColor)
			{
				case "black":
					buttonXThemeColorBlack.Checked = true;
					break;
				case "blue":
					buttonXThemeColorBlue.Checked = true;
					break;
				case "gray":
					buttonXThemeColorGray.Checked = true;
					break;
				case "green":
					buttonXThemeColorGreen.Checked = true;
					break;
				case "orange":
					buttonXThemeColorOrange.Checked = true;
					break;
				case "teal":
					buttonXThemeColorTeal.Checked = true;
					break;
			}
			checkEditThemeColorApplyForAll.Checked = OutputData.ApplyForAllThemeColor;


			buttonXStyleBigDate.Checked = OutputData.ShowBigDate;
			#endregion

			#region Logo
			buttonXLogo.Checked = OutputData.ShowLogo;
			checkEditLogoApplyForAll.Checked = OutputData.ApplyForAllLogo;
			var selectedLogo = ListManager.Instance.Images.FirstOrDefault(l => l.EncodedBigImage.Equals(OutputData.EncodedLogo));
			if (selectedLogo != null)
			{
				var index = ListManager.Instance.Images.IndexOf(selectedLogo);
				layoutViewLogoGallery.FocusedRowHandle = layoutViewLogoGallery.GetRowHandle(index);
			}
			else
				layoutViewLogoGallery.FocusedRowHandle = 0;
			#endregion

			_allowToSave = true;
			SettingsNotSaved = false;
		}

		public void SaveData()
		{
			if (!_allowToSave) return;

			#region Basic
			OutputData.ShowHeader = buttonXBasicSlideTitle.Checked;
			OutputData.Header = comboBoxEditBasicSlideTitle.EditValue != null ? comboBoxEditBasicSlideTitle.EditValue.ToString() : string.Empty;

			OutputData.ShowBusinessName = buttonXBasicBusinessName.Checked;
			OutputData.ShowDecisionMaker = buttonXBasicDecisionMaker.Checked;

			OutputData.ApplyForAllBasic = checkEditBasicApplyForAll.Checked;
			foreach (var outputData in _month.Parent.Months.Select(m => m.OutputData).OfType<CommonCalendarOutputData>())
			{
				if (outputData == OutputData) continue;
				outputData.ApplyForAllBasic = OutputData.ApplyForAllBasic;
				if (!OutputData.ApplyForAllBasic) continue;
				outputData.ShowHeader = OutputData.ShowHeader;
				outputData.Header = OutputData.Header;
				outputData.ShowBusinessName = OutputData.ShowBusinessName;
				outputData.ShowDecisionMaker = OutputData.ShowDecisionMaker;
			}

			OutputData.ShowCustomComment = buttonXNotesCustomComment.Checked;
			OutputData.CustomComment = memoEditNotesCustomComment.EditValue != null ? memoEditNotesCustomComment.EditValue.ToString() : string.Empty;
			OutputData.ApplyForAllCustomComment = checkEditNotesCustomCommentApplyFoAll.Checked;
			foreach (var outputData in _month.Parent.Months.Select(m => m.OutputData).OfType<CommonCalendarOutputData>())
			{
				if (outputData == OutputData) continue;
				outputData.ApplyForAllCustomComment = OutputData.ApplyForAllCustomComment;
				if (!OutputData.ApplyForAllCustomComment) continue;
				outputData.ShowCustomComment = OutputData.ShowCustomComment;
				outputData.CustomComment = OutputData.CustomComment;
			}
			#endregion

			#region Cost
			OutputData.ShowPrintTotalCostManual = buttonXCostNewspaperManual.Checked;
			OutputData.PrintTotalCost = spinEditCostNewspaper.Value > 0 ? (double?)spinEditCostNewspaper.Value : null;

			OutputData.ShowDigitalTotalCost = buttonXCostDigital.Checked;
			OutputData.DigitalTotalCost = spinEditCostDigital.Value > 0 ? (double?)spinEditCostDigital.Value : null;

			OutputData.ApplyForAlCost = checkEditCostApplyForAll.Checked;
			foreach (var outputData in _month.Parent.Months.Select(m => m.OutputData).OfType<CommonCalendarOutputData>())
			{
				if (outputData == OutputData) continue;
				outputData.ApplyForAlCost = OutputData.ApplyForAlCost;
				if (!OutputData.ApplyForAlCost) continue;
				outputData.ShowPrintTotalCostManual = OutputData.ShowPrintTotalCostManual;
				outputData.PrintTotalCost = OutputData.PrintTotalCost;
				outputData.ShowDigitalTotalCost = OutputData.ShowDigitalTotalCost;
				outputData.DigitalTotalCost = OutputData.DigitalTotalCost;
			}
			#endregion

			#region Notes
			OutputData.ShowActiveDays = buttonXOtherNumbersActiveDays.Checked;
			OutputData.ActiveDays = (int)spinEditOtherNumbersActiveDays.Value;

			OutputData.ShowPrintAdsNumber = buttonXOtherNumbersNewspaperAdsNumber.Checked;
			OutputData.PrintAdsNumber = (int)spinEditOtherNumbersNewspaperAdsNumber.Value;

			OutputData.ShowImpressions = buttonXOtherNumbersImpressions.Checked;
			OutputData.Impressions = spinEditOtherNumbersImpressions.Value > 0 ? (double?)spinEditOtherNumbersImpressions.Value : null;

			OutputData.ShowDigitalCPM = buttonXOtherNumbersDigitalCPM.Checked;
			OutputData.DigitalCPM = spinEditOtherNumbersDigitalCPM.Value > 0 ? (double?)spinEditOtherNumbersDigitalCPM.Value : null;

			OutputData.ApplyForAllOtherNumbers = checkEditOtherNumbersApplyForAll.Checked;
			foreach (var outputData in _month.Parent.Months.Select(m => m.OutputData).OfType<CommonCalendarOutputData>())
			{
				if (outputData == OutputData) continue;
				outputData.ApplyForAllOtherNumbers = OutputData.ApplyForAllOtherNumbers;
				if (!OutputData.ApplyForAllOtherNumbers) continue;
				outputData.ShowActiveDays = OutputData.ShowActiveDays;
				outputData.ActiveDays = OutputData.ActiveDays;
				outputData.ShowPrintAdsNumber = OutputData.ShowPrintAdsNumber;
				outputData.PrintAdsNumber = OutputData.PrintAdsNumber;
				outputData.ShowImpressions = OutputData.ShowImpressions;
				outputData.Impressions = OutputData.Impressions;
				outputData.ShowDigitalCPM = OutputData.ShowDigitalCPM;
				outputData.DigitalCPM = OutputData.DigitalCPM;
			}
			#endregion

			#region Style
			if (buttonXThemeColorBlack.Checked)
				OutputData.SlideColor = "black";
			else if (buttonXThemeColorBlue.Checked)
				OutputData.SlideColor = "blue";
			else if (buttonXThemeColorGray.Checked)
				OutputData.SlideColor = "gray";
			else if (buttonXThemeColorGreen.Checked)
				OutputData.SlideColor = "green";
			else if (buttonXThemeColorOrange.Checked)
				OutputData.SlideColor = "orange";
			else if (buttonXThemeColorTeal.Checked)
				OutputData.SlideColor = "teal";
			OutputData.ApplyForAllThemeColor = checkEditThemeColorApplyForAll.Checked;
			foreach (var outputData in _month.Parent.Months.Select(m => m.OutputData).OfType<CommonCalendarOutputData>())
			{
				if (outputData == OutputData) continue;
				outputData.ApplyForAllThemeColor = OutputData.ApplyForAllThemeColor;
				if (OutputData.ApplyForAllThemeColor)
					outputData.SlideColor = OutputData.SlideColor;
			}
			OutputData.ShowBigDate = buttonXStyleBigDate.Checked;
			#endregion

			#region Logo
			OutputData.ShowLogo = buttonXLogo.Checked;
			var selecteImageSource = layoutViewLogoGallery.GetFocusedRow() as ImageSource;
			OutputData.Logo = OutputData.ShowLogo && selecteImageSource != null ? selecteImageSource.BigImage : null;
			OutputData.EncodedLogo = null;
			OutputData.ApplyForAllLogo = checkEditLogoApplyForAll.Checked;
			foreach (var outputData in _month.Parent.Months.Select(m => m.OutputData).OfType<CommonCalendarOutputData>())
			{
				if (outputData == OutputData) continue;
				outputData.ApplyForAllLogo = OutputData.ApplyForAllLogo;
				if (!OutputData.ApplyForAllLogo) continue;
				outputData.ShowLogo = OutputData.ShowLogo;
				outputData.Logo = OutputData.Logo;
				outputData.EncodedLogo = null;
			}
			#endregion

			SettingsNotSaved = false;
		}

		private void barLargeButtonItemHelp_ItemClick(object sender, ItemClickEventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink(_helpKey);
		}

		private void barLargeButtonItemClose_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (Closed != null)
				Closed(sender, e);
		}

		private void propertiesControl_PropertiesChanged(object sender, EventArgs e)
		{
			SettingsNotSaved = true;
		}

		private void xtraTabControl_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			if (xtraTabControl.SelectedTabPage == xtraTabPageBasic)
				_helpKey = "info";
			else if (xtraTabControl.SelectedTabPage == xtraTabPageCost)
				_helpKey = "cost";
			else if (xtraTabControl.SelectedTabPage == xtraTabPageNotes)
				_helpKey = "notes";
			else if (xtraTabControl.SelectedTabPage == xtraTabPageStyle)
				_helpKey = "style";
			else if (xtraTabControl.SelectedTabPage == xtraTabPageLogo)
				_helpKey = "logo";
			else
				_helpKey = string.Empty;
		}

		#region Basic Event Handlers
		private void buttonXBasicSlideTitle_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxEditBasicSlideTitle.Enabled = buttonXBasicSlideTitle.Checked;
		}

		private void buttonXBasicBusinessName_CheckedChanged(object sender, EventArgs e)
		{
			labelControlAdvertiser.Enabled = buttonXBasicBusinessName.Checked;
		}

		private void buttonXBasicDecisionMaker_CheckedChanged(object sender, EventArgs e)
		{
			labelControlDecisionMaker.Enabled = buttonXBasicDecisionMaker.Checked;
		}
		#endregion

		#region Cost Event Handlers
		private void buttonXCostNewspaperManual_CheckedChanged(object sender, EventArgs e)
		{
			spinEditCostNewspaper.Enabled = buttonXCostNewspaperManual.Checked;
		}

		private void buttonXCostDigital_CheckedChanged(object sender, EventArgs e)
		{
			spinEditCostDigital.Enabled = buttonXCostDigital.Checked;
		}
		#endregion

		#region Notes Event Handlers
		private void buttonXCustomComment_CheckedChanged(object sender, EventArgs e)
		{
			memoEditNotesCustomComment.Enabled = buttonXNotesCustomComment.Checked;
		}

		private void buttonXOtherNumbersActiveDays_CheckedChanged(object sender, EventArgs e)
		{
			spinEditOtherNumbersActiveDays.Enabled = buttonXOtherNumbersActiveDays.Checked;
		}

		private void buttonXOtherNumbersNewspaperAdsNumber_CheckedChanged(object sender, EventArgs e)
		{
			spinEditOtherNumbersNewspaperAdsNumber.Enabled = buttonXOtherNumbersNewspaperAdsNumber.Checked;
		}

		private void buttonXOtherNumbersImpressions_CheckedChanged(object sender, EventArgs e)
		{
			spinEditOtherNumbersImpressions.Enabled = buttonXOtherNumbersImpressions.Checked;
		}

		private void buttonXOtherNumbersDigitalCPM_CheckedChanged(object sender, EventArgs e)
		{
			spinEditOtherNumbersDigitalCPM.Enabled = buttonXOtherNumbersDigitalCPM.Checked;
		}
		#endregion

		#region Style Event Handlers
		private void buttonXThemeColor_Click(object sender, EventArgs e)
		{
			buttonXThemeColorBlack.Checked = false;
			buttonXThemeColorBlue.Checked = false;
			buttonXThemeColorGray.Checked = false;
			buttonXThemeColorGreen.Checked = false;
			buttonXThemeColorOrange.Checked = false;
			buttonXThemeColorTeal.Checked = false;
			(sender as ButtonX).Checked = true;

			if (buttonXThemeColorBlack.Checked)
				OutputData.SlideColor = "black";
			else if (buttonXThemeColorBlue.Checked)
				OutputData.SlideColor = "blue";
			else if (buttonXThemeColorGray.Checked)
				OutputData.SlideColor = "gray";
			else if (buttonXThemeColorGreen.Checked)
				OutputData.SlideColor = "green";
			else if (buttonXThemeColorOrange.Checked)
				OutputData.SlideColor = "orange";
			else if (buttonXThemeColorTeal.Checked)
				OutputData.SlideColor = "teal";
			OnThemeChanged(EventArgs.Empty);
		}
		#endregion

		#region Logo Event Handlers
		private void buttonXLogo_CheckedChanged(object sender, EventArgs e)
		{
			gridControlLogoGallery.Enabled = buttonXLogo.Checked;
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
		#endregion


	}
}