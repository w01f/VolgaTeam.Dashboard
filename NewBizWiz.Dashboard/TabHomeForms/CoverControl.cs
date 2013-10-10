using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Dashboard;
using NewBizWiz.Dashboard.InteropClasses;
using NewBizWiz.Dashboard.ToolForms;
using ListManager = NewBizWiz.Core.Dashboard.ListManager;
using SettingsManager = NewBizWiz.Core.Dashboard.SettingsManager;

namespace NewBizWiz.Dashboard.TabHomeForms
{
	[ToolboxItem(false)]
	public partial class CoverControl : UserControl
	{
		private static CoverControl _instance;
		private bool _allowToSave;
		private BindingList<User> _users;

		private CoverControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AppManager.Instance.SetClickEventHandler(this);
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 3, laTitle.Font.Style);
				laAdvertiser.Font = new Font(laAdvertiser.Font.FontFamily, laAdvertiser.Font.Size - 2, laAdvertiser.Font.Style);
				laDecisionMaker.Font = new Font(laDecisionMaker.Font.FontFamily, laDecisionMaker.Font.Size - 2, laDecisionMaker.Font.Style);
				laSalesRepEmail.Font = new Font(laSalesRepEmail.Font.FontFamily, laSalesRepEmail.Font.Size - 2, laSalesRepEmail.Font.Style);
				laSalesRepPhone.Font = new Font(laSalesRepPhone.Font.FontFamily, laSalesRepPhone.Font.Size - 2, laSalesRepPhone.Font.Style);
				laSlideHeader.Font = new Font(laSlideHeader.Font.FontFamily, laSlideHeader.Font.Size - 2, laSlideHeader.Font.Style);
				checkEditFirstSlide.Font = new Font(checkEditFirstSlide.Font.FontFamily, checkEditFirstSlide.Font.Size - 2, checkEditFirstSlide.Font.Style);
				checkEditPresentationDate.Font = new Font(checkEditPresentationDate.Font.FontFamily, checkEditPresentationDate.Font.Size - 2, checkEditPresentationDate.Font.Style);
				checkEditSalesRep.Font = new Font(checkEditSalesRep.Font.FontFamily, checkEditSalesRep.Font.Size - 2, checkEditSalesRep.Font.Style);
				checkEditUseEmptyCover.Font = new Font(checkEditUseEmptyCover.Font.FontFamily, checkEditUseEmptyCover.Font.Size - 2, checkEditUseEmptyCover.Font.Style);
				buttonXDeleteSalesQuote.Font = new Font(buttonXDeleteSalesQuote.Font.FontFamily, buttonXDeleteSalesQuote.Font.Size - 2, buttonXDeleteSalesQuote.Font.Style);
				buttonXSavedFiles.Font = new Font(buttonXSavedFiles.Font.FontFamily, buttonXSavedFiles.Font.Size - 2, buttonXSavedFiles.Font.Style);
				textEditSalesQuoteAuthor.Font = new Font(textEditSalesQuoteAuthor.Font.FontFamily, textEditSalesQuoteAuthor.Font.Size - 2, textEditSalesQuoteAuthor.Font.Style);
				memoEditSalesQuote.Font = new Font(memoEditSalesQuote.Font.FontFamily, memoEditSalesQuote.Font.Size - 2, memoEditSalesQuote.Font.Style);
			}
			comboBoxEditSlideHeader.MouseUp += FormMain.Instance.Editor_MouseUp;
			comboBoxEditSlideHeader.MouseDown += FormMain.Instance.Editor_MouseDown;
			comboBoxEditSlideHeader.Enter += FormMain.Instance.Editor_Enter;
			comboBoxEditAdvertiser.MouseUp += FormMain.Instance.Editor_MouseUp;
			comboBoxEditAdvertiser.MouseDown += FormMain.Instance.Editor_MouseDown;
			comboBoxEditAdvertiser.Enter += FormMain.Instance.Editor_Enter;
			comboBoxEditDecisionMaker.MouseUp += FormMain.Instance.Editor_MouseUp;
			comboBoxEditDecisionMaker.MouseDown += FormMain.Instance.Editor_MouseDown;
			comboBoxEditDecisionMaker.Enter += FormMain.Instance.Editor_Enter;
		}

		public AppManager.SingleParameterDelegate EnableOutput { get; set; }
		public AppManager.SingleParameterDelegate EnableSavedFiles { get; set; }

		public bool SettingsNotSaved { get; set; }

		public static CoverControl Instance
		{
			get
			{
				if (_instance == null)
					_instance = new CoverControl();
				return _instance;
			}
		}

		private void LoadSavedState()
		{
			_allowToSave = false;

			LoadSalesRep();

			if (string.IsNullOrEmpty(ViewSettingsManager.Instance.CoverState.SlideHeader))
			{
				if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
					comboBoxEditSlideHeader.SelectedIndex = 0;
			}
			else
			{
				int index = comboBoxEditSlideHeader.Properties.Items.IndexOf(ViewSettingsManager.Instance.CoverState.SlideHeader);
				if (index >= 0)
					comboBoxEditSlideHeader.SelectedIndex = index;
				else
					comboBoxEditSlideHeader.SelectedIndex = 0;
			}

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
			buttonXDeleteSalesQuote.Enabled = !checkEditUseEmptyCover.Checked;
			pbSalesQuotes.Enabled = !checkEditUseEmptyCover.Checked;
			dateEditPresentationDate.Enabled = !checkEditUseEmptyCover.Checked && checkEditPresentationDate.Checked;
			checkEditPresentationDate.Enabled = !checkEditUseEmptyCover.Checked;
			checkEditSalesRep.Enabled = !checkEditUseEmptyCover.Checked;
			lookUpEditSalesRep.Enabled = !checkEditUseEmptyCover.Checked && checkEditSalesRep.Checked;

			if (ViewSettingsManager.Instance.CoverState.Quote.IsSet)
			{
				textEditSalesQuoteAuthor.EditValue = ViewSettingsManager.Instance.CoverState.Quote.Author;
				memoEditSalesQuote.EditValue = ViewSettingsManager.Instance.CoverState.Quote.Text;
				buttonXDeleteSalesQuote.Visible = true;
			}
			else
			{
				textEditSalesQuoteAuthor.EditValue = null;
				memoEditSalesQuote.EditValue = null;
				buttonXDeleteSalesQuote.Visible = false;
			}

			comboBoxEditAdvertiser.EditValue = string.IsNullOrEmpty(ViewSettingsManager.Instance.CoverState.Advertiser) ? null : ViewSettingsManager.Instance.CoverState.Advertiser;
			comboBoxEditDecisionMaker.EditValue = string.IsNullOrEmpty(ViewSettingsManager.Instance.CoverState.DecisionMaker) ? null : ViewSettingsManager.Instance.CoverState.DecisionMaker;

			_allowToSave = true;
			SettingsNotSaved = false;

			UpdateOutputState();

			UpdateSavedFilesState();
		}

		private void SaveState()
		{
			ViewSettingsManager.Instance.CoverState.SlideHeader = comboBoxEditSlideHeader.EditValue != null ? comboBoxEditSlideHeader.EditValue.ToString() : string.Empty;
			ViewSettingsManager.Instance.CoverState.AddAsPageOne = checkEditFirstSlide.Checked;
			ViewSettingsManager.Instance.CoverState.ShowPresentationDate = checkEditPresentationDate.Checked;
			ViewSettingsManager.Instance.CoverState.PresentationDate = dateEditPresentationDate.DateTime;
			ViewSettingsManager.Instance.CoverState.UseGenericCover = checkEditUseEmptyCover.Checked;
			ViewSettingsManager.Instance.CoverState.Quote.Author = textEditSalesQuoteAuthor.EditValue != null ? textEditSalesQuoteAuthor.EditValue.ToString() : string.Empty;
			ViewSettingsManager.Instance.CoverState.Quote.Text = memoEditSalesQuote.EditValue != null ? memoEditSalesQuote.EditValue.ToString() : string.Empty;
			ViewSettingsManager.Instance.CoverState.Advertiser = comboBoxEditAdvertiser.EditValue != null ? comboBoxEditAdvertiser.EditValue.ToString() : string.Empty;
			ViewSettingsManager.Instance.CoverState.DecisionMaker = comboBoxEditDecisionMaker.EditValue != null ? comboBoxEditDecisionMaker.EditValue.ToString() : string.Empty;
			SaveSalesRep();
			SettingsNotSaved = false;
		}

		private void LoadSalesRep()
		{
			checkEditSalesRep.Checked = ViewSettingsManager.Instance.CoverState.ShowSalesRep;
			lookUpEditSalesRep.Enabled = checkEditSalesRep.Checked;
			User user = _users.Where(x => x.FullName.Equals(ViewSettingsManager.Instance.CoverState.SalesRep)).FirstOrDefault();
			if (user != null)
			{
				lookUpEditSalesRep.EditValue = ViewSettingsManager.Instance.CoverState.SalesRep;
				laSalesRepEmail.Text = user.Email;
				laSalesRepPhone.Text = user.Phone;
			}
			else
			{
				lookUpEditSalesRep.EditValue = null;
				laSalesRepEmail.Text = string.Empty;
				laSalesRepPhone.Text = string.Empty;
			}
		}

		private void SaveSalesRep()
		{
			ViewSettingsManager.Instance.CoverState.ShowSalesRep = checkEditSalesRep.Checked;
			User user = _users.Where(x => x.FullName.Equals(lookUpEditSalesRep.EditValue != null ? lookUpEditSalesRep.EditValue.ToString() : string.Empty)).FirstOrDefault();
			if (user != null)
			{
				laSalesRepEmail.Text = user.Email;
				laSalesRepPhone.Text = user.Phone;
				ViewSettingsManager.Instance.CoverState.SalesRep = user.FullName;
			}
			else
			{
				ViewSettingsManager.Instance.CoverState.SalesRep = string.Empty;
				laSalesRepEmail.Text = string.Empty;
				laSalesRepPhone.Text = string.Empty;
			}
			SettingsManager.Instance.SaveDashboardSettings();
		}

		private void CoverControl_Load(object sender, EventArgs e)
		{
			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(ListManager.Instance.CoverLists.Headers);

			comboBoxEditAdvertiser.Properties.Items.Clear();
			comboBoxEditAdvertiser.Properties.Items.AddRange(Core.Common.ListManager.Instance.Advertisers);

			comboBoxEditDecisionMaker.Properties.Items.Clear();
			comboBoxEditDecisionMaker.Properties.Items.AddRange(Core.Common.ListManager.Instance.DecisionMakers);

			_users = new BindingList<User>(new List<User>(ListManager.Instance.UsersList.GetUsersByStation(MasterWizardManager.Instance.SelectedWizard.Name)));
			lookUpEditSalesRep.Properties.DataSource = _users;

			FormMain.Instance.FormClosed += (sender1, e1) =>
			{
				if (SettingsNotSaved)
				{
					SaveState();
					ViewSettingsManager.Instance.CoverState.Save();
				}
			};

			LoadSavedState();
		}

		private void comboBoxEdit_EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				UpdateOutputState();
				SettingsNotSaved = true;
			}
		}

		private void checkEditSalesRep_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				SaveSalesRep();
				lookUpEditSalesRep.Enabled = checkEditSalesRep.Checked;
				SettingsNotSaved = true;
			}
		}

		private void lookUpEditSalesRep_EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				SaveSalesRep();
				SettingsNotSaved = true;
			}
		}

		private void ckPresentationDate_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				dateEditPresentationDate.Enabled = checkEditPresentationDate.Checked;
				SettingsNotSaved = true;
			}
		}

		private void buttonXSalesQuotes_Click(object sender, EventArgs e)
		{
			using (var form = new FormQuotes())
			{
				if (form.ShowDialog() == DialogResult.OK)
				{
					if (form.SelectedQuote != null)
					{
						textEditSalesQuoteAuthor.EditValue = form.SelectedQuote.Author;
						memoEditSalesQuote.EditValue = "\"" + form.SelectedQuote.Text + "\"";
						buttonXDeleteSalesQuote.Visible = true;
						SettingsNotSaved = true;
					}
				}
			}
		}

		private void buttonXDeleteSalesQuote_Click(object sender, EventArgs e)
		{
			textEditSalesQuoteAuthor.EditValue = null;
			memoEditSalesQuote.EditValue = null;
			buttonXDeleteSalesQuote.Visible = false;
			SettingsNotSaved = true;
		}

		private void checkEditUseEmptyCover_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				comboBoxEditAdvertiser.Enabled = !checkEditUseEmptyCover.Checked;
				comboBoxEditDecisionMaker.Enabled = !checkEditUseEmptyCover.Checked;
				comboBoxEditSlideHeader.Enabled = !checkEditUseEmptyCover.Checked;
				lookUpEditSalesRep.Enabled = !checkEditUseEmptyCover.Checked;
				buttonXDeleteSalesQuote.Enabled = !checkEditUseEmptyCover.Checked;
				pbSalesQuotes.Enabled = !checkEditUseEmptyCover.Checked;
				dateEditPresentationDate.Enabled = !checkEditUseEmptyCover.Checked && checkEditPresentationDate.Checked;
				checkEditPresentationDate.Enabled = !checkEditUseEmptyCover.Checked;
				UpdateOutputState();
				SettingsNotSaved = true;
			}
		}

		private void checkEdit_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
				SettingsNotSaved = true;
		}

		private void buttonXSavedFiles_Click(object sender, EventArgs e)
		{
			using (var form = new FormSavedCover())
			{
				if (form.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(form.SelectedFile))
				{
					ViewSettingsManager.Instance.CoverState.Load(form.SelectedFile);
					LoadSavedState();
				}
			}
		}

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

		#region Output Staff

		public string Title
		{
			get { return comboBoxEditSlideHeader.EditValue == null ? string.Empty : comboBoxEditSlideHeader.EditValue.ToString(); }
		}

		public string Advertiser
		{
			get { return comboBoxEditAdvertiser.EditValue == null ? string.Empty : comboBoxEditAdvertiser.EditValue.ToString(); }
		}

		public string DecisionMaker
		{
			get { return comboBoxEditDecisionMaker.EditValue == null ? string.Empty : comboBoxEditDecisionMaker.EditValue.ToString(); }
		}

		public string SalesRep
		{
			get
			{
				User selectedUser = lookUpEditSalesRep.EditValue == null ? null : _users[lookUpEditSalesRep.ItemIndex];
				return selectedUser == null || !checkEditSalesRep.Checked ? string.Empty : selectedUser.FullName + "          " + selectedUser.Email + "          " + selectedUser.Phone;
			}
		}

		public string PresentationDate
		{
			get { return !checkEditPresentationDate.Checked || dateEditPresentationDate.EditValue == null ? string.Empty : dateEditPresentationDate.DateTime.ToString("MMMM d, yyyy"); }
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
			bool result = false;
			if (comboBoxEditAdvertiser.EditValue != null && comboBoxEditDecisionMaker.EditValue != null)
				if (!string.IsNullOrEmpty(comboBoxEditAdvertiser.EditValue.ToString().Trim()) && !string.IsNullOrEmpty(comboBoxEditDecisionMaker.EditValue.ToString().Trim()))
					result = true;
			result = result | checkEditUseEmptyCover.Checked;
			if (EnableOutput != null)
				EnableOutput(result);
		}

		public void UpdateSavedFilesState()
		{
			if (EnableSavedFiles != null)
				EnableSavedFiles(ViewSettingsManager.Instance.CoverState.AllowToLoad());
			buttonXSavedFiles.Enabled = ViewSettingsManager.Instance.CoverState.AllowToLoad();
		}

		private void SaveChanges()
		{
			if (!Core.Common.ListManager.Instance.Advertisers.Contains(Advertiser) && !string.IsNullOrEmpty(Advertiser))
			{
				Core.Common.ListManager.Instance.Advertisers.Add(Advertiser);
				Core.Common.ListManager.Instance.SaveAdvertisers();
			}

			if (!Core.Common.ListManager.Instance.DecisionMakers.Contains(DecisionMaker) && !string.IsNullOrEmpty(DecisionMaker))
			{
				Core.Common.ListManager.Instance.DecisionMakers.Add(DecisionMaker);
				Core.Common.ListManager.Instance.SaveDecisionMakers();
			}

			if (SettingsNotSaved)
			{
				SaveState();
				ViewSettingsManager.Instance.CoverState.Save();
				UpdateSavedFilesState();
			}
		}

		public void Output()
		{
			SaveChanges();
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				form.TopMost = true;
				form.Show();
				if (checkEditUseEmptyCover.Checked)
					AppManager.Instance.ShowFloater(null, () =>
					{
						DashboardPowerPointHelper.Instance.AppendGenericCover(checkEditFirstSlide.Checked);
						form.Close();
					});

				else
					AppManager.Instance.ShowFloater(null, () =>
					{
						DashboardPowerPointHelper.Instance.AppendCover(checkEditFirstSlide.Checked);
						form.Close();
					});
			}
		}

		public void Preview()
		{
			SaveChanges();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				string tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				if (checkEditUseEmptyCover.Checked)
					DashboardPowerPointHelper.Instance.PrepareGenericCover(tempFileName, checkEditFirstSlide.Checked);
				else
					DashboardPowerPointHelper.Instance.PrepareCover(tempFileName, checkEditFirstSlide.Checked);
				Utilities.Instance.ActivateForm(FormMain.Instance.Handle, false, false);
				formProgress.Close();
				if (!File.Exists(tempFileName)) return;
				using (var formPreview = new FormPreview())
				{
					formPreview.Text = "Preview Slides";
					formPreview.PresentationFile = tempFileName;
					RegistryHelper.MainFormHandle = formPreview.Handle;
					RegistryHelper.MaximizeMainForm = false;
					var previewResult = formPreview.ShowDialog();
					RegistryHelper.MaximizeMainForm = false;
					RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
					if (previewResult != DialogResult.OK)
						Utilities.Instance.ActivateForm(FormMain.Instance.Handle, true, false);
					else
						Utilities.Instance.ActivateMiniBar();
				}
			}
		}
		#endregion
	}
}