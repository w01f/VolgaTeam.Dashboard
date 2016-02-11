using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Business.Online.Interfaces;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.ContentEditors.Controls;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.ToolForms;
using Asa.Online.Controls.InteropClasses;
using Asa.Online.Controls.PresentationClasses.Summary;
using Asa.Online.Controls.ToolForms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;

namespace Asa.Online.Controls.PresentationClasses.Products
{
	[ToolboxItem(false)]
	public abstract partial class DigitalProductContainer<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo> : BasePartitionEditControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo>, IDigitalProductsContainer
		where TPartitionContet : BaseSchedulePartitionContent<TSchedule, TScheduleSettings>, IDigitalProductsContent
		where TSchedule : IDigitalSchedule<TScheduleSettings>
		where TScheduleSettings : IDigitalScheduleSettings
		where TChangeInfo : DigitalScheduleChangeInfo
	{
		protected List<DigitalProductControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo>> _tabPages = new List<DigitalProductControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo>>();

		protected DigitalProductContainer()
		{
			InitializeComponent();
		}

		protected abstract IDigitalSchedule<IDigitalScheduleSettings> Schedule { get; }

		protected IDigitalScheduleSettings ScheduleSettings
		{
			get { return Schedule.Settings; }
		}

		public abstract Form MainForm { get; }
		public IDigitalProductsContent DigitalProductsContent
		{
			get { return EditedContent; }
		}
		public abstract Theme SelectedTheme { get; }
		protected abstract string SlideName { get; }
		public bool AllowApplyValues { get; set; }

		#region BaseContentEditControl Override
		public override void InitControl()
		{
			base.InitControl();
			if ((CreateGraphics()).DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;
				checkEditDuration.Font = font;
				checkEditMonths.Font = font;
				checkEditWeeks.Font = font;
			}
			spinEditDuration.Enter += TextEditorsHelper.Editor_Enter;
			spinEditDuration.MouseDown += TextEditorsHelper.Editor_MouseDown;
			spinEditDuration.MouseUp += TextEditorsHelper.Editor_MouseUp;
			AssignCloseActiveEditorsOnOutsideClick(pnHeader);
		}

		protected override void ApplyChanges()
		{
			if (xtraTabControlProducts.SelectedTabPage is IDigitalProductControl)
				SaveProduct((IDigitalProductControl)xtraTabControlProducts.SelectedTabPage);
			foreach (var tabPage in _tabPages)
				tabPage.SaveValues();
			xtraTabControlProducts.TabPages.OfType<DigitalSummaryControl>().First().Save();
		}
		#endregion

		#region Product management
		public void LoadProduct(DigitalProductControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo> productControl)
		{
			if (productControl == null) return;
			var temp = AllowApplyValues;
			AllowApplyValues = false;

			var product = productControl.Product;

			labelControlCategory.Text = product.Category;
			checkEditShowFlightDates.Checked = product.ShowFlightDates;

			checkEditDuration.Checked = product.ShowDuration;
			switch (product.DurationType)
			{
				case "Months":
					checkEditMonths.Checked = true;
					checkEditWeeks.Checked = false;
					break;
				case "Weeks":
					checkEditWeeks.Checked = true;
					checkEditMonths.Checked = false;
					break;
			}
			if (product.DurationValue.HasValue)
			{
				spinEditDuration.EditValue = product.DurationValue;
			}
			else
			{
				if (checkEditMonths.Checked)
					spinEditDuration.EditValue = product.MonthDuraton;
				else if (checkEditWeeks.Checked)
					spinEditDuration.EditValue = product.WeeksDuration;
			}
			AllowApplyValues = temp;
		}

		protected void SaveProduct(IDigitalProductControl productControl)
		{
			if (productControl == null) return;

			productControl.Product.ShowFlightDates = checkEditShowFlightDates.Checked;

			SaveDurationCheckboxValues(productControl);
			productControl.Product.DurationValue = spinEditDuration.EditValue != null ? (int?)spinEditDuration.Value : null;
		}
		#endregion

		#region Common Methods
		protected void AssignCloseActiveEditorsOnOutsideClick(Control control)
		{
			if (control.GetType() != typeof(TextEdit) &&
				control.GetType() != typeof(MemoEdit) &&
				control.GetType() != typeof(ComboBoxEdit) &&
				control.GetType() != typeof(LookUpEdit) &&
				control.GetType() != typeof(DateEdit) &&
				control.GetType() != typeof(CheckedListBoxControl) &&
				control.GetType() != typeof(SpinEdit) &&
				control.GetType() != typeof(CheckEdit))
			{
				control.Click += CloseActiveEditorsOnOutSideClick;
				foreach (Control childControl in control.Controls)
					AssignCloseActiveEditorsOnOutsideClick(childControl);
			}
		}

		protected void CloseActiveEditorsOnOutSideClick(object sender, EventArgs e)
		{
			labelControlCategory.Focus();
		}
		#endregion

		#region Control Event Handlers
		protected void OnProductsTabControlSelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			if (e.PrevPage is IDigitalProductControl)
				SaveProduct((IDigitalProductControl)e.PrevPage);
			else if (e.PrevPage is DigitalSummaryControl)
				((DigitalSummaryControl)e.PrevPage).Save();
			if (e.Page is DigitalSummaryControl)
			{
				_tabPages.ForEach(tp => tp.SaveValues());
				labelControlCategory.Text = "Digital Product Summary";
				checkEditDuration.Visible = false;
				spinEditDuration.Visible = false;
				checkEditMonths.Visible = false;
				checkEditWeeks.Visible = false;
				((DigitalSummaryControl)e.Page).SetFocus();
			}
			else
			{
				LoadProduct((DigitalProductControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo>)e.Page);
				checkEditDuration.Visible = true;
				spinEditDuration.Visible = true;
				checkEditMonths.Visible = true;
				checkEditWeeks.Visible = true;
			}
		}

