using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NewBizWiz.Core.Dashboard;
using NewBizWiz.Dashboard.InteropClasses;

namespace NewBizWiz.Dashboard.TabHomeForms
{
	[ToolboxItem(false)]
	public partial class ClientGoalsControl : UserControl
	{
		private static ClientGoalsControl _instance;
		private bool _allowToSave;

		private ClientGoalsControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AppManager.Instance.SetClickEventHandler(this);
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 3, laTitle.Font.Style);
				laSlideHeader.Font = new Font(laSlideHeader.Font.FontFamily, laSlideHeader.Font.Size - 2, laSlideHeader.Font.Style);
				laDetail.Font = new Font(laDetail.Font.FontFamily, laDetail.Font.Size - 3, laDetail.Font.Style);
				laGoal1.Font = new Font(laGoal1.Font.FontFamily, laGoal1.Font.Size - 3, laGoal1.Font.Style);
				laGoal2.Font = new Font(laGoal2.Font.FontFamily, laGoal2.Font.Size - 3, laGoal2.Font.Style);
				laGoal3.Font = new Font(laGoal3.Font.FontFamily, laGoal3.Font.Size - 3, laGoal3.Font.Style);
				laGoal4.Font = new Font(laGoal4.Font.FontFamily, laGoal4.Font.Size - 3, laGoal4.Font.Style);
				laGoal5.Font = new Font(laGoal5.Font.FontFamily, laGoal5.Font.Size - 3, laGoal5.Font.Style);
			}
			comboBoxEditSlideHeader.MouseUp += FormMain.Instance.Editor_MouseUp;
			comboBoxEditSlideHeader.MouseDown += FormMain.Instance.Editor_MouseDown;
			comboBoxEditSlideHeader.Enter += FormMain.Instance.Editor_Enter;
			comboBoxEditGoal1.MouseUp += FormMain.Instance.Editor_MouseUp;
			comboBoxEditGoal1.MouseDown += FormMain.Instance.Editor_MouseDown;
			comboBoxEditGoal1.Enter += FormMain.Instance.Editor_Enter;
			comboBoxEditGoal2.MouseUp += FormMain.Instance.Editor_MouseUp;
			comboBoxEditGoal2.MouseDown += FormMain.Instance.Editor_MouseDown;
			comboBoxEditGoal2.Enter += FormMain.Instance.Editor_Enter;
			comboBoxEditGoal3.MouseUp += FormMain.Instance.Editor_MouseUp;
			comboBoxEditGoal3.MouseDown += FormMain.Instance.Editor_MouseDown;
			comboBoxEditGoal3.Enter += FormMain.Instance.Editor_Enter;
			comboBoxEditGoal4.MouseUp += FormMain.Instance.Editor_MouseUp;
			comboBoxEditGoal4.MouseDown += FormMain.Instance.Editor_MouseDown;
			comboBoxEditGoal4.Enter += FormMain.Instance.Editor_Enter;
			comboBoxEditGoal5.MouseUp += FormMain.Instance.Editor_MouseUp;
			comboBoxEditGoal5.MouseDown += FormMain.Instance.Editor_MouseDown;
			comboBoxEditGoal5.Enter += FormMain.Instance.Editor_Enter;
		}

		public AppManager.SingleParameterDelegate EnableOutput { get; set; }
		public AppManager.SingleParameterDelegate EnableSavedFiles { get; set; }

		public bool SettingsNotSaved { get; set; }

		public static ClientGoalsControl Instance
		{
			get
			{
				if (_instance == null)
					_instance = new ClientGoalsControl();
				return _instance;
			}
		}

		private void LoadSavedState()
		{
			_allowToSave = false;
			if (string.IsNullOrEmpty(ViewSettingsManager.Instance.ClientGoalsState.SlideHeader))
			{
				if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
					comboBoxEditSlideHeader.SelectedIndex = 0;
			}
			else
			{
				int index = comboBoxEditSlideHeader.Properties.Items.IndexOf(ViewSettingsManager.Instance.ClientGoalsState.SlideHeader);
				if (index >= 0)
					comboBoxEditSlideHeader.SelectedIndex = index;
				else
					comboBoxEditSlideHeader.SelectedIndex = 0;
			}
			comboBoxEditGoal1.EditValue = !string.IsNullOrEmpty(ViewSettingsManager.Instance.ClientGoalsState.Goal1) ? ViewSettingsManager.Instance.ClientGoalsState.Goal1 : null;
			comboBoxEditGoal2.EditValue = !string.IsNullOrEmpty(ViewSettingsManager.Instance.ClientGoalsState.Goal2) ? ViewSettingsManager.Instance.ClientGoalsState.Goal2 : null;
			comboBoxEditGoal3.EditValue = !string.IsNullOrEmpty(ViewSettingsManager.Instance.ClientGoalsState.Goal3) ? ViewSettingsManager.Instance.ClientGoalsState.Goal3 : null;
			comboBoxEditGoal4.EditValue = !string.IsNullOrEmpty(ViewSettingsManager.Instance.ClientGoalsState.Goal4) ? ViewSettingsManager.Instance.ClientGoalsState.Goal4 : null;
			comboBoxEditGoal5.EditValue = !string.IsNullOrEmpty(ViewSettingsManager.Instance.ClientGoalsState.Goal5) ? ViewSettingsManager.Instance.ClientGoalsState.Goal5 : null;

			_allowToSave = true;
			SettingsNotSaved = false;

			UpdateSavedFilesState();
		}

		private void SaveState()
		{
			ViewSettingsManager.Instance.ClientGoalsState.SlideHeader = comboBoxEditSlideHeader.EditValue != null ? comboBoxEditSlideHeader.EditValue.ToString() : string.Empty;
			ViewSettingsManager.Instance.ClientGoalsState.Goal1 = comboBoxEditGoal1.EditValue != null ? comboBoxEditGoal1.EditValue.ToString() : string.Empty;
			ViewSettingsManager.Instance.ClientGoalsState.Goal2 = comboBoxEditGoal2.EditValue != null ? comboBoxEditGoal2.EditValue.ToString() : string.Empty;
			ViewSettingsManager.Instance.ClientGoalsState.Goal3 = comboBoxEditGoal3.EditValue != null ? comboBoxEditGoal3.EditValue.ToString() : string.Empty;
			ViewSettingsManager.Instance.ClientGoalsState.Goal4 = comboBoxEditGoal4.EditValue != null ? comboBoxEditGoal4.EditValue.ToString() : string.Empty;
			ViewSettingsManager.Instance.ClientGoalsState.Goal5 = comboBoxEditGoal5.EditValue != null ? comboBoxEditGoal5.EditValue.ToString() : string.Empty;
			SettingsNotSaved = false;
		}

		public void LoadFromFile()
		{
			using (var form = new FormSavedClentGoals())
			{
				if (form.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(form.SelectedFile))
				{
					ViewSettingsManager.Instance.ClientGoalsState.Load(form.SelectedFile);
					LoadSavedState();
				}
			}
		}

		private void ClientGoalsControl_Load(object sender, EventArgs e)
		{
			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(ListManager.Instance.ClientGoalsLists.Headers);

			comboBoxEditGoal1.Properties.Items.Clear();
			comboBoxEditGoal1.Properties.Items.AddRange(ListManager.Instance.ClientGoalsLists.Goals);

			comboBoxEditGoal2.Properties.Items.Clear();
			comboBoxEditGoal2.Properties.Items.AddRange(ListManager.Instance.ClientGoalsLists.Goals);

			comboBoxEditGoal3.Properties.Items.Clear();
			comboBoxEditGoal3.Properties.Items.AddRange(ListManager.Instance.ClientGoalsLists.Goals);

			comboBoxEditGoal4.Properties.Items.Clear();
			comboBoxEditGoal4.Properties.Items.AddRange(ListManager.Instance.ClientGoalsLists.Goals);

			comboBoxEditGoal5.Properties.Items.Clear();
			comboBoxEditGoal5.Properties.Items.AddRange(ListManager.Instance.ClientGoalsLists.Goals);

			FormMain.Instance.FormClosed += (sender1, e1) =>
				                                {
					                                if (SettingsNotSaved)
					                                {
						                                SaveState();
						                                ViewSettingsManager.Instance.ClientGoalsState.Save();
					                                }
				                                };

			LoadSavedState();
		}

		private void comboBoxEditGoal_EditValueChanged(object sender, EventArgs e)
		{
			UpdateOutputState();
			if (_allowToSave)
				SettingsNotSaved = true;
		}

		#region Output Staff
		public int GoalsCount
		{
			get
			{
				int result = 0;
				if (comboBoxEditGoal1.EditValue != null)
					if (!string.IsNullOrEmpty(comboBoxEditGoal1.EditValue.ToString().Trim()))
						result++;
				if (comboBoxEditGoal2.EditValue != null)
					if (!string.IsNullOrEmpty(comboBoxEditGoal2.EditValue.ToString().Trim()))
						result++;
				if (comboBoxEditGoal3.EditValue != null)
					if (!string.IsNullOrEmpty(comboBoxEditGoal3.EditValue.ToString().Trim()))
						result++;
				if (comboBoxEditGoal4.EditValue != null)
					if (!string.IsNullOrEmpty(comboBoxEditGoal4.EditValue.ToString().Trim()))
						result++;
				if (comboBoxEditGoal5.EditValue != null)
					if (!string.IsNullOrEmpty(comboBoxEditGoal5.EditValue.ToString().Trim()))
						result++;
				return result;
			}
		}

		public string Title
		{
			get { return comboBoxEditSlideHeader.EditValue == null ? string.Empty : comboBoxEditSlideHeader.EditValue.ToString(); }
		}

		public string[] SelectedGoals
		{
			get
			{
				var result = new List<string>();
				if (comboBoxEditGoal1.EditValue != null)
					if (!string.IsNullOrEmpty(comboBoxEditGoal1.EditValue.ToString().Trim()))
						result.Add(comboBoxEditGoal1.EditValue.ToString());
				if (comboBoxEditGoal2.EditValue != null)
					if (!string.IsNullOrEmpty(comboBoxEditGoal2.EditValue.ToString().Trim()))
						result.Add(comboBoxEditGoal2.EditValue.ToString());
				if (comboBoxEditGoal3.EditValue != null)
					if (!string.IsNullOrEmpty(comboBoxEditGoal3.EditValue.ToString().Trim()))
						result.Add(comboBoxEditGoal3.EditValue.ToString());
				if (comboBoxEditGoal4.EditValue != null)
					if (!string.IsNullOrEmpty(comboBoxEditGoal4.EditValue.ToString().Trim()))
						result.Add(comboBoxEditGoal4.EditValue.ToString());
				if (comboBoxEditGoal5.EditValue != null)
					if (!string.IsNullOrEmpty(comboBoxEditGoal5.EditValue.ToString().Trim()))
						result.Add(comboBoxEditGoal5.EditValue.ToString());
				return result.ToArray();
			}
		}

		public void Output()
		{
			if (SettingsNotSaved)
			{
				SaveState();
				ViewSettingsManager.Instance.ClientGoalsState.Save();
				UpdateSavedFilesState();
			}
			DashboardPowerPointHelper.Instance.AppendClientGoals();
		}

		public void UpdateOutputState()
		{
			bool result = false;
			if (comboBoxEditGoal1.EditValue != null)
			{
				if (!string.IsNullOrEmpty(comboBoxEditGoal1.EditValue.ToString().Trim()))
					result = true;
			}
			if (comboBoxEditGoal2.EditValue != null)
			{
				if (!string.IsNullOrEmpty(comboBoxEditGoal2.EditValue.ToString().Trim()))
					result = true;
			}
			if (comboBoxEditGoal3.EditValue != null)
			{
				if (!string.IsNullOrEmpty(comboBoxEditGoal3.EditValue.ToString().Trim()))
					result = true;
			}
			if (comboBoxEditGoal4.EditValue != null)
			{
				if (!string.IsNullOrEmpty(comboBoxEditGoal4.EditValue.ToString().Trim()))
					result = true;
			}
			if (comboBoxEditGoal5.EditValue != null)
			{
				if (!string.IsNullOrEmpty(comboBoxEditGoal5.EditValue.ToString().Trim()))
					result = true;
			}
			if (EnableOutput != null)
				EnableOutput(result);
		}

		public void UpdateSavedFilesState()
		{
			if (EnableSavedFiles != null)
				EnableSavedFiles(ViewSettingsManager.Instance.ClientGoalsState.AllowToLoad());
		}
		#endregion
	}
}