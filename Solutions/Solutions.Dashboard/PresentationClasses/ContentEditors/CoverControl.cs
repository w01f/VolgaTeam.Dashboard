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
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Dashboard.InteropClasses;
using Asa.Solutions.Dashboard.PresentationClasses.Output;
using Asa.Solutions.Dashboard.Properties;

namespace Asa.Solutions.Dashboard.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class CoverControl : DashboardSlideControl, ICoverOutputData, IDashboardSlide
	{
		private bool _allowToSave;
		private readonly List<User> _users = new List<User>();

		public override SlideType SlideType => SlideType.Cover;
		public string SlideName => "A. Cover";

		public CoverControl(BaseDashboardContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();
			Text = SlideName;
			if ((CreateGraphics()).DpiX > 96)
			{
				laAdvertiser.Font = new Font(laAdvertiser.Font.FontFamily, laAdvertiser.Font.Size - 2, laAdvertiser.Font.Style);
				laDecisionMaker.Font = new Font(laDecisionMaker.Font.FontFamily, laDecisionMaker.Font.Size - 2, laDecisionMaker.Font.Style);
				checkEditPresentationDate.Font = new Font(checkEditPresentationDate.Font.FontFamily, checkEditPresentationDate.Font.Size - 2, checkEditPresentationDate.Font.Style);
				checkEditSalesRep.Font = new Font(checkEditSalesRep.Font.FontFamily, checkEditSalesRep.Font.Size - 2, checkEditSalesRep.Font.Style);
				buttonXSalesQuote.Font = new Font(buttonXSalesQuote.Font.FontFamily, buttonXSalesQuote.Font.Size - 2, buttonXSalesQuote.Font.Style);
				textEditSalesQuoteAuthor.Font = new Font(textEditSalesQuoteAuthor.Font.FontFamily, textEditSalesQuoteAuthor.Font.Size - 2, textEditSalesQuoteAuthor.Font.Style);
				memoEditSalesQuote.Font = new Font(memoEditSalesQuote.Font.FontFamily, memoEditSalesQuote.Font.Size - 2, memoEditSalesQuote.Font.Style);
				laSalesRepEmail.Font = new Font(laSalesRepEmail.Font.FontFamily, laSalesRepEmail.Font.Size - 2, laSalesRepEmail.Font.Style);
				laSalesRepPhone.Font = new Font(laSalesRepPhone.Font.FontFamily, laSalesRepPhone.Font.Size - 2, laSalesRepPhone.Font.Style);
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

			pbSplash.Image = SlideContainer.DashboardInfo.CoverSplashLogo;
		}

		public override void LoadData()
		{
			_allowToSave = false;
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

			checkEditPresentationDate.Checked = SlideContainer.EditedContent.CoverState.ShowPresentationDate;
			dateEditPresentationDate.Enabled = checkEditPresentationDate.Checked;
			if (checkEditPresentationDate.Checked)
				dateEditPresentationDate.EditValue = SlideContainer.EditedContent.CoverState.PresentationDate != DateTime.MinValue ? (object)SlideContainer.EditedContent.CoverState.PresentationDate : null;
			else
				dateEditPresentationDate.EditValue = null;
			dateEditPresentationDate.Enabled = checkEditPresentationDate.Checked;
			checkEditSalesRep.Checked = !String.IsNullOrEmpty(SlideContainer.EditedContent.CoverState.SalesRep);
			comboBoxEditSalesRep.Enabled = checkEditSalesRep.Checked;
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
			SlideContainer.EditedContent.CoverState.SlideHeader = comboBoxEditSlideHeader.EditValue as String;
			SlideContainer.EditedContent.CoverState.ShowPresentationDate = checkEditPresentationDate.Checked;
			SlideContainer.EditedContent.CoverState.PresentationDate = dateEditPresentationDate.DateTime;
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
			laSalesRepEmail.Text = user?.Email;
			laSalesRepPhone.Text = user?.Phone;
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

		#region Output Staff

		public override bool ReadyForOutput => !String.IsNullOrEmpty(comboBoxEditAdvertiser.EditValue as String);

		public Theme SelectedTheme => SlideContainer.GetSelectedTheme(SlideType.Cover);

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

		public void GenerateOutput()
		{
			SolutionDashboardPowerPointHelper.Instance.AppendCover(this);
		}

		public PreviewGroup GeneratePreview()
		{
			var tempFileName = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			SolutionDashboardPowerPointHelper.Instance.PrepareCover(this, tempFileName);
			return new PreviewGroup { Name = SlideName, PresentationSourcePath = tempFileName };
		}
		#endregion
	}
}