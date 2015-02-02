using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraGrid.Views.Layout;
using NewBizWiz.Calendar.Controls.Properties;
using NewBizWiz.CommonGUI.RetractableBar;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Calendar.Controls.PresentationClasses.SlideInfo
{
	public partial class SlideInfoControl : UserControl, ISlideInfoControl
	{
		private bool _allowToSave;
		private CalendarMonth _month;

		public SlideInfoControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			favoriteImagesControl.Init();

			#region Assign Properties Changed Event To Controls

			#region Comments
			buttonXComment.CheckedChanged += propertiesControl_PropertiesChanged;
			checkEditCommentApplyForAll.CheckedChanged += propertiesControl_PropertiesChanged;
			memoEditComment.EditValueChanged += propertiesControl_PropertiesChanged;
			memoEditComment.Enter += Utilities.Instance.Editor_Enter;
			memoEditComment.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEditComment.MouseUp += Utilities.Instance.Editor_MouseUp;
			#endregion

			#region Style
			buttonXThemeColorBlack.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXThemeColorBlue.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXThemeColorGray.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXThemeColorGreen.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXThemeColorOrange.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXThemeColorTeal.CheckedChanged += propertiesControl_PropertiesChanged;
			checkEditStyleBigDate.CheckedChanged += propertiesControl_PropertiesChanged;
			checkEditThemeColorApplyForAll.CheckedChanged += propertiesControl_PropertiesChanged;
			#endregion

			#region Logo
			buttonXLogo.CheckedChanged += propertiesControl_PropertiesChanged;
			checkEditLogoApplyForAll.CheckedChanged += propertiesControl_PropertiesChanged;
			layoutViewLogoGallery.FocusedRowChanged += propertiesControl_PropertiesChanged;
			gridControlLogoGallery.DataSource = Core.AdSchedule.ListManager.Instance.Images.SelectMany(g => g.Images).ToList();
			#endregion

			#endregion
		}

		public string MonthTitle { get; set; }
		public bool SettingsNotSaved { get; set; }

		protected CommonCalendarOutputData OutputData
		{
			get { return _month != null ? _month.OutputData as CommonCalendarOutputData : null; }
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

		public IEnumerable<ButtonInfo> GetChapters()
		{
			return new[]
			{
				new ButtonInfo
				{
					Logo = Resources.CalendarOptionsFavorites,
					Tooltip = "Open My Gallery",
					Action = () => { xtraTabControl.SelectedTabPage = xtraTabPageFavorites; }
				},
				new ButtonInfo
				{
					Logo = Resources.CalendarOptionsStyle,
					Tooltip = "Open Slide Style",
					Action = () => { xtraTabControl.SelectedTabPage = xtraTabPageStyle; }
				},
				new ButtonInfo
				{
					Logo = Resources.CalendarOptionsComments,
					Tooltip = "Open Comments",
					Action = () => { xtraTabControl.SelectedTabPage = xtraTabPageComments; }
				},
			};
		}

		public void LoadCurrentMonthData()
		{
			if (_month == null) return;
			_allowToSave = false;
			var isFirstMonth = _month.Parent.Months.OrderBy(m => m.DaysRangeBegin).FirstOrDefault() == _month;
			MonthTitle = "Slide Info - " + _month.Date.ToString("MMMM yyyy");
			laCommentMonth.Text = _month.Date.ToString("MMMM, yyyy");

			#region Comments
			buttonXComment.Enabled = Core.AdSchedule.ListManager.Instance.DefaultCalendarViewSettings.EnableComments;
			buttonXComment.Checked = OutputData.ShowCustomComment && buttonXComment.Enabled;
			memoEditComment.EditValue = OutputData.CustomComment;
			checkEditCommentApplyForAll.Visible = isFirstMonth;
			buttonXComment.Enabled = isFirstMonth || !_month.OutputData.ApplyForAllCustomComment;
			#endregion

			#region Style
			buttonXThemeColorBlack.Enabled = Core.AdSchedule.ListManager.Instance.DefaultCalendarViewSettings.EnableBlack;
			buttonXThemeColorBlue.Enabled = Core.AdSchedule.ListManager.Instance.DefaultCalendarViewSettings.EnableBlue;
			buttonXThemeColorGray.Enabled = Core.AdSchedule.ListManager.Instance.DefaultCalendarViewSettings.EnableGray;
			buttonXThemeColorGreen.Enabled = Core.AdSchedule.ListManager.Instance.DefaultCalendarViewSettings.EnableGreen;
			buttonXThemeColorOrange.Enabled = Core.AdSchedule.ListManager.Instance.DefaultCalendarViewSettings.EnableOrange;
			buttonXThemeColorTeal.Enabled = Core.AdSchedule.ListManager.Instance.DefaultCalendarViewSettings.EnableTeal;
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
			checkEditStyleBigDate.Enabled = Core.AdSchedule.ListManager.Instance.DefaultCalendarViewSettings.EnableBigDate;
			checkEditStyleBigDate.Checked = OutputData.ShowBigDate && checkEditStyleBigDate.Enabled;
			checkEditThemeColorApplyForAll.Checked = OutputData.ApplyForAllThemeColor;
			#endregion

			#region Logo
			buttonXLogo.Enabled = Core.AdSchedule.ListManager.Instance.DefaultCalendarViewSettings.EnableLogo; ;
			buttonXLogo.Checked = OutputData.ShowLogo && buttonXLogo.Enabled;
			checkEditLogoApplyForAll.Checked = OutputData.ApplyForAllLogo;
			var selectedLogo = Core.AdSchedule.ListManager.Instance.Images.SelectMany(g => g.Images).FirstOrDefault(l => l.EncodedBigImage.Equals(OutputData.EncodedLogo));
			if (selectedLogo != null)
			{
				var index = Core.AdSchedule.ListManager.Instance.Images.SelectMany(g => g.Images).ToList().IndexOf(selectedLogo);
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

			#region Commemts
			OutputData.ShowCustomComment = buttonXComment.Checked;
			OutputData.CustomComment = memoEditComment.EditValue != null && OutputData.ShowCustomComment ? memoEditComment.EditValue.ToString() : string.Empty;
			OutputData.ApplyForAllCustomComment = checkEditCommentApplyForAll.Checked;
			foreach (var outputData in _month.Parent.Months.Select(m => m.OutputData).OfType<CommonCalendarOutputData>())
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
			OutputData.ShowBigDate = checkEditStyleBigDate.Checked;
			OutputData.ApplyForAllThemeColor = checkEditThemeColorApplyForAll.Checked;
			foreach (var outputData in _month.Parent.Months.Select(m => m.OutputData).OfType<CommonCalendarOutputData>())
			{
				if (outputData == OutputData) continue;
				outputData.ApplyForAllThemeColor = OutputData.ApplyForAllThemeColor;
				if (OutputData.ApplyForAllThemeColor)
				{
					outputData.ShowBigDate = OutputData.ShowBigDate;
					outputData.SlideColor = OutputData.SlideColor;
				}
			}
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

		private void propertiesControl_PropertiesChanged(object sender, EventArgs e)
		{
			SettingsNotSaved = true;
		}
		#region Info Event Handlers
		private void ShowComment_CheckedChanged(object sender, EventArgs e)
		{
			memoEditComment.Enabled = buttonXComment.Checked;
		}
		#endregion

		#region Style Event Handlers
		private void checkEditStyleBigDate_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			OutputData.ShowBigDate = checkEditStyleBigDate.Checked;
		}

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