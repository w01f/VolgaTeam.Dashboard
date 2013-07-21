using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.BusinessClasses;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses.ToolForms;
using NewBizWiz.OnlineSchedule.Controls.Properties;
using ListManager = NewBizWiz.Core.OnlineSchedule.ListManager;
using SettingsManager = NewBizWiz.Core.OnlineSchedule.SettingsManager;

namespace NewBizWiz.OnlineSchedule.Controls.PresentationClasses
{
	[ToolboxItem(false)]
	public partial class ScheduleSettingsControl : UserControl
	{
		private Schedule _localSchedule;

		public ScheduleSettingsControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			SettingsNotSaved = false;
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate()
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			});
			LoadCategories();
		}

		public bool SettingsNotSaved { get; set; }

		public bool AllowToLeaveControl
		{
			get
			{
				bool result = false;
				if (SettingsNotSaved)
				{
					if (Utilities.Instance.ShowWarningQuestion("Schedule settings have changed.\nDo you want to save changes?") == DialogResult.Yes)
					{
						if (SaveSchedule())
							result = true;
					}
				}
				else
					result = true;
				return result;
			}
		}

		private void AssignCloseActiveEditorsOnOutSideClick(Control control)
		{
			if (control != Controller.Instance.HomeBusinessName
			    && control != Controller.Instance.HomeDecisionMaker
			    && control != Controller.Instance.HomePresentationDate
			    && control != Controller.Instance.HomeFlightDatesStart
			    && control != Controller.Instance.HomeFlightDatesEnd)
			{
				control.Click += CloseActiveEditorsonOutSideClick;
				foreach (Control childControl in control.Controls)
					AssignCloseActiveEditorsOnOutSideClick(childControl);
			}
		}

		private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
		{
			Focus();
			advBandedGridViewProducts.CloseEditor();
		}

		private void UpdateProductsCount()
		{
			laProducts.Text = "Web Sales Products: " + _localSchedule.Products.Count.ToString();
		}

		public void LoadCategories()
		{
			Controller.Instance.HomePanel.SuspendLayout();
			Controller.Instance.HomePanel.Controls.Add(Controller.Instance.HomeExitBar);
			Controller.Instance.HomePanel.Controls.Add(Controller.Instance.HomeHelpBar);
			Controller.Instance.HomePanel.Controls.Add(Controller.Instance.HomeSaveBar);
			int leftPosition = 287;
			if (ListManager.Instance.Categories.Count > 5)
			{
				var categoryListButton = new ButtonItem();
				categoryListButton.Image = File.Exists(SettingsManager.Instance.InventoryImagePath) ? new Bitmap(SettingsManager.Instance.InventoryImagePath) : Resources.Inventory;
				categoryListButton.ImagePaddingHorizontal = 8;
				categoryListButton.SubItemsExpandWidth = 14;
				categoryListButton.AutoExpandOnClick = true;
				Controller.Instance.Supertip.SetSuperTooltip(categoryListButton, new SuperTooltipInfo("Web Sales Inventory", "", "Select the Web Sales Products you want to sell", null, null, eTooltipColor.Gray));

				foreach (Category category in ListManager.Instance.Categories)
				{
					var categoryButton = new ButtonItem();
					categoryButton.Image = category.Logo;
					categoryButton.Text = "<b>" + category.TooltipTitle + "</b><p>" + category.TooltipValue + "</p>";
					categoryButton.ImagePaddingHorizontal = 8;
					categoryButton.SubItemsExpandWidth = 14;
					categoryButton.Tag = category;
					categoryButton.Click += buttonItemProductAddProduct_Click;
					categoryListButton.SubItems.Add(categoryButton);
				}

				var categoryListRibbonBar = new RibbonBar();
				categoryListRibbonBar.AutoOverflowEnabled = true;
				categoryListRibbonBar.Dock = DockStyle.Left;
				categoryListRibbonBar.Items.AddRange(new BaseItem[] { categoryListButton });
				categoryListRibbonBar.Location = new Point(leftPosition, 0);
				categoryListRibbonBar.Name = leftPosition.ToString();
				categoryListRibbonBar.Size = new Size(79, 135);
				categoryListRibbonBar.Style = eDotNetBarStyle.Office2007;
				categoryListRibbonBar.Text = "What do you want to sell?";

				Controller.Instance.HomePanel.Controls.Add(categoryListRibbonBar);
				leftPosition += 70;
			}
			else
			{
				foreach (Category category in ListManager.Instance.Categories)
				{
					var categoryButton = new ButtonItem();
					categoryButton.Image = category.Logo;
					categoryButton.ImagePaddingHorizontal = 8;
					categoryButton.SubItemsExpandWidth = 14;
					Controller.Instance.Supertip.SetSuperTooltip(categoryButton, new SuperTooltipInfo(category.TooltipTitle, "", category.TooltipValue, null, null, eTooltipColor.Gray));
					categoryButton.Tag = category;
					categoryButton.Click += buttonItemProductAddProduct_Click;

					var categoryRibbonBar = new RibbonBar();
					categoryRibbonBar.AutoOverflowEnabled = true;
					categoryRibbonBar.Dock = DockStyle.Left;
					categoryRibbonBar.Items.AddRange(new BaseItem[] { categoryButton });
					categoryRibbonBar.Location = new Point(leftPosition, 0);
					categoryRibbonBar.Name = leftPosition.ToString();
					categoryRibbonBar.Size = new Size(79, 135);
					categoryRibbonBar.Style = eDotNetBarStyle.Office2007;
					categoryRibbonBar.Text = category.Name;

					Controller.Instance.HomePanel.Controls.Add(categoryRibbonBar);
					leftPosition += 70;
				}
			}
			Controller.Instance.HomePanel.Controls.Add(Controller.Instance.HomeFlightDatesBar);
			Controller.Instance.HomePanel.Controls.Add(Controller.Instance.HomeAdvertiserProfileBar);
			Controller.Instance.HomePanel.ResumeLayout(false);
		}

		public void LoadSchedule(bool quickLoad)
		{
			_localSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			gridControlProducts.DataSource = new BindingList<DigitalProduct>(_localSchedule.Products);
			if (!quickLoad)
			{
				laScheduleName.Text = _localSchedule.Name;

				Controller.Instance.HomeBusinessName.Properties.Items.Clear();
				Controller.Instance.HomeBusinessName.Properties.Items.AddRange(Core.Common.ListManager.Instance.Advertisers.ToArray());
				Controller.Instance.HomeDecisionMaker.Properties.Items.Clear();
				Controller.Instance.HomeDecisionMaker.Properties.Items.AddRange(Core.Common.ListManager.Instance.DecisionMakers.ToArray());

				Controller.Instance.HomeBusinessName.EditValue = _localSchedule.BusinessName;
				Controller.Instance.HomeDecisionMaker.EditValue = _localSchedule.DecisionMaker;

				Controller.Instance.HomePresentationDate.EditValue = _localSchedule.PresentationDateObject;
				Controller.Instance.HomeFlightDatesStart.EditValue = _localSchedule.FlightDateStartObject;
				Controller.Instance.HomeFlightDatesEnd.EditValue = _localSchedule.FlightDateEndObject;

				UpdateProductsCount();

				Controller.Instance.UpdateSimpleOutputTabPageState(_localSchedule.Products.Count > 0);
				Controller.Instance.UpdateSummaryOutputTabPageState(_localSchedule.Products.Count > 1);
			}
			SettingsNotSaved = false;
		}

		private bool SaveSchedule(string scheduleName = "")
		{
			if (!string.IsNullOrEmpty(scheduleName))
				_localSchedule.Name = scheduleName;
			advBandedGridViewProducts.CloseEditor();
			if (Controller.Instance.HomeBusinessName.EditValue != null)
				_localSchedule.BusinessName = Controller.Instance.HomeBusinessName.EditValue.ToString();
			else
			{
				Utilities.Instance.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Business Name before you proceed.");
				return false;
			}
			if (Controller.Instance.HomeDecisionMaker.EditValue != null)
				_localSchedule.DecisionMaker = Controller.Instance.HomeDecisionMaker.EditValue.ToString();
			else
			{
				Utilities.Instance.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Owner/Decision-maker before you proceed.");
				return false;
			}

			if (Controller.Instance.HomePresentationDate.EditValue != null)
				_localSchedule.PresentationDate = Controller.Instance.HomePresentationDate.DateTime;
			else
			{
				Utilities.Instance.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Presentation Date before you proceed.");
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
				Utilities.Instance.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Flight Start Date before you proceed.");
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
				Utilities.Instance.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Flight End Date before you proceed.");
				return false;
			}

			foreach (DigitalProduct publication in _localSchedule.Products)
				if (string.IsNullOrEmpty(publication.Name))
				{
					Utilities.Instance.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Web Product in each line before you proceed.");
					return false;
				}

			Controller.Instance.HomeBusinessName.Properties.Items.Clear();
			Controller.Instance.HomeBusinessName.Properties.Items.AddRange(Core.Common.ListManager.Instance.Advertisers.ToArray());
			Controller.Instance.HomeDecisionMaker.Properties.Items.Clear();
			Controller.Instance.HomeDecisionMaker.Properties.Items.AddRange(Core.Common.ListManager.Instance.DecisionMakers.ToArray());

			_localSchedule.ProductPackage.UpdateWebProducts();
			Controller.Instance.SaveSchedule(_localSchedule, false, this);

			laScheduleName.Text = _localSchedule.Name;
			SettingsNotSaved = false;
			return true;
		}

		private void RefreshDataAfterAddProduct()
		{
			((BindingList<DigitalProduct>)gridControlProducts.DataSource).ResetBindings();
			advBandedGridViewProducts.FocusedRowHandle = advBandedGridViewProducts.RowCount - 1;
			UpdateProductsCount();
			Controller.Instance.UpdateSimpleOutputTabPageState(_localSchedule.Products.Count > 0);
			Controller.Instance.UpdateSummaryOutputTabPageState(_localSchedule.Products.Count > 1);
			SettingsNotSaved = true;
		}

		private void ScheduleSettingsControl_Load(object sender, EventArgs e)
		{
			repositoryItemComboBoxProductType.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemComboBoxProductType.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemComboBoxProductType.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemComboBoxProductNames.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemComboBoxProductNames.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemComboBoxProductNames.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemSpinEditSize.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemSpinEditSize.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemSpinEditSize.MouseUp += Utilities.Instance.Editor_MouseUp;

			AssignCloseActiveEditorsOnOutSideClick(Controller.Instance.Ribbon);
			AssignCloseActiveEditorsOnOutSideClick(pnHeader);
		}

		public void buttonItemProductAddProduct_Click(object sender, EventArgs e)
		{
			var category = (sender as ButtonItem).Tag as Category;
			_localSchedule.AddProduct(category.Name);
			RefreshDataAfterAddProduct();
		}

		public void buttonItemScheduleSettingsSave_Click(object sender, EventArgs e)
		{
			if (SaveSchedule())
				Utilities.Instance.ShowInformation("Schedule Saved");
		}

		public void buttonItemScheduleHelp_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("Home");
		}

		public void buttonItemScheduleSettingsSaveAs_Click(object sender, EventArgs e)
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

		private void repositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			switch (e.Button.Index)
			{
				case 0:
					_localSchedule.UpProduct(advBandedGridViewProducts.GetDataSourceRowIndex(advBandedGridViewProducts.FocusedRowHandle));
					if (advBandedGridViewProducts.FocusedRowHandle > 0)
						advBandedGridViewProducts.FocusedRowHandle--;
					break;
				case 1:
					_localSchedule.DownProduct(advBandedGridViewProducts.GetDataSourceRowIndex(advBandedGridViewProducts.FocusedRowHandle));
					if (advBandedGridViewProducts.FocusedRowHandle < advBandedGridViewProducts.RowCount - 1)
						advBandedGridViewProducts.FocusedRowHandle++;
					break;
			}
			SettingsNotSaved = true;
		}

		private void repositoryItemButtonEditDelete_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Are you sure you want to delete this line?") == DialogResult.Yes)
			{
				advBandedGridViewProducts.DeleteSelectedRows();
				_localSchedule.RebuildProductIndexes();
				UpdateProductsCount();
				RefreshDataAfterAddProduct();
				SettingsNotSaved = true;
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
			if (Controller.Instance.HomeFlightDatesStart.DateTime != null && Controller.Instance.HomeFlightDatesEnd.DateTime != null)
			{
				TimeSpan datesRange = Controller.Instance.HomeFlightDatesEnd.DateTime - Controller.Instance.HomeFlightDatesStart.DateTime;
				int weeksCount = datesRange.Days / 7 + 1;
				Controller.Instance.HomeWeeks.Text = weeksCount.ToString() + (weeksCount > 1 ? " Weeks" : " Week");
				Controller.Instance.HomeWeeks.Visible = true;
			}
		}

		public void SchedulePropertyEditValueChanged(object sender, EventArgs e)
		{
			SettingsNotSaved = true;
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

		private void gridViewProducts_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			if (e.Column == gridColumnName)
			{
				if (e.Value != null)
				{
					string value = e.Value.ToString();
					ProductSource productSource = ListManager.Instance.ProductSources.Where(x => x.Name.Equals(value)).FirstOrDefault();
					if (productSource != null)
					{
						_localSchedule.Products[advBandedGridViewProducts.GetFocusedDataSourceRowIndex()].ApplyDefaultValues();
						advBandedGridViewProducts.RefreshData();
					}
				}
			}
			SchedulePropertyEditValueChanged(null, null);
		}

		private void repositoryItemComboBoxProductName_Closed(object sender, ClosedEventArgs e)
		{
			advBandedGridViewProducts.CloseEditor();
		}

		private void gridViewProducts_ShowingEditor(object sender, CancelEventArgs e)
		{
			e.Cancel = false;
			if (advBandedGridViewProducts.FocusedColumn == gridColumnName)
			{
				string category = _localSchedule.Products[advBandedGridViewProducts.GetFocusedDataSourceRowIndex()].Category;
				string subCategory = _localSchedule.Products[advBandedGridViewProducts.GetFocusedDataSourceRowIndex()].SubCategory;
				repositoryItemComboBoxProductNames.Items.Clear();
				repositoryItemComboBoxProductNames.Items.AddRange(ListManager.Instance.ProductSources.Where(x => x.Category.Name.Equals(category) && (x.SubCategory.Equals(subCategory) || string.IsNullOrEmpty(subCategory))).Select(x => x.Name).Distinct().ToArray());
			}
			else if (advBandedGridViewProducts.FocusedColumn == gridColumnType)
			{
				string category = _localSchedule.Products[advBandedGridViewProducts.GetFocusedDataSourceRowIndex()].Category;
				string subCategory = _localSchedule.Products[advBandedGridViewProducts.GetFocusedDataSourceRowIndex()].SubCategory;
				string[] subCategories = ListManager.Instance.ProductSources.Where(x => x.Category.Name.Equals(category) && !string.IsNullOrEmpty(x.SubCategory)).Select(x => x.SubCategory).Distinct().ToArray();
				if (subCategories.Length > 0)
				{
					repositoryItemComboBoxProductType.Items.Clear();
					repositoryItemComboBoxProductType.Items.AddRange(subCategories);
				}
				else
					e.Cancel = true;
			}
		}

		private void repositoryItemComboBoxProductType_CloseUp(object sender, CloseUpEventArgs e)
		{
			if (e.CloseMode == PopupCloseMode.Normal)
			{
				e.AcceptValue = false;
				advBandedGridViewProducts.SetFocusedRowCellValue(gridColumnSubCategory, e.Value);
				advBandedGridViewProducts.CloseEditor();
			}
		}
	}
}