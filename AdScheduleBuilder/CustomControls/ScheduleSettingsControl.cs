using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AdScheduleBuilder.BusinessClasses;
using AdScheduleBuilder.ToolForms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;

namespace AdScheduleBuilder.CustomControls
{
	[ToolboxItem(false)]
	public partial class ScheduleSettingsControl : UserControl
	{
		private static ScheduleSettingsControl _instance;
		private bool _allowToSave;
		private Schedule _localSchedule;

		private ScheduleSettingsControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			SettingsNotSaved = false;
			ScheduleManager.Instance.SettingsSaved += (sender, e) =>
														  {
															  if (sender != this)
																  LoadSchedule(e.QuickSave);
														  };
		}

		public bool SettingsNotSaved { get; set; }

		public static ScheduleSettingsControl Instance
		{
			get
			{
				if (_instance == null)
					_instance = new ScheduleSettingsControl();
				return _instance;
			}
		}

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

		public static void RemoveInstance()
		{
			try
			{
				_instance.Dispose();
			}
			catch { }
			finally
			{
				_instance = null;
			}
		}

		private void AssignCloseActiveEditorsonOutSideClick(Control control)
		{
			if (control != FormMain.Instance.comboBoxEditBusinessName
				&& control != FormMain.Instance.comboBoxEditClientType
				&& control != FormMain.Instance.comboBoxEditDecisionMaker
				&& control != FormMain.Instance.textEditAccountNumber
				&& control != FormMain.Instance.comboBoxEditRateCard
				&& control != FormMain.Instance.comboBoxEditRateCards
				&& control != FormMain.Instance.dateEditFlightDatesEnd
				&& control != FormMain.Instance.dateEditFlightDatesStart
				&& control != FormMain.Instance.dateEditPresentationDate
				&& control != FormMain.Instance.spinEditStandartHeight
				&& control != FormMain.Instance.spinEditStandartWidth
				&& control != FormMain.Instance.comboBoxEditPercentOfPage
				&& control != FormMain.Instance.comboBoxEditRateCard
				&& control != FormMain.Instance.comboBoxEditSharePagePageSize
				&& control != FormMain.Instance.comboBoxEditStandartPageSize
				&& control != FormMain.Instance.checkedListBoxControlSharePageSquare
				&& control != FormMain.Instance.spinEditCostPerInch)
			{
				control.Click += CloseActiveEditorsonOutSideClick;
				foreach (Control childControl in control.Controls)
					AssignCloseActiveEditorsonOutSideClick(childControl);
			}
		}

		private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
		{
			Focus();
			gridViewPublications.CloseEditor();
		}

		private void UpdatePublicationsCount()
		{
			laPublications.Text = "Publications: " + _localSchedule.Publications.Count.ToString();
		}

		public void LoadSchedule(bool quickLoad)
		{
			_allowToSave = false;
			_localSchedule = ScheduleManager.Instance.GetLocalSchedule();
			gridControlPublications.DataSource = new BindingList<Publication>(_localSchedule.Publications);
			laScheduleName.Text = _localSchedule.Name;
			if (!quickLoad)
			{
				LoadView();

				repositoryItemComboBox.Items.Clear();
				repositoryItemComboBox.Items.AddRange(ListManager.Instance.PublicationSources.Where(x => !x.Name.Equals("Default")).Select(x => x.Name).Distinct().ToArray());
				FormMain.Instance.comboBoxEditBusinessName.Properties.Items.Clear();
				FormMain.Instance.comboBoxEditBusinessName.Properties.Items.AddRange(ListManager.Instance.Advertisers.ToArray());
				FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.Clear();
				FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.AddRange(ListManager.Instance.DecisionMakers.ToArray());
				FormMain.Instance.comboBoxEditClientType.Properties.Items.Clear();
				FormMain.Instance.comboBoxEditClientType.Properties.Items.AddRange(ListManager.Instance.ClientTypes.ToArray());

				FormMain.Instance.comboBoxEditBusinessName.EditValue = _localSchedule.BusinessName;
				FormMain.Instance.comboBoxEditDecisionMaker.EditValue = _localSchedule.DecisionMaker;

				if (!string.IsNullOrEmpty(_localSchedule.ClientType))
					FormMain.Instance.comboBoxEditClientType.SelectedIndex = FormMain.Instance.comboBoxEditClientType.Properties.Items.IndexOf(_localSchedule.ClientType);

				FormMain.Instance.checkBoxItemHomeAccountNumber.Checked = !string.IsNullOrEmpty(_localSchedule.AccountNumber);
				FormMain.Instance.textEditAccountNumber.EditValue = _localSchedule.AccountNumber;

				FormMain.Instance.dateEditPresentationDate.EditValue = _localSchedule.PresentationDateObject;
				FormMain.Instance.dateEditFlightDatesStart.EditValue = _localSchedule.FlightDateStartObject;
				FormMain.Instance.dateEditFlightDatesEnd.EditValue = _localSchedule.FlightDateEndObject;

				UpdatePublicationsCount();

				FormMain.Instance.UpdateScheduleTab(_localSchedule.Publications.Count > 0);
				FormMain.Instance.UpdateOutputTabs(_localSchedule.Publications.Select(x => x.Inserts.Count).Sum() > 0);
			}
			SettingsNotSaved = false;
			_allowToSave = true;
		}

