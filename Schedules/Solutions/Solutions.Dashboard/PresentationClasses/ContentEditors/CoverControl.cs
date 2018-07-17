using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Dictionaries;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Dashboard.InteropClasses;
using Asa.Solutions.Dashboard.PresentationClasses.Output;
using DevExpress.Skins;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout.Utils;

namespace Asa.Solutions.Dashboard.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class CoverControl : DashboardSlideControl, ICoverOutputData, IDashboardSlide
	{
		private bool _allowToSave;
		private readonly List<User> _users = new List<User>();

		public override SlideType SlideType => SlideType.Cover;
		public override string ControlName => SlideContainer.DashboardInfo.CoverTitle;

		public CoverControl(BaseDashboardContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();
			Text = ControlName;

			comboBoxEditSlideHeader.EnableSelectAll();
			comboBoxEditAdvertiser.EnableSelectAll();
			comboBoxEditDecisionMaker.EnableSelectAll();

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(slideContainer.DashboardInfo.CoverLists.Headers.Select(item => item.Value).ToArray());

			_users.Clear();
			_users.AddRange(slideContainer.DashboardInfo.UsersList.GetUsersByStation(MasterWizardManager.Instance.SelectedWizard.Name));
			comboBoxEditSalesRep.Properties.Items.Clear();
			comboBoxEditSalesRep.Properties.Items.AddRange(_users);

			pictureEditSplash.Image = SlideContainer.DashboardInfo.CoverSplashLogo;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControl.MaximumSize = RectangleHelper.ScaleSize(layoutControl.MaximumSize, scaleFactor);
			layoutControl.MinimumSize = RectangleHelper.ScaleSize(layoutControl.MinimumSize, scaleFactor);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);
			emptySpaceItemAdvertiserLeft.MaxSize = RectangleHelper.ScaleSize(emptySpaceItemAdvertiserLeft.MaxSize, scaleFactor);
			emptySpaceItemAdvertiserLeft.MinSize = RectangleHelper.ScaleSize(emptySpaceItemAdvertiserLeft.MinSize, scaleFactor);
			layoutControlItemAdvertiserLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAdvertiserLogo.MaxSize, scaleFactor);
			layoutControlItemAdvertiserLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemAdvertiserLogo.MinSize, scaleFactor);
			simpleLabelItemAdvertiserTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemAdvertiserTitle.MaxSize, scaleFactor);
			simpleLabelItemAdvertiserTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemAdvertiserTitle.MinSize, scaleFactor);
			layoutControlItemAdvertiserValue.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAdvertiserValue.MaxSize, scaleFactor);
			layoutControlItemAdvertiserValue.MinSize = RectangleHelper.ScaleSize(layoutControlItemAdvertiserValue.MinSize, scaleFactor);
			emptySpaceItemAdvertiserUnder.MaxSize = RectangleHelper.ScaleSize(emptySpaceItemAdvertiserUnder.MaxSize, scaleFactor);
			emptySpaceItemAdvertiserUnder.MinSize = RectangleHelper.ScaleSize(emptySpaceItemAdvertiserUnder.MinSize, scaleFactor);
			emptySpaceItemDecisionMakerLeft.MaxSize = RectangleHelper.ScaleSize(emptySpaceItemDecisionMakerLeft.MaxSize, scaleFactor);
			emptySpaceItemDecisionMakerLeft.MinSize = RectangleHelper.ScaleSize(emptySpaceItemDecisionMakerLeft.MinSize, scaleFactor);
			layoutControlItemDecisionMakerLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDecisionMakerLogo.MaxSize, scaleFactor);
			layoutControlItemDecisionMakerLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemDecisionMakerLogo.MinSize, scaleFactor);
			simpleLabelItemDecisionMakerTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemDecisionMakerTitle.MaxSize, scaleFactor);
			simpleLabelItemDecisionMakerTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemDecisionMakerTitle.MinSize, scaleFactor);
			layoutControlItemDecisionMakerValue.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDecisionMakerValue.MaxSize, scaleFactor);
			layoutControlItemDecisionMakerValue.MinSize = RectangleHelper.ScaleSize(layoutControlItemDecisionMakerValue.MinSize, scaleFactor);
			emptySpaceItemDecisionmakerUnder.MaxSize = RectangleHelper.ScaleSize(emptySpaceItemDecisionmakerUnder.MaxSize, scaleFactor);
			emptySpaceItemDecisionmakerUnder.MinSize = RectangleHelper.ScaleSize(emptySpaceItemDecisionmakerUnder.MinSize, scaleFactor);
			layoutControlItemSalesRepToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSalesRepToggle.MaxSize, scaleFactor);
			layoutControlItemSalesRepToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemSalesRepToggle.MinSize, scaleFactor);
			layoutControlItemSalesRepLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSalesRepLogo.MaxSize, scaleFactor);
			layoutControlItemSalesRepLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemSalesRepLogo.MinSize, scaleFactor);
			simpleLabelItemSalesRepTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemSalesRepTitle.MaxSize, scaleFactor);
			simpleLabelItemSalesRepTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemSalesRepTitle.MinSize, scaleFactor);
			layoutControlItemSalesRepValue.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSalesRepValue.MaxSize, scaleFactor);
			layoutControlItemSalesRepValue.MinSize = RectangleHelper.ScaleSize(layoutControlItemSalesRepValue.MinSize, scaleFactor);
			simpleLabelItemSalesRepDescription.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemSalesRepDescription.MaxSize, scaleFactor);
			simpleLabelItemSalesRepDescription.MinSize = RectangleHelper.ScaleSize(simpleLabelItemSalesRepDescription.MinSize, scaleFactor);
			emptySpaceItemSalesRepValueUnder.MaxSize = RectangleHelper.ScaleSize(emptySpaceItemSalesRepValueUnder.MaxSize, scaleFactor);
			emptySpaceItemSalesRepValueUnder.MinSize = RectangleHelper.ScaleSize(emptySpaceItemSalesRepValueUnder.MinSize, scaleFactor);
			layoutControlItemDateToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDateToggle.MaxSize, scaleFactor);
			layoutControlItemDateToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemDateToggle.MinSize, scaleFactor);
			layoutControlItemDateLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDateLogo.MaxSize, scaleFactor);
			layoutControlItemDateLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemDateLogo.MinSize, scaleFactor);
			simpleLabelItemDateTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemDateTitle.MaxSize, scaleFactor);
			simpleLabelItemDateTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemDateTitle.MinSize, scaleFactor);
			layoutControlItemDateValue.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDateValue.MaxSize, scaleFactor);
			layoutControlItemDateValue.MinSize = RectangleHelper.ScaleSize(layoutControlItemDateValue.MinSize, scaleFactor);
			emptySpaceItemDateUnder.MaxSize = RectangleHelper.ScaleSize(emptySpaceItemDateUnder.MaxSize, scaleFactor);
			emptySpaceItemDateUnder.MinSize = RectangleHelper.ScaleSize(emptySpaceItemDateUnder.MinSize, scaleFactor);
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
			comboBoxEditSalesRep.EditValue = _users.FirstOrDefault(user => user.FullName == SlideContainer.SettingsContainer.SalesRep);
			checkEditAddAsPageOne.Checked = SlideContainer.EditedContent.CoverState.AddAsPageOne;

			if (SlideContainer.EditedContent.CoverState.Quote.IsSet)
			{
				simpleLabelItemSalesQuoteAuthor.CustomizationFormText = SlideContainer.EditedContent.CoverState.Quote.Author;
				simpleLabelItemSalesQuoteValue.CustomizationFormText = SlideContainer.EditedContent.CoverState.Quote.Text;
				simpleLabelItemSalesQuoteAuthor.Text = String.Format("<size=+2><b>{0}</b></size>", SlideContainer.EditedContent.CoverState.Quote.Author);
				simpleLabelItemSalesQuoteValue.Text = String.Format("<size=+1><b><i>{0}</i></b></size>", SlideContainer.EditedContent.CoverState.Quote.Text);
				emptySpaceItemSalesQuoteDefault.Visibility = LayoutVisibility.Never;
				layoutControlGroupSalesQuoteValue.Visibility = LayoutVisibility.Always;
				hyperLinkEditResetProductName.Text = String.Format("<size=+2><color=red><i><u>{0}</u></i></color></size>", "Remove Sales Quote");
			}
			else
			{
				simpleLabelItemSalesQuoteAuthor.CustomizationFormText = " ";
				simpleLabelItemSalesQuoteValue.CustomizationFormText = " ";
				simpleLabelItemSalesQuoteAuthor.Text = " ";
				simpleLabelItemSalesQuoteValue.Text = " ";
				layoutControlGroupSalesQuoteValue.Visibility = LayoutVisibility.Never;
				emptySpaceItemSalesQuoteDefault.Visibility = LayoutVisibility.Always;
				hyperLinkEditResetProductName.Text = String.Format("<size=+2><color={1}><i><u>{0}</u></i></color></size>", "Add Sales Quote",
					SlideContainer.AccentColor.HasValue
						? SlideContainer.AccentColor.Value.ToHex()
						: "blue");
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

			SlideContainer.SettingsContainer.SalesRep = (comboBoxEditSalesRep.EditValue as User)?.FullName;
			SlideContainer.SettingsContainer.SaveSettings();
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			SlideContainer.RaiseDataChanged();
		}

		private void OnSalesRepCheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupSalesRep.Enabled = checkEditSalesRep.Checked;
			if (!checkEditSalesRep.Checked)
				comboBoxEditSalesRep.EditValue = null;
			OnEditValueChanged(sender, e);
		}

		private void OnSalesRepEditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditSalesRep.EditValue as User;
			simpleLabelItemSalesRepDescription.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
		}

		private void OnDateCheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupDate.Enabled = checkEditPresentationDate.Checked;
			if (!checkEditPresentationDate.Checked)
				dateEditPresentationDate.EditValue = null;
			OnEditValueChanged(sender, e);
		}

		private void OnSalesQuoteClick(object sender, OpenLinkEventArgs e)
		{
			e.Handled = true;
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
					emptySpaceItemSalesQuoteDefault.Visibility = LayoutVisibility.Never;
					layoutControlGroupSalesQuoteValue.Visibility = LayoutVisibility.Always;
					hyperLinkEditResetProductName.Text = String.Format("<size=+2><color=red><i><u>{0}</u></i></color></size>", "Remove Sales Quote");
				}
			}
			else
			{
				simpleLabelItemSalesQuoteAuthor.CustomizationFormText = " ";
				simpleLabelItemSalesQuoteValue.CustomizationFormText = " ";
				simpleLabelItemSalesQuoteAuthor.Text = " ";
				simpleLabelItemSalesQuoteValue.Text = " ";
				layoutControlGroupSalesQuoteValue.Visibility = LayoutVisibility.Never;
				emptySpaceItemSalesQuoteDefault.Visibility = LayoutVisibility.Always;
				hyperLinkEditResetProductName.Text = String.Format("<size=+2><color={1}><i><u>{0}</u></i></color></size>", "Add Sales Quote",
					SlideContainer.AccentColor.HasValue
						? SlideContainer.AccentColor.Value.ToHex()
						: "blue");
			}

			OnEditValueChanged(sender, e);
		}

		#region Output Staff

		public override bool ReadyForOutput => !String.IsNullOrEmpty(comboBoxEditAdvertiser.EditValue as String);

		public string Title => (comboBoxEditSlideHeader.EditValue as String) ?? String.Empty;

		public string Advertiser => (comboBoxEditAdvertiser.EditValue as String) ?? String.Empty;

		public string DecisionMaker => (comboBoxEditDecisionMaker.EditValue as String) ?? String.Empty;

		public string SalesRep => comboBoxEditSalesRep.EditValue is User user ?
			String.Join("          ", user.FullName, user.Email, user.Phone) :
			String.Empty;

		public bool AddAsPageOne => checkEditAddAsPageOne.Checked;

		public string PresentationDate => dateEditPresentationDate.EditValue != null ? dateEditPresentationDate.DateTime.ToString("MMMM d, yyyy") : String.Empty;

		public string Quote => simpleLabelItemSalesQuoteAuthor.CustomizationFormText
							   + (char)13 + simpleLabelItemSalesQuoteValue.CustomizationFormText;

		public OutputGroup GetOutputData()
		{
			return new OutputGroup
			{
				Name = ControlName,
				IsCurrent = SlideContainer.SelectedSlideType == SlideType,
				Items = new List<OutputItem>(new[]
				{
					new OutputItem
					{
						Name = ControlName,
						InsertOnTop = AddAsPageOne,
						PresentationSourcePath = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath,
							Path.GetFileName(Path.GetTempFileName())),
						SlidesCount = 1,
						IsCurrent = true,
						SlideGeneratingAction = (processor, destinationPresentation) =>
						{
							processor.AppendDashboardCover(this,destinationPresentation);
						},
						PreviewGeneratingAction = (processor, presentationSourcePath) =>
						{
							processor.PrepareDashboardCover(this, presentationSourcePath);
						}
					}
				})
			};
		}
		#endregion
	}
}