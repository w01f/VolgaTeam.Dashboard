using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings
{
	public partial class QuarterSelectorControl : UserControl
	{
		private const Int32 ButtonWidth = 60;
		private const Int32 ButtonHeight = 30;
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
			SelectedQuarter = selectedQuarter;
			Controls.Clear();
			_quarterButtons.Clear();

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			foreach (var quarter in quarters)
			{
				var button = new ButtonX
				{
					ColorTable = eButtonColor.OrangeWithBackground,
					Style = eDotNetBarStyle.StyleManagerControlled,
					Text = quarter.ToString(),
					Cursor = Cursors.Hand,
					Tag = quarter,
					Checked = quarter == SelectedQuarter,
					Height = (Int32)(ButtonHeight * scaleFactor.Height),
					Width = (Int32)(ButtonWidth * scaleFactor.Width)
				};
				button.Click += OnQuarterClick;
				_quarterButtons.Add(button);
				Controls.Add(button);
			}
			ResizeButtons();
		}

		private void ResizeButtons()
		{
			var areaWidth = Width;

			var topPosition = ButtonPadding;
			var leftPosition = 0;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);

			foreach (var quarterButton in _quarterButtons)
			{
				quarterButton.Top = topPosition;
				quarterButton.Left = leftPosition;

				leftPosition += (Int32)(ButtonWidth * scaleFactor.Width) + ButtonPadding;
				if (leftPosition >= areaWidth)
				{
					leftPosition = 0;
					topPosition += ((Int32)(ButtonHeight * scaleFactor.Height) + ButtonPadding);
				}
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
			QuarterSelected?.Invoke(this, EventArgs.Empty);
		}

		private void QuarterSelectorControl_Resize(object sender, EventArgs e)
		{
			ResizeButtons();
		}
	}
}