		private void LoadView()
		{
			FormMain.Instance.checkBoxItemHomeAccountNumber.Enabled = _localSchedule.ViewSettings.HomeViewSettings.EnableAccountNumber;
			buttonXCode.Enabled = _localSchedule.ViewSettings.HomeViewSettings.EnableCode;
			buttonXLogo.Enabled = _localSchedule.ViewSettings.HomeViewSettings.EnableLogo;
			buttonXDelivery.Enabled = _localSchedule.ViewSettings.HomeViewSettings.EnableDelivery;
			buttonXReadership.Enabled = _localSchedule.ViewSettings.HomeViewSettings.EnableReadership;

			FormMain.Instance.checkBoxItemHomeAccountNumber.Checked = _localSchedule.ViewSettings.HomeViewSettings.ShowAccountNumber;
			buttonXCode.Checked = _localSchedule.ViewSettings.HomeViewSettings.ShowCode;
			buttonXLogo.Checked = _localSchedule.ViewSettings.HomeViewSettings.ShowLogo;
			buttonXDelivery.Checked = _localSchedule.ViewSettings.HomeViewSettings.ShowDelivery;
			buttonXReadership.Checked = _localSchedule.ViewSettings.HomeViewSettings.ShowReadership;
		}

		private void SaveView()
		{
			_localSchedule.ViewSettings.HomeViewSettings.ShowCode = buttonXCode.Checked;
			_localSchedule.ViewSettings.HomeViewSettings.ShowLogo = buttonXLogo.Checked;
			_localSchedule.ViewSettings.HomeViewSettings.ShowDelivery = buttonXDelivery.Checked;
			_localSchedule.ViewSettings.HomeViewSettings.ShowReadership = buttonXReadership.Checked;
		}

		private bool AllowToAddPublication()
		{
			gridViewPublications.CloseEditor();
			return FormMain.Instance.comboBoxEditBusinessName.EditValue != null &&
				   FormMain.Instance.comboBoxEditDecisionMaker.EditValue != null &&
				   FormMain.Instance.comboBoxEditClientType.EditValue != null &&
				   FormMain.Instance.dateEditPresentationDate.EditValue != null &&
				   FormMain.Instance.dateEditFlightDatesStart.EditValue != null &&
				   FormMain.Instance.dateEditFlightDatesEnd.EditValue != null;
		}