		private void OnProductsTabControlMouseDown(object sender, MouseEventArgs e)
		{
			var c = sender as XtraTabControl;
			var hi = c.CalcHitInfo(new Point(e.X, e.Y));
			if (hi.HitTest != XtraTabHitTest.PageHeader || e.Button != MouseButtons.Right) return;
			var productControl = hi.Page as DigitalProductControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo>;
			using (var form = new FormCloneProduct())
			{
				if (form.ShowDialog() != DialogResult.Yes || productControl == null) return;
				var selectedPage = xtraTabControlProducts.SelectedTabPage as DigitalProductControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo>;
				var newPrintProduct = productControl.Product.Clone<DigitalProduct, DigitalProduct>();
				xtraTabControlProducts.SelectedPageChanged -= OnProductsTabControlSelectedPageChanged;
				xtraTabControlProducts.TabPages.Clear();
				var newPublicationTab = new DigitalProductControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo>(this);
				newPublicationTab.Product = newPrintProduct;
				newPublicationTab.Text = newPrintProduct.Name.Replace("&", "&&");
				newPublicationTab.LoadValues();
				_tabPages.Add(newPublicationTab);
				_tabPages.Sort((x, y) => x.Product.Index.CompareTo(y.Product.Index));
				xtraTabControlProducts.TabPages.AddRange(_tabPages.ToArray());
				xtraTabControlProducts.SelectedPageChanged += OnProductsTabControlSelectedPageChanged;
				xtraTabControlProducts.SelectedTabPage = selectedPage;
				SettingsNotSaved = true;
			}
		}

		private void OnDurationCheckedChanged(object sender, EventArgs e)
		{
			spinEditDuration.Enabled = checkEditDuration.Checked;
			checkEditMonths.Enabled = checkEditDuration.Checked;
			checkEditWeeks.Enabled = checkEditDuration.Checked;
			if (!AllowApplyValues) return;
			SaveDurationCheckboxValues(xtraTabControlProducts.SelectedTabPage as DigitalProductControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo>);
		}

		private void SaveDurationCheckboxValues(IDigitalProductControl productControl)
		{
			productControl.Product.ShowDuration = checkEditDuration.Checked;
			if (checkEditMonths.Checked)
				productControl.Product.DurationType = "Months";
			else if (checkEditWeeks.Checked)
				productControl.Product.DurationType = "Weeks";
			SettingsNotSaved = true;
		}

