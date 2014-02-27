using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;
using SettingsManager = NewBizWiz.Core.AdSchedule.SettingsManager;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	[ToolboxItem(false)]
	public partial class SummariesControl : UserControl
	{
		private ISummaryOutputControl _selectedOutput;

		public OutputBasicOverviewControl BasicOverview { get; private set; }
		public OutputMultiSummaryControl MultiSummary { get; private set; }
		public OutputSnapshotControl Snapshot { get; private set; }

		public ButtonItem HelpButtonItem { get; set; }

		public SummariesControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			BasicOverview = new OutputBasicOverviewControl();
			MultiSummary = new OutputMultiSummaryControl();
			Snapshot = new OutputSnapshotControl();
		}

		public bool AllowToLeaveControl
		{
			get
			{
				bool result = false;
				if (_selectedOutput.SettingsNotSaved)
				{
					SaveSchedule();
					result = true;
				}
				else
					result = true;
				return result;
			}
		}

		private void SaveSchedule(string newName = "")
		{
			if (_selectedOutput != null)
			{
				if (!string.IsNullOrEmpty(newName))
					_selectedOutput.LocalSchedule.Name = newName;
				_selectedOutput.SettingsNotSaved = false;
				_selectedOutput.LocalSchedule.ViewSettings.SaveDefaultViewSettings(SettingsManager.Instance.ViewSettingsPath);
				Controller.Instance.SaveSchedule(_selectedOutput.LocalSchedule, true, _selectedOutput as Control);
				_selectedOutput.UpdateOutput(true);
			}
		}

		public void SelectSummary(SummaryType summaryType)
		{
			switch (summaryType)
			{
				case SummaryType.Overview:
					_selectedOutput = BasicOverview;
					HelpButtonItem = Controller.Instance.BasicOverviewHelp;
					break;
				case SummaryType.MultiSummary:
					_selectedOutput = MultiSummary;
					HelpButtonItem = Controller.Instance.MultiSummaryHelp;
					break;
				case SummaryType.Snapshot:
					_selectedOutput = Snapshot;
					HelpButtonItem = Controller.Instance.SnapshotHelp;
					break;
				default:
					_selectedOutput = null;
					break;
			}

			if (_selectedOutput != null)
			{
				if (!pnMain.Controls.Contains(_selectedOutput as Control))
				{
					Application.DoEvents();
					pnEmpty.BringToFront();
					Application.DoEvents();
					pnMain.Controls.Add(_selectedOutput as Control);
					Application.DoEvents();
					pnMain.BringToFront();
					Application.DoEvents();
				}
				(_selectedOutput as Control).BringToFront();
				Controller.Instance.Supertip.SetSuperTooltip(HelpButtonItem, _selectedOutput.HelpToolTip);
			}
			else
			{
				pnEmpty.BringToFront();
				Controller.Instance.Supertip.SetSuperTooltip(HelpButtonItem, null);
			}
		}

		public void ExternalOptionChanged(object sender, EventArgs e)
		{
			if (_selectedOutput == null) return;
			_selectedOutput.OnOptionChanged(sender, e);
		}

		public void Preview_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			_selectedOutput.Preview();
		}

		public void Digital_Click(object sender, EventArgs e)
		{
			_selectedOutput.EditDigitalLegend();
		}

		public void PowerPoint_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			_selectedOutput.PrintOutput();
		}

		public void Email_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			_selectedOutput.Email();
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
						SaveSchedule(from.ScheduleName);
						Utilities.Instance.ShowInformation("Schedule was saved");
					}
					else
					{
						Utilities.Instance.ShowWarning("Schedule Name can't be empty");
					}
				}
			}
		}

		public void Help_Click(object sender, EventArgs e)
		{
			_selectedOutput.OpenHelp();
		}
	}

	public enum SummaryType
	{
		Overview,
		MultiSummary,
		Snapshot,
	}

	public interface ISummaryOutputControl
	{
		Schedule LocalSchedule { get; set; }
		bool SettingsNotSaved { get; set; }
		SuperTooltipInfo HelpToolTip { get; }
		void UpdateOutput(bool quickLoad);
		void OnOptionChanged(object sender, EventArgs e);
		void EditDigitalLegend();
		void PrintOutput();
		void Email();
		void Preview();
		void OpenHelp();
	}
}