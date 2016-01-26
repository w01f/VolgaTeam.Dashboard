using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.CommonGUI.Common;
using DevComponents.DotNetBar;
using Asa.CommonGUI.Preview;
using Asa.CommonGUI.ToolForms;
using Asa.Core.Common;
using Asa.Core.Dashboard;
using Asa.Dashboard.InteropClasses;
using Asa.Dashboard.Properties;
using ListManager = Asa.Core.Dashboard.ListManager;

namespace Asa.Dashboard.TabHomeForms
{
	[ToolboxItem(false)]
	public sealed partial class SlideCoverControl : SlideBaseControl
	{
		private bool _allowToSave;
		private readonly SuperTooltipInfo _toolTipLoad = new SuperTooltipInfo("Cover Slides", "", "Open previously-saved Cover slide data files", null, null, eTooltipColor.Gray);
		private readonly SuperTooltipInfo _toolTipHelp = new SuperTooltipInfo("HELP", "", "Help me with the Cover Slide", null, null, eTooltipColor.Gray);
		private readonly List<User> _users = new List<User>();

		public SlideCoverControl()
			: base()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AppManager.Instance.SetClickEventHandler(this);
			if ((CreateGraphics()).DpiX > 96)
			{
				laAdvertiser.Font = new Font(laAdvertiser.Font.FontFamily, laAdvertiser.Font.Size - 2, laAdvertiser.Font.Style);
				laDecisionMaker.Font = new Font(laDecisionMaker.Font.FontFamily, laDecisionMaker.Font.Size - 2, laDecisionMaker.Font.Style);
				checkEditFirstSlide.Font = new Font(checkEditFirstSlide.Font.FontFamily, checkEditFirstSlide.Font.Size - 2, checkEditFirstSlide.Font.Style);
				checkEditPresentationDate.Font = new Font(checkEditPresentationDate.Font.FontFamily, checkEditPresentationDate.Font.Size - 2, checkEditPresentationDate.Font.Style);
				checkEditSalesRep.Font = new Font(checkEditSalesRep.Font.FontFamily, checkEditSalesRep.Font.Size - 2, checkEditSalesRep.Font.Style);
				checkEditUseEmptyCover.Font = new Font(checkEditUseEmptyCover.Font.FontFamily, checkEditUseEmptyCover.Font.Size - 2, checkEditUseEmptyCover.Font.Style);
				buttonXSalesQuote.Font = new Font(buttonXSalesQuote.Font.FontFamily, buttonXSalesQuote.Font.Size - 2, buttonXSalesQuote.Font.Style);
				textEditSalesQuoteAuthor.Font = new Font(textEditSalesQuoteAuthor.Font.FontFamily, textEditSalesQuoteAuthor.Font.Size - 2, textEditSalesQuoteAuthor.Font.Style);
				memoEditSalesQuote.Font = new Font(memoEditSalesQuote.Font.FontFamily, memoEditSalesQuote.Font.Size - 2, memoEditSalesQuote.Font.Style);
			}
			comboBoxEditSlideHeader.MouseUp += TextEditorsHelper.Editor_MouseUp;
			comboBoxEditSlideHeader.MouseDown += TextEditorsHelper.Editor_MouseDown;
			comboBoxEditSlideHeader.Enter += TextEditorsHelper.Editor_Enter;
			comboBoxEditAdvertiser.MouseUp += TextEditorsHelper.Editor_MouseUp;
			comboBoxEditAdvertiser.MouseDown += TextEditorsHelper.Editor_MouseDown;
			comboBoxEditAdvertiser.Enter += TextEditorsHelper.Editor_Enter;
			comboBoxEditDecisionMaker.MouseUp += TextEditorsHelper.Editor_MouseUp;
			comboBoxEditDecisionMaker.MouseDown += TextEditorsHelper.Editor_MouseDown;
			comboBoxEditDecisionMaker.Enter += TextEditorsHelper.Editor_Enter;

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(ListManager.Instance.CoverLists.Headers);

			_users.Clear();
			_users.AddRange(ListManager.Instance.UsersList.GetUsersByStation(MasterWizardManager.Instance.SelectedWizard.Name));
			comboBoxEditSalesRep.Properties.Items.Clear();
			comboBoxEditSalesRep.Properties.Items.AddRange(_users.Select(it => it.FullName).ToArray());

			checkEditSolutionNew.EditValueChanged += EditValueChanged;

			FormMain.Instance.FormClosed += (sender1, e1) =>
			{
				if (!SettingsNotSaved) return;
				SaveState();
				ViewSettingsManager.Instance.CoverState.Save();
			};

