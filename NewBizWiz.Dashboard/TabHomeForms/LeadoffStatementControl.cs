using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Dashboard;
using NewBizWiz.Dashboard.InteropClasses;
using NewBizWiz.Dashboard.ToolForms;
using ListManager = NewBizWiz.Core.Dashboard.ListManager;

namespace NewBizWiz.Dashboard.TabHomeForms
{
	[ToolboxItem(false)]
	public partial class LeadoffStatementControl : UserControl
	{
		private static LeadoffStatementControl _instance;
		private bool _allowToSave;

		private LeadoffStatementControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AppManager.Instance.SetClickEventHandler(this);
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 3, laTitle.Font.Style);
				laSlideHeader.Font = new Font(laSlideHeader.Font.FontFamily, laSlideHeader.Font.Size - 2, laSlideHeader.Font.Style);
				laDetail.Font = new Font(laDetail.Font.FontFamily, laDetail.Font.Size - 3, laDetail.Font.Style);
				ckA.Font = new Font(ckA.Font.FontFamily, ckA.Font.Size - 3, ckA.Font.Style);
				ckB.Font = new Font(ckB.Font.FontFamily, ckB.Font.Size - 3, ckB.Font.Style);
				ckC.Font = new Font(ckC.Font.FontFamily, ckC.Font.Size - 3, ckC.Font.Style);
			}
			UpdateEditState();
			comboBoxEditSlideHeader.MouseUp += FormMain.Instance.Editor_MouseUp;
			comboBoxEditSlideHeader.MouseDown += FormMain.Instance.Editor_MouseDown;
			comboBoxEditSlideHeader.Enter += FormMain.Instance.Editor_Enter;
			memoEditA.MouseUp += FormMain.Instance.Editor_MouseUp;
			memoEditA.MouseDown += FormMain.Instance.Editor_MouseDown;
			memoEditA.Enter += FormMain.Instance.Editor_Enter;
			memoEditB.MouseUp += FormMain.Instance.Editor_MouseUp;
			memoEditB.MouseDown += FormMain.Instance.Editor_MouseDown;
			memoEditB.Enter += FormMain.Instance.Editor_Enter;
			memoEditC.MouseUp += FormMain.Instance.Editor_MouseUp;
			memoEditC.MouseDown += FormMain.Instance.Editor_MouseDown;
			memoEditC.Enter += FormMain.Instance.Editor_Enter;
		}

		public AppManager.SingleParameterDelegate EnableOutput { get; set; }
		public AppManager.SingleParameterDelegate EnableSavedFiles { get; set; }

		public bool SettingsNotSaved { get; set; }

		public static LeadoffStatementControl Instance
		{
			get
			{
				if (_instance == null)
					_instance = new LeadoffStatementControl();
				return _instance;
			}
		}

		private void UpdateEditState()
		{
			memoEditA.Enabled = ckA.Checked;
			memoEditB.Enabled = ckB.Checked;
			memoEditC.Enabled = ckC.Checked;
		}

		private void LoadSavedState()
		{
			_allowToSave = false;
			if (string.IsNullOrEmpty(ViewSettingsManager.Instance.LeadoffStatementState.SlideHeader))
			{
				if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
					comboBoxEditSlideHeader.SelectedIndex = 0;
			}
			else
			{
				int index = comboBoxEditSlideHeader.Properties.Items.IndexOf(ViewSettingsManager.Instance.LeadoffStatementState.SlideHeader);
				if (index >= 0)
					comboBoxEditSlideHeader.SelectedIndex = index;
				else
					comboBoxEditSlideHeader.SelectedIndex = 0;
			}
			ckA.Checked = ViewSettingsManager.Instance.LeadoffStatementState.ShowStatement1;
			ckB.Checked = ViewSettingsManager.Instance.LeadoffStatementState.ShowStatement2;
			ckC.Checked = ViewSettingsManager.Instance.LeadoffStatementState.ShowStatement3;
			memoEditA.EditValue = !string.IsNullOrEmpty(ViewSettingsManager.Instance.LeadoffStatementState.Statement1) ? ViewSettingsManager.Instance.LeadoffStatementState.Statement1 : (ListManager.Instance.LeadoffStatementLists.Statements.Count > 0 ? ListManager.Instance.LeadoffStatementLists.Statements[0] : string.Empty);
			memoEditB.EditValue = !string.IsNullOrEmpty(ViewSettingsManager.Instance.LeadoffStatementState.Statement2) ? ViewSettingsManager.Instance.LeadoffStatementState.Statement2 : (ListManager.Instance.LeadoffStatementLists.Statements.Count > 1 ? ListManager.Instance.LeadoffStatementLists.Statements[1] : string.Empty);
			memoEditC.EditValue = !string.IsNullOrEmpty(ViewSettingsManager.Instance.LeadoffStatementState.Statement3) ? ViewSettingsManager.Instance.LeadoffStatementState.Statement3 : (ListManager.Instance.LeadoffStatementLists.Statements.Count > 2 ? ListManager.Instance.LeadoffStatementLists.Statements[2] : string.Empty);

			_allowToSave = true;
			SettingsNotSaved = false;

			UpdateOutputState();
			UpdateSavedFilesState();
		}

		private void SaveState()
		{
			ViewSettingsManager.Instance.LeadoffStatementState.SlideHeader = comboBoxEditSlideHeader.EditValue != null ? comboBoxEditSlideHeader.EditValue.ToString() : string.Empty;
			ViewSettingsManager.Instance.LeadoffStatementState.ShowStatement1 = ckA.Checked;
			ViewSettingsManager.Instance.LeadoffStatementState.ShowStatement2 = ckB.Checked;
			ViewSettingsManager.Instance.LeadoffStatementState.ShowStatement3 = ckC.Checked;
			ViewSettingsManager.Instance.LeadoffStatementState.Statement1 = memoEditA.EditValue != null ? memoEditA.EditValue.ToString() : string.Empty;
			ViewSettingsManager.Instance.LeadoffStatementState.Statement2 = memoEditB.EditValue != null ? memoEditB.EditValue.ToString() : string.Empty;
			ViewSettingsManager.Instance.LeadoffStatementState.Statement3 = memoEditC.EditValue != null ? memoEditC.EditValue.ToString() : string.Empty;
			SettingsNotSaved = false;
		}

		public void LoadFromFile()
		{
			using (var form = new FormSavedLeadoffStatement())
			{
				if (form.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(form.SelectedFile))
				{
					ViewSettingsManager.Instance.LeadoffStatementState.Load(form.SelectedFile);
					LoadSavedState();
				}
			}
		}

		private void LeadoffStatementControl_Load(object sender, EventArgs e)
		{
			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(ListManager.Instance.LeadoffStatementLists.Headers);

			FormMain.Instance.FormClosed += (sender1, e1) =>
												{
													if (SettingsNotSaved)
													{
														SaveState();
														ViewSettingsManager.Instance.LeadoffStatementState.Save();
													}
												};

			LoadSavedState();
		}

		private void checkBoxes_CheckedChanged(object sender, EventArgs e)
		{
			UpdateEditState();
			UpdateOutputState();
			if (_allowToSave)
				SettingsNotSaved = true;
		}

		private void memoEditC_EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
				SettingsNotSaved = true;
		}

		#region Output Staff
		public int StatementsCount
		{
			get
			{
				int result = 0;
				if (ckA.Checked && memoEditA.EditValue != null)
					if (!string.IsNullOrEmpty(memoEditA.EditValue.ToString().Trim()))
						result++;
				if (ckB.Checked && memoEditB.EditValue != null)
					if (!string.IsNullOrEmpty(memoEditB.EditValue.ToString().Trim()))
						result++;
				if (ckC.Checked && memoEditC.EditValue != null)
					if (!string.IsNullOrEmpty(memoEditC.EditValue.ToString().Trim()))
						result++;
				return result;
			}
		}

		public string Title
		{
			get { return comboBoxEditSlideHeader.EditValue == null ? string.Empty : comboBoxEditSlideHeader.EditValue.ToString(); }
		}

		public string[] SelectedStatements
		{
			get
			{
				var result = new List<string>();
				if (ckA.Checked && memoEditA.EditValue != null)
					if (!string.IsNullOrEmpty(memoEditA.EditValue.ToString().Trim()))
						result.Add(memoEditA.EditValue.ToString());
				if (ckB.Checked && memoEditB.EditValue != null)
					if (!string.IsNullOrEmpty(memoEditB.EditValue.ToString().Trim()))
						result.Add(memoEditB.EditValue.ToString());
				if (ckC.Checked && memoEditC.EditValue != null)
					if (!string.IsNullOrEmpty(memoEditC.EditValue.ToString().Trim()))
						result.Add(memoEditC.EditValue.ToString());
				return result.ToArray();
			}
		}

		public void UpdateOutputState()
		{
			if (EnableOutput != null)
				EnableOutput(ckA.Checked || ckB.Checked || ckC.Checked);
		}

		public void UpdateSavedFilesState()
		{
			if (EnableSavedFiles != null)
				EnableSavedFiles(ViewSettingsManager.Instance.LeadoffStatementState.AllowToLoad());
		}

		private void SaveChanges()
		{
			if (SettingsNotSaved)
			{
				SaveState();
				ViewSettingsManager.Instance.LeadoffStatementState.Save();
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
				AppManager.Instance.ShowFloater(null, () =>
				{
					DashboardPowerPointHelper.Instance.AppendLeadoffStatements();
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
				var tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				DashboardPowerPointHelper.Instance.PrepareLeadoffStatements(tempFileName);
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