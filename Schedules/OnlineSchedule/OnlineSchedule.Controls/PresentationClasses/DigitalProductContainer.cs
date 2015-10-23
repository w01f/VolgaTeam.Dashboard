using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using Asa.CommonGUI.Preview;
using Asa.CommonGUI.ToolForms;
using Asa.Core.Common;
using Asa.Core.OnlineSchedule;
using Asa.OnlineSchedule.Controls.InteropClasses;
using Asa.OnlineSchedule.Controls.ToolForms;

namespace Asa.OnlineSchedule.Controls.PresentationClasses
{
	[ToolboxItem(false)]
	public abstract partial class DigitalProductContainer : UserControl
	{
		protected Form _formContainer;
		protected List<DigitalProductControl> _tabPages = new List<DigitalProductControl>();

		protected DigitalProductContainer(Form formContainer)
		{
			InitializeComponent();
			_formContainer = formContainer;
			Dock = DockStyle.Fill;
			AllowApplyValues = false;
			if ((base.CreateGraphics()).DpiX > 96)
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
			spinEditDuration.Enter += Utilities.Instance.Editor_Enter;
			spinEditDuration.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditDuration.MouseUp += Utilities.Instance.Editor_MouseUp;
			AssignCloseActiveEditorsonOutSideClick(pnHeader);
		}

		#region CommandButtons
		public abstract HelpManager HelpManager { get; }
		#endregion
		protected abstract string SlideName { get; }
		public IDigitalSchedule LocalSchedule { get; protected set; }
		public bool SettingsNotSaved { get; set; }
		public bool AllowApplyValues { get; set; }
		public abstract Theme SelectedTheme { get; }