			LoadSavedState();
		}

		public override string SlideName
		{
			get { return "Cover"; }
		}

		public override SuperTooltipInfo TooltipLoad
		{
			get { return _toolTipLoad; }
		}

		public override SuperTooltipInfo TooltipHelp
		{
			get { return _toolTipHelp; }
		}

		public override ButtonItem ThemeButton
		{
			get { return FormMain.Instance.buttonItemHomeThemeCover; }
		}

		private void LoadSavedState()
		{
			_allowToSave = false;
			checkEditSolutionNew.Checked = ViewSettingsManager.Instance.CoverState.IsNewSolution;
			if (string.IsNullOrEmpty(ViewSettingsManager.Instance.CoverState.SlideHeader))
			{
				if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
					comboBoxEditSlideHeader.SelectedIndex = 0;
			}
			else
			{
				var index = comboBoxEditSlideHeader.Properties.Items.IndexOf(ViewSettingsManager.Instance.CoverState.SlideHeader);
				comboBoxEditSlideHeader.SelectedIndex = index >= 0 ? index : 0;
			}
			comboBoxEditAdvertiser.EditValue = String.IsNullOrEmpty(ViewSettingsManager.Instance.CoverState.Advertiser) ? null : ViewSettingsManager.Instance.CoverState.Advertiser;
			comboBoxEditDecisionMaker.EditValue = String.IsNullOrEmpty(ViewSettingsManager.Instance.CoverState.DecisionMaker) ? null : ViewSettingsManager.Instance.CoverState.DecisionMaker;

			checkEditFirstSlide.Checked = ViewSettingsManager.Instance.CoverState.AddAsPageOne;
			checkEditPresentationDate.Checked = ViewSettingsManager.Instance.CoverState.ShowPresentationDate;
			dateEditPresentationDate.Enabled = checkEditPresentationDate.Checked;
			if (checkEditPresentationDate.Checked)
				dateEditPresentationDate.EditValue = ViewSettingsManager.Instance.CoverState.PresentationDate != DateTime.MinValue ? (object)ViewSettingsManager.Instance.CoverState.PresentationDate : null;
			else
				dateEditPresentationDate.EditValue = null;
			checkEditUseEmptyCover.Checked = ViewSettingsManager.Instance.CoverState.UseGenericCover;
			comboBoxEditAdvertiser.Enabled = !checkEditUseEmptyCover.Checked;
			comboBoxEditDecisionMaker.Enabled = !checkEditUseEmptyCover.Checked;
			comboBoxEditSlideHeader.Enabled = !checkEditUseEmptyCover.Checked;
			buttonXSalesQuote.Enabled = !checkEditUseEmptyCover.Checked;
			dateEditPresentationDate.Enabled = !checkEditUseEmptyCover.Checked && checkEditPresentationDate.Checked;
			checkEditPresentationDate.Enabled = !checkEditUseEmptyCover.Checked;
			checkEditSalesRep.Checked = !String.IsNullOrEmpty(Core.Dashboard.SettingsManager.Instance.SalesRep);
			checkEditSalesRep.Enabled = !checkEditUseEmptyCover.Checked;
			comboBoxEditSalesRep.Enabled = !checkEditUseEmptyCover.Checked && checkEditSalesRep.Checked;
			comboBoxEditSalesRep.EditValue = Core.Dashboard.SettingsManager.Instance.SalesRep;

			if (ViewSettingsManager.Instance.CoverState.Quote.IsSet)
			{
				textEditSalesQuoteAuthor.EditValue = ViewSettingsManager.Instance.CoverState.Quote.Author;
				memoEditSalesQuote.EditValue = ViewSettingsManager.Instance.CoverState.Quote.Text;
				buttonXSalesQuote.Image = null;
				buttonXSalesQuote.Text = "Remove";
				textEditSalesQuoteAuthor.Visible = true;
				memoEditSalesQuote.Visible = true;
				laSalesQuotesHint.Visible = false;
			}
			else
			{
				textEditSalesQuoteAuthor.EditValue = null;
				memoEditSalesQuote.EditValue = null;
				buttonXSalesQuote.Image = Resources.SalesQuotes;
				buttonXSalesQuote.Text = String.Empty;
				textEditSalesQuoteAuthor.Visible = false;
				memoEditSalesQuote.Visible = false;
				laSalesQuotesHint.Visible = true;
			}

			_allowToSave = true;
			SettingsNotSaved = false;

			UpdateOutputState();

			UpdateSavedFilesState();
		}

