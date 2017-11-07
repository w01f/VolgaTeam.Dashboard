using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Output;
using DevComponents.DotNetBar;

namespace Asa.Common.GUI.OutputColors
{
	public partial class OutputColorSelector : UserControl
	{
		private const int ButtonHeight = 40;
		private const int TopPadding = 40;
		private const int LeftPadding = 20;
		private const int RightPadding = 20;

		private OutputColorList _colorList;

		public event EventHandler<EventArgs> ColorChanged;

		public string SelectedColor
		{
			get
			{
				return xtraScrollableControlColors.Controls
					.OfType<OutputColorButton>()
					.Where(b => b.Checked)
					.Select(b => ((ColorFolder)b.Tag).Name)
					.FirstOrDefault() ?? String.Empty;
			}
		}

		public OutputColorSelector()
		{
			InitializeComponent();
		}

		public void InitData(OutputColorList colorList, string savedColor)
		{
			ColorChanged = null;
			_colorList = colorList;
			if (!_colorList.Items.Any()) return;
			xtraScrollableControlColors.Controls.Clear();
			var selectedColor = (!String.IsNullOrEmpty(savedColor) ?
				_colorList.Items.FirstOrDefault(c => c.Name.ToLower().Equals(savedColor.ToLower())) :
				null)
				?? _colorList.Items.First();
			var columOrder = 0;
			var rowOrder = 0;
			foreach (var color in _colorList.Items)
			{
				var button = new OutputColorButton();
				button.ColumnOrder = columOrder;
				button.RowOrder = rowOrder;
				button.Text = color.Name;
				button.Height = (Int32)(ButtonHeight * Utilities.GetScaleFactor(CreateGraphics().DpiX).Height); 
				button.TextAlignment = eButtonTextAlignment.Center;
				button.ColorTable = eButtonColor.OrangeWithBackground;
				button.Style = eDotNetBarStyle.StyleManagerControlled;
				button.Tag = color;
				button.Checked = color.Name.Equals(selectedColor.Name);
				button.Click += (sender, e) =>
				{
					var clickedButton = (OutputColorButton)sender;
					if (clickedButton.Checked) return;
					foreach (var colorButton in xtraScrollableControlColors.Controls.OfType<OutputColorButton>())
						colorButton.Checked = false;
					clickedButton.Checked = true;
				};
				button.CheckedChanged += (sender, e) =>
				{
					var clickedButton = (OutputColorButton)sender;
					if (!clickedButton.Checked) return;
					ColorChanged?.Invoke(sender, e);
				};
				xtraScrollableControlColors.Controls.Add(button);
				if (columOrder > 0)
				{
					columOrder = 0;
					rowOrder++;
				}
				else
					columOrder++;
			}
			ResizeButtons();
		}

		private void ResizeButtons()
		{
			xtraScrollableControlColors.SuspendLayout();
			var buttonWidth = Width / 2 - LeftPadding - RightPadding;
			foreach (var colorButton in xtraScrollableControlColors.Controls
				.OfType<OutputColorButton>()
				.OrderBy(b => b.RowOrder)
				.ThenBy(b => b.ColumnOrder))
			{
				colorButton.Width = buttonWidth;
				var left = LeftPadding + colorButton.ColumnOrder * (buttonWidth + RightPadding + LeftPadding);
				var top = TopPadding / 2 + colorButton.RowOrder * ((Int32)(ButtonHeight * Utilities.GetScaleFactor(CreateGraphics().DpiX).Height) + TopPadding);
				colorButton.Location = new Point(left, top);
			}
			xtraScrollableControlColors.ResumeLayout();
		}

		private void OutputColorSelector_Resize(object sender, EventArgs e)
		{
			ResizeButtons();
		}
	}
}