		private bool SaveSchedule(string scheduleName = "")
		{
			if (!string.IsNullOrEmpty(scheduleName))
				_localSchedule.Name = scheduleName;
			gridViewPublications.CloseEditor();
			if (FormMain.Instance.comboBoxEditBusinessName.EditValue != null)
				_localSchedule.BusinessName = FormMain.Instance.comboBoxEditBusinessName.EditValue.ToString();
			else
			{
				AppManager.ShowWarning("You must set Business Name before save");
				return false;
			}
			if (FormMain.Instance.comboBoxEditDecisionMaker.EditValue != null)
				_localSchedule.DecisionMaker = FormMain.Instance.comboBoxEditDecisionMaker.EditValue.ToString();
			else
			{
				AppManager.ShowWarning("You must set Owner/Decision-maker before save");
				return false;
			}


			if (FormMain.Instance.comboBoxEditClientType.EditValue != null)
				_localSchedule.ClientType = FormMain.Instance.comboBoxEditClientType.EditValue.ToString();
			else
			{
				AppManager.ShowWarning("You must set Client type before save");
				return false;
			}

			if (FormMain.Instance.checkBoxItemHomeAccountNumber.Checked && FormMain.Instance.textEditAccountNumber.EditValue != null)
				_localSchedule.AccountNumber = FormMain.Instance.textEditAccountNumber.EditValue.ToString();
			else
				_localSchedule.AccountNumber = string.Empty;

			if (FormMain.Instance.dateEditPresentationDate.DateTime != null)
				_localSchedule.PresentationDate = FormMain.Instance.dateEditPresentationDate.DateTime;
			else
			{
				AppManager.ShowWarning("You must set Presentation Date before save");
				return false;
			}
			if (FormMain.Instance.dateEditFlightDatesStart.DateTime != null)
			{
				_localSchedule.FlightDateStart = FormMain.Instance.dateEditFlightDatesStart.DateTime;
				if (_localSchedule.FlightDateStart.DayOfWeek != DayOfWeek.Sunday)
				{
					AppManager.ShowWarning("Flight Start Date must be Sunday\nFlight End Date must be Saturday\nFlight Start Date must be less then Flight End Date.");
					return false;
				}
			}
			else
			{
				AppManager.ShowWarning("You must set Flight Start Dates before save");
				return false;
			}
			if (FormMain.Instance.dateEditFlightDatesEnd.DateTime != null)
			{
				_localSchedule.FlightDateEnd = FormMain.Instance.dateEditFlightDatesEnd.DateTime;
				if (_localSchedule.FlightDateEnd.DayOfWeek != DayOfWeek.Saturday || _localSchedule.FlightDateEnd < _localSchedule.FlightDateStart)
				{
					AppManager.ShowWarning("Flight Start Date must be Sunday\nFlight End Date must be Saturday\nFlight Start Date must be less then Flight End Date.");
					return false;
				}
			}
			else
			{
				AppManager.ShowWarning("You must set Flight End Dates before save");
				return false;
			}

			foreach (Publication publication in _localSchedule.Publications)
				if (string.IsNullOrEmpty(publication.Name))
				{
					AppManager.ShowWarning("You must Select Name for all Publications before save");
					return false;
				}

			SaveView();

			repositoryItemComboBox.Items.Clear();
			repositoryItemComboBox.Items.AddRange(ListManager.Instance.PublicationSources.Select(x => x.Name).Distinct().ToArray());
			FormMain.Instance.comboBoxEditBusinessName.Properties.Items.Clear();
			FormMain.Instance.comboBoxEditBusinessName.Properties.Items.AddRange(ListManager.Instance.Advertisers.ToArray());
			FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.Clear();
			FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.AddRange(ListManager.Instance.DecisionMakers.ToArray());

			ScheduleManager.Instance.SaveSchedule(_localSchedule, false, this);
			laScheduleName.Text = _localSchedule.Name;
			SettingsNotSaved = false;
			return true;
		}

		private void ScheduleSettingsControl_Load(object sender, EventArgs e)
		{
			repositoryItemComboBox.Enter += FormMain.Instance.Editor_Enter;
			repositoryItemComboBox.MouseDown += FormMain.Instance.Editor_MouseDown;
			repositoryItemComboBox.MouseUp += FormMain.Instance.Editor_MouseUp;
			repositoryItemSpinEdit.Enter += FormMain.Instance.Editor_Enter;
			repositoryItemSpinEdit.MouseDown += FormMain.Instance.Editor_MouseDown;
			repositoryItemSpinEdit.MouseUp += FormMain.Instance.Editor_MouseUp;
			repositoryItemTextEdit.Enter += FormMain.Instance.Editor_Enter;
			repositoryItemTextEdit.MouseDown += FormMain.Instance.Editor_MouseDown;
			repositoryItemTextEdit.MouseUp += FormMain.Instance.Editor_MouseUp;

			AssignCloseActiveEditorsonOutSideClick(FormMain.Instance.ribbonControl);
			AssignCloseActiveEditorsonOutSideClick(pnHeader);
		}

		public void buttonItemPublicationsAdd_Click(object sender, EventArgs e)
		{
			if (AllowToAddPublication())
			{
				_localSchedule.AddPublication();
				((BindingList<Publication>)gridControlPublications.DataSource).ResetBindings();
				gridViewPublications.FocusedRowHandle = gridViewPublications.RowCount - 1;
				UpdatePublicationsCount();
				FormMain.Instance.UpdateScheduleTab(_localSchedule.Publications.Count > 0);
				FormMain.Instance.UpdateOutputTabs(_localSchedule.Publications.Select(x => x.Inserts.Count).Sum() > 0);
				SettingsNotSaved = true;
			}
			else
				using (var form = new FormAddPublicationWarning())
				{
					form.ShowDialog();
				}
		}

