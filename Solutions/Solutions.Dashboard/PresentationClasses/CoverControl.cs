using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Dashboard.Dictionaries;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Dashboard.Properties;

namespace Asa.Solutions.Dashboard.PresentationClasses
{
	[ToolboxItem(false)]
	public sealed partial class CoverControl : DashboardSlideControl
	{
		private bool _allowToSave;
		private readonly List<User> _users = new List<User>();

		public override SlideType SlideType => SlideType.Cover;
		public override string SlideName => "Cover";

		public CoverControl(BaseDashboardContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();
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
			comboBoxEditSlideHeader.EnableSelectAll();
			comboBoxEditAdvertiser.EnableSelectAll();
			comboBoxEditDecisionMaker.EnableSelectAll();

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(slideContainer.DashboardInfo.CoverLists.Headers);

			_users.Clear();
			_users.AddRange(slideContainer.DashboardInfo.UsersList.GetUsersByStation(MasterWizardManager.Instance.SelectedWizard.Name));
			comboBoxEditSalesRep.Properties.Items.Clear();
			comboBoxEditSalesRep.Properties.Items.AddRange(_users.Select(it => it.FullName).ToArray());

			checkEditSolutionNew.EditValueChanged += EditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;
			checkEditSolutionNew.Checked = SlideContainer.EditedContent.CoverState.IsNewSolution;
			if (string.IsNullOrEmpty(SlideContainer.EditedContent.CoverState.SlideHeader))
			{
				if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
					comboBoxEditSlideHeader.SelectedIndex = 0;
			}
			else
			{
				var index = comboBoxEditSlideHeader.Properties.Items.IndexOf(SlideContainer.EditedContent.CoverState.SlideHeader);
				comboBoxEditSlideHeader.SelectedIndex = index >= 0 ? index : 0;
			}
			comboBoxEditAdvertiser.EditValue = String.IsNullOrEmpty(SlideContainer.EditedContent.CoverState.Advertiser) ? null : SlideContainer.EditedContent.CoverState.Advertiser;
			comboBoxEditDecisionMaker.EditValue = String.IsNullOrEmpty(SlideContainer.EditedContent.CoverState.DecisionMaker) ? null : SlideContainer.EditedContent.CoverState.DecisionMaker;

			checkEditFirstSlide.Checked = SlideContainer.EditedContent.CoverState.AddAsPageOne;
			checkEditPresentationDate.Checked = SlideContainer.EditedContent.CoverState.ShowPresentationDate;
			dateEditPresentationDate.Enabled = checkEditPresentationDate.Checked;
			if (checkEditPresentationDate.Checked)
				dateEditPresentationDate.EditValue = SlideContainer.EditedContent.CoverState.PresentationDate != DateTime.MinValue ? (object)SlideContainer.EditedContent.CoverState.PresentationDate : null;
			else
				dateEditPresentationDate.EditValue = null;
			checkEditUseEmptyCover.Checked = SlideContainer.EditedContent.CoverState.UseGenericCover;
			comboBoxEditAdvertiser.Enabled = !checkEditUseEmptyCover.Checked;
			comboBoxEditDecisionMaker.Enabled = !checkEditUseEmptyCover.Checked;
			comboBoxEditSlideHeader.Enabled = !checkEditUseEmptyCover.Checked;
			buttonXSalesQuote.Enabled = !checkEditUseEmptyCover.Checked;
			dateEditPresentationDate.Enabled = !checkEditUseEmptyCover.Checked && checkEditPresentationDate.Checked;
			checkEditPresentationDate.Enabled = !checkEditUseEmptyCover.Checked;
			checkEditSalesRep.Checked = !String.IsNullOrEmpty(SlideContainer.EditedContent.CoverState.SalesRep);
			checkEditSalesRep.Enabled = !checkEditUseEmptyCover.Checked;
			comboBoxEditSalesRep.Enabled = !checkEditUseEmptyCover.Checked && checkEditSalesRep.Checked;
			comboBoxEditSalesRep.EditValue = SlideContainer.EditedContent.CoverState.SalesRep;

			if (SlideContainer.EditedContent.CoverState.Quote.IsSet)
			{
				textEditSalesQuoteAuthor.EditValue = SlideContainer.EditedContent.CoverState.Quote.Author;
				memoEditSalesQuote.EditValue = SlideContainer.EditedContent.CoverState.Quote.Text;
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
		}

		public override void ApplyChanges()
		{
			SlideContainer.EditedContent.CoverState.IsNewSolution = checkEditSolutionNew.Checked;
			SlideContainer.EditedContent.CoverState.SlideHeader = comboBoxEditSlideHeader.EditValue as String;
			SlideContainer.EditedContent.CoverState.AddAsPageOne = checkEditFirstSlide.Checked;
			SlideContainer.EditedContent.CoverState.ShowPresentationDate = checkEditPresentationDate.Checked;
			SlideContainer.EditedContent.CoverState.PresentationDate = dateEditPresentationDate.DateTime;
			SlideContainer.EditedContent.CoverState.UseGenericCover = checkEditUseEmptyCover.Checked;
			SlideContainer.EditedContent.CoverState.Quote.Author = textEditSalesQuoteAuthor.EditValue as String;
			SlideContainer.EditedContent.CoverState.Quote.Text = memoEditSalesQuote.EditValue as String;
			SlideContainer.EditedContent.CoverState.Advertiser = comboBoxEditAdvertiser.EditValue as String;
			SlideContainer.EditedContent.CoverState.DecisionMaker = comboBoxEditDecisionMaker.EditValue as String;
			SlideContainer.EditedContent.CoverState.SalesRep = comboBoxEditSalesRep.EditValue as String;

			Business.Common.Dictionaries.ListManager.Instance.Advertisers.Add(Advertiser);
			Business.Common.Dictionaries.ListManager.Instance.Advertisers.Save();

			Business.Common.Dictionaries.ListManager.Instance.DecisionMakers.Add(DecisionMaker);
			Business.Common.Dictionaries.ListManager.Instance.DecisionMakers.Save();
		}

		private void EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			SlideContainer.RaiseDataChanged();
		}

