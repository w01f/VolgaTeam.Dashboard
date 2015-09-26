using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.Calendar.Controls.BusinessClasses;
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
		private readonly List<ImageSource> _imageSources = new List<ImageSource>();

		public SlideInfoControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			favoriteImagesControl.Init();

			_imageSources.AddRange(Core.AdSchedule.ListManager.Instance.Images.SelectMany(g => g.Images).OrderByDescending(i => i.IsDefault).ThenBy(i => i.Name));

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
			checkEditStyleBigDate.CheckedChanged += propertiesControl_PropertiesChanged;
			checkEditThemeColorApplyForAll.CheckedChanged += propertiesControl_PropertiesChanged;
			#endregion

			#region Logo
			checkEditShowLogo.CheckedChanged += propertiesControl_PropertiesChanged;
			checkEditLogoApplyForAll.CheckedChanged += propertiesControl_PropertiesChanged;
			calendarHeaderSelector.SelectionChanged += propertiesControl_PropertiesChanged;
			calendarHeaderSelector.LoadData(_imageSources);
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
			outputColorSelector.InitData(BusinessWrapper.Instance.OutputManager.CalendarColors, OutputData.SlideColor);
			outputColorSelector.ColorChanged += OnColorChanged;
			checkEditStyleBigDate.Enabled = Core.AdSchedule.ListManager.Instance.DefaultCalendarViewSettings.EnableBigDate;
			checkEditStyleBigDate.Checked = OutputData.ShowBigDate && checkEditStyleBigDate.Enabled;
			checkEditThemeColorApplyForAll.Checked = OutputData.ApplyForAllThemeColor;
			#endregion

			#region Logo
			checkEditShowLogo.Enabled = Core.AdSchedule.ListManager.Instance.DefaultCalendarViewSettings.EnableLogo; ;
			checkEditShowLogo.Checked = OutputData.ShowLogo && checkEditShowLogo.Enabled;
			checkEditLogoApplyForAll.Checked = OutputData.ApplyForAllLogo;
			var selectedLogo =
				_imageSources.FirstOrDefault(l => l.EncodedBigImage.Equals(_month.OutputData.EncodedLogo)) ??
				_imageSources.FirstOrDefault();
			calendarHeaderSelector.SelectedImageSource = selectedLogo;
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
			OutputData.SlideColor = outputColorSelector.SelectedColor;
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
			OutputData.ShowLogo = checkEditShowLogo.Checked;
			var selecteImageSource = calendarHeaderSelector.SelectedImageSource;
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
			if (!_allowToSave) return;
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

		private void OnColorChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			OutputData.SlideColor = outputColorSelector.SelectedColor;
			SettingsNotSaved = true;
			if (PropertyChanged != null)
				PropertyChanged(sender, e);
		}
		#endregion

		#region Logo Event Handlers
		private void buttonXLogo_CheckedChanged(object sender, EventArgs e)
		{
			calendarHeaderSelector.Enabled = checkEditShowLogo.Checked;
		}
		#endregion
	}
}