		public void buttonItemPublicationsClone_Click(object sender, EventArgs e)
		{
			if (gridViewPublications.FocusedRowHandle >= 0)
			{
				int newRowHandle = gridViewPublications.FocusedRowHandle + 1;
				((BindingList<Publication>)gridControlPublications.DataSource)[gridViewPublications.GetDataSourceRowIndex(gridViewPublications.FocusedRowHandle)].Clone();
				((BindingList<Publication>)gridControlPublications.DataSource).ResetBindings();
				gridViewPublications.FocusedRowHandle = newRowHandle;
				UpdatePublicationsCount();
				FormMain.Instance.UpdateScheduleTab(_localSchedule.Publications.Count > 0);
				FormMain.Instance.UpdateOutputTabs(_localSchedule.Publications.Select(x => x.Inserts.Count).Sum() > 0);
				if (_allowToSave)
				{
					SettingsNotSaved = true;
				}
			}
		}

		public void buttonItemPublicationsDelete_Click(object sender, EventArgs e)
		{
			if (AppManager.ShowWarningQuestion("Are you SURE you want to DELETE\nthis publication from your schedule?") == DialogResult.Yes)
			{
				gridViewPublications.DeleteSelectedRows();
				_localSchedule.RebuildPublicationIndexes();
				UpdatePublicationsCount();
				FormMain.Instance.UpdateScheduleTab(_localSchedule.Publications.Count > 0);
				FormMain.Instance.UpdateOutputTabs(_localSchedule.Publications.Select(x => x.Inserts.Count).Sum() > 0);
				if (_allowToSave)
				{
					SettingsNotSaved = true;
				}
			}
		}

		public void buttonItemPrintScheduleettingsSave_Click(object sender, EventArgs e)
		{
			if (SaveSchedule())
				AppManager.ShowInformation("Schedule Saved");
		}

		public void buttonItemPrintScheduleettingsSaveAs_Click(object sender, EventArgs e)
		{
			using (var from = new FormNewSchedule())
			{
				from.Text = "Save Schedule";
				from.laLogo.Text = "Please set a new name for your Schedule:";
				if (from.ShowDialog() == DialogResult.OK)
				{
					if (!string.IsNullOrEmpty(from.ScheduleName))
					{
						if (SaveSchedule(from.ScheduleName))
							AppManager.ShowInformation("Schedule was saved");
					}
					else
					{
						AppManager.ShowWarning("Schedule Name can't be empty");
					}
				}
			}
		}

