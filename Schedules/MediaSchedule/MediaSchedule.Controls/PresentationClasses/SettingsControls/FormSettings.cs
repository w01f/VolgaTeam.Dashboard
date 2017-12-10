using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Helpers;
using Asa.Media.Controls.BusinessClasses.Managers;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;

namespace Asa.Media.Controls.PresentationClasses.SettingsControls
{
	public partial class FormSettings : MetroForm
	{
		private bool _allowToSave;
		private bool _settingsChanged;
		private bool _calendarTypeChanged;
		private readonly MediaScheduleSettings _settings;

		public EventHandler<EventArgs> CalendarTypeChanged;

		public FormSettings(MediaScheduleSettings settings)
		{
			InitializeComponent();
			_settings = settings;

			pictureEditDemoLogo.Image =
				BusinessObjects.Instance.ImageResourcesManager.HomeSettingsDemoImage ?? pictureEditDemoLogo.Image;
			pictureEditDaypartsLogo.Image =
				BusinessObjects.Instance.ImageResourcesManager.HomeSettingsDaypartsImage ?? pictureEditDaypartsLogo.Image;
			pictureEditCalendarTypeLogo.Image =
				BusinessObjects.Instance.ImageResourcesManager.HomeSettingsCalendarTypeImage ?? pictureEditCalendarTypeLogo.Image;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			emptySpaceItemDemoSeparator.MaxSize = RectangleHelper.ScaleSize(emptySpaceItemDemoSeparator.MaxSize, scaleFactor);
			emptySpaceItemDemoSeparator.MinSize = RectangleHelper.ScaleSize(emptySpaceItemDemoSeparator.MinSize, scaleFactor);
			emptySpaceItemDaypartsSeparator.MaxSize = RectangleHelper.ScaleSize(emptySpaceItemDaypartsSeparator.MaxSize, scaleFactor);
			emptySpaceItemDaypartsSeparator.MinSize = RectangleHelper.ScaleSize(emptySpaceItemDaypartsSeparator.MinSize, scaleFactor);
			emptySpaceItemCalendarSeparator.MaxSize = RectangleHelper.ScaleSize(emptySpaceItemCalendarSeparator.MaxSize, scaleFactor);
			emptySpaceItemCalendarSeparator.MinSize = RectangleHelper.ScaleSize(emptySpaceItemCalendarSeparator.MinSize, scaleFactor);
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, scaleFactor);
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, scaleFactor);
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, scaleFactor);
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, scaleFactor);
		}

		private void OnFormLoad(object sender, EventArgs e)
		{
			_allowToSave = false;

			LoadDemos();
			LoadDayparts();
			LoadCalendarType();

			_allowToSave = true;
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			if (!_settingsChanged) return;
			if (checkEditDemosDisabled.Checked)
			{
				_settings.UseDemo = false;
				_settings.Demo = null;
			}
			else
			{
				_settings.UseDemo = true;
				if (checkEditDemosRtg.Checked)
					_settings.DemoType = DemoType.Rtg;
				else if (checkEditDemosImps.Checked)
					_settings.DemoType = DemoType.Imp;
				_settings.Demo = comboBoxEditDemos.EditValue as String;
			}
			_settings.MondayBased = checkEditCalendarTypeMonday.Checked;
			if (_calendarTypeChanged)
				CalendarTypeChanged?.Invoke(this, EventArgs.Empty);
		}

		#region Demos Processing
		private void LoadDemos()
		{
			comboBoxEditDemos.Properties.Items.Clear();
			comboBoxEditDemos.Properties.Items.AddRange(MediaMetaData.Instance.ListManager.CustomDemos);
			comboBoxEditDemos.EditValue = MediaMetaData.Instance.ListManager.CustomDemos.FirstOrDefault();

			if (_settings.UseDemo)
			{
				if (_settings.DemoType == DemoType.Rtg)
				{
					checkEditDemosDisabled.Checked = false;
					checkEditDemosRtg.Checked = true;
					checkEditDemosImps.Checked = false;
				}
				else if (_settings.DemoType == DemoType.Imp)
				{
					checkEditDemosDisabled.Checked = false;
					checkEditDemosRtg.Checked = false;
					checkEditDemosImps.Checked = true;
				}
				comboBoxEditDemos.EditValue = _settings.Demo ?? MediaMetaData.Instance.ListManager.CustomDemos.FirstOrDefault();
			}
			else
			{
				checkEditDemosDisabled.Checked = true;
				checkEditDemosRtg.Checked = false;
				checkEditDemosImps.Checked = false;
			}
		}

		private void OnDemoTypeCheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemDemosItems.Visibility = !checkEditDemosDisabled.Checked
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			if (!_allowToSave) return;
			_settingsChanged = true;
		}

		private void OnDemoValueEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_settingsChanged = true;
		}
		#endregion

		#region Dayparts processing
		private void LoadDayparts()
		{
			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			var layoutItems = new List<BaseLayoutItem>();
			foreach (var daypart in _settings.Dayparts)
			{
				var control = new CheckEdit();
				control.Properties.AllowFocused = false;
				control.Properties.AutoWidth = true;
				control.Properties.Caption = daypart.Name;
				control.StyleController = layoutControl;
				control.Tag = daypart;
				control.Checked = daypart.Available;
				control.CheckedChanged += OnDaypartStateChanged;
				layoutControl.Controls.Add(control);

				var layoutItem = new LayoutControlItem();
				layoutItem.Control = control;
				layoutItem.FillControlToClientArea = false;
				layoutItem.TextVisible = false;
				layoutItem.TrimClientAreaToControl = false;
				layoutItem.ControlAlignment = ContentAlignment.MiddleLeft;
				layoutItem.SizeConstraintsType = SizeConstraintsType.Custom;
				layoutItem.MinSize = new Size(control.Width + (Int32)(30 * scaleFactor.Width), (Int32)(30 * scaleFactor.Width));
				layoutItems.Add(layoutItem);
			}
			layoutControlGroupDaypartValues.Items.AddRange(layoutItems.ToArray());
		}

		private void OnDaypartStateChanged(Object sender, EventArgs e)
		{
			var control = (CheckEdit)sender;
			var daypart = (Daypart)control.Tag;
			daypart.Available = control.Checked;
			_settingsChanged = true;
		}
		#endregion

		#region Calendar processing
		private void LoadCalendarType()
		{
			checkEditCalendarTypeMonday.Checked = _settings.MondayBased;
			checkEditCalendarTypeSunday.Checked = !_settings.MondayBased;
		}

		private void OnCalendarTypeEditValueChanging(object sender, ChangingEventArgs e)
		{
			if (!_allowToSave) return;
			if ((bool)e.NewValue != true) return;
			e.Cancel = PopupMessageHelper.Instance.ShowWarningQuestion(
						   String.Format("Your current schedule will be reset.{0}Do you want to continue and change calendar type?",
							   Environment.NewLine)) != DialogResult.Yes;
		}

		private void OnCalendarTypeCheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_settingsChanged = true;
			_calendarTypeChanged = true;
		}
		#endregion
	}
}
