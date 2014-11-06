using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using NewBizWiz.CommonGUI.Common;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.BusinessClasses;
using ListManager = NewBizWiz.Core.OnlineSchedule.ListManager;
using Schedule = NewBizWiz.Core.OnlineSchedule.Schedule;

namespace NewBizWiz.OnlineSchedule.Controls.PresentationClasses
{
	[ToolboxItem(false)]
	public partial class ScheduleSettingsControl : UserControl
	{
		private bool _allowToSave;
		private Schedule _localSchedule;

		public ScheduleSettingsControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			SettingsNotSaved = false;
			LoadCategories();
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			});
		}

		public bool SettingsNotSaved { get; set; }
		public bool AllowToLeaveControl
		{
			get
			{
				bool result = false;
				if (SettingsNotSaved)
				{
					if (SaveSchedule())
						result = true;
				}
				else
					result = true;
				return result;
			}
		}

		#region Methods
		public void LoadCategories()
		{
			foreach (Category category in ListManager.Instance.Categories)
			{
				var categoryButton = new ButtonItem();
				categoryButton.Image = category.Logo;
				categoryButton.Text = "<b>" + category.TooltipTitle + "</b><p>" + category.TooltipValue + "</p>";
				categoryButton.ImagePaddingHorizontal = 8;
				categoryButton.SubItemsExpandWidth = 14;
				categoryButton.Tag = category;
				categoryButton.Click += DigitalProductAdd;
				Controller.Instance.HomeProductAdd.SubItems.Add(categoryButton);
			}
		}

		private void AssignCloseActiveEditorsonOutSideClick(Control control)
		{
			if (control.GetType() != typeof(CheckEdit)
				&& control.GetType() != typeof(SpinEdit)
				&& control.GetType() != typeof(DateEdit)
				&& control.GetType() != typeof(TextEdit)
				&& control.GetType() != typeof(ImageListBoxControl)
				&& control.GetType() != typeof(CheckedListBoxControl)
				&& control.GetType() != typeof(ComboBoxEdit)
				&& control.GetType() != typeof(TabbedDateEdit)
				&& control.GetType() != typeof(TabbedCombobox)
				&& control.GetType() != typeof(ComboBoxListEdit))
			{
				control.Click += CloseActiveEditorsonOutSideClick;
				foreach (Control childControl in control.Controls)
					AssignCloseActiveEditorsonOutSideClick(childControl);
			}
		}

		private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
		{
			Focus();
			digitalProductListControl.CloseEditors();
		}

		private void UpdateProductsCount()
		{
			xtraTabPageDigitalProducts.Text = String.Format("Digital ({0})", _localSchedule.DigitalProducts.Count);
		}

		public void LoadSchedule(bool quickLoad)
		{
			_allowToSave = false;
			_localSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			digitalProductListControl.UpdateData(_localSchedule,
				() =>
				{
					UpdateProductsCount();
					Controller.Instance.UpdateSimpleOutputTabPageState(_localSchedule.DigitalProducts.Any(p => !String.IsNullOrEmpty(p.Name)));
					if (_allowToSave)
						SettingsNotSaved = true;
				},
				activity =>
				{
					var propertyEditActivity = activity as PropertyEditActivity;
					if (propertyEditActivity != null)
						propertyEditActivity.Advertiser = Controller.Instance.HomeBusinessName.EditValue as String;
					BusinessWrapper.Instance.ActivityManager.AddActivity(activity);
				});
			if (!quickLoad)
			{
				LoadView();

				Controller.Instance.HomeClientType.Properties.Items.Clear();
				Controller.Instance.HomeClientType.Properties.Items.AddRange(Core.AdSchedule.ListManager.Instance.ClientTypes.ToArray());

				Controller.Instance.HomeBusinessName.EditValue = _localSchedule.BusinessName;
				Controller.Instance.HomeDecisionMaker.EditValue = _localSchedule.DecisionMaker;

				if (!string.IsNullOrEmpty(_localSchedule.ClientType))
					Controller.Instance.HomeClientType.SelectedIndex = Controller.Instance.HomeClientType.Properties.Items.IndexOf(_localSchedule.ClientType);

				Controller.Instance.HomeAccountNumberCheck.Checked = !string.IsNullOrEmpty(_localSchedule.AccountNumber);
				Controller.Instance.HomeAccountNumberText.EditValue = _localSchedule.AccountNumber;

				Controller.Instance.HomePresentationDate.EditValue = _localSchedule.PresentationDateObject;
				Controller.Instance.HomeFlightDatesStart.EditValue = _localSchedule.FlightDateStartObject;
				Controller.Instance.HomeFlightDatesEnd.EditValue = _localSchedule.FlightDateEndObject;
			}
			SettingsNotSaved = false;
			_allowToSave = true;
		}

		private void LoadView()
		{
			Controller.Instance.HomeAccountNumberCheck.Enabled = _localSchedule.ViewSettings.SharedHomeViewSettings.EnableAccountNumber;
			digitalProductListControl.LoadView();
		}

		private void SaveView()
		{
			digitalProductListControl.SaveView();
		}

		private bool SaveSchedule(string scheduleName = "")
		{
			digitalProductListControl.CloseEditors();
			var businessName = Controller.Instance.HomeBusinessName.EditValue as String;
			if (!String.IsNullOrEmpty(businessName))
			{
				if (_localSchedule.BusinessName != businessName)
				{
					_localSchedule.BusinessName = businessName;
					BusinessWrapper.Instance.ActivityManager.AddActivity(new PropertyEditActivity("Business Name", businessName));
				}
			}
			else
			{
				Utilities.Instance.ShowWarning("You must set Business Name before save");
				return false;
			}
			var decisionMaker = Controller.Instance.HomeDecisionMaker.EditValue as String;
			if (!String.IsNullOrEmpty(decisionMaker))
			{
				if (_localSchedule.DecisionMaker != decisionMaker)
				{
					_localSchedule.DecisionMaker = decisionMaker;
					BusinessWrapper.Instance.ActivityManager.AddActivity(new PropertyEditActivity("Decision Maker", decisionMaker));
				}
			}
			else
			{
				Utilities.Instance.ShowWarning("You must set Owner/Decision-maker before save");
				return false;
			}

			if (Controller.Instance.HomeClientType.EditValue != null)
				_localSchedule.ClientType = Controller.Instance.HomeClientType.EditValue.ToString();
			else
			{
				Utilities.Instance.ShowWarning("You must set Client type before save");
				return false;
			}

			if (Controller.Instance.HomeAccountNumberCheck.Checked && Controller.Instance.HomeAccountNumberText.EditValue != null)
				_localSchedule.AccountNumber = Controller.Instance.HomeAccountNumberText.EditValue.ToString();
			else
				_localSchedule.AccountNumber = string.Empty;

			if (Controller.Instance.HomePresentationDate.EditValue != null)
				_localSchedule.PresentationDate = Controller.Instance.HomePresentationDate.DateTime;
			else
			{
				Utilities.Instance.ShowWarning("You must set Presentation Date before save");
				return false;
			}

			var firstDayOfWeek = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
			var lastDayOfWeek = DayOfWeek.Saturday;
			if (firstDayOfWeek == DayOfWeek.Monday)
				lastDayOfWeek = DayOfWeek.Sunday;
			if (Controller.Instance.HomeFlightDatesStart.EditValue != null)
			{
				_localSchedule.FlightDateStart = Controller.Instance.HomeFlightDatesStart.DateTime;
				if (_localSchedule.FlightDateStart.Value.DayOfWeek != firstDayOfWeek)
				{
					Utilities.Instance.ShowWarning(String.Format("Flight Start Date must be {0}\nFlight End Date must be {1}\nFlight Start Date must be less then Flight End Date.", firstDayOfWeek, lastDayOfWeek));
					return false;
				}
			}
			else
			{
				Utilities.Instance.ShowWarning("You must set Flight Start Dates before save");
				return false;
			}
			if (Controller.Instance.HomeFlightDatesEnd.EditValue != null)
			{
				_localSchedule.FlightDateEnd = Controller.Instance.HomeFlightDatesEnd.DateTime;
				if (_localSchedule.FlightDateEnd.Value.DayOfWeek != lastDayOfWeek || _localSchedule.FlightDateEnd < _localSchedule.FlightDateStart)
				{
					Utilities.Instance.ShowWarning(String.Format("Flight Start Date must be {0}\nFlight End Date must be {1}\nFlight Start Date must be less then Flight End Date.", firstDayOfWeek, lastDayOfWeek));
					return false;
				}
			}
			else
			{
				Utilities.Instance.ShowWarning("You must set Flight End Dates before save");
				return false;
			}

			if (_localSchedule.DigitalProducts.Any(publication => string.IsNullOrEmpty(publication.Name)))
			{
				Utilities.Instance.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Web Product in each line before you proceed.");
				return false;
			}

			SaveView();

			var nameChanged = !string.IsNullOrEmpty(scheduleName);
			if (nameChanged)
				_localSchedule.Name = scheduleName;
			Controller.Instance.SaveSchedule(_localSchedule, nameChanged, false, this);
			SettingsNotSaved = false;
			return true;
		}
		#endregion

		#region Schedule Event Handlers
		private void ScheduleSettingsControl_Load(object sender, EventArgs e)
		{
			AssignCloseActiveEditorsonOutSideClick(Controller.Instance.Ribbon);
		}

		public void ScheduleSettingsSave_Click(object sender, EventArgs e)
		{
			if (SaveSchedule())
				Utilities.Instance.ShowInformation("Schedule Saved");
		}

		public void ScheduleSettingsSaveAs_Click(object sender, EventArgs e)
		{
			using (var form = new FormNewSchedule())
			{
				form.Text = "Save Schedule";
				form.laLogo.Text = "Please set a new name for your Schedule:";
				if (form.ShowDialog() != DialogResult.OK) return;
				if (!string.IsNullOrEmpty(form.ScheduleName))
				{
					if (SaveSchedule(form.ScheduleName))
						Utilities.Instance.ShowInformation("Schedule was saved");
				}
				else
				{
					Utilities.Instance.ShowWarning("Schedule Name can't be empty");
				}
			}
		}

		public void FlightDateStartEditValueChanged(object sender, EventArgs e)
		{
			if (Controller.Instance.HomeFlightDatesStart.EditValue != null)
			{
				DateTime dateStart = Controller.Instance.HomeFlightDatesStart.DateTime;
				while (dateStart.DayOfWeek != DayOfWeek.Saturday)
					dateStart = dateStart.AddDays(1);
				Controller.Instance.HomeFlightDatesEnd.EditValue = dateStart;
			}
		}

		public void CalcWeeksOnFlightDatesChange(object sender, EventArgs e)
		{
			Controller.Instance.HomeWeeks.Text = "";
			Controller.Instance.HomeWeeks.Visible = false;
			if (Controller.Instance.HomeFlightDatesStart.EditValue == null || Controller.Instance.HomeFlightDatesEnd.EditValue == null) return;
			var datesRange = Controller.Instance.HomeFlightDatesEnd.DateTime - Controller.Instance.HomeFlightDatesStart.DateTime;
			var weeksCount = datesRange.Days / 7 + 1;
			Controller.Instance.HomeWeeks.Text = weeksCount + (weeksCount > 1 ? " Weeks" : " Week");
			Controller.Instance.HomeWeeks.Visible = true;
		}

		public void checkBoxItemAccountNumber_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
		{
			Controller.Instance.HomeAccountNumberText.Enabled = Controller.Instance.HomeAccountNumberCheck.Checked;
			SchedulePropertyEditValueChanged(null, null);
		}

		public void SchedulePropertyEditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				SettingsNotSaved = true;
			}
		}

		public void dateEditFlightDatesStart_CloseUp(object sender, CloseUpEventArgs e)
		{
			if (e.Value == null) return;
			var temp = DateTime.MinValue;
			if (!DateTime.TryParse(e.Value.ToString(), out temp)) return;
			while (temp.DayOfWeek != Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
				temp = temp.AddDays(-1);
			e.Value = temp;
		}

		public void dateEditFlightDatesEnd_CloseUp(object sender, CloseUpEventArgs e)
		{
			if (e.Value == null) return;
			var temp = DateTime.MinValue;
			if (!DateTime.TryParse(e.Value.ToString(), out temp)) return;
			var lastDayOfWeek = DayOfWeek.Saturday;
			if (Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek == DayOfWeek.Monday)
				lastDayOfWeek = DayOfWeek.Sunday;
			while (temp.DayOfWeek != lastDayOfWeek)
				temp = temp.AddDays(1);
			e.Value = temp;
		}

		public void SchedulePropertiesEditor_KeyDown(object sender, KeyEventArgs e)
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

		public void ScheduleSettingsHelp_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("homeweb");
		}
		#endregion

		#region Digital Product Events
		public void DigitalProductAdd(object sender, EventArgs e)
		{
			var category = (sender as ButtonItem).Tag as Category;
			digitalProductListControl.AddProduct(category);
		}

		public void DigitalProductClone(object sender, EventArgs e)
		{
			digitalProductListControl.CloneProduct();
		}
		#endregion

	}
}