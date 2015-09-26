using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.Calendar.Controls.PresentationClasses.SlideInfo;
using NewBizWiz.CommonGUI.RetractableBar;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses
{
	public partial class CalendarSlideInfoControl : UserControl, ISlideInfoControl
	{
		protected bool _allowToSave;
		protected CalendarMonth _month;
		private readonly List<ImageSource> _imageSources = new List<ImageSource>();

		public CalendarSlideInfoControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			xtraTabPageData.PageVisible = false;

			favoriteImagesControl.Init();

			_imageSources.AddRange(MediaMetaData.Instance.ListManager.Images.SelectMany(g => g.Images).OrderByDescending(i => i.IsDefault).ThenBy(i => i.Name));

			#region Assign Properties Changed Event To Controls

			#region Comment
			buttonXComment.CheckedChanged += OnPropertiesChanged;
			memoEditComment.EditValueChanged += OnPropertiesChanged;
			memoEditComment.Enter += Utilities.Instance.Editor_Enter;
			memoEditComment.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEditComment.MouseUp += Utilities.Instance.Editor_MouseUp;
			checkEditCommentApplyForAll.CheckedChanged += OnPropertiesChanged;
			#endregion

			#region Style
			checkEditThemeColorApplyForAll.CheckedChanged += OnPropertiesChanged;
			checkEditStyleBigDate.CheckedChanged += OnPropertiesChanged;
			#endregion

			#region Logo
			xtraTabPageStyleLogo.PageEnabled = MediaMetaData.Instance.ListManager.Images.Any();
			checkEditShowLogo.CheckedChanged += OnPropertiesChanged;
			checkEditLogoApplyForAll.CheckedChanged += OnPropertiesChanged;
			calendarHeaderSelector.SelectionChanged += OnPropertiesChanged;
			calendarHeaderSelector.LoadData(_imageSources);
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

		public virtual IEnumerable<ButtonInfo> GetChapters()
		{
			return new[]
			{
				new ButtonInfo
				{
					Logo = NewBizWiz.Calendar.Controls.Properties.Resources.CalendarOptionsFavorites,
					Tooltip = "Open My Gallery",
					Action = () => { xtraTabControl.SelectedTabPage = xtraTabPageFavorites; }
				},
				new ButtonInfo
				{
					Logo = NewBizWiz.Calendar.Controls.Properties.Resources.CalendarOptionsStyle,
					Tooltip = "Open Slide Style",
					Action = () => { xtraTabControl.SelectedTabPage = xtraTabPageStyle; }
				},
				new ButtonInfo
				{
					Logo = NewBizWiz.Calendar.Controls.Properties.Resources.CalendarOptionsComments,
					Tooltip = "Open Comments",
					Action = () => { xtraTabControl.SelectedTabPage = xtraTabPageComments; }
				},
			};
		}

		public virtual void LoadCurrentMonthData()
		{
			if (_month == null) return;
			_allowToSave = false;

			var isFirstMonth = _month.Parent.Months.OrderBy(m => m.DaysRangeBegin).FirstOrDefault() == _month;
			MonthTitle = "Slide Info - " + _month.Date.ToString("MMMM yyyy");
			laCommentMonth.Text = _month.Date.ToString("MMMM, yyyy");

			#region Comment
			buttonXComment.Checked = _month.OutputData.ShowCustomComment;
			memoEditComment.EditValue = _month.OutputData.CustomComment;
			checkEditCommentApplyForAll.Checked = _month.OutputData.ApplyForAllCustomComment;
			checkEditCommentApplyForAll.Visible = isFirstMonth;
			buttonXComment.Enabled = isFirstMonth || !_month.OutputData.ApplyForAllCustomComment;
			memoEditComment.Enabled = _month.OutputData.ShowCustomComment && (isFirstMonth || !_month.OutputData.ApplyForAllCustomComment);
			#endregion

			#region Style
			outputColorSelector.InitData(BusinessWrapper.Instance.OutputManager.CalendarColors, _month.OutputData.SlideColor);
			outputColorSelector.ColorChanged += OnColorChanged;
			checkEditThemeColorApplyForAll.Checked = _month.OutputData.ApplyForAllThemeColor;
			checkEditStyleBigDate.Checked = _month.OutputData.ShowBigDate;
			#endregion

			#region Logo
			checkEditShowLogo.Checked = _month.OutputData.ShowLogo;
			checkEditLogoApplyForAll.Checked = _month.OutputData.ApplyForAllLogo;
			var selectedLogo =
				_imageSources.FirstOrDefault(l => l.EncodedBigImage.Equals(_month.OutputData.EncodedLogo)) ??
				_imageSources.FirstOrDefault();
			calendarHeaderSelector.SelectedImageSource = selectedLogo;
			#endregion

			_allowToSave = true;
			SettingsNotSaved = false;
		}

		public virtual void SaveData()
		{
			if (!_allowToSave) return;

			#region Comment
			_month.OutputData.ShowCustomComment = buttonXComment.Checked;
			_month.OutputData.CustomComment = memoEditComment.EditValue as String;
			_month.OutputData.ApplyForAllCustomComment = checkEditCommentApplyForAll.Checked;
			foreach (var month in _month.Parent.Months.Where(month => month != _month))
			{
				month.OutputData.ApplyForAllCustomComment = _month.OutputData.ApplyForAllCustomComment;
				if (!_month.OutputData.ApplyForAllCustomComment) continue;
				month.OutputData.ShowCustomComment = _month.OutputData.ShowCustomComment;
				month.OutputData.CustomComment = _month.OutputData.CustomComment;
			}
			#endregion

			#region Style
			_month.OutputData.SlideColor = outputColorSelector.SelectedColor;
			_month.OutputData.ApplyForAllThemeColor = checkEditThemeColorApplyForAll.Checked;
			_month.OutputData.ShowBigDate = checkEditStyleBigDate.Checked;
			foreach (var month in _month.Parent.Months.Where(month => month != _month))
			{
				month.OutputData.ApplyForAllThemeColor = _month.OutputData.ApplyForAllThemeColor;
				if (!_month.OutputData.ApplyForAllThemeColor) continue;
				month.OutputData.SlideColor = _month.OutputData.SlideColor;
				month.OutputData.ShowBigDate = _month.OutputData.ShowBigDate;
			}
			#endregion

			#region Logo
			_month.OutputData.ShowLogo = checkEditShowLogo.Checked;
			var selecteImageSource = calendarHeaderSelector.SelectedImageSource;
			_month.OutputData.Logo = _month.OutputData.ShowLogo && selecteImageSource != null ? selecteImageSource.BigImage : null;
			_month.OutputData.EncodedLogo = null;
			_month.OutputData.ApplyForAllLogo = checkEditLogoApplyForAll.Checked;
			foreach (var month in _month.Parent.Months.Where(month => month != _month))
			{
				month.OutputData.ApplyForAllLogo = _month.OutputData.ApplyForAllLogo;
				if (!_month.OutputData.ApplyForAllLogo) continue;
				month.OutputData.ShowLogo = _month.OutputData.ShowLogo;
				month.OutputData.Logo = _month.OutputData.Logo;
				month.OutputData.EncodedLogo = null;
			}
			#endregion

			SettingsNotSaved = false;
		}

		protected void OnPropertiesChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			SettingsNotSaved = true;
		}

		private void OnColorChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_month.OutputData.SlideColor = outputColorSelector.SelectedColor;
			SettingsNotSaved = true;
			OnPropertyChanged(EventArgs.Empty);
		}

		#region Comment Event Handlers
		private void buttonXComment_CheckedChanged(object sender, EventArgs e)
		{
			memoEditComment.Enabled = buttonXComment.Checked;
			if (!_allowToSave) return;
			if (!buttonXComment.Checked)
				memoEditComment.EditValue = null;
		}
		#endregion

		#region Logo Event Handlers
		private void checkEditShowLogo_CheckedChanged(object sender, EventArgs e)
		{
			calendarHeaderSelector.Enabled = checkEditShowLogo.Checked;
		}
		#endregion
	}
}