using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using DevComponents.DotNetBar;
using Padding = System.Windows.Forms.Padding;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	public partial class QuarterSelectorControl : UserControl
	{
		private const Int32 ButtonWidth = 60;
		private const Int32 ButtonPadding = 5;

		private readonly List<ButtonX> _quarterButtons = new List<ButtonX>();

		public Quarter SelectedQuarter { get; private set; }
		public event EventHandler<EventArgs> QuarterSelected;

		public QuarterSelectorControl()
		{
			InitializeComponent();
		}

		public void InitControls(IEnumerable<Quarter> quarters, Quarter selectedQuarter)
		{
			Visible = quarters.Count() > 1;
			SelectedQuarter = selectedQuarter;
			Controls.Clear();
			_quarterButtons.Clear();
			foreach (var quarter in quarters)
			{
				var panel = new Panel
				{
					Dock = DockStyle.Right,
					Width = ButtonWidth,
					Padding = new Padding(ButtonPadding)
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
			Width = Controls.Count * (ButtonWidth + (ButtonPadding * 2));
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