		private void OnMonthsCheckedChanged(object sender, EventArgs e)
		{
			var productControl = xtraTabControlProducts.SelectedTabPage as DigitalProductControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo>;
			if (productControl == null) return;
			checkEditWeeks.CheckedChanged -= OnWeeksCheckedChanged;
			checkEditWeeks.Checked = !checkEditMonths.Checked;
			if (checkEditMonths.Checked)
				spinEditDuration.EditValue = productControl.Product.MonthDuraton;
			else if (checkEditWeeks.Checked)
				spinEditDuration.EditValue = productControl.Product.WeeksDuration;
			checkEditWeeks.CheckedChanged += OnWeeksCheckedChanged;

			if (!AllowApplyValues) return;
			SaveDurationCheckboxValues(xtraTabControlProducts.SelectedTabPage as DigitalProductControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo>);
		}

		private void OnWeeksCheckedChanged(object sender, EventArgs e)
		{
			var productControl = xtraTabControlProducts.SelectedTabPage as DigitalProductControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo>;
			if (productControl == null) return;
			checkEditMonths.CheckedChanged -= OnMonthsCheckedChanged;
			checkEditMonths.Checked = !checkEditWeeks.Checked;
			if (checkEditMonths.Checked)
				spinEditDuration.EditValue = productControl.Product.MonthDuraton;
			else if (checkEditWeeks.Checked)
				spinEditDuration.EditValue = productControl.Product.WeeksDuration;
			checkEditMonths.CheckedChanged += OnMonthsCheckedChanged;

			if (!AllowApplyValues) return;
			SaveDurationCheckboxValues(xtraTabControlProducts.SelectedTabPage as DigitalProductControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo>);
		}

		private void OnProductTogleCheckedChanged(object sender, EventArgs e)
		{
			if (!AllowApplyValues) return;
			SettingsNotSaved = true;
		}

		private void OnDigitalProductContainerResize(object sender, EventArgs e)
		{
			checkEditShowFlightDates.Left = (Width - checkEditShowFlightDates.Width) / 2;
		}
		#endregion

		#region Output Staff
		protected abstract void GeneratePowerPointSlides(IEnumerable<IDigitalSlideControl> tabsForOutput);

		protected abstract void GeneratePdfSlides(IEnumerable<PreviewGroup> previewGroups);

		protected abstract void PreviewSlides(IEnumerable<PreviewGroup> previewGroups);

		protected abstract void EmailSlides(IEnumerable<PreviewGroup> previewGroups);

