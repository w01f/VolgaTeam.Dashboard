using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Dashboard.Dictionaries;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Dashboard.InteropClasses;
using Asa.Solutions.Dashboard.PresentationClasses.Output;
using Asa.Solutions.Dashboard.Properties;
using DevExpress.Skins;
using DevExpress.XtraLayout.Utils;

namespace Asa.Solutions.Dashboard.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class CoverControl : DashboardSlideControl, ICoverOutputData, IDashboardSlide
	{
		private bool _allowToSave;
		private readonly List<User> _users = new List<User>();

		public override SlideType SlideType => SlideType.Cover;
		public string SlideName => SlideContainer.DashboardInfo.CoverTitle;

		public CoverControl(BaseDashboardContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();
			Text = SlideName;

			comboBoxEditSlideHeader.EnableSelectAll();
			comboBoxEditAdvertiser.EnableSelectAll();
			comboBoxEditDecisionMaker.EnableSelectAll();

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(slideContainer.DashboardInfo.CoverLists.Headers);

			_users.Clear();
			_users.AddRange(slideContainer.DashboardInfo.UsersList.GetUsersByStation(MasterWizardManager.Instance.SelectedWizard.Name));
			comboBoxEditSalesRep.Properties.Items.Clear();
			comboBoxEditSalesRep.Properties.Items.AddRange(_users.Select(it => it.FullName).ToArray());

			pictureEditSplash.Image = SlideContainer.DashboardInfo.CoverSplashLogo;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControl.MaximumSize = RectangleHelper.ScaleSize(layoutControl.MaximumSize, scaleFactor);
			layoutControl.MinimumSize = RectangleHelper.ScaleSize(layoutControl.MinimumSize, scaleFactor);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);
			layoutControlItemAdvertiserLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAdvertiserLogo.MaxSize, scaleFactor);
			layoutControlItemAdvertiserLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemAdvertiserLogo.MinSize, scaleFactor);
			layoutControlItemAdvertiserValue.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAdvertiserValue.MaxSize, scaleFactor);
			layoutControlItemAdvertiserValue.MinSize = RectangleHelper.ScaleSize(layoutControlItemAdvertiserValue.MinSize, scaleFactor);
			layoutControlItemDecisionMakerLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDecisionMakerLogo.MaxSize, scaleFactor);
			layoutControlItemDecisionMakerLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemDecisionMakerLogo.MinSize, scaleFactor);
			layoutControlItemDecisionMakerValue.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDecisionMakerValue.MaxSize, scaleFactor);
			layoutControlItemDecisionMakerValue.MinSize = RectangleHelper.ScaleSize(layoutControlItemDecisionMakerValue.MinSize, scaleFactor);
			layoutControlItemSalesRepLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSalesRepLogo.MaxSize, scaleFactor);
			layoutControlItemSalesRepLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemSalesRepLogo.MinSize, scaleFactor);
			layoutControlItemSalesRepToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSalesRepToggle.MaxSize, scaleFactor);
			layoutControlItemSalesRepToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemSalesRepToggle.MinSize, scaleFactor);
			layoutControlItemSalesRepValue.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSalesRepValue.MaxSize, scaleFactor);
			layoutControlItemSalesRepValue.MinSize = RectangleHelper.ScaleSize(layoutControlItemSalesRepValue.MinSize, scaleFactor);
			simpleLabelItemSalesRepEmail.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemSalesRepEmail.MaxSize, scaleFactor);
			simpleLabelItemSalesRepEmail.MinSize = RectangleHelper.ScaleSize(simpleLabelItemSalesRepEmail.MinSize, scaleFactor);
			simpleLabelItemSalesRepPhone.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemSalesRepPhone.MaxSize, scaleFactor);
			simpleLabelItemSalesRepPhone.MinSize = RectangleHelper.ScaleSize(simpleLabelItemSalesRepPhone.MinSize, scaleFactor);
			layoutControlItemDateLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDateLogo.MaxSize, scaleFactor);
			layoutControlItemDateLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemDateLogo.MinSize, scaleFactor);
			layoutControlItemDateValue.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDateValue.MaxSize, scaleFactor);
			layoutControlItemDateValue.MinSize = RectangleHelper.ScaleSize(layoutControlItemDateValue.MinSize, scaleFactor);
			layoutControlItemSalesQuoteButton.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSalesQuoteButton.MaxSize, scaleFactor);
			layoutControlItemSalesQuoteButton.MinSize = RectangleHelper.ScaleSize(layoutControlItemSalesQuoteButton.MinSize, scaleFactor);
			simpleLabelItemSalesQuoteDefault.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemSalesQuoteDefault.MaxSize, scaleFactor);
			simpleLabelItemSalesQuoteDefault.MinSize = RectangleHelper.ScaleSize(simpleLabelItemSalesQuoteDefault.MinSize, scaleFactor);
			simpleLabelItemSalesQuoteAuthor.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemSalesQuoteAuthor.MaxSize, scaleFactor);
			simpleLabelItemSalesQuoteAuthor.MinSize = RectangleHelper.ScaleSize(simpleLabelItemSalesQuoteAuthor.MinSize, scaleFactor);
			simpleLabelItemSalesQuoteValue.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemSalesQuoteValue.MaxSize, scaleFactor);
			simpleLabelItemSalesQuoteValue.MinSize = RectangleHelper.ScaleSize(simpleLabelItemSalesQuoteValue.MinSize, scaleFactor);
		}

		public override void LoadData()
		{
			_allowToSave = false;
			if (String.IsNullOrEmpty(SlideContainer.EditedContent.CoverState.SlideHeader))
			{
				if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
					comboBoxEditSlideHeader.SelectedIndex = 0;
			}
			else
				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.CoverState.SlideHeader;

			comboBoxEditAdvertiser.EditValue = SlideContainer.EditedContent.CoverState.Advertiser ?? SlideContainer.EditedContent.ScheduleSettings.BusinessName;
			comboBoxEditDecisionMaker.EditValue = SlideContainer.EditedContent.CoverState.DecisionMaker ?? SlideContainer.EditedContent.ScheduleSettings.DecisionMaker;

			checkEditPresentationDate.Checked = SlideContainer.EditedContent.CoverState.ShowPresentationDate;
			dateEditPresentationDate.Enabled = checkEditPresentationDate.Checked;
			if (checkEditPresentationDate.Checked)
				dateEditPresentationDate.EditValue = SlideContainer.EditedContent.CoverState.PresentationDate != DateTime.MinValue ?
					SlideContainer.EditedContent.CoverState.PresentationDate :
					SlideContainer.EditedContent.ScheduleSettings.PresentationDate;
			else
				dateEditPresentationDate.EditValue = null;
			dateEditPresentationDate.Enabled = checkEditPresentationDate.Checked;
			checkEditSalesRep.Checked = !String.IsNullOrEmpty(SlideContainer.SettingsContainer.SalesRep);
			comboBoxEditSalesRep.Enabled = checkEditSalesRep.Checked;
			comboBoxEditSalesRep.EditValue = SlideContainer.SettingsContainer.SalesRep;
			checkEditAddAsPageOne.Checked = SlideContainer.EditedContent.CoverState.AddAsPageOne;

			if (SlideContainer.EditedContent.CoverState.Quote.IsSet)
			{
				simpleLabelItemSalesQuoteAuthor.CustomizationFormText = SlideContainer.EditedContent.CoverState.Quote.Author;
				simpleLabelItemSalesQuoteValue.CustomizationFormText = SlideContainer.EditedContent.CoverState.Quote.Text;
				simpleLabelItemSalesQuoteAuthor.Text = String.Format("<size=+2><b>{0}</b></size>", SlideContainer.EditedContent.CoverState.Quote.Author);
				simpleLabelItemSalesQuoteValue.Text = String.Format("<size=+1><b><i>{0}</i></b></size>", SlideContainer.EditedContent.CoverState.Quote.Text);
				layoutControlGroupSalesQuoteDefault.Visibility = LayoutVisibility.Never;
				layoutControlGroupSalesQuoteValue.Visibility = LayoutVisibility.Always;
				buttonXSalesQuote.Image = null;
				buttonXSalesQuote.Text = "Remove";
			}
			else
			{
				simpleLabelItemSalesQuoteAuthor.CustomizationFormText = " ";
				simpleLabelItemSalesQuoteValue.CustomizationFormText = " ";
				simpleLabelItemSalesQuoteAuthor.Text = " ";
				simpleLabelItemSalesQuoteValue.Text = " ";
				layoutControlGroupSalesQuoteValue.Visibility = LayoutVisibility.Never;
				layoutControlGroupSalesQuoteDefault.Visibility = LayoutVisibility.Always;
				buttonXSalesQuote.Image = Resources.SalesQuotes;
				buttonXSalesQuote.Text = String.Empty;
			}

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			SlideContainer.EditedContent.CoverState.SlideHeader = comboBoxEditSlideHeader.EditValue as String;
			SlideContainer.EditedContent.CoverState.ShowPresentationDate = checkEditPresentationDate.Checked;
			SlideContainer.EditedContent.CoverState.PresentationDate = dateEditPresentationDate.DateTime;
			SlideContainer.EditedContent.CoverState.Quote.Author = simpleLabelItemSalesQuoteAuthor.CustomizationFormText;
			SlideContainer.EditedContent.CoverState.Quote.Text = simpleLabelItemSalesQuoteValue.CustomizationFormText;
			SlideContainer.EditedContent.CoverState.Advertiser = comboBoxEditAdvertiser.EditValue as String;
			SlideContainer.EditedContent.CoverState.DecisionMaker = comboBoxEditDecisionMaker.EditValue as String;
			SlideContainer.EditedContent.CoverState.AddAsPageOne = checkEditAddAsPageOne.Checked;

			Business.Common.Dictionaries.ListManager.Instance.Advertisers.Add(Advertiser);
			Business.Common.Dictionaries.ListManager.Instance.Advertisers.Save();

			Business.Common.Dictionaries.ListManager.Instance.DecisionMakers.Add(DecisionMaker);
			Business.Common.Dictionaries.ListManager.Instance.DecisionMakers.Save();

			SlideContainer.SettingsContainer.SalesRep = comboBoxEditSalesRep.EditValue as String;
			SlideContainer.SettingsContainer.SaveSettings();
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			SlideContainer.RaiseDataChanged();
		}

		private void OnSalesRepCheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemSalesRepValue.Enabled = checkEditSalesRep.Checked;
			simpleLabelItemSalesRepEmail.Enabled = checkEditSalesRep.Checked;
			simpleLabelItemSalesRepPhone.Enabled = checkEditSalesRep.Checked;
			if (!checkEditSalesRep.Checked)
				comboBoxEditSalesRep.EditValue = null;
			OnEditValueChanged(sender, e);
		}

		private void OnSalesRepEditValueChanged(object sender, EventArgs e)
		{
			var user = _users.FirstOrDefault(u => u.FullName.Equals(comboBoxEditSalesRep.EditValue as String));
			simpleLabelItemSalesRepEmail.Text = user?.Email ?? " ";
			simpleLabelItemSalesRepPhone.Text = user?.Phone ?? " ";
			OnEditValueChanged(sender, e);
		}

		private void OnDateCheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemDateValue.Enabled = checkEditPresentationDate.Checked;
			if (!checkEditPresentationDate.Checked)
				dateEditPresentationDate.EditValue = null;
			OnEditValueChanged(sender, e);
		}

		private void OnSalesQuoteClick(object sender, EventArgs e)
		{
			if (simpleLabelItemSalesQuoteAuthor.Text == " " && simpleLabelItemSalesQuoteValue.Text == " ")
			{
				using (var form = new FormQuotes(SlideContainer.DashboardInfo.CoverLists.Quotes))
				{
					if (form.ShowDialog() != DialogResult.OK) return;
					if (form.SelectedQuote == null) return;

					simpleLabelItemSalesQuoteAuthor.CustomizationFormText = form.SelectedQuote.Author;
					simpleLabelItemSalesQuoteValue.CustomizationFormText = "\"" + form.SelectedQuote.Text + "\"";
					simpleLabelItemSalesQuoteAuthor.Text = String.Format("<size=+2><b>{0}</b></size>", form.SelectedQuote.Author);
					simpleLabelItemSalesQuoteValue.Text = String.Format("<size=+1><b><i>{0}</i></b></size>", "\"" + form.SelectedQuote.Text + "\"");
					layoutControlGroupSalesQuoteDefault.Visibility = LayoutVisibility.Never;
					layoutControlGroupSalesQuoteValue.Visibility = LayoutVisibility.Always;
					buttonXSalesQuote.Image = null;
					buttonXSalesQuote.Text = "Remove";
				}
			}
			else
			{
				simpleLabelItemSalesQuoteAuthor.CustomizationFormText = " ";
				simpleLabelItemSalesQuoteValue.CustomizationFormText = " ";
				simpleLabelItemSalesQuoteAuthor.Text = " ";
				simpleLabelItemSalesQuoteValue.Text = " ";
				layoutControlGroupSalesQuoteValue.Visibility = LayoutVisibility.Never;
				layoutControlGroupSalesQuoteDefault.Visibility = LayoutVisibility.Always;
				buttonXSalesQuote.Image = Resources.SalesQuotes;
				buttonXSalesQuote.Text = String.Empty;
			}

			OnEditValueChanged(sender, e);
		}

		#region Output Staff

		public override bool ReadyForOutput => !String.IsNullOrEmpty(comboBoxEditAdvertiser.EditValue as String);

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

		public bool AddAsPageOne => checkEditAddAsPageOne.Checked;

		public string PresentationDate => dateEditPresentationDate.EditValue != null ? dateEditPresentationDate.DateTime.ToString("MMMM d, yyyy") : String.Empty;

		public string Quote => simpleLabelItemSalesQuoteValue.CustomizationFormText
							   + (char)13 + simpleLabelItemSalesQuoteValue.CustomizationFormText;

		public void GenerateOutput()
		{
			SlideContainer.PowerPointProcessor.AppendCover(this);
		}

		public PreviewGroup GeneratePreview()
		{
			var tempFileName = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			SlideContainer.PowerPointProcessor.PrepareCover(this, tempFileName);
			return new PreviewGroup { Name = SlideName, PresentationSourcePath = tempFileName };
		}
		#endregion
	}
}