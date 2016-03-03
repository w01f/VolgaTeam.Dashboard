using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Business.Media.Configuration;
using Asa.Calendar.Controls.PresentationClasses.SlideInfo;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Images;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.BusinessClasses;

namespace Asa.Media.Controls.PresentationClasses.Calendar
{
	public partial class CalendarSlideInfoControl : UserControl, ISlideInfoControl
	{
		protected bool _allowToSave;
		private readonly List<ImageSource> _imageSources = new List<ImageSource>();

		protected CalendarMonth Month { get; set; }
		public string MonthTitle { get; set; }
		public bool SettingsNotSaved { get; set; }

		[Browsable(true)]
		[Category("Action")]
		public event EventHandler Closed;

		[Browsable(true)]
		[Category("Action")]
		public event EventHandler<EventArgs> PropertyChanged;

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
			memoEditComment.EnableSelectAll();
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

			BusinessObjects.Instance.OutputManager.ColorsChanged += (o, e) => InitColorControls();
		}

		public void OnPropertyChanged(EventArgs e)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, e);
		}

		public void LoadMonth(CalendarMonth month)
		{
			Month = month;
			LoadCurrentMonthData();
		}

		public virtual IEnumerable<ButtonInfo> GetChapters()
		{
			return new[]
			{
				new ButtonInfo
				{
					Logo = Asa.Calendar.Controls.Properties.Resources.CalendarOptionsFavorites,
					Tooltip = "Open My Gallery",
					Action = () => { xtraTabControl.SelectedTabPage = xtraTabPageFavorites; }
				},
				new ButtonInfo
				{
					Logo = Asa.Calendar.Controls.Properties.Resources.CalendarOptionsStyle,
					Tooltip = "Open Slide Style",
					Action = () => { xtraTabControl.SelectedTabPage = xtraTabPageStyle; }
				},
				new ButtonInfo
				{
					Logo = Asa.Calendar.Controls.Properties.Resources.CalendarOptionsComments,
					Tooltip = "Open Comments",
					Action = () => { xtraTabControl.SelectedTabPage = xtraTabPageComments; }
				},
			};
		}

		public virtual void LoadCurrentMonthData()
		{
			if (Month == null) return;
			_allowToSave = false;

			var isFirstMonth = Month.Parent.Months.OrderBy(m => m.DaysRangeBegin).FirstOrDefault() == Month;
			MonthTitle = "Slide Info - " + Month.Date.ToString("MMMM yyyy");
			laCommentMonth.Text = Month.Date.ToString("MMMM, yyyy");

			#region Comment
			buttonXComment.Checked = Month.OutputData.ShowCustomComment;
			memoEditComment.EditValue = Month.OutputData.CustomComment;
			checkEditCommentApplyForAll.Checked = Month.OutputData.ApplyForAllCustomComment;
			checkEditCommentApplyForAll.Visible = isFirstMonth;
			buttonXComment.Enabled = isFirstMonth || !Month.OutputData.ApplyForAllCustomComment;
			memoEditComment.Enabled = Month.OutputData.ShowCustomComment && (isFirstMonth || !Month.OutputData.ApplyForAllCustomComment);
			#endregion

			#region Style
			InitColorControls();
			checkEditThemeColorApplyForAll.Checked = Month.OutputData.ApplyForAllThemeColor;
			checkEditStyleBigDate.Checked = Month.OutputData.ShowBigDate;
			#endregion

			#region Logo
			checkEditShowLogo.Checked = Month.OutputData.ShowLogo;
			checkEditLogoApplyForAll.Checked = Month.OutputData.ApplyForAllLogo;
			var selectedLogo =
				_imageSources.FirstOrDefault(l => Month.OutputData.Logo != null && l.FileName == Month.OutputData.Logo.FileName) ??
				_imageSources.FirstOrDefault();
			calendarHeaderSelector.SelectedImageSource = selectedLogo;
			#endregion

			_allowToSave = true;
			SettingsNotSaved = false;
		}

		public virtual void SaveData()
		{
			if (!_allowToSave || Month == null) return;

			#region Comment
			Month.OutputData.ShowCustomComment = buttonXComment.Checked;
			Month.OutputData.CustomComment = memoEditComment.EditValue as String;
			Month.OutputData.ApplyForAllCustomComment = checkEditCommentApplyForAll.Checked;
			foreach (var month in Month.Parent.Months.Where(month => month != Month))
			{
				month.OutputData.ApplyForAllCustomComment = Month.OutputData.ApplyForAllCustomComment;
				if (!Month.OutputData.ApplyForAllCustomComment) continue;
				month.OutputData.ShowCustomComment = Month.OutputData.ShowCustomComment;
				month.OutputData.CustomComment = Month.OutputData.CustomComment;
			}
			#endregion

			#region Style
			Month.OutputData.SlideColor = outputColorSelector.SelectedColor;
			Month.OutputData.ApplyForAllThemeColor = checkEditThemeColorApplyForAll.Checked;
			Month.OutputData.ShowBigDate = checkEditStyleBigDate.Checked;
			foreach (var month in Month.Parent.Months.Where(month => month != Month))
			{
				month.OutputData.ApplyForAllThemeColor = Month.OutputData.ApplyForAllThemeColor;
				if (!Month.OutputData.ApplyForAllThemeColor) continue;
				month.OutputData.SlideColor = Month.OutputData.SlideColor;
				month.OutputData.ShowBigDate = Month.OutputData.ShowBigDate;
			}
			#endregion

			#region Logo
			Month.OutputData.ShowLogo = checkEditShowLogo.Checked;
			var selecteImageSource = calendarHeaderSelector.SelectedImageSource;
			Month.OutputData.Logo = Month.OutputData.ShowLogo && selecteImageSource != null ? selecteImageSource.Clone<ImageSource, ImageSource>() : null;
			Month.OutputData.ApplyForAllLogo = checkEditLogoApplyForAll.Checked;
			foreach (var month in Month.Parent.Months.Where(month => month != Month))
			{
				month.OutputData.ApplyForAllLogo = Month.OutputData.ApplyForAllLogo;
				if (!Month.OutputData.ApplyForAllLogo) continue;
				month.OutputData.ShowLogo = Month.OutputData.ShowLogo;
				month.OutputData.Logo = Month.OutputData.Logo.Clone<ImageSource, ImageSource>();
			}
			#endregion

			SettingsNotSaved = false;
		}

		public virtual void Release()
		{
			_allowToSave = false;
			Month = null;
		}

		private void InitColorControls()
		{
			if (Month == null) return;
			outputColorSelector.InitData(BusinessObjects.Instance.OutputManager.CalendarColors, Month.OutputData.SlideColor);
			outputColorSelector.ColorChanged += OnColorChanged;
		}

		protected void OnPropertiesChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			SettingsNotSaved = true;
			OnPropertyChanged(EventArgs.Empty);
		}

		private void OnColorChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			Month.OutputData.SlideColor = outputColorSelector.SelectedColor;
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