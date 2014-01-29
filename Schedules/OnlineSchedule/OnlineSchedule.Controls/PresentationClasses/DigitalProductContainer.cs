using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;
using NewBizWiz.OnlineSchedule.Controls.ToolForms;
using SettingsManager = NewBizWiz.Core.Common.SettingsManager;

namespace NewBizWiz.OnlineSchedule.Controls.PresentationClasses
{
	[ToolboxItem(false)]
	public abstract partial class DigitalProductContainer : UserControl
	{
		protected Form _formContainer;
		protected List<DigitalProductControl> _tabPages = new List<DigitalProductControl>();

		public DigitalProductContainer(Form formContainer)
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
		}

		#region CommandButtons
		public abstract HelpManager HelpManager { get; }
		#endregion

		public bool SettingsNotSaved { get; set; }
		public bool AllowApplyValues { get; set; }
		public abstract Theme SelectedTheme { get; }

		protected void AssignCloseActiveEditorsonOutSideClick(Control control)
		{
			if (control.GetType() != typeof(TextEdit) && control.GetType() != typeof(MemoEdit) && control.GetType() != typeof(ComboBoxEdit) && control.GetType() != typeof(LookUpEdit) && control.GetType() != typeof(DateEdit) && control.GetType() != typeof(CheckedListBoxControl) && control.GetType() != typeof(SpinEdit) && control.GetType() != typeof(CheckEdit))
			{
				control.Click += CloseActiveEditorsonOutSideClick;
				foreach (Control childControl in control.Controls)
					AssignCloseActiveEditorsonOutSideClick(childControl);
			}
		}

		protected void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
		{
			labelControlFlightDates.Focus();
		}

		public void LoadProduct(DigitalProductControl productControl)
		{
			if (productControl == null) return;
			var tempSettingsNotSaved = SettingsNotSaved;
			var temp = AllowApplyValues;
			AllowApplyValues = false;

			var product = productControl.Product;
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
			SaveDurationCheckboxValues(productControl);
			productControl.Product.DurationValue = spinEditDuration.EditValue != null ? (int?)spinEditDuration.Value : null;
		}

		protected virtual bool SaveSchedule(string scheduleName = "")
		{
			SaveProduct(xtraTabControlProducts.SelectedTabPage as DigitalProductControl);
			foreach (var tabPage in _tabPages)
				tabPage.SaveValues();
			return true;
		}

		protected void ScheduleBuilderControl_Load(object sender, EventArgs e)
		{
			AssignCloseActiveEditorsonOutSideClick(pnHeader);
		}

		protected void xtraTabControlProducts_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			SaveProduct(e.PrevPage as DigitalProductControl);
			LoadProduct(e.Page as DigitalProductControl);
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
			if (!productControl.Product.DurationValue.HasValue)
			{
				if (checkEditMonths.Checked)
					spinEditDuration.EditValue = productControl.Product.MonthDuraton;
				else if (checkEditWeeks.Checked)
					spinEditDuration.EditValue = productControl.Product.WeeksDuration;
			}
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
			if (!productControl.Product.DurationValue.HasValue)
			{
				if (checkEditMonths.Checked)
					spinEditDuration.EditValue = productControl.Product.MonthDuraton;
				else if (checkEditWeeks.Checked)
					spinEditDuration.EditValue = productControl.Product.WeeksDuration;
			}

			checkEditMonths.CheckedChanged += checkEditMonths_CheckedChanged;

			if (!AllowApplyValues) return;
			SaveDurationCheckboxValues(xtraTabControlProducts.SelectedTabPage as DigitalProductControl);

		}

		public void PowerPoint_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			OutputSlides(_tabPages);
		}

		public abstract void OutputSlides(IEnumerable<DigitalProductControl> tabsForOutput);

		public void Email_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				var tempFileName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				OnlineSchedulePowerPointHelper.Instance.PrepareScheduleEmail(tempFileName, _tabPages.Select(t => t.Product).ToArray(), SelectedTheme);
				Utilities.Instance.ActivateForm(_formContainer.Handle, true, false);
				formProgress.Close();
				if (File.Exists(tempFileName))
					using (var formEmail = new FormEmail(_formContainer, OnlineSchedulePowerPointHelper.Instance, HelpManager))
					{
						formEmail.Text = "Email this Online Schedule";
						formEmail.PresentationFile = tempFileName;
						RegistryHelper.MainFormHandle = formEmail.Handle;
						RegistryHelper.MaximizeMainForm = false;
						formEmail.ShowDialog();
						RegistryHelper.MaximizeMainForm = true;
						RegistryHelper.MainFormHandle = _formContainer.Handle;
					}
			}
		}

		public void Preview_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				var tempFileName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				OnlineSchedulePowerPointHelper.Instance.PrepareScheduleEmail(tempFileName, _tabPages.Select(t => t.Product).ToArray(), SelectedTheme);
				Utilities.Instance.ActivateForm(_formContainer.Handle, true, false);
				formProgress.Close();
				if (File.Exists(tempFileName))
					ShowPreview(tempFileName);
			}
		}

		public abstract void ShowPreview(string tempFileName);

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
}