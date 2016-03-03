using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.Persistent;
using Asa.Business.Media.Enums;
using Asa.Business.Online.Dictionaries;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.ContentEditors.Controls;
using Asa.Common.GUI.ContentEditors.Events;
using Asa.Media.Controls.BusinessClasses;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.SettingsControls
{
	[ToolboxItem(false)]
	//public partial class HomeControl : UserControl
	public partial class HomeControl : BaseScheduleSettingsEditControl<MediaScheduleSettings, MediaScheduleChangeInfo>
	{
		private bool _allowToSave;
		public DigitalProductsContent DigitalContent { get; set; }
		private MediaSchedule Schedule
		{
			get { return BusinessObjects.Instance.ScheduleManager.ActiveSchedule; }
		}
		private MediaScheduleSettings OriginalSettings
		{
			get { return Schedule.Settings; }
			set { Schedule.Settings = value; }
		}

		public override string Identifier
		{
			get { return ContentIdentifiers.ScheduleSettings; }
		}

		public override RibbonTabItem TabPage
		{
			get { return Controller.Instance.TabHome; }
		}

		public HomeControl()
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96) { }
		}

		#region BaseContentEditControl Override
		public override void InitControl()
		{
			base.InitControl();

			LoadDigitalCategories();
			((RibbonBar)Controller.Instance.HomeProductAdd.ContainerControl).Visible = Controller.Instance.TabDigitalProduct.Visible || Controller.Instance.TabDigitalPackage.Visible;

			stationsControl.Changed += (o, e) => { SettingsNotSaved = true; };
			daypartsControl.Changed += (o, e) => { SettingsNotSaved = true; };
			xtraTabPageDigital.PageVisible = Controller.Instance.TabDigitalProduct.Visible || Controller.Instance.TabDigitalPackage.Visible;


			Controller.Instance.ContentController.RibbonTabsStateChanged += OnRibbonRibbonTabsStateChanged;

			Controller.Instance.HomeBusinessName.EditValueChanged += OnSchedulePropertyValueChanged;
			Controller.Instance.HomeDecisionMaker.EditValueChanged += OnSchedulePropertyValueChanged;
			Controller.Instance.HomeClientType.EditValueChanged += OnSchedulePropertyValueChanged;
			Controller.Instance.HomeAccountNumberText.EditValueChanged += OnSchedulePropertyValueChanged;
			Controller.Instance.HomeAccountNumberCheck.CheckedChanged += checkBoxItemAccountNumber_CheckedChanged;
			Controller.Instance.HomeFlightDatesStart.EditValueChanged += OnSchedulePropertyValueChanged;
			Controller.Instance.HomeFlightDatesStart.EditValueChanged += OnFlightDateStartValueChanged;
			Controller.Instance.HomeFlightDatesStart.EditValueChanged += OnFlightDatesValuesChange;
			Controller.Instance.HomeFlightDatesEnd.EditValueChanged += OnSchedulePropertyValueChanged;
			Controller.Instance.HomeFlightDatesEnd.EditValueChanged += OnFlightDatesValuesChange;
			Controller.Instance.HomeFlightDatesEnd.EditValueChanged += OnFlightDateEndValueChanged;
			Controller.Instance.HomeFlightDatesStart.CloseUp += OnFlightDatesStartCloseUp;
			Controller.Instance.HomeFlightDatesEnd.CloseUp += OnFlightDatesEndCloseUp;
			Controller.Instance.HomeProductClone.Click += DigitalProductClone;
			Controller.Instance.HomeBusinessName.EnableSelectAll();
			Controller.Instance.HomeDecisionMaker.EnableSelectAll();

			Controller.Instance.HomeBusinessName.TabIndex = 0;
			Controller.Instance.HomeBusinessName.KeyDown += OnSchedulePropertiesEditorKeyDown;
			Controller.Instance.HomeDecisionMaker.KeyDown += OnSchedulePropertiesEditorKeyDown;
			Controller.Instance.HomeClientType.KeyDown += OnSchedulePropertiesEditorKeyDown;
			Controller.Instance.HomePresentationDate.KeyDown += OnSchedulePropertiesEditorKeyDown;
			Controller.Instance.HomeFlightDatesStart.KeyDown += OnSchedulePropertiesEditorKeyDown;
			Controller.Instance.HomeFlightDatesEnd.KeyDown += OnSchedulePropertiesEditorKeyDown;
		}

		protected override void UpdateEditedContet()
		{
			_allowToSave = false;
			if (EditedSettings == null || ContentUpdateInfo.ChangeInfo.WholeScheduleChanged)
			{
				if (EditedSettings != null)
					EditedSettings.Dispose();
				EditedSettings = OriginalSettings.Clone<MediaScheduleSettings, MediaScheduleSettings>();
				SettingsNotSaved = EditedSettings.IsNew;

				#region Media Tab
				Controller.Instance.HomeAccountNumberCheck.Enabled = EditedSettings.HomeViewSettings.EnableAccountNumber;

				Controller.Instance.HomeClientType.Properties.Items.Clear();
				Controller.Instance.HomeClientType.Properties.Items.AddRange(MediaMetaData.Instance.ListManager.ClientTypes.ToArray());

				Controller.Instance.HomeBusinessName.EditValue = EditedSettings.BusinessName;
				Controller.Instance.HomeDecisionMaker.EditValue = EditedSettings.DecisionMaker;
				Controller.Instance.HomeClientType.EditValue = EditedSettings.ClientType;
				Controller.Instance.HomeAccountNumberCheck.Checked = !String.IsNullOrEmpty(EditedSettings.AccountNumber);
				Controller.Instance.HomeAccountNumberText.EditValue = EditedSettings.AccountNumber;

				Controller.Instance.HomePresentationDate.EditValue = EditedSettings.PresentationDate;
				Controller.Instance.HomeFlightDatesStart.EditValue = EditedSettings.UserFlightDateStart;
				Controller.Instance.HomeFlightDatesEnd.EditValue = EditedSettings.UserFlightDateEnd;

				switch (EditedSettings.SelectedSpotType)
				{
					case SpotType.Week:
						buttonXWeeklySchedule.Checked = true;
						buttonXMonthlySchedule.Checked = false;
						break;
					case SpotType.Month:
						buttonXWeeklySchedule.Checked = false;
						buttonXMonthlySchedule.Checked = true;
						break;
				}

				var importedDemos = MediaMetaData.Instance.ListManager.SourcePrograms.SelectMany(sp => sp.Demos);
				if (!importedDemos.Any())
				{
					buttonXUseDemos.Text = "Show Demo Estimates";
					pnDemosCustom.Visible = false;
					pnDemosImport.Visible = false;
					pnSelectSource.Visible = false;
				}

				comboBoxEditSource.Properties.Items.Clear();
				comboBoxEditSource.Properties.Items.AddRange(MediaMetaData.Instance.ListManager.SourcePrograms
					.SelectMany(sp => sp.Demos)
					.Select(d => d.Source)
					.Distinct()
					.ToArray());

				if (EditedSettings.UseDemo)
				{
					buttonXUseDemos.Checked = true;

					buttonXDemosCustom.Enabled = true;
					buttonXDemosCustom.Checked = !EditedSettings.ImportDemo;

					buttonXDemosImport.Enabled = true;
					buttonXDemosImport.Checked = EditedSettings.ImportDemo;

					if (EditedSettings.ImportDemo)
					{
						comboBoxEditSource.Enabled = true;
						comboBoxEditSource.EditValue = EditedSettings.Source;

						buttonXDemosRtg.Enabled = false;
						buttonXDemosRtg.Checked = false;

						buttonXDemosImps.Enabled = false;
						buttonXDemosImps.Checked = true;

						comboBoxEditDemos.Properties.Items.Clear();
						var demos = MediaMetaData.Instance.ListManager.SourcePrograms
							.SelectMany(sp => sp.Demos)
							.Where(d => d.Source == EditedSettings.Source);
						if (demos.Any())
						{
							comboBoxEditDemos.Enabled = true;
							comboBoxEditDemos.Properties.Items.AddRange(demos.GroupBy(d => d.DisplayString).Select(g => g.First()).ToArray());
							comboBoxEditDemos.EditValue = demos
								.FirstOrDefault(d =>
									d.DemoType == EditedSettings.DemoType &&
									d.Source == EditedSettings.Source &&
									d.Name == EditedSettings.Demo);
						}
						else
						{
							comboBoxEditDemos.Enabled = false;
							comboBoxEditDemos.EditValue = null;
						}
					}
					else
					{
						comboBoxEditDemos.Enabled = true;

						comboBoxEditSource.Enabled = false;
						comboBoxEditSource.EditValue = null;

						buttonXDemosRtg.Enabled = true;
						buttonXDemosRtg.Checked = EditedSettings.DemoType == DemoType.Rtg;

						buttonXDemosImps.Enabled = true;
						buttonXDemosImps.Checked = EditedSettings.DemoType == DemoType.Imp;

						comboBoxEditDemos.Properties.Items.Clear();
						comboBoxEditDemos.Properties.Items.AddRange(MediaMetaData.Instance.ListManager.CustomDemos);
						comboBoxEditDemos.EditValue = EditedSettings.Demo;
					}
				}
				else
				{
					buttonXUseDemos.Checked = false;

					buttonXDemosCustom.Enabled = false;
					buttonXDemosCustom.Checked = true;

					buttonXDemosImport.Enabled = false;
					buttonXDemosImport.Checked = false;

					comboBoxEditDemos.Enabled = false;
					comboBoxEditDemos.Properties.Items.Clear();
					comboBoxEditDemos.Properties.Items.AddRange(MediaMetaData.Instance.ListManager.CustomDemos);
					comboBoxEditDemos.EditValue = null;

					comboBoxEditSource.Enabled = false;
					comboBoxEditSource.EditValue = null;

					buttonXDemosRtg.Enabled = false;
					buttonXDemosRtg.Checked = false;
					buttonXDemosImps.Enabled = false;
					buttonXDemosImps.Checked = true;
				}

				buttonXCalendarTypeMondayBased.Checked = EditedSettings.MondayBased;
				buttonXCalendarTypeSundayBased.Checked = !EditedSettings.MondayBased;
				Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = EditedSettings.StartDayOfWeek;

				stationsControl.LoadData(EditedSettings);
				daypartsControl.LoadData(EditedSettings);
				#endregion
			}

			if (DigitalContent == null ||
				ContentUpdateInfo.ChangeInfo.WholeScheduleChanged ||
				ContentUpdateInfo.ChangeInfo.DigitalContentChanged)
			{
				DigitalContent = Schedule.DigitalProductsContent.Clone<DigitalProductsContent, DigitalProductsContent>();
				digitalProductListControl.UpdateData(
					DigitalContent,
					EditedSettings,
					() =>
					{
						UpdateProductsCount();
						Controller.Instance.ContentController.UpdateTabsSate();
						if (_allowToSave)
						{
							ChangeInfo.DigitalContentChanged = true;
							SettingsNotSaved = true;
						}
					}
				);
			}

			OnProductsTabPageChanged(this, new TabPageChangedEventArgs(null, xtraTabControlMain.SelectedTabPage));

			UpdateScheduleControls();
			UpdateFlexFlightDatesWarning();

			_allowToSave = true;
		}

		protected override void ApplyChanges()
		{
			digitalProductListControl.ApplyChanges();

			EditedSettings.BusinessName = Controller.Instance.HomeBusinessName.EditValue as String;
			EditedSettings.DecisionMaker = Controller.Instance.HomeDecisionMaker.EditValue as String;
			EditedSettings.ClientType = Controller.Instance.HomeClientType.EditValue as String;
			EditedSettings.AccountNumber = Controller.Instance.HomeAccountNumberCheck.Checked ?
				Controller.Instance.HomeAccountNumberText.EditValue as String :
				null;
			EditedSettings.PresentationDate = (DateTime?)Controller.Instance.HomePresentationDate.EditValue;
			EditedSettings.UserFlightDateStart = (DateTime?)Controller.Instance.HomeFlightDatesStart.EditValue;
			EditedSettings.UserFlightDateEnd = (DateTime?)Controller.Instance.HomeFlightDatesEnd.EditValue;
			EditedSettings.UseDemo = buttonXUseDemos.Checked;
			EditedSettings.ImportDemo = buttonXDemosImport.Checked;
			EditedSettings.Source = comboBoxEditSource.EditValue as String;
			if (buttonXDemosImport.Checked)
			{
				var demo = comboBoxEditDemos.EditValue as Demo;
				EditedSettings.Demo = demo != null ? demo.Name : null;
				EditedSettings.DemoType = demo != null ? demo.DemoType : DemoType.Rtg;
			}
			else
			{
				if (buttonXDemosRtg.Checked)
					EditedSettings.DemoType = DemoType.Rtg;
				else if (buttonXDemosImps.Checked)
					EditedSettings.DemoType = DemoType.Imp;
				EditedSettings.Demo = comboBoxEditDemos.EditValue as String;
			}

			if (stationsControl.HasChanged)
			{
				EditedSettings.Stations.Clear();
				EditedSettings.Stations.AddRange(stationsControl.GetData());
				stationsControl.HasChanged = false;
			}

			if (daypartsControl.HasChanged)
			{
				EditedSettings.Dayparts.Clear();
				EditedSettings.Dayparts.AddRange(daypartsControl.GetData());
				daypartsControl.HasChanged = false;
			}

			UpdateScheduleControls();

			OriginalSettings.CompareChanges(EditedSettings, ChangeInfo);
		}

		protected override void ValidateChanges(ContentSavingEventArgs savingArgs)
		{
			if (String.IsNullOrEmpty(EditedSettings.BusinessName))
			{
				savingArgs.Cancel = true;
				savingArgs.ErrorMessages.Add("Your schedule is missing important information!\nPlease make sure you have a Business Name before you proceed.");
				return;
			}
			if (String.IsNullOrEmpty(EditedSettings.DecisionMaker))
			{
				savingArgs.Cancel = true;
				savingArgs.ErrorMessages.Add("Your schedule is missing important information!\nPlease make sure you have a Owner/Decision-maker before you proceed.");
				return;
			}
			if (String.IsNullOrEmpty(EditedSettings.ClientType))
			{
				savingArgs.Cancel = true;
				savingArgs.ErrorMessages.Add("You must set Client type before save");
				return;
			}
			if (!EditedSettings.PresentationDate.HasValue)
			{
				savingArgs.Cancel = true;
				savingArgs.ErrorMessages.Add("Your schedule is missing important information!\nPlease make sure you have a Presentation Date before you proceed.");
				return;
			}
			if (!EditedSettings.UserFlightDateStart.HasValue || !EditedSettings.UserFlightDateEnd.HasValue)
			{
				savingArgs.Cancel = true;
				savingArgs.ErrorMessages.Add("Your schedule is missing important information!\nPlease make sure you have a Flight Dates before you proceed.");
				return;
			}
			if (DigitalContent.DigitalProducts.Any(product => String.IsNullOrEmpty(product.Name)))
			{
				savingArgs.Cancel = true;
				savingArgs.ErrorMessages.Add("Your schedule is missing important information!\nPlease make sure you have a Web Product in each line before you proceed.");
				return;
			}
			if (ChangeInfo.ScheduleDatesChanged)
			{
				if (PopupMessageHelper.Instance.ShowWarningQuestion("Flight Dates have been changed and all Spots will be recreated\nDo you want to proceed?") != DialogResult.Yes)
					savingArgs.Cancel = true;
			}
		}

		protected override void SaveData()
		{
			OriginalSettings = EditedSettings.Clone<MediaScheduleSettings, MediaScheduleSettings>();
			OriginalSettings.UpdateDictionaries();

			Schedule.ApplySettingsChanges(ChangeInfo);

			if (ChangeInfo.DigitalContentChanged)
				Schedule.DigitalProductsContent = DigitalContent.Clone<DigitalProductsContent, DigitalProductsContent>();
		}

		public override void GetHelp()
		{
			if (xtraTabControlMain.SelectedTabPage == xtraTabPageMedia)
				BusinessObjects.Instance.HelpManager.OpenHelpLink(
					String.Format("home{0}", MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? "tv" : "rd"));
			else if (xtraTabControlMain.SelectedTabPage == xtraTabPageDigital)
				BusinessObjects.Instance.HelpManager.OpenHelpLink(String.Format("home{0}", "dg"));
		}
		#endregion

		#region Common Methods
		private void UpdateScheduleControls()
		{
			var enableSchedules = !String.IsNullOrEmpty(EditedSettings.BusinessName) &
								   !String.IsNullOrEmpty(EditedSettings.DecisionMaker) &
								   !String.IsNullOrEmpty(EditedSettings.ClientType) &
								   EditedSettings.PresentationDate.HasValue &
								   EditedSettings.UserFlightDateStart.HasValue &
								   EditedSettings.UserFlightDateEnd.HasValue;
			Controller.Instance.ContentController.UpdateTabsSate();
			buttonXWeeklySchedule.Enabled = enableSchedules;
			buttonXMonthlySchedule.Enabled = enableSchedules;
			if (enableSchedules)
				pnMedia.BringToFront();
			else
				pnMediaDefault.BringToFront();
		}

		private void LoadDigitalCategories()
		{
			foreach (var category in ListManager.Instance.Categories)
			{
				var categoryButton = new ButtonItem
				{
					Image = category.Logo,
					Text = "<b>" + category.TooltipTitle + "</b><p>" + category.TooltipValue + "</p>",
					ImagePaddingHorizontal = 8,
					SubItemsExpandWidth = 14,
					Tag = category
				};
				categoryButton.Click += DigitalProductAdd;
				Controller.Instance.HomeProductAdd.SubItems.Add(categoryButton);
			}
			((RibbonBar)Controller.Instance.HomeProductAdd.ContainerControl).RecalcLayout();
			Controller.Instance.HomePanel.PerformLayout();
		}

		private void UpdateProductsCount()
		{
			xtraTabPageDigital.Text = String.Format("Digital Strategy  ({0})", DigitalContent.DigitalProducts.Count);
		}

		private void UpdateFlexFlightDatesWarning()
		{
			if (MediaMetaData.Instance.ListManager.FlexFlightDatesAllowed)
			{
				var warningText = new List<string>();
				if (Controller.Instance.HomeFlightDatesStart.EditValue != null)
				{
					var startDate = Controller.Instance.HomeFlightDatesStart.DateTime;
					if (startDate.DayOfWeek != EditedSettings.StartDayOfWeek)
					{
						var weekEnd = startDate;
						while (weekEnd.DayOfWeek != EditedSettings.EndDayOfWeek)
							weekEnd = weekEnd.AddDays(1);
						warningText.Add(String.Format("*The FIRST WEEK of your schedule STARTS on a {0}.{1}({2} - {3})", startDate.DayOfWeek, Environment.NewLine, startDate.ToString("M/d/yy"), weekEnd.ToString("M/d/yy")));
					}
				}
				if (Controller.Instance.HomeFlightDatesEnd.EditValue != null)
				{
					var endDate = Controller.Instance.HomeFlightDatesEnd.DateTime;
					if (endDate.DayOfWeek != EditedSettings.EndDayOfWeek)
					{
						var weekStart = endDate;
						while (weekStart.DayOfWeek != EditedSettings.StartDayOfWeek)
							weekStart = weekStart.AddDays(-1);
						warningText.Add(String.Format("*The LAST WEEK of your schedule ENDS on a {0}.{1}({2} - {3})", endDate.DayOfWeek, Environment.NewLine, weekStart.ToString("M/d/yy"), endDate.ToString("M/d/yy")));
					}
				}
			}
		}

		private void OnProductsTabPageChanged(object sender, TabPageChangedEventArgs e)
		{
			((RibbonBar)Controller.Instance.HomeProductAdd.ContainerControl).Enabled = e.Page == xtraTabPageDigital;
			UpdateProductsCount();
			splitContainerControl.PanelVisibility = e.Page == xtraTabPageDigital ? SplitPanelVisibility.Panel1 : SplitPanelVisibility.Both;
		}

		private void OnRibbonRibbonTabsStateChanged(object sender, EventArgs e)
		{
			buttonXSnapshot.Enabled = Controller.Instance.TabSnapshot.Visible && Controller.Instance.TabSnapshot.Enabled;
			buttonXOptions.Enabled = Controller.Instance.TabOptions.Visible && Controller.Instance.TabOptions.Enabled;
			buttonXCalendar.Enabled = Controller.Instance.TabCalendar2.Visible && Controller.Instance.TabCalendar2.Enabled;
		}

		private void OnDefaultPanelResize(object sender, EventArgs e)
		{
			if (pnMediaDefault.Width > pbMediaDefault.Image.Width)
			{
				pbMediaDefault.SizeMode = PictureBoxSizeMode.Normal;
				pbMediaDefault.Dock = DockStyle.Fill;
			}
			else
			{
				pbMediaDefault.SizeMode = PictureBoxSizeMode.Zoom;
				pbMediaDefault.Dock = DockStyle.None;
				pbMediaDefault.Top = 0;
				pbMediaDefault.Left = 0;
				pbMediaDefault.Width = pnMediaDefault.Width;
				pbMediaDefault.Height = (Int32)(pbMediaDefault.Image.Height * ((decimal)pbMediaDefault.Width / pbMediaDefault.Image.Width));
			}
		}
		#endregion

		#region Editors Events

		public void OnSchedulePropertyValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			ApplyChanges();
			SettingsNotSaved = true;
		}

		public void checkBoxItemAccountNumber_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
		{
			Controller.Instance.HomeAccountNumberText.Enabled = Controller.Instance.HomeAccountNumberCheck.Checked;
			OnSchedulePropertyValueChanged(null, null);
		}

		public void OnFlightDateStartValueChanged(object sender, EventArgs e)
		{
			if (Controller.Instance.HomeFlightDatesStart.EditValue != null && _allowToSave)
			{
				var dateStart = Controller.Instance.HomeFlightDatesStart.DateTime;
				SettingsNotSaved = true;
				if (Controller.Instance.HomeFlightDatesEnd.EditValue == null)
				{
					while (dateStart.DayOfWeek != EditedSettings.EndDayOfWeek)
						dateStart = dateStart.AddDays(1);
					_allowToSave = false;
					Controller.Instance.HomeFlightDatesEnd.EditValue = dateStart;
					_allowToSave = true;
				}
				UpdateFlexFlightDatesWarning();
			}
			OnSchedulePropertyValueChanged(null, null);
		}

		public void OnFlightDateEndValueChanged(object sender, EventArgs e)
		{
			if (Controller.Instance.HomeFlightDatesStart.EditValue != null && _allowToSave)
			{
				UpdateFlexFlightDatesWarning();
				SettingsNotSaved = true;
			}
			OnSchedulePropertyValueChanged(null, null);
		}

		public void OnFlightDatesValuesChange(object sender, EventArgs e)
		{
			Controller.Instance.HomeWeeks.Text = "";
			Controller.Instance.HomeWeeks.Visible = false;
			if (Controller.Instance.HomeFlightDatesStart.EditValue == null || Controller.Instance.HomeFlightDatesEnd.EditValue == null) return;
			var startDate = Controller.Instance.HomeFlightDatesStart.DateTime;
			while (startDate.DayOfWeek != EditedSettings.StartDayOfWeek)
				startDate = startDate.AddDays(-1);
			var endDate = Controller.Instance.HomeFlightDatesEnd.DateTime;
			while (endDate.DayOfWeek != EditedSettings.EndDayOfWeek)
				endDate = endDate.AddDays(1);
			var datesRange = endDate - startDate;
			var weeksCount = datesRange.Days / 7 + 1;
			Controller.Instance.HomeWeeks.Text = weeksCount + (weeksCount > 1 ? " Weeks" : " Week");
			Controller.Instance.HomeWeeks.Visible = true;
		}

		public void OnFlightDatesStartCloseUp(object sender, CloseUpEventArgs e)
		{
			var dateEdit = sender as DateEdit;
			if (dateEdit == null) return;
			if (dateEdit.EditValue == e.Value) return;
			if (e.Value == null) return;
			DateTime temp;
			if (!DateTime.TryParse(e.Value.ToString(), out temp)) return;
			var moveDateToWeekStart = true;
			if (MediaMetaData.Instance.ListManager.FlexFlightDatesAllowed)
			{
				if (temp.DayOfWeek != EditedSettings.StartDayOfWeek)
					if (PopupMessageHelper.Instance.ShowWarningQuestion(String.Format("Are you sure you want to start your schedule on a {0}?{1}{1}The broadcast week normally starts on a {2}.", temp.DayOfWeek, Environment.NewLine, EditedSettings.StartDayOfWeek)) == DialogResult.Yes)
						moveDateToWeekStart = false;
			}
			if (moveDateToWeekStart)
			{
				while (temp.DayOfWeek != EditedSettings.StartDayOfWeek)
					temp = temp.AddDays(-1);
				e.Value = temp;
			}
		}

		public void OnFlightDatesEndCloseUp(object sender, CloseUpEventArgs e)
		{
			var dateEdit = sender as DateEdit;
			if (dateEdit == null) return;
			if (dateEdit.EditValue == e.Value) return;
			if (e.Value == null) return;
			DateTime temp;
			if (!DateTime.TryParse(e.Value.ToString(), out temp)) return;
			var moveDateToWeekEnd = true;
			if (MediaMetaData.Instance.ListManager.FlexFlightDatesAllowed)
			{
				if (temp.DayOfWeek != EditedSettings.EndDayOfWeek)
					if (PopupMessageHelper.Instance.ShowWarningQuestion(String.Format("Are you sure you want to end your schedule on a {0}?{1}{1}The broadcast week normally ends on a {2}.", temp.DayOfWeek, Environment.NewLine, EditedSettings.EndDayOfWeek)) == DialogResult.Yes)
						moveDateToWeekEnd = false;
			}
			if (moveDateToWeekEnd)
			{
				while (temp.DayOfWeek != EditedSettings.EndDayOfWeek)
					temp = temp.AddDays(1);
				e.Value = temp;
			}
		}

		public void OnSchedulePropertiesEditorKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Tab) return;
			if (sender == Controller.Instance.HomeBusinessName)
				Controller.Instance.HomeDecisionMaker.Focus();
			else if (sender == Controller.Instance.HomeDecisionMaker)
				Controller.Instance.HomeClientType.Focus();
			else if (sender == Controller.Instance.HomeClientType)
				Controller.Instance.HomePresentationDate.Focus();
			else if (sender == Controller.Instance.HomePresentationDate)
				Controller.Instance.HomeFlightDatesStart.Focus();
			else if (sender == Controller.Instance.HomeFlightDatesStart)
				Controller.Instance.HomeFlightDatesEnd.Focus();
			else if (sender == Controller.Instance.HomeFlightDatesEnd)
				Controller.Instance.HomeBusinessName.Focus();
			e.Handled = true;
		}
		#endregion

		#region Demos Processing
		private void buttonXDemosNo_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			if (buttonXUseDemos.Checked)
			{
				buttonXDemosCustom.Enabled = true;
				buttonXDemosImport.Enabled = true;
				buttonXDemos_CheckedChanged(sender, e);
			}
			else
			{
				buttonXDemosCustom.Enabled = false;
				buttonXDemosCustom.Checked = true;

				buttonXDemosImport.Enabled = false;
				buttonXDemosImport.Checked = false;

				comboBoxEditDemos.Enabled = false;
				comboBoxEditDemos.Properties.Items.Clear();
				comboBoxEditDemos.Properties.Items.AddRange(MediaMetaData.Instance.ListManager.CustomDemos);
				comboBoxEditDemos.EditValue = null;

				comboBoxEditSource.Enabled = false;
				comboBoxEditSource.EditValue = null;

				buttonXDemosRtg.Enabled = false;
				buttonXDemosRtg.Checked = false;
				buttonXDemosImps.Enabled = false;
				buttonXDemosImps.Checked = true;
			}
			SettingsNotSaved = true;
		}

		private void buttonXDemos_Click(object sender, EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null || button.Checked) return;
			buttonXDemosCustom.Checked = false;
			buttonXDemosImport.Checked = false;
			button.Checked = true;
		}

		private void buttonXDemos_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			if (buttonXDemosImport.Checked)
			{
				comboBoxEditSource.Enabled = true;
				buttonXDemosRtg.Enabled = false;
				buttonXDemosRtg.Checked = false;
				buttonXDemosImps.Enabled = false;
				buttonXDemosImps.Checked = true;
				comboBoxEditSource_EditValueChanged(sender, e);
			}
			else
			{
				comboBoxEditDemos.Enabled = true;
				comboBoxEditSource.Enabled = false;
				comboBoxEditSource.EditValue = null;
				buttonXDemosRtg.Enabled = true;
				buttonXDemosImps.Enabled = true;
				comboBoxEditDemos.Properties.Items.Clear();
				comboBoxEditDemos.Properties.Items.AddRange(MediaMetaData.Instance.ListManager.CustomDemos);
				comboBoxEditDemos.EditValue = null;
			}
			SettingsNotSaved = true;
		}

		private void comboBoxEditSource_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			comboBoxEditDemos.Properties.Items.Clear();
			var demos = MediaMetaData.Instance.ListManager.SourcePrograms.SelectMany(sp => sp.Demos).Where(d => d.Source == comboBoxEditSource.EditValue as String);
			comboBoxEditDemos.Enabled = demos.Any();
			comboBoxEditDemos.Properties.Items.AddRange(demos.GroupBy(d => d.DisplayString).Select(g => g.First()).ToArray());
			comboBoxEditDemos.EditValue = null;
		}

		private void buttonXDemosType_Click(object sender, EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null || button.Checked) return;
			buttonXDemosRtg.Checked = false;
			buttonXDemosImps.Checked = false;
			button.Checked = true;
			SettingsNotSaved = true;
		}

		private void buttonXDemosType_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			SettingsNotSaved = true;
		}

		private void comboBoxEditDemos_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			SettingsNotSaved = true;
		}
		#endregion

		#region Calendar processing
		private void buttonXCalendarType_Click(object sender, EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null) return;
			if (button.Checked) return;
			if (PopupMessageHelper.Instance.ShowWarningQuestion(String.Format("Your current schedule will be reset.{0}Do you want to continue and change calendar type?", Environment.NewLine)) != DialogResult.Yes) return;
			buttonXCalendarTypeMondayBased.Checked = false;
			buttonXCalendarTypeSundayBased.Checked = false;
			button.Checked = true;
		}

		private void buttonXCalendarType_CheckedChanged(object sender, EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null) return;
			if (!button.Checked) return;
			if (!_allowToSave) return;
			Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = EditedSettings.StartDayOfWeek;
			Controller.Instance.HomeFlightDatesStart.EditValue = null;
			Controller.Instance.HomeFlightDatesEnd.EditValue = null;
			Controller.Instance.HomeWeeks.Text = String.Empty;
			SettingsNotSaved = true;
		}
		#endregion

		#region Buttons Clicks Events
		private void buttonXScheduleType_Click(object sender, EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null) return;
			if (button.Checked) return;
			buttonXWeeklySchedule.Checked = false;
			buttonXMonthlySchedule.Checked = false;
			button.Checked = true;
		}

		private void buttonXScheduleType_CheckedChanged(object sender, EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null) return;
			if (!button.Checked) return;
			if (!_allowToSave) return;
			if (buttonXWeeklySchedule.Checked)
				EditedSettings.SelectedSpotType = SpotType.Week;
			else if (buttonXMonthlySchedule.Checked)
				EditedSettings.SelectedSpotType = SpotType.Month;
			SettingsNotSaved = true;
		}

		private void buttonXSnapshot_Click(object sender, EventArgs e)
		{
			Controller.Instance.TabSnapshot.Select();
		}

		private void buttonXOptions_Click(object sender, EventArgs e)
		{
			Controller.Instance.TabOptions.Select();
		}

		private void buttonXCalendar_Click(object sender, EventArgs e)
		{
			Controller.Instance.TabCalendar2.Select();
		}
		#endregion

		#region Digital Product Events
		public void DigitalProductAdd(object sender, EventArgs e)
		{
			var category = (Category)((ButtonItem)sender).Tag;
			digitalProductListControl.AddProduct(category);
		}

		public void DigitalProductClone(object sender, EventArgs e)
		{
			digitalProductListControl.CloneProduct();
		}
		#endregion
	}
}