		private void checkEditSalesRep_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			comboBoxEditSalesRep.Enabled = checkEditSalesRep.Checked;
			if (!checkEditSalesRep.Checked)
				comboBoxEditSalesRep.EditValue = null;
			SlideContainer.RaiseDataChanged();
		}

		private void comboBoxEditSalesRep_EditValueChanged(object sender, EventArgs e)
		{
			var user = _users.FirstOrDefault(u => u.FullName.Equals(comboBoxEditSalesRep.EditValue as String));
			laSalesRepDetails.Text = user != null ? String.Format("{0}{2}{1}", user.Phone, user.Email, Environment.NewLine) : String.Empty;
			if (!_allowToSave) return;
			SlideContainer.RaiseDataChanged();
		}

		private void ckPresentationDate_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			dateEditPresentationDate.Enabled = checkEditPresentationDate.Checked;
			if (!checkEditPresentationDate.Checked)
				dateEditPresentationDate.EditValue = null;
			SlideContainer.RaiseDataChanged();
		}

		private void buttonXSalesQuote_Click(object sender, EventArgs e)
		{
			if (textEditSalesQuoteAuthor.EditValue == null && memoEditSalesQuote.EditValue == null)
			{
				using (var form = new FormQuotes(SlideContainer.DashboardInfo.CoverLists.Quotes))
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
			SlideContainer.RaiseDataChanged();
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
			SlideContainer.RaiseDataChanged();
		}

		private void checkEdit_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
				SlideContainer.RaiseDataChanged();
		}

		#region Output Staff

		public override bool ReadyForOutput =>
			!String.IsNullOrEmpty(comboBoxEditAdvertiser.EditValue as String) || checkEditUseEmptyCover.Checked;

		public string Title => (comboBoxEditSlideHeader.EditValue as String) ?? String.Empty;

		public string Advertiser => (comboBoxEditAdvertiser.EditValue as String) ?? String.Empty;

		public string DecisionMaker => (comboBoxEditDecisionMaker.EditValue as String) ?? String.Empty;

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

		public string PresentationDate => dateEditPresentationDate.EditValue != null ? dateEditPresentationDate.DateTime.ToString("MMMM d, yyyy") : String.Empty;

		public string Quote => (memoEditSalesQuote.EditValue?.ToString() ?? string.Empty)
							   + (char)13 + (textEditSalesQuoteAuthor.EditValue?.ToString() ?? string.Empty);

		public override void GenerateOutput()
		{
			//SaveChanges();
			//FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			//FormProgress.ShowProgress();
			//if (checkEditUseEmptyCover.Checked)
			//	AppManager.Instance.ShowFloater(() =>
			//	{
			//		DashboardPowerPointHelper.Instance.AppendGenericCover(checkEditFirstSlide.Checked);
			//		FormProgress.CloseProgress();
			//	});

			//else
			//	AppManager.Instance.ShowFloater(() =>
			//	{
			//		DashboardPowerPointHelper.Instance.AppendCover(checkEditFirstSlide.Checked);
			//		FormProgress.CloseProgress();
			//	});
		}

		public override PreviewGroup GeneratePreview()
		{
			throw new NotImplementedException();
			//SaveChanges();
			//FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			//FormProgress.ShowProgress();
			//var tempFileName = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			//if (checkEditUseEmptyCover.Checked)
			//	DashboardPowerPointHelper.Instance.PrepareGenericCover(tempFileName);
			//else
			//	DashboardPowerPointHelper.Instance.PrepareCover(tempFileName);
			//Utilities.ActivateForm(FormMain.Instance.Handle, false, false);
			//FormProgress.CloseProgress();
			//if (!File.Exists(tempFileName)) return;
			//using (var formPreview = new FormPreview(FormMain.Instance, DashboardPowerPointHelper.Instance, AppManager.Instance.HelpManager, AppManager.Instance.ShowFloater))
			//{
			//	formPreview.Text = "Preview Slides";
			//	formPreview.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName, InsertOnTop = checkEditFirstSlide.Checked } });
			//	RegistryHelper.MainFormHandle = formPreview.Handle;
			//	RegistryHelper.MaximizeMainForm = false;
			//	var previewResult = formPreview.ShowDialog();
			//	RegistryHelper.MaximizeMainForm = false;
			//	RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
			//	if (previewResult != DialogResult.OK)
			//		AppManager.Instance.ActivateMainForm();
			//}
		}
		#endregion
	}
}