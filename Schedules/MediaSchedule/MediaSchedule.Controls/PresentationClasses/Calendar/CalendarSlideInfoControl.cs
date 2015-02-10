using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Manina.Windows.Forms;
using NewBizWiz.Calendar.Controls.PresentationClasses.SlideInfo;
using NewBizWiz.CommonGUI.RetractableBar;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses
{
	public sealed partial class CalendarSlideInfoControl : UserControl, ISlideInfoControl
	{
		private bool _allowToSave;
		private CalendarMonth _month;

		public CalendarSlideInfoControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			favoriteImagesControl.Init();

			#region Assign Properties Changed Event To Controls

			#region Comment
			buttonXComment.CheckedChanged += propertiesControl_PropertiesChanged;
			memoEditComment.EditValueChanged += propertiesControl_PropertiesChanged;
			memoEditComment.Enter += Utilities.Instance.Editor_Enter;
			memoEditComment.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEditComment.MouseUp += Utilities.Instance.Editor_MouseUp;
			checkEditCommentApplyForAll.CheckedChanged += propertiesControl_PropertiesChanged;
			#endregion

			#region Style
			checkEditThemeColorApplyForAll.CheckedChanged += propertiesControl_PropertiesChanged;
			checkEditStyleBigDate.CheckedChanged += propertiesControl_PropertiesChanged;
			#endregion

			#region Logo
			xtraTabPageStyleLogo.PageEnabled = MediaMetaData.Instance.ListManager.Images.Any();
			buttonXLogo.CheckedChanged += propertiesControl_PropertiesChanged;
			checkEditLogoApplyForAll.CheckedChanged += propertiesControl_PropertiesChanged;
			imageListViewHeaderLogo.SelectionChanged += propertiesControl_PropertiesChanged;
			imageListViewHeaderLogo.Items.Clear();
			imageListViewHeaderLogo.Items.AddRange(MediaMetaData.Instance.ListManager.Images.SelectMany(g => g.Images).Select(ims => new ImageListViewItem(ims.FileName, ims.Name) { Tag = ims }).ToArray());
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

		public void OnThemeChanged(EventArgs e)
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

		public void LoadCurrentMonthData()
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
			buttonXLogo.Checked = _month.OutputData.ShowLogo;
			checkEditLogoApplyForAll.Checked = _month.OutputData.ApplyForAllLogo;
			var selectedLogo = MediaMetaData.Instance.ListManager.Images.SelectMany(g => g.Images).FirstOrDefault(l => l.EncodedBigImage.Equals(_month.OutputData.EncodedLogo));
			imageListViewHeaderLogo.ClearSelection();
			if (selectedLogo != null)
			{
				var index = MediaMetaData.Instance.ListManager.Images.SelectMany(g => g.Images).ToList().IndexOf(selectedLogo);
				if (index < imageListViewHeaderLogo.Items.Count)
					imageListViewHeaderLogo.Items[index].Selected = true;
			}
			else if (imageListViewHeaderLogo.Items.Count > 0)
				imageListViewHeaderLogo.Items[0].Selected = true;
			#endregion

			_allowToSave = true;
			SettingsNotSaved = false;
		}

		public void SaveData()
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
			_month.OutputData.ShowLogo = buttonXLogo.Checked;
			var selecteImageSource = imageListViewHeaderLogo.SelectedItems.Select(item => item.Tag as ImageSource).FirstOrDefault();
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

		private void propertiesControl_PropertiesChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			SettingsNotSaved = true;
		}

		private void OnColorChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_month.OutputData.SlideColor = outputColorSelector.SelectedColor;
			SettingsNotSaved = true;
			if (PropertyChanged != null)
				PropertyChanged(sender, e);
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
		private void buttonXLogo_CheckedChanged(object sender, EventArgs e)
		{
			imageListViewHeaderLogo.Enabled = buttonXLogo.Checked;
		}

		private void imageListViewHeaderLogo_MouseMove(object sender, MouseEventArgs e)
		{
			imageListViewHeaderLogo.Focus();
		}
		#endregion


	}
}