		private void repositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			switch (e.Button.Index)
			{
				case 0:
					_localSchedule.UpPublication(gridViewPublications.GetDataSourceRowIndex(gridViewPublications.FocusedRowHandle));
					if (gridViewPublications.FocusedRowHandle > 0)
						gridViewPublications.FocusedRowHandle--;
					break;
				case 1:
					_localSchedule.DownPublication(gridViewPublications.GetDataSourceRowIndex(gridViewPublications.FocusedRowHandle));
					if (gridViewPublications.FocusedRowHandle < gridViewPublications.RowCount - 1)
						gridViewPublications.FocusedRowHandle++;
					break;
			}
			if (_allowToSave)
			{
				SettingsNotSaved = true;
			}
		}

		public void FlightDateStartEditValueChanged(object sender, EventArgs e)
		{
			if (FormMain.Instance.dateEditFlightDatesStart.EditValue != null)
			{
				DateTime dateStart = FormMain.Instance.dateEditFlightDatesStart.DateTime;
				while (dateStart.DayOfWeek != DayOfWeek.Saturday)
					dateStart = dateStart.AddDays(1);
				FormMain.Instance.dateEditFlightDatesEnd.EditValue = dateStart;
			}
		}

		public void CalcWeeksOnFlightDatesChange(object sender, EventArgs e)
		{
			FormMain.Instance.labelItemHomeFlightDatesWeeks.Text = "";
			FormMain.Instance.labelItemHomeFlightDatesWeeks.Visible = false;
			if (FormMain.Instance.dateEditFlightDatesStart.DateTime != null && FormMain.Instance.dateEditFlightDatesEnd.DateTime != null)
			{
				TimeSpan datesRange = FormMain.Instance.dateEditFlightDatesEnd.DateTime - FormMain.Instance.dateEditFlightDatesStart.DateTime;
				int weeksCount = datesRange.Days / 7 + 1;
				FormMain.Instance.labelItemHomeFlightDatesWeeks.Text = weeksCount.ToString() + (weeksCount > 1 ? " Weeks" : " Week");
				FormMain.Instance.labelItemHomeFlightDatesWeeks.Visible = true;
			}
		}

		public void checkBoxItemAccountNumber_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
		{
			FormMain.Instance.textEditAccountNumber.Enabled = FormMain.Instance.checkBoxItemHomeAccountNumber.Checked;
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
			if (e.Value != null)
			{
				DateTime temp = DateTime.MinValue;
				if (DateTime.TryParse(e.Value.ToString(), out temp))
				{
					while (temp.DayOfWeek != DayOfWeek.Sunday)
						temp = temp.AddDays(-1);
					e.Value = temp;
				}
			}
		}

		public void dateEditFlightDatesEnd_CloseUp(object sender, CloseUpEventArgs e)
		{
			if (e.Value != null)
			{
				DateTime temp = DateTime.MinValue;
				if (DateTime.TryParse(e.Value.ToString(), out temp))
				{
					while (temp.DayOfWeek != DayOfWeek.Saturday)
						temp = temp.AddDays(1);
					e.Value = temp;
				}
			}
		}

		public void buttonItemPrintScheduleettingsHelp_Click(object sender, EventArgs e)
		{
			HelpManager.Instance.OpenHelpLink("home");
		}

		private void gridViewPublications_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			if (e.Column == gridColumnName)
			{
				if (e.Value != null)
				{
					string value = e.Value.ToString();
					PublicationSource publicationSource = ListManager.Instance.PublicationSources.Where(x => x.Name.Equals(value)).FirstOrDefault();
					if (publicationSource != null)
					{
						_localSchedule.Publications[gridViewPublications.GetFocusedDataSourceRowIndex()].ApplyDefaultValues();
						gridViewPublications.RefreshData();
					}
				}
			}
			SchedulePropertyEditValueChanged(null, null);
		}

		private void repositoryItemComboBox_Closed(object sender, ClosedEventArgs e)
		{
			gridViewPublications.CloseEditor();
		}

		private void repositoryItemButtonEditChangeLogo_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			using (var form = new FormImageGallery())
			{
				if (form.ShowDialog() == DialogResult.OK)
				{
					if (form.SelectedSource != null)
					{
						_localSchedule.Publications[gridViewPublications.GetFocusedDataSourceRowIndex()].BigLogo = new Bitmap(form.SelectedSource.BigLogo);
						_localSchedule.Publications[gridViewPublications.GetFocusedDataSourceRowIndex()].SmallLogo = new Bitmap(form.SelectedSource.SmallLogo);
						_localSchedule.Publications[gridViewPublications.GetFocusedDataSourceRowIndex()].TinyLogo = new Bitmap(form.SelectedSource.TinyLogo);
						gridViewPublications.RefreshData();
					}
				}
			}
		}

		public void buttonItemSalesStrategyAbbreviation_CheckedChanged(object sender, EventArgs e)
		{
			gridBandAbbreviation.Visible = buttonXCode.Checked;
			if (_allowToSave)
			{
				SettingsNotSaved = true;
			}
		}

		public void buttonItemSalesStrategyLogo_CheckedChanged(object sender, EventArgs e)
		{
			gridBandLogo.Visible = buttonXLogo.Checked;
			if (_allowToSave)
			{
				SettingsNotSaved = true;
			}
		}

		public void buttonItemSalesStrategyReadership_CheckedChanged(object sender, EventArgs e)
		{
			gridColumnDailyReadership.Visible = buttonXReadership.Checked;
			gridColumnSundayReadership.Visible = buttonXReadership.Checked;
			if (!buttonXReadership.Checked && !buttonXDelivery.Checked)
				gridColumnName.RowCount = 2;
			else
				gridColumnName.RowCount = 1;
			if (_allowToSave)
			{
				SettingsNotSaved = true;
			}
		}

		public void buttonItemSalesStrategyDelivery_CheckedChanged(object sender, EventArgs e)
		{
			gridColumnDailyDelivery.Visible = buttonXDelivery.Checked;
			gridColumnSundayDelivery.Visible = buttonXDelivery.Checked;
			if (!buttonXReadership.Checked && !buttonXDelivery.Checked)
				gridColumnName.RowCount = 2;
			else
				gridColumnName.RowCount = 1;
			if (_allowToSave)
			{
				SettingsNotSaved = true;
			}
		}
	}
}