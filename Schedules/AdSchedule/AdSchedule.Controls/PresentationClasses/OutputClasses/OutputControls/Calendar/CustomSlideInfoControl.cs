using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Manina.Windows.Forms;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.Calendar.Controls.PresentationClasses.SlideInfo;
using NewBizWiz.CommonGUI.RetractableBar;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;
using ListManager = NewBizWiz.Core.AdSchedule.ListManager;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls.Calendar
{
	public partial class CustomSlideInfoControl : UserControl, ISlideInfoControl
	{
		private bool _allowToSave;
		private CalendarMonth _month;

		public CustomSlideInfoControl()
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
			checkEditStyleBigDate.CheckedChanged += propertiesControl_PropertiesChanged;
			checkEditThemeColorApplyForAll.CheckedChanged += propertiesControl_PropertiesChanged;
			#endregion

			#region Logo
			buttonXLogo.CheckedChanged += propertiesControl_PropertiesChanged;
			checkEditLogoApplyForAll.CheckedChanged += propertiesControl_PropertiesChanged;
			imageListViewHeaderLogo.SelectionChanged += propertiesControl_PropertiesChanged;
			imageListViewHeaderLogo.Items.Clear();
			imageListViewHeaderLogo.Items.AddRange(ListManager.Instance.Images.SelectMany(g => g.Images).Select(ims => new ImageListViewItem(ims.FileName, ims.Name) { Tag = ims }).ToArray());
			#endregion

			#endregion
		}

		public string MonthTitle { get; set; }
		public bool SettingsNotSaved { get; set; }

		protected CustomCalendarOutputData OutputData
		{
			get { return _month != null ? _month.OutputData as CustomCalendarOutputData : null; }
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

			#region Comments
			buttonXComment.Enabled = ListManager.Instance.DefaultCalendarViewSettings.EnableComments;
			buttonXComment.Checked = OutputData.ShowCustomComment && buttonXComment.Enabled;
			memoEditComment.EditValue = OutputData.CustomComment;
			checkEditCommentApplyForAll.Visible = isFirstMonth;
			buttonXComment.Enabled = isFirstMonth || !_month.OutputData.ApplyForAllCustomComment;
			#endregion

			#region Style
			outputColorSelector.InitData(BusinessWrapper.Instance.OutputManager.CalendarColors, OutputData.SlideColor);
			outputColorSelector.ColorChanged += OnColorChanged;
			checkEditStyleBigDate.Enabled = ListManager.Instance.DefaultCalendarViewSettings.EnableBigDate;
			checkEditStyleBigDate.Checked = OutputData.ShowBigDate && checkEditStyleBigDate.Enabled;
			checkEditThemeColorApplyForAll.Checked = OutputData.ApplyForAllThemeColor;
			#endregion

			#region Logo
			buttonXLogo.Enabled = ListManager.Instance.DefaultCalendarViewSettings.EnableLogo; ;
			buttonXLogo.Checked = OutputData.ShowLogo && buttonXLogo.Enabled;
			checkEditLogoApplyForAll.Checked = OutputData.ApplyForAllLogo;
			var selectedLogo = ListManager.Instance.Images.SelectMany(g => g.Images).FirstOrDefault(l => l.EncodedBigImage.Equals(OutputData.EncodedLogo));
			imageListViewHeaderLogo.ClearSelection();
			if (selectedLogo != null)
			{
				var index = ListManager.Instance.Images.SelectMany(g => g.Images).ToList().IndexOf(selectedLogo);
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

			#region Commemts
			OutputData.ShowCustomComment = buttonXComment.Checked;
			OutputData.CustomComment = memoEditComment.EditValue != null && OutputData.ShowCustomComment ? memoEditComment.EditValue.ToString() : string.Empty;
			OutputData.ApplyForAllCustomComment = checkEditCommentApplyForAll.Checked;
			foreach (var outputData in _month.Parent.Months.Select(m => m.OutputData).OfType<CustomCalendarOutputData>())
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
			foreach (var outputData in _month.Parent.Months.Select(m => m.OutputData).OfType<CustomCalendarOutputData>())
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
			var selecteImageSource = imageListViewHeaderLogo.SelectedItems.Select(item => item.Tag as ImageSource).FirstOrDefault();
			OutputData.Logo = OutputData.ShowLogo && selecteImageSource != null ? selecteImageSource.BigImage : null;
			OutputData.EncodedLogo = null;
			OutputData.ApplyForAllLogo = checkEditLogoApplyForAll.Checked;
			foreach (var outputData in _month.Parent.Months.Select(m => m.OutputData).OfType<CustomCalendarOutputData>())
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
			imageListViewHeaderLogo.Enabled = buttonXLogo.Checked;
		}

		private void imageListViewHeaderLogo_MouseMove(object sender, MouseEventArgs e)
		{
			imageListViewHeaderLogo.Focus();
		}
		#endregion


	}
}