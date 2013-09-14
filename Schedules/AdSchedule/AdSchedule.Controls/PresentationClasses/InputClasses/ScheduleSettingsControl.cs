using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraTab;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using ListManager = NewBizWiz.Core.OnlineSchedule.ListManager;
using Schedule = NewBizWiz.Core.AdSchedule.Schedule;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.InputClasses
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
				Controller.Instance.HomeDigitalProductAdd.SubItems.Add(categoryButton);
			}
		}

		private void AssignCloseActiveEditorsonOutSideClick(Control control)
		{
			if (control != Controller.Instance.HomeBusinessName
				&& control != Controller.Instance.HomeClientType
				&& control != Controller.Instance.HomeDecisionMaker
				&& control != Controller.Instance.HomeAccountNumberText
				&& control != Controller.Instance.PrintProductRateCard
				&& control != Controller.Instance.RateCardCombo
				&& control != Controller.Instance.HomeFlightDatesEnd
				&& control != Controller.Instance.HomeFlightDatesStart
				&& control != Controller.Instance.HomePresentationDate
				&& control != Controller.Instance.PrintProductStandartHeight
				&& control != Controller.Instance.PrintProductStandartWidth
				&& control != Controller.Instance.PrintProductPercentOfPage
				&& control != Controller.Instance.PrintProductSharePagePageSizeCombo
				&& control != Controller.Instance.PrintProductStandartPageSizeCombo
				&& control != Controller.Instance.PrintProductSharePageSquare
				&& control != Controller.Instance.PrintProductCostPerInch)
			{
				control.Click += CloseActiveEditorsonOutSideClick;
				foreach (Control childControl in control.Controls)
					AssignCloseActiveEditorsonOutSideClick(childControl);
			}
		}

		private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
		{
			Focus();
			gridViewPrintProducts.CloseEditor();
			advBandedGridViewDigitalProducts.CloseEditor();
		}

		private void UpdateProductsCount()
		{
			xtraTabPagePrintProducts.Text = String.Format("Publications ({0})", _localSchedule.PrintProducts.Count);
			xtraTabPageDigitalProducts.Text = String.Format("Digital ({0})", _localSchedule.DigitalProducts.Count);
		}

		public void LoadSchedule(bool quickLoad)
		{
			_allowToSave = false;
			_localSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			gridControlPrintProducts.DataSource = new BindingList<PrintProduct>(_localSchedule.PrintProducts);
			gridControlDigitalProducts.DataSource = new BindingList<DigitalProduct>(_localSchedule.DigitalProducts);
			if (!quickLoad)
			{
				LoadView();

				#region Print Products
				repositoryItemComboBoxPrintProducts.Items.Clear();
				repositoryItemComboBoxPrintProducts.Items.AddRange(Core.AdSchedule.ListManager.Instance.PublicationSources.Where(x => !x.Name.Equals("Default")).Select(x => x.Name).Distinct().ToArray());
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
				Controller.Instance.UpdatePrintProductTab(_localSchedule.PrintProducts.Any(p => !String.IsNullOrEmpty(p.Name)));
				#endregion

				#region Digital Products
				Controller.Instance.UpdateDigitalProductTab(_localSchedule.DigitalProducts.Any(p => !String.IsNullOrEmpty(p.Name)));
				#endregion

				UpdateProductsCount();
				Controller.Instance.UpdateOutputTabs(_localSchedule.PrintProducts.Select(x => x.Inserts.Count).Sum() > 0);
			}
			SettingsNotSaved = false;
			_allowToSave = true;
		}

		private void LoadView()
		{
			Controller.Instance.HomeAccountNumberCheck.Checked = _localSchedule.ViewSettings.HomeViewSettings.ShowAccountNumber;
			buttonXPrintProductCode.Checked = _localSchedule.ViewSettings.HomeViewSettings.ShowPrintCode;
			buttonXPrintProductLogo.Checked = _localSchedule.ViewSettings.HomeViewSettings.ShowPrintLogo;
			buttonXPrintProductDelivery.Checked = _localSchedule.ViewSettings.HomeViewSettings.ShowPrintDelivery;
			buttonXPrintProductReadership.Checked = _localSchedule.ViewSettings.HomeViewSettings.ShowPrintReadership;

			buttonXDigitalProductDimensions.Checked = _localSchedule.ViewSettings.HomeViewSettings.ShowDigitalDimensions;
			buttonXDigitalProductStrategy.Checked = _localSchedule.ViewSettings.HomeViewSettings.ShowDigitalStrategy;
		}

		private void SaveView()
		{
			_localSchedule.ViewSettings.HomeViewSettings.ShowPrintCode = buttonXPrintProductCode.Checked;
			_localSchedule.ViewSettings.HomeViewSettings.ShowPrintLogo = buttonXPrintProductLogo.Checked;
			_localSchedule.ViewSettings.HomeViewSettings.ShowPrintDelivery = buttonXPrintProductDelivery.Checked;
			_localSchedule.ViewSettings.HomeViewSettings.ShowPrintReadership = buttonXPrintProductReadership.Checked;

			_localSchedule.ViewSettings.HomeViewSettings.ShowDigitalDimensions = buttonXDigitalProductDimensions.Checked;
			_localSchedule.ViewSettings.HomeViewSettings.ShowDigitalStrategy = buttonXDigitalProductStrategy.Checked;
		}

		private bool AllowToAddPublication()
		{
			gridViewPrintProducts.CloseEditor();
			return Controller.Instance.HomeBusinessName.EditValue != null && !String.IsNullOrEmpty(Controller.Instance.HomeBusinessName.EditValue.ToString()) &&
				   Controller.Instance.HomeDecisionMaker.EditValue != null && !String.IsNullOrEmpty(Controller.Instance.HomeDecisionMaker.EditValue.ToString()) &&
				   Controller.Instance.HomeClientType.EditValue != null && !String.IsNullOrEmpty(Controller.Instance.HomeClientType.EditValue.ToString()) &&
				   Controller.Instance.HomePresentationDate.DateTime != DateTime.MinValue &&
				   Controller.Instance.HomeFlightDatesStart.DateTime != DateTime.MinValue &&
				   Controller.Instance.HomeFlightDatesEnd.DateTime != DateTime.MinValue;
		}

		private bool SaveSchedule(string scheduleName = "")
		{
			if (!string.IsNullOrEmpty(scheduleName))
				_localSchedule.Name = scheduleName;
			gridViewPrintProducts.CloseEditor();
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
				if (_localSchedule.FlightDateStart.DayOfWeek != DayOfWeek.Sunday)
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
				if (_localSchedule.FlightDateEnd.DayOfWeek != DayOfWeek.Saturday || _localSchedule.FlightDateEnd < _localSchedule.FlightDateStart)
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

			foreach (PrintProduct publication in _localSchedule.PrintProducts)
				if (string.IsNullOrEmpty(publication.Name))
				{
					Utilities.Instance.ShowWarning("You must Select Name for all Publications before save");
					return false;
				}
			foreach (DigitalProduct publication in _localSchedule.DigitalProducts)
				if (string.IsNullOrEmpty(publication.Name))
				{
					Utilities.Instance.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Web Product in each line before you proceed.");
					return false;
				}

			SaveView();

			repositoryItemComboBoxPrintProducts.Items.Clear();
			repositoryItemComboBoxPrintProducts.Items.AddRange(Core.AdSchedule.ListManager.Instance.PublicationSources.Select(x => x.Name).Distinct().ToArray());
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
			Controller.Instance.UpdateDigitalProductTab(_localSchedule.DigitalProducts.Any(p => !String.IsNullOrEmpty(p.Name)));
			SettingsNotSaved = true;
		}
		#endregion

		#region Schedule Event Handlers
		private void ScheduleSettingsControl_Load(object sender, EventArgs e)
		{
			repositoryItemComboBoxPrintProducts.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemComboBoxPrintProducts.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemComboBoxPrintProducts.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemSpinEditPrintProducts.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemSpinEditPrintProducts.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemSpinEditPrintProducts.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemTextEditPrintProducts.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemTextEditPrintProducts.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemTextEditPrintProducts.MouseUp += Utilities.Instance.Editor_MouseUp;

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

		private void xtraTabControlProducts_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			if (e.Page == xtraTabPagePrintProducts)
			{
				Controller.Instance.HomeAdProduct.Visible = true;
				Controller.Instance.HomeDigitalProduct.Visible = false;
			}
			else
			{
				Controller.Instance.HomeAdProduct.Visible = false;
				Controller.Instance.HomeDigitalProduct.Visible = true;
			}
			Controller.Instance.HomeProduct.RecalcLayout();
			Controller.Instance.HomePanel.PerformLayout();
			UpdateProductsCount();
		}

		public void buttonItemPrintScheduleettingsSave_Click(object sender, EventArgs e)
		{
			if (SaveSchedule())
				Utilities.Instance.ShowInformation("Schedule Saved");
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

		public void buttonItemPrintScheduleettingsHelp_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("home");
		}
		#endregion

		#region Print Product Events
		public void PrintProductAdd(object sender, EventArgs e)
		{
			if (AllowToAddPublication())
			{
				_localSchedule.AddPublication();
				((BindingList<PrintProduct>)gridControlPrintProducts.DataSource).ResetBindings();
				gridViewPrintProducts.FocusedRowHandle = gridViewPrintProducts.RowCount - 1;
				UpdateProductsCount();
				Controller.Instance.UpdatePrintProductTab(_localSchedule.PrintProducts.Any(p => !String.IsNullOrEmpty(p.Name)));
				Controller.Instance.UpdateOutputTabs(_localSchedule.PrintProducts.Select(x => x.Inserts.Count).Sum() > 0);
				SettingsNotSaved = true;
			}
			else
				using (var form = new FormAddPublicationWarning())
				{
					form.ShowDialog();
				}
		}

		public void PrintProductClone(object sender, EventArgs e)
		{
			if (gridViewPrintProducts.FocusedRowHandle >= 0)
			{
				int newRowHandle = gridViewPrintProducts.FocusedRowHandle + 1;
				((BindingList<PrintProduct>)gridControlPrintProducts.DataSource)[gridViewPrintProducts.GetDataSourceRowIndex(gridViewPrintProducts.FocusedRowHandle)].Clone();
				((BindingList<PrintProduct>)gridControlPrintProducts.DataSource).ResetBindings();
				gridViewPrintProducts.FocusedRowHandle = newRowHandle;
				UpdateProductsCount();
				Controller.Instance.UpdatePrintProductTab(_localSchedule.PrintProducts.Any(p => !String.IsNullOrEmpty(p.Name)));
				Controller.Instance.UpdateOutputTabs(_localSchedule.PrintProducts.Select(x => x.Inserts.Count).Sum() > 0);
				if (_allowToSave)
				{
					SettingsNotSaved = true;
				}
			}
		}

		public void PrintProductDelete(object sender, EventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Are you SURE you want to DELETE\nthis publication from your schedule?") == DialogResult.Yes)
			{
				gridViewPrintProducts.DeleteSelectedRows();
				_localSchedule.RebuildPublicationIndexes();
				UpdateProductsCount();
				Controller.Instance.UpdatePrintProductTab(_localSchedule.PrintProducts.Any(p => !String.IsNullOrEmpty(p.Name)));
				Controller.Instance.UpdateOutputTabs(_localSchedule.PrintProducts.Select(x => x.Inserts.Count).Sum() > 0);
				if (_allowToSave)
				{
					SettingsNotSaved = true;
				}
			}
		}

		private void gridViewPublications_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			if (e.Column == gridColumnPrintProductsName)
			{
				if (e.Value != null)
				{
					string value = e.Value.ToString();
					PrintProductSource printProductSource = Core.AdSchedule.ListManager.Instance.PublicationSources.Where(x => x.Name.Equals(value)).FirstOrDefault();
					if (printProductSource != null)
					{
						_localSchedule.PrintProducts[gridViewPrintProducts.GetFocusedDataSourceRowIndex()].ApplyDefaultValues();
						gridViewPrintProducts.RefreshData();
					}
				}
			}
			Controller.Instance.UpdatePrintProductTab(_localSchedule.PrintProducts.Any(p => !String.IsNullOrEmpty(p.Name)));
			SchedulePropertyEditValueChanged(null, null);
		}

		private void repositoryItemComboBox_Closed(object sender, ClosedEventArgs e)
		{
			gridViewPrintProducts.CloseEditor();
		}

		private void repositoryItemButtonEditChangeLogo_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			using (var form = new FormImageGallery(Core.AdSchedule.ListManager.Instance.Images))
			{
				form.SelectedImage = _localSchedule.PrintProducts[gridViewPrintProducts.GetFocusedDataSourceRowIndex()].BigLogo;
				if (form.ShowDialog() != DialogResult.OK) return;
				if (form.SelectedImageSource == null) return;
				_localSchedule.PrintProducts[gridViewPrintProducts.GetFocusedDataSourceRowIndex()].BigLogo = new Bitmap(form.SelectedImageSource.BigImage);
				_localSchedule.PrintProducts[gridViewPrintProducts.GetFocusedDataSourceRowIndex()].SmallLogo = new Bitmap(form.SelectedImageSource.SmallImage);
				_localSchedule.PrintProducts[gridViewPrintProducts.GetFocusedDataSourceRowIndex()].TinyLogo = new Bitmap(form.SelectedImageSource.TinyImage);
				gridViewPrintProducts.RefreshData();
			}
		}

		public void buttonItemSalesStrategyAbbreviation_CheckedChanged(object sender, EventArgs e)
		{
			gridBandAbbreviation.Visible = buttonXPrintProductCode.Checked;
			if (_allowToSave)
			{
				SettingsNotSaved = true;
			}
		}

		public void buttonItemSalesStrategyLogo_CheckedChanged(object sender, EventArgs e)
		{
			gridBandLogo.Visible = buttonXPrintProductLogo.Checked;
			if (_allowToSave)
			{
				SettingsNotSaved = true;
			}
		}

		public void buttonItemSalesStrategyReadership_CheckedChanged(object sender, EventArgs e)
		{
			gridColumnPrintProductsDailyReadership.Visible = buttonXPrintProductReadership.Checked;
			gridColumnPrintProductsSundayReadership.Visible = buttonXPrintProductReadership.Checked;
			if (!buttonXPrintProductReadership.Checked && !buttonXPrintProductDelivery.Checked)
				gridColumnPrintProductsName.RowCount = 2;
			else
				gridColumnPrintProductsName.RowCount = 1;
			if (_allowToSave)
			{
				SettingsNotSaved = true;
			}
		}

		public void buttonItemSalesStrategyDelivery_CheckedChanged(object sender, EventArgs e)
		{
			gridColumnPrintProductsDailyDelivery.Visible = buttonXPrintProductDelivery.Checked;
			gridColumnPrintProductsSundayDelivery.Visible = buttonXPrintProductDelivery.Checked;
			if (!buttonXPrintProductReadership.Checked && !buttonXPrintProductDelivery.Checked)
				gridColumnPrintProductsName.RowCount = 2;
			else
				gridColumnPrintProductsName.RowCount = 1;
			if (_allowToSave)
			{
				SettingsNotSaved = true;
			}
		}

		private void repositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			switch (e.Button.Index)
			{
				case 0:
					_localSchedule.UpPublication(gridViewPrintProducts.GetDataSourceRowIndex(gridViewPrintProducts.FocusedRowHandle));
					if (gridViewPrintProducts.FocusedRowHandle > 0)
						gridViewPrintProducts.FocusedRowHandle--;
					break;
				case 1:
					_localSchedule.DownPublication(gridViewPrintProducts.GetDataSourceRowIndex(gridViewPrintProducts.FocusedRowHandle));
					if (gridViewPrintProducts.FocusedRowHandle < gridViewPrintProducts.RowCount - 1)
						gridViewPrintProducts.FocusedRowHandle++;
					break;
			}
			if (_allowToSave)
			{
				SettingsNotSaved = true;
			}
		}
		#endregion

		#region Digital Product Events
		public void DigitalProductAdd(object sender, EventArgs e)
		{
			var category = (sender as ButtonItem).Tag as Category;
			_localSchedule.AddDigital(category.Name);
			RefreshDigitalAfterAddProduct();
		}

		public void DigitalProductDelete(object sender, EventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Are you sure you want to delete this line?") == DialogResult.Yes)
			{
				advBandedGridViewDigitalProducts.DeleteSelectedRows();
				_localSchedule.RebuildDigitalIndexes();
				UpdateProductsCount();
				RefreshDigitalAfterAddProduct();
				SettingsNotSaved = true;
			}
		}

		private void repositoryItemButtonEditDigitalProducts_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			switch (e.Button.Index)
			{
				case 0:
					_localSchedule.UpDigital(advBandedGridViewDigitalProducts.GetDataSourceRowIndex(advBandedGridViewDigitalProducts.FocusedRowHandle));
					if (advBandedGridViewDigitalProducts.FocusedRowHandle > 0)
						advBandedGridViewDigitalProducts.FocusedRowHandle--;
					break;
				case 1:
					_localSchedule.DownDigital(advBandedGridViewDigitalProducts.GetDataSourceRowIndex(advBandedGridViewDigitalProducts.FocusedRowHandle));
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
			if (e.Column == gridColumnDigitalProductsName)
			{
				if (e.Value != null)
				{
					string value = e.Value.ToString();
					ProductSource productSource = ListManager.Instance.ProductSources.Where(x => x.Name.Equals(value)).FirstOrDefault();
					if (productSource != null)
					{
						_localSchedule.DigitalProducts[advBandedGridViewDigitalProducts.GetFocusedDataSourceRowIndex()].ApplyDefaultValues();
						advBandedGridViewDigitalProducts.RefreshData();
					}
				}
			}
			SchedulePropertyEditValueChanged(null, null);
			Controller.Instance.UpdateDigitalProductTab(_localSchedule.DigitalProducts.Any(p => !String.IsNullOrEmpty(p.Name)));
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
				string category = _localSchedule.DigitalProducts[advBandedGridViewDigitalProducts.GetFocusedDataSourceRowIndex()].Category;
				string subCategory = _localSchedule.DigitalProducts[advBandedGridViewDigitalProducts.GetFocusedDataSourceRowIndex()].SubCategory;
				repositoryItemComboBoxDigitalProductsNames.Items.Clear();
				repositoryItemComboBoxDigitalProductsNames.Items.AddRange(ListManager.Instance.ProductSources.Where(x => x.Category.Name.Equals(category) && (x.SubCategory.Equals(subCategory) || string.IsNullOrEmpty(subCategory))).Select(x => x.Name).Distinct().ToArray());
			}
			else if (advBandedGridViewDigitalProducts.FocusedColumn == gridColumnDigitalProductsType)
			{
				string category = _localSchedule.DigitalProducts[advBandedGridViewDigitalProducts.GetFocusedDataSourceRowIndex()].Category;
				string[] subCategories = ListManager.Instance.ProductSources.Where(x => x.Category.Name.Equals(category) && !string.IsNullOrEmpty(x.SubCategory)).Select(x => x.SubCategory).Distinct().ToArray();
				if (subCategories.Length > 0)
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