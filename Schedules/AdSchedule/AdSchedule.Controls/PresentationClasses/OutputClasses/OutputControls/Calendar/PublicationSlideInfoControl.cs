using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Asa.AdSchedule.Controls.BusinessClasses;
using Asa.AdSchedule.Controls.Properties;
using Asa.Calendar.Controls.PresentationClasses.SlideInfo;
using Asa.CommonGUI.Common;
using Asa.CommonGUI.RetractableBar;
using Asa.Core.AdSchedule;
using Asa.Core.Calendar;
using Asa.Core.Common;
using ListManager = Asa.Core.AdSchedule.ListManager;

namespace Asa.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls.Calendar
{
	public partial class PublicationSlideInfoControl : UserControl, ISlideInfoControl
	{
		private bool _allowToSave;
		private CalendarMonth _month;
		private readonly List<ImageSource> _imageSources = new List<ImageSource>();

		public PublicationSlideInfoControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			favoriteImagesControl.Init();

			_imageSources.AddRange(ListManager.Instance.Images.SelectMany(g => g.Images).OrderByDescending(i => i.IsDefault).ThenBy(i => i.Name));

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
			memoEditNotesCustomComment.Enter += TextEditorsHelper.Editor_Enter;
			memoEditNotesCustomComment.MouseDown += TextEditorsHelper.Editor_MouseDown;
			memoEditNotesCustomComment.MouseUp += TextEditorsHelper.Editor_MouseUp;
			#endregion

			#region Style
			checkEditThemeColorApplyForAll.CheckedChanged += propertiesControl_PropertiesChanged;
			#endregion

			#region Logo
			checkEditShowLogo.CheckedChanged += propertiesControl_PropertiesChanged;
			checkEditLogoApplyForAll.CheckedChanged += propertiesControl_PropertiesChanged;
			calendarHeaderSelector.SelectionChanged += propertiesControl_PropertiesChanged;
			calendarHeaderSelector.LoadData(_imageSources);
			#endregion

			#endregion

			BusinessObjects.Instance.OutputManager.ColorsChanged += (o, e) => InitColorControls();
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

		public IEnumerable<ButtonInfo> GetChapters()
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
					Logo = Resources.CalendarOptionsInfo,
					Tooltip = "Open Info",
					Action = () => { xtraTabControl.SelectedTabPage = xtraTabPageInfo; }
				},
			};
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
			InitColorControls();
			checkEditThemeColorApplyForAll.Checked = OutputData.ApplyForAllThemeColor;
			#endregion

			#region Logo
			checkEditShowLogo.Enabled = ListManager.Instance.DefaultCalendarViewSettings.EnableLogo; ;
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
			OutputData.SlideColor = outputColorSelector.SelectedColor;
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
			OutputData.ShowLogo = checkEditShowLogo.Checked;
			var selecteImageSource = calendarHeaderSelector.SelectedImageSource;
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

		private void InitColorControls()
		{
			if (_month == null) return;
			outputColorSelector.InitData(BusinessObjects.Instance.OutputManager.CalendarColors, OutputData.SlideColor);
			outputColorSelector.ColorChanged += OnColorChanged;
		}

		private void propertiesControl_PropertiesChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			SettingsNotSaved = true;
		}

		private void OnColorChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			OutputData.SlideColor = outputColorSelector.SelectedColor;
			SettingsNotSaved = true;
			OnPropertyChanged(EventArgs.Empty);
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

		#region Logo Event Handlers
		private void buttonXLogo_CheckedChanged(object sender, EventArgs e)
		{
			calendarHeaderSelector.Enabled = checkEditShowLogo.Checked;
		}
		#endregion


	}
}