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
using NewBizWiz.Calendar.Controls.PresentationClasses.SlideInfo;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses
{
	public partial class CalendarSlideInfoControl : UserControl, ISlideInfoControl
	{
		private bool _allowToSave;
		private string _helpKey = "info";
		private CalendarMonth _month;

		public CalendarSlideInfoControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			#region Assign Properties Changed Event To Controls

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
				var selectedLogo = Core.AdSchedule.ListManager.Instance.Images.FirstOrDefault(l => l.EncodedBigImage.Equals(_month.OutputData.EncodedLogo));
				if (selectedLogo != null)
				{
					var index = Core.AdSchedule.ListManager.Instance.Images.IndexOf(selectedLogo);
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
			if (xtraTabControl.SelectedTabPage == xtraTabPageStyle)
				_helpKey = "style";
			else if (xtraTabControl.SelectedTabPage == xtraTabPageLogo)
				_helpKey = "logo";
			else
				_helpKey = string.Empty;
		}

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