		private void SaveState()
		{
			ViewSettingsManager.Instance.CoverState.IsNewSolution = checkEditSolutionNew.Checked;
			ViewSettingsManager.Instance.CoverState.SlideHeader = (comboBoxEditSlideHeader.EditValue as String) ?? String.Empty;
			ViewSettingsManager.Instance.CoverState.AddAsPageOne = checkEditFirstSlide.Checked;
			ViewSettingsManager.Instance.CoverState.ShowPresentationDate = checkEditPresentationDate.Checked;
			ViewSettingsManager.Instance.CoverState.PresentationDate = dateEditPresentationDate.DateTime;
			ViewSettingsManager.Instance.CoverState.UseGenericCover = checkEditUseEmptyCover.Checked;
			ViewSettingsManager.Instance.CoverState.Quote.Author = (textEditSalesQuoteAuthor.EditValue as String) ?? String.Empty;
			ViewSettingsManager.Instance.CoverState.Quote.Text = (memoEditSalesQuote.EditValue as String) ?? String.Empty;
			ViewSettingsManager.Instance.CoverState.Advertiser = (comboBoxEditAdvertiser.EditValue as String) ?? String.Empty;
			ViewSettingsManager.Instance.CoverState.DecisionMaker = (comboBoxEditDecisionMaker.EditValue as String) ?? String.Empty;

			Core.Dashboard.SettingsManager.Instance.SalesRep = (comboBoxEditSalesRep.EditValue as String) ?? String.Empty;
			Core.Dashboard.SettingsManager.Instance.SaveDashboardSettings();

			SettingsNotSaved = false;
		}

