using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTab;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.CommonGUI.Common;
using NewBizWiz.CommonGUI.ImageGallery;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using ListManager = NewBizWiz.Core.OnlineSchedule.ListManager;
using Schedule = NewBizWiz.Core.AdSchedule.Schedule;
using ScheduleManager = NewBizWiz.Core.AdSchedule.ScheduleManager;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.InputClasses
{
	[ToolboxItem(false)]
	public partial class ScheduleSettingsControl : UserControl
	{
		private bool _allowToSave;
		private Schedule _localSchedule;
		private GridDragDropHelper _dragDropHelper;

		public ScheduleSettingsControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			SettingsNotSaved = false;
			LoadDigitalCategories();
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.BeginInvoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			});
		}

		public bool SettingsNotSaved { get; set; }
		public bool AllowToLeaveControl
		{
			get { return !SettingsNotSaved || SaveSchedule(); }
		}

		#region Methods
		private void LoadDigitalCategories()
		{
			foreach (var category in ListManager.Instance.Categories)
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
			gridViewPrintProducts.CloseEditor();
			digitalProductListControl.CloseEditors();
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
			digitalProductListControl.UpdateData(_localSchedule,
				() =>
				{
					UpdateProductsCount();
					Controller.Instance.UpdateDigitalProductTab(_localSchedule.DigitalProducts.Any(p => !String.IsNullOrEmpty(p.Name)));
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
			if (_dragDropHelper == null)
			{
				_dragDropHelper = new GridDragDropHelper(gridViewPrintProducts, true);
				_dragDropHelper.AfterDrop += PrintProductsAfterDrop;
			}
			if (!quickLoad)
			{
				LoadView();

				#region Print Products
				repositoryItemComboBox.Items.Clear();
				repositoryItemComboBox.Items.AddRange(Core.AdSchedule.ListManager.Instance.PublicationSources.Where(x => !x.Name.Equals("Default")).Select(x => x.Name).Distinct().ToArray());
				Controller.Instance.HomeClientType.Properties.Items.Clear();
				Controller.Instance.HomeClientType.Properties.Items.AddRange(Core.AdSchedule.ListManager.Instance.ClientTypes.ToArray());

				Controller.Instance.HomeBusinessName.EditValue = _localSchedule.BusinessName;
				Controller.Instance.HomeDecisionMaker.EditValue = _localSchedule.DecisionMaker;

				if (!string.IsNullOrEmpty(_localSchedule.ClientType))
					Controller.Instance.HomeClientType.SelectedIndex = Controller.Instance.HomeClientType.Properties.Items.IndexOf(_localSchedule.ClientType);

				Controller.Instance.HomeAccountNumberCheck.Checked = !string.IsNullOrEmpty(_localSchedule.AccountNumber);
				Controller.Instance.HomeAccountNumberText.EditValue = _localSchedule.AccountNumber;

				Controller.Instance.HomePresentationDate.EditValue = _localSchedule.PresentationDate;
				Controller.Instance.HomeFlightDatesStart.EditValue = _localSchedule.FlightDateStart;
				Controller.Instance.HomeFlightDatesEnd.EditValue = _localSchedule.FlightDateEnd;
				Controller.Instance.UpdatePrintProductTab(_localSchedule.PrintProducts.Any(p => !String.IsNullOrEmpty(p.Name)));
				#endregion

				CalcWeeksOnFlightDatesChange(null, EventArgs.Empty);
				Controller.Instance.UpdateOutputTabs(_localSchedule.PrintProducts.Select(x => x.Inserts.Count).Sum() > 0);
				xtraTabControlProducts_SelectedPageChanged(xtraTabControlProducts, new TabPageChangedEventArgs(null, xtraTabControlProducts.SelectedTabPage));
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

			digitalProductListControl.LoadView();
		}

		private void SaveView()
		{
			_localSchedule.ViewSettings.HomeViewSettings.ShowPrintCode = buttonXPrintProductCode.Checked;
			_localSchedule.ViewSettings.HomeViewSettings.ShowPrintLogo = buttonXPrintProductLogo.Checked;
			_localSchedule.ViewSettings.HomeViewSettings.ShowPrintDelivery = buttonXPrintProductDelivery.Checked;
			_localSchedule.ViewSettings.HomeViewSettings.ShowPrintReadership = buttonXPrintProductReadership.Checked;
			digitalProductListControl.SaveView();
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
			gridViewPrintProducts.CloseEditor();
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

			if (_localSchedule.PrintProducts.Any(publication => String.IsNullOrEmpty(publication.Name)))
			{
				Utilities.Instance.ShowWarning("You must Select Name for all Publications before save");
				return false;
			}
			if (_localSchedule.DigitalProducts.Any(publication => String.IsNullOrEmpty(publication.Name)))
			{
				Utilities.Instance.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Web Product in each line before you proceed.");
				return false;
			}

			SaveView();

			repositoryItemComboBox.Items.Clear();
			repositoryItemComboBox.Items.AddRange(Core.AdSchedule.ListManager.Instance.PublicationSources.Select(x => x.Name).Distinct().ToArray());

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
			repositoryItemComboBox.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemComboBox.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemComboBox.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemSpinEdit.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemSpinEdit.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemSpinEdit.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemTextEdit.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemTextEdit.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemTextEdit.MouseUp += Utilities.Instance.Editor_MouseUp;
			AssignCloseActiveEditorsonOutSideClick(Controller.Instance.Ribbon);
		}

		private void xtraTabControlProducts_SelectedPageChanging(object sender, TabPageChangingEventArgs e)
		{
			e.Cancel = !AllowToLeaveControl;
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
			using (var form = new FormNewSchedule(ScheduleManager.GetShortScheduleList().Select(s => s.ShortFileName)))
			{
				form.Text = "Save Schedule";
				form.laLogo.Text = "Please set a new name for your Schedule:";
				if (form.ShowDialog() == DialogResult.OK)
				{
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
		}

		public void FlightDateStartEditValueChanged(object sender, EventArgs e)
		{
			if (Controller.Instance.HomeFlightDatesStart.EditValue == null) return;
			var dateStart = Controller.Instance.HomeFlightDatesStart.DateTime;
			while (dateStart.DayOfWeek != DayOfWeek.Saturday)
				dateStart = dateStart.AddDays(1);
			Controller.Instance.HomeFlightDatesEnd.EditValue = dateStart;
		}

		public void CalcWeeksOnFlightDatesChange(object sender, EventArgs e)
		{
			Controller.Instance.HomeWeeks.Text = "";
			Controller.Instance.HomeWeeks.Visible = false;
			if (Controller.Instance.HomeFlightDatesStart.EditValue == null || Controller.Instance.HomeFlightDatesEnd.EditValue == null)
			{
				Controller.Instance.UpdateCalendar2Tab(false);
				return;
			}
			var datesRange = Controller.Instance.HomeFlightDatesEnd.DateTime - Controller.Instance.HomeFlightDatesStart.DateTime;
			var weeksCount = datesRange.Days / 7 + 1;
			Controller.Instance.UpdateCalendar2Tab(weeksCount > 0);
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
			if (!_allowToSave) return;
			SettingsNotSaved = true;
		}

		public void dateEditFlightDatesStart_CloseUp(object sender, CloseUpEventArgs e)
		{
			if (e.Value == null) return;
			DateTime temp;
			if (!DateTime.TryParse(e.Value.ToString(), out temp)) return;
			while (temp.DayOfWeek != DayOfWeek.Sunday)
				temp = temp.AddDays(-1);
			e.Value = temp;
		}

		public void dateEditFlightDatesEnd_CloseUp(object sender, CloseUpEventArgs e)
		{
			if (e.Value == null) return;
			DateTime temp;
			if (!DateTime.TryParse(e.Value.ToString(), out temp)) return;
			while (temp.DayOfWeek != DayOfWeek.Saturday)
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

		public void buttonItemPrintScheduleettingsHelp_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink(xtraTabControlProducts.SelectedTabPage == xtraTabPageDigitalProducts ? "homedigital" : "home");
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
			if (gridViewPrintProducts.FocusedRowHandle == GridControl.InvalidRowHandle) return;
			var newRowHandle = gridViewPrintProducts.FocusedRowHandle + 1;
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

		private void repositoryItemButtonEditDelete_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Are you SURE you want to DELETE\nthis publication from your schedule?") != DialogResult.Yes) return;
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

		private void gridViewPublications_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			if (e.Column == gridColumnName)
			{
				if (e.Value != null)
				{
					var value = e.Value.ToString();
					var printProductSource = Core.AdSchedule.ListManager.Instance.PublicationSources.FirstOrDefault(x => x.Name.Equals(value));
					if (printProductSource != null)
					{
						BusinessWrapper.Instance.ActivityManager.AddActivity(new PropertyEditActivity("Publication Name", printProductSource.Name, advertiser: Controller.Instance.HomeBusinessName.EditValue as String));
						_localSchedule.PrintProducts[gridViewPrintProducts.GetFocusedDataSourceRowIndex()].ApplyDefaultValues();
						gridViewPrintProducts.RefreshData();
					}
				}
			}
			Controller.Instance.UpdatePrintProductTab(_localSchedule.PrintProducts.Any(p => !String.IsNullOrEmpty(p.Name)));
			SchedulePropertyEditValueChanged(null, null);
		}

		private void gridViewPrintProducts_RowCellClick(object sender, RowCellClickEventArgs e)
		{
			if (e.Column != gridColumnLogo) return;
			using (var form = new FormImageGallery(Core.AdSchedule.ListManager.Instance.Images))
			{
				if (form.ShowDialog() != DialogResult.OK) return;
				if (form.SelectedImageSource == null) return;
				_localSchedule.PrintProducts[gridViewPrintProducts.GetFocusedDataSourceRowIndex()].BigLogo = new Bitmap(form.SelectedImageSource.BigImage);
				_localSchedule.PrintProducts[gridViewPrintProducts.GetFocusedDataSourceRowIndex()].SmallLogo = new Bitmap(form.SelectedImageSource.SmallImage);
				_localSchedule.PrintProducts[gridViewPrintProducts.GetFocusedDataSourceRowIndex()].TinyLogo = new Bitmap(form.SelectedImageSource.TinyImage);
				gridViewPrintProducts.RefreshData();
				SettingsNotSaved = true;
			}
		}

		private void gridViewPrintProducts_MouseMove(object sender, MouseEventArgs e)
		{
			var hi = ((GridView)sender).CalcHitInfo(new Point(e.X, e.Y));
			if (hi.InRowCell && hi.Column == gridColumnLogo)
			{
				Cursor = Cursors.Hand;
			}
			else
				Cursor = Cursors.Default;
		}

		private void gridViewPrintProducts_MouseLeave(object sender, EventArgs e)
		{
			Cursor = Cursors.Default;
		}

		private void repositoryItemComboBox_Closed(object sender, ClosedEventArgs e)
		{
			gridViewPrintProducts.CloseEditor();
		}

		public void buttonItemSalesStrategyAbbreviation_CheckedChanged(object sender, EventArgs e)
		{
			gridBandAbbreviation.Visible = buttonXPrintProductCode.Checked;
			var tooltip = new SuperToolTip();
			tooltip.Items.Add(new ToolTipItem() { Text = buttonXPrintProductCode.Checked ? "Hide Publication Code" : "Show Publication Code" });
			toolTipControllerButtons.SetSuperTip(buttonXPrintProductCode, tooltip);
			if (_allowToSave)
			{
				SettingsNotSaved = true;
			}
		}

		public void buttonItemSalesStrategyLogo_CheckedChanged(object sender, EventArgs e)
		{
			gridBandLogo.Visible = buttonXPrintProductLogo.Checked;
			var tooltip = new SuperToolTip();
			tooltip.Items.Add(new ToolTipItem() { Text = buttonXPrintProductLogo.Checked ? "Hide Logo" : "Show Logo" });
			toolTipControllerButtons.SetSuperTip(buttonXPrintProductLogo, tooltip);
			if (_allowToSave)
			{
				SettingsNotSaved = true;
			}
		}

		public void buttonItemSalesStrategyReadership_CheckedChanged(object sender, EventArgs e)
		{
			gridColumnDailyReadership.Visible = buttonXPrintProductReadership.Checked;
			gridColumnSundayReadership.Visible = buttonXPrintProductReadership.Checked;
			if (!buttonXPrintProductReadership.Checked && !buttonXPrintProductDelivery.Checked)
				gridColumnName.RowCount = 2;
			else
				gridColumnName.RowCount = 1;
			var tooltip = new SuperToolTip();
			tooltip.Items.Add(new ToolTipItem { Text = buttonXPrintProductReadership.Checked ? "Hide Readership" : "Show Readership" });
			toolTipControllerButtons.SetSuperTip(buttonXPrintProductReadership, tooltip);
			if (_allowToSave)
			{
				SettingsNotSaved = true;
			}
		}

		public void buttonItemSalesStrategyDelivery_CheckedChanged(object sender, EventArgs e)
		{
			gridColumnDailyDelivery.Visible = buttonXPrintProductDelivery.Checked;
			gridColumnSundayDelivery.Visible = buttonXPrintProductDelivery.Checked;
			if (!buttonXPrintProductReadership.Checked && !buttonXPrintProductDelivery.Checked)
				gridColumnName.RowCount = 2;
			else
				gridColumnName.RowCount = 1;
			var tooltip = new SuperToolTip();
			tooltip.Items.Add(new ToolTipItem() { Text = buttonXPrintProductDelivery.Checked ? "Hide Delivery" : "Show Delivery" });
			toolTipControllerButtons.SetSuperTip(buttonXPrintProductDelivery, tooltip);
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

		private void PrintProductsAfterDrop(object sender, DragEventArgs e)
		{
			var grid = (GridControl)sender;
			var view = (GridView)grid.MainView;
			var hitInfo = view.CalcHitInfo(grid.PointToClient(new Point(e.X, e.Y)));
			var downHitInfo = (BandedGridHitInfo)e.Data.GetData(typeof(BandedGridHitInfo));
			var sourceRow = downHitInfo.RowHandle;
			var targetRow = hitInfo.HitTest == GridHitTest.EmptyRow ? view.DataRowCount : hitInfo.RowHandle;
			view.CloseEditor();
			_localSchedule.ChangePublicationPosition(sourceRow, targetRow);
			view.RefreshData();
			SettingsNotSaved = true;
		}

		private void toolTipController_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
		{
			ToolTipControlInfo info = null;
			try
			{
				var view = gridControlPrintProducts.GetViewAt(e.ControlMousePosition) as GridView;
				if (view == null) return;
				var hi = view.CalcHitInfo(e.ControlMousePosition);
				if (!hi.InRowCell) return;
				if (hi.Column == gridColumnLogo)
					info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), "Click to change logo");
				else if (hi.Column == gridColumnDelete)
					info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), "Delete this publication");
			}
			finally
			{
				e.Info = info;
			}
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