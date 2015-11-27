using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Asa.Core.MediaSchedule;
using DevComponents.DotNetBar;
using Padding = System.Windows.Forms.Padding;

namespace Asa.MediaSchedule.Controls.PresentationClasses.ScheduleControls
{
	public partial class QuarterSelectorControl : UserControl
	{
		private readonly List<ButtonX> _quarterButtons = new List<ButtonX>();

		public Quarter SelectedQuarter { get; private set; }
		public event EventHandler<EventArgs> QuarterSelected;

		public QuarterSelectorControl()
		{
			InitializeComponent();
		}

		public void InitControls(IEnumerable<Quarter> quarters, Quarter selectedQuarter)
		{
			SelectedQuarter = selectedQuarter;
			Controls.Clear();
			_quarterButtons.Clear();
			foreach (var quarter in quarters)
			{
				var panel = new Panel
				{
					Dock = DockStyle.Right,
					Width = 60,
					Padding = new Padding(5)
				};
				var button = new ButtonX
				{
					Dock = DockStyle.Fill,
					ColorTable = eButtonColor.OrangeWithBackground,
					Style = eDotNetBarStyle.StyleManagerControlled,
					Text = quarter.ToString(),
					Cursor = Cursors.Hand,
					Tag = quarter,
					Checked = quarter == SelectedQuarter
				};
				button.Click += OnQuarterClick;
				_quarterButtons.Add(button);
				panel.Controls.Add(button);
				Controls.Add(panel);
			}
		}

		private void OnQuarterClick(object sender, EventArgs e)
		{
			var button = (ButtonX)sender;
			if (button.Checked)
			{
				button.Checked = false;
				SelectedQuarter = null;
			}
			else
			{
				_quarterButtons.ForEach(b => b.Checked = false);
				button.Checked = true;
				SelectedQuarter = (Quarter)button.Tag;
			}
			if (QuarterSelected != null)
				QuarterSelected(this, EventArgs.Empty);
		}
	}
}
