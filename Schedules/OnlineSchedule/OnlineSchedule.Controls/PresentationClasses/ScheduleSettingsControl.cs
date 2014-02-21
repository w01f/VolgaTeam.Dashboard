using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraTab;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.BusinessClasses;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses.ToolForms;
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
			if (control != Controller.Instance.HomeBusinessName
				&& control != Controller.Instance.HomeClientType
				&& control != Controller.Instance.HomeDecisionMaker
				&& control != Controller.Instance.HomeAccountNumberText
				&& control != Controller.Instance.HomeFlightDatesEnd
				&& control != Controller.Instance.HomeFlightDatesStart
				&& control != Controller.Instance.HomePresentationDate)
			{
				control.Click += CloseActiveEditorsonOutSideClick;
				foreach (Control childControl in control.Controls)
					AssignCloseActiveEditorsonOutSideClick(childControl);
			}
		}

		private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
		{
			Focus();
			advBandedGridViewDigitalProducts.CloseEditor();
		}

		private void UpdateProductsCount()
		{
			xtraTabPageDigitalProducts.Text = String.Format("Digital ({0})", _localSchedule.DigitalProducts.Count);
		}

		public void LoadSchedule(bool quickLoad)
		{
			_allowToSave = false;
			_localSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			gridControlDigitalProducts.DataSource = new BindingList<DigitalProduct>(_localSchedule.DigitalProducts);
			if (ListManager.Instance.LockedMode)
			{
				gridColumnDigitalProductsWidth.OptionsColumn.ReadOnly = true;
				gridColumnDigitalProductsWidth.OptionsColumn.AllowEdit = false;
				gridColumnDigitalProductsHeight.OptionsColumn.ReadOnly = true;
				gridColumnDigitalProductsHeight.OptionsColumn.AllowEdit = false;
				gridColumnDigitalProductsRate.OptionsColumn.ReadOnly = true;
				gridColumnDigitalProductsRate.OptionsColumn.AllowEdit = false;
				gridColumnDigitalProductsRateType.OptionsColumn.ReadOnly = true;
				gridColumnDigitalProductsRateType.OptionsColumn.AllowEdit = false;
				repositoryItemComboBoxDigitalProductsNames.TextEditStyle = TextEditStyles.DisableTextEditor;
			}
			if (!quickLoad)
			{
				LoadView();

				Controller.Instance.HomeBusinessName.Properties.Items.Clear();
				Controller.Instance.HomeBusinessName.Properties.Items.AddRange(Core.Common.ListManager.Instance.Advertisers.ToArray());
				Controller.Instance.HomeDecisionMaker.Properties.Items.Clear();
				Controller.Instance.HomeDecisionMaker.Properties.Items.AddRange(Core.Common.ListManager.Instance.DecisionMakers.ToArray());
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

				UpdateProductsCount();

				Controller.Instance.UpdateSimpleOutputTabPageState(_localSchedule.DigitalProducts.Any(p => !String.IsNullOrEmpty(p.Name)));
			}
			SettingsNotSaved = false;
			_allowToSave = true;
		}

		private void LoadView()
		{
			Controller.Instance.HomeAccountNumberCheck.Enabled = _localSchedule.ViewSettings.HomeViewSettings.EnableAccountNumber;
			buttonXDigitalProductDimensions.Checked = _localSchedule.ViewSettings.HomeViewSettings.ShowDigitalDimensions;
			buttonXDigitalProductStrategy.Checked = _localSchedule.ViewSettings.HomeViewSettings.ShowDigitalStrategy;
		}

		private void SaveView()
		{
			_localSchedule.ViewSettings.HomeViewSettings.ShowDigitalDimensions = buttonXDigitalProductDimensions.Checked;
			_localSchedule.ViewSettings.HomeViewSettings.ShowDigitalStrategy = buttonXDigitalProductStrategy.Checked;
		}

		private bool SaveSchedule(string scheduleName = "")
		{
			if (!string.IsNullOrEmpty(scheduleName))
				_localSchedule.Name = scheduleName;
			advBandedGridViewDigitalProducts.CloseEditor();
			if (Controller.Instance.HomeBusinessName.EditValue != null)
				_localSchedule.BusinessName = Controller.Instance.HomeBusinessName.EditValue.ToString();
			else
			{
				Utilities.Instance.ShowWarning("You must set Business Name before save");
				return false;
			}
			if (Controller.Instance.HomeDecisionMaker.EditValue != null)
				_localSchedule.DecisionMaker = Controller.Instance.HomeDecisionMaker.EditValue.ToString();
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
			if (Controller.Instance.HomeFlightDatesStart.EditValue != null)
			{
				_localSchedule.FlightDateStart = Controller.Instance.HomeFlightDatesStart.DateTime;
				if (_localSchedule.FlightDateStart.Value.DayOfWeek != DayOfWeek.Sunday)
				{
					Utilities.Instance.ShowWarning("Flight Start Date must be Sunday\nFlight End Date must be Saturday\nFlight Start Date must be less then Flight End Date.");
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
				if (_localSchedule.FlightDateEnd.Value.DayOfWeek != DayOfWeek.Saturday || _localSchedule.FlightDateEnd < _localSchedule.FlightDateStart)
				{
					Utilities.Instance.ShowWarning("Flight Start Date must be Sunday\nFlight End Date must be Saturday\nFlight Start Date must be less then Flight End Date.");
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

			Controller.Instance.HomeBusinessName.Properties.Items.Clear();
			Controller.Instance.HomeBusinessName.Properties.Items.AddRange(Core.Common.ListManager.Instance.Advertisers.ToArray());
			Controller.Instance.HomeDecisionMaker.Properties.Items.Clear();
			Controller.Instance.HomeDecisionMaker.Properties.Items.AddRange(Core.Common.ListManager.Instance.DecisionMakers.ToArray());
			Controller.Instance.SaveSchedule(_localSchedule, false, this);
			SettingsNotSaved = false;
			return true;
		}

		private void RefreshDigitalAfterAddProduct()
		{
			((BindingList<DigitalProduct>)advBandedGridViewDigitalProducts.DataSource).ResetBindings();
			advBandedGridViewDigitalProducts.FocusedRowHandle = advBandedGridViewDigitalProducts.RowCount - 1;
			UpdateProductsCount();
			Controller.Instance.UpdateSimpleOutputTabPageState(_localSchedule.DigitalProducts.Any(p => !String.IsNullOrEmpty(p.Name)));
			SettingsNotSaved = true;
		}
		#endregion

		#region Schedule Event Handlers
		private void ScheduleSettingsControl_Load(object sender, EventArgs e)
		{
			repositoryItemComboBoxDigitalProductsType.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemComboBoxDigitalProductsType.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemComboBoxDigitalProductsType.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemComboBoxDigitalProductsNames.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemComboBoxDigitalProductsNames.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemComboBoxDigitalProductsNames.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemSpinEditDigitalProductsSize.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemSpinEditDigitalProductsSize.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemSpinEditDigitalProductsSize.MouseUp += Utilities.Instance.Editor_MouseUp;

			AssignCloseActiveEditorsonOutSideClick(Controller.Instance.Ribbon);
		}

		public void ScheduleSettingsSave_Click(object sender, EventArgs e)
		{
			if (SaveSchedule())
				Utilities.Instance.ShowInformation("Schedule Saved");
		}

		public void ScheduleSettingsSaveAs_Click(object sender, EventArgs e)
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
							Utilities.Instance.ShowInformation("Schedule was saved");
					}
					else
					{
						Utilities.Instance.ShowWarning("Schedule Name can't be empty");
					}
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
			if (Controller.Instance.HomeFlightDatesStart.EditValue != null && Controller.Instance.HomeFlightDatesEnd.EditValue != null)
			{
				TimeSpan datesRange = Controller.Instance.HomeFlightDatesEnd.DateTime - Controller.Instance.HomeFlightDatesStart.DateTime;
				int weeksCount = datesRange.Days / 7 + 1;
				Controller.Instance.HomeWeeks.Text = weeksCount.ToString() + (weeksCount > 1 ? " Weeks" : " Week");
				Controller.Instance.HomeWeeks.Visible = true;
			}
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
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("home");
		}
		#endregion

		#region Digital Product Events
		public void DigitalProductAdd(object sender, EventArgs e)
		{
			var category = (sender as ButtonItem).Tag as Category;
			_localSchedule.AddProduct(category.Name);
			RefreshDigitalAfterAddProduct();
		}

		public void DigitalProductClone(object sender, EventArgs e)
		{
			if (advBandedGridViewDigitalProducts.FocusedRowHandle == GridControl.InvalidRowHandle) return;
			var newRowHandle = advBandedGridViewDigitalProducts.FocusedRowHandle + 1;
			((BindingList<DigitalProduct>)advBandedGridViewDigitalProducts.DataSource)[advBandedGridViewDigitalProducts.GetDataSourceRowIndex(advBandedGridViewDigitalProducts.FocusedRowHandle)].Clone();
			((BindingList<DigitalProduct>)advBandedGridViewDigitalProducts.DataSource).ResetBindings();
			advBandedGridViewDigitalProducts.FocusedRowHandle = newRowHandle;
			RefreshDigitalAfterAddProduct();
			if (_allowToSave)
				SettingsNotSaved = true;
		}

		public void DigitalProductDelete(object sender, EventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Are you sure you want to delete this line?") != DialogResult.Yes) return;
			advBandedGridViewDigitalProducts.DeleteSelectedRows();
			_localSchedule.RebuildDigitalProductIndexes();
			UpdateProductsCount();
			RefreshDigitalAfterAddProduct();
			SettingsNotSaved = true;
		}

		private void repositoryItemButtonEditDigitalProducts_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			switch (e.Button.Index)
			{
				case 0:
					_localSchedule.UpProduct(advBandedGridViewDigitalProducts.GetDataSourceRowIndex(advBandedGridViewDigitalProducts.FocusedRowHandle));
					if (advBandedGridViewDigitalProducts.FocusedRowHandle > 0)
						advBandedGridViewDigitalProducts.FocusedRowHandle--;
					break;
				case 1:
					_localSchedule.DownProduct(advBandedGridViewDigitalProducts.GetDataSourceRowIndex(advBandedGridViewDigitalProducts.FocusedRowHandle));
					if (advBandedGridViewDigitalProducts.FocusedRowHandle < advBandedGridViewDigitalProducts.RowCount - 1)
						advBandedGridViewDigitalProducts.FocusedRowHandle++;
					break;
			}
			SettingsNotSaved = true;
		}

		private void repositoryItemButtonEditDigitalProductsDelete_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			DigitalProductDelete(sender, EventArgs.Empty);
		}

		private void gridViewDigitalProducts_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			if (e.Column == gridColumnDigitalProductsName ||
				e.Column == gridColumnDigitalProductsCategory ||
				e.Column == gridColumnDigitalProductsSubCategory)
			{
				var product = advBandedGridViewDigitalProducts.GetFocusedRow() as DigitalProduct;
				if (product != null)
				{
					var productSource = ListManager.Instance.ProductSources.FirstOrDefault(x => x.Name.Equals(product.Name));
					if (productSource != null)
					{
						product.ApplyDefaultValues();
						advBandedGridViewDigitalProducts.RefreshData();
					}
				}
			}
			SchedulePropertyEditValueChanged(null, null);
			Controller.Instance.UpdateSimpleOutputTabPageState(_localSchedule.DigitalProducts.Any(p => !String.IsNullOrEmpty(p.Name)));
		}

		private void repositoryItemComboBoxDigitalProductName_Closed(object sender, ClosedEventArgs e)
		{
			advBandedGridViewDigitalProducts.CloseEditor();
		}

		private void gridViewDigitalProducts_ShowingEditor(object sender, CancelEventArgs e)
		{
			e.Cancel = false;
			if (advBandedGridViewDigitalProducts.FocusedColumn == gridColumnDigitalProductsName)
			{
				var category = _localSchedule.DigitalProducts[advBandedGridViewDigitalProducts.GetFocusedDataSourceRowIndex()].Category;
				var subCategories = ListManager.Instance.ProductSources.Where(x => x.Category.Name.Equals(category) && !string.IsNullOrEmpty(x.SubCategory)).Select(x => x.SubCategory).Distinct().ToArray();
				var subCategory = _localSchedule.DigitalProducts[advBandedGridViewDigitalProducts.GetFocusedDataSourceRowIndex()].SubCategory;
				if ((subCategories.Any() && !String.IsNullOrEmpty(subCategory)) || !subCategories.Any())
				{
					repositoryItemComboBoxDigitalProductsNames.Items.Clear();
					repositoryItemComboBoxDigitalProductsNames.Items.AddRange(
						ListManager.Instance.ProductSources.
							Where(x => x.Category.Name.Equals(category) &&
									   (x.SubCategory.Equals(subCategory) || String.IsNullOrEmpty(subCategory))).
							Select(x => x.Name).Distinct().ToArray());
				}
				else
				{
					e.Cancel = true;
					Utilities.Instance.ShowWarning("You need to select Web Category first");
				}
			}
			else if (advBandedGridViewDigitalProducts.FocusedColumn == gridColumnDigitalProductsType)
			{
				var category = _localSchedule.DigitalProducts[advBandedGridViewDigitalProducts.GetFocusedDataSourceRowIndex()].Category;
				var subCategories = ListManager.Instance.ProductSources.Where(x => x.Category.Name.Equals(category) && !string.IsNullOrEmpty(x.SubCategory)).Select(x => x.SubCategory).Distinct().ToArray();
				if (subCategories.Any())
				{
					repositoryItemComboBoxDigitalProductsType.Items.Clear();
					repositoryItemComboBoxDigitalProductsType.Items.AddRange(subCategories);
				}
				else
					e.Cancel = true;
			}
		}

		private void repositoryItemComboBoxDigitalProductType_CloseUp(object sender, CloseUpEventArgs e)
		{
			if (e.CloseMode == PopupCloseMode.Normal)
			{
				e.AcceptValue = false;
				advBandedGridViewDigitalProducts.SetFocusedRowCellValue(gridColumnDigitalProductsSubCategory, e.Value);
				advBandedGridViewDigitalProducts.CloseEditor();
			}
		}

		private void buttonXDigitalProductDimensions_CheckedChanged(object sender, EventArgs e)
		{
			gridBandDigitalProductWidth.Visible = buttonXDigitalProductDimensions.Checked;
			gridBandDigitalProductHeight.Visible = buttonXDigitalProductDimensions.Checked;
			if (_allowToSave)
			{
				SettingsNotSaved = true;
			}
		}

		private void buttonXDigitalProductStrategy_CheckedChanged(object sender, EventArgs e)
		{
			gridBandDigitalProductRate.Visible = buttonXDigitalProductStrategy.Checked;
			if (_allowToSave)
			{
				SettingsNotSaved = true;
			}
		}
		#endregion
	}
}