		public override void OutputPowerPoint()
		{
			var selectedTabPages = new List<IDigitalSlideControl>();
			if (_tabPages.Count > 1)
			{
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Digital Products";
					var currentProduct = xtraTabControlProducts.SelectedTabPage as IDigitalSlideControl; foreach (var tabPage in xtraTabControlProducts.TabPages.OfType<IDigitalSlideControl>())
					{
						var item = new CheckedListBoxItem(tabPage, tabPage.SlideName);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (tabPage == currentProduct)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						selectedTabPages.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<IDigitalSlideControl>());
				}
			}
			else
				selectedTabPages.AddRange(xtraTabControlProducts.TabPages.OfType<IDigitalSlideControl>());
			if (!selectedTabPages.Any()) return;
			GeneratePowerPointSlides(selectedTabPages);
		}

		public override void OutputPdf()
		{
			var previewGroups = new List<PreviewGroup>();
			var selectedTabPages = new List<IDigitalSlideControl>();
			if (_tabPages.Count > 1)
			{
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Digital Products";
					var currentProduct = xtraTabControlProducts.SelectedTabPage as IDigitalSlideControl;
					foreach (var tabPage in xtraTabControlProducts.TabPages.OfType<IDigitalSlideControl>())
					{
						var item = new CheckedListBoxItem(tabPage, tabPage.SlideName);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (tabPage == currentProduct)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						selectedTabPages.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<IDigitalSlideControl>());
				}
			}
			else
				selectedTabPages.AddRange(xtraTabControlProducts.TabPages.OfType<IDigitalSlideControl>());
			if (!selectedTabPages.Any()) return;
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Presentation for Output...");
			FormProgress.ShowProgress();
			foreach (var productControl in selectedTabPages)
			{
				var previewGroup = productControl.GetPreviewGroup();
				if (productControl is DigitalProductControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo>)
					OnlineSchedulePowerPointHelper.Instance.PrepareScheduleEmail(previewGroup.PresentationSourcePath, new[] { ((DigitalProductControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo>)productControl).Product }, SelectedTheme);
				else if (productControl is DigitalSummaryControl)
				{
					var summaryControl = (DigitalSummaryControl)productControl;
					summaryControl.PopulateReplacementsList();
					OnlineSchedulePowerPointHelper.Instance.PrepareDigitalSummaryEmail(previewGroup.PresentationSourcePath, summaryControl);
				}
				previewGroups.Add(previewGroup);
			}
			FormProgress.CloseProgress();
			if (previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))
				GeneratePdfSlides(previewGroups);
		}

		public override void Preview()
		{
			var previewGroups = new List<PreviewGroup>();
			var selectedTabPages = new List<IDigitalSlideControl>();
			if (_tabPages.Count > 1)
			{
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Digital Products";
					var currentProduct = xtraTabControlProducts.SelectedTabPage as IDigitalSlideControl;
					foreach (var tabPage in xtraTabControlProducts.TabPages.OfType<IDigitalSlideControl>())
					{
						var item = new CheckedListBoxItem(tabPage, tabPage.SlideName);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (tabPage == currentProduct)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						selectedTabPages.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<IDigitalSlideControl>());
				}
			}
			else
				selectedTabPages.AddRange(xtraTabControlProducts.TabPages.OfType<IDigitalSlideControl>());
			if (!selectedTabPages.Any()) return;
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			foreach (var productControl in selectedTabPages)
			{
				var previewGroup = productControl.GetPreviewGroup();
				if (productControl is DigitalProductControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo>)
					OnlineSchedulePowerPointHelper.Instance.PrepareScheduleEmail(previewGroup.PresentationSourcePath, new[] { ((DigitalProductControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo>)productControl).Product }, SelectedTheme);
				else if (productControl is DigitalSummaryControl)
				{
					var summaryControl = (DigitalSummaryControl)productControl;
					summaryControl.PopulateReplacementsList();
					OnlineSchedulePowerPointHelper.Instance.PrepareDigitalSummaryEmail(previewGroup.PresentationSourcePath, summaryControl);
				}
				previewGroups.Add(previewGroup);
			}
			Utilities.ActivateForm(MainForm.Handle, MainForm.WindowState == FormWindowState.Maximized, false);
			FormProgress.CloseProgress();

			if (previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))
				PreviewSlides(previewGroups);
		}

		public override void Email()
		{
			var previewGroups = new List<PreviewGroup>();
			var selectedTabPages = new List<IDigitalSlideControl>();
			if (_tabPages.Count > 1)
			{
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Digital Products";
					var currentProduct = xtraTabControlProducts.SelectedTabPage as IDigitalSlideControl;
					foreach (var tabPage in xtraTabControlProducts.TabPages.OfType<IDigitalSlideControl>())
					{
						var item = new CheckedListBoxItem(tabPage, tabPage.SlideName);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (tabPage == currentProduct)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						selectedTabPages.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<IDigitalSlideControl>());
				}
			}
			else
				selectedTabPages.AddRange(xtraTabControlProducts.TabPages.OfType<IDigitalSlideControl>());
			if (!selectedTabPages.Any()) return;
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Presentation for Email...");
			FormProgress.ShowProgress();
			foreach (var productControl in selectedTabPages)
			{
				var previewGroup = productControl.GetPreviewGroup();
				if (productControl is DigitalProductControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo>)
					OnlineSchedulePowerPointHelper.Instance.PrepareScheduleEmail(previewGroup.PresentationSourcePath, new[] { ((DigitalProductControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo>)productControl).Product }, SelectedTheme);
				else if (productControl is DigitalSummaryControl)
				{
					var summaryControl = (DigitalSummaryControl)productControl;
					summaryControl.PopulateReplacementsList();
					OnlineSchedulePowerPointHelper.Instance.PrepareDigitalSummaryEmail(previewGroup.PresentationSourcePath, summaryControl);
				}
				previewGroups.Add(previewGroup);
			}
			FormProgress.CloseProgress();

			if (previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))
				EmailSlides(previewGroups);
		}
		#endregion
	}
}