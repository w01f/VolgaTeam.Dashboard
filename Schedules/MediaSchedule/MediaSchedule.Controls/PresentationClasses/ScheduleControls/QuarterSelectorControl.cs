using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Asa.Core.MediaSchedule;

namespace Asa.MediaSchedule.Controls.PresentationClasses.ScheduleControls
{
	public partial class QuarterSelectorControl : UserControl
	{
		private readonly List<Label> _quarterLabels = new List<Label>();

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
			_quarterLabels.Clear();
			foreach (var quarter in quarters)
			{
				var label = new Label
				{
					Dock = DockStyle.Right,
					Width = 55,
					TextAlign = ContentAlignment.MiddleCenter,
					Text = quarter.ToString(),
					Cursor = Cursors.Hand,
					Tag = quarter
				};
				label.Click += OnQuarterSelect;
				if (quarter == SelectedQuarter)
					SelectQuarter(label);
				else
					UnelectQuarter(label);
				_quarterLabels.Add(label);
			}
			Controls.AddRange(_quarterLabels.ToArray());
		}

		private void OnQuarterSelect(object sender, EventArgs e)
		{
			var label = sender as Label;
			if (label == null) return;
			var quarter = label.Tag as Quarter;
			if (quarter == null) return;
			if (IsQuarterSelected(label)) return;
			foreach (var quarterLabel in _quarterLabels)
				UnelectQuarter(quarterLabel);
			SelectQuarter(label);
			SelectedQuarter = quarter;
			if (QuarterSelected != null)
				QuarterSelected(this, EventArgs.Empty);
		}

		private static void SelectQuarter(Label label)
		{
			label.ForeColor = Color.Black;
			label.Font = new Font(label.Font.Name, label.Font.Size, FontStyle.Bold | FontStyle.Underline);
		}

		private static void UnelectQuarter(Label label)
		{
			label.ForeColor = Color.FromArgb(4, 34, 196);
			label.Font = new Font(label.Font.Name, label.Font.Size, FontStyle.Regular);
		}

		private static bool IsQuarterSelected(Label label)
		{
			return label.ForeColor == Color.Black;
		}
	}
}