		private void EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			UpdateOutputState();
			SettingsNotSaved = true;
		}

		private void checkEditSalesRep_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			comboBoxEditSalesRep.Enabled = checkEditSalesRep.Checked;
			if (!checkEditSalesRep.Checked)
				comboBoxEditSalesRep.EditValue = null;
			SettingsNotSaved = true;
		}

		private void comboBoxEditSalesRep_EditValueChanged(object sender, EventArgs e)
		{
			var user = _users.FirstOrDefault(u => u.FullName.Equals(comboBoxEditSalesRep.EditValue as String));
			laSalesRepDetails.Text = user != null ? String.Format("{0}{2}{1}", user.Phone, user.Email, Environment.NewLine) : String.Empty;
			if (!_allowToSave) return;
			UpdateOutputState();
			SettingsNotSaved = true;
		}

		private void ckPresentationDate_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			dateEditPresentationDate.Enabled = checkEditPresentationDate.Checked;
			if (!checkEditPresentationDate.Checked)
				dateEditPresentationDate.EditValue = null;
			SettingsNotSaved = true;
		}

		private void buttonXSalesQuote_Click(object sender, EventArgs e)
		{
			if (textEditSalesQuoteAuthor.EditValue == null && memoEditSalesQuote.EditValue == null)
			{
				using (var form = new FormQuotes())
				{
					if (form.ShowDialog() != DialogResult.OK) return;
					if (form.SelectedQuote == null) return;

					textEditSalesQuoteAuthor.EditValue = form.SelectedQuote.Author;
					memoEditSalesQuote.EditValue = "\"" + form.SelectedQuote.Text + "\"";
					textEditSalesQuoteAuthor.Visible = true;
					memoEditSalesQuote.Visible = true;
					laSalesQuotesHint.Visible = false;

					buttonXSalesQuote.Image = null;
					buttonXSalesQuote.Text = "Remove";
				}
			}
			else
			{
				textEditSalesQuoteAuthor.EditValue = null;
				memoEditSalesQuote.EditValue = null;
				textEditSalesQuoteAuthor.Visible = false;
				memoEditSalesQuote.Visible = false;
				laSalesQuotesHint.Visible = true;

				buttonXSalesQuote.Image = Resources.SalesQuotes;
				buttonXSalesQuote.Text = String.Empty;
			}
			SettingsNotSaved = true;
		}

		private void checkEditUseEmptyCover_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			comboBoxEditAdvertiser.Enabled = !checkEditUseEmptyCover.Checked;
			comboBoxEditDecisionMaker.Enabled = !checkEditUseEmptyCover.Checked;
			comboBoxEditSlideHeader.Enabled = !checkEditUseEmptyCover.Checked;
			comboBoxEditSalesRep.Enabled = !checkEditUseEmptyCover.Checked;
			buttonXSalesQuote.Enabled = !checkEditUseEmptyCover.Checked;
			dateEditPresentationDate.Enabled = !checkEditUseEmptyCover.Checked && checkEditPresentationDate.Checked;
			checkEditPresentationDate.Enabled = !checkEditUseEmptyCover.Checked;
			UpdateOutputState();
			SettingsNotSaved = true;
		}

		private void checkEdit_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
				SettingsNotSaved = true;
		}

		public override void LoadClick()
		{
			using (var form = new FormSavedCover())
			{
				if (form.ShowDialog() == DialogResult.OK && !String.IsNullOrEmpty(form.SelectedFile))
				{
					ViewSettingsManager.Instance.CoverState.Load(form.SelectedFile);
					LoadSavedState();
				}
			}
			base.LoadClick();
		}

		#region Output Staff

		public string Title
		{
			get { return (comboBoxEditSlideHeader.EditValue as String) ?? String.Empty; }
		}

		public string Advertiser
		{
			get { return (comboBoxEditAdvertiser.EditValue as String) ?? String.Empty; }
		}

		public string DecisionMaker
		{
			get { return (comboBoxEditDecisionMaker.EditValue as String) ?? String.Empty; }
		}

		public string SalesRep
		{
			get
			{
				var userName = comboBoxEditSalesRep.EditValue as String;
				var user = _users.FirstOrDefault(u => u.FullName.Equals(userName));
				return user != null ?
					(user.FullName + "          " + user.Email + "          " + user.Phone) :
					(userName ?? String.Empty);
			}
		}

		public string PresentationDate
		{
			get { return dateEditPresentationDate.EditValue != null ? dateEditPresentationDate.DateTime.ToString("MMMM d, yyyy") : String.Empty; }
		}

		public string Quote
		{
			get
			{
				return (memoEditSalesQuote.EditValue == null ? string.Empty : memoEditSalesQuote.EditValue.ToString())
					   + (char)13 + (textEditSalesQuoteAuthor.EditValue == null ? string.Empty : textEditSalesQuoteAuthor.EditValue.ToString());
			}
		}

		public void UpdateOutputState()
		{
			var result = comboBoxEditAdvertiser.EditValue != null && !string.IsNullOrEmpty(comboBoxEditAdvertiser.EditValue.ToString().Trim());
			result = result | checkEditUseEmptyCover.Checked;
			SetOutputState(result);
		}

		protected override void UpdateSavedFilesState()
		{
			SetLoadState(ViewSettingsManager.Instance.CoverState.AllowToLoad());
		}

		protected override void SaveChanges(string fileName = "")
		{
			Core.Common.ListManager.Instance.Advertisers.Add(Advertiser);
			Core.Common.ListManager.Instance.Advertisers.Save();

			Core.Common.ListManager.Instance.DecisionMakers.Add(DecisionMaker);
			Core.Common.ListManager.Instance.DecisionMakers.Save();

			if (!SettingsNotSaved) return;
			SaveState();
			ViewSettingsManager.Instance.CoverState.Save(fileName);
			UpdateSavedFilesState();
		}

		public void Output()
		{
			SaveChanges();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			FormProgress.ShowProgress();
			if (checkEditUseEmptyCover.Checked)
				AppManager.Instance.ShowFloater(() =>
				{
					DashboardPowerPointHelper.Instance.AppendGenericCover(checkEditFirstSlide.Checked);
					FormProgress.CloseProgress();
				});

			else
				AppManager.Instance.ShowFloater(() =>
				{
					DashboardPowerPointHelper.Instance.AppendCover(checkEditFirstSlide.Checked);
					FormProgress.CloseProgress();
				});
		}

		public void Preview()
		{
			SaveChanges();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			var tempFileName = Path.Combine(Core.Common.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			if (checkEditUseEmptyCover.Checked)
				DashboardPowerPointHelper.Instance.PrepareGenericCover(tempFileName);
			else
				DashboardPowerPointHelper.Instance.PrepareCover(tempFileName);
			Utilities.Instance.ActivateForm(FormMain.Instance.Handle, false, false);
			FormProgress.CloseProgress();
			if (!File.Exists(tempFileName)) return;
			using (var formPreview = new FormPreview(FormMain.Instance, DashboardPowerPointHelper.Instance, AppManager.Instance.HelpManager, AppManager.Instance.ShowFloater))
			{
				formPreview.Text = "Preview Slides";
				formPreview.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName, InsertOnTop = checkEditFirstSlide.Checked } });
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = false;
				RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
				if (previewResult != DialogResult.OK)
					AppManager.Instance.ActivateMainForm();
			}
		}
		#endregion

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