using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.BusinessClasses;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses.ToolForms;
using NewBizWiz.OnlineSchedule.Controls.Properties;
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
				comboBoxEditSlideHeader.Font = font;
			}
		}

		#region CommandButtons
		public ButtonX Title { get { return buttonXTitle; } }
		public ButtonX CPM { get { return buttonXCPM; } }
		public ButtonX BusinessName { get { return buttonXBusinessName; } }
		public ButtonX DecisionMaker { get { return buttonXDecisionMaker; } }
		public ButtonX PresentationDate { get { return buttonXPresentationDate; } }
		public ButtonX ActiveDays { get { return buttonXActiveDays; } }
		public ButtonX FlightDates { get { return buttonXFlightDates; } }
		public ButtonX AdRate { get { return buttonXAdRate; } }
		public ButtonX Description { get { return buttonXDescription; } }
		public ButtonX Dimensions { get { return buttonXDimensions; } }
		public ButtonX AvgMonthlyRate { get { return buttonXAvgMonthlyRate; } }
		public ButtonX TotalMonthlyRate { get { return buttonXTotalMonthlyRate; } }
		public ButtonX Comments { get { return buttonXComments; } }
		public ButtonX TotalAds { get { return buttonXTotalAds; } }
		public ButtonX AvgTotalRate { get { return buttonXAvgTotalRate; } }
		public ButtonX TotalRate { get { return buttonXTotalRate; } }
		public ButtonX ImageIcons { get { return buttonXImageIcons; } }
		public ButtonX ScreenshotViewer { get { return buttonXScreenshotViewer; } }
		public ButtonX SignatureLine { get { return buttonXSignatureLine; } }
		public ButtonX Websites { get { return buttonXWebsites; } }
		public abstract ButtonItem Preview { get; }
		public abstract ButtonItem PowerPoint { get; }
		public abstract ButtonItem Email { get; }
		public abstract ButtonItem Theme { get; }
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
			labelControlOutputStatus.Focus();
		}

		protected void LoadProduct()
		{
			bool tempSettingsNotSaved = SettingsNotSaved;
			bool temp = AllowApplyValues;
			AllowApplyValues = false;
			if (xtraTabControlProducts.SelectedTabPageIndex >= 0)
			{
				DigitalProduct product = (xtraTabControlProducts.TabPages[xtraTabControlProducts.SelectedTabPageIndex] as DigitalProductControl).Product;
				Websites.CheckedChanged -= TogledButton_CheckedChanged;
				Websites.Checked = product.ShowWebsite;
				Websites.CheckedChanged += TogledButton_CheckedChanged;
				(xtraTabControlProducts.TabPages[xtraTabControlProducts.SelectedTabPageIndex] as DigitalProductControl).WebsiteCheckedChanged();
				BusinessName.Checked = product.ShowBusinessName;
				PresentationDate.Checked = product.ShowPresentationDate;
				DecisionMaker.Checked = product.ShowDecisionMaker;
				Title.Checked = product.ShowProduct;
				ActiveDays.Checked = product.ShowActiveDays;
				AdRate.Checked = product.ShowAdRate;
				TotalRate.Checked = product.ShowTotalInvestment;
				TotalMonthlyRate.Checked = product.ShowMonthlyInvestment;
				AvgTotalRate.Checked = product.ShowTotalImpressions;
				AvgMonthlyRate.Checked = product.ShowMonthlyImpressions;
				if ((AvgTotalRate.Checked && TotalRate.Checked) || (AvgMonthlyRate.Checked && TotalMonthlyRate.Checked))
					CPM.Enabled = true;
				else
				{
					CPM.Checked = false;
					CPM.Enabled = false;
				}
				CPM.Checked = product.ShowCPMButton;
				Description.Checked = product.ShowDescription;
				Dimensions.Checked = product.ShowDimensions;
				FlightDates.Checked = product.ShowFlightDates;
				Comments.Checked = product.ShowComments;
				TotalAds.Checked = product.ShowTotalAds;
				ImageIcons.Checked = product.ShowImages;
				ScreenshotViewer.Checked = product.ShowScreenshot;
				SignatureLine.Checked = product.ShowSignature;
				SettingsNotSaved = tempSettingsNotSaved;
			}

			if (xtraTabControlProducts.SelectedTabPage != null)
				(xtraTabControlProducts.SelectedTabPage as DigitalProductControl).UpdateOutputStatus();
			AllowApplyValues = temp;
		}

		protected abstract bool SaveSchedule(string scheduleName = "");

		protected void ApplyProductValues(DigitalProductControl tabPage)
		{
			if (AllowApplyValues)
			{
				tabPage.Product.ShowBusinessName = BusinessName.Checked;
				tabPage.Product.ShowDecisionMaker = DecisionMaker.Checked;
				tabPage.Product.ShowPresentationDate = PresentationDate.Checked;
				tabPage.Product.ShowProduct = Title.Checked;
				tabPage.Product.ShowActiveDays = ActiveDays.Checked;
				tabPage.Product.ShowAdRate = AdRate.Checked;
				tabPage.Product.ShowCPMButton = CPM.Checked;
				tabPage.Product.ShowDescription = Description.Checked;
				tabPage.Product.ShowDimensions = Dimensions.Checked;
				tabPage.Product.ShowFlightDates = FlightDates.Checked;
				tabPage.Product.ShowMonthlyImpressions = AvgMonthlyRate.Checked;
				tabPage.Product.ShowMonthlyInvestment = TotalMonthlyRate.Checked;
				tabPage.Product.ShowComments = Comments.Checked;
				tabPage.Product.ShowTotalAds = TotalAds.Checked;
				tabPage.Product.ShowTotalImpressions = AvgTotalRate.Checked;
				tabPage.Product.ShowTotalInvestment = TotalRate.Checked;
				tabPage.Product.ShowImages = ImageIcons.Checked;
				tabPage.Product.ShowScreenshot = ScreenshotViewer.Checked;
				tabPage.Product.ShowSignature = SignatureLine.Checked;
				tabPage.Product.ShowWebsite = Websites.Checked;
				tabPage.WebsiteCheckedChanged();
				tabPage.UpdateView();
				tabPage.UpdateDefaultCPM();
			}
		}

		protected void ScheduleBuilderControl_Load(object sender, EventArgs e)
		{
			AssignCloseActiveEditorsonOutSideClick(pnHeader);
		}

		protected void xtraTabControlProducts_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			LoadProduct();
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

		public virtual void TogledButton_CheckedChanged(object sender, EventArgs e)
		{
			if ((AvgTotalRate.Checked && TotalRate.Checked) || (AvgMonthlyRate.Checked && TotalMonthlyRate.Checked))
				CPM.Enabled = true;
			else
			{
				bool temp = AllowApplyValues;
				AllowApplyValues = false;
				CPM.Checked = false;
				AllowApplyValues = temp;
				CPM.Enabled = false;
			}

			if (xtraTabControlProducts.SelectedTabPage != null)
				ApplyProductValues(xtraTabControlProducts.SelectedTabPage as DigitalProductControl);

			(xtraTabControlProducts.TabPages[xtraTabControlProducts.SelectedTabPageIndex] as DigitalProductControl).UpdateOutputStatus();
			SettingsNotSaved = true;
		}

		private void hyperLinkEditReset_OpenLink(object sender, OpenLinkEventArgs e)
		{
			var selectedProductControl = xtraTabControlProducts.SelectedTabPage as DigitalProductControl;
			if (selectedProductControl != null)
			{
				selectedProductControl.Product.ApplyDefaultView();
				LoadProduct();
				selectedProductControl.ResetProductName(this, new OpenLinkEventArgs(String.Empty));
				selectedProductControl.UpdateView();
				SaveSchedule();
			}
			e.Handled = true;
		}

		private void pbOutputHelp_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink(pbOutputHelp.Text);
		}

		public void PowerPoint_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			using (var form = new FormSelectPublication())
			{
				form.Text = "Digital Slide Output";
				form.pbLogo.Image = Resources.PowerPoint;
				form.laTitle.Text = "You have Several Online Schedule tabs available for output to PowerPoint…";
				form.buttonXCurrentPublication.Text = "Send just the active Online Schedule Slide to PowerPoint";
				form.buttonXSelectedPublications.Text = "Send ALL SELECTED Online Schedule Slides to PowerPoint";
				foreach (DigitalProductControl tabPage in _tabPages)
				{
					tabPage.SaveValues();
					form.checkedListBoxControlPublications.Items.Add(tabPage.Product.UniqueID, tabPage.Product.Name, CheckState.Checked, true);
				}
				var result = DialogResult.Yes;
				if (form.checkedListBoxControlPublications.Items.Count > 1)
				{
					RegistryHelper.MainFormHandle = form.Handle;
					RegistryHelper.MaximizeMainForm = false;
					result = form.ShowDialog();
					RegistryHelper.MaximizeMainForm = true;
					RegistryHelper.MainFormHandle = _formContainer.Handle;
					if (result == DialogResult.Cancel)
						return;
				}
				var tabsForOutput = new List<DigitalProductControl>();
				if (result == DialogResult.Yes)
				{
					if (xtraTabControlProducts.SelectedTabPage != null)
						tabsForOutput.Add(xtraTabControlProducts.SelectedTabPage as DigitalProductControl);
				}
				else if (result == DialogResult.No)
				{
					foreach (CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
					{
						if (item.CheckState != CheckState.Checked) continue;
						var tabPage = _tabPages.FirstOrDefault(x => x.Product.UniqueID.Equals(item.Value));
						if (tabPage != null)
							tabsForOutput.Add(tabPage);
					}
				}
				OutputSlides(tabsForOutput);
			}
		}

		public abstract void OutputSlides(IEnumerable<DigitalProductControl> tabsForOutput);

		public void Email_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			using (var form = new FormSelectPublication())
			{
				form.Text = "Digital Email Output";
				form.pbLogo.Image = Resources.Email;
				form.laTitle.Text = "You have Several Online Schedules that you may choose to email…";
				form.buttonXCurrentPublication.Text = "Attach just the active Online Schedule Slide to my Outlook Email Message";
				form.buttonXSelectedPublications.Text = "Attach ALL SELECTED Online Schedule Slides to my Outlook Email Message";
				foreach (DigitalProductControl tabPage in _tabPages)
				{
					tabPage.SaveValues();
					form.checkedListBoxControlPublications.Items.Add(tabPage.Product.UniqueID, tabPage.Product.Name, CheckState.Checked, true);
				}
				var result = DialogResult.Yes;
				if (form.checkedListBoxControlPublications.Items.Count > 1)
				{
					RegistryHelper.MainFormHandle = form.Handle;
					RegistryHelper.MaximizeMainForm = false;
					result = form.ShowDialog();
					RegistryHelper.MaximizeMainForm = true;
					RegistryHelper.MainFormHandle = _formContainer.Handle;
					if (result == DialogResult.Cancel)
						return;
				}
				using (var formProgress = new FormProgress())
				{
					formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
					formProgress.TopMost = true;
					formProgress.Show();
					string tempFileName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
					if (result == DialogResult.Yes)
						OnlineSchedulePowerPointHelper.Instance.PrepareScheduleEmail(tempFileName, new[] { (xtraTabControlProducts.SelectedTabPage as DigitalProductControl).Product }, SelectedTheme);
					else if (result == DialogResult.No)
					{
						var outputProducts = new List<DigitalProduct>();
						foreach (CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
						{
							if (item.CheckState == CheckState.Checked)
							{
								DigitalProductControl tabPage = _tabPages.Where(x => x.Product.UniqueID.Equals(item.Value)).FirstOrDefault();
								if (tabPage != null)
									outputProducts.Add(tabPage.Product);
							}
						}
						OnlineSchedulePowerPointHelper.Instance.PrepareScheduleEmail(tempFileName, outputProducts.ToArray(), SelectedTheme);
					}
					Utilities.Instance.ActivateForm(_formContainer.Handle, true, false);
					formProgress.Close();
					if (File.Exists(tempFileName))
						using (var formEmail = new FormEmail())
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
		}

		public void Preview_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			using (var form = new FormSelectPublication())
			{
				form.Text = "Digital Slide Preview";
				form.pbLogo.Image = Resources.Preview;
				form.laTitle.Text = "You have Several Digital Slides…";
				form.buttonXCurrentPublication.Text = "Preview just the Current Digital Product";
				form.buttonXSelectedPublications.Text = "Preview all Digital Products";
				foreach (var tabPage in _tabPages)
				{
					tabPage.SaveValues();
					form.checkedListBoxControlPublications.Items.Add(tabPage.Product.UniqueID, tabPage.Product.Name, CheckState.Checked, true);
				}
				var result = DialogResult.Yes;
				if (form.checkedListBoxControlPublications.Items.Count > 1)
				{
					RegistryHelper.MainFormHandle = form.Handle;
					RegistryHelper.MaximizeMainForm = false;
					result = form.ShowDialog();
					RegistryHelper.MaximizeMainForm = _formContainer.WindowState == FormWindowState.Maximized;
					RegistryHelper.MainFormHandle = _formContainer.Handle;
					if (result == DialogResult.Cancel)
						return;
				}
				using (var formProgress = new FormProgress())
				{
					formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
					formProgress.TopMost = true;
					formProgress.Show();
					string tempFileName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
					if (result == DialogResult.Yes)
						OnlineSchedulePowerPointHelper.Instance.PrepareScheduleEmail(tempFileName, new[] { (xtraTabControlProducts.SelectedTabPage as DigitalProductControl).Product }, SelectedTheme);
					else if (result == DialogResult.No)
					{
						var outputProducts = new List<DigitalProduct>();
						foreach (CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
						{
							if (item.CheckState == CheckState.Checked)
							{
								var tabPage = _tabPages.Where(x => x.Product.UniqueID.Equals(item.Value)).FirstOrDefault();
								if (tabPage != null)
									outputProducts.Add(tabPage.Product);
							}
						}
						OnlineSchedulePowerPointHelper.Instance.PrepareScheduleEmail(tempFileName, outputProducts.ToArray(), SelectedTheme);
					}
					Utilities.Instance.ActivateForm(_formContainer.Handle, true, false);
					formProgress.Close();
					if (File.Exists(tempFileName))
						ShowPreview(tempFileName);
				}
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