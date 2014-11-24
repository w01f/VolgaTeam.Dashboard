using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraGrid.Views.Layout;
using NewBizWiz.Calendar.Controls.PresentationClasses.SlideInfo;
using NewBizWiz.CommonGUI.RetractableBar;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;

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
			buttonXThemeColorBlack.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXThemeColorBlue.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXThemeColorGray.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXThemeColorGreen.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXThemeColorOrange.CheckedChanged += propertiesControl_PropertiesChanged;
			buttonXThemeColorTeal.CheckedChanged += propertiesControl_PropertiesChanged;
			checkEditThemeColorApplyForAll.CheckedChanged += propertiesControl_PropertiesChanged;
			checkEditStyleBigDate.CheckedChanged += propertiesControl_PropertiesChanged;
			#endregion

			#region Logo
			xtraTabPageStyleLogo.PageEnabled = MediaMetaData.Instance.ListManager.Images.Any();
			buttonXLogo.CheckedChanged += propertiesControl_PropertiesChanged;
			checkEditLogoApplyForAll.CheckedChanged += propertiesControl_PropertiesChanged;
			layoutViewLogoGallery.FocusedRowChanged += propertiesControl_PropertiesChanged;
			gridControlLogoGallery.DataSource = MediaMetaData.Instance.ListManager.Images;
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

		[Browsable(true)]
		[Category("Action")]
		public event EventHandler<EventArgs> Reset;

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


			checkEditStyleBigDate.Checked = _month.OutputData.ShowBigDate;
			#endregion

			#region Logo
			buttonXLogo.Checked = _month.OutputData.ShowLogo;
			checkEditLogoApplyForAll.Checked = _month.OutputData.ApplyForAllLogo;
			var selectedLogo = MediaMetaData.Instance.ListManager.Images.FirstOrDefault(l => l.EncodedBigImage.Equals(_month.OutputData.EncodedLogo));
			if (selectedLogo != null)
			{
				var index = MediaMetaData.Instance.ListManager.Images.IndexOf(selectedLogo);
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
			var selecteImageSource = layoutViewLogoGallery.GetFocusedRow() as ImageSource;
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

		#region Comment Event Handlers
		private void buttonXComment_CheckedChanged(object sender, EventArgs e)
		{
			memoEditComment.Enabled = buttonXComment.Checked;
			if (!_allowToSave) return;
			if (!buttonXComment.Checked)
				memoEditComment.EditValue = null;
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

		private void hyperLinkEditReset_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Are you SURE you want to RESET your calendar to the default Information?") == DialogResult.Yes)
			{
				if (Reset != null)
					Reset(this, EventArgs.Empty);
			}
			e.Handled = true;
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
			if (view.FocusedRowHandle != e.RowHandle) return;
			e.Appearance.BackColor = Color.Orange;
			e.Appearance.BackColor2 = Color.Orange;
		}

		private void layoutViewLogoGallery_MouseMove(object sender, MouseEventArgs e)
		{
			layoutViewLogoGallery.Focus();
		}
		#endregion
	}
}