using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraGrid.Views.Layout;
using NewBizWiz.Calendar.Controls.PresentationClasses.SlideInfo;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;
using ListManager = NewBizWiz.Core.AdSchedule.ListManager;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls.Calendar
{
	public partial class PublicationSlideInfoControl : UserControl, ISlideInfoControl
	{
		private bool _allowToSave;
		private CalendarMonth _month;

		public PublicationSlideInfoControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			favoriteImagesControl.Init();

			#region Assign Properties Changed Event To Controls

			#region Info
			buttonXShowSection.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXShowCost.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXShowColor.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXShowBigDate.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXShowAdSize.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXShowPageSize.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXShowPersentOfPage.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXShowAbbreviation.CheckedChanged += propertiesControl_PropertiesChanged;
			checkEditInfoApplyForAll.CheckedChanged += propertiesControl_PropertiesChanged;

			checkEditShowComment.CheckedChanged += propertiesControl_PropertiesChanged;
			memoEditNotesCustomComment.EditValueChanged += propertiesControl_PropertiesChanged;
			checkEditNotesCustomCommentApplyFoAll.CheckedChanged += propertiesControl_PropertiesChanged;
			memoEditNotesCustomComment.Enter += Utilities.Instance.Editor_Enter;
			memoEditNotesCustomComment.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEditNotesCustomComment.MouseUp += Utilities.Instance.Editor_MouseUp;
			#endregion

			#region Style
			buttonXThemeColorBlack.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXThemeColorBlue.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXThemeColorGray.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXThemeColorGreen.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXThemeColorOrange.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXThemeColorTeal.CheckedChanged += propertiesControl_PropertiesChanged;
			checkEditThemeColorApplyForAll.CheckedChanged += propertiesControl_PropertiesChanged;
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

		protected PublicationCalendarOutputData OutputData
		{
			get { return _month != null ? _month.OutputData as PublicationCalendarOutputData : null; }
		}

		[Browsable(true)]
		[Category("Action")]
		public event EventHandler Closed;

		[Browsable(true)]
		[Category("Action")]
		public event EventHandler<EventArgs> PropertyChanged;

		public void OnPropertyChanged(EventArgs e)
		{
			var handler = PropertyChanged;
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

			#region Info
			buttonXShowSection.Enabled = ListManager.Instance.DefaultCalendarViewSettings.EnableSection;
			buttonXShowCost.Enabled = ListManager.Instance.DefaultCalendarViewSettings.EnableCost;
			buttonXShowColor.Enabled = ListManager.Instance.DefaultCalendarViewSettings.EnableColor;
			buttonXShowBigDate.Enabled = ListManager.Instance.DefaultCalendarViewSettings.EnableBigDate;
			buttonXShowAdSize.Enabled = ListManager.Instance.DefaultCalendarViewSettings.EnableAdSize;
			buttonXShowPageSize.Enabled = ListManager.Instance.DefaultCalendarViewSettings.EnablePageSize;
			buttonXShowPersentOfPage.Enabled = ListManager.Instance.DefaultCalendarViewSettings.EnablePercentOfPage;
			buttonXShowAbbreviation.Enabled = ListManager.Instance.DefaultCalendarViewSettings.EnableAbbreviationOnly;

			buttonXShowSection.Checked = OutputData.ShowSection && buttonXShowSection.Enabled;
			buttonXShowCost.Checked = OutputData.ShowCost && buttonXShowCost.Enabled;
			buttonXShowColor.Checked = OutputData.ShowColor && buttonXShowColor.Enabled;
			buttonXShowBigDate.Checked = OutputData.ShowBigDate && buttonXShowBigDate.Enabled;
			buttonXShowAdSize.Checked = OutputData.ShowAdSize && buttonXShowAdSize.Enabled;
			buttonXShowPageSize.Checked = OutputData.ShowPageSize && buttonXShowPageSize.Enabled;
			buttonXShowPersentOfPage.Checked = OutputData.ShowPercentOfPage && buttonXShowPersentOfPage.Enabled;
			buttonXShowAbbreviation.Checked = OutputData.ShowAbbreviationOnly && buttonXShowAbbreviation.Enabled;
			checkEditInfoApplyForAll.Checked = OutputData.ApplyForAllBasic;

			checkEditShowComment.Enabled = ListManager.Instance.DefaultCalendarViewSettings.EnableComments;
			checkEditShowComment.Text = String.Format("{0} Slide Comment", _month.Date.ToString("MMMM"));
			checkEditShowComment.Checked = OutputData.ShowCustomComment && checkEditShowComment.Enabled;
			memoEditNotesCustomComment.EditValue = OutputData.CustomComment;
			checkEditNotesCustomCommentApplyFoAll.Checked = OutputData.ApplyForAllCustomComment;
			#endregion

			#region Style
			buttonXThemeColorBlack.Enabled = ListManager.Instance.DefaultCalendarViewSettings.EnableBlack;
			buttonXThemeColorBlue.Enabled = ListManager.Instance.DefaultCalendarViewSettings.EnableBlue;
			buttonXThemeColorGray.Enabled = ListManager.Instance.DefaultCalendarViewSettings.EnableGray;
			buttonXThemeColorGreen.Enabled = ListManager.Instance.DefaultCalendarViewSettings.EnableGreen;
			buttonXThemeColorOrange.Enabled = ListManager.Instance.DefaultCalendarViewSettings.EnableOrange;
			buttonXThemeColorTeal.Enabled = ListManager.Instance.DefaultCalendarViewSettings.EnableTeal;
			buttonXThemeColorBlack.Checked = false;
			buttonXThemeColorBlue.Checked = false;
			buttonXThemeColorGray.Checked = false;
			buttonXThemeColorGreen.Checked = false;
			buttonXThemeColorOrange.Checked = false;
			buttonXThemeColorTeal.Checked = false;
			switch (OutputData.SlideColor)
			{
				case "black":
					buttonXThemeColorBlack.Checked = buttonXThemeColorBlack.Enabled;
					break;
				case "blue":
					buttonXThemeColorBlue.Checked = buttonXThemeColorBlue.Enabled;
					break;
				case "gray":
					buttonXThemeColorGray.Checked = buttonXThemeColorGray.Enabled;
					break;
				case "green":
					buttonXThemeColorGreen.Checked = buttonXThemeColorGreen.Enabled;
					break;
				case "orange":
					buttonXThemeColorOrange.Checked = buttonXThemeColorOrange.Enabled;
					break;
				case "teal":
					buttonXThemeColorTeal.Checked = buttonXThemeColorTeal.Enabled;
					break;
			}
			checkEditThemeColorApplyForAll.Checked = OutputData.ApplyForAllThemeColor;
			#endregion

			#region Logo
			buttonXLogo.Enabled = ListManager.Instance.DefaultCalendarViewSettings.EnableLogo; ;
			buttonXLogo.Checked = OutputData.ShowLogo && buttonXLogo.Enabled;
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

			#region Info
			OutputData.ShowSection = buttonXShowSection.Checked;
			OutputData.ShowCost = buttonXShowCost.Checked;
			OutputData.ShowColor = buttonXShowColor.Checked;
			OutputData.ShowBigDate = buttonXShowBigDate.Checked;
			OutputData.ShowAdSize = buttonXShowAdSize.Checked;
			OutputData.ShowPageSize = buttonXShowPageSize.Checked;
			OutputData.ShowPercentOfPage = buttonXShowPersentOfPage.Checked;
			OutputData.ShowAbbreviationOnly = buttonXShowAbbreviation.Checked;

			OutputData.ApplyForAllBasic = checkEditInfoApplyForAll.Checked;
			foreach (var outputData in _month.Parent.Months.Select(m => m.OutputData).OfType<PublicationCalendarOutputData>())
			{
				if (outputData == OutputData) continue;
				outputData.ApplyForAllBasic = OutputData.ApplyForAllBasic;
				if (!OutputData.ApplyForAllBasic) continue;
				outputData.ShowSection = OutputData.ShowSection;
				outputData.ShowCost = OutputData.ShowCost;
				outputData.ShowColor = OutputData.ShowColor;
				outputData.ShowBigDate = OutputData.ShowBigDate;
				outputData.ShowAdSize = OutputData.ShowAdSize;
				outputData.ShowPageSize = OutputData.ShowPageSize;
				outputData.ShowPercentOfPage = OutputData.ShowPercentOfPage;
				outputData.ShowAbbreviationOnly = OutputData.ShowAbbreviationOnly;
			}

			OutputData.ShowCustomComment = checkEditShowComment.Checked;
			OutputData.CustomComment = memoEditNotesCustomComment.EditValue != null && OutputData.ShowCustomComment ? memoEditNotesCustomComment.EditValue.ToString() : string.Empty;
			OutputData.ApplyForAllCustomComment = checkEditNotesCustomCommentApplyFoAll.Checked;
			foreach (var outputData in _month.Parent.Months.Select(m => m.OutputData).OfType<PublicationCalendarOutputData>())
			{
				if (outputData == OutputData) continue;
				outputData.ApplyForAllCustomComment = OutputData.ApplyForAllCustomComment;
				if (!OutputData.ApplyForAllCustomComment) continue;
				outputData.ShowCustomComment = OutputData.ShowCustomComment;
				outputData.CustomComment = OutputData.CustomComment;
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
			foreach (var outputData in _month.Parent.Months.Select(m => m.OutputData).OfType<PublicationCalendarOutputData>())
			{
				if (outputData == OutputData) continue;
				outputData.ApplyForAllThemeColor = OutputData.ApplyForAllThemeColor;
				if (OutputData.ApplyForAllThemeColor)
					outputData.SlideColor = OutputData.SlideColor;
			}
			#endregion

			#region Logo
			OutputData.ShowLogo = buttonXLogo.Checked;
			var selecteImageSource = layoutViewLogoGallery.GetFocusedRow() as ImageSource;
			OutputData.Logo = OutputData.ShowLogo && selecteImageSource != null ? selecteImageSource.BigImage : null;
			OutputData.EncodedLogo = null;
			OutputData.ApplyForAllLogo = checkEditLogoApplyForAll.Checked;
			foreach (var outputData in _month.Parent.Months.Select(m => m.OutputData).OfType<PublicationCalendarOutputData>())
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

		private void propertiesControl_PropertiesChanged(object sender, EventArgs e)
		{
			SettingsNotSaved = true;
		}
		#region Info Event Handlers
		private void buttonXShowProperty_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			OutputData.ShowSection = buttonXShowSection.Checked;
			OutputData.ShowCost = buttonXShowCost.Checked;
			OutputData.ShowColor = buttonXShowColor.Checked;
			OutputData.ShowBigDate = buttonXShowBigDate.Checked;
			OutputData.ShowAdSize = buttonXShowAdSize.Checked;
			OutputData.ShowPageSize = buttonXShowPageSize.Checked;
			OutputData.ShowPercentOfPage = buttonXShowPersentOfPage.Checked;
			OutputData.ShowAbbreviationOnly = buttonXShowAbbreviation.Checked;
			OnPropertyChanged(EventArgs.Empty);
		}

		private void ShowComment_CheckedChanged(object sender, EventArgs e)
		{
			memoEditNotesCustomComment.Enabled = checkEditShowComment.Checked;
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
			OnPropertyChanged(EventArgs.Empty);
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