		protected void AssignCloseActiveEditorsonOutSideClick(Control control)
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
				control.Click += CloseActiveEditorsonOutSideClick;
				foreach (Control childControl in control.Controls)
					AssignCloseActiveEditorsonOutSideClick(childControl);
			}
		}

		protected void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
		{
			labelControlCategory.Focus();
		}

		public void LoadProduct(DigitalProductControl productControl)
		{
			if (productControl == null) return;
			var tempSettingsNotSaved = SettingsNotSaved;
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
			SettingsNotSaved = tempSettingsNotSaved;
			AllowApplyValues = temp;
		}

		protected void SaveProduct(DigitalProductControl productControl)
		{
			if (productControl == null) return;

			productControl.Product.ShowFlightDates = checkEditShowFlightDates.Checked;

			SaveDurationCheckboxValues(productControl);
			productControl.Product.DurationValue = spinEditDuration.EditValue != null ? (int?)spinEditDuration.Value : null;
		}

		protected virtual bool SaveSchedule(string scheduleName = "")
		{
			SaveProduct(xtraTabControlProducts.SelectedTabPage as DigitalProductControl);
			foreach (var tabPage in _tabPages)
				tabPage.SaveValues();
			xtraTabControlProducts.TabPages.OfType<DigitalSummaryControl>().First().Save();
			return true;
		}

		protected void ScheduleBuilderControl_Load(object sender, EventArgs e)
		{
			AssignCloseActiveEditorsonOutSideClick(pnHeader);
		}

		protected void xtraTabControlProducts_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
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
				SaveProduct(e.PrevPage as DigitalProductControl);
				LoadProduct(e.Page as DigitalProductControl);

				checkEditDuration.Visible = true;
				spinEditDuration.Visible = true;
				checkEditMonths.Visible = true;
				checkEditWeeks.Visible = true;
			}
		}

		private void xtraTabControlProducts_MouseDown(object sender, MouseEventArgs e)
		{
			var c = sender as XtraTabControl;
			var hi = c.CalcHitInfo(new Point(e.X, e.Y));
			if (hi.HitTest != XtraTabHitTest.PageHeader || e.Button != MouseButtons.Right) return;
			var productControl = hi.Page as DigitalProductControl;
			using (var form = new FormCloneProduct())
			{
				if (form.ShowDialog() != DialogResult.Yes || productControl == null) return;
				var selectedPage = xtraTabControlProducts.SelectedTabPage as DigitalProductControl;
				var newPrintProduct = productControl.Product.Clone();
				xtraTabControlProducts.SelectedPageChanged -= xtraTabControlProducts_SelectedPageChanged;
				xtraTabControlProducts.TabPages.Clear();
				var newPublicationTab = new DigitalProductControl(this);
				newPublicationTab.Product = newPrintProduct;
				newPublicationTab.Text = newPrintProduct.Name.Replace("&", "&&");
				newPublicationTab.LoadValues();
				_tabPages.Add(newPublicationTab);
				_tabPages.Sort((x, y) => x.Product.Index.CompareTo(y.Product.Index));
				xtraTabControlProducts.TabPages.AddRange(_tabPages.ToArray());
				xtraTabControlProducts.SelectedPageChanged += xtraTabControlProducts_SelectedPageChanged;
				xtraTabControlProducts.SelectedTabPage = selectedPage;
				SettingsNotSaved = true;
			}
		}

		private void ckDuration_CheckedChanged(object sender, EventArgs e)
		{
			spinEditDuration.Enabled = checkEditDuration.Checked;
			checkEditMonths.Enabled = checkEditDuration.Checked;
			checkEditWeeks.Enabled = checkEditDuration.Checked;
			if (!AllowApplyValues) return;
			SaveDurationCheckboxValues(xtraTabControlProducts.SelectedTabPage as DigitalProductControl);
		}

		private void SaveDurationCheckboxValues(DigitalProductControl productControl)
		{
			productControl.Product.ShowDuration = checkEditDuration.Checked;
			if (checkEditMonths.Checked)
				productControl.Product.DurationType = "Months";
			else if (checkEditWeeks.Checked)
				productControl.Product.DurationType = "Weeks";
			SettingsNotSaved = true;
		}

		private void checkEditMonths_CheckedChanged(object sender, EventArgs e)
		{
			var productControl = xtraTabControlProducts.SelectedTabPage as DigitalProductControl;
			if (productControl == null) return;
			checkEditWeeks.CheckedChanged -= checkEditWeeks_CheckedChanged;
			checkEditWeeks.Checked = !checkEditMonths.Checked;
			if (checkEditMonths.Checked)
				spinEditDuration.EditValue = productControl.Product.MonthDuraton;
			else if (checkEditWeeks.Checked)
				spinEditDuration.EditValue = productControl.Product.WeeksDuration;
			checkEditWeeks.CheckedChanged += checkEditWeeks_CheckedChanged;

			if (!AllowApplyValues) return;
			SaveDurationCheckboxValues(xtraTabControlProducts.SelectedTabPage as DigitalProductControl);
		}

		private void checkEditWeeks_CheckedChanged(object sender, EventArgs e)
		{
			var productControl = xtraTabControlProducts.SelectedTabPage as DigitalProductControl;
			if (productControl == null) return;
			checkEditMonths.CheckedChanged -= checkEditMonths_CheckedChanged;
			checkEditMonths.Checked = !checkEditWeeks.Checked;
			if (checkEditMonths.Checked)
				spinEditDuration.EditValue = productControl.Product.MonthDuraton;
			else if (checkEditWeeks.Checked)
				spinEditDuration.EditValue = productControl.Product.WeeksDuration;
			checkEditMonths.CheckedChanged += checkEditMonths_CheckedChanged;

			if (!AllowApplyValues) return;
			SaveDurationCheckboxValues(xtraTabControlProducts.SelectedTabPage as DigitalProductControl);
		}

		private void checkEditProductTogle_CheckedChanged(object sender, EventArgs e)
		{
			if (!AllowApplyValues) return;
			SettingsNotSaved = true;
		}

		private void DigitalProductContainer_Resize(object sender, EventArgs e)
		{
			checkEditShowFlightDates.Left = (Width - checkEditShowFlightDates.Width) / 2;
		}

		protected virtual IEnumerable<UserActivity> TrackOutput(IEnumerable<DigitalProductControl> tabsForOutput)
		{
			return tabsForOutput.Select(outputControl => new DigitalProductOutputActivity(
					SlideName,
					LocalSchedule.BusinessName,
					outputControl.SlideName,
					outputControl.Product.OutputData.Investments))
				.ToList();
		}

		public void PowerPoint_Click(object sender, EventArgs e)
		{
			SaveSchedule();

			var selectedTabPages = new List<IDigitalOutputControl>();
			if (_tabPages.Count > 1)
			{
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Digital Products";
					var currentProduct = xtraTabControlProducts.SelectedTabPage as IDigitalOutputControl; foreach (var tabPage in xtraTabControlProducts.TabPages.OfType<IDigitalOutputControl>())
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
							OfType<IDigitalOutputControl>());
				}
			}
			else
				selectedTabPages.AddRange(xtraTabControlProducts.TabPages.OfType<IDigitalOutputControl>());
			if (!selectedTabPages.Any()) return;
			TrackOutput(selectedTabPages.OfType<DigitalProductControl>());
			OutputSlides(selectedTabPages);
		}

		public abstract void OutputSlides(IEnumerable<IDigitalOutputControl> tabsForOutput);

		public void Email_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			var previewGroups = new List<PreviewGroup>();
			var selectedTabPages = new List<IDigitalOutputControl>();
			if (_tabPages.Count > 1)
			{
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Digital Products";
					var currentProduct = xtraTabControlProducts.SelectedTabPage as IDigitalOutputControl;
					foreach (var tabPage in xtraTabControlProducts.TabPages.OfType<IDigitalOutputControl>())
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
							OfType<IDigitalOutputControl>());
				}
			}
			else
				selectedTabPages.AddRange(xtraTabControlProducts.TabPages.OfType<IDigitalOutputControl>());
			if (!selectedTabPages.Any()) return;
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Presentation for Email...");
			FormProgress.ShowProgress();
			foreach (var productControl in selectedTabPages)
			{
				var previewGroup = productControl.GetPreviewGroup();
				if (productControl is DigitalProductControl)
					OnlineSchedulePowerPointHelper.Instance.PrepareScheduleEmail(previewGroup.PresentationSourcePath, new[] { ((DigitalProductControl)productControl).Product }, SelectedTheme);
				else if (productControl is DigitalSummaryControl)
				{
					var summaryControl = productControl as DigitalSummaryControl;
					summaryControl.PopulateReplacementsList();
					OnlineSchedulePowerPointHelper.Instance.PrepareDigitalSummaryEmail(previewGroup.PresentationSourcePath, summaryControl);
				}
				previewGroups.Add(previewGroup);
			}
			FormProgress.CloseProgress();

			if (!previewGroups.Any() || !previewGroups.All(pg => File.Exists(pg.PresentationSourcePath))) return;
			using (var formEmail = new FormEmail(OnlineSchedulePowerPointHelper.Instance, HelpManager))
			{
				formEmail.Text = "Email this Online Schedule";
				formEmail.LoadGroups(previewGroups);
				Utilities.Instance.ActivateForm(_formContainer.Handle, true, false);
				RegistryHelper.MainFormHandle = formEmail.Handle;
				RegistryHelper.MaximizeMainForm = false;
				formEmail.ShowDialog();
				RegistryHelper.MaximizeMainForm = true;
				RegistryHelper.MainFormHandle = _formContainer.Handle;
			}
		}

		public void Preview_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			var previewGroups = new List<PreviewGroup>();
			var selectedTabPages = new List<IDigitalOutputControl>();
			if (_tabPages.Count > 1)
			{
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Digital Products";
					var currentProduct = xtraTabControlProducts.SelectedTabPage as IDigitalOutputControl;
					foreach (var tabPage in xtraTabControlProducts.TabPages.OfType<IDigitalOutputControl>())
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
							OfType<IDigitalOutputControl>());
				}
			}
			else
				selectedTabPages.AddRange(xtraTabControlProducts.TabPages.OfType<IDigitalOutputControl>());
			if (!selectedTabPages.Any()) return;
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			foreach (var productControl in selectedTabPages)
			{
				var previewGroup = productControl.GetPreviewGroup();
				if (productControl is DigitalProductControl)
					OnlineSchedulePowerPointHelper.Instance.PrepareScheduleEmail(previewGroup.PresentationSourcePath, new[] { ((DigitalProductControl)productControl).Product }, SelectedTheme);
				else if (productControl is DigitalSummaryControl)
				{
					var summaryControl = productControl as DigitalSummaryControl;
					summaryControl.PopulateReplacementsList();
					OnlineSchedulePowerPointHelper.Instance.PrepareDigitalSummaryEmail(previewGroup.PresentationSourcePath, summaryControl);
				}
				previewGroups.Add(previewGroup);
			}
			Utilities.Instance.ActivateForm(_formContainer.Handle, true, false);
			FormProgress.CloseProgress();

			if (previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))
				ShowPreview(previewGroups, () => TrackOutput(selectedTabPages.OfType<DigitalProductControl>()));
		}

		public abstract void ShowPreview(IEnumerable<PreviewGroup> previewGroups, Action trackOutput);

		public void Pdf_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			var previewGroups = new List<PreviewGroup>();
			var selectedTabPages = new List<IDigitalOutputControl>();
			if (_tabPages.Count > 1)
			{
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Digital Products";
					var currentProduct = xtraTabControlProducts.SelectedTabPage as IDigitalOutputControl;
					foreach (var tabPage in xtraTabControlProducts.TabPages.OfType<IDigitalOutputControl>())
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
							OfType<IDigitalOutputControl>());
				}
			}
			else
				selectedTabPages.AddRange(xtraTabControlProducts.TabPages.OfType<IDigitalOutputControl>());
			if (!selectedTabPages.Any()) return;
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Presentation for Output...");
			FormProgress.ShowProgress();
			foreach (var productControl in selectedTabPages)
			{
				var previewGroup = productControl.GetPreviewGroup();
				if (productControl is DigitalProductControl)
					OnlineSchedulePowerPointHelper.Instance.PrepareScheduleEmail(previewGroup.PresentationSourcePath, new[] { ((DigitalProductControl)productControl).Product }, SelectedTheme);
				else if (productControl is DigitalSummaryControl)
				{
					var summaryControl = productControl as DigitalSummaryControl;
					summaryControl.PopulateReplacementsList();
					OnlineSchedulePowerPointHelper.Instance.PrepareDigitalSummaryEmail(previewGroup.PresentationSourcePath, summaryControl);
				}
				previewGroups.Add(previewGroup);
			}
			FormProgress.CloseProgress();
			if (previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))
				ShowPdf(previewGroups, () => TrackOutput(selectedTabPages.OfType<DigitalProductControl>()));
		}

		public abstract void ShowPdf(IEnumerable<PreviewGroup> previewGroups, Action trackOutput);

		#region Picture Box Clicks Habdlers
		/// <summary>
		/// Buttonize the PictureBox 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top += 1;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top -= 1;
		}
		#endregion
	}

	public interface IDigitalOutputControl
	{
		string SlideName { get; }
		PreviewGroup GetPreviewGroup();
		void Output();
	}

}