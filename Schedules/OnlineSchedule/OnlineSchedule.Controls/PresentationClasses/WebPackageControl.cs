using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.BusinessClasses;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses.ToolForms;
using ListManager = NewBizWiz.Core.OnlineSchedule.ListManager;

namespace NewBizWiz.OnlineSchedule.Controls.PresentationClasses
{
	[ToolboxItem(false)]
	public partial class WebPackageControl : UserControl
	{
		private ProductPackageControl _productPackage;

		public WebPackageControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AllowApplyValues = false;
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) =>
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			};

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
				labelControlAdvertiser.Font = font;
				labelControlPresentationDate.Font = font;
				labelControlOutputStatus.Font = new Font(labelControlOutputStatus.Font.FontFamily, labelControlOutputStatus.Font.Size - 3, labelControlOutputStatus.Font.Style);
				labelControlFormula.Font = new Font(labelControlFormula.Font.FontFamily, labelControlFormula.Font.Size - 2, labelControlFormula.Font.Style);
				checkEditFormulaCPM.Font = new Font(checkEditFormulaCPM.Font.FontFamily, checkEditFormulaCPM.Font.Size - 2, checkEditFormulaCPM.Font.Style);
				checkEditFormulaImpressions.Font = new Font(checkEditFormulaImpressions.Font.FontFamily, checkEditFormulaImpressions.Font.Size - 2, checkEditFormulaImpressions.Font.Style);
				checkEditFormulaInvestment.Font = new Font(checkEditFormulaInvestment.Font.FontFamily, checkEditFormulaInvestment.Font.Size - 2, checkEditFormulaInvestment.Font.Style);
			}
		}

		public bool SettingsNotSaved { get; set; }
		public bool AllowApplyValues { get; set; }
		public Schedule LocalSchedule { get; set; }

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

		private void AssignCloseActiveEditorsonOutSideClick(Control control)
		{
			if (control.GetType() != typeof(TextEdit) && control.GetType() != typeof(MemoEdit) && control.GetType() != typeof(ComboBoxEdit) && control.GetType() != typeof(LookUpEdit) && control.GetType() != typeof(DateEdit) && control.GetType() != typeof(CheckedListBoxControl) && control.GetType() != typeof(SpinEdit) && control.GetType() != typeof(CheckEdit))
			{
				control.Click += CloseActiveEditorsonOutSideClick;
				foreach (Control childControl in control.Controls)
					AssignCloseActiveEditorsonOutSideClick(childControl);
			}
		}

		private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
		{
			labelControlAdvertiser.Focus();
		}

		public void UpdateOutputStatus()
		{
			string templateName = _productPackage.ProductPackage.GetSlideSource(BusinessWrapper.Instance.OutputManager.OneSheetsTemplatesFolderPath);
			Controller.Instance.WebPackagePowerPoint.Enabled = !string.IsNullOrEmpty(templateName);
			Controller.Instance.WebPackageEmail.Enabled = !string.IsNullOrEmpty(templateName);
			if (!string.IsNullOrEmpty(templateName))
			{
				labelControlOutputStatus.ForeColor = Color.Green;
				labelControlOutputStatus.Text = "Slide Output AVAILABLE";
			}
			else
			{
				labelControlOutputStatus.ForeColor = Color.Red;
				labelControlOutputStatus.Text = "Slide Output DISABLED";
			}
		}

		private void ApplyProductValues()
		{
			if (AllowApplyValues)
			{
				_productPackage.HideDefaultPanel();

				if (!buttonXCPM.Checked)
				{
					_productPackage.ProductPackage.Formula = FormulaType.CPM;
					AllowApplyValues = false;
					checkEditFormulaCPM.Checked = true;
					checkEditFormulaInvestment.Checked = false;
					checkEditFormulaImpressions.Checked = false;
					AllowApplyValues = true;
				}
				else
				{
					if (checkEditFormulaCPM.Checked)
						_productPackage.ProductPackage.Formula = FormulaType.CPM;
					else if (checkEditFormulaInvestment.Checked)
						_productPackage.ProductPackage.Formula = FormulaType.Investment;
					else if (checkEditFormulaImpressions.Checked)
						_productPackage.ProductPackage.Formula = FormulaType.Impressions;
				}

				_productPackage.ProductPackage.ShowBusinessName = buttonXBusinessName.Checked;
				_productPackage.ProductPackage.ShowDecisionMaker = buttonXDecisionMaker.Checked;
				_productPackage.ProductPackage.ShowPresentationDate = buttonXPresentationDate.Checked;
				_productPackage.ProductPackage.ShowActiveDays = buttonXActiveDays.Checked;
				_productPackage.ProductPackage.ShowAdRate = buttonXAdRate.Checked;
				_productPackage.ProductPackage.ShowCPMButton = buttonXCPM.Checked;
				_productPackage.ProductPackage.ShowFlightDates = buttonXFlightDates.Checked;
				_productPackage.ProductPackage.ShowMonthlyImpressions = buttonXAvgMonthlyRate.Checked;
				_productPackage.ProductPackage.ShowMonthlyInvestment = buttonXTotalMonthlyRate.Checked;
				_productPackage.ProductPackage.ShowComments = buttonXComments.Checked;
				_productPackage.ProductPackage.ShowTotalAds = buttonXTotalAds.Checked;
				_productPackage.ProductPackage.ShowTotalImpressions = buttonXAvgTotalRate.Checked;
				_productPackage.ProductPackage.ShowTotalInvestment = buttonXTotalRate.Checked;
				_productPackage.ProductPackage.ShowImages = buttonXImageIcons.Checked;
				_productPackage.ProductPackage.ShowScreenshot = buttonXScreenshotViewer.Checked;
				_productPackage.ProductPackage.ShowSignature = buttonXSignatureLine.Checked;
				_productPackage.ProductPackage.ShowWebsite = buttonXWebsites.Checked;
				_productPackage.WebsiteCheckedChanged();
				_productPackage.UpdateView();
			}
		}

		public void LoadSchedule(bool quickLoad)
		{
			LocalSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();

			if (!quickLoad || _productPackage == null)
			{
				comboBoxEditSlideHeader.Properties.Items.Clear();
				comboBoxEditSlideHeader.Properties.Items.AddRange(ListManager.Instance.SlideHeaders.ToArray());
				if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
					comboBoxEditSlideHeader.SelectedIndex = 0;

				pnMain.Controls.Clear();
				Application.DoEvents();
				_productPackage = new ProductPackageControl();
				Application.DoEvents();
				_productPackage.ProductPackage = LocalSchedule.ProductPackage;
				_productPackage.LoadValues();
				Application.DoEvents();
				pnMain.Controls.Add(_productPackage);
				Application.DoEvents();

				LoadProduct();
				Application.DoEvents();
				AllowApplyValues = true;
			}
			else
			{
				_productPackage.ProductPackage = LocalSchedule.ProductPackage;
				Application.DoEvents();
			}

			SettingsNotSaved = false;
		}

		private void LoadProduct()
		{
			bool tempSettingsNotSaved = SettingsNotSaved;
			bool temp = AllowApplyValues;
			AllowApplyValues = false;
			if (_productPackage != null)
			{
				buttonXWebsites.CheckedChanged -= TogledButton_CheckedChanged;
				buttonXWebsites.Checked = _productPackage.ProductPackage.ShowWebsite;
				buttonXWebsites.CheckedChanged += TogledButton_CheckedChanged;
				_productPackage.WebsiteCheckedChanged();
				buttonXBusinessName.Checked = _productPackage.ProductPackage.ShowBusinessName;
				buttonXPresentationDate.Checked = _productPackage.ProductPackage.ShowPresentationDate;
				buttonXDecisionMaker.Checked = _productPackage.ProductPackage.ShowDecisionMaker;
				buttonXActiveDays.Checked = _productPackage.ProductPackage.ShowActiveDays;
				buttonXAdRate.Checked = _productPackage.ProductPackage.ShowAdRate;
				buttonXTotalRate.Checked = _productPackage.ProductPackage.ShowTotalInvestment;
				buttonXTotalMonthlyRate.Checked = _productPackage.ProductPackage.ShowMonthlyInvestment;
				buttonXAvgTotalRate.Checked = _productPackage.ProductPackage.ShowTotalImpressions;
				buttonXAvgMonthlyRate.Checked = _productPackage.ProductPackage.ShowMonthlyImpressions;
				if ((buttonXAvgTotalRate.Checked && buttonXTotalRate.Checked) || (buttonXAvgMonthlyRate.Checked && buttonXTotalMonthlyRate.Checked))
					buttonXCPM.Enabled = true;
				else
				{
					buttonXCPM.Checked = false;
					buttonXCPM.Enabled = false;
				}
				buttonXCPM.Checked = _productPackage.ProductPackage.ShowCPMButton;
				buttonXFlightDates.Checked = _productPackage.ProductPackage.ShowFlightDates;
				buttonXComments.Checked = _productPackage.ProductPackage.ShowComments;
				buttonXTotalAds.Checked = _productPackage.ProductPackage.ShowTotalAds;
				buttonXImageIcons.Checked = _productPackage.ProductPackage.ShowImages;
				buttonXScreenshotViewer.Checked = _productPackage.ProductPackage.ShowScreenshot;
				buttonXSignatureLine.Checked = _productPackage.ProductPackage.ShowSignature;

				labelControlPresentationDate.Visible = _productPackage.ProductPackage.ShowPresentationDate;
				if (_productPackage.ProductPackage.ShowBusinessName && _productPackage.ProductPackage.ShowDecisionMaker)
				{
					labelControlAdvertiser.Visible = true;
					labelControlAdvertiser.Text = "Prepared For: " + _productPackage.ProductPackage.Parent.BusinessName + "\n\n" + _productPackage.ProductPackage.Parent.DecisionMaker;
				}
				else if (!_productPackage.ProductPackage.ShowBusinessName && _productPackage.ProductPackage.ShowDecisionMaker)
				{
					labelControlAdvertiser.Visible = true;
					labelControlAdvertiser.Text = _productPackage.ProductPackage.Parent.DecisionMaker;
				}
				else if (_productPackage.ProductPackage.ShowBusinessName && !_productPackage.ProductPackage.ShowDecisionMaker)
				{
					labelControlAdvertiser.Visible = true;
					labelControlAdvertiser.Text = "Prepared For: " + _productPackage.ProductPackage.Parent.BusinessName;
				}
				else
				{
					labelControlAdvertiser.Visible = false;
				}

				if (_productPackage.ProductPackage.ShowActiveDays ||
				    _productPackage.ProductPackage.ShowAdRate ||
				    _productPackage.ProductPackage.ShowBusinessName ||
				    _productPackage.ProductPackage.ShowComments ||
				    _productPackage.ProductPackage.ShowCPMButton ||
				    _productPackage.ProductPackage.ShowDecisionMaker ||
				    _productPackage.ProductPackage.ShowFlightDates ||
				    _productPackage.ProductPackage.ShowMonthlyImpressions ||
				    _productPackage.ProductPackage.ShowMonthlyInvestment ||
				    _productPackage.ProductPackage.ShowPresentationDate ||
				    _productPackage.ProductPackage.ShowTotalAds ||
				    _productPackage.ProductPackage.ShowTotalImpressions ||
				    _productPackage.ProductPackage.ShowTotalInvestment)
					_productPackage.HideDefaultPanel();

				switch (_productPackage.ProductPackage.Formula)
				{
					case FormulaType.CPM:
						checkEditFormulaCPM.Checked = true;
						checkEditFormulaInvestment.Checked = false;
						checkEditFormulaImpressions.Checked = false;
						break;
					case FormulaType.Investment:
						checkEditFormulaCPM.Checked = false;
						checkEditFormulaInvestment.Checked = true;
						checkEditFormulaImpressions.Checked = false;
						break;
					case FormulaType.Impressions:
						checkEditFormulaCPM.Checked = false;
						checkEditFormulaInvestment.Checked = false;
						checkEditFormulaImpressions.Checked = true;
						break;
				}

				SettingsNotSaved = tempSettingsNotSaved;
			}
			UpdateOutputStatus();
			AllowApplyValues = temp;
		}

		private bool SaveSchedule(string scheduleName = "")
		{
			if (!string.IsNullOrEmpty(scheduleName))
			{
				LocalSchedule.Name = scheduleName;
			}

			if (_productPackage != null)
				_productPackage.SaveValues();

			Controller.Instance.SaveSchedule(LocalSchedule, false, this);
			SettingsNotSaved = false;
			return true;
		}

		private void WebPackageControl_Load(object sender, EventArgs e)
		{
			AssignCloseActiveEditorsonOutSideClick(panelExHeader);
			AssignCloseActiveEditorsonOutSideClick(pnHeader);
		}

		public void Options_CheckedChanged(object sender, EventArgs e)
		{
			splitContainerControl.PanelVisibility = Controller.Instance.ScheduleSlidesOptions.Checked ? SplitPanelVisibility.Both : SplitPanelVisibility.Panel2;
		}

		public void Save_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			Utilities.Instance.ShowInformation("Schedule Saved");
		}

		public void SaveAs_Click(object sender, EventArgs e)
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

		public void TogledButton_CheckedChanged(object sender, EventArgs e)
		{
			if ((buttonXAvgTotalRate.Checked && buttonXTotalRate.Checked) || (buttonXAvgMonthlyRate.Checked && buttonXTotalMonthlyRate.Checked))
				buttonXCPM.Enabled = true;
			else
			{
				bool temp = AllowApplyValues;
				AllowApplyValues = false;
				buttonXCPM.Checked = false;
				AllowApplyValues = temp;
				buttonXCPM.Enabled = false;
			}

			ApplyProductValues();
			UpdateOutputStatus();
			SettingsNotSaved = true;
		}

		private void checkEditFormula_CheckedChanged(object sender, EventArgs e)
		{
			if (AllowApplyValues && (sender as CheckEdit).Checked)
			{
				AllowApplyValues = false;
				checkEditFormulaCPM.Checked = false;
				checkEditFormulaImpressions.Checked = false;
				checkEditFormulaInvestment.Checked = false;
				(sender as CheckEdit).Checked = true;
				AllowApplyValues = true;
				TogledButton_CheckedChanged(null, null);
			}
		}

		public void PowerPoint_Click(object sender, EventArgs e)
		{
			if (_productPackage != null)
				_productPackage.Output();
		}

		public void Email_Click(object sender, EventArgs e)
		{
			if (_productPackage != null)
				_productPackage.Email();
		}

		public void Help_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("pkg");
		}
	}
}