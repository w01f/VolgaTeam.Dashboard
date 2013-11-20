using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Dashboard;
using NewBizWiz.Dashboard.InteropClasses;
using NewBizWiz.Dashboard.ToolForms;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;
using ListManager = NewBizWiz.Core.Dashboard.ListManager;

namespace NewBizWiz.Dashboard.TabHomeForms
{
	[ToolboxItem(false)]
	public partial class TargetCustomersControl : UserControl
	{
		private static TargetCustomersControl _instance;
		private bool _allowToSave;

		private TargetCustomersControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AppManager.Instance.SetClickEventHandler(this);
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 3, laTitle.Font.Style);
				laSlideHeader.Font = new Font(laSlideHeader.Font.FontFamily, laSlideHeader.Font.Size - 2, laSlideHeader.Font.Style);
				labelXDetail.Font = new Font(labelXDetail.Font.FontFamily, labelXDetail.Font.Size - 3, labelXDetail.Font.Style);
			}
			comboBoxEditSlideHeader.MouseUp += FormMain.Instance.Editor_MouseUp;
			comboBoxEditSlideHeader.MouseDown += FormMain.Instance.Editor_MouseDown;
			comboBoxEditSlideHeader.Enter += FormMain.Instance.Editor_Enter;
		}

		public AppManager.SingleParameterDelegate EnableOutput { get; set; }
		public AppManager.SingleParameterDelegate EnableSavedFiles { get; set; }

		public bool SettingsNotSaved { get; set; }

		public static TargetCustomersControl Instance
		{
			get
			{
				if (_instance == null)
					_instance = new TargetCustomersControl();
				return _instance;
			}
		}

		private void LoadSavedState()
		{
			_allowToSave = false;
			if (string.IsNullOrEmpty(ViewSettingsManager.Instance.TargetCustomersState.SlideHeader))
			{
				if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
					comboBoxEditSlideHeader.SelectedIndex = 0;
			}
			else
			{
				int index = comboBoxEditSlideHeader.Properties.Items.IndexOf(ViewSettingsManager.Instance.TargetCustomersState.SlideHeader);
				if (index >= 0)
					comboBoxEditSlideHeader.SelectedIndex = index;
				else
					comboBoxEditSlideHeader.SelectedIndex = 0;
			}

			foreach (CheckedListBoxItem item in checkedListBoxControlTargetDemo.Items)
				if (ViewSettingsManager.Instance.TargetCustomersState.Demo.Contains(item.Value.ToString()))
					item.CheckState = CheckState.Checked;
			foreach (CheckedListBoxItem item in checkedListBoxControlHouseholdIncome.Items)
				if (ViewSettingsManager.Instance.TargetCustomersState.Income.Contains(item.Value.ToString()))
					item.CheckState = CheckState.Checked;
			foreach (CheckedListBoxItem item in checkedListBoxControlGeographicResidence.Items)
				if (ViewSettingsManager.Instance.TargetCustomersState.Geographic.Contains(item.Value.ToString()))
					item.CheckState = CheckState.Checked;

			_allowToSave = true;
			SettingsNotSaved = false;

			UpdateSavedFilesState();
			UpdateOutputState();
		}

		private void SaveState()
		{
			ViewSettingsManager.Instance.TargetCustomersState.SlideHeader = comboBoxEditSlideHeader.EditValue != null ? comboBoxEditSlideHeader.EditValue.ToString() : string.Empty;

			ViewSettingsManager.Instance.TargetCustomersState.Demo.Clear();
			foreach (CheckedListBoxItem item in checkedListBoxControlTargetDemo.Items)
				if (item.CheckState == CheckState.Checked)
					ViewSettingsManager.Instance.TargetCustomersState.Demo.Add(item.Value.ToString());
			ViewSettingsManager.Instance.TargetCustomersState.Income.Clear();
			foreach (CheckedListBoxItem item in checkedListBoxControlHouseholdIncome.Items)
				if (item.CheckState == CheckState.Checked)
					ViewSettingsManager.Instance.TargetCustomersState.Income.Add(item.Value.ToString());
			ViewSettingsManager.Instance.TargetCustomersState.Geographic.Clear();
			foreach (CheckedListBoxItem item in checkedListBoxControlGeographicResidence.Items)
				if (item.CheckState == CheckState.Checked)
					ViewSettingsManager.Instance.TargetCustomersState.Geographic.Add(item.Value.ToString());
			SettingsNotSaved = false;
		}

		public void LoadFromFile()
		{
			using (var form = new FormSavedTargetCustomers())
			{
				if (form.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(form.SelectedFile))
				{
					ViewSettingsManager.Instance.TargetCustomersState.Load(form.SelectedFile);
					LoadSavedState();
				}
			}
		}

		private void TargetCustomersControl_Load(object sender, EventArgs e)
		{
			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(ListManager.Instance.TargetCustomersLists.Headers);

			checkedListBoxControlTargetDemo.Items.Clear();
			checkedListBoxControlTargetDemo.Items.AddRange(ListManager.Instance.TargetCustomersLists.Demos.ToArray());

			checkedListBoxControlHouseholdIncome.Items.Clear();
			checkedListBoxControlHouseholdIncome.Items.AddRange(ListManager.Instance.TargetCustomersLists.HHIs.ToArray());

			checkedListBoxControlGeographicResidence.Items.Clear();
			checkedListBoxControlGeographicResidence.Items.AddRange(ListManager.Instance.TargetCustomersLists.Geographies.ToArray());

			FormMain.Instance.FormClosed += (sender1, e1) =>
												{
													if (SettingsNotSaved)
													{
														SaveState();
														ViewSettingsManager.Instance.TargetCustomersState.Save();
													}
												};

			LoadSavedState();
		}

		private void checkedListBoxControl_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (_allowToSave)
			{
				UpdateOutputState();
				SettingsNotSaved = true;
			}
		}

		private void comboBoxEditSlideHeader_EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
				SettingsNotSaved = true;
		}

		#region Output Staff
		public string Title
		{
			get { return comboBoxEditSlideHeader.EditValue == null ? string.Empty : comboBoxEditSlideHeader.EditValue.ToString(); }
		}

		public string TargetDemo
		{
			get
			{
				string result = string.Empty;
				foreach (CheckedListBoxItem item in checkedListBoxControlTargetDemo.CheckedItems)
					result += ", " + item.Value;
				if (!string.IsNullOrEmpty(result))
					result = result.Substring(2);
				return result;
			}
		}

		public string HHI
		{
			get
			{
				string result = string.Empty;
				foreach (CheckedListBoxItem item in checkedListBoxControlHouseholdIncome.CheckedItems)
					result += ", " + item.Value;
				if (!string.IsNullOrEmpty(result))
					result = result.Substring(2);
				return result;
			}
		}

		public string Geography
		{
			get
			{
				string result = string.Empty;
				foreach (CheckedListBoxItem item in checkedListBoxControlGeographicResidence.CheckedItems)
					result += ", " + item.Value;
				if (!string.IsNullOrEmpty(result))
					result = result.Substring(2);
				return result;
			}
		}

		public void UpdateOutputState()
		{
			if (EnableOutput != null)
				EnableOutput(checkedListBoxControlGeographicResidence.CheckedItems.Count > 0 && checkedListBoxControlHouseholdIncome.CheckedItems.Count > 0 && checkedListBoxControlTargetDemo.CheckedItems.Count > 0);
		}

		public void UpdateSavedFilesState()
		{
			if (EnableSavedFiles != null)
				EnableSavedFiles(ViewSettingsManager.Instance.TargetCustomersState.AllowToLoad());
		}

		private void SaveChanges()
		{
			if (SettingsNotSaved)
			{
				SaveState();
				ViewSettingsManager.Instance.TargetCustomersState.Save();
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
					DashboardPowerPointHelper.Instance.AppendTargetCustomers();
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
				DashboardPowerPointHelper.Instance.PrepareTargetCustomers(tempFileName);
				Utilities.Instance.ActivateForm(FormMain.Instance.Handle, false, false);
				formProgress.Close();
				if (!File.Exists(tempFileName)) return;
				using (var formPreview = new FormPreview(FormMain.Instance, DashboardPowerPointHelper.Instance, AppManager.Instance.HelpManager, AppManager.Instance.ShowFloater))
				{
					formPreview.Text = "Preview Slides";
					formPreview.PresentationFile = tempFileName;
					RegistryHelper.MainFormHandle = formPreview.Handle;
					RegistryHelper.MaximizeMainForm = false;
					var previewResult = formPreview.ShowDialog();
					RegistryHelper.MaximizeMainForm = false;
					RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
					if (previewResult != DialogResult.OK)
						AppManager.Instance.ActivateMainForm();
					else
						Utilities.Instance.ActivateMiniBar();
				}
			}
		}
		#endregion
	}
}