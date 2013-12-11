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

		[Browsable(true)]
		[Category("Action")]
		public event EventHandler Closed;

		[Browsable(true)]
		[Category("Action")]
		public event EventHandler<EventArgs> ThemeChanged;

		public void OnThemeChanged(EventArgs e)
		{
			EventHandler<EventArgs> handler = ThemeChanged;
			if (handler != null) handler(this, e);
		}

		public void LoadMonth(CalendarMonth month)
		{
			_month = month;
			LoadCurrentMonthData();
		}

		public void LoadCurrentMonthData()
		{
			if (_month != null)
			{
				_allowToSave = false;
				MonthTitle = "Slide Info - " + _month.Date.ToString("MMMM yyyy");

				#region Basic
				string[] slideHeaders = ListManager.Instance.OutputHeaders.Union(Core.OnlineSchedule.ListManager.Instance.SlideHeaders).ToArray();
				buttonXBasicSlideTitle.Checked = _month.OutputData.ShowHeader;
				comboBoxEditBasicSlideTitle.Properties.Items.Clear();
				comboBoxEditBasicSlideTitle.Properties.Items.AddRange(slideHeaders);
				if (!string.IsNullOrEmpty(_month.OutputData.Header))
					comboBoxEditBasicSlideTitle.EditValue = _month.OutputData.Header;
				else
					comboBoxEditBasicSlideTitle.EditValue = slideHeaders.FirstOrDefault();

				buttonXBasicBusinessName.Checked = _month.OutputData.ShowBusinessName;
				labelControlAdvertiser.Text = !string.IsNullOrEmpty(_month.OutputData.BusinessName) ? _month.OutputData.BusinessName : _month.Parent.Schedule.BusinessName;

				buttonXBasicDecisionMaker.Checked = _month.OutputData.ShowDecisionMaker;
				labelControlDecisionMaker.Text = !string.IsNullOrEmpty(_month.OutputData.DecisionMaker) ? _month.OutputData.DecisionMaker : _month.Parent.Schedule.DecisionMaker;

				buttonXNotesCustomComment.Checked = _month.OutputData.ShowCustomComment;
				memoEditNotesCustomComment.EditValue = _month.OutputData.CustomComment;
				checkEditNotesCustomCommentApplyFoAll.Checked = _month.OutputData.ApplyForAllCustomComment;

				checkEditBasicApplyForAll.Checked = _month.OutputData.ApplyForAllBasic;
				#endregion

				#region Cost
				buttonXCostNewspaperManual.Checked = _month.OutputData.ShowPrintTotalCostManual;
				spinEditCostNewspaper.Value = _month.OutputData.PrintTotalCost.HasValue ? (decimal)_month.OutputData.PrintTotalCost.Value : 0;

				buttonXCostDigital.Checked = _month.OutputData.ShowDigitalTotalCost;
				spinEditCostDigital.Value = _month.OutputData.DigitalTotalCost.HasValue ? (decimal)_month.OutputData.DigitalTotalCost.Value : 0;

				checkEditCostApplyForAll.Checked = _month.OutputData.ApplyForAlCost;
				#endregion

				#region Notes
				buttonXOtherNumbersActiveDays.Checked = _month.OutputData.ShowActiveDays;
				spinEditOtherNumbersActiveDays.Value = _month.OutputData.ActiveDays;

				buttonXOtherNumbersNewspaperAdsNumber.Checked = _month.OutputData.ShowPrintAdsNumber;
				spinEditOtherNumbersNewspaperAdsNumber.Value = _month.OutputData.PrintAdsNumber;

				buttonXOtherNumbersImpressions.Checked = _month.OutputData.ShowImpressions;
				spinEditOtherNumbersImpressions.Value = _month.OutputData.Impressions.HasValue ? (decimal)_month.OutputData.Impressions.Value : 0;

				buttonXOtherNumbersDigitalCPM.Checked = _month.OutputData.ShowDigitalCPM;
				spinEditOtherNumbersDigitalCPM.Value = _month.OutputData.DigitalCPM.HasValue ? (decimal)_month.OutputData.DigitalCPM.Value : 0;

				checkEditOtherNumbersApplyForAll.Checked = _month.OutputData.ApplyForAllOtherNumbers;
				#endregion

				#region Style
				buttonXThemeColorBlack.Checked = false;
				buttonXThemeColorBlue.Checked = false;
				buttonXThemeColorGray.Checked = false;
				buttonXThemeColorGreen.Checked = false;
				buttonXThemeColorOrange.Checked = false;
				buttonXThemeColorTeal.Checked = false;
				switch (_month.OutputData.SlideColor)
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
				checkEditThemeColorApplyForAll.Checked = _month.OutputData.ApplyForAllThemeColor;


				buttonXStyleBigDate.Checked = _month.OutputData.ShowBigDate;
				#endregion

				#region Logo
				buttonXLogo.Checked = _month.OutputData.ShowLogo;
				checkEditLogoApplyForAll.Checked = _month.OutputData.ApplyForAllLogo;
				var selectedLogo = ListManager.Instance.Images.FirstOrDefault(l => l.EncodedBigImage.Equals(_month.OutputData.EncodedLogo));
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
		}

		public void SaveData()
		{
			if (_allowToSave)
			{
				#region Basic
				_month.OutputData.ShowHeader = buttonXBasicSlideTitle.Checked;
				_month.OutputData.Header = comboBoxEditBasicSlideTitle.EditValue != null ? comboBoxEditBasicSlideTitle.EditValue.ToString() : string.Empty;

				_month.OutputData.ShowBusinessName = buttonXBasicBusinessName.Checked;
				_month.OutputData.ShowDecisionMaker = buttonXBasicDecisionMaker.Checked;

				_month.OutputData.ApplyForAllBasic = checkEditBasicApplyForAll.Checked;
				if (_month.OutputData.ApplyForAllBasic)
				{
					foreach (CalendarMonth month in _month.Parent.Months)
					{
						if (month != _month)
						{
							month.OutputData.ShowHeader = _month.OutputData.ShowHeader;
							month.OutputData.Header = _month.OutputData.Header;
							month.OutputData.ShowBusinessName = _month.OutputData.ShowBusinessName;
							month.OutputData.ShowDecisionMaker = _month.OutputData.ShowDecisionMaker;
							month.OutputData.ApplyForAllBasic = _month.OutputData.ApplyForAllBasic;
						}
					}
				}

				_month.OutputData.ShowCustomComment = buttonXNotesCustomComment.Checked;
				_month.OutputData.CustomComment = memoEditNotesCustomComment.EditValue != null ? memoEditNotesCustomComment.EditValue.ToString() : string.Empty;
				_month.OutputData.ApplyForAllCustomComment = checkEditNotesCustomCommentApplyFoAll.Checked;
				if (_month.OutputData.ApplyForAllCustomComment)
				{
					foreach (CalendarMonth month in _month.Parent.Months)
					{
						if (month != _month)
						{
							month.OutputData.ShowCustomComment = _month.OutputData.ShowCustomComment;
							month.OutputData.CustomComment = _month.OutputData.CustomComment;
							month.OutputData.ApplyForAllCustomComment = _month.OutputData.ApplyForAllCustomComment;
						}
					}
				}
				#endregion

				#region Cost
				_month.OutputData.ShowPrintTotalCostManual = buttonXCostNewspaperManual.Checked;
				_month.OutputData.PrintTotalCost = spinEditCostNewspaper.Value > 0 ? (double?)spinEditCostNewspaper.Value : null;

				_month.OutputData.ShowDigitalTotalCost = buttonXCostDigital.Checked;
				_month.OutputData.DigitalTotalCost = spinEditCostDigital.Value > 0 ? (double?)spinEditCostDigital.Value : null;

				_month.OutputData.ApplyForAlCost = checkEditCostApplyForAll.Checked;
				if (_month.OutputData.ApplyForAlCost)
				{
					foreach (CalendarMonth month in _month.Parent.Months)
					{
						if (month != _month)
						{
							month.OutputData.ShowPrintTotalCostManual = _month.OutputData.ShowPrintTotalCostManual;
							month.OutputData.PrintTotalCost = _month.OutputData.PrintTotalCost;
							month.OutputData.ShowDigitalTotalCost = _month.OutputData.ShowDigitalTotalCost;
							month.OutputData.DigitalTotalCost = _month.OutputData.DigitalTotalCost;
							month.OutputData.ApplyForAlCost = _month.OutputData.ApplyForAlCost;
						}
					}
				}
				#endregion

				#region Notes
				_month.OutputData.ShowActiveDays = buttonXOtherNumbersActiveDays.Checked;
				_month.OutputData.ActiveDays = (int)spinEditOtherNumbersActiveDays.Value;

				_month.OutputData.ShowPrintAdsNumber = buttonXOtherNumbersNewspaperAdsNumber.Checked;
				_month.OutputData.PrintAdsNumber = (int)spinEditOtherNumbersNewspaperAdsNumber.Value;

				_month.OutputData.ShowImpressions = buttonXOtherNumbersImpressions.Checked;
				_month.OutputData.Impressions = spinEditOtherNumbersImpressions.Value > 0 ? (double?)spinEditOtherNumbersImpressions.Value : null;

				_month.OutputData.ShowDigitalCPM = buttonXOtherNumbersDigitalCPM.Checked;
				_month.OutputData.DigitalCPM = spinEditOtherNumbersDigitalCPM.Value > 0 ? (double?)spinEditOtherNumbersDigitalCPM.Value : null;

				_month.OutputData.ApplyForAllOtherNumbers = checkEditOtherNumbersApplyForAll.Checked;
				if (_month.OutputData.ApplyForAllOtherNumbers)
				{
					foreach (CalendarMonth month in _month.Parent.Months)
					{
						if (month != _month)
						{
							month.OutputData.ShowActiveDays = _month.OutputData.ShowActiveDays;
							month.OutputData.ActiveDays = _month.OutputData.ActiveDays;
							month.OutputData.ShowPrintAdsNumber = _month.OutputData.ShowPrintAdsNumber;
							month.OutputData.PrintAdsNumber = _month.OutputData.PrintAdsNumber;
							month.OutputData.ShowImpressions = _month.OutputData.ShowImpressions;
							month.OutputData.Impressions = _month.OutputData.Impressions;
							month.OutputData.ShowDigitalCPM = _month.OutputData.ShowDigitalCPM;
							month.OutputData.DigitalCPM = _month.OutputData.DigitalCPM;
							month.OutputData.ApplyForAllOtherNumbers = _month.OutputData.ApplyForAllOtherNumbers;
						}
					}
				}
				#endregion

				#region Style
				if (buttonXThemeColorBlack.Checked)
					_month.OutputData.SlideColor = "black";
				else if (buttonXThemeColorBlue.Checked)
					_month.OutputData.SlideColor = "blue";
				else if (buttonXThemeColorGray.Checked)
					_month.OutputData.SlideColor = "gray";
				else if (buttonXThemeColorGreen.Checked)
					_month.OutputData.SlideColor = "green";
				else if (buttonXThemeColorOrange.Checked)
					_month.OutputData.SlideColor = "orange";
				else if (buttonXThemeColorTeal.Checked)
					_month.OutputData.SlideColor = "teal";
				_month.OutputData.ApplyForAllThemeColor = checkEditThemeColorApplyForAll.Checked;
				if (_month.OutputData.ApplyForAllThemeColor)
				{
					foreach (CalendarMonth month in _month.Parent.Months)
					{
						if (month != _month)
						{
							month.OutputData.SlideColor = _month.OutputData.SlideColor;
							month.OutputData.ApplyForAllThemeColor = _month.OutputData.ApplyForAllThemeColor;
						}
					}
				}
				_month.OutputData.ShowBigDate = buttonXStyleBigDate.Checked;
				#endregion

				#region Logo
				_month.OutputData.ShowLogo = buttonXLogo.Checked;
				var selecteImageSource = layoutViewLogoGallery.GetFocusedRow() as ImageSource;
				_month.OutputData.Logo = _month.OutputData.ShowLogo && selecteImageSource != null ? selecteImageSource.BigImage : null;
				_month.OutputData.EncodedLogo = null;
				_month.OutputData.ApplyForAllLogo = checkEditLogoApplyForAll.Checked;
				if (_month.OutputData.ApplyForAllLogo)
				{
					foreach (CalendarMonth month in _month.Parent.Months)
					{
						if (month != _month)
						{
							month.OutputData.ShowLogo = _month.OutputData.ShowLogo;
							month.OutputData.Logo = _month.OutputData.Logo;
							month.OutputData.EncodedLogo = null;
							month.OutputData.ApplyForAllLogo = _month.OutputData.ApplyForAllLogo;
						}
					}
				}
				#endregion

				SettingsNotSaved = false;
			}
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
				_month.OutputData.SlideColor = "black";
			else if (buttonXThemeColorBlue.Checked)
				_month.OutputData.SlideColor = "blue";
			else if (buttonXThemeColorGray.Checked)
				_month.OutputData.SlideColor = "gray";
			else if (buttonXThemeColorGreen.Checked)
				_month.OutputData.SlideColor = "green";
			else if (buttonXThemeColorOrange.Checked)
				_month.OutputData.SlideColor = "orange";
			else if (buttonXThemeColorTeal.Checked)
				_month.OutputData.SlideColor = "teal";
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